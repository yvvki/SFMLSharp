using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

using SFML.System;

namespace SFML.Window
{
	public static class VideoModeExtensions
	{
		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2<uint> ToGeneric(this VideoMode @this)
		{
			VideoMode result = @this;
			return AsGeneric(ref result);
		}

		[RequiresPreviewFeatures]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2<uint> AsGeneric(ref VideoMode mode)
		{
			return Unsafe.As<VideoMode, Vector2<uint>>(ref mode);
		}
	}
}
