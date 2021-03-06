/* 
 * Copyright 2012-2015 Aerospike, Inc.
 *
 * Portions may be licensed to Aerospike, Inc. under one or more contributor
 * license agreements.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */
using System;
using System.Threading;
using System.Collections.Concurrent;

namespace Aerospike.Client
{
	public sealed class QueryAggregateCommand : QueryCommand
	{
		private readonly BlockingCollection<object> inputQueue;
		private readonly CancellationToken cancelToken;

		public QueryAggregateCommand
		(
			Node node,
			Policy policy,
			Statement statement,
			BlockingCollection<object> inputQueue,
			CancellationToken cancelToken
		) : base(node, policy, statement)
		{
			this.inputQueue = inputQueue;
			this.cancelToken = cancelToken;
		}

		protected internal override bool ParseRecordResults(int receiveSize)
		{
			// Read/parse remaining message bytes one record at a time.
			dataOffset = 0;

			while (dataOffset < receiveSize)
			{
				ReadBytes(MSG_REMAINING_HEADER_SIZE);
				int resultCode = dataBuffer[5];

				if (resultCode != 0)
				{
					if (resultCode == ResultCode.KEY_NOT_FOUND_ERROR)
					{
						return false;
					}
					throw new AerospikeException(resultCode);
				}

				byte info3 = dataBuffer[3];

				// If this is the end marker of the response, do not proceed further
				if ((info3 & Command.INFO3_LAST) == Command.INFO3_LAST)
				{
					return false;
				}

				int fieldCount = ByteUtil.BytesToShort(dataBuffer, 18);
				int opCount = ByteUtil.BytesToShort(dataBuffer, 20);

				ParseKey(fieldCount);

				if (opCount != 1)
				{
					throw new AerospikeException("Query aggregate expected exactly one bin.  Received " + opCount);
				}

				// Parse aggregateValue.
				ReadBytes(8);
				int opSize = ByteUtil.BytesToInt(dataBuffer, 0);
				byte particleType = dataBuffer[5];
				byte nameSize = dataBuffer[7];

				ReadBytes(nameSize);
				string name = ByteUtil.Utf8ToString(dataBuffer, 0, nameSize);

				int particleBytesSize = (int)(opSize - (4 + nameSize));
				ReadBytes(particleBytesSize);

				if (! name.Equals("SUCCESS"))
				{
					if (name.Equals("FAILURE"))
					{
						object value = ByteUtil.BytesToParticle(particleType, dataBuffer, 0, particleBytesSize);
						throw new AerospikeException(ResultCode.QUERY_GENERIC, value.ToString());
					}
					else
					{
						throw new AerospikeException(ResultCode.QUERY_GENERIC, "Query aggregate expected bin name SUCCESS.  Received " + name);
					}
				}

				object aggregateValue = LuaInstance.BytesToLua(particleType, dataBuffer, 0, particleBytesSize);

				if (!valid)
				{
					throw new AerospikeException.QueryTerminated();
				}

				if (aggregateValue != null)
				{
					try
					{
						inputQueue.Add(aggregateValue, cancelToken);
					}
					catch (OperationCanceledException)
					{
					}
				}
			}
			return true;
		}
	}
}
