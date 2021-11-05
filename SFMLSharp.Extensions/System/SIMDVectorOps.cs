using System.Numerics;
using System.Runtime.Versioning;

namespace SFML.System
{
	public static partial class SIMDVector
	{
		[RequiresPreviewFeatures]
		public static Vector2<T> Negate<T>(Vector2<T> value)
			where T : unmanaged, INumber<T>
		{
			return IsVector2Supported<T>() ? (-value.ToVector()).ToVector2() : -value;
		}

		[RequiresPreviewFeatures]
		public static void Negate<T>(this ref Vector2<T> @this)
			where T : unmanaged, INumber<T>
		{
			@this = Negate(@this);
		}

		public static Vector2I Negate(Vector2I value)
		{
			return IsVector2Supported<int>() ? (-value.ToVector()).ToVector2() : -value;
		}

		public static void Negate(this ref Vector2I @this)
		{
			@this = Negate(@this);
		}

		public static Vector2F Negate(Vector2F value)
		{
			return (Vector2F)(-(Vector2)value);
		}

		public static void Negate(this ref Vector2F @this)
		{
			@this = Negate(@this);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Add<T>(Vector2<T> left, Vector2<T> right)
			where T : unmanaged, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() + right.ToVector()).ToVector2() : left + right;
		}

		[RequiresPreviewFeatures]
		public static void Add<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : unmanaged, INumber<T>
		{
			@this = Add(@this, other);
		}

		public static Vector2I Add(Vector2I left, Vector2I right)
		{
			return IsVector2Supported<int> () ? (left.ToVector() + right.ToVector()).ToVector2() : left + right;
		}

		public static void Add(this ref Vector2I @this, Vector2I other)
		{
			@this = Add(@this, other);
		}

		public static Vector2U Add(Vector2U left, Vector2U right)
		{
			return IsVector2Supported<uint>() ? (left.ToVector() + right.ToVector()).ToVector2() : left + right;
		}

		public static void Add(this ref Vector2U @this, Vector2U other)
		{
			@this = Add(@this, other);
		}

		public static Vector2F Add(Vector2F left, Vector2F right)
		{
			return (Vector2F)((Vector2)left + (Vector2)right);
		}

		public static void Add(this ref Vector2F @this, Vector2F other)
		{
			@this = Add(@this, other);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Subtract<T>(Vector2<T> left, Vector2<T> right)
			where T : unmanaged, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() - right.ToVector()).ToVector2() : left - right;
		}

		[RequiresPreviewFeatures]
		public static void Subtract<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : unmanaged, INumber<T>
		{
			@this = Subtract(@this, other);
		}

		public static Vector2I Subtract(Vector2I left, Vector2I right)
		{
			return IsVector2Supported<int>() ? (left.ToVector() + right.ToVector()).ToVector2() : left - right;
		}

		public static void Subtract(this ref Vector2I @this, Vector2I other)
		{
			@this = Subtract(@this, other);
		}

		public static Vector2U Subtract(Vector2U left, Vector2U right)
		{
			return IsVector2Supported<uint>() ? (left.ToVector() + right.ToVector()).ToVector2() : left - right;
		}

		public static void Subtract(this ref Vector2U @this, Vector2U other)
		{
			@this = Subtract(@this, other);
		}

		public static Vector2F Subtract(Vector2F left, Vector2F right)
		{
			return (Vector2F)((Vector2)left - (Vector2)right);
		}

		public static void Subtract(this ref Vector2F @this, Vector2F other)
		{
			@this = Add(@this, other);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Multiply<T>(Vector2<T> left, Vector2<T> right)
			where T : unmanaged, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() * right.ToVector()).ToVector2() : left * right;
		}

		[RequiresPreviewFeatures]
		public static void Multiply<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : unmanaged, INumber<T>
		{
			@this = Multiply(@this, other);
		}

		public static Vector2I Multiply(Vector2I left, Vector2I right)
		{
			return IsVector2Supported<int>() ? (left.ToVector() + right.ToVector()).ToVector2() : left * right;
		}

		public static void Multiply(this ref Vector2I @this, Vector2I other)
		{
			@this = Multiply(@this, other);
		}

		public static Vector2U Multiply(Vector2U left, Vector2U right)
		{
			return IsVector2Supported<uint>() ? (left.ToVector() + right.ToVector()).ToVector2() : left * right;
		}

		public static void Multiply(this ref Vector2U @this, Vector2U other)
		{
			@this = Multiply(@this, other);
		}

		public static Vector2F Multiply(Vector2F left, Vector2F right)
		{
			return (Vector2F)((Vector2)left * (Vector2)right);
		}

		public static void Multiply(this ref Vector2F @this, Vector2F other)
		{
			@this = Add(@this, other);
		}

		[RequiresPreviewFeatures]
		public static Vector2<T> Divide<T>(Vector2<T> left, Vector2<T> right)
			where T : unmanaged, INumber<T>
		{
			return IsVector2Supported<T>() ? (left.ToVector() / right.ToVector()).ToVector2() : left / right;
		}

		[RequiresPreviewFeatures]
		public static void Divide<T>(this ref Vector2<T> @this, Vector2<T> other)
			where T : unmanaged, INumber<T>
		{
			@this = Divide(@this, other);
		}

		public static Vector2I Divide(Vector2I left, Vector2I right)
		{
			return IsVector2Supported<int>() ? (left.ToVector() + right.ToVector()).ToVector2() : left / right;
		}

		public static void Divide(this ref Vector2I @this, Vector2I other)
		{
			@this = Divide(@this, other);
		}

		public static Vector2U Divide(Vector2U left, Vector2U right)
		{
			return IsVector2Supported<uint>() ? (left.ToVector() + right.ToVector()).ToVector2() : left / right;
		}

		public static void Divide(this ref Vector2U @this, Vector2U other)
		{
			@this = Divide(@this, other);
		}

		public static Vector2F Divide(Vector2F left, Vector2F right)
		{
			return (Vector2F)((Vector2)left / (Vector2)right);
		}

		public static void Divide(this ref Vector2F @this, Vector2F other)
		{
			@this = Divide(@this, other);
		}
	}
}
