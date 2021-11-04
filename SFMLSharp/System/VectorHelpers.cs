using System.Globalization;
using System.Text;

namespace SFML.System
{
	public static class VectorHelpers
	{
		public static string Format<T>(this IReadOnlyVector2<T> value, string? format, IFormatProvider? formatProvider)
			where T : IFormattable
		{
			StringBuilder sb = new();
			string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

			sb.Append('<');
			sb.Append(value.X.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(value.Y.ToString(format, formatProvider));
			sb.Append('>');

			return sb.ToString();
		}

		public static string Format<T>(this IReadOnlyVector3<T> value, string? format, IFormatProvider? formatProvider)
			where T : IFormattable
		{
			StringBuilder sb = new();
			string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

			sb.Append('<');
			sb.Append(value.X.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(value.Y.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(value.Z.ToString(format, formatProvider));
			sb.Append('>');

			return sb.ToString();
		}

		public static void Deconstruct<T>(this IReadOnlyVector2<T> value, out T x, out T y)
		{
			x = value.X;
			y = value.Y;
		}

		public static void Deconstruct<T>(this IReadOnlyVector3<T> value, out T x, out T y, out T z)
		{
			x = value.X;
			y = value.Y;
			z = value.Z;
		}
	}
}
