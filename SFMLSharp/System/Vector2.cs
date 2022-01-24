using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace SFML.System
{
	/// <summary>
	///   Represents a vector with two <typeparamref name="T" /> number values.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	[RequiresPreviewFeatures]
	[Serializable]
	public struct Vector2<T> :
		IVectorOperators<Vector2<T>, T>,
		IMultiplyOperators<Vector2<T>, T, Vector2<T>>,
		IDivisionOperators<Vector2<T>, T, Vector2<T>>,
		IFormattable,
		IEnumerable<T>
		where T : INumber<T>
	{
		#region Fields & Properties

		internal const int Count = 2;

		/// <summary>
		///   The X component of the vector.
		/// </summary>
		public T X;
		/// <summary>
		///   The Y component of the vector.
		/// </summary>
		public T Y;

		public T this[int index]
		{
			get => GetElement(this, index);
			set => this = WithElement(this, index, value);
		}

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

		/// <include file='Vector.xml' path='doc/vector[@name="Constructs2"]'/>
		public Vector2(T x, T y)
		{
			X = x;
			Y = y;
		}

		public Vector2(ReadOnlySpan<T> values)
		{
			if (values.Length is < Count)
			{
				throw new ArgumentOutOfRangeException(nameof(values));
			}

			this = Unsafe.ReadUnaligned<Vector2<T>>(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(values)));
		}

		#endregion

		#region Static Methods

		/// <inheritdoc cref="operator -(Vector2{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> Negate(Vector2<T> value)
		{
			return -value;
		}

		/// <inheritdoc cref="operator +(Vector2{T}, Vector2{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> Add(Vector2<T> left, Vector2<T> right)
		{
			return left + right;
		}

		/// <inheritdoc cref="operator -(Vector2{T}, Vector2{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> Subtract(Vector2<T> left, Vector2<T> right)
		{
			return left - right;
		}

		/// <inheritdoc cref="operator *(Vector2{T}, Vector2{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> Multiply(Vector2<T> left, Vector2<T> right)
		{
			return left * right;
		}

		/// <inheritdoc cref="operator /(Vector2{T}, Vector2{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> Divide(Vector2<T> left, Vector2<T> right)
		{
			return left / right;
		}

		#endregion

		#region Methods



		#endregion

		#region Interface Methods

		public void CopyTo(T[] array, int index)
		{
			GetSpanUnsafe(ref this).CopyTo(array.AsSpan()[index..]);
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
		internal static unsafe Span<T> GetSpanUnsafe(ref Vector2<T> vector)
		{
			//return new(Unsafe.AsPointer(ref transform), Count);

			return MemoryMarshal.CreateSpan(ref Unsafe.As<Vector2<T>, T>(ref vector), Count);
		}

		internal static T GetElement(Vector2<T> vector, int index)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			return GetElementUnsafe(ref vector, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetElementUnsafe(ref Vector2<T> vector, int index)
		{
			Debug.Assert(index is >= 0 and < Count);
			return Unsafe.Add(ref Unsafe.As<Vector2<T>, T>(ref vector), index);
		}

		internal static Vector2<T> WithElement(Vector2<T> vector, int index, T value)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			Vector2<T> result = vector;

			SetElementUnsafe(ref result, index, value);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void SetElementUnsafe(ref Vector2<T> vector, int index, T value)
		{
			Debug.Assert(index is >= 0 and < Count);
			Unsafe.Add(ref Unsafe.As<Vector2<T>, T>(ref vector), index) = value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out T x, out T y)
		{
			x = X;
			y = Y;
		}

		public bool Equals(Vector2<T> other)
		{
			return X.Equals(other.X)
				&& Y.Equals(other.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
			StringBuilder sb = new();
			string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

			sb.Append('<');
			sb.Append(X.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(Y.ToString(format, formatProvider));
			sb.Append('>');

			return sb.ToString();
		}

		public IEnumerator<T> GetEnumerator()
		{
			yield return X;
			yield return Y;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Operators

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2<T> left, Vector2<T> right)
		{
			return left.X == right.X
				&& left.Y == right.Y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2<T> left, Vector2<T> right)
		{
			return left.X != right.X
				|| left.Y != right.Y;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator -(Vector2<T> value)
		{
			return new(
				-value.X,
				-value.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator +(Vector2<T> left, Vector2<T> right)
		{
			return new(
				left.X + right.X,
				left.Y + right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator -(Vector2<T> left, Vector2<T> right)
		{
			return new(
				left.X - right.X,
				left.Y - right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator *(Vector2<T> left, Vector2<T> right)
		{
			return new(
				left.X * right.X,
				left.Y * right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator *(Vector2<T> left, T right)
		{
			return new(
				left.X * right,
				left.Y * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator *(T left, Vector2<T> right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator /(Vector2<T> left, Vector2<T> right)
		{
			return new(
				left.X / right.X,
				left.Y / right.Y);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<T> operator /(Vector2<T> left, T right)
		{
			return new(
				left.X / right,
				left.Y / right);
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
