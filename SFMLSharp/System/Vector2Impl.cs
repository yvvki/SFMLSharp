using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using System.Runtime.Versioning;

namespace SFML.System
{
	/// <summary>
	///   Represents a 2-component vectors of <see cref="float" />.
	/// </summary>
	public partial struct Vector2F :
		IVector2<float>,
		IReadOnlyVector2<float>,
		IEquatable<Vector2F>,
		IFormattable
	{
		#region Fields & Properties

		/// <inheritdoc cref="IVector2{T}.X"/>
		public float X;
		/// <inheritdoc cref="IVector2{T}.Y"/>
		public float Y;

		float IVector2<float>.X { get => X; set => X = value; }
		float IVector2<float>.Y { get => Y; set => Y = value; }

		float IReadOnlyVector2<float>.X => X;
		float IReadOnlyVector2<float>.Y => Y;

		#endregion

		#region Static Properties

		public static Vector2F Zero => default;
		public static Vector2F UnitX => new(1, 0);
		public static Vector2F UnitY => new(0, 1);
		public static Vector2F One => new(1);

		#endregion

		#region Constructors

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2F(float value) : this(value, value) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2F(IReadOnlyVector2<float> value) : this(value.X, value.Y) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs2"]'/>
		public Vector2F(float x, float y)
		{
			X = x;
			Y = y;
		}

		#endregion

		#region Static Methods

		///// <inheritdoc cref="operator -(Vector2F)"/>
		//public static Vector2F Negate(Vector2F value)
		//{
		//	return -value;
		//}

		///// <inheritdoc cref="operator +(Vector2F, Vector2F)"/>
		//public static Vector2F Add(Vector2F left, Vector2F right)
		//{
		//	return left + right;
		//}

		///// <inheritdoc cref="operator -(Vector2F, Vector2F)"/>
		//public static Vector2F Subtract(Vector2F left, Vector2F right)
		//{
		//	return left - right;
		//}

		///// <inheritdoc cref="operator *(Vector2F, Vector2F)"/>
		//public static Vector2F Multiply(Vector2F left, Vector2F right)
		//{
		//	return left * right;
		//}

		///// <inheritdoc cref="operator /(Vector2F, Vector2F)"/>
		//public static Vector2F Divide(Vector2F left, Vector2F right)
		//{
		//	return left / right;
		//}

		#endregion

		#region Interface Implementations

		public bool Equals(Vector2F other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Vector2F other && Equals(other);
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

		public static bool operator ==(Vector2F left, Vector2F right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(Vector2F left, Vector2F right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		public static Vector2F operator -(Vector2F value)
		{
			return new(-value.X, -value.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		public static Vector2F operator +(Vector2F left, Vector2F right)
		{
			return new(
				left.X + right.X,
				left.Y + right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		public static Vector2F operator -(Vector2F left, Vector2F right)
		{
			return new(
				left.X - right.X,
				left.Y - right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2F operator *(Vector2F left, Vector2F right)
		{
			return new(
				left.X * right.X,
				left.Y * right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2F operator *(Vector2F left, float right)
		{
			return new(
				left.X * right,
				left.Y * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2F operator *(float left, Vector2F right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2F operator /(Vector2F left, Vector2F right)
		{
			return new(
				left.X / right.X,
				left.Y / right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2F operator /(Vector2F left, float right)
		{
			return new(
				left.X / right,
				left.Y / right);
		}

		#endregion

		#region Cast Operators

		public static implicit operator (float, float)(Vector2F value)
		{
			return (value.X, value.Y);
		}
		public static implicit operator Vector2F((float x, float y) value)
		{
			return new(value.x, value.y);
		}

		[RequiresPreviewFeatures]
		public static implicit operator Vector2<float>(Vector2F value)
		{
			return new(value.X, value.Y);
		}
		[RequiresPreviewFeatures]
		public static implicit operator Vector2F(Vector2<float> value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator Vector2F(Vector2I value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2F(Vector2U value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator Vector2(Vector2F value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2F(Vector2 value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator PointF(Vector2F value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2F(PointF value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator SizeF(Vector2F value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2F(SizeF value)
		{
			return new(value.Width, value.Height);
		}

		public static explicit operator Point(Vector2F value)
		{
			return new((int)value.X, (int)value.Y);
		}
		public static explicit operator Vector2F(Point value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator Size(Vector2F value)
		{
			return new((int)value.X, (int)value.Y);
		}
		public static explicit operator Vector2F(Size value)
		{
			return new(value.Width, value.Height);
		}

		#endregion
	}

	/// <summary>
	///   Represents a 2-component vectors of <see cref="int" />.
	/// </summary>
	public partial struct Vector2I :
		IVector2<int>,
		IReadOnlyVector2<int>,
		IEquatable<Vector2I>,
		IFormattable
	{
		#region Fields & Properties

		/// <inheritdoc cref="IVector2{T}.X"/>
		public int X;
		/// <inheritdoc cref="IVector2{T}.Y"/>
		public int Y;

		int IVector2<int>.X { get => X; set => X = value; }
		int IVector2<int>.Y { get => Y; set => Y = value; }

		int IReadOnlyVector2<int>.X => X;
		int IReadOnlyVector2<int>.Y => Y;

		#endregion

		#region Static Properties

		public static Vector2I Zero => default;
		public static Vector2I UnitX => new(1, 0);
		public static Vector2I UnitY => new(0, 1);
		public static Vector2I One => new(1);

		#endregion

		#region Constructors

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2I(int value) : this(value, value) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2I(IReadOnlyVector2<int> value) : this(value.X, value.Y) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs2"]'/>
		public Vector2I(int x, int y)
		{
			X = x;
			Y = y;
		}

		#endregion

		#region Static Methods

		///// <inheritdoc cref="operator -(Vector2I)"/>
		//public static Vector2I Negate(Vector2I value)
		//{
		//	return -value;
		//}

		///// <inheritdoc cref="operator +(Vector2I, Vector2I)"/>
		//public static Vector2I Add(Vector2I left, Vector2I right)
		//{
		//	return left + right;
		//}

		///// <inheritdoc cref="operator -(Vector2I, Vector2I)"/>
		//public static Vector2I Subtract(Vector2I left, Vector2I right)
		//{
		//	return left - right;
		//}

		///// <inheritdoc cref="operator *(Vector2I, Vector2I)"/>
		//public static Vector2I Multiply(Vector2I left, Vector2I right)
		//{
		//	return left * right;
		//}

		///// <inheritdoc cref="operator /(Vector2I, Vector2I)"/>
		//public static Vector2I Divide(Vector2I left, Vector2I right)
		//{
		//	return left / right;
		//}

		#endregion

		#region Interface Implementations

		public bool Equals(Vector2I other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Vector2I other && Equals(other);
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

		public static bool operator ==(Vector2I left, Vector2I right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(Vector2I left, Vector2I right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		public static Vector2I operator -(Vector2I value)
		{
			return new(-value.X, -value.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		public static Vector2I operator +(Vector2I left, Vector2I right)
		{
			return new(
				left.X + right.X,
				left.Y + right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		public static Vector2I operator -(Vector2I left, Vector2I right)
		{
			return new(
				left.X - right.X,
				left.Y - right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2I operator *(Vector2I left, Vector2I right)
		{
			return new(
				left.X * right.X,
				left.Y * right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2I operator *(Vector2I left, int right)
		{
			return new(
				left.X * right,
				left.Y * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2I operator *(int left, Vector2I right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2I operator /(Vector2I left, Vector2I right)
		{
			return new(
				left.X / right.X,
				left.Y / right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2I operator /(Vector2I left, int right)
		{
			return new(
				left.X / right,
				left.Y / right);
		}

		#endregion

		#region Cast Operators

		public static implicit operator (int, int)(Vector2I value)
		{
			return (value.X, value.Y);
		}
		public static implicit operator Vector2I((int x, int y) value)
		{
			return new(value.x, value.y);
		}

		[RequiresPreviewFeatures]
		public static implicit operator Vector2<int>(Vector2I value)
		{
			return new(value.X, value.Y);
		}
		[RequiresPreviewFeatures]
		public static implicit operator Vector2I(Vector2<int> value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator Vector2I(Vector2F value)
		{
			return new((int)value.X, (int)value.Y);
		}
		public static explicit operator Vector2I(Vector2U value)
		{
			return new((int)value.X, (int)value.Y);
		}

		public static explicit operator Vector2(Vector2I value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2I(Vector2 value)
		{
			return new((int)value.X, (int)value.Y);
		}

		public static explicit operator PointF(Vector2I value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2I(PointF value)
		{
			return new((int)value.X, (int)value.Y);
		}

		public static explicit operator SizeF(Vector2I value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2I(SizeF value)
		{
			return new((int)value.Width, (int)value.Height);
		}

		public static explicit operator Point(Vector2I value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2I(Point value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator Size(Vector2I value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2I(Size value)
		{
			return new(value.Width, value.Height);
		}

		#endregion
	}

	/// <summary>
	///   Represents a 2-component vectors of <see cref="uint" />.
	/// </summary>
	public partial struct Vector2U :
		IVector2<uint>,
		IReadOnlyVector2<uint>,
		IEquatable<Vector2U>,
		IFormattable
	{
		#region Fields & Properties

		/// <inheritdoc cref="IVector2{T}.X"/>
		public uint X;
		/// <inheritdoc cref="IVector2{T}.Y"/>
		public uint Y;

		uint IVector2<uint>.X { get => X; set => X = value; }
		uint IVector2<uint>.Y { get => Y; set => Y = value; }

		uint IReadOnlyVector2<uint>.X => X;
		uint IReadOnlyVector2<uint>.Y => Y;

		#endregion

		#region Static Properties

		public static Vector2U Zero => default;
		public static Vector2U UnitX => new(1, 0);
		public static Vector2U UnitY => new(0, 1);
		public static Vector2U One => new(1);

		#endregion

		#region Constructors

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2U(uint value) : this(value, value) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector2U(IReadOnlyVector2<uint> value) : this(value.X, value.Y) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs2"]'/>
		public Vector2U(uint x, uint y)
		{
			X = x;
			Y = y;
		}

		#endregion

		#region Static Methods

		///// <inheritdoc cref="operator +(Vector2U, Vector2U)"/>
		//public static Vector2U Add(Vector2U left, Vector2U right)
		//{
		//	return left + right;
		//}

		///// <inheritdoc cref="operator -(Vector2U, Vector2U)"/>
		//public static Vector2U Subtract(Vector2U left, Vector2U right)
		//{
		//	return left - right;
		//}

		///// <inheritdoc cref="operator *(Vector2U, Vector2U)"/>
		//public static Vector2U Multiply(Vector2U left, Vector2U right)
		//{
		//	return left * right;
		//}

		///// <inheritdoc cref="operator /(Vector2U, Vector2U)"/>
		//public static Vector2U Divide(Vector2U left, Vector2U right)
		//{
		//	return left / right;
		//}

		#endregion

		#region Interface Implementations

		public bool Equals(Vector2U other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Vector2U other && Equals(other);
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

		public static bool operator ==(Vector2U left, Vector2U right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(Vector2U left, Vector2U right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		public static Vector2U operator +(Vector2U left, Vector2U right)
		{
			return new(
				left.X + right.X,
				left.Y + right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		public static Vector2U operator -(Vector2U left, Vector2U right)
		{
			return new(
				left.X - right.X,
				left.Y - right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2U operator *(Vector2U left, Vector2U right)
		{
			return new(
				left.X * right.X,
				left.Y * right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2U operator *(Vector2U left, uint right)
		{
			return new(
				left.X * right,
				left.Y * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector2U operator *(uint left, Vector2U right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2U operator /(Vector2U left, Vector2U right)
		{
			return new(
				left.X / right.X,
				left.Y / right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector2U operator /(Vector2U left, uint right)
		{
			return new(
				left.X / right,
				left.Y / right);
		}

		#endregion

		#region Cast Operators

		public static implicit operator (uint, uint)(Vector2U value)
		{
			return (value.X, value.Y);
		}
		public static implicit operator Vector2U((uint x, uint y) value)
		{
			return new(value.x, value.y);
		}

		[RequiresPreviewFeatures]
		public static implicit operator Vector2<uint>(Vector2U value)
		{
			return new(value.X, value.Y);
		}
		[RequiresPreviewFeatures]
		public static implicit operator Vector2U(Vector2<uint> value)
		{
			return new(value.X, value.Y);
		}

		public static explicit operator Vector2U(Vector2F value)
		{
			return new((uint)value.X, (uint)value.Y);
		}
		public static explicit operator Vector2U(Vector2I value)
		{
			return new((uint)value.X, (uint)value.Y);
		}

		public static explicit operator Vector2(Vector2U value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2U(Vector2 value)
		{
			return new((uint)value.X, (uint)value.Y);
		}

		public static explicit operator PointF(Vector2U value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2U(PointF value)
		{
			return new((uint)value.X, (uint)value.Y);
		}

		public static explicit operator SizeF(Vector2U value)
		{
			return new(value.X, value.Y);
		}
		public static explicit operator Vector2U(SizeF value)
		{
			return new((uint)value.Width, (uint)value.Height);
		}

		public static explicit operator Point(Vector2U value)
		{
			return new((int)value.X, (int)value.Y);
		}
		public static explicit operator Vector2U(Point value)
		{
			return new((uint)value.X, (uint)value.Y);
		}

		public static explicit operator Size(Vector2U value)
		{
			return new((int)value.X, (int)value.Y);
		}
		public static explicit operator Vector2U(Size value)
		{
			return new((uint)value.Width, (uint)value.Height);
		}

		#endregion
	}

}
