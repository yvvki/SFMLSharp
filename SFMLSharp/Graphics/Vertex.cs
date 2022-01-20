using SFML.System;

namespace SFML.Graphics
{
	public record struct Vertex(Vector2<float> Position, Color Color, Vector2<float> TexCoords)
	{
		public Vertex(Vector2<float> Position) : this(Position, Color.White, default) { }
		public Vertex(Vector2<float> Position, Color Color) : this(Position, Color, default) { }
		public Vertex(Vector2<float> Position, Vector2<float> TexCoords) : this(Position, Color.White, TexCoords) { }
	};
}
