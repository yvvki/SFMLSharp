using SFML.System;

namespace SFML.Graphics
{
	public record struct Vertex(Vector2<float> Position, Color Color, Vector2<float> TexCoords)
	{
		public Vertex(Vector2<float> position) : this(position, Color.White, default) { }
		public Vertex(Vector2<float> position, Color color) : this(position, color, default) { }
		public Vertex(Vector2<float> position, Vector2<float> texCoords) : this(position, Color.White, texCoords) { }
	};
}
