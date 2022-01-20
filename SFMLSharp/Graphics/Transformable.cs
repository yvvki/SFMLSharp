
using SFML.System;

namespace SFML.Graphics
{
	public interface ITransformable
	{
		public Vector2<float> Position { get; set; }
		public float Rotation { get; set; }
		public Vector2<float> Scaling { get; set; }
		public Vector2<float> Origin { get; set; }

		public void Move(Vector2<float> offset);
		public void Rotate(float angle);
		public void Scale(Vector2<float> factor);

		public Transform GetTransform();
		public Transform GetInverseTransform();

		// Based of the implementation in the native code.
		public static Transform CalculateTransform(ITransformable @this)
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

	public class Transformable : ITransformable
	{
		private Vector2<float> _position;
		public Vector2<float> Position
		{
			get => _position;
			set
			{
				_position = value;
				_transformNeedUpdate = true;
				_inverseTransformNeedUpdate = true;
			}
		}

		private float _rotation;
		public float Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				_transformNeedUpdate = true;
				_inverseTransformNeedUpdate = true;
			}
		}

		private Vector2<float> _size;
		public Vector2<float> Scaling
		{
			get => _size;
			set
			{
				_size = value;
				_transformNeedUpdate = true;
				_inverseTransformNeedUpdate = true;
			}
		}

		private Vector2<float> _origin;
		public Vector2<float> Origin
		{
			get => _origin;
			set
			{
				_origin = value;
				_transformNeedUpdate = true;
				_inverseTransformNeedUpdate = true;
			}
		}

		public void Move(Vector2<float> offset)
		{
			Position += offset;
		}

		public void Rotate(float angle)
		{
			Rotation += angle;
		}

		public void Scale(Vector2<float> factor)
		{
			Scaling += factor;
		}

		private bool _transformNeedUpdate;
		private bool _inverseTransformNeedUpdate;

		private Transform _transform;
		private Transform _inverseTransform;

		public Transform GetTransform()
		{
			if (_transformNeedUpdate)
			{
				_transform = ITransformable.CalculateTransform(this);
				_transformNeedUpdate = false;
			}

			return _transform;
		}

		public Transform GetInverseTransform()
		{
			if (_inverseTransformNeedUpdate)
			{
				_inverseTransform = GetTransform().GetInverse();
				_inverseTransformNeedUpdate = false;
			}

			return _inverseTransform;
		}
	}
}
