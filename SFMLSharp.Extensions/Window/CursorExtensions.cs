using Microsoft.Toolkit.HighPerformance;

using SFML.Graphics;
using SFML.System;

namespace SFML.Window
{
	public static class CursorExtensions
	{
		public static unsafe bool TryLoadFromPixels(
			this Cursor @this,
			Span<Color> pixels,
			Vector2<uint> size,
			Vector2<uint> hotspot = default)
		{
			fixed (Color* pixels_ptr = pixels)
			{
				return @this.TryLoadFromPixels(
				(byte*)pixels_ptr,
				size,
				hotspot);
			}
		}

		public static unsafe bool TryLoadFromPixels(
			this Cursor @this,
			Span2D<Color> pixels,
			Vector2<uint> hotspot = default)
		{
			fixed (Color* pixels_ptr = pixels)
			{
				return @this.TryLoadFromPixels(
				(byte*)pixels_ptr,
				new((uint)pixels.Width, (uint)pixels.Height),
				hotspot);
			}
		}

		public static unsafe bool TryLoadFromImage(this Cursor @this, Image image, Vector2<uint> hotspot = default)
		{
			return @this.TryLoadFromPixels(image.GetPixelsPointer(), image.Size, hotspot);
		}
	}
}
