using Microsoft.Toolkit.HighPerformance;

using SFML.System;

namespace SFML.Graphics
{
	public static class TextureExtensions
	{
		public static unsafe void Update(this Texture @this, ReadOnlySpan2D<Color> pixels, uint x, uint y)
		{
			fixed (Color* pixels_ptr = pixels)
			{
				@this.Update(
					(byte*)pixels_ptr,
					(uint)pixels.Width,
					(uint)pixels.Height,
					x,
					y);
			}
		}

		public static void Update(this Texture @this, ReadOnlySpan2D<Color> pixels, IReadOnlyVector2<uint> offset)
		{
			@this.Update(
				pixels,
				offset.X,
				offset.Y);
		}


		public static void Update(this Texture @this, Color[,] pixels, uint x, uint y)
		{
			@this.Update(
				(ReadOnlySpan2D<Color>)pixels,
				x,
				y);
		}

		public static void Update(this Texture @this, Color[,] pixels, IReadOnlyVector2<uint> offset)
		{
			@this.Update(
				pixels,
				offset.X,
				offset.Y);
		}
	}
}
