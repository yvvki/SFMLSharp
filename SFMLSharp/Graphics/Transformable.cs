
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
