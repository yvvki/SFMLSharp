using System.Text;

namespace SFML.System
{
	/// <summary>
	///   Utility struct to parse unsigned 32-bit interger pointer as string and back.
	/// </summary>
	internal static unsafe class UTF32Ptr
	{
		public static int GetLength(uint* data)
		{
			if (data is null) return default;

			uint* ptr = data;
			while (*ptr is not default(uint))
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

		public static uint* ToPointer(string? str)
		{
			if (str is null) return default;
			fixed (byte* data = Encoding.UTF32.GetBytes(str)) return (uint*)data;
		}
	}
}