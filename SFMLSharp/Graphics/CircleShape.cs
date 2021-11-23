using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public unsafe class CircleShape :
		ReadOnlyShape,
		ICloneable
	{
		#region Properties

		public int Count
		{
			get => GetPointCount();
			set => sfCircleShape_setPointCount(Handle, (nuint)value);
		}

		public Vector2<float> this[int index] => GetPoint(index);

		protected override bool IsFixedSize => true;

		public float Radius
		{
			get => sfCircleShape_getRadius(Handle);
			set => sfCircleShape_setRadius(Handle, value);
		}

		public override Vector2<float> Position
		{
			get => sfCircleShape_getPosition(Handle);
			set => sfCircleShape_setPosition(Handle, value);
		}
		public override float Rotation
		{
			get => sfCircleShape_getRotation(Handle);
			set => sfCircleShape_setRotation(Handle, value);
		}
		public override Vector2<float> Scaling
		{
			get => sfCircleShape_getScale(Handle);
			set => sfCircleShape_setScale(Handle, value);
		}
		public override Vector2<float> Origin
		{
			get => sfCircleShape_getOrigin(Handle);
			set => sfCircleShape_setOrigin(Handle, value);
		}

		public override Texture? Texture
		{
			get
			{
				Texture.Native* handle = sfCircleShape_getTexture(Handle);
				if (handle is not null && (_texture is null || _texture.Handle != handle))
				{
					_texture = new(handle);
				}
				return _texture;
			}
			set
			{
				_texture = value;
				sfCircleShape_setTexture(Handle, value!.Handle, false);
			}
		}
		public override Rect<int> TextureRect
		{
			get => sfCircleShape_getTextureRect(Handle);
			set => sfCircleShape_setTextureRect(Handle, value);
		}

		public override Color FillColor
		{
			get => sfCircleShape_getFillColor(Handle);
			set => sfCircleShape_setFillColor(Handle, value);
		}
		public override Color OutlineColor
		{
			get => sfCircleShape_getOutlineColor(Handle);
			set => sfCircleShape_setOutlineColor(Handle, value);
		}
		public override float OutlineThickness
		{
			get => sfCircleShape_getOutlineThickness(Handle);
			set => sfCircleShape_setOutlineThickness(Handle, value);
		}

		#endregion

		#region Constructors

		internal CircleShape(Native* handle) : base(handle) { }

		public CircleShape() : base() { }

		protected override void OnCreate()
		{
			Handle = sfCircleShape_create();
		}

		#endregion

		#region Methods

		protected override int GetPointCount()
		{
			return (int)GetPointCountNuint();
		}

		private protected override nuint GetPointCountNuint()
		{
			return sfCircleShape_getPointCount(Handle);
		}

		protected override Vector2<float> GetPoint(int index)
		{
			return GetPointNuint((nuint)index);
		}

		private protected override Vector2<float> GetPointNuint(nuint index)
		{
			return sfCircleShape_getPoint(Handle, index);
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

		#region Interface Methods

		public override void Move(Vector2<float> offset)
		{
			sfCircleShape_move(Handle, offset);
		}

		public override void Rotate(float angle)
		{
			sfCircleShape_rotate(Handle, angle);
		}

		public override void Scale(Vector2<float> factor)
		{
			sfCircleShape_scale(Handle, factor);
		}

		public override Transform GetTransform()
		{
			return sfCircleShape_getTransform(Handle);
		}

		public override Transform GetInverseTransform()
		{
			return sfCircleShape_getInverseTransform(Handle);
		}

		public override Rect<float> GetLocalBounds()
		{
			return sfCircleShape_getLocalBounds(Handle);
		}

		public override Rect<float> GetGlobalBounds()
		{
			return sfCircleShape_getGlobalBounds(Handle);
		}

		public RectangleShape Clone()
		{
			return new(sfCircleShape_copy(Handle));
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		private protected override void OnDispose()
		{
			sfCircleShape_destroy(Handle);
		}

		#endregion

		#region Imports

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfCircleShape_create();

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfCircleShape_copy(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_destroy(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setPosition(Native* shape, Vector2<float> position);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setRotation(Native* shape, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setScale(Native* shape, Vector2<float> scale);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setOrigin(Native* shape, Vector2<float> origin);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfCircleShape_getPosition(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfCircleShape_getRotation(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfCircleShape_getScale(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfCircleShape_getOrigin(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_move(Native* shape, Vector2<float> offset);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_rotate(Native* shape, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_scale(Native* shape, Vector2<float> factors);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfCircleShape_getTransform(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfCircleShape_getInverseTransform(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setTexture(Native* shape, Texture.Native* texture, bool resetRect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setTextureRect(Native* shape, Rect<int> rect);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setFillColor(Native* shape, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setOutlineColor(Native* shape, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setOutlineThickness(Native* shape, float thickness);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Texture.Native* sfCircleShape_getTexture(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<int> sfCircleShape_getTextureRect(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfCircleShape_getFillColor(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfCircleShape_getOutlineColor(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfCircleShape_getOutlineThickness(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern nuint sfCircleShape_getPointCount(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfCircleShape_getPoint(Native* shape, nuint index);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setRadius(Native* shape, float size);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfCircleShape_getRadius(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCircleShape_setPointCount(Native* shape, nuint count);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<float> sfCircleShape_getLocalBounds(Native* shape);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<float> sfCircleShape_getGlobalBounds(Native* shape);

		#endregion
	}
}
