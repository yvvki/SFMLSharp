using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace SFML.System
{
	/// <summary>
	///   Defines a generalized properties for manipulating 2-component vectors.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	public interface IVector2<T> : IEquatable<IVector2<T>>, IFormattable
		where T : IEquatable<T>, IFormattable
	{
		#region Properties

		/// <summary>
		///   The X component of the vector.
		/// </summary>
		T X { get; set; }
		/// <summary>
		///   The Y component of the vector.
		/// </summary>
		T Y { get; set; }

		#endregion

		#region Explicit Method Implementations

		bool IEquatable<IVector2<T>>.Equals([NotNullWhen(true)] IVector2<T>? other)
		{
			return other is not null && X.Equals(other.X) && Y.Equals(other.Y);
		}

		#endregion
	}

	/// <summary>
	///   Defines a read-only generalized properties for 2-component vectors.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	public interface IReadOnlyVector2<out T>
	{
		#region Properties

		/// <inheritdoc cref="IVector2{T}.X"/>
		T X { get; }
		/// <inheritdoc cref="IVector2{T}.Y"/>
		T Y { get; }

		#endregion
	}

	/// <summary>
	///   Represents a generalized struct for manipulating 2-component vectors.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	[RequiresPreviewFeatures]
	public partial struct Vector2<T> :
		IVector2<T>,
		IReadOnlyVector2<T>,
		IEquatable<Vector2<T>>,
		IFormattable,
		IVectorOperators<Vector2<T>, T>,
		IMultiplyOperators<Vector2<T>, T, Vector2<T>>,
		IDivisionOperators<Vector2<T>, T, Vector2<T>>
		where T : INumber<T>
	{
		#region Fields & Properties

		/// <inheritdoc cref="IVector2{T}.X"/>
		public T X;
		/// <inheritdoc cref="IVector2{T}.Y"/>
		public T Y;

		T IVector2<T>.X { get => X; set => X = value; }
		T IVector2<T>.Y { get => Y; set => Y = value; }

		T IReadOnlyVector2<T>.X => X;
		T IReadOnlyVector2<T>.Y => Y;

		#endregion

		#region Static Properties

		public static Vector2<T> Zero => new(T.Zero);
		public static Vector2<T> UnitX => new(T.One, T.Zero);
		public static Vector2<T> UnitY => new(T.Zero, T.One);
		public static Vector2<T> One => new(T.One);

		public static Vector2<T> AdditiveIdentity => new(T.AdditiveIdentity);
		public static Vector2<T> MultiplicativeIdentity => new(T.MultiplicativeIdentity);

		#endregion

		#region Constructors

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2(T value) : this(value, value) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2(IReadOnlyVector2<T> value) : this(value.X, value.Y) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs2"]'/>
		public Vector2(T x, T y)
		{
			X = x;
			Y = y;
		}

		#endregion

		#region Static Methods

		///// <inheritdoc cref="operator -(Vector2{T})"/>
		//public static Vector2<T> Negate(Vector2<T> value)
		//{
		//	return -value;
		//}

		///// <inheritdoc cref="operator +(Vector2{T}, Vector2{T})"/>
		//public static Vector2<T> Add(Vector2<T> left, Vector2<T> right)
		//{
		//	return left + right;
		//}

		///// <inheritdoc cref="operator -(Vector2{T}, Vector2{T})"/>
		//public static Vector2<T> Subtract(Vector2<T> left, Vector2<T> right)
		//{
		//	return left - right;
		//}

		///// <inheritdoc cref="operator *(Vector2{T}, Vector2{T})"/>
		//public static Vector2<T> Multiply(Vector2<T> left, Vector2<T> right)
		//{
		//	return left * right;
		//}

		///// <inheritdoc cref="operator /(Vector2{T}, Vector2{T})"/>
		//public static Vector2<T> Divide(Vector2<T> left, Vector2<T> right)
		//{
		//	return left / right;
		//}

		#endregion

		#region Interface Method Implementations

		public bool Equals(Vector2<T> other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Vector2<T> other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			return this.Format(format, formatProvider);
		}

		#endregion

		#region Operators

		public static bool operator ==(Vector2<T> left, Vector2<T> right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(Vector2<T> left, Vector2<T> right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		public static Vector2<T> operator -(Vector2<T> value)
		{
			return new(-value.X, -value.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		public static Vector2<T> operator +(Vector2<T> left, Vector2<T> right)
		{
			return new(left.X + right.X, left.Y + right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		public static Vector2<T> operator -(Vector2<T> left, Vector2<T> right)
		{
			return new(left.X - right.X, left.Y - right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2<T> operator *(Vector2<T> left, Vector2<T> right)
		{
			return new(left.X * right.X, left.Y * right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2<T> operator *(Vector2<T> left, T right)
		{
			return new(left.X * right, left.Y * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2<T> operator *(T left, Vector2<T> right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2<T> operator /(Vector2<T> left, Vector2<T> right)
		{
			return new(left.X / right.X, left.Y / right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2<T> operator /(Vector2<T> left, T right)
		{
			return new(left.X / right, left.Y / right);
		}

		#endregion

		#region Cast Operators

		public static implicit operator (T, T)(Vector2<T> value)
		{
			return (value.X, value.Y);
		}
		public static implicit operator Vector2<T>((T x, T y) value)
		{
			return new(value.x, value.y);
		}

		#endregion
	}
}
