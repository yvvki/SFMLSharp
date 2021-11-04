using System.Runtime.InteropServices;

using static SFML.System.DllName;

namespace SFML.System
{
	/// <summary>
	///   Represents an utility class that measures the elapsed time.
	/// </summary>
	public unsafe class Clock : IDisposable
	{
		internal readonly IntPtr _handle;

		/// <summary>
		///   Gets the elapsed time.
		/// </summary>
		/// <remarks>
		///   This property returns the time elapsed since the last call to <see cref="Restart" />
		///   (or the construction of the instance if <see cref="Restart" /> has not been called).
		/// </remarks>
		public Time ElapsedTime => sfClock_getElapsedTime(_handle);

		/// <summary>
		///   Construct a new instance of the <see cref="Clock" /> class.
		/// </summary>
		/// <remarks>
		///   The clock starts automatically after being constructed.
		/// </remarks>
		public Clock()
		{
			_handle = sfClock_create();
		}

		/// <summary>
		///   Restarts the clock.
		/// </summary>
		/// <remarks>
		///   This function puts the time counter back to zero.
		///   It also returns the time elapsed since the clock was started.
		/// </remarks>
		/// <returns>Time elapsed.</returns>
		public Time Restart()
		{
			return sfClock_restart(_handle);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Clock() => Dispose(false);

		private bool _disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			if (disposing) sfClock_destroy(_handle);
			_disposed = true;
		}

		#region Imports

		public static explicit operator IntPtr(Clock value)
		{
			return value._handle;
		}

		[DllImport(csfml_system, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr sfClock_create();

		//[DllImport(csfml_system, CallingConvention = CallingConvention.Cdecl)]
		//private static extern Wrapper* sfClock_copy(Wrapper* clock);

		[DllImport(csfml_system, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfClock_destroy(IntPtr clock);

		[DllImport(csfml_system, CallingConvention = CallingConvention.Cdecl)]
		private static extern Time sfClock_getElapsedTime(IntPtr clock);

		[DllImport(csfml_system, CallingConvention = CallingConvention.Cdecl)]
		private static extern Time sfClock_restart(IntPtr clock);

		#endregion
	}
}
