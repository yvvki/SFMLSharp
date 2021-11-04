using System.Numerics;
using System.Runtime.Versioning;

namespace SFML.System
{
	public static partial class SIMDVector
	{
		public static bool IsVector2Supported<T>() where T : struct
		{
			return Vector<T>.Count >= 2;
		}

		public static bool IsVector3Supported<T>() where T : struct
		{
			return Vector<T>.Count >= 3;
		}

		public static Vector<T> ToVector<T>(this IReadOnlyVector2<T> @this)
			where T : struct
		{
			return new(new T[] { @this.X, @this.Y });
		}

		public static Vector<T> ToVector<T>(this IReadOnlyVector3<T> @this)
			where T : struct
		{
			return new(new T[] { @this.X, @this.Y, @this.Z });
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> ToVector2<T>(this Vector<T> @this)
			where T : struct, INumber<T>
		{
			return new(@this[0], @this[1]);
		}

		public static Vector2I ToVector2(this Vector<int> @this)
		{
			return new(@this[0], @this[1]);
		}

		public static Vector2U ToVector2(this Vector<uint> @this)
		{
			return new(@this[0], @this[1]);
		}

		public static Vector2F ToVector2(this Vector<float> @this)
		{
			return new(@this[0], @this[1]);
		}

		[RequiresPreviewFeatures]
		public static Vector3<T> ToVector3<T>(this Vector<T> @this)
			where T : struct, INumber<T>
		{
			return new(@this[0], @this[1], @this[2]);
		}

		public static Vector3F ToVector3(this Vector<float> @this)
		{
			return new(@this[0], @this[1], @this[2]);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Negate<T>(Vector2<T> value)
			where T : struct, INumber<T>
		{
			return IsVector2Supported<T>() ? (-value.ToVector()).ToVector2() : -value;
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Negate<T>(this ref Vector2<T> @this)
			where T : struct, INumber<T>
		{
			return @this = Negate(@this);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Add<T>(Vector2<T> left, Vector2<T> right)
			where T : struct, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() + right.ToVector()).ToVector2() : left + right;
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Add<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : struct, INumber<T>
		{
			return @this = Add(@this, other);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Subtract<T>(Vector2<T> left, Vector2<T> right)
			where T : struct, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() - right.ToVector()).ToVector2() : left - right;
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Subtract<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : struct, INumber<T>
		{
			return @this = Subtract(@this, other);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Multiply<T>(Vector2<T> left, Vector2<T> right)
			where T : struct, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() * right.ToVector()).ToVector2() : left * right;
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Multiply<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : struct, INumber<T>
		{
			return @this = Multiply(@this, other);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Divide<T>(Vector2<T> left, Vector2<T> right)
			where T : struct, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() / right.ToVector()).ToVector2() : left / right;
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Divide<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : struct, INumber<T>
		{
			return @this = Divide(@this, other);
		}
	}
}
