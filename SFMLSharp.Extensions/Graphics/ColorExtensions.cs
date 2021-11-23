using SFML.System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace SFML.Graphics
{
	public static class ColorExtensions
	{
		public static global::System.Drawing.Color ToDrawing(this Color @this)
		{
			return global::System.Drawing.Color.FromArgb(@this.A, @this.R, @this.G, @this.B);
		}

		public static Color ToSFML(this global::System.Drawing.Color @this)
		{
			return new(@this.R, @this.G, @this.B, @this.A);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<float> ToGeneric(this Color @this)
		{
			Color value = @this;
			return AsGeneric(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4<float> AsGeneric(ref Color value)
		{
			return Unsafe.As<Color, Vector4<float>>(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color ToRect(this Vector4<float> @this)
		{
			Vector4<float> value = @this;
			return AsRect(ref value);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color AsRect(ref Vector4<float> value)
		{
			return Unsafe.As<Vector4<float>, Color>(ref value);
		}
	}
}
