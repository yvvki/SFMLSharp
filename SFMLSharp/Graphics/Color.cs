using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace SFML.Graphics
{
	/// <summary>
	///   Represents a struct for manipulating RGBA colors.
	/// </summary>
	[Serializable]
	public struct Color :
		IEquatable<Color>,
		IFormattable,
		IEnumerable<byte>
	{
		#region Fields & Properties

		internal const int Count = 4;

		/// <summary>
		///   Red component.
		/// </summary>
		public byte R;
		/// <summary>
		///   Green component.
		/// </summary>
		public byte G;
		/// <summary>
		///   Blue component.
		/// </summary>
		public byte B;
		/// <summary>
		///   Alpha (opacity) component.
		/// </summary>
		public byte A;

		public byte this[int index]
		{
			get => GetElement(this, index);
			set => this = WithElement(this, index, value);
		}

		#endregion

		#region Static Properties

		public static Color Black => new();
		public static Color White => new(byte.MaxValue);

		public static Color Red => new(byte.MaxValue, default, default);
		public static Color Green => new(default, byte.MaxValue, default);
		public static Color Blue => new(default, default, byte.MaxValue);

		public static Color Yellow => new(byte.MaxValue, byte.MaxValue, default);
		public static Color Magenta => new(byte.MaxValue, default, byte.MaxValue);
		public static Color Cyan => new(default, byte.MaxValue, byte.MaxValue);

		public static Color Transparent => default;

		#endregion

		#region Constructors

		public Color(
			byte r,
			byte g,
			byte b,
			byte a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public Color(
			byte r,
			byte g,
			byte b) : this(r, g, b, byte.MaxValue) { }

		public Color(byte value) : this(value, value, value) { }

		public Color() : this(byte.MinValue) { } // Black

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color FromInterger(uint value)
		{
			return new(
				r: (byte)(value >> 24),
				g: (byte)(value >> 16),
				b: (byte)(value >> 8),
				a: (byte)value);
		}

		#endregion

		#region Static Methods

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color Add(Color left, Color right)
		{
			return left + right;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color Subtract(Color left, Color right)
		{
			return left - right;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color Modulate(Color left, Color right)
		{
			return left * right;
		}

		#endregion

		#region Methods

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint ToInterger()
		{
			//return
			//	(uint)(R << 24) |
			//	(uint)(G << 16) |
			//	(uint)(B << 8) |
			//	A;

			return Unsafe.As<Color, uint>(ref this);
		}

		#endregion

		#region Interface Methods

		public static byte GetElement(Color color, int index)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			return GetElementUnsafe(ref color, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte GetElementUnsafe(ref Color color, int index)
		{
			Debug.Assert(index is >= 0 and < Count);
			return Unsafe.Add(ref Unsafe.As<Color, byte>(ref color), index);
		}

		internal static Color WithElement(Color color, int index, byte value)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			Color result = color;

			SetElementUnsafe(ref result, index, value);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void SetElementUnsafe(ref Color color, int index, byte value)
		{
			Debug.Assert(index is >= 0 and < Count);
			Unsafe.Add(ref Unsafe.As<Color, byte>(ref color), index) = value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out byte r, out byte g, out byte b, out byte a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}
		public bool Equals(Color other)
		{
			return
				R.Equals(other.R) &&
				G.Equals(other.G) &&
				B.Equals(other.B) &&
				A.Equals(other.A);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Color color && Equals(color);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(R, G, B, A);
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			StringBuilder sb = new();

			string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

			sb.Append('(');
			sb.Append(R.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(G.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(B.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(A.ToString(format, formatProvider));
			sb.Append(')');

			return sb.ToString();
		}

		public IEnumerator<byte> GetEnumerator()
		{
			yield return R;
			yield return G;
			yield return B;
			yield return A;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Operators

		public static bool operator ==(Color left, Color right)
		{
			return (left.R == right.R)
				&& (left.G == right.G)
				&& (left.B == right.B)
				&& (left.A == right.A);
		}

		public static bool operator !=(Color left, Color right)
		{
			return (left.R != right.R)
				|| (left.G != right.G)
				|| (left.B != right.B)
				|| (left.A != right.A);
		}

		public static Color operator +(Color left, Color right)
		{
			return new(
				r: (byte)Math.Min(left.R + right.R, byte.MaxValue),
				g: (byte)Math.Min(left.G + right.G, byte.MaxValue),
				b: (byte)Math.Min(left.B + right.B, byte.MaxValue),
				a: (byte)Math.Min(left.A + right.A, byte.MaxValue));
		}

		public static Color operator -(Color left, Color right)
		{
			return new(
				r: (byte)Math.Max(left.R - right.R, byte.MinValue),
				g: (byte)Math.Max(left.G - right.G, byte.MinValue),
				b: (byte)Math.Max(left.B - right.B, byte.MinValue),
				a: (byte)Math.Max(left.A - right.A, byte.MinValue));
		}

		public static Color operator *(Color left, Color right)
		{
			return new(
				r: (byte)(left.R * right.R / byte.MaxValue),
				g: (byte)(left.G * right.G / byte.MaxValue),
				b: (byte)(left.B * right.B / byte.MaxValue),
				a: (byte)(left.A * right.A / byte.MaxValue));
		}

		#endregion

		#region Cast Operators

		//public static implicit operator (byte, byte, byte, byte)(Color value)
		//{
		//	return (value.R, value.G, value.B, value.A);
		//}

		//public static implicit operator Color((byte r, byte g, byte b, byte a) value)
		//{
		//	return new(value.r, value.g, value.b, value.a);
		//}

		#endregion
	}
}
