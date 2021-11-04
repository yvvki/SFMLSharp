using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Window.DllName;

namespace SFML.Window
{
	/// <summary>
	///   Enumeration of window styles.
	/// </summary>
	[Flags]
	public enum WindowStyle : uint
	{
		/// <summary>
		///   No border / title bar (this flag and all others are mutually exclusive).
		/// </summary>
		None = 0,
		/// <summary>
		///   Titlebar + fixed border.
		/// </summary>
		Titlebar = 1,
		/// <summary>
		///   Titlebar + resizable border + maximize button.
		/// </summary>
		Resize = 1 << 1,
		/// <summary>
		///   Titlebar + close button.
		/// </summary>
		Close = 1 << 2,
		/// <summary>
		///   Fullscreen mode (this flag and all others are mutually exclusive).
		/// </summary>
		Fullscreen = 1 << 3,

		/// <summary>
		///   Default window style.
		/// </summary>
		Default = Titlebar | Resize | Close
	}

	public unsafe class Window : IDisposable
	{
		internal Native* Handle;

		#region Properties

		/// <summary>
		///   Tell whether or not the window is open.
		/// </summary>
		/// <remarks>
		///   This function returns whether or not the window exists.
		///   Note that a hidden window (<see cref="Visible" /> = <see langword="false" />)
		///   is open (therefore this function would return <see langword="true" />).
		/// </remarks>
		/// <value>
		///   <see langword="true" /> if the window is open;
		///   <see langword="false" /> if it has been closed.
		/// </value>
		public bool IsOpen => sfWindow_isOpen(Handle);

		/// <summary>
		///   Gets the settings of the OpenGL context of the window.
		/// </summary>
		/// <remarks>
		///   Note that these settings may be different from what was passed to the constructor
		///   or the <see cref="Create" /> function, if one or more settings were not supported.
		///   In this case, SFML chose the closest match.
		/// </remarks>
		/// <value>Structure containing the OpenGL context settings.</value>
		public ContextSettings Settings => sfWindow_getSettings(Handle);

		/// <summary>
		///   Gets or sets the position of the window.
		/// </summary>
		/// <remarks>
		///   The set property only works for top-level windows
		///   (i.e. it will be ignored for windows created from the handle of a child window/control).
		/// </remarks>
		/// <value>Position of the window, in pixels.</value>
		public Vector2I Position
		{
			get => sfWindow_getPosition(Handle);
			set => sfWindow_setPosition(Handle, value);
		}

		/// <summary>
		///   Gets or sets the size of the rendering region of the window.
		/// </summary>
		/// <remarks>
		///   The size doesn't include the titlebar and borders of the window.
		/// </remarks>
		/// <value>Size of the window, in pixels</value>
		public Vector2U Size
		{
			get => sfWindow_getSize(Handle);
			set => sfWindow_setSize(Handle, value);
		}

		/// <summary>
		///   Sets the title of the window.
		/// </summary>
		/// <seealso cref="Icon" />
		public string Title
		{
			set => sfWindow_setUnicodeTitle(Handle, UTF32Ptr.ToPointer(value));
		}

		///// <summary>
		/////   Sets the window's icon.
		///// </summary>
		///// <seealso cref="Title" />
		//public Image Icon
		//{
		//	set
		//	{
		//		Vector2U size = value.Size;
		//		sfWindow_setIcon(Handle, size.X, size.Y, value.PixelsPtr);
		//	}
		//}

		/// <summary>
		///   Show or hide the window.
		/// </summary>
		/// <remarks>
		///   The window is shown by default.
		/// </remarks>
		public bool Visible
		{
			set => sfWindow_setVisible(Handle, value);
		}

		/// <summary>
		///   Enable or disable vertical synchronization.
		/// </summary>
		/// <remarks>
		///   Activating vertical synchronization will limit
		///   the number of frames displayed to the refresh rate of the monitor.
		///   This can avoid some visual artifacts, and limit the framerate to a good value
		///   (but not constant across different computers).
		///   <para>Vertical synchronization is disabled by default.</para>
		/// </remarks>
		public bool VerticalSyncEnabled
		{
			set => sfWindow_setVerticalSyncEnabled(Handle, value);
		}

		/// <summary>
		///   Show or hide the mouse cursor.
		/// </summary>
		/// <remarks>
		///   The mouse cursor is visible by default.
		/// </remarks>
		public bool MouseCursorVisible
		{
			set => sfWindow_setMouseCursorVisible(Handle, value);
		}

		/// <summary>
		///   Grab or release the mouse cursor.
		/// </summary>
		/// <remarks>
		///   If set to <see langword="true" />, grabs the mouse cursor inside this window's client area
		///   so it may no longer be moved outside its bounds.
		///   Note that grabbing is only active while the window has focus.
		/// </remarks>
		public bool MouseCursorGrabbed
		{
			set => sfWindow_setMouseCursorGrabbed(Handle, value);
		}

		/// <summary>
		///   Set the displayed cursor to a native system cursor.
		/// </summary>
		/// <remarks>
		///   Upon window creation, the arrow cursor is used by default.
		///   <para>Be warn, the cursor must not be destroyed while in use by the window.</para>
		///   <para>Please be advised, features related to Cursor are not supported on iOS and Android.</para>
		/// </remarks>
		/// <seealso cref="Cursor" />
		public Cursor MouseCursor
		{
			set => sfWindow_setMouseCursor(Handle,
				value is null ? throw new ArgumentNullException(nameof(value)) :
				value.IsCreated is false ? throw new ArgumentException("Cursor is not created.", nameof(value)) :
				value.Handle);
		}

		/// <summary>
		///   Enable or disable automatic key-repeat.
		/// </summary>
		/// <remarks>
		///   If key repeat is enabled,
		///   you will receive repeated <see cref="EventType.KeyPressed" /> events while keeping a key pressed.
		///   If it is disabled, you will only get a single event when the key is pressed.
		/// </remarks>
		public bool KeyRepeatEnabled
		{
			set => sfWindow_setKeyRepeatEnabled(Handle, value);
		}

		/// <summary>
		///   Limit the framerate to a maximum fixed frequency.
		/// </summary>
		/// <remarks>
		///   If a limit is set, the window will use a small delay after each call to <see cref="Display" />
		///   to ensure that the current frame lasted long enough to match the framerate limit.
		///   SFML will try to match the given limit as much as it can,
		///   but since it internally uses <c>sf::sleep</c>,
		///   whose precision depends on the underlying OS,
		///   the results may be a little unprecise as well
		///   (for example, you can get 65 FPS when requesting 60).
		/// </remarks>
		public uint FramerateLimit
		{
			set => sfWindow_setFramerateLimit(Handle, value);
		}

		/// <summary>
		///   Change the joystick threshold.
		/// </summary>
		/// <remarks>
		///   The joystick threshold is the value below which
		///   no <see cref="EventType.JoystickMoved" /> event will be generated.
		///   <para>The threshold value is 0.1 by default.</para>
		/// </remarks>
		public float JoystickThreshold
		{
			set => sfWindow_setJoystickThreshold(Handle, value);
		}

		#endregion

		#region Constructors

		/// <summary>
		///   Default constructor.
		/// </summary>
		/// <remarks>
		///   This constructor doesn't actually create the window,
		///   use the other constructors or call <see cref="Create"/> to do so.
		/// </remarks>
		public Window() { }

		/// <summary>
		///   Construct a new window.
		/// </summary>
		/// <remarks>
		///   This constructor creates the window with the size and pixel depth defined in <paramref name="mode"/>.
		///   An optional style can be passed to customize the look and behaviour of the window
		///   (borders, title bar, resizable, closable, ...).
		///   If <paramref name="style" /> contains <see cref="WindowStyle.Fullscreen" />,
		///   then <paramref name="mode" /> must be a valid video mode.
		///   <para>
		///     The <paramref name="settings"/> parameter is a reference to a structure
		///     specifying advanced OpenGL context settings such as antialiasing, depth-buffer bits, etc.
		///   </para>
		/// </remarks>
		/// <param name="mode">Video mode to use (defines the width, height and depth of the rendering area of the window).</param>
		/// <param name="title">Title of the window.</param>
		/// <param name="style">Window style, a bitwise OR combination of <see cref="WindowStyle" /> enumerators.</param>
		/// <param name="settings">Additional settings for the underlying OpenGL context.</param>
		public Window(
			VideoMode mode,
			string title,
			WindowStyle style = WindowStyle.Default,
			ContextSettings? settings = null)
		{
			Create(
				mode,
				title,
				style,
				settings ?? ContextSettings.Default);
		}

		/// <summary>
		///   Construct the window from an existing control.
		/// </summary>
		/// <remarks>
		///   Use this constructor if you want to create an OpenGL rendering area
		///   into an already existing control.
		///   <para>
		///     The <paramref name="settings"/> parameter is a reference to a structure
		///     specifying advanced OpenGL context settings such as antialiasing, depth-buffer bits, etc.
		///   </para>
		/// </remarks>
		/// <param name="handle">Platform-specific handle of the control.</param>
		/// <param name="settings">Additional settings for the underlying OpenGL context.</param>
		public Window(
			IntPtr handle,
			ContextSettings? settings = null)
		{
			Create(
				handle,
				settings ?? ContextSettings.Default);
		}

		/// <summary>
		///   Create (or recreate) the window.
		/// </summary>
		/// <remarks>
		///   If the window was already created, it closes it first.
		///   <br>
		///     If <paramref name="style" /> contains <see cref="WindowStyle.Fullscreen" />,
		///     then <paramref name="mode" /> must be a valid video mode.
		///   </br>
		///   <para>
		///     The <paramref name="settings" /> parameter is an optional structure
		///     specifying advanced OpenGL context settings such as antialiasing, depth-buffer bits, etc.
		///   </para>
		/// </remarks>
		/// <param name="mode">Video mode to use (defines the width, height and depth of the rendering area of the window).</param>
		/// <param name="title">Title of the window.</param>
		/// <param name="style">Window style, a bitwise OR combination of <see cref="WindowStyle" /> enumerators.</param>
		/// <param name="settings">Additional settings for the underlying OpenGL context.</param>
		public void Create(
			VideoMode mode,
			string title,
			WindowStyle style = WindowStyle.Default,
			ContextSettings? settings = null)
		{
			Close();
			OnCreate(mode, title, style, settings);
		}

		private protected virtual void OnCreate(
			VideoMode mode,
			string title,
			WindowStyle style = WindowStyle.Default,
			ContextSettings? settings = null)
		{
			fixed (ContextSettings.Native* settings_ptr = settings)
			{
				Handle = sfWindow_createUnicode(
					mode,
					UTF32Ptr.ToPointer(title),
					style,
					settings_ptr);
			}
		}

		/// <summary>
		///   Create (or recreate) the window from an existing control.
		/// </summary>
		/// <remarks>
		///   Use this function if you want to create an OpenGL rendering area
		///   into an already existing control.
		///   <br>
		///     If the window was already created, it closes it first.
		///   </br>
		///   <para>
		///     The second parameter is an optional structure specifying advanced OpenGL context settings
		///     such as antialiasing, depth-buffer bits, etc.
		///   </para>
		/// </remarks>
		/// <param name="handle">Platform-specific handle of the control.</param>
		/// <param name="settings">Additional settings for the underlying OpenGL context.</param>
		public void Create(
			IntPtr handle,
			ContextSettings? settings = null)
		{
			Close();
			OnCreate(handle, settings);
		}

		private protected virtual void OnCreate(IntPtr handle,
			ContextSettings? settings = null)
		{
			fixed (ContextSettings.Native* settings_ptr = settings)
			{
				Handle = sfWindow_createFromHandle(handle, settings_ptr);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///   Close the window and destroy all the attached resources.
		/// </summary>
		/// <remarks>
		///   After calling this function, the <see cref="Window" /> instance remains valid
		///   and you can call <see cref="Create" /> to recreate the window.
		///   All other functions such as <see cref="PollEvent" /> or <see cref="Display"/>
		///   will still work (i.e. you don't have to test <see cref="IsOpen"/> every time),
		///   and will have no effect on closed windows.
		/// </remarks>
		public void Close()
		{
			if (Handle is not null)
			{
				sfWindow_close(Handle);
				sfWindow_destroy(Handle);
			}
		}

		/// <summary>
		///   Pop the event on top of the event queue, if any, and return it.
		/// </summary>
		/// <remarks>
		///   This function is not blocking:
		///   if there's no pending event then it will return <see langword="false" />
		///   and leave a default <paramref name="event" />.
		///   Note that more than one event may be present in the event queue,
		///   thus you should always call this function in a loop
		///   to make sure that you process every pending event.
		///   <example><code>
		///     while (window.PollEvent(out Event @event))
		///     {
		///       // process event ...
		///     }
		///   </code></example>
		/// </remarks>
		/// <param name="event">Event to be returned.</param>
		/// <returns>
		///   <see langword="true"/> if an event was returned,
		///   or <see langword="false"/> if the event queue was empty.
		/// </returns>
		/// <seealso cref="WaitEvent(out Event)" />
		public bool PollEvent(out Event @event)
		{
			ThrowIfNotOpen();
			return sfWindow_pollEvent(Handle, out @event);
		}

		/// <summary>
		///   Wait for an event and return it.
		/// </summary>
		/// <remarks>
		///   This function is blocking:
		///   if there's no pending event then it will wait until an event is received.
		///   After this function returns (and no error occurred),
		///   the <paramref name="event"/> object is always valid and filled properly.
		///   This function is typically used when you have a thread that s dedicated to events handling:
		///   you want to make this thread sleep as long as no new event is received.
		///   <example><code>
		///     if (window.WaitEvent(out Event @event))
		///     {
		///       // process event ...
		///     }
		///   </code></example>
		/// </remarks>
		/// <param name="event">Event to be returned.</param>
		/// <returns><see langword="false" /> if any error occurred.</returns>
		/// <seealso cref="PollEvent(out Event)" />
		public bool WaitEvent(out Event @event)
		{
			ThrowIfNotOpen();
			return sfWindow_waitEvent(Handle, out @event);
		}

		/// <summary>
		///   Sets the window's icon.
		/// </summary>
		internal void SetIcon(uint width, uint height, byte* pixels)
		{
			ThrowIfNotOpen();
			//if (pixels is null) throw new ArgumentNullException(nameof(pixels));

			sfWindow_setIcon(Handle,
				width,
				height,
				pixels);
		}

		/// <inheritdoc cref="SetIcon(uint, uint, byte*)"/>
		public void SetIcon(uint width, uint height, ReadOnlySpan<byte> pixels)
		{
			if (pixels.IsEmpty) throw new ArgumentException("Pixels is empty.", nameof(pixels));
			if ((uint)pixels.Length != width * height) throw new ArgumentException("Pixels length does not match size parameter.", nameof(pixels));

			fixed (byte* pixels_ptr = pixels)
			{
				SetIcon(
					width,
					height,
					pixels_ptr);
			}
		}

		/// <inheritdoc cref="SetIcon(uint, uint, ReadOnlySpan{byte})"/>
		public void SetIcon(IReadOnlyVector2<uint> size, ReadOnlySpan<byte> pixels)
		{
			SetIcon(size.X, size.Y, pixels);
		}

		/// <summary>
		///   Activate or deactivate the window as the current target for OpenGL rendering.
		/// </summary>
		/// <remarks>
		///   A window is active only on the current thread,
		///   if you want to make it active on another thread
		///   you have to deactivate it on the previous thread first if it was active.
		///   Only one window can be active on a thread at a time,
		///   thus the window previously active (if any) automatically gets deactivated.
		///   <br>This is not to be confused with <seealso cref="RequestFocus" />.</br>
		/// </remarks>
		/// <param name="active"><see langword="true" /> to activate, <see langword="false" /> to deactivate.</param>
		/// <returns>
		///   <see langword="true" /> if operation was successful;
		///   <see langword="false" /> otherwise.
		/// </returns>
		public bool SetActive(bool active)
		{
			ThrowIfNotOpen();
			return sfWindow_setActive(Handle, active);
		}

		/// <summary>
		///   Request the current window to be made the active foreground window.
		/// </summary>
		/// <remarks>
		///   At any given time,
		///   only one window may have the input focus to receive input events such as keystrokes or mouse events.
		///   If a window requests focus, it only hints to the operating system,
		///   that it would like to be focused.
		///   The operating system is free to deny the request.
		///   <br>This is not to be confused with <seealso cref="SetActive"/>.</br>
		/// </remarks>
		public void RequestFocus()
		{
			ThrowIfNotOpen();
			sfWindow_requestFocus(Handle);
		}

		/// <summary>
		///   Check whether the window has the input focus.
		/// </summary>
		/// <remarks>
		///   At any given time,
		///   only one window may have the input focus to receive input events
		///   such as keystrokes or most mouse events.
		/// </remarks>
		/// <returns>
		///   <see langword="true" /> if window has focus;
		///   <see langword="false" /> otherwise.
		/// </returns>
		public bool HasFocus()
		{
			ThrowIfNotOpen();
			return sfWindow_hasFocus(Handle);
		}

		/// <summary>
		///   Display on screen what has been rendered to the window so far.
		/// </summary>
		/// <remarks>
		///   This function is typically called after all OpenGL rendering
		///   has been done for the current frame,
		///   in order to show it on screen.
		/// </remarks>
		public void Display()
		{
			ThrowIfNotOpen();
			sfWindow_display(Handle);
		}

		/// <summary>
		///   Get the OS-specific handle of the window.
		/// </summary>
		/// <remarks>
		///   The type of the returned handle is <c>sf::WindowHandle</c>,
		///   which is a native typedef to the handle type defined by the OS.
		///   You shouldn't need to use this function,
		///   unless you have very specific stuff to implement that SFML doesn't support,
		///   or implement a temporary workaround until a bug is fixed.
		/// </remarks>
		/// <returns>System handle of the window.</returns>
		public IntPtr GetSystemHandle()
		{
			ThrowIfNotOpen();
			return sfWindow_getSystemHandle(Handle);
		}

		private protected void ThrowIfNotOpen()
		{
			if (IsOpen is false) ThrowNotOpen();
		}

		[DoesNotReturn]
		private protected static void ThrowNotOpen()
		{
			throw new InvalidOperationException("Window is closed.");
		}

		#endregion

		#region Interface Method Implementations

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Window()
		{
			Dispose(disposing: false);
		}

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			if (disposing) Close();
			_disposed = true;
		}

		#endregion

		#region Imports

		internal readonly struct Native { }

		//[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		//private static extern IntPtr sfWindow_create(
		//	VideoMode mode,
		//	string title,
		//	WindowStyle style,
		//	[Const] ContextSettings settings);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfWindow_createUnicode(
			VideoMode mode,
			uint* title,
			WindowStyle style,
			ContextSettings.Native* settings);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfWindow_createFromHandle(
			IntPtr handle,
			ContextSettings.Native* settings);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_destroy(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_close(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfWindow_isOpen(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern ContextSettings sfWindow_getSettings(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfWindow_pollEvent(Native* window, out Event @event);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfWindow_waitEvent(Native* window, out Event @event);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2I sfWindow_getPosition(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setPosition(Native* window, Vector2I position);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2U sfWindow_getSize(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setSize(Native* window, Vector2U size);

		//[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		//private static extern void sfWindow_setTitle(Native* window, string title);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setUnicodeTitle(Native* window, uint* title);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setIcon(Native* window, uint width, uint height, byte* pixels);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setVisible(Native* window, bool visible);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setVerticalSyncEnabled(Native* window, bool enabled);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setMouseCursorVisible(Native* window, bool visible);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setMouseCursorGrabbed(Native* window, bool grabbed);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setMouseCursor(Native* window, Cursor.Native* cursor);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setKeyRepeatEnabled(Native* window, bool enabled);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setFramerateLimit(Native* window, uint limit);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_setJoystickThreshold(Native* window, float threshold);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfWindow_setActive(Native* window, bool active);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_requestFocus(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfWindow_hasFocus(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfWindow_display(Native* window);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr sfWindow_getSystemHandle(Native* window);

		#endregion
	}
}
