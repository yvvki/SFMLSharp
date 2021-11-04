using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public enum ShaderType
	{
		Vertex,
		Geometry,
		Fragment
	}

	public unsafe class Shader : IDisposable
	{
		internal readonly Native* Handle;

		internal Shader(Native* handle)
		{
			Handle = handle;
		}

		public static Shader FromFile(string filename, ShaderType type)
		{
			if (filename is null) throw new ArgumentNullException(nameof(filename));

			return new(type switch
			{
				ShaderType.Vertex => sfShader_createFromFile(filename, null, null),
				ShaderType.Geometry => sfShader_createFromFile(null, filename, null),
				ShaderType.Fragment => sfShader_createFromFile(null, null, filename),
				_ => throw new NotSupportedException(),
			});
		}

		public static Shader FromFile(
			string vertexShaderFilename,
			string fragmentShaderFilename)
		{
			if (vertexShaderFilename is null) throw new ArgumentNullException(nameof(vertexShaderFilename));
			if (fragmentShaderFilename is null) throw new ArgumentNullException(nameof(fragmentShaderFilename));

			return new(sfShader_createFromFile(
				vertexShaderFilename,
				null,
				fragmentShaderFilename));
		}

		public static Shader FromFile(
			string vertexShaderFilename,
			string geometryShaderFilename,
			string fragmentShaderFilename)
		{
			if (vertexShaderFilename is null) throw new ArgumentNullException(nameof(vertexShaderFilename));
			if (geometryShaderFilename is null) throw new ArgumentNullException(nameof(geometryShaderFilename));
			if (fragmentShaderFilename is null) throw new ArgumentNullException(nameof(fragmentShaderFilename));

			return new(sfShader_createFromFile(
				vertexShaderFilename,
				geometryShaderFilename,
				fragmentShaderFilename));
		}

		public static Shader FromMemory(string shader, ShaderType type)
		{
			if (shader is null) throw new ArgumentNullException(nameof(shader));

			return new(type switch
			{
				ShaderType.Vertex => sfShader_createFromMemory(shader, null, null),
				ShaderType.Geometry => sfShader_createFromMemory(null, shader, null),
				ShaderType.Fragment => sfShader_createFromMemory(null, null, shader),
				_ => throw new NotSupportedException(),
			});
		}

		public static Shader FromMemory(
			string vertexShader,
			string fragmentShader)
		{
			if (vertexShader is null) throw new ArgumentNullException(nameof(vertexShader));
			if (fragmentShader is null) throw new ArgumentNullException(nameof(fragmentShader));

			return new(sfShader_createFromMemory(
				vertexShader,
				null,
				fragmentShader));
		}

		public static Shader FromMemory(
			string vertexShader,
			string geometryShader,
			string fragmentShader)
		{
			if (vertexShader is null) throw new ArgumentNullException(nameof(vertexShader));
			if (geometryShader is null) throw new ArgumentNullException(nameof(geometryShader));
			if (fragmentShader is null) throw new ArgumentNullException(nameof(fragmentShader));

			return new(sfShader_createFromMemory(
				vertexShader,
				geometryShader,
				fragmentShader));
		}

		public static Shader FromStream(Stream shaderStream, ShaderType type)
		{
			if (shaderStream is null) throw new ArgumentNullException(nameof(shaderStream));

			using InputStream shaderHandle = new(shaderStream);

			return new(type switch
			{
				ShaderType.Vertex => sfShader_createFromStream(&shaderHandle, null, null),
				ShaderType.Geometry => sfShader_createFromStream(null, &shaderHandle, null),
				ShaderType.Fragment => sfShader_createFromStream(null, null, &shaderHandle),
				_ => throw new NotSupportedException(),
			});
		}

		public static Shader FromStream(
			Stream vertexShaderStream,
			Stream fragmentShaderStream)
		{
			using InputStream vertexShaderHandle = new(vertexShaderStream);
			using InputStream fragmentShaderHandle = new(fragmentShaderStream);

			return new(sfShader_createFromStream(
				&vertexShaderHandle,
				null,
				&fragmentShaderHandle));
		}

		public static Shader FromStream(
			Stream vertexShaderStream,
			Stream geometryShaderStream,
			Stream fragmentShaderStream)
		{
			using InputStream vertexShaderHandle = new(vertexShaderStream);
			using InputStream geometryShaderHandle = new(geometryShaderStream);
			using InputStream fragmentShaderHandle = new(fragmentShaderStream);

			return new(sfShader_createFromStream(
				&vertexShaderHandle,
				&geometryShaderHandle,
				&fragmentShaderHandle));
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Shader()
		{
			Dispose(disposing: false);
		}

		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			if (disposing) sfShader_destroy(Handle);
			_disposed = true;
		}

		#region Import

		internal readonly struct Native { }

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern Native* sfShader_createFromFile(
			string? vertexShaderFilename,
			string? geometryShaderFilename,
			string? fragmentShaderFilename);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern Native* sfShader_createFromMemory(
			string? vertexShader,
			string? geometryShader,
			string? fragmentShader);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Native* sfShader_createFromStream(
			InputStream* vertexShader,
			InputStream* geometryShader,
			InputStream* fragmentShader);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfShader_destroy(Native* shader);

		#endregion
	}
}
