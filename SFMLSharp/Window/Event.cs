using System.Runtime.InteropServices;

using SFML.System;

namespace SFML.Window
{
	public enum EventType : int
	{
		/// <summary>
		///   The window requested to be closed (no data).
		/// </summary>
		Closed,
		/// <summary>
		///   The window was resized (data in <see cref="Event.Size" />).
		/// </summary>
		Resized,
		/// <summary>
		///   The window lost the focus (no data).
		/// </summary>
		LostFocus,
		/// <summary>
		///   The window gained the focus (no data).
		/// </summary>
		GainedFocus,
		/// <summary>
		///   A character was entered (data in <see cref="Event.Text" />).
		/// </summary>
		TextEntered,
		/// <summary>
		///   A key was pressed (data in <see cref="Event.Key" />).
		/// </summary>
		KeyPressed,
		/// <summary>
		///   A key was released (data in <see cref="Event.Key" />).
		/// </summary>
		KeyReleased,
		/// <summary>
		///   The mouse wheel was scrolled (data in <see cref="Event.MouseWheel" />).
		/// </summary>
		[Obsolete("Use EventType.MouseWheelScroll instead.")]
		MouseWheelMoved,
		/// <summary>
		///   The mouse wheel was scrolled (data in <see cref="Event.MouseWheelScroll" />).
		/// </summary>
		MouseWheelScrolled,
		/// <summary>
		///   A mouse button was pressed (data in <see cref="Event.MouseButton" />).
		/// </summary>
		MouseButtonPressed,
		/// <summary>
		///   A mouse button was released (data in <see cref="Event.MouseButton" />).
		/// </summary>
		MouseButtonReleased,
		/// <summary>
		///   The mouse cursor moved (data in <see cref="Event.MouseMove" />).
		/// </summary>
		MouseMoved,
		/// <summary>
		///   The mouse cursor entered the area of the window (no data).
		/// </summary>
		MouseEntered,
		/// <summary>
		///   The mouse cursor left the area of the window (no data).
		/// </summary>
		MouseLeft,
		/// <summary>
		///   A joystick button was pressed (data in <see cref="Event.JoystickButton" />).
		/// </summary>
		JoystickButtonPressed,
		/// <summary>
		///   A joystick button was released (data in <see cref="Event.JoystickButton" />).
		/// </summary>
		JoystickButtonReleased,
		/// <summary>
		///   The joystick moved along an axis (data in <see cref="Event.JoystickMove" />).
		/// </summary>
		JoystickMoved,
		/// <summary>
		///   A joystick was connected (data in <see cref="Event.JoystickConnect" />).
		/// </summary>
		JoystickConnected,
		/// <summary>
		///   A joystick was disconnected (data in <see cref="Event.JoystickConnect" />).
		/// </summary>
		JoystickDisconnected,
		/// <summary>
		///   A touch event began (data in <see cref="Event.Touch" />).
		/// </summary>
		TouchBegan,
		/// <summary>
		///   A touch moved (data in <see cref="Event.Touch" />).
		/// </summary>
		TouchMoved,
		/// <summary>
		///   A touch event ended (data in <see cref="Event.Touch" />).
		/// </summary>
		TouchEnded,
		/// <summary>
		///   A sensor value changed (data in <see cref="Event.Sensor" />).
		/// </summary>
		SensorChanged,

		/// <summary>
		///   The total number of event types.
		/// </summary>
		Count
	}

	#region Arguments

	/// <summary>
	///   Keyboard event argument parameters (<see cref="EventType.KeyPressed" />, <see cref="EventType.KeyReleased" />).
	/// </summary>
	/// <param name="Code">Code of the key that has been pressed.</param>
	/// <param name="Alt">Is the Alt key pressed?</param>
	/// <param name="Control">Is the Control key pressed?</param>
	/// <param name="Shift">Is the Shift key pressed?</param>
	/// <param name="System">Is the System key pressed?</param>
	public record struct KeyEventArgs(KeyCode Code, bool Alt, bool Control, bool Shift, bool System);

	/// <summary>
	///   Text event argument parameters (<see cref="EventType.TextEntered" />).
	/// </summary>
	/// <param name="Text">UTF-32 Unicode value of the character.</param>
	public record struct TextEventArgs(string Text)
	{
		private readonly unsafe uint _data = (uint)char.ConvertToUtf32(Text, 0);

		/// <summary>
		///   UTF-32 Unicode value of the character.
		/// </summary>
		public unsafe string Text => char.ConvertFromUtf32((int)_data);
	};

	/// <summary>
	///   Mouse move event argument parameters (<see cref="EventType.MouseMoved" />).
	/// </summary>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	public record struct MouseMoveEventArgs(int X, int Y) : IReadOnlyVector2<int>
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2I Position => new(X, Y);

		public static implicit operator Vector2I(MouseMoveEventArgs value)
		{
			return new(value.X, value.Y);
		}
	}

	/// <summary>
	///   Mouse buttons events argument parameters
	///   (<see cref="EventType.MouseButtonPressed" />, <see cref="EventType.MouseButtonReleased" />).
	/// </summary>
	/// <param name="Button">Code of the button that has been pressed.</param>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	public record struct MouseButtonEventArgs(MouseButton Button, int X, int Y) : IReadOnlyVector2<int>
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2I Position => new(X, Y);

		public static explicit operator Vector2I(MouseButtonEventArgs value)
		{
			return new(value.X, value.Y);
		}
	};

	/// <summary>
	///   Mouse wheel events argument parameters (<see cref="EventType.MouseWheelMoved" />).
	/// </summary>
	/// <param name="Delta">Number of ticks the wheel has moved (positive is up, negative is down).</param>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	[Obsolete("This event arguments is deprecated and potentially inaccurate. Use MouseWheelScrollEventArgs instead.")]
	public record struct MouseWheelEventArgs(int Delta, int X, int Y) : IReadOnlyVector2<int>
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2I Position => new(X, Y);

		public static explicit operator Vector2I(MouseWheelEventArgs value)
		{
			return new(value.X, value.Y);
		}
	};

	/// <summary>
	///   Mouse wheel events argument parameter (<see cref="EventType.MouseWheelScrolled" />)
	/// </summary>
	/// <param name="Wheel">Which wheel (for mice with multiple ones).</param>
	/// <param name="Delta">Wheel offset (positive is up/left, negative is down/right). High-precision mice may use non-integral offsets.</param>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	public record struct MouseWheelScrollEventArgs(MouseWheel Wheel, int Delta, int X, int Y) : IReadOnlyVector2<int>
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2I Position => new(X, Y);

		public static explicit operator Vector2I(MouseWheelScrollEventArgs value)
		{
			return new(value.X, value.Y);
		}
	};

	/// <summary>
	///   Joystick axis move event argument parameters (<see cref="EventType.JoystickMoved" />).
	/// </summary>
	/// <param name="JoystickId">Index of the joystick (in range [0 .. <see cref="Joystick.Count" /> - 1]).</param>
	/// <param name="Axis">Axis on which the joystick moved.</param>
	/// <param name="Position">New position on the axis (in range [-100 .. 100]).</param>
	public record struct JoystickMoveEventArgs(uint JoystickId, JoystickAxis Axis, float Position);

	public record struct JoystickButtonEventArgs(uint JoystickId, uint Button);

	/// <summary>
	///   Joystick connection events argument parameters
	///   (<see cref="EventType.JoystickConnected" />, <see cref="EventType.JoystickDisconnected" />).
	/// </summary>
	/// <param name="JoystickId">Index of the joystick (in range [0 .. <see cref="Joystick.Count" /> - 1]).</param>
	public record struct JoystickConnectEventArgs(uint JoystickId);

	/// <summary>
	///   Size events argument parameters (<see cref="EventType.Resized" />).
	/// </summary>
	/// <param name="Width">New width, in pixels.</param>
	/// <param name="Height">New height, in pixels.</param>
	public record struct SizeEventArgs(uint Width, uint Height) : IReadOnlyVector2<uint>
	{
		/// <summary>
		///   New size, in pixels.
		/// </summary>
		public Vector2U Size => new(Width, Height);

		uint IReadOnlyVector2<uint>.X => Width;
		uint IReadOnlyVector2<uint>.Y => Height;

		public static implicit operator Vector2U(SizeEventArgs value)
		{
			return new(value.Width, value.Height);
		}
	};

	/// <summary>
	///   Touch events argument parameters
	///   (<see cref="EventType.TouchBegan" />, <see cref="EventType.TouchMoved" />, <see cref="EventType.TouchEnded" />).
	/// </summary>
	/// <param name="Finger">Index of the finger in case of multi-touch events.</param>
	/// <param name="X">X position of the touch, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the touch, relative to the top of the owner window.</param>
	public record struct TouchEventArgs(uint Finger, int X, int Y) : IReadOnlyVector2<int>
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2I Position => new(X, Y);

		public static explicit operator Vector2I(TouchEventArgs value)
		{
			return new(value.X, value.Y);
		}
	};

	/// <summary>
	///   Sensor event argument parameters ((<see cref="EventType.SensorChanged" />).
	/// </summary>
	/// <param name="Type">Type of the sensor.</param>
	/// <param name="X">Current value of the sensor on X axis.</param>
	/// <param name="Y">Current value of the sensor on Y axis.</param>
	/// <param name="Z">Current value of the sensor on Z axis.</param>
	public record struct SensorEventArgs(SensorType Type, float X, float Y, float Z) : IReadOnlyVector3<float>
	{
		/// <summary>
		///   Current value of the sensor.
		/// </summary>
		public Vector3F Value => new(X, Y, Z);

		public static explicit operator Vector3F(SensorEventArgs value)
		{
			return new(value.X, value.Y, value.Z);
		}
	}

	#endregion

	#region Events

	/// <summary>
	///   Defines a system event and its parameters.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public readonly struct Event
	{
		/// <summary>
		///   Type of the event.
		/// </summary>
		[FieldOffset(0)]
		public readonly EventType Type;

		/// <summary>
		///   Size event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly SizeEventArgs Size;

		/// <summary>
		///   Key event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly KeyEventArgs Key;

		/// <summary>
		///   Text event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly TextEventArgs Text;

		/// <summary>
		///   Mouse move event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly MouseMoveEventArgs MouseMove;

		/// <summary>
		///   Mouse button event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly MouseButtonEventArgs MouseButton;

		/// <summary>
		///   Mouse wheel event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		[Obsolete("Use MouseWheelScroll instead.")]
		public readonly MouseWheelEventArgs MouseWheel;

		/// <summary>
		///   Mouse wheel event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly MouseWheelScrollEventArgs MouseWheelScroll;

		/// <summary>
		///   Joystick move event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly JoystickMoveEventArgs JoystickMove;

		/// <summary>
		///   Joystick button event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly JoystickButtonEventArgs JoystickButton;

		/// <summary>
		///   Joystick (dis)connect event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly JoystickConnectEventArgs JoystickConnect;

		/// <summary>
		///   Touch events parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly TouchEventArgs Touch;

		/// <summary>
		///   Sensor event parameters.
		/// </summary>
		[FieldOffset(sizeof(EventType))]
		public readonly SensorEventArgs Sensor;
	}

	#endregion
}
