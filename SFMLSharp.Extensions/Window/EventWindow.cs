using System.Diagnostics;

namespace SFML.Window
{
	public class EventWindow
	{
		private readonly Window _window;
		public Window Window => _window;

		public EventWindow(Window window)
		{
			_window = window;
		}

		public void DispatchEvent()
		{
			while (_window.PollEvent(out Event e))
			{
				HandleEvent(e);
			}
		}

		public void WaitAndDispatchEvent()
		{
			if (_window.WaitEvent(out Event e))
			{
				HandleEvent(e);
			};
		}

		private void HandleEvent(Event e)
		{
			switch (e.Type)
			{
				case EventType.Closed:
					Closed?.Invoke(this, EventArgs.Empty);
					break;
				case EventType.Resized:
					Resized?.Invoke(this, e.Size);
					break;
				case EventType.LostFocus:
					LostFocus?.Invoke(this, EventArgs.Empty);
					break;
				case EventType.GainedFocus:
					GainedFocus?.Invoke(this, EventArgs.Empty);
					break;
				case EventType.TextEntered:
					TextEntered?.Invoke(this, e.Text);
					break;
				case EventType.KeyPressed:
					KeyPressed?.Invoke(this, e.Key);
					break;
				case EventType.KeyReleased:
					KeyReleased?.Invoke(this, e.Key);
					break;
#pragma warning disable CS0618 // Implement obsolete member.
				case EventType.MouseWheelMoved:
					MouseWheelMoved?.Invoke(this, e.MouseWheel);
#pragma warning restore CS0618
					break;
				case EventType.MouseWheelScrolled:
					MouseWheelScrolled?.Invoke(this, e.MouseWheelScroll);
					break;
				case EventType.MouseButtonPressed:
					MouseButtonPressed?.Invoke(this, e.MouseButton);
					break;
				case EventType.MouseButtonReleased:
					MouseButtonReleased?.Invoke(this, e.MouseButton);
					break;
				case EventType.MouseMoved:
					MouseMoved?.Invoke(this, e.MouseMove);
					break;
				case EventType.MouseEntered:
					MouseEntered?.Invoke(this, EventArgs.Empty);
					break;
				case EventType.MouseLeft:
					MouseLeft?.Invoke(this, EventArgs.Empty);
					break;
				case EventType.JoystickButtonPressed:
					JoystickButtonPressed?.Invoke(this, e.JoystickButton);
					break;
				case EventType.JoystickButtonReleased:
					JoystickButtonReleased?.Invoke(this, e.JoystickButton);
					break;
				case EventType.JoystickMoved:
					JoystickMoved?.Invoke(this, e.JoystickMove);
					break;
				case EventType.JoystickConnected:
					JoystickConnected?.Invoke(this, e.JoystickConnect);
					break;
				case EventType.JoystickDisconnected:
					JoystickDisconnected?.Invoke(this, e.JoystickConnect);
					break;
				case EventType.TouchBegan:
					TouchBegan?.Invoke(this, e.Touch);
					break;
				case EventType.TouchMoved:
					TouchMoved?.Invoke(this, e.Touch);
					break;
				case EventType.TouchEnded:
					TouchEnded?.Invoke(this, e.Touch);
					break;
				case EventType.SensorChanged:
					SensorChanged?.Invoke(this, e.Sensor);
					break;
				default:
					Debug.Assert(false);
					break;
			}
		}

		public event EventHandler? Closed;
		public event EventHandler<SizeEventArgs>? Resized;
		public event EventHandler? LostFocus;
		public event EventHandler? GainedFocus;
		public event EventHandler<TextEventArgs>? TextEntered;
		public event EventHandler<KeyEventArgs>? KeyPressed;
		public event EventHandler<KeyEventArgs>? KeyReleased;
		[Obsolete("Use MouseWheelScrolled instead.")]
		public event EventHandler<MouseWheelEventArgs>? MouseWheelMoved;
		public event EventHandler<MouseWheelScrollEventArgs>? MouseWheelScrolled;
		public event EventHandler<MouseButtonEventArgs>? MouseButtonPressed;
		public event EventHandler<MouseButtonEventArgs>? MouseButtonReleased;
		public event EventHandler<MouseMoveEventArgs>? MouseMoved;
		public event EventHandler? MouseEntered;
		public event EventHandler? MouseLeft;
		public event EventHandler<JoystickButtonEventArgs>? JoystickButtonPressed;
		public event EventHandler<JoystickButtonEventArgs>? JoystickButtonReleased;
		public event EventHandler<JoystickMoveEventArgs>? JoystickMoved;
		public event EventHandler<JoystickConnectEventArgs>? JoystickConnected;
		public event EventHandler<JoystickConnectEventArgs>? JoystickDisconnected;
		public event EventHandler<TouchEventArgs>? TouchBegan;
		public event EventHandler<TouchEventArgs>? TouchMoved;
		public event EventHandler<TouchEventArgs>? TouchEnded;
		public event EventHandler<SensorEventArgs>? SensorChanged;
	}
}
