using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

using SFML.System;

using static SFML.Window.DllName;

namespace SFML.Window
{
	/// <summary>
	///   Defines a video mode (width, height, bpp, frequency)
	///   and provides functions for getting modes supported by the display device.
	/// </summary>
	/// <param name="Width">Video mode width, in pixels.</param>
	/// <param name="Height">Video mode height, in pixels.</param>
	/// <param name="BitsPerPixel">Video mode pixel depth, in bits per pixels.</param>
	public readonly unsafe record struct VideoMode(
		uint Width,
		uint Height,
		uint BitsPerPixel = 32)
	{
		#region Fields & Properties

		/// <summary>
		///   Tell whether or not a video mode is valid.
		/// </summary>
		/// <remarks>
		///   The validity of video modes is only relevant when using fullscreen windows;
		///   otherwise any video mode can be used with no restriction.
		/// </remarks>
		/// <value>
		///   <see langword="true"/> if the video mode is valid for fullscreen mode;
		///   <see langword="false"/> otherwise.
		/// </value>
		public bool IsValid => sfVideoMode_isValid(this);

		public Vector2<uint> Size
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(Width, Height);
		}

		#endregion

		#region Constructors

		/// <summary>
		///   Construct the video mode using 2-component vector.
		/// </summary>
		/// <param name="size">Width and height in pixels.</param>
		/// <param name="bitsPerPixel">Pixel depths in bits per pixel.</param>
		[RequiresPreviewFeatures]
		public VideoMode(Vector2<uint> size, uint bitsPerPixel = 32)
			: this(size.X, size.Y, bitsPerPixel) { }

		#endregion

		#region Static Methods

		/// <summary>
		///   Get the current desktop video mode.
		/// </summary>
		/// <returns>Current desktop video mode.</returns>
		public static VideoMode GetDesktopMode()
		{
			return sfVideoMode_getDesktopMode();
		}

		/// <summary>
		///   Retrieve all the video modes supported in fullscreen mode.
		/// </summary>
		/// <remarks>
		///   When creating a fullscreen window, the video mode is restricted
		///   to be compatible with what the graphics driver and monitor support.
		///   <br>
		///     This function returns the complete list of all video modes that can be used in fullscreen mode.
		///   </br>
		///   <br>
		///     The returned array is sorted from best to worst,
		///     so that the first element will always give the best mode
		///     (higher width, height and bits-per-pixel).
		///   </br>
		/// </remarks>
		/// <returns>A span containing all the supported fullscreen modes.</returns>
		public static ReadOnlySpan<VideoMode> GetFullscreenModes()
		{
			return new(
				sfVideoMode_getFullscreenModes(out nuint count),
				(int)count);
		}

		#endregion

		#region Cast Operators

		[RequiresPreviewFeatures]
		public static explicit operator Vector2<uint>(VideoMode value)
		{
			return new(value.Width, value.Height);
		}

		#endregion

		#region Imports

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern VideoMode sfVideoMode_getDesktopMode();

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern VideoMode* sfVideoMode_getFullscreenModes(out nuint count);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfVideoMode_isValid(VideoMode mode);

		#endregion
	}
}
