
using SFML.System;

namespace SFML.Graphics
{
	public interface ITransformable
	{
		public Vector2F Position { get; set; }
		public float Rotation { get; set; }
		public Vector2F Scaling { get; set; }
		public Vector2F Origin { get; set; }

		public void Move(Vector2F offset);
		public void Rotate(float angle);
		public void Scale(Vector2F factor);

		public Transform GetTransform();
		public Transform GetInverseTransform();

		public static Transform Calculate(ITransformable transformable)
		{
			float angle = -transformable.Rotation * 3.141592654f / 180f;
			float cosine = (float)Math.Cos(angle);
			float sine = (float)Math.Sin(angle);
			float sxc = transformable.Scaling.X * cosine;
			float syc = transformable.Scaling.Y * cosine;
			float sxs = transformable.Scaling.X * sine;
			float sys = transformable.Scaling.Y * sine;
			float tx = -transformable.Origin.X * sxc - transformable.Origin.Y * sys + transformable.Position.X;
			float ty = transformable.Origin.X * sxs - transformable.Origin.Y * syc + transformable.Position.Y;

			return new(
				sxc, sys, tx,
				-sxs, syc, ty,
				0f, 0f, 1f);
		}
	}

	public class Transformable : ITransformable
	{
		private Vector2F _position;
		public Vector2F Position
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

		private Vector2F _size;
		public Vector2F Scaling
		{
			get => _size;
			set
			{
				_size = value;
				_transformNeedUpdate = true;
				_inverseTransformNeedUpdate = true;
			}
		}

		private Vector2F _origin;
		public Vector2F Origin
		{
			get => _origin;
			set
			{
				_origin = value;
				_transformNeedUpdate = true;
				_inverseTransformNeedUpdate = true;
			}
		}

		public void Move(Vector2F offset)
		{
			Position += offset;
		}

		public void Rotate(float angle)
		{
			Rotation += angle;
		}

		public void Scale(Vector2F factor)
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
				float angle = -_rotation * 3.141592654f / 180f;
				float cosine = (float)Math.Cos(angle);
				float sine = (float)Math.Sin(angle);
				float sxc = _size.X * cosine;
				float syc = _size.Y * cosine;
				float sxs = _size.X * sine;
				float sys = _size.Y * sine;
				float tx = -_origin.X * sxc - _origin.Y * sys + _position.X;
				float ty = _origin.X * sxs - _origin.Y * syc + _position.Y;

				_transform = new(sxc, sys, tx,
										-sxs, syc, ty,
										 0f, 0f, 1f);
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
