using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	/// <summary>
	///   Represents various information about a font.
	/// </summary>
	/// <param name="Family">The name of the font family.</param>
	public record struct FontInfo(string Family);

	/// <summary>
	///   Represents a glyph (that is, a visual character).
	/// </summary>
	/// <param name="Advance">Offset to move horizontically to the next character.</param>
	/// <param name="Bounds">Bounding rectangle of the glyph, in coordinates relative to the baseline.</param>
	/// <param name="TextureRect">Texture coordinates of the glyph inside the font's image.</param>
	public record struct Glyph(float Advance, FloatRect Bounds, IntRect TextureRect);

	public unsafe class Font :
		ICloneable,
		IDisposable
	{
		internal Native* Handle;

		public FontInfo Info => sfFont_getInfo(Handle);

		internal Font(Native* handle)
		{
			Handle = handle;
		}

		public Font() { }

		public bool TryLoadFromFile(string filename)
		{
			Native* handle = sfFont_createFromFile(filename);

			if (handle is not null)
			{
				Handle = handle;
				return true;
			}
			else
			{
				return false;
			}
		}

		internal bool TryLoadFromMemory(byte* data, nuint sizeInBytes)
		{
			Native* handle = sfFont_createFromMemory(data, sizeInBytes);

			if (handle is not null)
			{
				Handle = handle;
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool TryLoadFromMemory(ReadOnlySpan<byte> data)
		{
			fixed (byte* data_ptr = data) return TryLoadFromMemory(data_ptr, (nuint)data.Length);
		}

		public bool TryLoadFromStream(Stream stream)
		{
			using InputStream inputStream = new(stream);
			Native* handle = sfFont_createFromStream(&inputStream);

			if (handle is not null)
			{
				Handle = handle;
				return true;
			}
			else
			{
				return false;
			}
		}

		public Glyph GetGlyph(uint codePoint, uint characterSize, bool bold = default, float outlineThickness = default)
		{
			return sfFont_getGlyph(Handle,
				codePoint,
				characterSize,
				bold,
				outlineThickness);
		}

		//public Glyph GetGlyph(string character, uint characterSize, bool bold, float outlineThickness)
		//{
		//	return GetGlyph(
		//		 (uint)char.ConvertToUtf32(character, 0),
		//		 characterSize,
		//		 bold,
		//		 outlineThickness);
		//}

		//public Glyph GetGlyph(char highSurrogate, char lowSurrogate, uint characterSize, bool bold, float outlineThickness)
		//{
		//	return GetGlyph(
		//		 (uint)char.ConvertToUtf32(highSurrogate, lowSurrogate),
		//		 characterSize,
		//		 bold,
		//		 outlineThickness);
		//}

		public float GetKerning(uint first, uint second, uint characterSize)
		{
			return sfFont_getKerning(Handle, first, second, characterSize);
		}

		public float GetLineSpacing(uint characterSize)
		{
			return sfFont_getLineSpacing(Handle, characterSize);
		}

		public float GetUnderlinePosition(uint characterSize)
		{
			return sfFont_getUnderlinePosition(Handle, characterSize);
		}

		public float GetUnderlineThickness(uint characterSize)
		{
			return sfFont_getUnderlineThickness(Handle, characterSize);
		}

		public Texture GetTexture(uint characterSize)
		{
			return new(sfFont_getTexture(Handle, characterSize));
		}

		public Font Clone()
		{
			return new(sfFont_copy(Handle));
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

		~Font() => Dispose(disposing: false);

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing) sfFont_destroy(Handle);

			_disposed = true;
		}

		#region Imports

		internal readonly struct Native { }

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern Native* sfFont_createFromFile(string filename);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfFont_createFromMemory(void* data, nuint sizeInBytes);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfFont_createFromStream(InputStream* stream);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfFont_copy(Native* font);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfFont_destroy(Native* font);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Glyph sfFont_getGlyph(Native* font, uint codePoint, uint characterSize, bool bold, float outlineThickness);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfFont_getKerning(Native* font, uint first, uint second, uint characterSize);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfFont_getLineSpacing(Native* font, uint characterSize);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfFont_getUnderlinePosition(Native* font, uint characterSize);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfFont_getUnderlineThickness(Native* font, uint characterSize);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Texture.Native* sfFont_getTexture(Native* font, uint characterSize);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern FontInfo sfFont_getInfo(Native* font);

		#endregion
	}
}
