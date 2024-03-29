﻿using System.Runtime.Versioning;

namespace SFML.Graphics
{
	[RequiresPreviewFeatures]
	internal interface IRectOperators<TSelf, TNumber> :
		IEqualityOperators<TSelf, TSelf>
		where TSelf : IRectOperators<TSelf, TNumber>
		where TNumber : INumber<TNumber>
	{ }
}
