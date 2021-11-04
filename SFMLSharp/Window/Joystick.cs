using System.Runtime.InteropServices;

using static SFML.Window.DllName;

namespace SFML.Window
{
	/// <summary>
	///   Structure holding a joystick's identification
	/// </summary>
	/// <param name="Name">Name of the joystick.</param>
	/// <param name="VendorId">Manufacturer identifier.</param>
	/// <param name="ProductId">Product identifier.</param>
	public record struct JoystickIdentification(string Name, uint VendorId, uint ProductId);

	/// <summary>
	///   Axes supported by SFML joysticks.
	/// </summary>
	public enum JoystickAxis
	{
		/// <summary>
		///   The X axis.
		/// </summary>
		X,
		/// <summary>
		///   The Y axis.
		/// </summary>
		Y,
		/// <summary>
		///   The Z axis.
		/// </summary>
		Z,
		/// <summary>
		///   The R axis.
		/// </summary>
		R,
		/// <summary>
		///   The U axis.
		/// </summary>
		U,
		/// <summary>
		///   The V axis.
		/// </summary>
		V,
		/// <summary>
		///   The X axis of the point-of-view hat.
		/// </summary>
		PovX,
		/// <summary>
		///   The Y axis of the point-of-view hat.
		/// </summary>
		PovY
	}

	/// <summary>
	///   Give access to the real-time state of the joysticks.
	/// </summary>
	public static unsafe class Joystick
	{
		/// <summary>
		///   Maximum number of supported joysticks.
		/// </summary>
		public const uint Count = 8;

		/// <summary>
		///   Maximum number of supported buttons.
		/// </summary>
		public const uint ButtonCount = 32;

		/// <summary>
		///   Maximum number of supported axes.
		/// </summary>
		public const uint AxisCount = 8;

		/// <summary>
		///   Check if a joystick is connected.
		/// </summary>
		/// <param name="joystick">Index of the joystick to check.</param>
		/// <returns>
		///   <see langword="true" /> if the joystick is connected;
		///   <see langword="false" /> otherwise.
		/// </returns>

		public static bool IsConnected(uint joystick)
		{
			return sfJoystick_isConnected(joystick);
		}

		/// <summary>
		///   Return the number of buttons supported by a joystick.
		/// </summary>
		/// <remarks>
		///   If the joystick is not connected, this function returns 0.
		/// </remarks>
		/// <param name="joystick">Index of the joystick.</param>
		/// <returns>Number of buttons supported by the joystick.</returns>

		public static uint GetButtonCount(uint joystick)
		{
			return sfJoystick_getButtonCount(joystick);
		}

		/// <summary>
		///   Check if a joystick supports a given axis.
		/// </summary>
		/// <remarks>
		///   If the joystick is not connected, this function returns <see langword="false" />.
		/// </remarks>
		/// <param name="joystick">Index of the joystick.</param>
		/// <param name="axis">Axis to check.</param>
		/// <returns>
		/// <see langword="true" /> if the joystick supports the axis;
		/// <see langword="false" /> otherwise.
		/// </returns>

		public static bool HasAxis(uint joystick, JoystickAxis axis)
		{
			return sfJoystick_hasAxis(joystick, axis);
		}

		/// <summary>
		///   Check if a joystick button is pressed.
		/// </summary>
		/// <remarks>
		///   If the joystick is not connected,
		///   this function returns <see langword="false" />.
		/// </remarks>
		/// <param name="joystick">Index of the joystick.</param>
		/// <param name="button">Button to check.</param>
		/// <returns>
		/// <see langword="true" /> if the button is pressed;
		/// <see langword="false" /> otherwise.
		/// </returns>
		public static bool IsButtonPressed(uint joystick, uint button)
		{
			return sfJoystick_isButtonPressed(joystick, button);
		}

		/// <summary>
		///   Get the current position of a joystick axis.
		/// </summary>
		/// <remarks>
		///   If the joystick is not connected, this function returns <see langword="false" />.
		/// </remarks>
		/// <param name="joystick">Index of the joystick.</param>
		/// <param name="axis">Axis to check.</param>
		/// <returns>Current position of the axis, in range from -100 to 100.</returns>
		public static float GetAxisPosition(uint joystick, JoystickAxis axis)
		{
			return sfJoystick_getAxisPosition(joystick, axis);
		}

		/// <summary>
		///   Get the joystick information.
		/// </summary>
		/// <remarks>
		///   The result of this function will only remain valid until the next time the function is called.
		/// </remarks>
		/// <param name="joystick">Index of the joystick.</param>
		/// <returns>
		///   An <see cref="JoystickIdentification" /> structure containing joystick information.
		/// </returns>
		public static JoystickIdentification GetIdentification(uint joystick)
		{
			return sfJoystick_getIdentification(joystick);
		}

		/// <summary>
		///   Update the states of all joysticks.
		/// </summary>
		/// <remarks>
		///   This function is used internally by SFML,
		///   so you normally don't have to call it explicitely.
		///   However, you may need to call it if you have no window yet (or no window at all):
		///   in this case the joysticks states are not updated automatically.
		/// </remarks>
		public static void Update()
		{
			sfJoystick_update();
		}

		#region Imports

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfJoystick_isConnected(uint joystick);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint sfJoystick_getButtonCount(uint joystick);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfJoystick_hasAxis(uint joystick, JoystickAxis axis);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfJoystick_isButtonPressed(uint joystick, uint button);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern float sfJoystick_getAxisPosition(uint joystick, JoystickAxis axis);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		private static extern JoystickIdentification sfJoystick_getIdentification(uint joystick);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfJoystick_update();

		#endregion
	}
}
