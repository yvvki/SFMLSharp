using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;

namespace SFML.System
{
	/// <summary>
	///   Represents a vector with three <typeparamref name="T" /> number values.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	[RequiresPreviewFeatures]
	[Serializable]
	public struct Vector3<T> :
		IVectorOperators<Vector3<T>, T>,
		IMultiplyOperators<Vector3<T>, T, Vector3<T>>,
		IDivisionOperators<Vector3<T>, T, Vector3<T>>,
		IFormattable,
		IEnumerable<T>
		where T : INumber<T>
	{
		#region Fields & Properties

		internal const int Count = 3;

		/// <inheritdoc cref="Vector2{T}.X"/>
		public T X;
		/// <inheritdoc cref="Vector2{T}.Y"/>
		public T Y;
		/// <summary>
		///   The Z component of the vector.
		/// </summary>
		public T Z;

		public T this[int index]
		{
			get => GetElement(this, index);
			set => this = WithElement(this, index, value);
		}

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

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3From2"]'/>
		public Vector3(Vector2<T> value, T z) : this(value.X, value.Y, z) { }

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs3"]'/>
		public Vector3(T x, T y, T z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		#endregion

		#region Static Methods

		/// <inheritdoc cref="operator -(Vector3{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> Negate(Vector3<T> value)
		{
			return -value;
		}

		/// <inheritdoc cref="operator +(Vector3{T}, Vector3{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> Add(Vector3<T> left, Vector3<T> right)
		{
			return left + right;
		}

		/// <inheritdoc cref="operator -(Vector3{T}, Vector3{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> Subtract(Vector3<T> left, Vector3<T> right)
		{
			return left - right;
		}

		/// <inheritdoc cref="operator *(Vector3{T}, Vector3{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> Multiply(Vector3<T> left, Vector3<T> right)
		{
			return left * right;
		}

		/// <inheritdoc cref="operator /(Vector3{T}, Vector3{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> Divide(Vector3<T> left, Vector3<T> right)
		{
			return left / right;
		}

		#endregion

		#region Methods



		#endregion

		#region Interface Methods

		public void CopyTo(T[] array, int index)
		{
			GetSpanUnsafe(ref this).ToArray().CopyTo(array, index);
		}

		public void CopyTo(Span<T> destination)
		{
			GetSpanUnsafe(ref this).CopyTo(destination);
		}

		public bool TryCopyTo(Span<T> destination)
		{
			return GetSpanUnsafe(ref this).TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static unsafe Span<T> GetSpanUnsafe(ref Vector3<T> vector)
		{
			return new(Unsafe.AsPointer(ref vector), Count);
		}

		public static T GetElement(Vector3<T> vector, int index)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			return GetElementUnsafe(ref vector, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetElementUnsafe(ref Vector3<T> vector, int index)
		{
			Debug.Assert(index is >= 0 and < Count);
			return Unsafe.Add(ref Unsafe.As<Vector3<T>, T>(ref vector), index);
		}

		internal static Vector3<T> WithElement(Vector3<T> vector, int index, T value)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			Vector3<T> result = vector;

			SetElementUnsafe(ref result, index, value);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void SetElementUnsafe(ref Vector3<T> vector, int index, T value)
		{
			Debug.Assert(index is >= 0 and < Count);
			Unsafe.Add(ref Unsafe.As<Vector3<T>, T>(ref vector), index) = value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out T x, out T y, out T z)
		{
			x = X;
			y = Y;
			z = Z;
		}

		public bool Equals(Vector3<T> other)
		{
			return X.Equals(other.X)
				&& Y.Equals(other.Y)
				&& Z.Equals(other.Z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
			StringBuilder sb = new();
			string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

			sb.Append('<');
			sb.Append(X.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(Y.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(Z.ToString(format, formatProvider));
			sb.Append('>');

			return sb.ToString();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Enumerator GetEnumerator()
		{
			return new(this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator<T>
		{
			private readonly Vector3<T> _vector;

			private int _index;

			public Enumerator(in Vector3<T> value)
			{
				_vector = value;
			}

			public T Current => _vector[_index];
			object? IEnumerator.Current => Current;

			public bool MoveNext()
			{
				if (_index < Count)
				{
					_index++;
					return true;
				}
				else
				{
					return false;
				}
			}

			public void Reset()
			{
				_index = default;
			}

			public void Dispose()
			{
				GC.SuppressFinalize(this);
			}
		}

		#endregion

		#region Operators

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3<T> left, Vector3<T> right)
		{
			return left.X == right.X
				&& left.Y == right.Y
				&& left.Z == right.Z;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3<T> left, Vector3<T> right)
		{
			return left.X != right.X
				|| left.Y != right.Y
				|| left.Z != right.Z;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator -(Vector3<T> value)
		{
			return new(
				-value.X,
				-value.Y,
				-value.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator +(Vector3<T> left, Vector3<T> right)
		{
			return new(
				left.X + right.X,
				left.Y + right.Y,
				left.Z + right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator -(Vector3<T> left, Vector3<T> right)
		{
			return new(
				left.X - right.X,
				left.Y - right.Y,
				left.Z - right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator *(Vector3<T> left, Vector3<T> right)
		{
			return new(
				left.X * right.X,
				left.Y * right.Y,
				left.Z * right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator *(Vector3<T> left, T right)
		{
			return new(
				left.X * right,
				left.Y * right,
				left.Z * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator *(T left, Vector3<T> right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator /(Vector3<T> left, Vector3<T> right)
		{
			return new(
				left.X / right.X,
				left.Y / right.Y,
				left.Z / right.Z);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<T> operator /(Vector3<T> left, T right)
		{
			return new(
				left.X / right,
				left.Y / right,
				left.Z / right);
		}

		#endregion

		#region Cast Operators

		//public static implicit operator (T, T)(Vector2<T> value)
		//{
		//	return (value.X, value.Y);
		//}
		//public static implicit operator Vector2<T>((T x, T y) value)
		//{
		//	return new(value.x, value.y);
		//}

		#endregion
	}
}
