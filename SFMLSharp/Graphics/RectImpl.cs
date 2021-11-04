using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.Versioning;

using SFML.System;

namespace SFML.Graphics
{
	public struct FloatRect :
		IRect<FloatRect, float>,
		IReadOnlyRect<float>,
		IEquatable<FloatRect>,
		IFormattable
	{
		#region Fields & Properties

		public float Left;
		public float Top;
		public float Width;
		public float Height;

		float IRect<FloatRect, float>.Left { get => Left; set => Left = value; }
		float IRect<FloatRect, float>.Top { get => Top; set => Top = value; }
		float IRect<FloatRect, float>.Width { get => Width; set => Width = value; }
		float IRect<FloatRect, float>.Height { get => Height; set => Height = value; }

		float IReadOnlyRect<float>.Left => Left;
		float IReadOnlyRect<float>.Top => Top;
		float IReadOnlyRect<float>.Width => Width;
		float IReadOnlyRect<float>.Height => Height;

		public Vector2F Position
		{
			get => new(Left, Top);
			set
			{
				Left = value.X;
				Top = value.Y;
			}
		}
		public Vector2F Size
		{
			get => new(Width, Height);
			set
			{
				Width = value.X;
				Height = value.Y;
			}
		}

		IReadOnlyVector2<float> IRect<FloatRect, float>.Position
		{
			get => Position;
			set => Position = new(value);
		}
		IReadOnlyVector2<float> IRect<FloatRect, float>.Size
		{
			get => Size;
			set => Size = new(value);
		}

		IReadOnlyVector2<float> IReadOnlyRect<float>.Position => Position;
		IReadOnlyVector2<float> IReadOnlyRect<float>.Size => Size;

		#endregion

		#region Constructors

		public FloatRect(IReadOnlyRect<float> value) : this(value.Left, value.Top, value.Width, value.Height) { }

		public FloatRect(IReadOnlyVector2<float> position, IReadOnlyVector2<float> size)
			: this(position.X, position.Y, size.X, size.Y) { }

		public FloatRect(float left, float top, float width, float height)
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}

		#endregion

		#region Interface Methods Implementations

		public FloatRect GetNormalized()
		{
			if (Size == default) return this;

			float left;
			float top;
			float width;
			float height;

			if (Width < 0)
			{
				left = Left - Width;
				width = -Width;
			}
			else
			{
				left = Left;
				width = Width;
			}

			if (Height < 0)
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

		public bool Contains(float x, float y)
		{
			FloatRect r = GetNormalized();

			return (r.Left <= x) && (x < r.Left + r.Width)
				&& (r.Top <= y) && (y < r.Top + r.Height);
		}

		public bool Contains(IReadOnlyVector2<float> point)
		{
			return Contains(point.X, point.Y);
		}

		public bool Intersects(FloatRect rectangle, out FloatRect intersection)
		{
			FloatRect r1 = GetNormalized();
			FloatRect r2 = rectangle.GetNormalized();
			float interLeft = MathF.Max(r1.Left, r2.Left);
			float interTop = MathF.Max(r1.Top, r2.Top);
			float interRight = MathF.Max(r1.Left + r1.Width, r2.Left + r2.Width);
			float interBottom = MathF.Max(r1.Top + r1.Height, r2.Top + r2.Height);

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

		public bool Equals(FloatRect other)
		{
			return Left.Equals(other.Left)
				&& Top.Equals(other.Top)
				&& Width.Equals(other.Width)
				&& Height.Equals(other.Height);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is FloatRect other && Equals(other);
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

		public static bool operator ==(FloatRect left, FloatRect right)
		{
			return left.Left == right.Left
				&& left.Top == right.Top
				&& left.Width == right.Width
				&& left.Height == right.Height;
		}

		public static bool operator !=(FloatRect left, FloatRect right)
		{
			return left.Left != right.Left
				|| left.Top != right.Top
				|| left.Width != right.Width
				|| left.Height != right.Height;
		}

		#endregion

		#region Cast Operators

		public static implicit operator (Vector2F, Vector2F)(FloatRect value)
		{
			return (value.Position, value.Size);
		}
		public static implicit operator FloatRect((Vector2F position, Vector2F size) value)
		{
			return new(value.position, value.size);
		}

		public static implicit operator (float, float, float, float)(FloatRect value)
		{
			return (value.Left, value.Top, value.Width, value.Height);
		}
		public static implicit operator FloatRect((float left, float top, float width, float height) value)
		{
			return new(value.left, value.top, value.width, value.height);
		}

		[RequiresPreviewFeatures]
		public static implicit operator Rect<float>(FloatRect value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}
		[RequiresPreviewFeatures]
		public static implicit operator FloatRect(Rect<float> value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}

		public static explicit operator FloatRect(IntRect value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}

		public static explicit operator RectangleF(FloatRect value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}
		public static explicit operator FloatRect(RectangleF value)
		{
			return new(value.X, value.Y, value.Width, value.Height);
		}

		public static explicit operator Rectangle(FloatRect value)
		{
			return new((int)value.Left, (int)value.Top, (int)value.Width, (int)value.Height);
		}
		public static explicit operator FloatRect(Rectangle value)
		{
			return new(value.X, value.Y, value.Width, value.Height);
		}

		#endregion
	}

	public struct IntRect :
		IRect<IntRect, int>,
		IReadOnlyRect<int>,
		IEquatable<IntRect>,
		IFormattable
	{
		#region Fields & Properties

		public int Left;
		public int Top;
		public int Width;
		public int Height;

		int IRect<IntRect, int>.Left { get => Left; set => Left = value; }
		int IRect<IntRect, int>.Top { get => Top; set => Top = value; }
		int IRect<IntRect, int>.Width { get => Width; set => Width = value; }
		int IRect<IntRect, int>.Height { get => Height; set => Height = value; }

		int IReadOnlyRect<int>.Left => Left;
		int IReadOnlyRect<int>.Top => Top;
		int IReadOnlyRect<int>.Width => Width;
		int IReadOnlyRect<int>.Height => Height;

		public Vector2I Position
		{
			get => new(Left, Top);
			set
			{
				Left = value.X;
				Top = value.Y;
			}
		}
		public Vector2I Size
		{
			get => new(Width, Height);
			set
			{
				Width = value.X;
				Height = value.Y;
			}
		}

		IReadOnlyVector2<int> IRect<IntRect, int>.Position
		{
			get => Position;
			set => Position = new(value);
		}
		IReadOnlyVector2<int> IRect<IntRect, int>.Size
		{
			get => Size;
			set => Size = new(value);
		}

		IReadOnlyVector2<int> IReadOnlyRect<int>.Position => Position;
		IReadOnlyVector2<int> IReadOnlyRect<int>.Size => Size;

		#endregion

		#region Constructors

		public IntRect(IReadOnlyRect<int> value) : this(value.Left, value.Top, value.Width, value.Height) { }

		public IntRect(IReadOnlyVector2<int> position, IReadOnlyVector2<int> size)
			: this(position.X, position.Y, size.X, size.Y) { }

		public IntRect(int left, int top, int width, int height)
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}

		#endregion

		#region Interface Methods Implementations

		public IntRect GetNormalized()
		{
			if (Size == default) return this;

			int left;
			int top;
			int width;
			int height;

			if (Width < 0)
			{
				left = Left - Width;
				width = -Width;
			}
			else
			{
				left = Left;
				width = Width;
			}

			if (Height < 0)
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

		public bool Contains(int x, int y)
		{
			IntRect r = GetNormalized();

			return (r.Left <= x) && (x < r.Left + r.Width)
				&& (r.Top <= y) && (y < r.Top + r.Height);
		}

		public bool Contains(IReadOnlyVector2<int> point)
		{
			return Contains(point.X, point.Y);
		}

		public bool Intersects(IntRect rectangle, out IntRect intersection)
		{
			IntRect r1 = GetNormalized();
			IntRect r2 = rectangle.GetNormalized();
			int interLeft = Math.Max(r1.Left, r2.Left);
			int interTop = Math.Max(r1.Top, r2.Top);
			int interRight = Math.Max(r1.Left + r1.Width, r2.Left + r2.Width);
			int interBottom = Math.Max(r1.Top + r1.Height, r2.Top + r2.Height);

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

		public bool Equals(IntRect other)
		{
			return Left.Equals(other.Left)
				&& Top.Equals(other.Top)
				&& Width.Equals(other.Width)
				&& Height.Equals(other.Height);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is IntRect other && Equals(other);
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

		public static bool operator ==(IntRect left, IntRect right)
		{
			return left.Left == right.Left
				&& left.Top == right.Top
				&& left.Width == right.Width
				&& left.Height == right.Height;
		}

		public static bool operator !=(IntRect left, IntRect right)
		{
			return left.Left != right.Left
				|| left.Top != right.Top
				|| left.Width != right.Width
				|| left.Height != right.Height;
		}

		#endregion

		#region Cast Operators

		public static implicit operator (Vector2I, Vector2I)(IntRect value)
		{
			return (value.Position, value.Size);
		}
		public static implicit operator IntRect((Vector2I position, Vector2I size) value)
		{
			return new(value.position, value.size);
		}

		public static implicit operator (int, int, int, int)(IntRect value)
		{
			return (value.Left, value.Top, value.Width, value.Height);
		}
		public static implicit operator IntRect((int left, int top, int width, int height) value)
		{
			return new(value.left, value.top, value.width, value.height);
		}

		[RequiresPreviewFeatures]
		public static implicit operator Rect<int>(IntRect value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}
		[RequiresPreviewFeatures]
		public static implicit operator IntRect(Rect<int> value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}

		public static explicit operator IntRect(FloatRect value)
		{
			return new((int)value.Left, (int)value.Top, (int)value.Width, (int)value.Height);
		}

		public static explicit operator RectangleF(IntRect value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}
		public static explicit operator IntRect(RectangleF value)
		{
			return new((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
		}

		public static explicit operator Rectangle(IntRect value)
		{
			return new(value.Left, value.Top, value.Width, value.Height);
		}
		public static explicit operator IntRect(Rectangle value)
		{
			return new(value.X, value.Y, value.Width, value.Height);
		}

		#endregion
	}

}
