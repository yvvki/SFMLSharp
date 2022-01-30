using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SFML.System
{
	/// <summary>
	///   Utility struct to parse unsigned 32-bit interger pointer as string and back.
	/// </summary>
	internal unsafe class UTF32Helper
	{
		public static int GetLength(uint* data)
		{
			if (data is null) return default;

			uint* ptr = data;
			while (*ptr is not 0)
			{
				ptr++;
			}

			return (int)(ptr - data);
		}

		public static string? GetString(uint* data)
		{
			if (data is null) return default;

			return Encoding.UTF32.GetString(
				(byte*)data,
				GetLength(data) * sizeof(uint));
		}

		public static byte[]? GetBytes(string? s)
		{
			if (s is null) return default;

			return Encoding.UTF32.GetBytes(s);
		}
	}
}