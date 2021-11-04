using Microsoft.Toolkit.HighPerformance;

using SFML.Graphics;
using SFML.System;

namespace SFML.Window
{
	public static class WindowExtensions
	{
		public static Vector2I GetMousePosition(this Window @this)
		{
			return Mouse.GetPosition(@this);
		}

		internal static unsafe void SetIcon(this Window @this, uint width, uint height, Color* pixels)
		{
			@this.SetIcon(
				width,
				height,
				(byte*)pixels);
		}

		public static unsafe void SetIcon(this Window @this, IReadOnlyVector2<uint> size, Color* pixels)
		{
			@this.SetIcon(size.X, size.Y, pixels);
		}

		public static unsafe void SetIcon(this Window @this, uint width, uint height, ReadOnlySpan<Color> pixels)
		{
			if (pixels.IsEmpty) throw new ArgumentException("Pixels is empty.", nameof(pixels));
			if ((uint)pixels.Length != width * height) throw new ArgumentException("Pixels length does not match size parameter.", nameof(pixels));

			fixed (Color* colors_ptr = pixels)
			{
				@this.SetIcon(
					width,
					height,
					colors_ptr);
			}
		}

		public static unsafe void SetIcon(this Window @this, IReadOnlyVector2<uint> size, ReadOnlySpan<Color> pixels)
		{
			@this.SetIcon(size.X, size.Y, pixels);
		}

		public static unsafe void SetIcon(this Window @this, ReadOnlySpan2D<Color> pixels)
		{
			if (pixels.IsEmpty) throw new ArgumentException("Pixels is empty.", nameof(pixels));

			fixed (Color* colors_ptr = pixels)
			{
				@this.SetIcon((uint)pixels.Width, (uint)pixels.Height, colors_ptr);
			}
		}

		public static unsafe void SetIcon(this Window @this, Image image)
		{
			if (image is null) throw new ArgumentNullException(nameof(image));

			Vector2U size = image.Size;
			@this.SetIcon(size.X, size.Y, image.GetPixelsPointer());
		}
	}
}
