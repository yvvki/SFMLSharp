using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.Versioning;

namespace SFML.System
{
	/// <summary>
	///   Represents a 3-component vectors of <see cref="float" />.
	/// </summary>
	public partial struct Vector3F :
		IVector3<float>,
		IReadOnlyVector3<float>,
		IEquatable<Vector3F>,
		IFormattable
	{
		#region Fields & Properties

		/// <inheritdoc cref="IVector2{T}.X"/>
		public float X;
		/// <inheritdoc cref="IVector2{T}.Y"/>
		public float Y;
		/// <inheritdoc cref="IVector3{T}.Z"/>
		public float Z;

		float IVector2<float>.X { get => X; set => X = value; }
		float IVector2<float>.Y { get => Y; set => Y = value; }
		float IVector3<float>.Z { get => Z; set => Z = value; }

		float IReadOnlyVector2<float>.X => X;
		float IReadOnlyVector2<float>.Y => Y;
		float IReadOnlyVector3<float>.Z => Z;

		#endregion

		#region Static Properties

		public static Vector3F Zero => default;
		public static Vector3F UnitX => new(1, 0, 0);
		public static Vector3F UnitY => new(0, 1, 0);
		public static Vector3F UnitZ => new(0, 0, 1);
		public static Vector3F One => new(1);

		#endregion

		#region Constructors

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector3F(float value) : this(value, value, value) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs1"]'/>
		public Vector3F(IReadOnlyVector3<float> value) : this(value.X, value.Y, value.Z) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3From2"]'/>
		public Vector3F(IReadOnlyVector2<float> value, float z) : this(value.X, value.Y, z) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3With2"]'/>
		public Vector3F(float x, IReadOnlyVector2<float> value) : this(x, value.X, value.Y) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3"]'/>
		public Vector3F(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		#endregion

		#region Static Methods

		///// <inheritdoc cref="operator -(Vector3F)"/>
		//public static Vector3F Negate(Vector3F value)
		//{
		//	return -value;
		//}

		///// <inheritdoc cref="operator +(Vector3F, Vector3F)"/>
		//public static Vector3F Add(Vector3F left, Vector3F right)
		//{
		//	return left + right;
		//}

		///// <inheritdoc cref="operator -(Vector3F, Vector3F)"/>
		//public static Vector3F Subtract(Vector3F left, Vector3F right)
		//{
		//	return left - right;
		//}

		///// <inheritdoc cref="operator *(Vector3F, Vector3F)"/>
		//public static Vector3F Multiply(Vector3F left, Vector3F right)
		//{
		//	return left * right;
		//}

		///// <inheritdoc cref="operator /(Vector3F, Vector3F)"/>
		//public static Vector3F Divide(Vector3F left, Vector3F right)
		//{
		//	return left / right;
		//}

		#endregion

		#region Interface Implementations

		public bool Equals(Vector3F other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Vector3F other && Equals(other);
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

		public static bool operator ==(Vector3F left, Vector3F right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		public static bool operator !=(Vector3F left, Vector3F right)
		{
			return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		public static Vector3F operator -(Vector3F value)
		{
			return new(-value.X, -value.Y, -value.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		public static Vector3F operator +(Vector3F left, Vector3F right)
		{
			return new(
				left.X + right.X,
				left.Y + right.Y,
				left.Z + right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		public static Vector3F operator -(Vector3F left, Vector3F right)
		{
			return new(
				left.X - right.X,
				left.Y - right.Y,
				left.Z - right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector3F operator *(Vector3F left, Vector3F right)
		{
			return new(
				left.X * right.X,
				left.Y * right.Y,
				left.Z * right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector3F operator *(Vector3F left, float right)
		{
			return new(
				left.X * right,
				left.Y * right,
				left.Z * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		public static Vector3F operator *(float left, Vector3F right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector3F operator /(Vector3F left, Vector3F right)
		{
			return new(
				left.X / right.X,
				left.Y / right.Y,
				left.Z / right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		public static Vector3F operator /(Vector3F left, float right)
		{
			return new(
				left.X / right,
				left.Y / right,
				left.Z / right);
		}

		#endregion

		#region Cast Operators

		public static implicit operator (float, float, float)(Vector3F value)
		{
			return (value.X, value.Y, value.Z);
		}
		public static implicit operator Vector3F((float x, float y, float z) value)
		{
			return new(value.x, value.y, value.z);
		}

		[RequiresPreviewFeatures]
		public static implicit operator Vector3<float>(Vector3F value)
		{
			return new(value.X, value.Y, value.Z);
		}
		[RequiresPreviewFeatures]
		public static implicit operator Vector3F(Vector3<float> value)
		{
			return new(value.X, value.Y, value.Z);
		}

		public static explicit operator Vector3(Vector3F value)
		{
			return new(value.X, value.Y, value.Z);
		}
		public static explicit operator Vector3F(Vector3 value)
		{
			return new(value.X, value.Y, value.Z);
		}

		#endregion
	}

}
