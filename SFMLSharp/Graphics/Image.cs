using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public unsafe class Image :
		ICollection,
		ICollection<Color>,
		IReadOnlyCollection<Color>,
		ICloneable,
		IDisposable
	{
		internal Native* Handle;

		#region Properties

		public Vector2<uint> Size => sfImage_getSize(Handle);

		internal int Length
		{
			get
			{
				Vector2<uint> size = Size;
				return (int)(size.X * size.Y);
			}
		}

		int ICollection.Count => Length;
		int ICollection<Color>.Count => Length;
		int IReadOnlyCollection<Color>.Count => Length;

		bool ICollection.IsSynchronized => false;
		object ICollection.SyncRoot => this;

		bool ICollection<Color>.IsReadOnly => true;

		public Color this[uint x, uint y]
		{
			get => sfImage_getPixel(Handle, x, y);
			set => sfImage_setPixel(Handle, x, y, value);
		}

		public Color this[Vector2<uint> index]
		{
			get => this[index.X, index.Y];
			set => this[index.X, index.Y] = value;
		}

		#endregion

		#region Constructors

		private Image() { }

		internal Image(Native* handle)
		{
			Handle = handle;
		}

		public Image(uint width, uint height)
			: this(sfImage_create(width, height)) { }

		public Image(Vector2<uint> size) : this(size.X, size.Y) { }

		public Image(uint width, uint height, Color color)
			: this(sfImage_createFromColor(width, height, color)) { }

		public Image(Vector2<uint> size, Color color) : this(size.X, size.Y, color) { }

		public static Image FromFile(string filename)
		{
			Image image = new();

			if (image.TryLoadFromFile(filename)) throw new FileNotFoundException("File could not be loaded", filename);

			return image;
		}

		public static Image FromMemory(ReadOnlySpan<byte> data)
		{
			Image image = new();

			image.TryLoadFromMemory(data);

			return image;
		}

		public static Image FromStream(Stream stream)
		{
			Image image = new();

			image.TryLoadFromStream(stream);

			return image;
		}

		public bool TryLoadFromFile(string filename)
		{
			return CheckAssignHandle(sfImage_createFromFile(filename));
		}

		internal bool TryLoadFromMemory(byte* data, nuint size)
		{
			return CheckAssignHandle(sfImage_createFromMemory(data, size));
		}

		public bool TryLoadFromMemory(ReadOnlySpan<byte> data)
		{
			fixed (byte* data_ptr = data)
			{
				return TryLoadFromMemory(data_ptr, (nuint)data.Length);
			}
		}

		public bool TryLoadFromStream(Stream stream)
		{
			using InputStream inputStream = new(stream);
			return CheckAssignHandle(sfImage_createFromStream(&inputStream));
		}

		private bool CheckAssignHandle(Native* handle)
		{
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

		#endregion

		#region Methods

		internal byte* GetPixelsPointer()
		{
			return sfImage_getPixelsPtr(Handle);
		}

		public Span<byte> GetPixelsByte()
		{
			Vector2<uint> size = Size;
			return new(GetPixelsPointer(), (int)(size.X * size.Y * sizeof(Color)));
		}

		public Span<Color> GetPixels()
		{
			Vector2<uint> size = Size;
			return new(GetPixelsPointer(), (int)(size.X * size.Y));
		}

		public bool TrySaveToFile(string file)
		{
			return sfImage_saveToFile(Handle, file);
		}

		public void CreateMaskFromColor(Color color, byte alpha)
		{
			sfImage_createMaskFromColor(Handle, color, alpha);
		}

		public void CopyImage(Image source, uint destX, uint destY, Rect<int> sourceRect, bool applyAlpha = true)
		{
			sfImage_copyImage(Handle, source.Handle, destX, destY, sourceRect, applyAlpha);
		}

		public void CopyImage(Image source, Vector2<uint> dest, Rect<int> sourceRect, bool applyAlpha = true)
		{
			sfImage_copyImage(Handle, source.Handle, dest.X, dest.Y, sourceRect, applyAlpha);
		}

		public void FlipHorizontally()
		{
			sfImage_flipHorizontally(Handle);
		}

		public void FlipVertically()
		{
			sfImage_flipVertically(Handle);
		}

		#endregion

		#region Interface Methods

		[DoesNotReturn]
		private static void ThrowFixed()
		{
			throw new NotSupportedException("Image has fixed length.");
		}

		void ICollection<Color>.Add(Color item)
		{
			ThrowFixed();
		}

		void ICollection<Color>.Clear()
		{
			ThrowFixed();
		}

		bool ICollection<Color>.Contains(Color item)
		{
			return GetPixels().Contains(item);
		}

		private void CopyTo(Span<Color> array, int arrayIndex)
		{
			GetPixels().CopyTo(array[arrayIndex..]);
		}

		void ICollection<Color>.CopyTo(Color[] array, int arrayIndex)
		{
			CopyTo(array, arrayIndex);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			CopyTo((Color[])array, index);
		}

		bool ICollection<Color>.Remove(Color item)
		{
			ThrowFixed();
			return false;
		}

		public Span<Color>.Enumerator GetEnumerator()
		{
			return GetPixels().GetEnumerator();
		}

		private class Enumerator : IEnumerator<Color>
		{
			private readonly Image _image;
			private uint _index = default;

			public Color Current
			{
				get
				{
					uint width = _image.Size.X;
					return _image[_index % width, _index / width];
				}
			}

			object IEnumerator.Current => Current;

			public Enumerator(Image image)
			{
				_image = image;
			}

			public bool MoveNext()
			{
				if (_index < _image.Length)
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

			private bool _disposed;
			private void Dispose(bool disposing)
			{
				if (_disposed) return;

				if (disposing) Reset();

				_disposed = true;
			}
		}

		private Enumerator GetEnumeratorUnsafe()
		{
			return new(this);
		}

		IEnumerator<Color> IEnumerable<Color>.GetEnumerator()
		{
			return GetEnumeratorUnsafe();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumeratorUnsafe();
		}

		/// <inheritdoc cref="ICloneable.Clone"/>
		public Image Clone()
		{
			return new(sfImage_copy(Handle));
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

		~Image()
		{
			Dispose(disposing: false);
		}

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			if (disposing) sfImage_destroy(Handle);
			_disposed = true;
		}

		#endregion

		#region Imports

		internal readonly struct Native { }

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfImage_create(uint width, uint height);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfImage_createFromColor(uint width, uint height, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfImage_createFromPixels(uint width, uint height, byte* pixels);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		private static extern Native* sfImage_createFromFile(string filename);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfImage_createFromMemory(void* data, nuint size);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfImage_createFromStream(InputStream* stream);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfImage_copy(Native* image);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfImage_destroy(Native* image);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		private static extern bool sfImage_saveToFile(Native* image, string filename);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<uint> sfImage_getSize(Native* image);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfImage_createMaskFromColor(Native* image, Color color, byte alpha);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfImage_copyImage(
			Native* image,
			Native* source,
			uint destX,
			uint destY,
			Rect<int> sourceRect,
			bool applyAlpha);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfImage_setPixel(Native* image, uint x, uint y, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Color sfImage_getPixel(Native* image, uint x, uint y);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern byte* sfImage_getPixelsPtr(Native* image);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfImage_flipHorizontally(Native* image);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfImage_flipVertically(Native* image);

		#endregion
	}
}
