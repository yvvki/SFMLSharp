using System.Numerics;
using System.Runtime.Versioning;

namespace SFML.System
{
	public static partial class SIMDVector
	{
		public static bool IsVector2Supported<T>() where T : unmanaged
		{
			return Vector<T>.Count >= 2;
		}

		public static bool IsVector3Supported<T>() where T : unmanaged
		{
			return Vector<T>.Count >= 3;
		}

		[RequiresPreviewFeatures]
		public static Vector<T> ToVector<T>(this Vector2<T> @this)
			where T : unmanaged, INumber<T>
		{
			return new(stackalloc T[] { @this.X, @this.Y });
		}

		public static Vector<int> ToVector(this Vector2I @this)
		{
			return new(stackalloc int[] { @this.X, @this.Y });
		}

		public static Vector<uint> ToVector(this Vector2U @this)
		{
			return new(stackalloc uint[] { @this.X, @this.Y });
		}

		//public static Vector<float> ToVector(this Vector2F @this)
		//{
		//	return new(stackalloc float[] { @this.X, @this.Y });
		//}

		[RequiresPreviewFeatures]
		public static Vector<T> ToVector<T>(this Vector3<T> @this)
			where T : unmanaged, INumber<T>
		{
			return new(stackalloc T[] { @this.X, @this.Y, @this.Z });
		}

		//public static Vector<float> ToVector(this Vector3F @this)
		//{
		//	return new(stackalloc float[] { @this.X, @this.Y, @this.Z });
		//}

		[RequiresPreviewFeatures]
		public static Vector2<T> ToVector2<T>(this in Vector<T> @this)
			where T : unmanaged, INumber<T>
		{
			return new(@this[0], @this[1]);
		}

		public static Vector2I ToVector2(this in Vector<int> @this)
		{
			return new(@this[0], @this[1]);
		}

		public static Vector2U ToVector2(this in Vector<uint> @this)
		{
			return new(@this[0], @this[1]);
		}

		//public static Vector2F ToVector2(this in Vector<float> @this)
		//{
		//	return new(@this[0], @this[1]);
		//}

		[RequiresPreviewFeatures]
		public static Vector3<T> ToVector3<T>(this in Vector<T> @this)
			where T : unmanaged, INumber<T>
		{
			return new(@this[0], @this[1], @this[2]);
		}

		//public static Vector3F ToVector3(this in Vector<float> @this)
		//{
		//	return new(@this[0], @this[1], @this[2]);
		//}
	}
}
