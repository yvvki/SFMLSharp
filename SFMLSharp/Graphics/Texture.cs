using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public enum TextureCoordinate
	{
		Normalized,
		Pixels
	}

	public unsafe class Texture :
		ICloneable,
		IDisposable
	{
		internal Native* Handle;

		#region Properties

		public static readonly uint MaxSize = sfTexture_getMaximumSize();

		public Vector2<uint> Size => sfTexture_getSize(Handle);

		internal int Length
		{
			get
			{
				Vector2<uint> size = Size;
				return (int)(size.X * size.Y);
			}
		}

		public bool IsSmooth
		{
			get => sfTexture_isSmooth(Handle);
			set => sfTexture_setSmooth(Handle, value);
		}

		public bool IsSrgb
		{
			get => sfTexture_isSrgb(Handle);
			set => sfTexture_setSrgb(Handle, value);
		}

		public bool IsRepeated
		{
			get => sfTexture_isRepeated(Handle);
			set => sfTexture_setRepeated(Handle, value);
		}

		#endregion

		#region Constructors

		private Texture() { }

		internal Texture(Native* handle)
		{
			Handle = handle;
		}

		public Texture(uint width, uint height)
			: this(sfTexture_create(width, height)) { }

		public Texture(Vector2<uint> size) : this(size.X, size.Y) { }

		public static Texture FromFile(string filename, Rect<int>? area = null)
		{
			Rect<int> area_notnull = area ?? default;
			return new(sfTexture_createFromFile(filename, area.HasValue ? &area_notnull : null));
		}

		public static Texture FromMemory(ReadOnlySpan<byte> data, Rect<int>? area = null)
		{
			Rect<int> area_notnull = area ?? default;
			fixed (byte* data_ptr = data)
			{
				return new(sfTexture_createFromMemory(data_ptr, (nuint)data.Length, area.HasValue ? &area_notnull : null));
			}
		}

		public static Texture FromStream(Stream stream, Rect<int>? area = null)
		{
			using InputStream inputStream = new(stream);
			Rect<int> area_notnull = area ?? default;
			return new(sfTexture_createFromStream(&inputStream, area.HasValue ? &area_notnull : null));
		}

		public static Texture FromImage(Image image, Rect<int>? area = null)
		{
			Rect<int> area_notnull = area ?? default;
			return new(sfTexture_createFromImage(image.Handle, area.HasValue ? &area_notnull : null));
		}

		#endregion

		#region Methods

		public Image ToImage()
		{
			return new(sfTexture_copyToImage(Handle));
		}

		internal void Update(byte* pixels, uint width, uint height, uint x, uint y)
		{
			sfTexture_updateFromPixels(
					Handle,
					pixels,
					width,
					height,
					x,
					y);
		}

		public void Update(ReadOnlySpan<Color> pixels, uint width, uint height, uint x, uint y)
		{
			fixed (Color* pixels_ptr = pixels)
			{
				Update(
					(byte*)pixels_ptr,
					width,
					height,
					x,
					y);
			}
		}

		public void Update(ReadOnlySpan<Color> pixels, Vector2<uint> size, uint x, uint y)
		{
			Update(
				pixels,
				size.X,
				size.Y,
				x,
				y);
		}

		public void Update(ReadOnlySpan<Color> pixels, uint width, uint height, Vector2<uint> offset)
		{
			Update(
				pixels,
				width,
				height,
				offset.X,
				offset.Y);
		}

		public void Update(ReadOnlySpan<Color> pixels, Vector2<uint> size, Vector2<uint> offset)
		{
			Update(
				pixels,
				size.X,
				size.Y,
				offset.X,
				offset.Y);
		}

		public void Update(Texture texture, uint x, uint y)
		{
			sfTexture_updateFromTexture(Handle, texture.Handle, x, y);
		}

		public void Update(Texture texture, Vector2<uint> offset)
		{
			Update(texture, offset.X, offset.Y);
		}

		public void Update(Image image, uint x, uint y)
		{
			sfTexture_updateFromImage(Handle, image.Handle, x, y);
		}

		public void Update(Image image, Vector2<uint> offset)
		{
			Update(image, offset.X, offset.Y);
		}

		public void Update(Window.Window window, uint x, uint y)
		{
			sfTexture_updateFromWindow(Handle, window.Handle, x, y);
		}

		public void Update(Window.Window window, Vector2<uint> offset)
		{
			Update(window, offset.X, offset.Y);
		}

		public bool GenerateMipmap()
		{
			return sfTexture_generateMipmap(Handle);
		}

		public static void Swap(Texture left, Texture right)
		{
			sfTexture_swap(left.Handle, right.Handle);
		}

		public void Swap(Texture other)
		{
			Swap(this, other);
		}

		public uint GetNativeHandle()
		{
			return sfTexture_getNativeHandle(Handle);
		}

		public void Bind()
		{
			sfTexture_bind(Handle);
		}

		#endregion

		#region Interface Methods

		public Texture Clone()
		{
			return new(sfTexture_copy(Handle));
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

		~Texture() => Dispose(disposing: false);

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing) sfTexture_destroy(Handle);

			_disposed = true;
		}

		#endregion

		#region Cast Operators

		public static explicit operator Texture(Image value)
		{
			return FromImage(value);
		}

		public static explicit operator Image(Texture value)
		{
			return value.ToImage();
		}

		#endregion

		#region Imports

		internal readonly struct Native { }

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfTexture_create(uint width, uint height);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern Native* sfTexture_createFromFile(string filename, Rect<int>* area);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfTexture_createFromMemory(void* data, nuint sizeInBytes, Rect<int>* area);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfTexture_createFromStream(InputStream* stream, Rect<int>* area);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfTexture_createFromImage(Image.Native* image, Rect<int>* area);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfTexture_copy(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_destroy(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<uint> sfTexture_getSize(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Image.Native* sfTexture_copyToImage(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_updateFromPixels(Native* texture, byte* pixels, uint width, uint height, uint x, uint y);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_updateFromTexture(Native* destination, Native* source, uint x, uint y);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_updateFromImage(Native* texture, Image.Native* image, uint x, uint y);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_updateFromWindow(Native* texture, Window.Window.Native* window, uint x, uint y);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_setSmooth(Native* texture, bool smooth);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfTexture_isSmooth(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_setSrgb(Native* texture, bool sRgb);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfTexture_isSrgb(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_setRepeated(Native* texture, bool repeated);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfTexture_isRepeated(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfTexture_generateMipmap(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_swap(Native* left, Native* right);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint sfTexture_getNativeHandle(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTexture_bind(Native* texture);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint sfTexture_getMaximumSize();

		#endregion
	}
}
