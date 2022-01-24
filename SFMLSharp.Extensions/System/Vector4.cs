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
	///   Represents a vector with four <typeparamref name="T" /> number values.
	/// </summary>
	/// <typeparam name="T">The type of the vector.</typeparam>
	[RequiresPreviewFeatures]
	[Serializable]
	public struct Vector4<T> :
		IVectorOperators<Vector4<T>, T>,
		IMultiplyOperators<Vector4<T>, T, Vector4<T>>,
		IDivisionOperators<Vector4<T>, T, Vector4<T>>,
		IFormattable,
		IEnumerable<T>
		where T : INumber<T>
	{
		#region Fields & Properties

		internal const int Count = 4;

		/// <inheritdoc cref="Vector2{T}.X"/>
		public T X;
		/// <inheritdoc cref="Vector2{T}.Y"/>
		public T Y;
		/// <inheritdoc cref="Vector3{T}.Y"/>
		public T Z;
		/// <summary>
		///   The W component of the vector.
		/// </summary>
		public T W;

		public T this[int index]
		{
			get => GetElement(this, index);
			set => this = WithElement(this, index, value);
		}

		#endregion

		#region Static Properties

		public static Vector4<T> Zero => new(T.Zero);
		public static Vector4<T> UnitX => new(T.One, T.Zero, T.Zero, T.Zero);
		public static Vector4<T> UnitY => new(T.Zero, T.One, T.Zero, T.Zero);
		public static Vector4<T> UnitZ => new(T.Zero, T.Zero, T.One, T.Zero);
		public static Vector4<T> UnitW => new(T.Zero, T.Zero, T.Zero, T.One);
		public static Vector4<T> One => new(T.One);

		public static Vector4<T> AdditiveIdentity => new(T.AdditiveIdentity);
		public static Vector4<T> MultiplicativeIdentity => new(T.MultiplicativeIdentity);

		#endregion

		#region Constructors

		/// <inheritdoc cref="Vector2{T}.Vector2(T)"/>
		public Vector4(T value) : this(value, value, value, value) { }

		/// <summary>
		///   Construct a new vector from a 3-component value and another component.
		/// </summary>
		/// <param name="value">The value for the X, Y, and Z component.</param>
		/// <param name="z">The value for the W component.</param>
		public Vector4(Vector3<T> value, T w) : this(value.X, value.Y, value.Z, w) { }

		/// <summary>
		///   Constructs a new vector from 4 components.
		/// </summary>
		/// <param name="x">The value for the X component.</param>
		/// <param name="y">The value for the Y component.</param>
		/// <param name="z">The value for the Z component.</param>
		/// <param name="w">The value for the W component.</param>
		public Vector4(T x, T y, T z, T w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		public Vector4(ReadOnlySpan<T> values)
		{
			if (values.Length is < Count)
			{
				throw new ArgumentOutOfRangeException(nameof(values));
			}

			this = Unsafe.ReadUnaligned<Vector4<T>>(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(values)));
		}

		#endregion

		#region Static Methods

		/// <inheritdoc cref="operator -(Vector4{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> Negate(Vector4<T> value)
		{
			return -value;
		}

		/// <inheritdoc cref="operator +(Vector4{T}, Vector4{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> Add(Vector4<T> left, Vector4<T> right)
		{
			return left + right;
		}

		/// <inheritdoc cref="operator -(Vector4{T}, Vector4{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> Subtract(Vector4<T> left, Vector4<T> right)
		{
			return left - right;
		}

		/// <inheritdoc cref="operator *(Vector4{T}, Vector4{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> Multiply(Vector4<T> left, Vector4<T> right)
		{
			return left * right;
		}

		/// <inheritdoc cref="operator /(Vector4{T}, Vector4{T})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> Divide(Vector4<T> left, Vector4<T> right)
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
		public static unsafe Span<T> GetSpanUnsafe(ref Vector4<T> vector)
		{
			//return new(Unsafe.AsPointer(ref transform), Count);

			return MemoryMarshal.CreateSpan(ref Unsafe.As<Vector4<T>, T>(ref vector), Count);
		}

		internal static T GetElement(Vector4<T> vector, int index)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			return GetElementUnsafe(ref vector, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetElementUnsafe(ref Vector4<T> vector, int index)
		{
			Debug.Assert(index is >= 0 and < Count);
			return Unsafe.Add(ref Unsafe.As<Vector4<T>, T>(ref vector), index);
		}

		internal static Vector4<T> WithElement(Vector4<T> vector, int index, T value)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			Vector4<T> result = vector;

			SetElementUnsafe(ref result, index, value);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void SetElementUnsafe(ref Vector4<T> vector, int index, T value)
		{
			Debug.Assert(index is >= 0 and < Count);
			Unsafe.Add(ref Unsafe.As<Vector4<T>, T>(ref vector), index) = value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out T x, out T y, out T z, out T w)
		{
			x = X;
			y = Y;
			z = Z;
			w = W;
		}

		public bool Equals(Vector4<T> other)
		{
			return X.Equals(other.X)
				&& Y.Equals(other.Y)
				&& Z.Equals(other.Z)
				&& W.Equals(other.W);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Vector4<T> other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y, Z, W);
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
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(W.ToString(format, formatProvider));
			sb.Append('>');

			return sb.ToString();
		}

		public IEnumerator<T> GetEnumerator()
		{
			yield return X;
			yield return Y;
			yield return Z;
			yield return W;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Operators

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4<T> left, Vector4<T> right)
		{
			return left.X == right.X
				&& left.Y == right.Y
				&& left.Z == right.Z
				&& left.W == right.W;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4<T> left, Vector4<T> right)
		{
			return left.X != right.X
				|| left.Y != right.Y
				|| left.Z != right.Z
				|| left.W != right.W;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Negate"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator -(Vector4<T> value)
		{
			return new(
				-value.X,
				-value.Y,
				-value.Z,
				-value.W);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Add"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator +(Vector4<T> left, Vector4<T> right)
		{
			return new(
				left.X + right.X,
				left.Y + right.Y,
				left.Z + right.Z,
				left.W + right.W);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Subtract"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator -(Vector4<T> left, Vector4<T> right)
		{
			return new(
				left.X - right.X,
				left.Y - right.Y,
				left.Z - right.Z,
				left.W - right.W);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator *(Vector4<T> left, Vector4<T> right)
		{
			return new(
				left.X * right.X,
				left.Y * right.Y,
				left.Z * right.Z,
				left.W * right.W);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator *(Vector4<T> left, T right)
		{
			return new(
				left.X * right,
				left.Y * right,
				left.Z * right,
				left.W * right);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Multiply"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator *(T left, Vector4<T> right)
		{
			return right * left;
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator /(Vector4<T> left, Vector4<T> right)
		{
			return new(
				left.X / right.X,
				left.Y / right.Y,
				left.Z / right.Z,
				left.W / right.W);
		}

		/// <include file='Vector.xml' path='doc/vector[@name="Divide"]'/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<T> operator /(Vector4<T> left, T right)
		{
			return new(
				left.X / right,
				left.Y / right,
				left.Z / right,
				left.W / right);
		}

		#endregion
	}
}
