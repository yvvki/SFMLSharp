using SFML.System;

namespace SFML.Graphics
{
	public interface IRenderTarget
	{
		void Clear(Color? color = null);

		View View { get; set; }
		View DefaultView { get; }

		IntRect Viewport { get; }

		Vector2F MapPixelToCoords(Vector2I point);
		Vector2F MapPixelToCoords(Vector2I point, View view);

		Vector2I MapCoordsToPixel(Vector2F point);
		Vector2I MapCoordsToPixel(Vector2F point, View view);

		void Draw(IDrawable drawable, RenderStates? states = null);

		void Draw(
			ReadOnlySpan<Vertex> vertices,
			PrimitiveType type,
			RenderStates? states = null);

		internal unsafe void Draw(Sprite sprite, RenderStates.Native* states);
		internal unsafe void Draw(Text text, RenderStates.Native* states);
		internal unsafe void Draw(Shape shape, RenderStates.Native* states);
		internal unsafe void Draw(CircleShape shape, RenderStates.Native* states);
		internal unsafe void Draw(RectangleShape shape, RenderStates.Native* states);
	}
}
