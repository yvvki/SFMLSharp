using System.Diagnostics.CodeAnalysis;

using SFML.System;

namespace SFML.Graphics
{
	public abstract unsafe class ReadOnlyShape : Shape
	{
		protected sealed override bool IsReadOnly => true;

		internal ReadOnlyShape(Native* handle) : base(handle) { }

		public ReadOnlyShape() : base() { }

		[DoesNotReturn]
		private static void ThrowReadOnly()
		{
			throw new InvalidOperationException("Shape is read-only.");
		}

		[DoesNotReturn]
		protected sealed override void SetPoint(int index, Vector2<float> value)
		{
			ThrowReadOnly();
		}

		[DoesNotReturn]
		protected sealed override int AddPoint(Vector2<float> point)
		{
			ThrowReadOnly();
			return default;
		}

		[DoesNotReturn]
		protected sealed override void InsertPoint(int index, Vector2<float> point)
		{
			ThrowReadOnly();
		}

		[DoesNotReturn]
		protected sealed override void ClearPoints()
		{
			ThrowReadOnly();
		}

		[DoesNotReturn]
		protected sealed override bool RemovePoint(Vector2<float> point)
		{
			ThrowReadOnly();
			return default;
		}

		[DoesNotReturn]
		protected sealed override void RemovePointAt(int index)
		{
			ThrowReadOnly();
		}
	}
}
