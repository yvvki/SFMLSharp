using System.Runtime.InteropServices;

using SFML.System;
using SFML.Window;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	/* Sorry SFML devs, this actually works...
	 * 
	 * Since the wrapper is just a pointer to an empty struct,
	 * and the CSFML function calls sfWindow->This and sfRenderWindow->This,
	 * they are getting the first field of that pointer,
	 * and so "it just works".
	 * 
	 * TODO: Please fix with private protected methods.
	 */
	public unsafe class RenderWindow : Window.Window, IRenderTarget
	{
		private View? _view;
		public View View
		{
			get
			{
				ThrowIfNotCreated();
				return _view!;
			}
			set => _view = value;
		}

		private View? _defaultView;
		public View DefaultView
		{
			get
			{
				ThrowIfNotCreated();
				return _defaultView!;
			}
		}

		public Rect<int> Viewport { get; }

		#region Constructors & Create Methods

		public RenderWindow() : base() { }

		public RenderWindow(
			VideoMode mode,
			string title,
			WindowStyle style = WindowStyle.Default,
			ContextSettings? settings = null)
			: base(mode, title, style, settings) { }

		public RenderWindow(
			IntPtr handle,
			ContextSettings? settings = null)
			: base(handle, settings) { }

		private protected override void OnCreate(
			VideoMode mode,
			string title,
			WindowStyle style,
			ContextSettings? settings = null)
		{
			Handle = sfRenderWindow_createUnicode(
				mode,
				UTF32Ptr.ToPointer(title),
				style,
				settings ?? ContextSettings.Default);
			Initialize();
		}

		private protected override void OnCreate(
			IntPtr handle,
			ContextSettings? settings = null)
		{
			Handle = sfRenderWindow_createFromHandle(
				handle,
				settings ?? ContextSettings.Default);
			Initialize();
		}

		private void Initialize()
		{
			_defaultView = new(sfRenderWindow_getDefaultView(Handle));
			_view = new(sfRenderWindow_getView(Handle));
		}

		#endregion

		#region Methods

		public void Clear(Color? color = null)
		{
			ThrowIfNotCreated();
			sfRenderWindow_clear(Handle, color ?? Color.Black);
		}

		public Vector2<float> MapPixelToCoords(Vector2<int> point)
		{
			return MapPixelToCoords(point, null);
		}

		public Vector2<float> MapPixelToCoords(Vector2<int> point, View? view)
		{
			ThrowIfNotCreated();
			return sfRenderWindow_mapPixelToCoords(Handle, point, view!.Handle);
		}

		public Vector2<int> MapCoordsToPixel(Vector2<float> point)
		{
			return MapCoordsToPixel(point, null);
		}

		public Vector2<int> MapCoordsToPixel(Vector2<float> point, View? view)
		{
			ThrowIfNotCreated();
			return sfRenderWindow_mapCoordsToPixel(Handle, point, view!.Handle);
		}

		public void Draw(IDrawable drawable, RenderStates? states = null)
		{
			drawable.Draw(this, states!);
		}

		public void Draw(ReadOnlySpan<Vertex> vertices, PrimitiveType type, RenderStates? states)
		{
			ThrowIfNotCreated();
			fixed (Vertex* vertices_ptr = vertices)
			{
				fixed (RenderStates.Native* states_ptr = states)
				{
					sfRenderWindow_drawPrimitives(
					Handle,
					vertices_ptr,
					(uint)vertices.Length,
					type,
					states_ptr);
				}
			}
		}

		void IRenderTarget.Draw(Sprite sprite, RenderStates.Native* states)
		{
			ThrowIfNotCreated();
			sfRenderWindow_drawSprite(Handle, sprite.Handle, states);
		}

		void IRenderTarget.Draw(Text text, RenderStates.Native* states)
		{
			ThrowIfNotCreated();
			sfRenderWindow_drawText(Handle, text.Handle, states);
		}

		void IRenderTarget.Draw(Shape shape, RenderStates.Native* states)
		{
			ThrowIfNotCreated();
			sfRenderWindow_drawShape(Handle, shape.Handle, states);
		}

		void IRenderTarget.Draw(CircleShape shape, RenderStates.Native* states)
		{
			ThrowIfNotCreated();
			sfRenderWindow_drawCircleShape(Handle, shape.Handle, states);
		}

		void IRenderTarget.Draw(RectangleShape shape, RenderStates.Native* states)
		{
			ThrowIfNotCreated();
			sfRenderWindow_drawRectangleShape(Handle, shape.Handle, states);
		}

		#endregion

		#region Imports

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private static extern unsafe Native* sfRenderWindow_create(
		//	VideoMode mode,
		//	string title,
		//	WindowStyle style,
		//	ContextSettings settings);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe Native* sfRenderWindow_createUnicode(
			VideoMode mode,
			uint* title,
			WindowStyle style,
			ContextSettings settings);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe Native* sfRenderWindow_createFromHandle(
			IntPtr handle,
			ContextSettings settings);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_destroy(Native* window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_close(IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern bool sfRenderWindow_isOpen([Const] IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern ContextSettings sfRenderWindow_getSettings([Const] IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern bool sfRenderWindow_pollEvent(IntPtr window, out Event @event);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern bool sfRenderWindow_waitEvent(IntPtr window, out Event @event);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern Vector2i sfRenderWindow_getPosition([Const] IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setPosition(IntPtr window, Vector2i position);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern Vector2u sfRenderWindow_getSize([Const] IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setSize(IntPtr window, Vector2u size);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		//private unsafe static extern void sfRenderWindow_setTitle(IntPtr window, string title);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setUnicodeTitle(IntPtr window, UTF32Ptr title);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setIcon(IntPtr window, uint width, uint height, byte[] pixels);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setVisible(IntPtr window, bool visible);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setVerticalSyncEnabled(IntPtr window, bool enabled);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setMouseCursorVisible(IntPtr window, bool visible);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setMouseCursorGrabbed(IntPtr window, bool grabbed);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setMouseCursor(IntPtr window, Cursor.IntPtr cursor);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setKeyRepeatEnabled(IntPtr window, bool enabled);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setFramerateLimit(IntPtr window, uint limit);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_setJoystickThreshold(IntPtr window, float threshold);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern bool sfRenderWindow_setActive(IntPtr window, bool active);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_requestFocus(IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern bool sfRenderWindow_hasFocus([Const] IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_display(IntPtr window);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern IntPtr sfRenderWindow_getSystemHandle([Const] IntPtr window);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_clear(Native* renderWindow, Color color);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_setView(Native* renderWindow, View.Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe View.Native* sfRenderWindow_getView(Native* renderWindow);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe View.Native* sfRenderWindow_getDefaultView(Native* renderWindow);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe View.Native* sfRenderWindow_getViewport(Native* renderWindow, View.Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe Vector2<float> sfRenderWindow_mapPixelToCoords(Native* renderWindow, Vector2<int> point, View.Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe Vector2<int> sfRenderWindow_mapCoordsToPixel(Native* renderWindow, Vector2<float> point, View.Native* view);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_drawSprite(
			Native* renderWindow,
			Sprite.Native* @object,
			RenderStates.Native* states);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_drawText(
			Native* renderWindow,
			Text.Native* @object,
			RenderStates.Native* states);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_drawShape(
			Native* renderWindow,
			Shape.Native* @object,
			RenderStates.Native* states);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_drawCircleShape(
			Native* renderWindow,
			Shape.Native* @object,
			RenderStates.Native* states);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_drawConvexShape(
		//	Native* renderWindow,
		//	[Const] sfConvexShape* @object,
		//	[Const] RenderStates.Unmanaged* states);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_drawRectangleShape(
			Native* renderWindow,
			Shape.Native* @object,
			RenderStates.Native* states);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_drawVertexArray(
		//	Native* renderWindow,
		//	VertexArray.Unmanaged* @object,
		//	RenderStates.IntPtr states);

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private unsafe static extern void sfRenderWindow_drawVertexBuffer(
		//	Native* renderWindow,
		//	VertexBuffer.Unmanaged* @object,
		//	RenderStates.IntPtr states);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfRenderWindow_drawPrimitives(
			Native* renderWindow,
			Vertex* vertices,
			nuint vertexCount,
			PrimitiveType type,
			RenderStates.Native* states);

		#endregion
	}
}
