using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace SFML.Graphics
{
	/// <summary>
	///   Represents a struct for manipulating RGBA colors.
	/// </summary>
	public struct Color : IEquatable<Color>, IFormattable, IEnumerable<byte>
	{
		#region Fields

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

		public Color(uint value)
			: this(
				r: (byte)(value >> 24),
				g: (byte)(value >> 16),
				b: (byte)(value >> 8),
				a: (byte)value)
		{ }

		public Color(
			byte r,
			byte g,
			byte b) : this(r, g, b, byte.MaxValue) { }

		public Color(byte value) : this(value, value, value) { }

		public Color() : this(default(byte)) { }

		public Color(IEnumerable<byte> color)
		{
			IEnumerator<byte> enumerator = color.GetEnumerator();

			R = enumerator.Current;
			if (!enumerator.MoveNext()) ThrowErrLess();
			G = enumerator.Current;
			if (!enumerator.MoveNext()) ThrowErrLess();
			B = enumerator.Current;
			if (!enumerator.MoveNext()) ThrowErrLess();
			A = enumerator.Current;

			static void ThrowErrLess() => throw new ArgumentOutOfRangeException(
					  nameof(color),
					  "Enumerable contains less than 4 items.");
		}

		#endregion

		#region Methods

		public uint ToInterger()
		{
			return
				(uint)(R << 24) |
				(uint)(G << 16) |
				(uint)(B << 8) |
				A;
		}

		public static Color Add(Color left, Color right)
		{
			return left + right;
		}

		public static Color Subtract(Color left, Color right)
		{
			return left - right;
		}

		public static Color Modulate(Color left, Color right)
		{
			return left * right;
		}

		public void Deconstruct(out byte r, out byte g, out byte b, out byte a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}

		#endregion

		#region Interface Method Implementations

		public bool Equals(Color other)
		{
			return
				R.Equals(other.R) &&
				G.Equals(other.G) &&
				B.Equals(other.B) &&
				A.Equals(other.A);
		}

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

		public static implicit operator (byte, byte, byte, byte)(Color value)
		{
			return (value.R, value.G, value.B, value.A);
		}

		public static implicit operator Color((byte r, byte g, byte b, byte a) value)
		{
			return new(value.r, value.g, value.b, value.a);
		}

		public static explicit operator global::System.Drawing.Color(Color value)
		{
			return global::System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
		}

		public static explicit operator Color(global::System.Drawing.Color value)
		{
			return new(value.R, value.G, value.B, value.A);
		}

		#endregion
	}
}
