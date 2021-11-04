using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Thanks to Zombie for helping me on this!
// https://github.com/DaZombieKiller
namespace SFML.System
{
	/// <summary>
	///   Represents a wrapper struct to use <see cref="Stream" /> as native SFML input stream.
	/// </summary>
	internal readonly unsafe ref struct InputStream
	{
		public readonly delegate* unmanaged[Cdecl]<void*, long, void*, long> ReadFunc;
		public readonly delegate* unmanaged[Cdecl]<long, void*, long> SeekFunc;
		public readonly delegate* unmanaged[Cdecl]<void*, long> TellFunc;
		public readonly delegate* unmanaged[Cdecl]<void*, long> GetSizeFunc;
		public readonly IntPtr UserData; // Stream as GCHandle address

		public InputStream(Stream stream)
		{
			ReadFunc = &Read;
			SeekFunc = &Seek;
			TellFunc = &Tell;
			GetSizeFunc = &GetSize;
			UserData = GCHandle.ToIntPtr(GCHandle.Alloc(stream));
		}

		[UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
		private static long Read(void* data, long size, void* userData)
		{
			return GetTarget((IntPtr)userData)
				.Read(new Span<byte>(data, size > int.MaxValue ? int.MaxValue : (int)size));
		}

		[UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
		private static long Seek(long position, void* userData)
		{
			return GetTarget((IntPtr)userData)
				.Seek(position, SeekOrigin.Begin);
		}

		[UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
		private static long Tell(void* userData)
		{
			return GetTarget((IntPtr)userData).Position;
		}

		[UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
		private static long GetSize(void* userData)
		{
			return GetTarget((IntPtr)userData).Length;
		}

		private static Stream GetTarget(IntPtr value)
		{
			return (Stream)GCHandle.FromIntPtr(value).Target!;
		}

		public void Dispose()
		{
			GCHandle handle = GCHandle.FromIntPtr(UserData);

			if (handle.IsAllocated) handle.Free();
		}
	}
}
