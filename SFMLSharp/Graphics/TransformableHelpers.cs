namespace SFML.Graphics
{
	public static class TransformableHelpers
	{
		public static Transform CalculateTransform(this ITransformable @this)
		{
			float angle = -@this.Rotation * 3.141592654f / 180f;
			float cosine = (float)Math.Cos(angle);
			float sine = (float)Math.Sin(angle);
			float sxc = @this.Scaling.X * cosine;
			float syc = @this.Scaling.Y * cosine;
			float sxs = @this.Scaling.X * sine;
			float sys = @this.Scaling.Y * sine;
			float tx = -@this.Origin.X * sxc - @this.Origin.Y * sys + @this.Position.X;
			float ty = @this.Origin.X * sxs - @this.Origin.Y * syc + @this.Position.Y;

			return new(
				sxc, sys, tx,
				-sxs, syc, ty,
				0f, 0f, 1f);
		}
	}
}
