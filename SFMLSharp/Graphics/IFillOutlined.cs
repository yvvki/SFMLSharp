namespace SFML.Graphics
{
	public interface IColored
	{
		Color Color { get; set; }
	}

	public interface ITextured
	{
		Texture? Texture { get; set; }
		Rect<int> TextureRect { get; set; }
	}

	public interface IFillOutlined
	{
		Color FillColor { get; set; }
		Color OutlineColor { get; set; }
		float OutlineThickness { get; set; }
	}
}
