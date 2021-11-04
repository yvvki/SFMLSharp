using System.Runtime.InteropServices;

using SFML.System;

using static SFML.Window.DllName;

namespace SFML.Window
{
	/// <summary>
	///   Sensor types.
	/// </summary>
	public enum SensorType
	{
		/// <summary>
		///   Measures the raw acceleration (m/s^2).
		/// </summary>
		Accelerometer,
		/// <summary>
		///   Measures the raw rotation rates (degrees/s).
		/// </summary>
		Gyroscope,
		/// <summary>
		///   Measures the ambient magnetic field (micro-teslas).
		/// </summary>
		Magnetometer,
		/// <summary>
		///   Measures the direction and intensity of gravity, independent of device acceleration (m/s^2).
		/// </summary>
		Gravity,
		/// <summary>
		///   Measures the direction and intensity of device acceleration, independent of the gravity (m/s^2).
		/// </summary>
		UserAcceleration,
		/// <summary>
		///   Measures the absolute 3D orientation (degrees).
		/// </summary>
		Orientation,

		/// <summary>
		///   The total number of sensor types.
		/// </summary>
		Count
	}

	/// <summary>
	///   Provides access to the real-time state of the sensors.
	/// </summary>
	public static class Sensor
	{
		/// <summary>
		///   Check if a sensor is available on the underlying platform.
		/// </summary>
		/// <param name="sensor">Sensor to check.</param>
		/// <returns>
		///   <see langword="true" /> if the sensor is available;
		///   <see langword="false" /> otherwise.
		/// </returns>
		public static bool IsAvailable(SensorType sensor)
		{
			return sfSensor_isAvailable(sensor);
		}

		/// <summary>
		///   Enable or disable a sensor.
		/// </summary>
		/// <remarks>
		///   All sensors are disabled by default, to avoid consuming too
		///   much battery power. Once a sensor is enabled, it starts
		///   sending events of the corresponding type.
		///   <para>
		///     This function does nothing if the sensor is unavailable.
		///   </para>
		/// </remarks>
		/// <param name="sensor">Sensor to enable.</param>
		/// <param name="enabled">
		///   <see langword="true" /> to enable;
		///   <see langword="false" /> to disable
		/// </param>
		public static void SetEnabled(SensorType sensor, bool enabled)
		{
			sfSensor_setEnabled(sensor, enabled);
		}

		/// <summary>
		///   Get the current sensor value.
		/// </summary>
		/// <param name="sensor">Sensor to read.</param>
		/// <returns>The current sensor value.</returns>
		public static Vector3F GetValue(SensorType sensor)
		{
			return sfSensor_getValue(sensor);
		}

		#region Imports

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool sfSensor_isAvailable(SensorType sensor);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sfSensor_setEnabled(SensorType sensor, bool enabled);

		[DllImport(csfml_window, CallingConvention = CallingConvention.Cdecl)]
		private static extern Vector3F sfSensor_getValue(SensorType sensor);

		#endregion
	}
}
