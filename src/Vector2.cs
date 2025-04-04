#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */

/* Derived from code by the Mono.Xna Team (Copyright 2006).
 * Released under the MIT License. See monoxna.LICENSE for details.
 */
#endregion

#region Using Statements
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

using Microsoft.Xna.Framework.Design;
#endregion

namespace Microsoft.Xna.Framework
{
	/// <summary>
	/// Describes a 2D-vector.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(Vector2Converter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Vector2 : IEquatable<Vector2>
	{
		#region Extensions

		public static Vector2 operator *(Vector2 value1, Matrix value2)
		{

			return Transform(value1, value2);
		}

		public static Vector4 operator ^(Vector2 a, double d)
		{
			int b = (int) (d * 10000d);
			return new Vector4(
				a[(int) Math.Floor(b / 1000f) % 10]
				, a[(int) Math.Floor(b / 100f) % 10]
				, a[(int) Math.Floor(b / 10f) % 10]
				, a[b % 10]
				);

		}
		// doesn't make sense for this to return something larger because it's flipping current data not swizzling
		public static Vector2 operator ^(Vector2 a, float d)
		{
			int b = (int) (d * 10000f);
			return a * new Vector2(
				Math.Floor(b / 1000f) % 10 == 0 ? 1 : -1
				, Math.Floor(b / 100f) % 10 == 0 ? 1 : -1
				);
		}
		public static Vector3 operator ~(Vector2 a)
		{
			return a[.1234];
		}

		public Vector2 this[float i]
		{
			get
			{
				return this ^ i;
			}
			set
			{

			}
		}
		public Vector4 this[double i]
		{
			get
			{
				return new Vector4(
					this[(int) Math.Floor(i * 10000 / 1000f) % 10]
					, this[(int) Math.Floor(i * 10000 / 100f) % 10]
					, this[(int) Math.Floor(i * 10000 / 10f) % 10]
					, this[(int) Math.Ceiling((i % .001) * 10000)]
					);//Vector4(a[b%10],0,0,0);
			}
			set
			{
				//v[.4321]= new vector4(1,2,3,4)
				this[(int) Math.Floor(i * 10000 / 1000f) % 10] = value.X;
				this[(int) Math.Floor(i * 10000 / 100f) % 10] = value.Y;
				this[(int) Math.Floor(i * 10000 / 10f) % 10] = value.Z;
				this[(int) Math.Ceiling((i % .001) * 10000)] = value.W;
			}
		}
		public float this[int i]
		{
			get
			{
				switch (i)
				{
					default: return 0;
					case 1: return X;
					case 2: return Y;
						//case 3: return 1;
						//case 4: return 1;
				}

			}
			set
			{
				switch (i)
				{
					//case 0: X = Y = value; break;
					case 1: X = value; break;
					case 2: Y = value; break;
						//case 4: W = value; break;
				}
			}
		}
		public static implicit operator Vector2(Vector4 value)
		{
			return new Vector2(value.X, value.Y);
		}
		public static implicit operator Vector2(Vector3 value)
		{
			return new Vector2(value.X, value.Y);
		}
		/*public static implicit operator Vector2(float value)
        {
            return new Vector2(value, 0);
        }*/
		/*public static implicit operator float(Vector2 value)
        {
            return value.X;
        }*/
		#endregion


		#region Public Static Properties

		/// <summary>
		/// Returns a <see cref="Vector2"/> with components 0, 0.
		/// </summary>
		public static Vector2 Zero
		{
			get
			{
				return zeroVector;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector2"/> with components 1, 1.
		/// </summary>
		public static Vector2 One
		{
			get
			{
				return unitVector;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector2"/> with components 1, 0.
		/// </summary>
		public static Vector2 UnitX
		{
			get
			{
				return unitXVector;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector2"/> with components 0, 1.
		/// </summary>
		public static Vector2 UnitY
		{
			get
			{
				return unitYVector;
			}
		}

		#endregion

		#region Internal Properties

		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(
					X.ToString(), " ",
					Y.ToString()
				);
			}
		}

		#endregion

		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Vector2"/>.
		/// </summary>
		public float X;

		/// <summary>
		/// The y coordinate of this <see cref="Vector2"/>.
		/// </summary>
		public float Y;

		#endregion

		#region Private Static Fields

		// These are NOT readonly, for weird performance reasons -flibit
		private static Vector2 zeroVector = new Vector2(0f, 0f);
		private static Vector2 unitVector = new Vector2(1f, 1f);
		private static Vector2 unitXVector = new Vector2(1f, 0f);
		private static Vector2 unitYVector = new Vector2(0f, 1f);

		#endregion

		#region Public Constructors

		/// <summary>
		/// Constructs a 2d vector with X and Y from two values.
		/// </summary>
		/// <param name="x">The x coordinate in 2d-space.</param>
		/// <param name="y">The y coordinate in 2d-space.</param>
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Constructs a 2d vector with X and Y set to the same value.
		/// </summary>
		/// <param name="value">The x and y coordinates in 2d-space.</param>
		public Vector2(float value)
		{
			this.X = value;
			this.Y = value;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public override bool Equals(object obj)
		{
			return (obj is Vector2) && Equals((Vector2) obj);
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Vector2"/>.
		/// </summary>
		/// <param name="other">The <see cref="Vector2"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Vector2 other)
		{
			return (	X == other.X &&
					Y == other.Y	);
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Vector2"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Vector2"/>.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode();
		}

		/// <summary>
		/// Returns the length of this <see cref="Vector2"/>.
		/// </summary>
		/// <returns>The length of this <see cref="Vector2"/>.</returns>
		public float Length()
		{
			return (float) Math.Sqrt((X * X) + (Y * Y));
		}

		/// <summary>
		/// Returns the squared length of this <see cref="Vector2"/>.
		/// </summary>
		/// <returns>The squared length of this <see cref="Vector2"/>.</returns>
		public float LengthSquared()
		{
			return (X * X) + (Y * Y);
		}

		/// <summary>
		/// Turns this <see cref="Vector2"/> to a unit vector with the same direction.
		/// </summary>
		public void Normalize()
		{
			float val = 1.0f / (float) Math.Sqrt((X * X) + (Y * Y));
			X *= val;
			Y *= val;
		}

		/// <summary>
		/// Returns a <see cref="String"/> representation of this <see cref="Vector2"/> in the format:
		/// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
		/// </summary>
		/// <returns>A <see cref="String"/> representation of this <see cref="Vector2"/>.</returns>
		public override string ToString()
		{
			return (
				"{X:" + X.ToString() +
				" Y:" + Y.ToString() +
				"}"
			);
		}

		#endregion

		#region Internal Methods

		[Conditional("DEBUG")]
		internal void CheckForNaNs()
		{
			if (float.IsNaN(X) || float.IsNaN(Y))
			{
				throw new InvalidOperationException("Vector2 contains NaNs!");
			}
		}

		#endregion

		#region Public Static Methods

		/// <summary>
		/// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
		/// </summary>
		/// <param name="value1">The first vector to add.</param>
		/// <param name="value2">The second vector to add.</param>
		/// <returns>The result of the vector addition.</returns>
		public static Vector2 Add(Vector2 value1, Vector2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		/// <summary>
		/// Performs vector addition on <paramref name="value1"/> and
		/// <paramref name="value2"/>, storing the result of the
		/// addition in <paramref name="result"/>.
		/// </summary>
		/// <param name="value1">The first vector to add.</param>
		/// <param name="value2">The second vector to add.</param>
		/// <param name="result">The result of the vector addition.</param>
		public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
		/// </summary>
		/// <param name="value1">The first vector of 2d-triangle.</param>
		/// <param name="value2">The second vector of 2d-triangle.</param>
		/// <param name="value3">The third vector of 2d-triangle.</param>
		/// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
		/// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
		/// <returns>The cartesian translation of barycentric coordinates.</returns>
		public static Vector2 Barycentric(
			Vector2 value1,
			Vector2 value2,
			Vector2 value3,
			float amount1,
			float amount2
		) {
			return new Vector2(
				MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
				MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
		/// </summary>
		/// <param name="value1">The first vector of 2d-triangle.</param>
		/// <param name="value2">The second vector of 2d-triangle.</param>
		/// <param name="value3">The third vector of 2d-triangle.</param>
		/// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
		/// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
		/// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
		public static void Barycentric(
			ref Vector2 value1,
			ref Vector2 value2,
			ref Vector2 value3,
			float amount1,
			float amount2,
			out Vector2 result
		) {
			result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
			result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains CatmullRom interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector in interpolation.</param>
		/// <param name="value2">The second vector in interpolation.</param>
		/// <param name="value3">The third vector in interpolation.</param>
		/// <param name="value4">The fourth vector in interpolation.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <returns>The result of CatmullRom interpolation.</returns>
		public static Vector2 CatmullRom(
			Vector2 value1,
			Vector2 value2,
			Vector2 value3,
			Vector2 value4,
			float amount
		) {
			return new Vector2(
				MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
				MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains CatmullRom interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector in interpolation.</param>
		/// <param name="value2">The second vector in interpolation.</param>
		/// <param name="value3">The third vector in interpolation.</param>
		/// <param name="value4">The fourth vector in interpolation.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
		public static void CatmullRom(
			ref Vector2 value1,
			ref Vector2 value2,
			ref Vector2 value3,
			ref Vector2 value4,
			float amount,
			out Vector2 result
		) {
			result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
			result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
		}

		/// <summary>
		/// Clamps the specified value within a range.
		/// </summary>
		/// <param name="value1">The value to clamp.</param>
		/// <param name="min">The min value.</param>
		/// <param name="max">The max value.</param>
		/// <returns>The clamped value.</returns>
		public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
		{
			return new Vector2(
				MathHelper.Clamp(value1.X, min.X, max.X),
				MathHelper.Clamp(value1.Y, min.Y, max.Y)
			);
		}

		/// <summary>
		/// Clamps the specified value within a range.
		/// </summary>
		/// <param name="value1">The value to clamp.</param>
		/// <param name="min">The min value.</param>
		/// <param name="max">The max value.</param>
		/// <param name="result">The clamped value as an output parameter.</param>
		public static void Clamp(
			ref Vector2 value1,
			ref Vector2 min,
			ref Vector2 max,
			out Vector2 result
		) {
			result.X = MathHelper.Clamp(value1.X, min.X, max.X);
			result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The distance between two vectors.</returns>
		public static float Distance(Vector2 value1, Vector2 value2)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (float) Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The distance between two vectors as an output parameter.</param>
		public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (float) Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The squared distance between two vectors.</returns>
		public static float DistanceSquared(Vector2 value1, Vector2 value2)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (v1 * v1) + (v2 * v2);
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The squared distance between two vectors as an output parameter.</param>
		public static void DistanceSquared(
			ref Vector2 value1,
			ref Vector2 value2,
			out float result
		) {
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (v1 * v1) + (v2 * v2);
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Divisor <see cref="Vector2"/>.</param>
		/// <returns>The result of dividing the vectors.</returns>
		public static Vector2 Divide(Vector2 value1, Vector2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Divisor <see cref="Vector2"/>.</param>
		/// <param name="result">The result of dividing the vectors as an output parameter.</param>
		public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector2"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <returns>The result of dividing a vector by a scalar.</returns>
		public static Vector2 Divide(Vector2 value1, float divider)
		{
			float factor = 1 / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector2"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
		public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
		{
			float factor = 1 / divider;
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The dot product of two vectors.</returns>
		public static float Dot(Vector2 value1, Vector2 value2)
		{
			return (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The dot product of two vectors as an output parameter.</param>
		public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains hermite spline interpolation.
		/// </summary>
		/// <param name="value1">The first position vector.</param>
		/// <param name="tangent1">The first tangent vector.</param>
		/// <param name="value2">The second position vector.</param>
		/// <param name="tangent2">The second tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <returns>The hermite spline interpolation vector.</returns>
		public static Vector2 Hermite(
			Vector2 value1,
			Vector2 tangent1,
			Vector2 value2,
			Vector2 tangent2,
			float amount
		) {
			Vector2 result = new Vector2();
			Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains hermite spline interpolation.
		/// </summary>
		/// <param name="value1">The first position vector.</param>
		/// <param name="tangent1">The first tangent vector.</param>
		/// <param name="value2">The second position vector.</param>
		/// <param name="tangent2">The second tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
		public static void Hermite(
			ref Vector2 value1,
			ref Vector2 tangent1,
			ref Vector2 value2,
			ref Vector2 tangent2,
			float amount,
			out Vector2 result
		) {
			result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		/// <returns>The result of linear interpolation of the specified vectors.</returns>
		public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
		{
			return new Vector2(
				MathHelper.Lerp(value1.X, value2.X, amount),
				MathHelper.Lerp(value1.Y, value2.Y, amount)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		/// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
		public static void Lerp(
			ref Vector2 value1,
			ref Vector2 value2,
			float amount,
			out Vector2 result
		) {
			result.X = MathHelper.Lerp(value1.X, value2.X, amount);
			result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Vector2"/> with maximal values from the two vectors.</returns>
		public static Vector2 Max(Vector2 value1, Vector2 value2)
		{
			return new Vector2(
				value1.X > value2.X ? value1.X : value2.X,
				value1.Y > value2.Y ? value1.Y : value2.Y
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Vector2"/> with maximal values from the two vectors as an output parameter.</param>
		public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X > value2.X ? value1.X : value2.X;
			result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Vector2"/> with minimal values from the two vectors.</returns>
		public static Vector2 Min(Vector2 value1, Vector2 value2)
		{
			return new Vector2(
				value1.X < value2.X ? value1.X : value2.X,
				value1.Y < value2.Y ? value1.Y : value2.Y
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Vector2"/> with minimal values from the two vectors as an output parameter.</param>
		public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X < value2.X ? value1.X : value2.X;
			result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Source <see cref="Vector2"/>.</param>
		/// <returns>The result of the vector multiplication.</returns>
		public static Vector2 Multiply(Vector2 value1, Vector2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a multiplication of <see cref="Vector2"/> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <returns>The result of the vector multiplication with a scalar.</returns>
		public static Vector2 Multiply(Vector2 value1, float scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a multiplication of <see cref="Vector2"/> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
		public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Source <see cref="Vector2"/>.</param>
		/// <param name="result">The result of the vector multiplication as an output parameter.</param>
		public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains the specified vector inversion.
		/// direction of <paramref name="value"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <returns>The result of the vector inversion.</returns>
		public static Vector2 Negate(Vector2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains the specified vector inversion.
		/// direction of <paramref name="value"/> in <paramref name="result"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="result">The result of the vector inversion as an output parameter.</param>
		public static void Negate(ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <returns>Unit vector.</returns>
		public static Vector2 Normalize(Vector2 value)
		{
			float val = 1.0f / (float) Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			value.X *= val;
			value.Y *= val;
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="result">Unit vector as an output parameter.</param>
		public static void Normalize(ref Vector2 value, out Vector2 result)
		{
			float val = 1.0f / (float) Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			result.X = value.X * val;
			result.Y = value.Y * val;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains reflect vector of the given vector and normal.
		/// </summary>
		/// <param name="vector">Source <see cref="Vector2"/>.</param>
		/// <param name="normal">Reflection normal.</param>
		/// <returns>Reflected vector.</returns>
		public static Vector2 Reflect(Vector2 vector, Vector2 normal)
		{
			Vector2 result;
			float val = 2.0f * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains reflect vector of the given vector and normal.
		/// </summary>
		/// <param name="vector">Source <see cref="Vector2"/>.</param>
		/// <param name="normal">Reflection normal.</param>
		/// <param name="result">Reflected vector as an output parameter.</param>
		public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			float val = 2.0f * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Source <see cref="Vector2"/>.</param>
		/// <param name="amount">Weighting value.</param>
		/// <returns>Cubic interpolation of the specified vectors.</returns>
		public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
		{
			return new Vector2(
				MathHelper.SmoothStep(value1.X, value2.X, amount),
				MathHelper.SmoothStep(value1.Y, value2.Y, amount)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Source <see cref="Vector2"/>.</param>
		/// <param name="amount">Weighting value.</param>
		/// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
		public static void SmoothStep(
			ref Vector2 value1,
			ref Vector2 value2,
			float amount,
			out Vector2 result
		) {
			result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
			result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains subtraction of on <see cref="Vector2"/> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Source <see cref="Vector2"/>.</param>
		/// <returns>The result of the vector subtraction.</returns>
		public static Vector2 Subtract(Vector2 value1, Vector2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains subtraction of on <see cref="Vector2"/> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/>.</param>
		/// <param name="value2">Source <see cref="Vector2"/>.</param>
		/// <param name="result">The result of the vector subtraction as an output parameter.</param>
		public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="position">Source <see cref="Vector2"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <returns>Transformed <see cref="Vector2"/>.</returns>
		public static Vector2 Transform(Vector2 position, Matrix matrix)
		{
			return new Vector2(
				(position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
				(position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="position">Source <see cref="Vector2"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
		public static void Transform(
			ref Vector2 position,
			ref Matrix matrix,
			out Vector2 result
		) {
			float x = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
			float y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
			result.X = x;
			result.Y = y;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <returns>Transformed <see cref="Vector2"/>.</returns>
		public static Vector2 Transform(Vector2 value, Quaternion rotation)
		{
			Transform(ref value, ref rotation, out value);
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
		public static void Transform(
			ref Vector2 value,
			ref Quaternion rotation,
			out Vector2 result
		) {
			float x = 2 * -(rotation.Z * value.Y);
			float y = 2 * (rotation.Z * value.X);
			float z = 2 * (rotation.X * value.Y - rotation.Y * value.X);

			result.X = value.X + x * rotation.W + (rotation.Y * z - rotation.Z * y);
			result.Y = value.Y + y * rotation.W + (rotation.Z * x - rotation.X * z);
		}

		/// <summary>
		/// Apply transformation on all vectors within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void Transform(
			Vector2[] sourceArray,
			ref Matrix matrix,
			Vector2[] destinationArray
		) {
			Transform(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
		}

		/// <summary>
		/// Apply transformation on vectors within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2"/> should be written.</param>
		/// <param name="length">The number of vectors to be transformed.</param>
		public static void Transform(
			Vector2[] sourceArray,
			int sourceIndex,
			ref Matrix matrix,
			Vector2[] destinationArray,
			int destinationIndex,
			int length
		) {
			for (int x = 0; x < length; x += 1)
			{
				Vector2 position = sourceArray[sourceIndex + x];
				Vector2 destination = destinationArray[destinationIndex + x];
				destination.X = (position.X * matrix.M11) + (position.Y * matrix.M21)
						+ matrix.M41;
				destination.Y = (position.X * matrix.M12) + (position.Y * matrix.M22)
						+ matrix.M42;
				destinationArray[destinationIndex + x] = destination;
			}
		}

		/// <summary>
		/// Apply transformation on all vectors within array of <see cref="Vector2"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void Transform(
			Vector2[] sourceArray,
			ref Quaternion rotation,
			Vector2[] destinationArray
		) {
			Transform(
				sourceArray,
				0,
				ref rotation,
				destinationArray,
				0,
				sourceArray.Length
			);
		}

		/// <summary>
		/// Apply transformation on vectors within array of <see cref="Vector2"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2"/> should be written.</param>
		/// <param name="length">The number of vectors to be transformed.</param>
		public static void Transform(
			Vector2[] sourceArray,
			int sourceIndex,
			ref Quaternion rotation,
			Vector2[] destinationArray,
			int destinationIndex,
			int length
		) {
			for (int i = 0; i < length; i += 1)
			{
				Vector2 position = sourceArray[sourceIndex + i];
				Vector2 v;
				Transform(ref position, ref rotation, out v);
				destinationArray[destinationIndex + i] = v;
			}
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="normal">Source <see cref="Vector2"/> which represents a normal vector.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <returns>Transformed normal.</returns>
		public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
		{
			return new Vector2(
				(normal.X * matrix.M11) + (normal.Y * matrix.M21),
				(normal.X * matrix.M12) + (normal.Y * matrix.M22)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="normal">Source <see cref="Vector2"/> which represents a normal vector.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="result">Transformed normal as an output parameter.</param>
		public static void TransformNormal(
			ref Vector2 normal,
			ref Matrix matrix,
			out Vector2 result
		) {
			float x = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
			float y = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
			result.X = x;
			result.Y = y;
		}

		/// <summary>
		/// Apply transformation on all normals within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void TransformNormal(
			Vector2[] sourceArray,
			ref Matrix matrix,
			Vector2[] destinationArray
		) {
			TransformNormal(
				sourceArray,
				0,
				ref matrix,
				destinationArray,
				0,
				sourceArray.Length
			);
		}

		/// <summary>
		/// Apply transformation on normals within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2"/> should be written.</param>
		/// <param name="length">The number of normals to be transformed.</param>
		public static void TransformNormal(
			Vector2[] sourceArray,
			int sourceIndex,
			ref Matrix matrix,
			Vector2[] destinationArray,
			int destinationIndex,
			int length
		) {
			for (int i = 0; i < length; i += 1)
			{
				Vector2 position = sourceArray[sourceIndex + i];
				Vector2 result;
				result.X = (position.X * matrix.M11) + (position.Y * matrix.M21);
				result.Y = (position.X * matrix.M12) + (position.Y * matrix.M22);
				destinationArray[destinationIndex + i] = result;
			}
		}

		#endregion

		#region Public Static Operators

		/// <summary>
		/// Inverts values in the specified <see cref="Vector2"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/> on the right of the sub sign.</param>
		/// <returns>Result of the inversion.</returns>
		public static Vector2 operator -(Vector2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		/// <summary>
		/// Compares whether two <see cref="Vector2"/> instances are equal.
		/// </summary>
		/// <param name="value1"><see cref="Vector2"/> instance on the left of the equal sign.</param>
		/// <param name="value2"><see cref="Vector2"/> instance on the right of the equal sign.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public static bool operator ==(Vector2 value1, Vector2 value2)
		{
			return (	value1.X == value2.X &&
					value1.Y == value2.Y	);
		}

		/// <summary>
		/// Compares whether two <see cref="Vector2"/> instances are equal.
		/// </summary>
		/// <param name="value1"><see cref="Vector2"/> instance on the left of the equal sign.</param>
		/// <param name="value2"><see cref="Vector2"/> instance on the right of the equal sign.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public static bool operator !=(Vector2 value1, Vector2 value2)
		{
			return !(value1 == value2);
		}

		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/> on the left of the add sign.</param>
		/// <param name="value2">Source <see cref="Vector2"/> on the right of the add sign.</param>
		/// <returns>Sum of the vectors.</returns>
		public static Vector2 operator +(Vector2 value1, Vector2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		/// <summary>
		/// Subtracts a <see cref="Vector2"/> from a <see cref="Vector2"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/> on the left of the sub sign.</param>
		/// <param name="value2">Source <see cref="Vector2"/> on the right of the sub sign.</param>
		/// <returns>Result of the vector subtraction.</returns>
		public static Vector2 operator -(Vector2 value1, Vector2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		/// <summary>
		/// Multiplies the components of two vectors by each other.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/> on the left of the mul sign.</param>
		/// <param name="value2">Source <see cref="Vector2"/> on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication.</returns>
		public static Vector2 operator *(Vector2 value1, Vector2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		/// <summary>
		/// Multiplies the components of vector by a scalar.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/> on the left of the mul sign.</param>
		/// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication with a scalar.</returns>
		public static Vector2 operator *(Vector2 value, float scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		/// <summary>
		/// Multiplies the components of vector by a scalar.
		/// </summary>
		/// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
		/// <param name="value">Source <see cref="Vector2"/> on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication with a scalar.</returns>
		public static Vector2 operator *(float scaleFactor, Vector2 value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/> on the left of the div sign.</param>
		/// <param name="value2">Divisor <see cref="Vector2"/> on the right of the div sign.</param>
		/// <returns>The result of dividing the vectors.</returns>
		public static Vector2 operator /(Vector2 value1, Vector2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector2"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector2"/> on the left of the div sign.</param>
		/// <param name="divider">Divisor scalar on the right of the div sign.</param>
		/// <returns>The result of dividing a vector by a scalar.</returns>
		public static Vector2 operator /(Vector2 value1, float divider)
		{
			float factor = 1 / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		#endregion
	}
}
