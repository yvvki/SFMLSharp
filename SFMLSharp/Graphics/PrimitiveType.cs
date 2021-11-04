namespace SFML.Graphics
{
	/// <summary>
	///   Types of primitives that a VertexArray can render.
	/// </summary>
	/// <remarks>
	///   Points and lines have no area, therefore their thickness will always be 1 pixel,
	///   regardless the current transform and view.
	/// </remarks>
	public enum PrimitiveType
	{
		/// <summary>
		///   List of individual points.
		/// </summary>
		Points,
		/// <summary>
		///   List of individual lines.
		/// </summary>
		Lines,
		/// <summary>
		///   List of connected lines, a point uses the previous point to form a line.
		/// </summary>
		LineStrip,
		/// <summary>
		///   List of individual triangles.
		/// </summary>
		Triangles,
		/// <summary>
		///   List of connected triangles, a point uses the two previous points to form a triangle.
		/// </summary>
		TriangleStrip,
		/// <summary>
		///   List of connected triangles, a point uses the common center and the previous point to form a triangle.
		/// </summary>
		TriangleFan,
		/// <summary>
		///   List of individual quads.
		/// </summary>
		[Obsolete("Don't work with OpenGL ES")]
		Quads,

		// Deprecated names
		[Obsolete("Use LineStrip instead.")]
		LinesStrip = LineStrip,
		[Obsolete("Use TriangleStrip instead.")]
		TrianglesStrip = TriangleStrip,
		[Obsolete(" Use TriangleFan instead.")]
		TrianglesFan = TriangleFan
	}
}
