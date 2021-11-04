using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public unsafe class RectangleShape :
		ReadOnlyShape,
		ICloneable
	{
		#region Properties

		public int Count => GetPointCount();

		public Vector2F this[int index] => GetPoint(index);

		protected override bool IsFixedSize => true;

		public Vector2F Size
		{
			get => sfRectangleShape_getSize(Handle);
			set => sfRectangleShape_setSize(Handle, value);
		}

		public override Vector2F Position
		{
			get => sfRectangleShape_getPosition(Handle);
			set => sfRectangleShape_setPosition(Handle, value);
		}
		public override float Rotation
		{
			get => sfRectangleShape_getRotation(Handle);
			set => sfRectangleShape_setRotation(Handle, value);
		}
		public override Vector2F Scaling
		{
			get => sfRectangleShape_getScale(Handle);
			set => sfRectangleShape_setScale(Handle, value);
		}
		public override Vector2F Origin
		{
			get => sfRectangleShape_getOrigin(Handle);
			set => sfRectangleShape_setOrigin(Handle, value);
		}

		public override Texture? Texture
		{
			get
			{
				Texture.Native* handle = sfRectangleShape_getTexture(Handle);
				if (handle is not null && (_texture is null || _texture.Handle != handle))
				{
					_texture = new(handle);
				}
				return _texture;
			}
			set
			{
				_texture = value;
				sfRectangleShape_setTexture(Handle, value!.Handle, false);
			}
		}
		public override IntRect TextureRect
		{
			get => sfRectangleShape_getTextureRect(Handle);
			set => sfRectangleShape_setTextureRect(Handle, value);
		}

		public override Color FillColor
		{
			get => sfRectangleShape_getFillColor(Handle);
			set => sfRectangleShape_setFillColor(Handle, value);
		}
		public override Color OutlineColor
		{
			get => sfRectangleShape_getOutlineColor(Handle);
			set => sfRectangleShape_setOutlineColor(Handle, value);
		}
		public override float OutlineThickness
		{
			get => sfRectangleShape_getOutlineThickness(Handle);
			set => sfRectangleShape_setOutlineThickness(Handle, value);
		}

		#endregion

		#region Constructors

		internal RectangleShape(Native* handle) : base(handle) { }

		public RectangleShape() : base() { }

		protected override void OnCreate()
		{
			Handle = sfRectangleShape_create();
		}

		#endregion

		#region Methods

		protected override int GetPointCount()
		{
			return (int)GetPointCountNuint();
		}

		private protected override nuint GetPointCountNuint()
		{
			return sfRectangleShape_getPointCount(Handle);
		}

		protected override Vector2F GetPoint(int index)
		{
			return GetPointNuint((nuint)index);
		}

		private protected override Vector2F GetPointNuint(nuint index)
		{
			return sfRectangleShape_getPoint(Handle, index);
		}

		private protected override void DrawNullable(IRenderTarget target, RenderStates? states = null)
		{
			fixed (RenderStates.Native* states_ptr = states)
			{
				target.Draw(this, states_ptr);
			}
		}

		public override void Draw(IRenderTarget target, RenderStates states)
		{
			DrawNullable(target, states);
		}

		#endregion

		#region Interface Method Implementations

		public override void Move(Vector2F offset)
		{
			sfRectangleShape_move(Handle, offset);
		}

		public override void Rotate(float angle)
		{
			sfRectangleShape_rotate(Handle, angle);
		}

		public override void Scale(Vector2F factor)
		{
			sfRectangleShape_scale(Handle, factor);
		}

		public override Transform GetTransform()
		{
			return sfRectangleShape_getTransform(Handle);
		}

		public override Transform GetInverseTransform()
		{
			return sfRectangleShape_getInverseTransform(Handle);
		}

		public override FloatRect GetLocalBounds()
		{
			return sfRectangleShape_getLocalBounds(Handle);
		}

		public override FloatRect GetGlobalBounds()
		{
			return sfRectangleShape_getGlobalBounds(Handle);
		}

		public RectangleShape Clone()
		{
			return new(sfRectangleShape_copy(Handle));
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		private protected override void OnDispose()
		{
			sfRectangleShape_destroy(Handle);
		}

		#endregion

		#region Imports

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfRectangleShape_create();

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfRectangleShape_copy(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_destroy(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setPosition(Native* shape, Vector2F position);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setRotation(Native* shape, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setScale(Native* shape, Vector2F scale);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setOrigin(Native* shape, Vector2F origin);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfRectangleShape_getPosition(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfRectangleShape_getRotation(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfRectangleShape_getScale(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfRectangleShape_getOrigin(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_move(Native* shape, Vector2F offset);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_rotate(Native* shape, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_scale(Native* shape, Vector2F factors);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfRectangleShape_getTransform(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfRectangleShape_getInverseTransform(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setTexture(Native* shape, Texture.Native* texture, bool resetRect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setTextureRect(Native* shape, IntRect rect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setFillColor(Native* shape, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setOutlineColor(Native* shape, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setOutlineThickness(Native* shape, float thickness);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Texture.Native* sfRectangleShape_getTexture(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntRect sfRectangleShape_getTextureRect(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfRectangleShape_getFillColor(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfRectangleShape_getOutlineColor(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfRectangleShape_getOutlineThickness(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern nuint sfRectangleShape_getPointCount(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfRectangleShape_getPoint(Native* shape, nuint index);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfRectangleShape_setSize(Native* shape, Vector2F size);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2F sfRectangleShape_getSize(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern FloatRect sfRectangleShape_getLocalBounds(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern FloatRect sfRectangleShape_getGlobalBounds(Native* shape);

		#endregion
	}
}
