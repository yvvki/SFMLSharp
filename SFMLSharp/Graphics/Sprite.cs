using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public unsafe class Sprite :
		IDrawable,
		ITransformable,
		ITextured,
		IColored,
		IBounds,
		ICloneable,
		IDisposable
	{
		internal Native* Handle;

		#region Properties

		public Vector2<float> Position
		{
			get => sfSprite_getPosition(Handle);
			set => sfSprite_setPosition(Handle, value);
		}
		public float Rotation
		{
			get => sfSprite_getRotation(Handle);
			set => sfSprite_setRotation(Handle, value);
		}
		public Vector2<float> Scaling
		{
			get => sfSprite_getScale(Handle);
			set => sfSprite_setScale(Handle, value);
		}
		public Vector2<float> Origin
		{
			get => sfSprite_getOrigin(Handle);
			set => sfSprite_setOrigin(Handle, value);
		}

		protected Texture? _texture;
		private protected Texture.Native* TextureHandle
		{
			get => sfSprite_getTexture(Handle);
			set => sfSprite_setTexture(Handle, value, false);
		}
		public Texture? Texture
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
		public Rect<int> TextureRect
		{
			get => sfSprite_getTextureRect(Handle);
			set => sfSprite_setTextureRect(Handle, value);
		}

		public Color Color
		{
			get => sfSprite_getColor(Handle);
			set => sfSprite_setColor(Handle, value);
		}

		#endregion

		#region Constructors

		internal Sprite(Native* handle)
		{
			Handle = handle;
		}

		public Sprite() : this(sfSprite_create()) { }

		#endregion

		#region Methods

		void IDrawable.DrawNullable(IRenderTarget target, RenderStates? states)
		{
			DrawNullable(target, states);
		}

		private void DrawNullable(IRenderTarget target, RenderStates? states = null)
		{
			fixed (RenderStates.Native* states_ptr = states)
			{
				target.Draw(this, states_ptr);
			}
		}

		public void Draw(IRenderTarget target, RenderStates? states)
		{
			DrawNullable(target, states);
		}

		#endregion

		#region Interface Methods

		public void Move(Vector2<float> offset)
		{
			sfSprite_move(Handle, offset);
		}

		public void Rotate(float angle)
		{
			sfSprite_rotate(Handle, angle);
		}

		public void Scale(Vector2<float> factor)
		{
			sfSprite_scale(Handle, factor);
		}

		public Transform GetTransform()
		{
			return sfSprite_getTransform(Handle);
		}

		public Transform GetInverseTransform()
		{
			return sfSprite_getInverseTransform(Handle);
		}

		public Rect<float> GetLocalBounds()
		{
			return sfSprite_getLocalBounds(Handle);
		}

		public Rect<float> GetGlobalBounds()
		{
			return sfSprite_getGlobalBounds(Handle);
		}

		public Sprite Clone()
		{
			return new(sfSprite_copy(Handle));
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Sprite() => Dispose(disposing: false);

		private bool _disposed;
		protected void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing) sfSprite_destroy(Handle);

			_disposed = true;
		}

		#endregion

		#region Imports

		internal readonly struct Native { }

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfSprite_create();

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfSprite_copy(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_destroy(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_setPosition(Native* sprite, Vector2<float> position);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_setRotation(Native* sprite, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_setScale(Native* sprite, Vector2<float> scale);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_setOrigin(Native* sprite, Vector2<float> origin);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfSprite_getPosition(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfSprite_getRotation(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfSprite_getScale(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfSprite_getOrigin(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_move(Native* sprite, Vector2<float> offset);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_rotate(Native* sprite, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_scale(Native* sprite, Vector2<float> factors);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfSprite_getTransform(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfSprite_getInverseTransform(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_setTexture(Native* sprite, Texture.Native* texture, bool resetRect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_setTextureRect(Native* sprite, Rect<int> rect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSprite_setColor(Native* sprite, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Texture.Native* sfSprite_getTexture(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<int> sfSprite_getTextureRect(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfSprite_getColor(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<float> sfSprite_getLocalBounds(Native* sprite);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<float> sfSprite_getGlobalBounds(Native* sprite);

		#endregion
	}
}
