using System.Reflection.Metadata;
using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	/// <summary>
	///   Represents text styles.
	/// </summary>
	public enum TextStyle : uint
	{
		/// <summary>
		///   Regular characters, no style.
		/// </summary>
		Regular = 0,
		/// <summary>
		///   Bold characters.
		/// </summary>
		Bold = 1 << 0,
		/// <summary>
		///   Italic characters.
		/// </summary>
		Italic = 1 << 1,
		/// <summary>
		///   Underlined characters.
		/// </summary>
		Underlined = 1 << 2,
		/// <summary>
		///   Strike-through characters.
		/// </summary>
		StrikeThrough = 1 << 3
	}

	public unsafe class Text :
		IDrawable,
		ITransformable,
		IColored,
		IFillOutlined,
		IBounds,
		ICloneable,
		IDisposable
	{
		internal readonly Native* Handle;

		#region Properties

		public Vector2<float> Position
		{
			get => sfText_getPosition(Handle);
			set => sfText_setPosition(Handle, value);
		}
		public float Rotation
		{
			get => sfText_getRotation(Handle);
			set => sfText_setRotation(Handle, value);
		}
		public Vector2<float> Scaling
		{
			get => sfText_getScale(Handle);
			set => sfText_setScale(Handle, value);
		}
		public Vector2<float> Origin
		{
			get => sfText_getOrigin(Handle);
			set => sfText_setOrigin(Handle, value);
		}

		public string? String
		{
			get
			{
				uint* value = sfText_getUnicodeString(Handle);
				string? result = UTF32Helper.GetString(value);

				return result;
			}
			set
			{
				byte[]? value_utf32 = UTF32Helper.GetBytes(value);
				fixed (byte* value_ptr = value_utf32)
				{
					sfText_setUnicodeString(Handle, (uint*)value_ptr);
				}
			}
		}

		private Font? _font;
		private Font.Native* FontHandle
		{
			get => sfText_getFont(Handle);
			set => sfText_setFont(Handle, value);
		}
		public Font? Font
		{
			get
			{
				Font.Native* handle = FontHandle;
				if (handle is not null && (_font is null || _font.Handle != handle))
				{
					_font = new Font(handle);
				}

				return _font;
			}
			set
			{
				_font = value;
				if (_font is not null)
				{
					_font.ThrowIfNotCreated();
					FontHandle = _font.Handle;
				}
				else
				{
					FontHandle = null;
				}
			}
		}

		public uint CharacterSize
		{
			get => sfText_getCharacterSize(Handle);
			set => sfText_setCharacterSize(Handle, value);
		}

		public float LineSpacing
		{
			get => sfText_getLineSpacing(Handle);
			set => sfText_setLineSpacing(Handle, value);
		}

		public float LetterSpacing
		{
			get => sfText_getLetterSpacing(Handle);
			set => sfText_setLetterSpacing(Handle, value);
		}

		public TextStyle Style
		{
			get => sfText_getStyle(Handle);
			set => sfText_setStyle(Handle, value);
		}

		public Color Color
		{
			get => sfText_getColor(Handle);
			set => sfText_setColor(Handle, value);
		}
		public Color FillColor
		{
			get => sfText_getFillColor(Handle);
			set => sfText_setFillColor(Handle, value);
		}
		public Color OutlineColor
		{
			get => sfText_getOutlineColor(Handle);
			set => sfText_setOutlineColor(Handle, value);
		}
		public float OutlineThickness
		{
			get => sfText_getOutlineThickness(Handle);
			set => sfText_setOutlineThickness(Handle, value);
		}

		#endregion

		#region Constructors

		internal Text(Native* handle)
		{
			Handle = handle;
		}

		public Text() : this(sfText_create()) { }

		#endregion

		#region Methods

		public Vector2<float> FindCharacterPosition(int index)
		{
			return sfText_findCharacterPos(Handle, (nuint)index);
		}

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
			sfText_move(Handle, offset);
		}

		public void Rotate(float angle)
		{
			sfText_rotate(Handle, angle);
		}

		public void Scale(Vector2<float> factor)
		{
			sfText_scale(Handle, factor);
		}

		public Transform GetTransform()
		{
			return sfText_getTransform(Handle);
		}

		public Transform GetInverseTransform()
		{
			return sfText_getInverseTransform(Handle);
		}

		public Rect<float> GetLocalBounds()
		{
			return sfText_getLocalBounds(Handle);
		}

		public Rect<float> GetGlobalBounds()
		{
			return sfText_getGlobalBounds(Handle);
		}

		public Text Clone()
		{
			return new(sfText_copy(Handle));
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

		~Text() => Dispose(disposing: false);

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing) sfText_destroy(Handle);

			_disposed = true;
		}

		#endregion

		#region Imports

		internal readonly struct Native { }

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfText_create();

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfText_copy(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_destroy(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setPosition(Native* text, Vector2<float> position);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setRotation(Native* text, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setScale(Native* text, Vector2<float> scale);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setOrigin(Native* text, Vector2<float> origin);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfText_getPosition(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfText_getRotation(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfText_getScale(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfText_getOrigin(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_move(Native* text, Vector2<float> offset);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_rotate(Native* text, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_scale(Native* text, Vector2<float> factors);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfText_getTransform(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfText_getInverseTransform(Native* text);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		//private extern static void sfText_setString(Native* text, string @string);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setUnicodeString(Native* text, uint* @string);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setFont(Native* text, Font.Native* font);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setCharacterSize(Native* text, uint size);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setLineSpacing(Native* text, float spacingFactor);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setLetterSpacing(Native* text, float spacingFactor);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setStyle(Native* text, TextStyle style);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setColor(Native* text, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setFillColor(Native* text, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setOutlineColor(Native* text, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfText_setOutlineThickness(Native* text, float thickness);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private extern static char* sfText_getString(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint* sfText_getUnicodeString(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Font.Native* sfText_getFont(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint sfText_getCharacterSize(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfText_getLetterSpacing(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfText_getLineSpacing(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern TextStyle sfText_getStyle(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfText_getColor(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfText_getFillColor(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfText_getOutlineColor(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfText_getOutlineThickness(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfText_findCharacterPos(Native* text, nuint index);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<float> sfText_getLocalBounds(Native* text);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<float> sfText_getGlobalBounds(Native* text);

		#endregion
	}
}
