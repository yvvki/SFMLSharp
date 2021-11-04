namespace SFML.Graphics
{
	public interface IBounds : ITransformable
	{
		public FloatRect GetLocalBounds();
		public FloatRect GetGlobalBounds();
	}
}
