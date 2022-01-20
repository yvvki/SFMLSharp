using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SFML.System
{
	/// <summary>
	///   Represents a time value.
	/// </summary>
	public readonly struct Time : IEquatable<Time>, IComparable, IComparable<Time>
	{
		#region Field & Properties

		private readonly long _microseconds; // Ticks

		internal const long TicksPerSecond = 1000000;
		public const long TicksPerMillisecond = 1000;

		/// <summary>
		///   Returns the time value as number of seconds.
		/// </summary>
		/// <seealso cref="Milliseconds" />
		/// <seealso cref="Microseconds" />
		public float Seconds => _microseconds / (float)TicksPerSecond;

		/// <summary>
		///   Returns the time value as number of milliseconds.
		/// </summary>
		/// <seealso cref="Seconds" />
		/// <seealso cref="Microseconds" />
		// literally unuseable
		public int Milliseconds => (int)(_microseconds / TicksPerMillisecond);

		/// <summary>
		///   Returns the time value as number of microseconds.
		/// </summary>
		/// <seealso cref="Seconds" />
		/// <seealso cref="Milliseconds" />
		public long Microseconds => _microseconds;

		#endregion

		#region Constructors

		private Time(long microseconds)
		{
			_microseconds = microseconds;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Time FromSeconds(float value)
		{
			return new((long)(value * TicksPerSecond));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Time FromMilliseconds(int value)
		{
			return new(value * TicksPerSecond);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Time FromMicroseconds(long value)
		{
			return new(value);
		}

		#endregion

		#region Static Methods

		public static Time Min(Time left, Time right)
		{
			return left <= right ? left : right;
		}

		public static Time Max(Time left, Time right)
		{
			return left >= right ? left : right;
		}

		public static Time Clamp(Time value, Time min, Time max)
		{
			if (max < min) throw new ArgumentOutOfRangeException(nameof(max));

			return value >= min ? value <= max ? value : max : min;
		}

		#endregion

		#region Interface Methods

		public int CompareTo(Time other)
		{
			return _microseconds.CompareTo(other._microseconds);
		}

		public int CompareTo(object? obj)
		{
			if (obj is null) return 1;

			else if (obj is Time other) return CompareTo(other);

			else throw new ArgumentException("Object must be of type SFML.System.Time");
		}

		public bool Equals(Time other)
		{
			return _microseconds.Equals(other._microseconds);
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Time other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(_microseconds);
		}

		#endregion

		#region Operators

		public static bool operator ==(Time left, Time right)
		{
			return left._microseconds == right._microseconds;
		}

		public static bool operator !=(Time left, Time right)
		{
			return left._microseconds != right._microseconds;
		}

		public static bool operator <(Time left, Time right)
		{
			return left._microseconds < right._microseconds;
		}

		public static bool operator <=(Time left, Time right)
		{
			return left._microseconds <= right._microseconds;
		}

		public static bool operator >(Time left, Time right)
		{
			return left._microseconds > right._microseconds;
		}

		public static bool operator >=(Time left, Time right)
		{
			return left._microseconds >= right._microseconds;
		}

		/// <summary>
		///   Negates a time value.
		/// </summary>
		/// <param name="value">The specified value.</param>
		/// <returns>The opposite time value.</returns>
		public static Time operator -(Time value)
		{
			return FromMicroseconds(-value._microseconds);
		}

		/// <summary>
		///   Adds two time values together.
		/// </summary>
		/// <param name="left">The augend value.</param>
		/// <param name="right">The addend value.</param>
		/// <returns>The sum time value.</returns>
		public static Time operator +(Time left, Time right)
		{
			return FromMicroseconds(left._microseconds + right._microseconds);
		}

		/// <summary>
		///   Subtracts a time value with another time value.
		/// </summary>
		/// <param name="left">The minuend value.</param>
		/// <param name="right">The subtrahend value.</param>
		/// <returns>The difference time value.</returns>
		public static Time operator -(Time left, Time right)
		{
			return FromMicroseconds(left._microseconds - right._microseconds);
		}

		/// <summary>
		///   Multiplies a time value with another value in seconds.
		/// </summary>
		/// <param name="left">The multiplicand value.</param>
		/// <param name="right">The multiplier value.</param>
		/// <returns>The product time value.</returns>
		public static Time operator *(Time left, float right)
		{
			return FromSeconds(left.Seconds * right);
		}

		/// <summary>
		///   Multiplies a time value with another value in microseconds.
		/// </summary>
		/// <param name="left">The multiplicand value.</param>
		/// <param name="right">The multiplier value.</param>
		/// <returns>The product time value.</returns>
		public static Time operator *(Time left, long right)
		{
			return FromMicroseconds(left._microseconds * right);
		}

		/// <inheritdoc cref="operator *(Time, float)" />
		public static Time operator *(float left, Time right)
		{
			return right * left;
		}

		/// <inheritdoc cref="operator *(Time, long)" />
		public static Time operator *(long left, Time right)
		{
			return right * left;
		}

		/// <summary>
		///   Divides a time value with another value in seconds.
		/// </summary>
		/// <param name="left">The dividend value.</param>
		/// <param name="right">The divisor value.</param>
		/// <returns>The quotient time value.</returns>
		public static Time operator /(Time left, float right)
		{
			return FromSeconds(left.Seconds / right);
		}

		/// <summary>
		///   Divides a time value with another value in microseconds.
		/// </summary>
		/// <param name="left">The dividend value.</param>
		/// <param name="right">The divisor value.</param>
		/// <returns>The quotient time value.</returns>
		public static Time operator /(Time left, long right)
		{
			return FromMicroseconds(left._microseconds / right);
		}

		/// <summary>
		///   Divides a time value with another time value.
		/// </summary>
		/// <param name="left">The dividend value.</param>
		/// <param name="right">The divisor value.</param>
		/// <returns>The ratio of the two values.</returns>
		public static float operator /(Time left, Time right)
		{
			return left.Seconds / right.Seconds;
		}

		/// <summary>
		///   Modulos a time value with another time value.
		/// </summary>
		/// <param name="left">The dividend value.</param>
		/// <param name="right">The divisor value.</param>
		/// <returns>The remainder time value.</returns>
		public static Time operator %(Time left, Time right)
		{
			return FromMicroseconds(left._microseconds % right._microseconds);
		}

		#endregion

		#region Cast Operators

		internal const long TimeSpanTicksRatio = TimeSpan.TicksPerMillisecond / TicksPerMillisecond;

		public static explicit operator TimeSpan(Time time)
		{
			return TimeSpan.FromTicks(time._microseconds * TimeSpanTicksRatio);
		}

		public static explicit operator Time(TimeSpan time)
		{
			return FromMicroseconds(time.Ticks / TimeSpanTicksRatio);
		}

		#endregion
	}
}
