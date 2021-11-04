using SFML.System;

namespace SFML.Graphics
{
	public record struct Vertex(Vector2F Position, Color Color, Vector2F TexCoords)
	{
		public Vertex(Vector2F position) : this(position, Color.White, default) { }
		public Vertex(Vector2F position, Color color) : this(position, color, default) { }
		public Vertex(Vector2F position, Vector2F texCoords) : this(position, Color.White, texCoords) { }
	};
}
