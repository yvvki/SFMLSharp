using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

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
	[Serializable]
	public readonly record struct KeyEventArgs(KeyCode Code, bool Alt, bool Control, bool Shift, bool System);

	/// <summary>
	///   Text event argument parameters (<see cref="EventType.TextEntered" />).
	/// </summary>
	/// <param name="Text">UTF-32 Unicode value of the character.</param>
	[Serializable]
	public readonly record struct TextEventArgs(string Text)
	{
		private readonly unsafe uint _data = (uint)char.ConvertToUtf32(Text, 0);

		/// <summary>
		///   UTF-32 Unicode value of the character.
		/// </summary>
		public readonly string Text => char.ConvertFromUtf32((int)_data);
	};

	/// <summary>
	///   Mouse move event argument parameters (<see cref="EventType.MouseMoved" />).
	/// </summary>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	[Serializable]
	public readonly record struct MouseMoveEventArgs(int X, int Y)
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2<int> Position
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(X, Y);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2<int>(MouseMoveEventArgs value)
		{
			return value.Position;
		}
	};

	/// <summary>
	///   Mouse buttons events argument parameters
	///   (<see cref="EventType.MouseButtonPressed" />, <see cref="EventType.MouseButtonReleased" />).
	/// </summary>
	/// <param name="Button">Code of the button that has been pressed.</param>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	[Serializable]
	public readonly record struct MouseButtonEventArgs(MouseButton Button, int X, int Y)
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2<int> Position
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(X, Y);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector2<int>(MouseButtonEventArgs value)
		{
			return value.Position;
		}
	};

	/// <summary>
	///   Mouse wheel events argument parameters (<see cref="EventType.MouseWheelMoved" />).
	/// </summary>
	/// <param name="Delta">Number of ticks the wheel has moved (positive is up, negative is down).</param>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	[Serializable]
	[Obsolete("This event arguments is deprecated and potentially inaccurate. Use MouseWheelScrollEventArgs instead.")]
	public readonly record struct MouseWheelEventArgs(int Delta, int X, int Y)
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2<int> Position
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(X, Y);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector2<int>(MouseWheelEventArgs value)
		{
			return value.Position;
		}
	};

	/// <summary>
	///   Mouse wheel events argument parameter (<see cref="EventType.MouseWheelScrolled" />)
	/// </summary>
	/// <param name="Wheel">Which wheel (for mice with multiple ones).</param>
	/// <param name="Delta">Wheel offset (positive is up/left, negative is down/right). High-precision mice may use non-integral offsets.</param>
	/// <param name="X">X position of the mouse pointer, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the mouse pointer, relative to the top of the owner window.</param>
	[Serializable]
	public readonly record struct MouseWheelScrollEventArgs(MouseWheel Wheel, int Delta, int X, int Y)
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2<int> Position
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(X, Y);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector2<int>(MouseWheelScrollEventArgs value)
		{
			return value.Position;
		}
	};

	/// <summary>
	///   Joystick axis move event argument parameters (<see cref="EventType.JoystickMoved" />).
	/// </summary>
	/// <param name="JoystickId">Index of the joystick (in range [0 .. <see cref="Joystick.Count" /> - 1]).</param>
	/// <param name="Axis">Axis on which the joystick moved.</param>
	/// <param name="Position">New position on the axis (in range [-100 .. 100]).</param>
	[Serializable]
	public readonly record struct JoystickMoveEventArgs(uint JoystickId, JoystickAxis Axis, float Position);

	[Serializable]
	public readonly record struct JoystickButtonEventArgs(uint JoystickId, uint Button);

	/// <summary>
	///   Joystick connection events argument parameters
	///   (<see cref="EventType.JoystickConnected" />, <see cref="EventType.JoystickDisconnected" />).
	/// </summary>
	/// <param name="JoystickId">Index of the joystick (in range [0 .. <see cref="Joystick.Count" /> - 1]).</param>
	[Serializable]
	public readonly record struct JoystickConnectEventArgs(uint JoystickId);

	/// <summary>
	///   Size events argument parameters (<see cref="EventType.Resized" />).
	/// </summary>
	/// <param name="Width">New width, in pixels.</param>
	/// <param name="Height">New height, in pixels.</param>
	[Serializable]
	public readonly record struct SizeEventArgs(uint Width, uint Height)
	{
		/// <summary>
		///   New size, in pixels.
		/// </summary>
		public Vector2<uint> Size
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(Width, Height);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2<uint>(SizeEventArgs value)
		{
			return value.Size;
		}
	};

	/// <summary>
	///   Touch events argument parameters
	///   (<see cref="EventType.TouchBegan" />, <see cref="EventType.TouchMoved" />, <see cref="EventType.TouchEnded" />).
	/// </summary>
	/// <param name="Finger">Index of the finger in case of multi-touch events.</param>
	/// <param name="X">X position of the touch, relative to the left of the owner window.</param>
	/// <param name="Y">Y position of the touch, relative to the top of the owner window.</param>
	[Serializable]
	public readonly record struct TouchEventArgs(uint Finger, int X, int Y)
	{
		/// <summary>
		///   Position of the mouse pointer, relative to the top left of the owner window.
		/// </summary>
		public Vector2<int> Position
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(X, Y);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector2<int>(TouchEventArgs value)
		{
			return value.Position;
		}
	};

	/// <summary>
	///   Sensor event argument parameters ((<see cref="EventType.SensorChanged" />).
	/// </summary>
	/// <param name="Type">Type of the sensor.</param>
	/// <param name="X">Current value of the sensor on X axis.</param>
	/// <param name="Y">Current value of the sensor on Y axis.</param>
	/// <param name="Z">Current value of the sensor on Z axis.</param>
	[Serializable]
	public readonly record struct SensorEventArgs(SensorType Type, float X, float Y, float Z)
	{
		/// <summary>
		///   Current value of the sensor.
		/// </summary>
		public Vector3<float> Value
		{
			[RequiresPreviewFeatures]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(X, Y, Z);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector3<float>(SensorEventArgs value)
		{
			return value.Value;
		}
	};

	#endregion

	#region Events

	/// <summary>
	///   Defines a system event and its parameters.
	/// </summary>
	[RequiresPreviewFeatures]
	[StructLayout(LayoutKind.Explicit)]
	[Serializable]
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
