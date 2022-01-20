using CommunityToolkit.HighPerformance;

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

		public static void Update(this Texture @this, ReadOnlySpan2D<Color> pixels, Vector2<uint> offset)
		{
			uint x = offset.X;
			uint y = offset.Y;

			@this.Update(
				pixels,
				x,
				y);
		}


		public static void Update(this Texture @this, Color[,] pixels, uint x, uint y)
		{
			@this.Update(
				(ReadOnlySpan2D<Color>)pixels,
				x,
				y);
		}

		public static void Update(this Texture @this, Color[,] pixels, Vector2<uint> offset)
		{
			uint x = offset.X;
			uint y = offset.Y;


			@this.Update(
				pixels,
				x,
				y);
		}
	}
}
