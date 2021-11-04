using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Window.DllName;

namespace SFML.Window
{
	/// <summary>
	///   Mouse buttons.
	/// </summary>
	public enum MouseButton
	{
		/// <summary>
		///   The left mouse button.
		/// </summary>
		Left,
		/// <summary>
		///   The right mouse button.
		/// </summary>
		Right,
		/// <summary>
		///   The middle (wheel) mouse button.
		/// </summary>
		Middle,
		/// <summary>
		///   The first extra mouse button.
		/// </summary>
		XButton1,
		/// <summary>
		///   The second extra mouse button.
		/// </summary>
		XButton2,

		/// <summary>
		///   The total number of mouse buttons.
		/// </summary>
		Count
	}

	/// <summary>
	///   Mouse wheels.
	/// </summary>
	public enum MouseWheel
	{
		/// <summary>
		///   The vertical mouse wheel.
		/// </summary>
		Vertical,
		/// <summary>
		///   The horizontal mouse wheel.
		/// </summary>
		Horizontal
	}

	public static unsafe class Mouse
	{
		/// <summary>
		///   The current position of the mouse in desktop coordinates.
		/// </summary>
		/// <remarks>
		///   Use <see cref="GetPosition(Window?)"/> and
		///   <see cref="SetPosition(Vector2i, Window?)"/> to get and set
		///   the current position of the mouse cursor relative to
		///   a window.
		/// </remarks>
		public static Vector2I Position
		{
			get => GetPosition(null);
			set => SetPosition(value, null);
		}

		/// <summary>
		///   Check if a mouse button is pressed.
		/// </summary>
		/// <param name="button">Button to check.</param>
		/// <returns>
		///   <see langword="true" /> if the button is pressed;
		///   <see langword="false" /> otherwise.
		/// </returns>
		public static bool IsButtonPressed(MouseButton button)
		{
			return sfMouse_isButtonPressed(button);
		}

		/// <summary>
		///   Get the current position of the mouse.
		/// </summary>
		/// <remarks>
		///   This function returns the current position of the mouse
		///   cursor relative to the given window, or desktop if
		///   <see langword="null" /> is passed.
		/// </remarks>
		/// <param name="relativeTo">Reference window.</param>
		/// <returns>
		///   Current position of the mouse.
		/// </returns>
		public static Vector2I GetPosition(Window? relativeTo)
		{
			return sfMouse_getPosition(relativeTo!.Handle);
		}

		/// <summary>
		///   Set the current position of the mouse.
		/// </summary>
		/// <remarks>
		///   This function sets the current position of the mouse
		///   cursor relative to the given window,
		///   or desktop if <see langword="null" /> is passed.
		/// </remarks>
		/// <param name="position">New position of the mouse.</param>
		/// <param name="relativeTo">Reference window.</param>
		public static void SetPosition(Vector2I position, Window? relativeTo)
		{
			sfMouse_setPosition(position, relativeTo!.Handle);
		}

		#region Imports

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfMouse_isButtonPressed(MouseButton button);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2I sfMouse_getPosition(Window.Native* relativeTo);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfMouse_setPosition(Vector2I position, Window.Native* relativeTo);

		#endregion
	}
}
