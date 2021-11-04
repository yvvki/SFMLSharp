using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public unsafe class View : ICloneable
	{
		internal Native* Handle;

		#region Properties

		public Vector2F Center
		{
			get => sfView_getCenter(Handle);
			set => sfView_setCenter(Handle, value);
		}

		public Vector2F Size
		{
			get => sfView_getSize(Handle);
			set => sfView_setSize(Handle, value);
		}

		public float Rotation
		{
			get => sfView_getRotation(Handle);
			set => sfView_setRotation(Handle, value);
		}

		public FloatRect Viewport
		{
			get => sfView_getViewport(Handle);
			set => sfView_setViewport(Handle, value);
		}

		#endregion

		#region Constructors

		public View()
		{
			Handle = sfView_create();
		}

		public View(FloatRect rectangle)
		{
			Handle = sfView_createFromRect(rectangle);
		}

		internal View(Native* handle)
		{
			Handle = handle;
		}

		#endregion

		#region Methods

		public void Reset(FloatRect rectangle)
		{
			sfView_reset(Handle, rectangle);
		}

		public void Move(Vector2F offset)
		{
			sfView_move(Handle, offset);
		}

		public void Rotate(float angle)
		{
			sfView_rotate(Handle, angle);
		}

		public void Zoom(float factor)
		{
			sfView_zoom(Handle, factor);
		}

		#endregion

		#region Interface Method Implementations

		/// <inheritdoc cref="ICloneable.Clone"/>
		public View Clone()
		{
			return new(sfView_copy(Handle));
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		~View()
		{
			sfView_destroy(Handle);
		}

		#endregion

		#region Imports

		internal readonly struct Native { }

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfView_create();

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfView_createFromRect(FloatRect rectangle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfView_copy(Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_destroy(Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_setCenter(Native* view, Vector2F center);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_setSize(Native* view, Vector2F size);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_setRotation(Native* view, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_setViewport(Native* view, FloatRect viewport);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_reset(Native* view, FloatRect rectangle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfView_getCenter(Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfView_getSize(Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfView_getRotation(Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern FloatRect sfView_getViewport(Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_move(Native* view, Vector2F offset);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_rotate(Native* view, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfView_zoom(Native* view, float factor);

		#endregion
	}
}
