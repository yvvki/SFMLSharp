namespace SFML.Graphics
{
	/// <summary>
	///   Enumeration of the blending factors.
	/// </summary>
	public enum BlendModeFactor
	{
		/// <summary>
		///   (0, 0, 0, 0)
		/// </summary>
		Zero,
		/// <summary>
		///   (1, 1, 1, 1)
		/// </summary>
		One,
		/// <summary>
		///   (src.R, src.G, src.B, src.A)
		/// </summary>
		SrcColor,
		/// <summary>
		///   (1, 1, 1, 1) - (src.R, src.G, src.B, src.A)
		/// </summary>
		OneMinusSrcColor,
		/// <summary>
		///   (dst.R, dst.G, dst.B, dst.A)
		/// </summary>
		DstColor,
		/// <summary>
		///   (1, 1, 1, 1) - (dst.R, dst.G, dst.B, dst.A)
		/// </summary>
		OneMinusDstColor,
		/// <summary>
		///   (src.A, src.A, src.A, src.A)
		/// </summary>
		SrcAlpha,
		/// <summary>
		///   (1, 1, 1, 1) - (src.A, src.A, src.A, src.A)
		/// </summary>
		OneMinusSrcAlpha,
		/// <summary>
		///   (dst.A, dst.A, dst.A, dst.A)
		/// </summary>
		DstAlpha,
		/// <summary>
		///   (1, 1, 1, 1) - (dst.A, dst.A, dst.A, dst.A)
		/// </summary>
		OneMinusDstAlpha
	}

	/// <summary>
	///   Enumeration of the blending equations.
	/// </summary>
	public enum BlendModeEquation
	{
		/// <summary>
		///   Pixel = Src * SrcFactor + Dst * DstFactor
		/// </summary>
		Add,
		/// <summary>
		///   Pixel = Src * SrcFactor - Dst * DstFactor
		/// </summary>
		Subtract,
		/// <summary>
		///   Pixel = Dst * DstFactor - Src * SrcFactor
		/// </summary>
		ReverseSubtract,
		Min,
		Max
	}

	/// <summary>
	///   Blending modes for drawing.
	/// </summary>
	/// <param name="ColorSrcFactor">Source blending factor for the color channels.</param>
	/// <param name="ColorDstFactor">Destination blending factor for the color channels.</param>
	/// <param name="ColorEquation">Blending equation for the color channels.</param>
	/// <param name="AlphaSrcFactor">Source blending factor for the alpha channel.</param>
	/// <param name="AlphaDstFactor">Destination blending factor for the alpha channel.</param>
	/// <param name="AlphaEquation">Blending equation for the alpha channel.</param>
	public readonly record struct BlendMode(
		BlendModeFactor ColorSrcFactor,
		BlendModeFactor ColorDstFactor,
		BlendModeEquation ColorEquation,
		BlendModeFactor AlphaSrcFactor,
		BlendModeFactor AlphaDstFactor,
		BlendModeEquation AlphaEquation)
	{
		/// <summary>
		///   Blend source and dest according to dest alpha.
		/// </summary>
		public static BlendMode Alpha =>
			new(
				BlendModeFactor.SrcAlpha,
				BlendModeFactor.OneMinusSrcAlpha,
				BlendModeEquation.Add,
				BlendModeFactor.One,
				BlendModeFactor.OneMinusSrcAlpha,
				BlendModeEquation.Add);

		/// <summary>
		///   Add source to dest.
		/// </summary>
		public static BlendMode Add =>
			new(
				 BlendModeFactor.SrcAlpha,
				BlendModeFactor.One,
				BlendModeEquation.Add,
				BlendModeFactor.One,
				BlendModeFactor.One,
				BlendModeEquation.Add);

		/// <summary>
		///   Multiply source and dest.
		/// </summary>
		public static BlendMode Multiply =>
			new(
				BlendModeFactor.DstColor,
				BlendModeFactor.Zero,
				BlendModeEquation.Add,
				BlendModeFactor.DstColor,
				BlendModeFactor.Zero,
				BlendModeEquation.Add);

		/// <summary>
		///   Overwrite dest with source.
		/// </summary>
		public static BlendMode None =>
			new(
				BlendModeFactor.One,
				BlendModeFactor.Zero,
				BlendModeEquation.Add,
				BlendModeFactor.One,
				BlendModeFactor.Zero,
				BlendModeEquation.Add);

		/// <summary>
		///   Blending modes for drawing.
		/// </summary>
		/// <param name="sourceFactor">Source blending factor for the color and alpha channels.</param>
		/// <param name="destinationFactor">Destination blending factor for the color and alpha channels.</param>
		/// <param name="blendEquation">Blending equation for the color and alpha channels.</param>
		public BlendMode(BlendModeFactor sourceFactor, BlendModeFactor destinationFactor, BlendModeEquation blendEquation = BlendModeEquation.Add)
			: this(
				sourceFactor,
				destinationFactor,
				blendEquation,
				sourceFactor,
				destinationFactor,
				blendEquation)
		{ }
	};
}
