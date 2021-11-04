using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public abstract unsafe class Shape :
		IList,
		IList<Vector2F>,
		IReadOnlyList<Vector2F>,
		IDrawable,
		ITransformable,
		ITextured,
		IFillOutlined,
		IBounds,
		IDisposable
	{
		private protected GCHandle _GCHandle;
		internal Native* Handle;

		#region Properties

		int ICollection.Count => GetPointCount();
		int ICollection<Vector2F>.Count => GetPointCount();
		int IReadOnlyCollection<Vector2F>.Count => GetPointCount();

		object? IList.this[int index]
		{
			get => GetPoint(index);
			set => SetPoint(index, (Vector2F)value!);
		}
		Vector2F IList<Vector2F>.this[int index]
		{
			get => GetPoint(index);
			set => SetPoint(index, value);
		}
		Vector2F IReadOnlyList<Vector2F>.this[int index] => GetPoint(index);

		bool ICollection.IsSynchronized => false;
		object ICollection.SyncRoot => this;

		protected abstract bool IsReadOnly { get; }
		bool IList.IsReadOnly => IsReadOnly;
		bool ICollection<Vector2F>.IsReadOnly => IsReadOnly;

		protected abstract bool IsFixedSize { get; }
		bool IList.IsFixedSize => IsFixedSize;

		public virtual Vector2F Position
		{
			get => sfShape_getPosition(Handle);
			set => sfShape_setPosition(Handle, value);
		}
		public virtual float Rotation
		{
			get => sfShape_getRotation(Handle);
			set => sfShape_setRotation(Handle, value);
		}
		public virtual Vector2F Scaling
		{
			get => sfShape_getScale(Handle);
			set => sfShape_setScale(Handle, value);
		}
		public virtual Vector2F Origin
		{
			get => sfShape_getOrigin(Handle);
			set => sfShape_setOrigin(Handle, value);
		}

		protected Texture? _texture;
		private protected virtual Texture.Native* TextureHandle
		{
			get => sfShape_getTexture(Handle);
			set => sfShape_setTexture(Handle, value, false);
		}
		public virtual Texture? Texture
		{
			get
			{
				Texture.Native* handle = TextureHandle;
				if (handle is not null && (_texture is null || _texture.Handle != handle))
				{
					_texture = new(handle);
				}
				return _texture;
			}
			set
			{
				_texture = value;
				TextureHandle = _texture!.Handle;
			}
		}
		public virtual IntRect TextureRect
		{
			get => sfShape_getTextureRect(Handle);
			set => sfShape_setTextureRect(Handle, value);
		}

		public virtual Color FillColor
		{
			get => sfShape_getFillColor(Handle);
			set => sfShape_setFillColor(Handle, value);
		}
		public virtual Color OutlineColor
		{
			get => sfShape_getOutlineColor(Handle);
			set => sfShape_setOutlineColor(Handle, value);
		}
		public virtual float OutlineThickness
		{
			get => sfShape_getOutlineThickness(Handle);
			set => sfShape_setOutlineThickness(Handle, value);
		}

		#endregion

		#region Constructor

		internal Shape(Native* handle)
		{
			Handle = handle;
		}

		protected Shape()
		{
			OnCreate();
		}

		protected virtual void OnCreate()
		{
			_GCHandle = GCHandle.Alloc(this);
			Handle = sfShape_create(
				&GetPointCount,
				&GetPoint,
				GCHandle.ToIntPtr(_GCHandle));
		}

		#endregion

		#region Methods

		protected abstract int GetPointCount();

		private protected virtual nuint GetPointCountNuint()
		{
			return (nuint)GetPointCount();
		}

		protected abstract Vector2F GetPoint(int index);

		private protected virtual Vector2F GetPointNuint(nuint index)
		{
			return GetPoint((int)index);
		}

		//public virtual Vector2F[] GetPoints()
		//{
		//	nuint count = GetPointCountNuint();
		//	Vector2F[] array = new Vector2F[count];
		//	for (nuint i = 0; i < count; i++)
		//	{
		//		array[i] = GetPointNuint(i);
		//	}

		//	return array;
		//}

		protected abstract void SetPoint(int index, Vector2F value);

		protected void Update()
		{
			sfShape_update(Handle);
		}

		void IDrawable.DrawNullable(IRenderTarget target, RenderStates? states)
		{
			DrawNullable(target, states);
		}

		private protected virtual void DrawNullable(IRenderTarget target, RenderStates? states = null)
		{
			fixed (RenderStates.Native* states_ptr = states)
			{
				target.Draw(this, states_ptr);
			}
		}

		public virtual void Draw(IRenderTarget target, RenderStates states)
		{
			DrawNullable(target, states);
		}

		#endregion

		#region Interface Method Implementations

		protected abstract int AddPoint(Vector2F point);
		int IList.Add(object? value)
		{
			return AddPoint((Vector2F)value!);
		}
		void ICollection<Vector2F>.Add(Vector2F item)
		{
			AddPoint(item);
		}

		protected abstract void InsertPoint(int index, Vector2F point);
		void IList.Insert(int index, object? value)
		{
			InsertPoint(index, (Vector2F)value!);
		}
		void IList<Vector2F>.Insert(int index, Vector2F item)
		{
			InsertPoint(index, item);
		}

		protected abstract bool RemovePoint(Vector2F point);
		void IList.Remove(object? value)
		{
			RemovePoint((Vector2F)value!);
		}
		bool ICollection<Vector2F>.Remove(Vector2F item)
		{
			return RemovePoint(item);
		}

		protected abstract void RemovePointAt(int index);
		void IList.RemoveAt(int index)
		{
			RemovePointAt(index);
		}
		void IList<Vector2F>.RemoveAt(int index)
		{
			RemovePointAt(index);
		}

		protected abstract void ClearPoints();
		void IList.Clear()
		{
			ClearPoints();
		}
		void ICollection<Vector2F>.Clear()
		{
			ClearPoints();
		}

		protected virtual int IndexPointOf(Vector2F point)
		{
			var found = this
				.Select((o, i) => new { o, i })
				.FirstOrDefault(x => x.o.Equals(point));
			return found != null ? found.i : -1;
		}
		int IList.IndexOf(object? value)
		{
			return IndexPointOf((Vector2F)value!);
		}
		int IList<Vector2F>.IndexOf(Vector2F item)
		{
			return IndexPointOf(item);
		}

		protected virtual bool ContainsPoint(Vector2F point)
		{
			return IndexPointOf(point) is not -1;
		}
		bool IList.Contains([NotNullWhen(true)] object? value)
		{
			return ContainsPoint((Vector2F)value!);
		}
		bool ICollection<Vector2F>.Contains(Vector2F item)
		{
			return ContainsPoint(item);
		}

		protected virtual void CopyPointsTo(Vector2F[] array, int arrayIndex)
		{
			nuint count = GetPointCountNuint();

			for (nuint i = 0; i < count; i++)
			{
				array[i + (nuint)arrayIndex] = GetPointNuint(i);
			}
		}
		void ICollection.CopyTo(Array array, int index)
		{
			CopyPointsTo((Vector2F[])array, index);
		}
		void ICollection<Vector2F>.CopyTo(Vector2F[] array, int arrayIndex)
		{
			CopyPointsTo(array, arrayIndex);
		}

		public virtual void Move(Vector2F offset)
		{
			sfShape_move(Handle, offset);
		}

		public virtual void Rotate(float angle)
		{
			sfShape_rotate(Handle, angle);
		}

		public virtual void Scale(Vector2F factor)
		{
			sfShape_scale(Handle, factor);
		}

		public virtual Transform GetTransform()
		{
			return sfShape_getTransform(Handle);
		}

		public virtual Transform GetInverseTransform()
		{
			return sfShape_getInverseTransform(Handle);
		}

		public virtual FloatRect GetLocalBounds()
		{
			return sfShape_getLocalBounds(Handle);
		}

		public virtual FloatRect GetGlobalBounds()
		{
			return sfShape_getGlobalBounds(Handle);
		}

		public class Enumerator : IEnumerator<Vector2F>
		{
			private readonly Shape _shape;
			private nuint _index = default;

			public Vector2F Current => _shape.GetPointNuint(_index);
			object IEnumerator.Current => Current;

			public Enumerator(Shape shape)
			{
				_shape = shape;
			}

			public bool MoveNext()
			{
				if (_index < _shape.GetPointCountNuint())
				{
					_index++;
					return true;
				}
				else
				{
					return false;
				}
			}

			public void Reset()
			{
				_index = default;
			}

			public void Dispose()
			{
				Dispose(disposing: true);
				GC.SuppressFinalize(this);
			}

			~Enumerator() => Dispose(disposing: false);

			private bool _disposed;
			protected virtual void Dispose(bool disposing)
			{
				if (_disposed) return;

				if (disposing) Reset();

				_disposed = true;
			}
		}

		public Enumerator GetEnumerator()
		{
			return new(this);
		}
		IEnumerator<Vector2F> IEnumerable<Vector2F>.GetEnumerator()
		{
			return GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Shape() => Dispose(disposing: false);

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing) OnDispose();

			_disposed = true;
		}

		private protected virtual void OnDispose()
		{
			sfShape_destroy(Handle);
			if (_GCHandle.IsAllocated) _GCHandle.Free();
		}

		#endregion

		#region Imports

		internal readonly struct Native { }

		[UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
		private static nuint GetPointCount(void* userData)
		{
			return GetTarget((Native*)userData).GetPointCountNuint();
		}

		[UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
		private static Vector2F GetPoint(nuint index, void* userData)
		{
			return GetTarget((Native*)userData).GetPointNuint(index);
		}

		private static Shape GetTarget(Native* value)
		{
			return (Shape)GCHandle.FromIntPtr((IntPtr)value).Target!;
		}

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfShape_create(
			delegate* unmanaged[Cdecl]<void*, nuint> getPointCount,
			delegate* unmanaged[Cdecl]<nuint, void*, Vector2F> getPoint,
			IntPtr userData);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_destroy(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setPosition(Native* shape, Vector2F position);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setRotation(Native* shape, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setScale(Native* shape, Vector2F scale);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setOrigin(Native* shape, Vector2F origin);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfShape_getPosition(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfShape_getRotation(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfShape_getScale(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfShape_getOrigin(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_move(Native* shape, Vector2F offset);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_rotate(Native* shape, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_scale(Native* shape, Vector2F factors);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfShape_getTransform(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfShape_getInverseTransform(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setTexture(Native* shape, Texture.Native* texture, bool resetRect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setTextureRect(Native* shape, IntRect rect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setFillColor(Native* shape, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setOutlineColor(Native* shape, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_setOutlineThickness(Native* shape, float thickness);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Texture.Native* sfShape_getTexture(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntRect sfShape_getTextureRect(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfShape_getFillColor(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfShape_getOutlineColor(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfShape_getOutlineThickness(Native* shape);

		//// These should be on the derivied classes.
		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private static extern nuint sfShape_getPointCount(Native* shape);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private static extern Vector2F sfShape_getPoint(Native* shape, nuint index);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern FloatRect sfShape_getLocalBounds(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern FloatRect sfShape_getGlobalBounds(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShape_update(Native* shape);

		#endregion
	}
}
