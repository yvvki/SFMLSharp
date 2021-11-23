using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Window.DllName;

namespace SFML.Window
{
	/// <summary>
	///   Enumeration of the native system cursor types.
	/// </summary>
	/// <include file='Cursor.xml' path='doc/cursor[@name="Type"]' />
	public enum CursorType
	{
		/// <summary>
		///   Arrow cursor (default).
		/// </summary>
		Arrow,
		/// <summary>
		///   Busy arrow cursor.
		/// </summary>
		ArrowWait,
		/// <summary>
		///   Busy cursor.
		/// </summary>
		Wait,
		/// <summary>
		///   I-beam, cursor when hovering over a field allowing text entry.
		/// </summary>
		Text,
		/// <summary>
		///   Pointing hand cursor.
		/// </summary>
		Hand,
		/// <summary>
		///   Horizontal double arrow cursor.
		/// </summary>
		SizeHorizontal,
		/// <summary>
		///   Vertical double arrow cursor.
		/// </summary>
		SizeVertical,
		/// <summary>
		///   Double arrow cursor going from top-left to bottom-right.
		/// </summary>
		SizeTopLeftBottomRight,
		/// <summary>
		///   Double arrow cursor going from bottom-left to top-right.
		/// </summary>
		SizeBottomLeftTopRight,
		/// <summary>
		///   Combination of SizeHorizontal and SizeVertical.
		/// </summary>
		SizeAll,
		/// <summary>
		///   Crosshair cursor.
		/// </summary>
		Cross,
		/// <summary>
		///   Help cursor.
		/// </summary>
		Help,
		/// <summary>
		///   Action not allowed cursor.
		/// </summary>
		NotAllowed
	}

	/// <summary>
	///   Defines the appearance of a system cursor.
	/// </summary>
	public unsafe class Cursor : IDisposable
	{
		internal Native* Handle;

		public bool IsCreated => Handle is not null;

		/// <summary>
		///   Construct a new instance of <see cref="Cursor" />.
		/// </summary>
		/// <remarks>
		///   This constructor doesn't actually create the cursor;
		///   initially the new instance is invalid and must not be used until
		///   either <see cref="TryLoadFromPixels(ReadOnlySpan{byte}, Vector2<uint>, Vector2<uint>)"/>
		///   or <see cref="TryLoadFromSystem(CursorType)"/> is called
		///   and successfully created a cursor.
		/// </remarks>
		public Cursor() { }

		internal Cursor(Native* handle)
		{
			Handle = handle;
		}

		/// <summary>
		///   Construct a new instance of <see cref="Cursor" /> with the provided image.
		/// </summary>
		/// <remarks>
		///   <paramref name="pixels" /> must be an array with the length of
		///   <paramref name="size" />'s width and height pixels in 32-bit RGBA format.
		///   If not, this will cause undefined behavior.
		///   <para>
		///     If <paramref name="pixels" /> is null
		///     or either <paramref name="size" />'s width or height are 0,
		///     the current cursor is left unchanged and the function will return false.
		///   </para>
		///   <para>
		///     In addition to specifying the pixel data,
		///     you can also specify the location of the hotspot of the cursor.
		///     The hotspot is the pixel coordinate within the cursor image
		///     which will be located exactly where the mouse pointer position is.
		///     Any mouse actions that are performed will return
		///     the window/screen location of the hotspot.
		///   </para>
		///   <para>
		///     On Unix platforms which do not support colored cursors,
		///     the pixels are mapped into a monochrome bitmap:
		///     pixels with an alpha channel to 0 are transparent,
		///     black if the RGB channel are close to zero, and white otherwise.
		///   </para>
		/// </remarks>
		internal static Cursor FromPixels(byte* pixels, Vector2<uint> size, Vector2<uint> hotspot = default)
		{
			Cursor cursor = new();

			Debug.Assert(cursor.TryLoadFromPixels(pixels, size, hotspot) is false);

			return cursor;
		}

		/// <inheritdoc cref="FromPixels(byte*, Vector2<uint>, Vector2<uint>)"/>
		public static Cursor FromPixels(ReadOnlySpan<byte> pixels, Vector2<uint> size, Vector2<uint> hotspot = default)
		{
			if (pixels.IsEmpty) throw new ArgumentException("Pixels is empty.", nameof(pixels));
			if ((uint)pixels.Length != (size.X * size.Y)) throw new ArgumentOutOfRangeException(nameof(pixels), pixels.Length, "Pixels length does not match size parameter.");

			fixed (byte* pixels_ptr = pixels)
			{
				return FromPixels(pixels_ptr, size, hotspot);
			}
		}

		/// <summary>
		///   Construct a new instance of native system <see cref="Cursor" />.
		/// </summary>
		/// <remarks>
		///   Refer to the list of cursor available on each system (see <see cref="CursorType" />)
		///   to know whether a given cursor is expected to load successfully
		///   or is not supported by the operating system.
		/// </remarks>
		/// <param name="type">Native system cursor type.</param>
		/// <exception cref="PlatformNotSupportedException">Cursor of type <paramref name="type"/> is not supported.</exception>
		public static Cursor FromSystem(CursorType type)
		{
			Cursor cursor = new();

			if (cursor.TryLoadFromSystem(type) is false) throw new PlatformNotSupportedException($"Cursor of type {type} is not supported.");

			return cursor;
		}

		/// <summary>
		///   Create a native system cursor.
		/// </summary>
		/// <remarks>
		///   Refer to the list of cursor available on each system (see <see cref="CursorType" />)
		///   to know whether a given cursor is expected to load successfully
		///   or is not supported by the operating system.
		/// </remarks>
		/// <param name="type">Native system cursor type.</param>
		/// <returns>
		///   <see langword="true" /> if the cursor was successfully loaded;
		///   <see langword="false" /> otherwise.
		/// </returns>
		public bool TryLoadFromSystem(CursorType type)
		{
			return CheckAssignHandle(sfCursor_createFromSystem(type));
		}

		/// <inheritdoc cref="FromPixels(byte*, Vector2<uint>, Vector2<uint>)"/>
		/// <returns>
		///   <see langword="true" /> if the cursor was successfully loaded;
		///   <see langword="false" /> otherwise.
		/// </returns>
		internal bool TryLoadFromPixels(byte* pixels, Vector2<uint> size, Vector2<uint> hotspot = default)
		{
			return CheckAssignHandle(sfCursor_createFromPixels(pixels, size, hotspot));
		}

		/// <inheritdoc cref="TryLoadFromPixels(byte*, Vector2<uint>, Vector2<uint>)"/>
		public bool TryLoadFromPixels(ReadOnlySpan<byte> pixels, Vector2<uint> size, Vector2<uint> hotspot = default)
		{
			//if (pixels.IsEmpty || pixels.Length != (size.X * size.Y)) return false;

			fixed (byte* pixels_ptr = pixels) return TryLoadFromPixels(pixels_ptr, size, hotspot);
		}

		private bool CheckAssignHandle([NotNullWhen(true)] Native* handle)
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

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Cursor()
		{
			Dispose(disposing: false);
		}

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			if (disposing) sfCursor_destroy(Handle);
			_disposed = true;
		}

		#region Import

		internal readonly struct Native { }

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfCursor_createFromPixels(byte* pixels, Vector2<uint> size, Vector2<uint> hotspot);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfCursor_createFromSystem(CursorType type);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfCursor_destroy(Native* cursor);

		#endregion
	}
}
