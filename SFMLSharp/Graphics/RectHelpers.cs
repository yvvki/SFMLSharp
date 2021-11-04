using System.Globalization;
using System.Text;

using SFML.System;

namespace SFML.Graphics
{
	public static class RectHelpers
	{
		public static string Format<T>(this IReadOnlyRect<T> value, string? format, IFormatProvider? formatProvider)
			where T : IFormattable
		{
			StringBuilder sb = new();
			string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

			sb.Append('<');
			sb.Append('<');
			sb.Append(value.Left.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(value.Top.ToString(format, formatProvider));
			sb.Append('>');
			sb.Append(separator);
			sb.Append(' ');
			sb.Append('<');
			sb.Append(value.Width.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(value.Height.ToString(format, formatProvider));
			sb.Append('>');
			sb.Append('>');

			return sb.ToString();
		}

		public static void Deconstruct<T>(
			this IReadOnlyRect<T> value,
			out IReadOnlyVector2<T> position,
			out IReadOnlyVector2<T> size)
		{
			position = value.Position;
			size = value.Size;
		}

		public static void Deconstruct<T>(
			this IReadOnlyRect<T> value,
			out T left, out T top,
			out T width, out T height)
		{
			left = value.Left;
			top = value.Top;
			width = value.Width;
			height = value.Height;
		}
	}
}
