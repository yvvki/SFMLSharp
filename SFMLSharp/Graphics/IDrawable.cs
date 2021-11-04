namespace SFML.Graphics
{
	public interface IDrawable
	{
		void Draw(IRenderTarget target, RenderStates states);

		internal void DrawNullable(IRenderTarget target, RenderStates? states = null)
		{
			Draw(target, states ?? RenderStates.Default);
		}
	}
}
