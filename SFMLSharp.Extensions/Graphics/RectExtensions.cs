using System.Runtime.Versioning;

namespace SFML.Graphics
{
	public static class RectExtensions
	{
		[RequiresPreviewFeatures]
		public static T Right<T>(this IReadOnlyRect<T> value)
			where T : INumber<T>
		{
			return value.Left + value.Width;
		}

		public static float Right(this IReadOnlyRect<float> value)
		{
			return value.Left + value.Width;
		}

		public static int Right(this IReadOnlyRect<int> value)
		{
			return value.Left + value.Width;
		}

		[RequiresPreviewFeatures]
		public static T Bottom<T>(this IReadOnlyRect<T> value)
			where T : INumber<T>
		{
			return value.Top + value.Height;
		}

		public static float Bottom(this IReadOnlyRect<float> value)
		{
			return value.Top + value.Height;
		}

		public static int Bottom(this IReadOnlyRect<int> value)
		{
			return value.Top + value.Height;
		}

		[RequiresPreviewFeatures]
		public static Rect<T> ToRect<T>(this IReadOnlyRect<T> value)
			where T : INumber<T>
		{
			return new(value);
		}

		public static FloatRect ToRect(this IReadOnlyRect<float> value)
		{
			return new(value);
		}

		public static IntRect ToRect(this IReadOnlyRect<int> value)
		{
			return new(value);
		}
	}
}
