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

		protected sealed override void SetPoint(int index, Vector2F value)
		{
			ThrowReadOnly();
		}

		protected sealed override int AddPoint(Vector2F point)
		{
			ThrowReadOnly();
			return default;
		}

		protected sealed override void InsertPoint(int index, Vector2F point)
		{
			ThrowReadOnly();
		}

		protected sealed override void ClearPoints()
		{
			ThrowReadOnly();
		}

		protected sealed override bool RemovePoint(Vector2F point)
		{
			ThrowReadOnly();
			return default;
		}

		protected sealed override void RemovePointAt(int index)
		{
			ThrowReadOnly();
		}
	}
}
