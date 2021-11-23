using SFML.System;

namespace SFML.Graphics
{
	public interface IRenderTarget
	{
		void Clear(Color? color = null);

		View View { get; set; }
		View DefaultView { get; }

		Rect<int> Viewport { get; }

		Vector2<float> MapPixelToCoords(Vector2<int> point);
		Vector2<float> MapPixelToCoords(Vector2<int> point, View view);

		Vector2<int> MapCoordsToPixel(Vector2<float> point);
		Vector2<int> MapCoordsToPixel(Vector2<float> point, View view);

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
