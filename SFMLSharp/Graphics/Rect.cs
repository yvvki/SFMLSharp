using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;

using SFML.System;

namespace SFML.Graphics
{
	[RequiresPreviewFeatures]
	[Serializable]
	public struct Rect<T> :
		IRectOperators<Rect<T>, T>,
		IEquatable<Rect<T>>,
		IFormattable,
		IEnumerable<Vector2<T>>
		where T : INumber<T>
	{
		#region Fields & Properties

		internal const int Count = 4;

		public T Left;
		public T Top;
		public T Width;
		public T Height;

		public Vector2<T> Position
		{
			get => GetVector2(this, 0);
			set => this = WithVector2(this, 0, value);
		}

		public Vector2<T> Size
		{
			get => GetVector2(this, 1);
			set => this = WithVector2(this, 1, value);
		}

		public T this[int index]
		{
			get => GetElement(this, index);
			set => this = WithElement(this, index, value);
		}

		#endregion

		#region Constructors

		public Rect(Vector2<T> position, Vector2<T> size)
			: this(
				position.X,
				position.Y,
				size.X,
				size.Y)
		{ }

		public Rect(T left, T top, T width, T height)
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}

		#endregion

		#region Static Methods



		#endregion

		#region Methods

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect<T> Normalize(in Rect<T> rect)
		{
			Rect<T> result = rect;

			if (result.Width < T.Zero)
			{
				result.Left -= result.Width;
				result.Width = -result.Width;
			}

			if (result.Height < T.Zero)
			{
				result.Top -= result.Height;
				result.Height = -result.Height;
			}

			return result;
		}

		public bool Contains(T x, T y)
		{
			Rect<T> r = Normalize(this);

			return (r.Left <= x)
				&& (r.Top <= y)
				&& (x < r.Left + r.Width)
				&& (y < r.Top + r.Height);
		}

		public bool Contains(Vector2<T> point)
		{
			return Contains(point.X, point.Y);
		}

		public bool Intersects(Rect<T> rect, out Rect<T> intersection)
		{
			Rect<T> r1 = Normalize(this);
			Rect<T> r2 = Normalize(rect);

			T interLeft = T.Max(r1.Left, r2.Left);
			T interTop = T.Max(r1.Top, r2.Top);
			T interRight = T.Max(r1.Left + r1.Width, r2.Left + r2.Width);
			T interBottom = T.Max(r1.Top + r1.Height, r2.Top + r2.Height);

			if ((interLeft < interRight) && (interTop < interBottom))
			{
				intersection = new(interLeft, interTop, interRight - interLeft, interBottom - interTop);
				return true;
			}
			else
			{
				intersection = default;
				return false;
			}
		}

		#endregion

		#region Interface Methods

		public void CopyTo(Vector2<T>[] array, int index)
		{
			GetSpanUnsafe(ref this).ToArray().CopyTo(array, index);
		}

		public void CopyTo(Span<Vector2<T>> destination)
		{
			GetSpanUnsafe(ref this).CopyTo(destination);
		}

		public bool TryCopyTo(Span<Vector2<T>> destination)
		{
			return GetSpanUnsafe(ref this).TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static unsafe Span<Vector2<T>> GetSpanUnsafe(ref Rect<T> rect)
		{
			return new(Unsafe.AsPointer(ref rect), Count / Vector2<T>.Count);
		}

		public static Vector2<T> GetVector2(Rect<T> rect, int index)
		{
			if ((uint)index is >= Count / Vector2<T>.Count)
			{
				throw new IndexOutOfRangeException();
			}

			return GetVector2Unsafe(ref rect, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector2<T> GetVector2Unsafe(ref Rect<T> rect, int index)
		{
			Debug.Assert(index is >= 0 and < Count / Vector2<T>.Count);
			return Unsafe.Add(ref Unsafe.As<Rect<T>, Vector2<T>>(ref rect), index / Vector2<T>.Count);
		}

		internal static Rect<T> WithVector2(Rect<T> rect, int index, Vector2<T> value)
		{
			if ((uint)index is >= Count / Vector2<T>.Count)
			{
				throw new IndexOutOfRangeException();
			}

			Rect<T> result = rect;

			SetVector2Unsafe(ref result, index, value);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void SetVector2Unsafe(ref Rect<T> rect, int index, Vector2<T> value)
		{
			Debug.Assert(index is >= 0 and < Count);
			Unsafe.Add(ref Unsafe.As<Rect<T>, Vector2<T>>(ref rect), index / Vector2<T>.Count) = value;
		}

		public static T GetElement(Rect<T> rect, int index)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			return GetElementUnsafe(ref rect, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetElementUnsafe(ref Rect<T> rect, int index)
		{
			Debug.Assert(index is >= 0 and < Count);
			return Unsafe.Add(ref Unsafe.As<Rect<T>, T>(ref rect), index);
		}

		internal static Rect<T> WithElement(Rect<T> rect, int index, T value)
		{
			if ((uint)index is >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			Rect<T> result = rect;

			SetElementUnsafe(ref result, index, value);

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void SetElementUnsafe(ref Rect<T> vector, int index, T value)
		{
			Debug.Assert(index is >= 0 and < Count);
			Unsafe.Add(ref Unsafe.As<Rect<T>, T>(ref vector), index) = value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out Vector2<T> position, out Vector2<T> size)
		{
			position = Position;
			size = Size;
		}

		public void Deconstruct(
			out T left, out T top,
			out T width, out T height)
		{
			left = Left;
			top = Top;
			width = Width;
			height = Height;
		}

		public bool Equals(Rect<T> other)
		{
			return Left.Equals(other.Left)
				&& Top.Equals(other.Top)
				&& Width.Equals(other.Width)
				&& Height.Equals(other.Height);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Rect<T> other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Left, Top, Width, Height);
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			StringBuilder sb = new();
			string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

			sb.Append('{');
			sb.Append(' ');
			sb.Append('<');
			sb.Append(Left.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(Top.ToString(format, formatProvider));
			sb.Append('>');
			sb.Append(separator);
			sb.Append(' ');
			sb.Append('<');
			sb.Append(Width.ToString(format, formatProvider));
			sb.Append(separator);
			sb.Append(' ');
			sb.Append(Height.ToString(format, formatProvider));
			sb.Append('>');
			sb.Append(' ');
			sb.Append('}');

			return sb.ToString();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Enumerator GetEnumerator()
		{
			return new(this);
		}

		IEnumerator<Vector2<T>> IEnumerable<Vector2<T>>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator<Vector2<T>>
		{
			private readonly Rect<T> _rect;

			private bool _size;

			public Enumerator(Rect<T> value)
			{
				_rect = value;
			}

			public Vector2<T> Current => _size ? _rect.Position : _rect.Size;
			object? IEnumerator.Current => Current;

			public bool MoveNext()
			{
				return _size == false && (_size = true);
			}

			public void Reset()
			{
				_size = false;
			}

			public void Dispose()
			{
				GC.SuppressFinalize(this);
			}
		}

		#endregion

		#region Operators

		public static bool operator ==(Rect<T> left, Rect<T> right)
		{
			return left.Left == right.Left
				&& left.Top == right.Top
				&& left.Width == right.Width
				&& left.Height == right.Height;
		}

		public static bool operator !=(Rect<T> left, Rect<T> right)
		{
			return left.Left != right.Left
				|| left.Top != right.Top
				|| left.Width != right.Width
				|| left.Height != right.Height;
		}

		#endregion

		#region Cast Operators

		//public static implicit operator (T, T, T, T)(Rect<T> value)
		//{
		//	return (value.Left, value.Top, value.Width, value.Height);
		//}
		//public static implicit operator Rect<T>((T left, T top, T width, T height) value)
		//{
		//	return new(value.left, value.top, value.width, value.height);
		//}

		//public static implicit operator (Vector2<T>, Vector2<T>)(Rect<T> value)
		//{
		//	return (value.Position, value.Size);
		//}
		//public static implicit operator Rect<T>((Vector2<T> position, Vector2<T> size) value)
		//{
		//	return new(value.position, value.size);
		//}

		#endregion
	}
}
