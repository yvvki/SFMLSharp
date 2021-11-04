using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace SFML.System
{
	/// <summary>
	///   Defines a generalized properties for manipulating 3-component vectors.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	public interface IVector3<T> : IVector2<T>, IEquatable<IVector3<T>>, IFormattable
		where T : IEquatable<T>, IFormattable
	{
		#region Properties

		/// <summary>
		///   The Z component of the vector.
		/// </summary>
		T Z { get; set; }

		#endregion

		#region Explicit Method Implementations

		bool IEquatable<IVector3<T>>.Equals([NotNullWhen(true)] IVector3<T>? other)
		{
			return other is not null && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
		}

		#endregion
	}

	/// <summary>
	///   Defines a read-only generalized properties for 3-component vectors.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	public interface IReadOnlyVector3<out T> : IReadOnlyVector2<T>
	{
		#region Properties

		/// <inheritdoc cref="IVector3{T}.Z"/>
		T Z { get; }

		#endregion
	}

	/// <summary>
	///   Represents a generalized struct for manipulating 3-component vectors.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	[RequiresPreviewFeatures]
	public partial struct Vector3<T> :
		IVector3<T>,
		IReadOnlyVector3<T>,
		IEquatable<Vector3<T>>,
		IFormattable,
		IVectorOperators<Vector3<T>, T>,
		IMultiplyOperators<Vector3<T>, T, Vector3<T>>,
		IDivisionOperators<Vector3<T>, T, Vector3<T>>
		where T : INumber<T>
	{
		#region Fields & Properties

		/// <inheritdoc cref="IVector2{T}.X"/>
		public T X;
		/// <inheritdoc cref="IVector2{T}.Y"/>
		public T Y;
		/// <inheritdoc cref="IVector3{T}.Z"/>
		public T Z;

		T IVector2<T>.X { get => X; set => X = value; }
		T IVector2<T>.Y { get => Y; set => Y = value; }
		T IVector3<T>.Z { get => Z; set => Z = value; }

		T IReadOnlyVector2<T>.X => X;
		T IReadOnlyVector2<T>.Y => Y;
		T IReadOnlyVector3<T>.Z => Z;

		#endregion

		#region Static Properties

		public static Vector3<T> Zero => new(T.Zero);
		public static Vector3<T> UnitX => new(T.One, T.Zero, T.Zero);
		public static Vector3<T> UnitY => new(T.Zero, T.One, T.Zero);
		public static Vector3<T> UnitZ => new(T.Zero, T.Zero, T.One);
		public static Vector3<T> One => new(T.One);

		public static Vector3<T> AdditiveIdentity => new(T.AdditiveIdentity);
		public static Vector3<T> MultiplicativeIdentity => new(T.MultiplicativeIdentity);

		#endregion

		#region Constructors

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector3(T value) : this(value, value, value) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector3(IReadOnlyVector3<T> value) : this(value.X, value.Y, value.Z) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3From2"]'/>
		public Vector3(IReadOnlyVector2<T> value, T z) : this(value.X, value.Y, z) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3With2"]'/>
		public Vector3(T x, IReadOnlyVector2<T> value) : this(x, value.X, value.Y) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3"]'/>
		public Vector3(T x, T y, T z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		#endregion

		#region Static Methods

		///// <inheritdoc cref="operator -(Vector2{T})"/>
		//public static Vector3<T> Negate(Vector3<T> value)
		//{
		//	return -value;
		//}

		///// <inheritdoc cref="operator +(Vector3{T}, Vector3{T})"/>
		//public static Vector3<T> Add(Vector3<T> left, Vector3<T> right)
		//{
		//	return left + right;
		//}

		///// <inheritdoc cref="operator -(Vector3{T}, Vector3{T})"/>
		//public static Vector3<T> Subtract(Vector3<T> left, Vector3<T> right)
		//{
		//	return left - right;
		//}

		///// <inheritdoc cref="operator *(Vector3{T}, Vector3{T})"/>
		//public static Vector3<T> Multiply(Vector3<T> left, Vector3<T> right)
		//{
		//	return left * right;
		//}

		///// <inheritdoc cref="operator /(Vector3{T}, Vector3{T})"/>
		//public static Vector3<T> Divide(Vector3<T> left, Vector3<T> right)
		//{
		//	return left / right;
		//}

		#endregion

		#region Interface Method Implementations

		public bool Equals(Vector3<T> other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Vector3<T> other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y, Z);
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			return this.Format(format, formatProvider);
		}

		#endregion

		#region Operators

		public static bool operator ==(Vector3<T> left, Vector3<T> right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		public static bool operator !=(Vector3<T> left, Vector3<T> right)
		{
			return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		public static Vector3<T> operator -(Vector3<T> value)
		{
			return new(-value.X, -value.Y, -value.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		public static Vector3<T> operator +(Vector3<T> left, Vector3<T> right)
		{
			return new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		public static Vector3<T> operator -(Vector3<T> left, Vector3<T> right)
		{
			return new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector3<T> operator *(Vector3<T> left, Vector3<T> right)
		{
			return new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector3<T> operator *(Vector3<T> left, T right)
		{
			return new(left.X * right, left.Y * right, left.Z * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector3<T> operator *(T left, Vector3<T> right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector3<T> operator /(Vector3<T> left, Vector3<T> right)
		{
			return new(left.X / right.X, left.Y / right.Y, left.Y / right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector3<T> operator /(Vector3<T> left, T right)
		{
			return new(left.X / right, left.Y / right, left.Z / right);
		}

		#endregion

		#region Cast Operators

		public static implicit operator (T, T, T)(Vector3<T> value)
		{
			return (value.X, value.Y, value.Z);
		}
		public static implicit operator Vector3<T>((T x, T y, T z) value)
		{
			return new(value.x, value.y, value.z);
		}

		#endregion
	}
}
