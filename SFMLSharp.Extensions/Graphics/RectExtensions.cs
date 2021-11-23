using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

using SFML.System;

namespace SFML.Graphics
{
	public static class RectExtensions
	{
		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Right<T>(this in Rect<T> @this)
			where T : INumber<T>
		{
			return @this.Left + @this.Width;
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Bottom<T>(this in Rect<T> @this)
			where T : INumber<T>
		{
			return @this.Top + @this.Height;
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<float> ToGeneric(this Rect<float> @this)
		{
			Rect<float> value = @this;
			return AsGeneric(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<float> AsGeneric(ref Rect<float> value)
		{
			return Unsafe.As<Rect<float>, Vector4<float>>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect<float> ToRect(this Vector4<float> @this)
		{
			Vector4<float> value = @this;
			return AsRect(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect<float> AsRect(ref Vector4<float> value)
		{
			return Unsafe.As<Vector4<float>, Rect<float>>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 ToVector(this Rect<float> @this)
		{
			Rect<float> value = @this;
			return AsVector(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 AsVector(ref Rect<float> value)
		{
			return Unsafe.As<Rect<float>, Vector4>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect<float> ToRect(this Vector4 @this)
		{
			Vector4 value = @this;
			return AsRect(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect<float> AsRect(ref Vector4 value)
		{
			return Unsafe.As<Vector4, Rect<float>>(ref value);
		}
	}
}
