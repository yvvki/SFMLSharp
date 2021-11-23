using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SFML.Window
{
	[Flags]
	public enum ContextSettingsAttribute : uint
	{
		/// <summary>
		///   Core attribute.
		/// </summary>
		Core = 1 << 0,
		/// <summary>
		///   Debug attribute.
		/// </summary>
		Debug = 1 << 2
	}

	/// <summary>
	///   Structure defining the window's creation settings.
	/// </summary>
	/// <param name="DepthBits">Bits of the depth buffer.</param>
	/// <param name="StencilBits">Bits of the stencil buffer.</param>
	/// <param name="AntialiasingLevel">Level of antialiasing.</param>
	/// <param name="MajorVersion">Major number of the context version to create.</param>
	/// <param name="MinorVersion">Minor number of the context version to create.</param>
	/// <param name="AttributeFlags">The attribute flags to create the context with.</param>
	/// <param name="SRgbCapable">Whether the context framebuffer is sRGB capable.</param>
	[StructLayout(LayoutKind.Sequential)]
	public record class ContextSettings(
		uint DepthBits = 0,
		uint StencilBits = 0,
		uint AntialiasingLevel = 0,
		uint MajorVersion = 1,
		uint MinorVersion = 1,
		ContextSettingsAttribute AttributeFlags = default,
		bool SRgbCapable = false)
	{
		public static readonly ContextSettings Default = new();

		internal readonly Native Handle = new(
				DepthBits,
				StencilBits,
				AntialiasingLevel,
				MajorVersion,
				MinorVersion,
				(uint)AttributeFlags,
				SRgbCapable);
		internal ref Native GetPinnableReference()
		{
			return ref Unsafe.AsRef(in Handle);
		}


		internal struct Native
		{
			public uint DepthBits;
			public uint StencilBits;
			public uint AntialiasingLevel;
			public uint MajorVersion;
			public uint MinorVersion;
			public uint AttributeFlags;
			public bool SRgbCapable;

			public Native(
				uint depthBits,
				uint stencilBits,
				uint antialiasingLevel,
				uint majorVersion,
				uint minorVersion,
				uint attributeFlags,
				bool sRgbCapable)
			{
				DepthBits = depthBits;
				StencilBits = stencilBits;
				AntialiasingLevel = antialiasingLevel;
				MajorVersion = majorVersion;
				MinorVersion = minorVersion;
				AttributeFlags = attributeFlags;
				SRgbCapable = sRgbCapable;
			}
		}
	};
}
