using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SFML.Graphics
{
	public record RenderStates(
		BlendMode BlendMode,
		Transform Transform,
		Texture? Texture,
		Shader? Shader)
	{
		public static readonly RenderStates Default = new();

		public RenderStates() : this(BlendMode.Alpha) { }

		public RenderStates(Transform transform) : this(
			BlendMode.Alpha,
			transform,
			default,
			default)
		{ }

		public RenderStates(BlendMode blendMode) : this(
			blendMode,
			Transform.Identity,
			default,
			default)
		{ }

		public RenderStates(Texture texture) : this(
			BlendMode.Alpha,
			Transform.Identity,
			texture,
			default)
		{ }

		public RenderStates(Shader shader) : this(
			BlendMode.Alpha,
			Transform.Identity,
			null,
			shader)
		{ }

		internal readonly unsafe Native Handle = new(
			BlendMode,
			Transform,
			Texture is not null ? Texture.Handle : null,
			Shader is not null ? Shader.Handle : null);
		internal unsafe ref Native GetPinnableReference()
		{
			return ref Unsafe.AsRef(in Handle);
		}

		internal readonly unsafe struct Native
		{
			public readonly BlendMode BlendMode;
			public readonly Transform Transform;
			public readonly Texture.Native* Texture;
			public readonly Shader.Native* Shader;

			public Native(
				BlendMode blendMode,
				Transform transform,
				Texture.Native* texture,
				Shader.Native* shader)
			{
				BlendMode = blendMode;
				Transform = transform;
				Texture = texture;
				Shader = shader;
			}
		}
	}
}
