using System.Runtime.InteropServices;

using static SFML.Window.DllName;

namespace SFML.Window
{
	public unsafe class Context : IDisposable
	{
		internal readonly IntPtr Handle;

		/// <summary>
		///   Get the settings of the context.
		/// </summary>
		/// <remarks>
		///   Note that these settings may be different than the ones passed to the constructor;
		///   they are indeed adjusted if the original settings are not directly supported by the system.
		/// </remarks>
		public unsafe ContextSettings Settings => sfContext_getSettings(Handle);

		/// <summary>
		///   Singleton global context.
		/// </summary>
		public static readonly Context Global = new();

		public Context()
		{
			Handle = sfContext_create();
		}

		/// <summary>
		///   Activate or deactivate explicitely a context.
		/// </summary>
		/// <param name="active">
		///   <see langword="true" /> to activate,
		///   <see langword="false" /> to deactivate.
		/// </param>
		/// <returns>
		///   <see langword="true" /> on success,
		///   <see langword="false" /> on failure.
		/// </returns>
		public unsafe bool SetActive(bool active)
		{
			return sfContext_setActive(Handle, active);
		}

		/// <summary>
		///   Get the currently active context's ID.
		/// </summary>
		/// <remarks>
		///   The context ID is used to identify contexts when managing unshareable OpenGL resources.
		/// </remarks>
		/// <returns>The active context's ID or 0 if no context is currently active.</returns>
		public static ulong GetActiveContextId()
		{
			return sfContext_getActiveContextId();
		}

		public void Dispose()
		{
			Dispose(disposing: false);
			GC.SuppressFinalize(this);
		}

		~Context()
		{
			Dispose(disposing: false);
		}

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			if (disposing) sfContext_destroy(Handle);
			_disposed = true;
		}

		#region Imports

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe IntPtr sfContext_create();

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfContext_destroy(IntPtr context);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe bool sfContext_setActive(IntPtr context, bool active);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe ContextSettings sfContext_getSettings(IntPtr context);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe ulong sfContext_getActiveContextId();

		#endregion
	}
}
