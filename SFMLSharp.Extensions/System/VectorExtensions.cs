using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace SFML.System
{
	public static class VectorExtensions
	{
		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ToVector(this Vector2<float> @this)
		{
			Vector2<float> value = @this;
			return AsVector(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 AsVector(ref Vector2<float> value)
		{
			return Unsafe.As<Vector2<float>, Vector2>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<float> ToGeneric(this Vector2 @this)
		{
			Vector2 value = @this;
			return AsGeneric(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<float> AsGeneric(ref Vector2 value)
		{
			return Unsafe.As<Vector2, Vector2<float>>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ToVector(this Vector3<float> @this)
		{
			Vector3<float> value = @this;
			return AsVector(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 AsVector(ref Vector3<float> value)
		{
			return Unsafe.As<Vector3<float>, Vector3>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<float> ToGeneric(this Vector3 @this)
		{
			Vector3 value = @this;
			return AsGeneric(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3<float> AsGeneric(ref Vector3 value)
		{
			return Unsafe.As<Vector3, Vector3<float>>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 ToVector(this Vector4<float> @this)
		{
			Vector4<float> value = @this;
			return AsVector(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 AsVector(ref Vector4<float> value)
		{
			return Unsafe.As<Vector4<float>, Vector4>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<float> ToGeneric(this Vector4 @this)
		{
			Vector4 value = @this;
			return AsGeneric(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<float> AsGeneric(ref Vector4 value)
		{
			return Unsafe.As<Vector4, Vector4<float>>(ref value);
		}
	}
}
