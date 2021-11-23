using System.Numerics;

using SFML.System;

namespace SFML.Graphics
{
	public static class TransformExtensions

	{
		public static Matrix3x2 CreateTranslation(Vector2<float> position)
		{
			return Matrix3x2.CreateTranslation(position.X, position.Y);
		}

		public static Matrix3x2 CreateScale(Vector2<float> scale)
		{
			return Matrix3x2.CreateScale(scale.X, scale.Y);
		}

		public static Matrix3x2 CreateScale(Vector2<float> scale, Vector2 centerPoint)
		{
			return Matrix3x2.CreateScale(scale.X, scale.Y, centerPoint);
		}

		public static Matrix3x2 CreateSkew(Vector2<float> radians)
		{
			return Matrix3x2.CreateSkew(radians.X, radians.Y);
		}

		public static Matrix3x2 CreateSkew(Vector2<float> radians, Vector2 centerPoint)
		{
			return Matrix3x2.CreateSkew(radians.X, radians.Y, centerPoint);
		}

		public static Transform Multiply(Transform left, Matrix3x2 right)
		{
			return (Transform)Matrix3x2.Multiply((Matrix3x2)left, right);
		}

		public static void Multiply(this ref Transform @this, Matrix3x2 matrix)
		{
			@this = Multiply(@this, matrix);
		}
	}
}
