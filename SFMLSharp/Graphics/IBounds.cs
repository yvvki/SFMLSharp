namespace SFML.Graphics
{
	public interface IBounds : ITransformable
	{
		public Rect<float> GetLocalBounds();
		public Rect<float> GetGlobalBounds();
	}
}
