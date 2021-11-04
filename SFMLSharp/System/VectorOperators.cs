using System.Runtime.Versioning;

namespace SFML.System
{
	/// <summary>
	///   Defines generalized abstract static operators for manipulating multi-component vectors.
	/// </summary>
	/// <typeparam name="TSelf">The type that implement the interface.</typeparam>
	/// <typeparam name="TNumber">The component type.</typeparam>
	[RequiresPreviewFeatures]
	public interface IVectorOperators<TSelf, TNumber> :
		IEqualityOperators<TSelf, TSelf>,
		IAdditiveIdentity<TSelf, TSelf>,
		IMultiplicativeIdentity<TSelf, TSelf>,
		IUnaryNegationOperators<TSelf, TSelf>,
		IAdditionOperators<TSelf, TSelf, TSelf>,
		ISubtractionOperators<TSelf, TSelf, TSelf>,
		IMultiplyOperators<TSelf, TSelf, TSelf>,
		IDivisionOperators<TSelf, TSelf, TSelf>
		where TSelf : IVectorOperators<TSelf, TNumber>
		where TNumber : INumber<TNumber>
	{
		static abstract TSelf Zero { get; }
		static abstract TSelf One { get; }

		//abstract static TSelf Negate(TSelf value);
		//abstract static TSelf Add(TSelf left, TSelf right);
		//abstract static TSelf Subtract(TSelf left, TSelf right);
		//abstract static TSelf Multiply(TSelf left, TSelf right);
		//abstract static TSelf Divide(TSelf left, TSelf right);

		static abstract TSelf operator *(TSelf left, TNumber right);
		static abstract TSelf operator *(TNumber left, TSelf right);
		static abstract TSelf operator /(TSelf left, TNumber right);
	}
}
