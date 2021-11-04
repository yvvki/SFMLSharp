using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

using SFML.System;

namespace SFML.Graphics
{
	public interface IRect<TSelf, T> : IEquatable<IRect<TSelf, T>>, IFormattable
		where TSelf : IRect<TSelf, T>
		where T : IEquatable<T>, IFormattable
	{
		#region Properties

		T Left { get; set; }

		T Top { get; set; }

		T Width { get; set; }

		T Height { get; set; }

		IReadOnlyVector2<T> Position { get; set; }

		IReadOnlyVector2<T> Size { get; set; }

		#endregion

		#region Methods

		TSelf GetNormalized();

		bool Contains(T x, T y);

		bool Contains(IReadOnlyVector2<T> point);

		bool Intersects(TSelf rectangle, out TSelf intersection);

		#endregion

		#region Explicit Method Implementations

		bool IEquatable<IRect<TSelf, T>>.Equals([NotNullWhen(true)] IRect<TSelf, T>? other)
		{
			return other is not null
				&& Left.Equals(other.Left)
				&& Top.Equals(other.Top)
				&& Width.Equals(other.Width)
				&& Height.Equals(other.Height);
		}

		#endregion
	}

	public interface IReadOnlyRect<out T>
	{
		#region Properties

		T Left { get; }

		T Top { get; }

		T Width { get; }

		T Height { get; }

		IReadOnlyVector2<T> Position { get; }

		IReadOnlyVector2<T> Size { get; }

		#endregion
	}

	[RequiresPreviewFeatures]
	public struct Rect<T> :
		IRect<Rect<T>, T>,
		IReadOnlyRect<T>,
		IEquatable<Rect<T>>,
		IFormattable,
		IRectOperators<Rect<T>, T>
		where T : INumber<T>
	{
		#region Fields & Properties

		public T Left;
		public T Top;
		public T Width;
		public T Height;

		T IRect<Rect<T>, T>.Left { get => Left; set => Left = value; }
		T IRect<Rect<T>, T>.Top { get => Top; set => Top = value; }
		T IRect<Rect<T>, T>.Width { get => Width; set => Width = value; }
		T IRect<Rect<T>, T>.Height { get => Height; set => Height = value; }

		T IReadOnlyRect<T>.Left => Left;
		T IReadOnlyRect<T>.Top => Top;
		T IReadOnlyRect<T>.Width => Width;
		T IReadOnlyRect<T>.Height => Height;

		public Vector2<T> Position
		{
			get => new(Left, Top);
			set
			{
				Left = value.X;
				Top = value.Y;
			}
		}
		public Vector2<T> Size
		{
			get => new(Width, Height);
			set
			{
				Width = value.X;
				Height = value.Y;
			}
		}

		IReadOnlyVector2<T> IRect<Rect<T>, T>.Position
		{
			get => Position;
			set => Position = new(value);
		}
		IReadOnlyVector2<T> IRect<Rect<T>, T>.Size
		{
			get => Size;
			set => Size = new(value);
		}

		IReadOnlyVector2<T> IReadOnlyRect<T>.Position => Position;
		IReadOnlyVector2<T> IReadOnlyRect<T>.Size => Size;

		#endregion

		#region Constructors

		public Rect(IReadOnlyRect<T> value) : this(value.Left, value.Top, value.Width, value.Height) { }

		public Rect(IReadOnlyVector2<T> position, IReadOnlyVector2<T> size)
			: this(position.X, position.Y, size.X, size.Y) { }

		public Rect(T left, T top, T width, T height)
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}

		#endregion

		#region Interface Methods Implementations

		public Rect<T> GetNormalized()
		{
			if (Size == default) return this;

			T left;
			T top;
			T width;
			T height;

			if (Width < T.Zero)
			{
				left = Left - Width;
				width = -Width;
			}
			else
			{
				left = Left;
				width = Width;
			}

			if (Height < T.Zero)
			{
				top = Top - Height;
				height = -Height;
			}
			else
			{
				top = Top;
				height = Height;
			}

			return new(left, top, width, height);
		}

		public bool Contains(T x, T y)
		{
			Rect<T> r = GetNormalized();

			return (r.Left <= x) && (x < r.Left + r.Width)
				&& (r.Top <= y) && (y < r.Top + r.Height);
		}

		public bool Contains(IReadOnlyVector2<T> point)
		{
			return Contains(point.X, point.Y);
		}

		public bool Intersects(Rect<T> rectangle, out Rect<T> intersection)
		{
			Rect<T> r1 = GetNormalized();
			Rect<T> r2 = rectangle.GetNormalized();

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

		public bool Equals(Rect<T> other)
		{
			return Left.Equals(other.Left)
				&& Top.Equals(other.Top)
				&& Width.Equals(other.Width)
				&& Height.Equals(other.Height);
		}

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
			return this.Format(format, formatProvider);
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

		public static implicit operator (Vector2<T>, Vector2<T>)(Rect<T> value)
		{
			return (value.Position, value.Size);
		}
		public static implicit operator Rect<T>((Vector2<T> position, Vector2<T> size) value)
		{
			return new(value.position, value.size);
		}

		public static implicit operator (T, T, T, T)(Rect<T> value)
		{
			return (value.Left, value.Top, value.Width, value.Height);
		}
		public static implicit operator Rect<T>((T left, T top, T width, T height) value)
		{
			return new(value.left, value.top, value.width, value.height);
		}

		#endregion
	}
}
