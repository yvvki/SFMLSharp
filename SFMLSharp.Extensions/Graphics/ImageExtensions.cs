using CommunityToolkit.HighPerformance;

using SFML.System;

namespace SFML.Graphics
{
	public static class ImageExtensions
	{
		public static unsafe Span2D<Color> GetPixels2D(this Image @this)
		{
			Vector2<uint> size = @this.Size;
			return new(@this.GetPixelsPtr(), (int)size.X, (int)size.Y, 1);
		}

		public static unsafe Span2D<byte> GetPixels2DByte(this Image @this)
		{
			Vector2<uint> size = @this.Size;
			return new(@this.GetPixelsPtr(), (int)size.X, (int)size.Y, sizeof(Color));
		}
	}
}
