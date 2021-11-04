using System.Runtime.Versioning;

namespace SFML.System
{
	public static class VectorExtensions
	{
		[RequiresPreviewFeatures]
		public static Vector2<T> ToVector2<T>(this IReadOnlyVector2<T> @this)
			where T : INumber<T>
		{
			return new(@this);
		}

		public static Vector2I ToVector2(this IReadOnlyVector2<int> @this)
		{
			return new(@this);
		}

		public static Vector2U ToVector2(this IReadOnlyVector2<uint> @this)
		{
			return new(@this);
		}

		public static Vector2F ToVector2(this IReadOnlyVector2<float> @this)
		{
			return new(@this);
		}

		[RequiresPreviewFeatures]
		public static Vector3<T> ToVector3<T>(this IReadOnlyVector3<T> @this)
			where T : INumber<T>
		{
			return new(@this);
		}

		public static Vector3F ToVector3(this IReadOnlyVector3<float> @this)
		{
			return new(@this);
		}
	}
}
