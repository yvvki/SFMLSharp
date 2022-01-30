using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Window.DllName;

namespace SFML.Window
{
	/// <summary>
	///   Provides access to the system clipboard.
	/// </summary>
	public static class Clipboard
	{
		/// <summary>
		///   The content of the clipboard as string data.
		/// </summary>
		/// <remarks>
		///   Due to limitations on some operating systems,
		///   setting the clipboard contents is only guaranteed to work
		///   if there is currently an open window for which events are being handled.
		/// </remarks>
		public static unsafe string? String
		{
			get
			{
				uint* value = sfClipboard_getUnicodeString();
				string? result = UTF32Helper.GetString(value);
				return result;
			}
			set
			{
				byte[]? value_utf32 = UTF32Helper.GetBytes(value);
				fixed (byte* value_ptr = value_utf32)
				{
					sfClipboard_setUnicodeString((uint*)value_ptr);
				}
			}
		}

		#region Imports

		//[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		//private static extern string? sfClipboard_getString();

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe uint* sfClipboard_getUnicodeString();

		//[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
		//private static extern void sfClipboard_setString(string? text);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern unsafe void sfClipboard_setUnicodeString(uint* text);

		#endregion
	}
}
