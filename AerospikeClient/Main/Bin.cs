/*
 * Aerospike Client - C# Library
 *
 * Copyright 2013 by Aerospike, Inc. All rights reserved.
 *
 * Availability of this source code to partners and customers includes
 * redistribution rights covered by individual contract. Please check your
 * contract for exact rights and responsibilities.
 */
using System.Collections.Generic;

namespace Aerospike.Client
{
	/// <summary>
	/// Column name/value pair. 
	/// </summary>
	public sealed class Bin
	{
		/// <summary>
		/// Bin name. Current limit is 14 characters.
		/// </summary>
		public readonly string name;

		/// <summary>
		/// Bin value.
		/// </summary>
		public readonly Value value;

		/// <summary>
		/// Constructor, specifying bin name and value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, Value value)
		{
			this.name = name;
			this.value = value;
		}

		/// <summary>
		/// Constructor, specifying bin name and string value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, string value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and byte array value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, byte[] value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and long value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, long value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and unsigned long value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, ulong value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and integer value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, int value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and unsigned integer value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, uint value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and short value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, short value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and unsigned short value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, ushort value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and boolean value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, bool value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and byte value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, byte value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and signed byte value.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, sbyte value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Constructor, specifying bin name and object value.
		/// This is the slowest of the Bin constructors because the type
		/// must be determined using multiple "instanceof" checks.
		/// <para>
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </para>
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public Bin(string name, object value)
		{
			this.name = name;
			this.value = Value.Get(value);
		}

		/// <summary>
		/// Create bin with a list value.  The list value will be serialized as a Aerospike 3 server list type.
		/// Supported by Aerospike 3 servers only.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public static Bin AsList(string name, List<object> value)
		{
			return new Bin(name, Value.GetAsList(value));
		}

		/// <summary>
		/// Create bin with a map value.  The map value will be serialized as a Aerospike 3 server map type.
		/// Supported by Aerospike 3 servers only.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public static Bin AsMap(string name, Dictionary<object,object> value)
		{
			return new Bin(name, Value.GetAsMap(value));
		}

		/// <summary>
		/// Create bin with a blob value.  The value will be java serialized.
		/// This method is faster than the bin Object constructor because the blob is converted 
		/// directly instead of using multiple "instanceof" type checks with a blob default.
		/// <para>
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </para>
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		/// <param name="value">bin value</param>
		public static Bin AsBlob(string name, object value)
		{
			return new Bin(name, Value.GetAsBlob(value));
		}

		/// <summary>
		/// Create bin with a null value. This is useful for bin deletions within a record.
		/// For servers configured as "single-bin", enter a null or empty name.
		/// </summary>
		/// <param name="name">bin name, current limit is 14 characters</param>
		public static Bin AsNull(string name)
		{
			return new Bin(name, Value.AsNull);
		}

		/// <summary>
		/// Return string representation of bin.
		/// </summary>
		public override string ToString()
		{
			return name + ':' + value;
		}
	}
}