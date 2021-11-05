
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
				_transform = this.CalculateTransform();
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
