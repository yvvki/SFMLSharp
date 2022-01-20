using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SFML.Window
{
	public class EventWindow
	{
		private readonly Window _window;
		public Window Window => _window;

		private readonly bool _handleObsolete;

		public EventWindow(Window window, bool handleObsolete = false)
		{
			_window = window;

			_handleObsolete = handleObsolete;
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
			}
		}

		private void HandleEvent(Event e)
		{
			switch (e.Type)
			{
				case EventType.Closed:
					Closed?.Invoke();
					break;
				case EventType.Resized:
					Resized?.Invoke(e.Size);
					break;
				case EventType.LostFocus:
					LostFocus?.Invoke();
					break;
				case EventType.GainedFocus:
					GainedFocus?.Invoke();
					break;
				case EventType.TextEntered:
					TextEntered?.Invoke(e.Text);
					break;
				case EventType.KeyPressed:
					KeyPressed?.Invoke(e.Key);
					break;
				case EventType.KeyReleased:
					KeyReleased?.Invoke(e.Key);
					break;
#pragma warning disable CS0618 // Support for obsolete.
				case EventType.MouseWheelMoved:
					if (_handleObsolete is false) break;
					MouseWheelMoved?.Invoke(e.MouseWheel);
					break;
#pragma warning restore CS0618
				case EventType.MouseWheelScrolled:
					MouseWheelScrolled?.Invoke(e.MouseWheelScroll);
					break;
				case EventType.MouseButtonPressed:
					MouseButtonPressed?.Invoke(e.MouseButton);
					break;
				case EventType.MouseButtonReleased:
					MouseButtonReleased?.Invoke(e.MouseButton);
					break;
				case EventType.MouseMoved:
					MouseMoved?.Invoke(e.MouseMove);
					break;
				case EventType.MouseEntered:
					MouseEntered?.Invoke();
					break;
				case EventType.MouseLeft:
					MouseLeft?.Invoke();
					break;
				case EventType.JoystickButtonPressed:
					JoystickButtonPressed?.Invoke(e.JoystickButton);
					break;
				case EventType.JoystickButtonReleased:
					JoystickButtonReleased?.Invoke(e.JoystickButton);
					break;
				case EventType.JoystickMoved:
					JoystickMoved?.Invoke(e.JoystickMove);
					break;
				case EventType.JoystickConnected:
					JoystickConnected?.Invoke(e.JoystickConnect);
					break;
				case EventType.JoystickDisconnected:
					JoystickDisconnected?.Invoke(e.JoystickConnect);
					break;
				case EventType.TouchBegan:
					TouchBegan?.Invoke(e.Touch);
					break;
				case EventType.TouchMoved:
					TouchMoved?.Invoke(e.Touch);
					break;
				case EventType.TouchEnded:
					TouchEnded?.Invoke(e.Touch);
					break;
				case EventType.SensorChanged:
					SensorChanged?.Invoke(e.Sensor);
					break;
				default:
					Debug.Assert(false);
					break;
			}
		}

		public event Action? Closed;
		public event Action<SizeEventArgs>? Resized;
		public event Action? LostFocus;
		public event Action? GainedFocus;
		public event Action<TextEventArgs>? TextEntered;
		public event Action<KeyEventArgs>? KeyPressed;
		public event Action<KeyEventArgs>? KeyReleased;
		[Obsolete("Use MouseWheelScrolled instead.")]
		public event Action<MouseWheelEventArgs>? MouseWheelMoved;
		public event Action<MouseWheelScrollEventArgs>? MouseWheelScrolled;
		public event Action<MouseButtonEventArgs>? MouseButtonPressed;
		public event Action<MouseButtonEventArgs>? MouseButtonReleased;
		public event Action<MouseMoveEventArgs>? MouseMoved;
		public event Action? MouseEntered;
		public event Action? MouseLeft;
		public event Action<JoystickButtonEventArgs>? JoystickButtonPressed;
		public event Action<JoystickButtonEventArgs>? JoystickButtonReleased;
		public event Action<JoystickMoveEventArgs>? JoystickMoved;
		public event Action<JoystickConnectEventArgs>? JoystickConnected;
		public event Action<JoystickConnectEventArgs>? JoystickDisconnected;
		public event Action<TouchEventArgs>? TouchBegan;
		public event Action<TouchEventArgs>? TouchMoved;
		public event Action<TouchEventArgs>? TouchEnded;
		public event Action<SensorEventArgs>? SensorChanged;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Window(EventWindow value)
		{
			return value.Window;
		}
	}
}
