﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crosspascal.ast.nodes
{

	//==========================================================================
	// Types' base classes
	//==========================================================================

	#region Type hierarchy
	/// <remarks>
	/// Types
	/// 	DeclaredType => may be any, user-defined
	/// 	UndefinedType	> for untyped parameters. incompatible with any type >
	/// 	ProceduralType
	/// 	ClassType
	/// 	VariableType
	/// 		ScalarType
	/// 			SimpleType		: IOrdinalType
	/// 				IntegerType
	/// 					UnsignedInt	...
	/// 					SignedInt	...
	/// 				Bool
	/// 				Char
	/// 			RealType
	/// 				FloatType
	/// 				DoubleType
	/// 				ExtendedType
	/// 				CurrencyType
	/// 			StringType
	/// 			VariantType
	/// 			PointerType > ScalarType
	/// 		EnumType			: IOrdinalType
	/// 		RangeType			: IOrdinalType
	/// 		MetaclassType > id
	/// 		StructuredType
	/// 			Array > VariableType 
	/// 			Set	  > VariableType 
	/// 			File  > VariableType
	/// 		Record
	/// </remarks>
	#endregion


	public abstract class TypeNode : Node
	{

		public override bool Equals(Object o)
		{
			if (o == null)
				return false;

			return (this.GetType() == o.GetType());
		}

		// TODO
	//	public abstract bool ISA(TypeNode o);
	}

	/// <summary>
	/// Undefined, incompatible type. For untyped parameters
	/// An expression with this type must be cast to some defined type before using
	/// </summary>
	public class UndefinedType: TypeNode
	{
		public static readonly UndefinedType Single = new UndefinedType();

	}

	/// <summary>
	/// Custom/User-defined type
	/// </summary>
	public class DeclaredType : TypeNode
	{
		public String name;

		public DeclaredType(String name)
		{
			this.name = name;
		}

		public override bool Equals(Object o)
		{
			if (o == null)
				return false;

			DeclaredType type = (DeclaredType) o;
			return (name == type.name);
		}
	}


	public class RecordType : VariableType
	{
		FieldList compTypes;

		public RecordType(FieldList compTypes)
		{
			this.compTypes = compTypes;
		}

		public override bool Equals(Object o)
		{
			if (o == null || this.GetType() != o.GetType())
				return false;

			RecordType rtype = (RecordType)o;
			var types = compTypes.Zip(rtype.compTypes,
				(x, y) =>
				{
					return new KeyValuePair<TypeNode, TypeNode>(x.type, y.type);
				});

			foreach (var x in types)
				if (!x.Key.Equals(x.Value))
					return false;

			//	var list = new List<KeyValuePair<IOrdinalType,IOrdinalType>>(types);
			//	list.ForEach( (x) => {	if (!x.Key.Equals(x.Value.Equals)) return false; });
			return true;
		}
	}


	/// <summary>
	/// Type of a Routine (function, procedure, method, etc)
	/// </summary>
	public partial class ProceduralType : TypeNode
	{
		// In file Routines.cs
	}

	public class ClassType : DeclaredType
	{
		public ClassType(String name) : base(name) { }
	}

	public abstract class VariableType : TypeNode
	{
		public int typeSize;

	}

	public class MetaclassType : TypeNode
	{
		public TypeNode baseType;

		public MetaclassType(TypeNode baseType)
		{
			this.baseType = baseType;
		}

		public override bool Equals(Object o)
		{
			if (o == null || this.GetType() != o.GetType())
				return false;

			MetaclassType rtype = (MetaclassType) o;
			return (this.Equals(rtype));
		}
	}


	public class TypeUnknown : TypeNode
	{
		public static readonly UndefinedType Single = new UndefinedType();

	}


	#region Ordinal Types

	public interface IOrdinalType
	{
		Object MinValue();

		Object MaxValue();

		UInt64 ValueRange();
	}


	public class EnumType : VariableType, IOrdinalType
	{
		public EnumValueList enumVals;

		public Object MinValue()	{ return enumVals.GetFirst().init.Value; }

		public Object MaxValue()	{ return enumVals.GetLast().init.Value; }

		public UInt64 ValueRange() {
			// TODO
			// return enumVals.GetLast().init.constantValue - enumVals.GetFirst().init.constantValue; 
			return 0;
		}

		public override bool Equals(Object o)
		{
			if (o == null || this.GetType() != o.GetType())
				return false;

			EnumType rtype = (EnumType)o;
			var types = enumVals.Zip(rtype.enumVals,
				(x, y) => {
					return new KeyValuePair<TypeNode, TypeNode>(x.type, y.type);
				});

			foreach (var x in types)
				if (!x.Key.Equals(x.Value))
					return false;

			return true;
		}

		public EnumType(EnumValueList enumVals)
		{
			this.enumVals = enumVals;
		}

		/// <summary>
		/// Determine and assign the Enum initializers, from the user-defined to the automatic
		/// </summary>
		public void AssignEnumInitializers()
		{
			int val = 0;	// default start val

			foreach (EnumValue al in enumVals)
			{
				if (al.init == null)
					al.init = new IntLiteral(val);
				else
				{
					if (al.init.Value.Type.GetType().IsSubclassOf(typeof(IntegerType)))
						val = (int)al.init.Value.value;
					else
						Error("Enum initializer must be an integer");
				}
				val++;
			}
		}
	}

	public class RangeType : VariableType, IOrdinalType
	{
		Literal low;
		Literal high;

		public Object MinValue() { return low.value; }

		public Object MaxValue() { return high.value; }

		public UInt64 ValueRange() { return 0; } // high.value - low.value; }

		public override bool Equals(Object o)
		{
			if (o == null || this.GetType() != o.GetType())
				return false;

			RangeType r = (RangeType)o;
			return (this.Equals(r) && low == r.low && high == r.high);
		}

		public RangeType(Literal low, Literal high)
		{
			this.low  = low;
			this.high = high;
		}
	}

	#endregion
	

	#region Scalar Types

	///	==========================================================================
	/// Scalar Types
	///	==========================================================================
	/// <remarks>
	///	ScalarType
	///		DiscreteType		: IOrdinalType
	///			IntegerType
	///				UnsignedInt	...
	///				SignedInt	...
	///			Bool
	///			Char
	///		RealType
	///			FloatType
	///			DoubleType
	///			ExtendedType
	///			CurrencyType
	///		StringType
	///		VariantType
	///		PointerType <!-- ScalarType--> 
	/// </remarks>

	public abstract class ScalarType : VariableType
	{

		/// <summary>
		/// Default Scalar Type comparison: directly compare references to singleton objects
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public override bool Equals(Object o)
		{
			if (o == null)
				return false;

			return (this.GetType() == o.GetType());
		}
	}

	public class StringType : ScalarType
	{
		public static readonly StringType Single = new StringType();
	}

	public class VariantType : ScalarType
	{
		ScalarType type;

		/// <summary>
		/// Actual type cannot be initially known.
		/// </summary>
		public VariantType() { }

		public override bool Equals(Object o)
		{
			// TODO
			return true;
		}
	}

	public class PointerType : ScalarType
	{
		ScalarType pointedType;

		public PointerType(ScalarType pointedType)
		{
			this.pointedType = pointedType;
		}

		public override bool Equals(Object o)
		{
			if (o == null || this.GetType() != o.GetType())
				return false;

			PointerType otype = (PointerType) o;
			return pointedType.Equals(otype.pointedType);
		}
	}


	#region Integral Types

	public class IntegralType : ScalarType, IOrdinalType
	{
		public static readonly IntegralType Single = new IntegralType();

		public override bool Equals(Object o)
		{
			if (o == null)
				return false;

			return (this.GetType() == o.GetType());
		}

		// Should not be used
		public virtual Object MinValue() { return 0; }
		public virtual Object MaxValue() { return 0; }
		public virtual UInt64 ValueRange() { return 0; }

		protected IntegralType() { }
	}
	

	#region Integer Types

	public class IntegerType : IntegralType
	{
		public static new readonly IntegerType Single = new IntegerType();

		// Should not be used
		public override Object MinValue() { return 0; }
		public override Object MaxValue() { return 0; }
		public override UInt64 ValueRange() { return 0; }
	}

	public class SignedIntegerType : IntegerType
	{
		public static new readonly SignedIntegerType Single = new SignedIntegerType();

		// Should not be used
		public override Object MinValue() { return 0; }
		public override Object MaxValue() { return 0; }
		public override UInt64 ValueRange() { return 0; }
	}

	public class UnsignedIntegerType : IntegerType
	{
		public static new readonly UnsignedIntegerType Single = new UnsignedIntegerType();

		// Should not be used
		public override Object MinValue() { return 0; }
		public override Object MaxValue() { return 0; }
		public override UInt64 ValueRange() { return 0; }
	}

	public class UnsignedInt8Type : UnsignedIntegerType // byte
	{
		public static new readonly UnsignedInt8Type Single = new UnsignedInt8Type();

		public override Object MinValue() { return Byte.MinValue; }

		public override Object MaxValue() { return Byte.MaxValue; }

		public override UInt64 ValueRange() { return Byte.MaxValue - Byte.MinValue; }
	}

	public class UnsignedInt16Type : UnsignedIntegerType // word
	{
		public static new readonly UnsignedInt16Type Single = new UnsignedInt16Type();

		public override Object MinValue() { return UInt16.MinValue; }

		public override Object MaxValue() { return UInt16.MaxValue; }

		public override UInt64 ValueRange() { return UInt16.MaxValue - UInt16.MinValue; }
	}

	public class UnsignedInt32Type : UnsignedIntegerType		// cardinal
	{
		public new static readonly UnsignedInt32Type Single = new UnsignedInt32Type();

		public override Object MinValue() { return UInt32.MinValue; }

		public override Object MaxValue() { return UInt32.MaxValue; }

		public override UInt64 ValueRange() { return UInt32.MaxValue - UInt32.MinValue; }
	}

	public class UnsignedInt64Type : UnsignedIntegerType	 // uint64
	{
		public static new readonly UnsignedInt64Type Single = new UnsignedInt64Type();

		public override Object MinValue() { return UInt64.MinValue; }

		public override Object MaxValue() { return UInt64.MaxValue; }

		public override UInt64 ValueRange() { return UInt64.MaxValue - UInt64.MinValue; }
	}

	public class SignedInt8Type : SignedIntegerType		// smallint
	{
		public static new readonly SignedInt8Type Single = new SignedInt8Type();

		public override Object MinValue() { return SByte.MinValue; }

		public override Object MaxValue() { return SByte.MaxValue; }

		public override UInt64 ValueRange() { return sbyte.MaxValue - (int)sbyte.MinValue; }
	}

	public class SignedInt16Type : SignedIntegerType	 // smallint
	{
		public static new readonly SignedInt16Type Single = new SignedInt16Type();

		public override Object MinValue() { return Int16.MinValue; }

		public override Object MaxValue() { return Int16.MaxValue; }

		public override UInt64 ValueRange() { return short.MaxValue - (int)short.MinValue; }
	}

	public class SignedInt32Type : SignedIntegerType	// integer
	{
		public static new readonly SignedInt32Type Single = new SignedInt32Type();

		public override Object MinValue() { return Int32.MinValue; }

		public override Object MaxValue() { return Int32.MaxValue; }

		public override UInt64 ValueRange() { return int.MaxValue - (long) int.MinValue; }
	}

	public class SignedInt64Type : IntegerType // int64
	{
		public static new readonly SignedInt64Type Single = new SignedInt64Type();

		public override Object MinValue() { return Int64.MinValue; }

		public override Object MaxValue() { return Int64.MaxValue; }

		public override UInt64 ValueRange() { return Int64.MaxValue; }
	}

	#endregion


	public class BoolType : IntegralType
	{
		public static new readonly BoolType Single = new BoolType();

		public override Object MinValue() { return false; }

		public override Object MaxValue() { return true; }

		public override UInt64 ValueRange() { return 2; }
	}

	public class CharType : IntegralType
	{
		public static new readonly CharType Single = new CharType();

		public override Object MinValue() { return Char.MinValue; }

		public override Object MaxValue() { return Char.MaxValue; }

		public override UInt64 ValueRange() { return Char.MaxValue - Char.MinValue; }
	}


	#endregion


	#region Floating-Point Types

	public class RealType : ScalarType
	{
		public static readonly RealType Single = new RealType();

		public override bool Equals(Object o)
		{
			if (o == null)
				return false;

			return (this.GetType() == o.GetType());
		}
	}

	public class FloatType : RealType
	{
		public static new readonly FloatType Single = new FloatType();
	}

	public class DoubleType : RealType
	{
		public static new readonly DoubleType Single = new DoubleType();
	}

	public class ExtendedType : RealType
	{
		public static new readonly ExtendedType Single = new ExtendedType();
	}

	public class CurrencyType : RealType
	{
		public static new readonly CurrencyType Single = new CurrencyType();
	}

	#endregion

	/// ==========================================================================
	/// ==========================================================================

	#endregion		// Scalar types


	#region Structured Types
	///	==========================================================================
	/// Structured Types
	///	==========================================================================
	///		StructuredType
	///			Array < VariableType> 
	///			Set	  < VariableType> 
	///			File  < VariableType> 

	public abstract class StructuredType<T> : VariableType where T : VariableType
	{
		public T basetype;
		bool IsPacked { get; set; }

		protected StructuredType(T t)
		{
			basetype = t;
		}

		public override bool Equals(Object o)
		{
			if (o == null || this.GetType() != o.GetType())
				return false;

			StructuredType<T> otype = (StructuredType<T>)o;
			return basetype.Equals(otype.basetype);
		}
	}

	public class ArrayType<T> : StructuredType<T> where T : VariableType
	{
		public List<int> dimensions = new List<int>();

		void AddDimension(uint size)
		{
			if (size * basetype.typeSize > (1<<9))	// 1gb max size
				Error("Array size too large: " + size);

			dimensions.Add((int)size);
		}

		public ArrayType(T type) : base(type)
		{
			// dynamic array
		}

		public ArrayType(T type, NodeList dims) : base(type)
		{
			// TODO Check constant and compute value
			foreach (Node n in dims)
			{
				SetRange range = (SetRange)n;
			//	int dim = range.max - range.min;
			//	AddDimension(dim);
			}
		}

		public ArrayType(T type, String ordinalTypeId) : base(type)
		{
			// TODO Resolve and check type size
		}

		public ArrayType(T type, IntegralType sizeType): base(type)
		{
			UInt64 size = sizeType.ValueRange();
			if (size > Int32.MaxValue) size = Int32.MaxValue;
			AddDimension((uint) size);
		}

		public override bool Equals(Object o)
		{
			if (o == null || this.GetType() != o.GetType())
				return false;

			ArrayType<T> otype = (ArrayType<T>) o;
			if (!dimensions.Equals(otype.dimensions))
				return false;

			return basetype.Equals(otype.basetype);
		}
	}

	public class SetType<T> : StructuredType<T> where T : VariableType
	{
		public SetType(T type) : base(type) { }
	}

	public class FileType<T> : StructuredType<T> where T : VariableType
	{
		public FileType(T type = null) : base(type) { }
	}

	#endregion

}
