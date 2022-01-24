using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
//using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	// A transposed 3x3 matrix.
	public unsafe struct Transform :
		IList,
		IList<float>,
		IReadOnlyList<float>,
		IEquatable<Transform>,
		IFormattable
	{
		#region Fields & Properties

		internal const int Count = 3 * 3;

		public float M00;
		public float M01;
		public float M02;

		public float M10;
		public float M11;
		public float M12;

		public float M20;
		public float M21;
		public float M22;

		public float this[int row, int column]
		{
			get
			{
				if ((uint)row >= 3)
				{
					throw new ArgumentOutOfRangeException(nameof(row));
				}

				Vector3<float> vrow = Unsafe.Add(ref Unsafe.As<Transform, Vector3<float>>(ref this), row);
				return vrow[column];
			}
			set
			{
				if ((uint)row >= 3)
				{
					throw new ArgumentOutOfRangeException(nameof(row));
				}

				ref Vector3<float> vrow = ref Unsafe.Add(ref Unsafe.As<Transform, Vector3<float>>(ref this), row);
				Vector3<float> tmp = Vector3<float>.WithElement(vrow, column, value);
				vrow = tmp;
			}
		}

		internal float this[int index]
		{
			get => GetElement(this, index);
			set => this = WithElement(this, index, value);
		}

		float IList<float>.this[int index]
		{
			get => this[index];
			set => this[index] = value;
		}
		float IReadOnlyList<float>.this[int index] => this[index];

		object? IList.this[int index]
		{
			get => this[index];
			set => this[index] = (float)value!; // automatically throws
		}

		int ICollection.Count => Count;
		int ICollection<float>.Count => Count;
		int IReadOnlyCollection<float>.Count => Count;

		bool ICollection.IsSynchronized => false;
		object ICollection.SyncRoot => this;

		bool ICollection<float>.IsReadOnly => false;

		bool IList.IsFixedSize => true;
		bool IList.IsReadOnly => false;

		public bool IsIdentity => Equals(Identity);

		public static Transform Identity => new();
		#endregion

		#region Constructors

		// Identity
		public Transform() : this(
			1, 0, 0,
			0, 1, 0,
			0, 0, 1)
		{ }

		public Transform(
			float m00, float m01, float m02,
			float m10, float m11, float m12,
			float m20, float m21, float m22)
		{
			M00 = m00; M01 = m01; M02 = m02;
			M10 = m10; M11 = m11; M12 = m12;
			M20 = m20; M21 = m21; M22 = m22;
		}

		public Transform(ReadOnlySpan<float> values)
		{
			if (values.Length is < Count)
			{
				throw new ArgumentOutOfRangeException(nameof(values));
			}

			this = Unsafe.ReadUnaligned<Transform>(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(values)));
		}

		#endregion

		#region Methods

		public Transform GetInverse()
		{
			fixed (Transform* this_ptr = &this)
			{
				return sfTransform_getInverse(this_ptr);
			}
		}

		private float[] GetMatrix()
		{
			// Matrix4x4
			float[] matrix = new float[16];

			fixed (Transform* this_ptr = &this)
			{
				fixed (float* matrix_ptr = matrix)
				{
					sfTransform_getMatrix(this_ptr, matrix_ptr);
				}
			}

			return matrix;
		}

		public Vector2<float> TransformPoint(Vector2<float> point)
		{
			fixed (Transform* this_ptr = &this)
			{
				return sfTransform_transformPoint(this_ptr, point);
			}
		}

		public Rect<float> TransformRect(Rect<float> rectangle)
		{
			fixed (Transform* this_ptr = &this)
			{
				return sfTransform_transformRect(this_ptr, rectangle);
			}
		}

		public void Combine(Transform other)
		{
			fixed (Transform* this_ptr = &this)
			{
				sfTransform_combine(this_ptr, &other);
			}
		}

		public static Transform Combine(Transform transform, Transform other)
		{
			Transform result = transform;

			result.Combine(other);
			return result;
		}

		public void Translate(float x, float y)
		{
			fixed (Transform* this_ptr = &this)
			{
				sfTransform_translate(this_ptr, x, y);
			}
		}

		public void Translate(Vector2<float> translates)
		{
			Translate(translates.X, translates.Y);
		}

		public static Transform Translate(Transform transform, float x, float y)
		{
			Transform result = transform;

			result.Translate(x, y);
			return result;
		}

		public static Transform Translate(Transform transform, Vector2<float> translates)
		{
			return Translate(transform, translates.X, translates.Y);
		}

		public static Transform CreateTranslate(float x, float y)
		{
			return Translate(Identity, x, y);
		}

		public static Transform CreateTranslate(Vector2<float> translates)
		{
			return CreateTranslate(translates.X, translates.Y);
		}

		public void Rotate(float angle)
		{
			fixed (Transform* this_ptr = &this)
			{
				sfTransform_rotate(this_ptr, angle);
			}
		}

		public static Transform Rotate(Transform transform, float angle)
		{
			Transform result = transform;

			result.Rotate(angle);
			return result;
		}

		public static Transform CreateRotate(float angle)
		{
			return Rotate(Identity, angle);
		}

		public void Rotate(float angle, float centerX, float centerY)
		{
			fixed (Transform* this_ptr = &this)
			{
				sfTransform_rotateWithCenter(this_ptr, angle, centerX, centerY);
			}
		}

		public void Rotate(float angle, Vector2<float> center)
		{
			Rotate(angle, center.X, center.Y);
		}

		public static Transform Rotate(Transform transform, float angle, float centerX, float centerY)
		{
			Transform result = transform;

			result.Rotate(angle, centerX, centerY);
			return result;
		}

		public static Transform Rotate(Transform transform, float angle, Vector2<float> center)
		{
			return Rotate(transform, angle, center.X, center.Y);
		}

		public static Transform CreateRotate(float angle, float centerX, float centerY)
		{
			return Rotate(Identity, angle, centerX, centerY);
		}

		public static Transform CreateRotate(float angle, Vector2<float> center)
		{
			return CreateRotate(angle, center.X, center.Y);
		}

		public void Scale(float x, float y)
		{
			fixed (Transform* this_ptr = &this)
			{
				sfTransform_scale(this_ptr, x, y);
			}
		}

		public void Scale(Vector2<float> scales)
		{
			Scale(scales.X, scales.Y);
		}

		public static Transform CreateScale(float x, float y)
		{
			return Scale(Identity, x, y);
		}

		public static Transform CreateScale(Vector2<float> scales)
		{
			return CreateScale(scales.X, scales.Y);
		}

		public static Transform Scale(Transform transform, float x, float y)
		{
			Transform result = transform;

			result.Scale(x, y);
			return result;
		}

		public static Transform Scale(Transform transform, Vector2<float> scaling)
		{
			return Scale(transform, scaling.X, scaling.Y);
		}

		public void Scale(float x, float y, float centerX, float centerY)
		{
			fixed (Transform* this_ptr = &this)
			{
				sfTransform_scaleWithCenter(this_ptr, x, y, centerX, centerY);
			}
		}

		public void Scale(Vector2<float> scales, float centerX, float centerY)
		{
			Scale(scales.X, scales.Y, centerX, centerY);
		}

		public void Scale(float x, float y, Vector2<float> center)
		{
			Scale(x, y, center.X, center.Y);
		}

		public void Scale(Vector2<float> scales, Vector2<float> center)
		{
			Scale(scales.X, scales.Y, center.X, center.Y);
		}

		public static Transform Scale(Transform transform, float x, float y, float centerX, float centerY)
		{
			Transform result = transform;

			result.Scale(x, y, centerX, centerY);
			return result;
		}

		public static Transform Scale(Transform transform, Vector2<float> scale, float centerX, float centerY)
		{
			return Scale(transform, scale.X, scale.Y, centerX, centerY);
		}

		public static Transform Scale(Transform transform, float x, float y, Vector2<float> center)
		{
			return Scale(transform, x, y, center.X, center.Y);
		}

		public static Transform Scale(Transform transform, Vector2<float> scale, Vector2<float> center)
		{
			return Scale(transform, scale.X, scale.Y, center.X, center.Y);
		}

		public static Transform CreateScale(float x, float y, float centerX, float centerY)
		{
			return Scale(Identity, x, y, centerX, centerY);
		}

		public static Transform CreateScale(Vector2<float> scales, float centerX, float centerY)
		{
			return CreateScale(scales.X, scales.Y, centerX, centerY);
		}

		public static Transform CreateScale(float x, float y, Vector2<float> center)
		{
			return CreateScale(x, y, center.X, center.Y);
		}

		public static Transform CreateScale(Vector2<float> scales, Vector2<float> center)
		{
			return CreateScale(scales.X, scales.Y, center.X, center.Y);
		}

		#endregion

		#region Interface Methods

		public void CopyTo(float[] array, int index)
		{
			GetSpanUnsafe(ref this).CopyTo(array.AsSpan()[index..]);
		}

		public void CopyTo(Span<float> destination)
		{
			GetSpanUnsafe(ref this).CopyTo(destination);
		}

		public bool TryCopyTo(Span<float> destination)
		{
			return GetSpanUnsafe(ref this).TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe Span<float> GetSpanUnsafe(ref Transform transform)
		{
			//return new(Unsafe.AsPointer(ref transform), Count);

			return MemoryMarshal.CreateSpan(ref Unsafe.As<Transform, float>(ref transform), Count);
		}

		internal static float GetElement(Transform transform, int index)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			return GetElementUnsafe(ref transform, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float GetElementUnsafe(ref Transform transform, int index)
		{
			Debug.Assert(index is >= 0 and < Count);
			return Unsafe.Add(ref Unsafe.As<Transform, float>(ref transform), index);
		}

		internal static Transform WithElement(Transform transform, int index, float value)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			Transform result = transform;

			SetElementUnsafe(ref result, index, value);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void SetElementUnsafe(ref Transform transform, int index, float value)
		{
			Debug.Assert(index is >= 0 and < Count);
			Unsafe.Add(ref Unsafe.As<Transform, float>(ref transform), index) = value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(
			out float m00, out float m01, out float m02,
			out float m10, out float m11, out float m12,
			out float m20, out float m21, out float m22)
		{
			m00 = M00; m01 = M01; m02 = M02;
			m10 = M10; m11 = M11; m12 = M12;
			m20 = M20; m21 = M21; m22 = M22;
		}

		public Vector3<float> GetRow(int column)
		{
			if (column is < 0 or > 3)
			{
				throw new ArgumentOutOfRangeException(nameof(column));
			}

			Span<float> matrix = GetSpanUnsafe(ref this);

			return new(matrix[column + (0 * 3)], matrix[column + (1 * 3)], matrix[column + (2 * 3)]);

		}

		public Vector3<float> GetColumn(int row)
		{
			if (row is < 0 or > 3)
			{
				throw new ArgumentOutOfRangeException(nameof(row));
			}

			Span<float> matrix = GetSpanUnsafe(ref this);

			return new(matrix[(row * 3) + 0], matrix[(row * 3) + 1], matrix[(row * 3) + 2]);

			//Vector3<float> vrow = Unsafe.Add(ref Unsafe.As<Transform, Vector3<float>>(ref this), row);
			//return vrow;
		}

		public IEnumerator<float> GetEnumerator()
		{
			yield return M00;
			yield return M01;
			yield return M02;
			yield return M10;
			yield return M11;
			yield return M12;
			yield return M20;
			yield return M21;
			yield return M22;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Equals(Transform other)
		{
			// Check diagonal elements first.
			return M00.Equals(other.M00)
				&& M11.Equals(other.M11)
				&& M22.Equals(other.M22)

				&& M01.Equals(other.M01)
				&& M02.Equals(other.M02)

				&& M10.Equals(other.M10)
				&& M12.Equals(other.M12)

				&& M20.Equals(other.M20)
				&& M21.Equals(other.M21);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Transform other && Equals(other);
		}

		public override int GetHashCode()
		{
			HashCode hashCode = new();
			hashCode.Add(M00); hashCode.Add(M01); hashCode.Add(M02);
			hashCode.Add(M10); hashCode.Add(M11); hashCode.Add(M12);
			hashCode.Add(M20); hashCode.Add(M21); hashCode.Add(M22);
			return hashCode.ToHashCode();
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			StringBuilder sb = new();
			string tab = "\t";
			string newLine = Environment.NewLine;

			sb.Append(M00.ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(M01.ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(M02.ToString(format, formatProvider));
			sb.Append(newLine);
			sb.Append(M10.ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(M11.ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(M12.ToString(format, formatProvider));
			sb.Append(newLine);
			sb.Append(M20.ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(M21.ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(M22.ToString(format, formatProvider));
			sb.Append(newLine);

			return sb.ToString();
		}


		[DoesNotReturn]
		private static void ThrowFixed()
		{
			throw new NotSupportedException("Transform has fixed size.");
		}

		int IList.Add(object? value)
		{
			ThrowFixed();
			return -1;
		}

		void ICollection<float>.Add(float item)
		{
			ThrowFixed();
		}

		void IList.Clear()
		{
			ThrowFixed();
		}

		void ICollection<float>.Clear()
		{
			ThrowFixed();
		}

		private bool Contains(float item)
		{
			return GetSpanUnsafe(ref this).Contains(item);
		}

		bool IList.Contains(object? value)
		{
			return value is float item && Contains(item);
		}

		bool ICollection<float>.Contains(float item)
		{
			return Contains(item);
		}

		private int IndexOf(float item)
		{
			return GetSpanUnsafe(ref this).IndexOf(item);
		}

		int IList.IndexOf(object? value)
		{
			return value is float item ? IndexOf(item) : -1;
		}

		int IList<float>.IndexOf(float item)
		{
			return IndexOf(item);
		}

		void IList.Insert(int index, object? value)
		{
			ThrowFixed();
		}

		void IList<float>.Insert(int index, float item)
		{
			ThrowFixed();
		}

		void IList.Remove(object? value)
		{
			ThrowFixed();
		}

		bool ICollection<float>.Remove(float item)
		{
			ThrowFixed();
			return false;
		}

		void IList.RemoveAt(int index)
		{
			ThrowFixed();
		}

		void IList<float>.RemoveAt(int index)
		{
			ThrowFixed();
		}

		void ICollection.CopyTo(Array array, int index)
		{
			CopyTo((float[])array, index);
		}

		void ICollection<float>.CopyTo(float[] array, int arrayIndex)
		{
			CopyTo(array, arrayIndex);
		}

		#endregion

		#region Imports

		//[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		//private static extern Transform sfTransform_fromMatrix(
		//	float a00, float a01, float a02,
		//	float a10, float a11, float a12,
		//	float a20, float a21, float a22);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTransform_getMatrix(Transform* transform, float* matrix);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Transform sfTransform_getInverse(Transform* transform);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector2<float> sfTransform_transformPoint(Transform* transform, Vector2<float> point);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern Rect<float> sfTransform_transformRect(Transform* transform, Rect<float> rectangle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTransform_combine(Transform* transform, Transform* other);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTransform_translate(Transform* transform, float x, float y);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTransform_rotate(Transform* transform, float angle);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTransform_rotateWithCenter(Transform* transform, float angle, float centerX, float centerY);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTransform_scale(Transform* transform, float scaleX, float scaleY);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfTransform_scaleWithCenter(Transform* transform, float scaleX, float scaleY, float centerX, float centerY);

		// Replaced with managed.
		// [DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		// private unsafe static extern bool sfTransform_equal(Transform* left, Transform* right);

		#endregion

		#region Operators

		public static bool operator ==(Transform left, Transform right)
		{
			return left.M00 == right.M00
				&& left.M11 == right.M11
				&& left.M22 == right.M22

				&& left.M01 == right.M01
				&& left.M02 == right.M02

				&& left.M10 == right.M10
				&& left.M12 == right.M12

				&& left.M20 == right.M20
				&& left.M21 == right.M21;
		}

		public static bool operator !=(Transform left, Transform right)
		{
			return left.M00 != right.M00
				|| left.M11 != right.M11
				|| left.M22 != right.M22

				|| left.M01 != right.M01
				|| left.M02 != right.M02

				|| left.M10 != right.M10
				|| left.M12 != right.M12

				|| left.M20 != right.M20
				|| left.M21 != right.M21;
		}

		// Natively supported
		public static Transform operator *(Transform left, Transform right)
		{
			return Combine(left, right);
		}

		#endregion

		#region Cast Operators

		//public static implicit operator (
		//	float, float, float,
		//	float, float, float,
		//	float, float, float)
		//	(Transform value)
		//{
		//	return (
		//		value._matrix[0], value._matrix[1], value._matrix[2],
		//		value._matrix[3], value._matrix[4], value._matrix[5],
		//		value._matrix[6], value._matrix[7], value._matrix[8]);
		//}

		//public static implicit operator Transform((
		//	float m00, float m01, float m02,
		//	float m10, float m11, float m12,
		//	float m20, float m21, float m22) value)
		//{
		//	return new(
		//		value.m00, value.m01, value.m02,
		//		value.m10, value.m11, value.m12,
		//		value.m20, value.m21, value.m22);
		//}

		public static explicit operator Matrix4x4(Transform value)
		{
			float[] m = value.GetMatrix();
			return new(
				m[0], m[1], m[2], m[3],
				m[4], m[5], m[6], m[7],
				m[8], m[9], m[10], m[11],
				m[12], m[13], m[14], m[15]);

			//return Unsafe.As<float, Matrix4x4>(ref MemoryMarshal.GetArrayDataReference(m));
		}

		//// Not supported.
		//public static explicit operator Transform(Matrix4x4 value)
		//{
		//	return new(
		//		value.M11, value.M21, value.M41,
		//		value.M12, value.M22, value.M42,
		//		value.M14, value.M24, value.M44);
		//}

		/*
		 * Transforms are transposed. We need to transpose it again
		 * for the 3x2 matrix.
		 * 
		 * The last column of the transform rarely change
		 * due to the nature of matrix transformation in 2D space.
		 * 
		 *      Transform                 Matrix3x2
		 * +-----+-----+-----+       +-----+-----+     +
		 * | M00 | M10 | M20 |       | M00 | M01 | {0} |
		 * +-----+-----+-----+       +-----+-----+     +
		 * | M01 | M11 | M21 |  ==>  | M10 | M11 | {0} |
		 * +-----+-----+-----+       +-----+-----+     +
		 * | {0} | {0} | {1} |       | M20 | M21 | {1} |
		 * +-----+-----+-----+       +-----+-----+     +
		 * 
		 * And the other way around.
		 * 
		 *      Matrix3x2                 Transform
		 * +-----+-----+     +       +-----+-----+-----+
		 * | M11 | M12 | {0} |       | M11 | M21 | M31 |
		 * +-----+-----+     +       +-----+-----+-----+
		 * | M21 | M22 | {0} |  ==>  | M12 | M22 | M32 |
		 * +-----+-----+     +       +-----+-----+-----+
		 * | M31 | M32 | {1} |       | {0} | {0} | {1} |
		 * +-----+-----+     +       +-----+-----+-----+
		 * 
		 * Good luck!
		 * 
		 */
		public static explicit operator Matrix3x2(Transform value)
		{
			return new(
				value.M00, value.M10, value.M20,
				value.M01, value.M11, value.M22);
		}

		public static explicit operator Transform(Matrix3x2 value)
		{
			return new(
				value.M11, value.M12, 0,
				value.M21, value.M22, 0,
				value.M31, value.M32, 1);
		}

		#endregion
	}
}
