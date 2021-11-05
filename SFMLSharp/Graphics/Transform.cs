using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

using SFML.System;

using static SFML.Graphics.DllName;

namespace SFML.Graphics
{
	public unsafe struct Transform :
		IList,
		IList<float>,
		IReadOnlyList<float>,
		IEquatable<Transform>,
		IFormattable
	{
		#region Fields & Properties

		internal const int FixedLength = 9;

		public static readonly int Length = FixedLength;

		private fixed float _matrix[FixedLength];

		public float M00 { get => _matrix[0]; set => _matrix[0] = value; }
		public float M01 { get => _matrix[1]; set => _matrix[1] = value; }
		public float M02 { get => _matrix[2]; set => _matrix[2] = value; }
		public float M10 { get => _matrix[3]; set => _matrix[3] = value; }
		public float M11 { get => _matrix[4]; set => _matrix[4] = value; }
		public float M12 { get => _matrix[5]; set => _matrix[5] = value; }
		public float M20 { get => _matrix[6]; set => _matrix[6] = value; }
		public float M21 { get => _matrix[7]; set => _matrix[7] = value; }
		public float M22 { get => _matrix[8]; set => _matrix[8] = value; }

		public float this[int index]
		{
			get => _matrix[index];
			set => _matrix[index] = value;
		}

		object? IList.this[int index]
		{
			get => this[index];
			set => this[index] =
				value is null ? throw new ArgumentNullException(nameof(value)) :
				value is float v ? v : throw new ArgumentException("Transform value must be of type float", nameof(value));
		}

		int ICollection.Count => Length;
		int ICollection<float>.Count => Length;
		int IReadOnlyCollection<float>.Count => Length;

		bool ICollection.IsSynchronized => false;
		object ICollection.SyncRoot => this;

		bool ICollection<float>.IsReadOnly => false;

		bool IList.IsFixedSize => true;
		bool IList.IsReadOnly => false;

		public bool IsIdentity => Equals(Identity);

		public static Transform Identity => new();

		#endregion

		#region Constructors

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
			_matrix[0] = m00; _matrix[1] = m01; _matrix[2] = m02;
			_matrix[3] = m10; _matrix[4] = m11; _matrix[5] = m12;
			_matrix[6] = m20; _matrix[7] = m21; _matrix[8] = m22;
		}

		public Transform(IEnumerable<float> matrix)
		{
			if (matrix is null) throw new ArgumentNullException(nameof(matrix));

			IEnumerator<float> enumerator = matrix.GetEnumerator();

			_matrix[0] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[1] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[2] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[3] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[4] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[5] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[6] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[7] = enumerator.Current;
			if (enumerator.MoveNext() is false) ThrowErr();
			_matrix[8] = enumerator.Current;
			if (enumerator.MoveNext() is true) ThrowErr();

			[DoesNotReturn]
			static void ThrowErr() => throw new ArgumentOutOfRangeException(nameof(matrix), "Enumerable does not contains 9 items.");
		}

		#endregion

		#region Methods

		public Span<float> AsSpan()
		{
			fixed (float* matrix_ptr = _matrix)
			{
				return new(matrix_ptr, FixedLength);
			}
		}

		private float* GetMatrixPtr()
		{
			float* matrix = stackalloc float[16];

			fixed (Transform* this_ptr = &this)
			{
				sfTransform_getMatrix(this_ptr, matrix);
			}

			return matrix;
		}

		public Span<float> GetMatrix()
		{
			return new(GetMatrixPtr(), 16);
		}

		public Transform GetInverse()
		{
			fixed (Transform* this_ptr = &this)
			{
				return sfTransform_getInverse(this_ptr);
			}
		}

		public Vector2F TransformPoint(Vector2F point)
		{
			fixed (Transform* this_ptr = &this)
			{
				return sfTransform_transformPoint(this_ptr, point);
			}
		}

		public FloatRect TransformRect(FloatRect rectangle)
		{
			fixed (Transform* this_ptr = &this)
			{
				return sfTransform_transformRect(this_ptr, rectangle);
			}
		}

		public static Transform Combine(Transform transform, Transform other)
		{
			sfTransform_combine(&transform, &other);
			return transform;
		}

		public static Transform Translate(Transform transform, float x, float y)
		{
			sfTransform_translate(&transform, x, y);
			return transform;
		}

		public static Transform Translate(Transform transform, IReadOnlyVector2<float> value)
		{
			return Translate(transform, value.X, value.Y);
		}

		public static Transform Rotate(Transform transform, float angle)
		{
			sfTransform_rotate(&transform, angle);
			return transform;
		}

		public static Transform Rotate(Transform transform, float angle, float centerX, float centerY)
		{
			sfTransform_rotateWithCenter(&transform, angle, centerX, centerY);
			return transform;
		}

		public static Transform Rotate(Transform transform, float angle, IReadOnlyVector2<float> center)
		{
			return Rotate(transform, angle, center.X, center.Y);
		}

		public static Transform Scale(Transform transform, float x, float y)
		{
			sfTransform_scale(&transform, x, y);
			return transform;
		}

		public static Transform Scale(Transform transform, IReadOnlyVector2<float> scale)
		{
			return Scale(transform, scale.X, scale.Y);
		}

		public static Transform Scale(Transform transform, float x, float y, float centerX, float centerY)
		{
			sfTransform_scaleWithCenter(&transform, x, y, centerX, centerY);
			return transform;
		}

		public static Transform Scale(Transform transform, IReadOnlyVector2<float> scale, float centerX, float centerY)
		{
			return Scale(transform, scale.X, scale.Y, centerX, centerY);
		}

		public static Transform Scale(Transform transform, float x, float y, IReadOnlyVector2<float> center)
		{
			return Scale(transform, x, y, center.X, center.Y);
		}

		public static Transform Scale(Transform transform, IReadOnlyVector2<float> scale, IReadOnlyVector2<float> center)
		{
			return Scale(transform, scale.X, scale.Y, center.X, center.Y);
		}

		#endregion

		#region Interface Method Implementations

		public void Deconstruct(
			out float m00, out float m01, out float m02,
			out float m10, out float m11, out float m12,
			out float m20, out float m21, out float m22)
		{
			m00 = _matrix[0]; m01 = _matrix[1]; m02 = _matrix[2];
			m10 = _matrix[3]; m11 = _matrix[4]; m12 = _matrix[5];
			m20 = _matrix[6]; m21 = _matrix[7]; m22 = _matrix[8];
		}

		public class Enumerator : IEnumerator<float>
		{
			private Transform _transform;
			private int _i;

			public float Current => _transform._matrix[_i];
			object IEnumerator.Current => Current;

			public Enumerator(in Transform transform)
			{
				_transform = transform;
			}

			public bool MoveNext()
			{
				if (_i < FixedLength)
				{
					_i++;
					return true;
				}
				else
				{
					return false;
				}
			}

			public void Reset()
			{
				_i = default;
			}

			public void Dispose() { GC.SuppressFinalize(this); }
		}

		public Enumerator GetEnumerator()
		{
			return new(this);
		}

		IEnumerator<float> IEnumerable<float>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Equals(Transform other)
		{
			//return _matrix[0].Equals(other._matrix[0])
			//	&& _matrix[1].Equals(other._matrix[1])
			//	&& _matrix[2].Equals(other._matrix[2])
			//	&& _matrix[3].Equals(other._matrix[3])
			//	&& _matrix[4].Equals(other._matrix[4])
			//	&& _matrix[5].Equals(other._matrix[5])
			//	&& _matrix[6].Equals(other._matrix[6])
			//	&& _matrix[7].Equals(other._matrix[7])
			//	&& _matrix[8].Equals(other._matrix[8]);

			// Check diagonal elements first.
			return _matrix[0].Equals(other._matrix[0])
				&& _matrix[4].Equals(other._matrix[3])
				&& _matrix[8].Equals(other._matrix[8])

				&& _matrix[1].Equals(other._matrix[1])
				&& _matrix[2].Equals(other._matrix[2])

				&& _matrix[3].Equals(other._matrix[3])
				&& _matrix[5].Equals(other._matrix[5])

				&& _matrix[6].Equals(other._matrix[6])
				&& _matrix[7].Equals(other._matrix[7]);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Transform other && Equals(other);
		}

		public override int GetHashCode()
		{
			HashCode hashCode = new();
			hashCode.Add(_matrix[0]); hashCode.Add(_matrix[1]); hashCode.Add(_matrix[2]);
			hashCode.Add(_matrix[3]); hashCode.Add(_matrix[4]); hashCode.Add(_matrix[5]);
			hashCode.Add(_matrix[6]); hashCode.Add(_matrix[7]); hashCode.Add(_matrix[8]);
			return hashCode.ToHashCode();
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			StringBuilder sb = new();
			string tab = "\t";
			string newLine = Environment.NewLine;

			sb.Append(_matrix[0].ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(_matrix[1].ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(_matrix[2].ToString(format, formatProvider));
			sb.Append(newLine);
			sb.Append(_matrix[3].ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(_matrix[4].ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(_matrix[5].ToString(format, formatProvider));
			sb.Append(newLine);
			sb.Append(_matrix[6].ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(_matrix[7].ToString(format, formatProvider));
			sb.Append(tab);
			sb.Append(_matrix[8].ToString(format, formatProvider));
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
			return AsSpan().Contains(item);
		}

		bool IList.Contains(object? value)
		{
			if (value is float item) return Contains(item);
			else return false;
		}

		bool ICollection<float>.Contains(float item)
		{
			return Contains(item);
		}

		private int IndexOf(float item)
		{
			return AsSpan().IndexOf(item);
		}

		int IList.IndexOf(object? value)
		{
			if (value is float item) return IndexOf(item);
			else return -1;
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

		private void CopyTo(float[] array, int arrayIndex)
		{
			AsSpan().CopyTo(array.AsSpan()[arrayIndex..]);
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
		private static extern Vector2F sfTransform_transformPoint(Transform* transform, Vector2F point);

		[DllImport(csfml_graphics, CallingConvention = CallingConvention.Cdecl)]
		private static extern FloatRect sfTransform_transformRect(Transform* transform, FloatRect rectangle);

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
			//return left.M00 == right.M00
			//	&& left.M01 == right.M01
			//	&& left.M02 == right.M02
			//	&& left.M10 == right.M10
			//	&& left.M11 == right.M11
			//	&& left.M12 == right.M12
			//	&& left.M20 == right.M20
			//	&& left.M21 == right.M21
			//	&& left.M22 == right.M22;

			return left._matrix[0] == right._matrix[0]
				&& left._matrix[4] == right._matrix[3]
				&& left._matrix[8] == right._matrix[8]

				&& left._matrix[1] == right._matrix[1]
				&& left._matrix[2] == right._matrix[2]

				&& left._matrix[3] == right._matrix[3]
				&& left._matrix[5] == right._matrix[5]

				&& left._matrix[6] == right._matrix[6]
				&& left._matrix[7] == right._matrix[7];
		}

		public static bool operator !=(Transform left, Transform right)
		{
			//return left.M00 != right.M00
			//	|| left.M01 != right.M01
			//	|| left.M02 != right.M02
			//	|| left.M10 != right.M10
			//	|| left.M11 != right.M11
			//	|| left.M12 != right.M12
			//	|| left.M20 != right.M20
			//	|| left.M21 != right.M21
			//	|| left.M22 != right.M22;

			return left._matrix[0] != right._matrix[0]
				|| left._matrix[4] != right._matrix[3]
				|| left._matrix[8] != right._matrix[8]

				|| left._matrix[1] != right._matrix[1]
				|| left._matrix[2] != right._matrix[2]

				|| left._matrix[3] != right._matrix[3]
				|| left._matrix[5] != right._matrix[5]

				|| left._matrix[6] != right._matrix[6]
				|| left._matrix[7] != right._matrix[7];
		}

		public static Transform operator *(Transform left, Transform right)
		{
			return Combine(left, right);
		}

		#endregion

		#region Cast Operators

		public static implicit operator (
			float, float, float,
			float, float, float,
			float, float, float)
			(Transform value)
		{
			return (
				value._matrix[0], value._matrix[1], value._matrix[2],
				value._matrix[3], value._matrix[4], value._matrix[5],
				value._matrix[6], value._matrix[7], value._matrix[8]);
		}

		public static implicit operator Transform((
			float m00, float m01, float m02,
			float m10, float m11, float m12,
			float m20, float m21, float m22) value)
		{
			return new(
				value.m00, value.m01, value.m02,
				value.m10, value.m11, value.m12,
				value.m20, value.m21, value.m22);
		}

		public static explicit operator Matrix4x4(Transform value)
		{
			float* m = value.GetMatrixPtr();
			return new(
				m[0], m[1], m[2], m[3],
				m[4], m[5], m[6], m[7],
				m[8], m[9], m[10], m[11],
				m[12], m[13], m[14], m[15]);
		}

		//public static explicit operator Transform(Matrix4x4 value)
		//{
		//	return new(
		//		value.M11, value.M21, value.M41,
		//		value.M12, value.M22, value.M42,
		//		value.M14, value.M24, value.M44);
		//}

		public static explicit operator Matrix3x2(Transform value)
		{
			return new(
				value._matrix[0], value._matrix[2], value._matrix[4],
				value._matrix[1], value._matrix[3], value._matrix[5]);
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
