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

using Microsoft.Xna.Framework.Design;
#endregion

namespace Microsoft.Xna.Framework
{
	/// <summary>
	/// Describes a 4D-vector.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(Vector4Converter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Vector4 : IEquatable<Vector4>
	{
		#region Extensions
		public static Vector4 operator *(Vector4 value1, Matrix value2)
		{

			return Transform(value1, value2);
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
					case 3: return Z;
					case 4: return W;
				}
			}
			set
			{
				switch (i)
				{
					//case 0: X = Y = Z = W = value; break;
					case 1: X = value; break;
					case 2: Y = value; break;
					case 3: Z = value; break;
					case 4: W = value; break;
				}
			}
		}
		/*public static implicit operator Vector4(Vector3 value)
        {
            return new Vector4(value, 0);
        }
        public static implicit operator Vector4(Vector2 value)
        {
            return new Vector4(value, 0,0);
        }*/
		/*public static implicit operator Vector4(float value)
        {
            return new Vector4(value,0, 0, 0);
        }*/
		#endregion


		#region Public Static Properties

		/// <summary>
		/// Returns a <see cref="Vector4"/> with components 0, 0, 0, 0.
		/// </summary>
		public static Vector4 Zero
		{
			get
			{
				return zero;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector4"/> with components 1, 1, 1, 1.
		/// </summary>
		public static Vector4 One
		{
			get
			{
				return unit;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector4"/> with components 1, 0, 0, 0.
		/// </summary>
		public static Vector4 UnitX
		{
			get
			{
				return unitX;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector4"/> with components 0, 1, 0, 0.
		/// </summary>
		public static Vector4 UnitY
		{
			get
			{
				return unitY;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector4"/> with components 0, 0, 1, 0.
		/// </summary>
		public static Vector4 UnitZ
		{
			get
			{
				return unitZ;
			}
		}

		/// <summary>
		/// Returns a <see cref="Vector4"/> with components 0, 0, 0, 1.
		/// </summary>
		public static Vector4 UnitW
		{
			get
			{
				return unitW;
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
					Y.ToString(), " ",
					Z.ToString(), " ",
					W.ToString()
				);
			}
		}

		#endregion

		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Vector4"/>.
		/// </summary>
		public float X;

		/// <summary>
		/// The y coordinate of this <see cref="Vector4"/>.
		/// </summary>
		public float Y;

		/// <summary>
		/// The z coordinate of this <see cref="Vector4"/>.
		/// </summary>
		public float Z;

		/// <summary>
		/// The w coordinate of this <see cref="Vector4"/>.
		/// </summary>
		public float W;

		#endregion

		#region Private Static Fields

		// These are NOT readonly, for weird performance reasons -flibit
		private static Vector4 zero = new Vector4();
		private static Vector4 unit = new Vector4(1f, 1f, 1f, 1f);
		private static Vector4 unitX = new Vector4(1f, 0f, 0f, 0f);
		private static Vector4 unitY = new Vector4(0f, 1f, 0f, 0f);
		private static Vector4 unitZ = new Vector4(0f, 0f, 1f, 0f);
		private static Vector4 unitW = new Vector4(0f, 0f, 0f, 1f);

		#endregion

		#region Public Constructors

		/// <summary>
		/// Constructs a 3d vector with X, Y, Z and W from four values.
		/// </summary>
		/// <param name="x">The x coordinate in 4d-space.</param>
		/// <param name="y">The y coordinate in 4d-space.</param>
		/// <param name="z">The z coordinate in 4d-space.</param>
		/// <param name="w">The w coordinate in 4d-space.</param>
		public Vector4(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>
		/// Constructs a 3d vector with X and Z from <see cref="Vector2"/> and Z and W from the scalars.
		/// </summary>
		/// <param name="value">The x and y coordinates in 4d-space.</param>
		/// <param name="z">The z coordinate in 4d-space.</param>
		/// <param name="w">The w coordinate in 4d-space.</param>
		public Vector4(Vector2 value, float z, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>
		/// Constructs a 3d vector with X, Y, Z from <see cref="Vector3"/> and W from a scalar.
		/// </summary>
		/// <param name="value">The x, y and z coordinates in 4d-space.</param>
		/// <param name="w">The w coordinate in 4d-space.</param>
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		/// <summary>
		/// Constructs a 4d vector with X, Y, Z and W set to the same value.
		/// </summary>
		/// <param name="value">The x, y, z and w coordinates in 4d-space.</param>
		public Vector4(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
			this.W = value;
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
			return (obj is Vector4) && Equals((Vector4) obj);
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Vector4"/>.
		/// </summary>
		/// <param name="other">The <see cref="Vector4"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Vector4 other)
		{
			return (	X == other.X &&
					Y == other.Y &&
					Z == other.Z &&
					W == other.W	);
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Vector4"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Vector4"/>.</returns>
		public override int GetHashCode()
		{
			return W.GetHashCode() + X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
		}

		/// <summary>
		/// Returns the length of this <see cref="Vector4"/>.
		/// </summary>
		/// <returns>The length of this <see cref="Vector4"/>.</returns>
		public float Length()
		{
			return (float) Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
		}

		/// <summary>
		/// Returns the squared length of this <see cref="Vector4"/>.
		/// </summary>
		/// <returns>The squared length of this <see cref="Vector4"/>.</returns>
		public float LengthSquared()
		{
			return (X * X) + (Y * Y) + (Z * Z) + (W * W);
		}

		/// <summary>
		/// Turns this <see cref="Vector4"/> to a unit vector with the same direction.
		/// </summary>
		public void Normalize()
		{
			float factor = 1.0f / (float) Math.Sqrt(
				(X * X) +
				(Y * Y) +
				(Z * Z) +
				(W * W)
			);
			X *= factor;
			Y *= factor;
			Z *= factor;
			W *= factor;
		}

		public override string ToString()
		{
			return (
				"{X:" + X.ToString() +
				" Y:" + Y.ToString() +
				" Z:" + Z.ToString() +
				" W:" + W.ToString() + "}"
			);
		}

		#endregion

		#region Internal Methods

		[Conditional("DEBUG")]
		internal void CheckForNaNs()
		{
			if (	float.IsNaN(X) ||
				float.IsNaN(Y) ||
				float.IsNaN(Z) ||
				float.IsNaN(W)	)
			{
				throw new InvalidOperationException("Vector4 contains NaNs!");
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
		public static Vector4 Add(Vector4 value1, Vector4 value2)
		{
			value1.W += value2.W;
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
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
		public static void Add(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.W = value1.W + value2.W;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 4d-triangle.
		/// </summary>
		/// <param name="value1">The first vector of 4d-triangle.</param>
		/// <param name="value2">The second vector of 4d-triangle.</param>
		/// <param name="value3">The third vector of 4d-triangle.</param>
		/// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 4d-triangle.</param>
		/// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 4d-triangle.</param>
		/// <returns>The cartesian translation of barycentric coordinates.</returns>
		public static Vector4 Barycentric(
			Vector4 value1,
			Vector4 value2,
			Vector4 value3,
			float amount1,
			float amount2
		) {
			return new Vector4(
				MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
				MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
				MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
				MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 4d-triangle.
		/// </summary>
		/// <param name="value1">The first vector of 4d-triangle.</param>
		/// <param name="value2">The second vector of 4d-triangle.</param>
		/// <param name="value3">The third vector of 4d-triangle.</param>
		/// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 4d-triangle.</param>
		/// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 4d-triangle.</param>
		/// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
		public static void Barycentric(
			ref Vector4 value1,
			ref Vector4 value2,
			ref Vector4 value3,
			float amount1,
			float amount2,
			out Vector4 result
		) {
			result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
			result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
			result.Z = MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2);
			result.W = MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains CatmullRom interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector in interpolation.</param>
		/// <param name="value2">The second vector in interpolation.</param>
		/// <param name="value3">The third vector in interpolation.</param>
		/// <param name="value4">The fourth vector in interpolation.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <returns>The result of CatmullRom interpolation.</returns>
		public static Vector4 CatmullRom(
			Vector4 value1,
			Vector4 value2,
			Vector4 value3,
			Vector4 value4,
			float amount
		) {
			return new Vector4(
				MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
				MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
				MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
				MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains CatmullRom interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector in interpolation.</param>
		/// <param name="value2">The second vector in interpolation.</param>
		/// <param name="value3">The third vector in interpolation.</param>
		/// <param name="value4">The fourth vector in interpolation.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
		public static void CatmullRom(
			ref Vector4 value1,
			ref Vector4 value2,
			ref Vector4 value3,
			ref Vector4 value4,
			float amount,
			out Vector4 result
		) {
			result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
			result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
			result.Z = MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount);
			result.W = MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount);
		}

		/// <summary>
		/// Clamps the specified value within a range.
		/// </summary>
		/// <param name="value1">The value to clamp.</param>
		/// <param name="min">The min value.</param>
		/// <param name="max">The max value.</param>
		/// <returns>The clamped value.</returns>
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
		{
			return new Vector4(
				MathHelper.Clamp(value1.X, min.X, max.X),
				MathHelper.Clamp(value1.Y, min.Y, max.Y),
				MathHelper.Clamp(value1.Z, min.Z, max.Z),
				MathHelper.Clamp(value1.W, min.W, max.W)
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
			ref Vector4 value1,
			ref Vector4 min,
			ref Vector4 max,
			out Vector4 result
		) {
			result.X = MathHelper.Clamp(value1.X, min.X, max.X);
			result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
			result.Z = MathHelper.Clamp(value1.Z, min.Z, max.Z);
			result.W = MathHelper.Clamp(value1.W, min.W, max.W);
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The distance between two vectors.</returns>
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			return (float) Math.Sqrt(DistanceSquared(value1, value2));
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The distance between two vectors as an output parameter.</param>
		public static void Distance(ref Vector4 value1, ref Vector4 value2, out float result)
		{
			result = (float) Math.Sqrt(DistanceSquared(value1, value2));
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The squared distance between two vectors.</returns>
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			return (
				(value1.W - value2.W) * (value1.W - value2.W) +
				(value1.X - value2.X) * (value1.X - value2.X) +
				(value1.Y - value2.Y) * (value1.Y - value2.Y) +
				(value1.Z - value2.Z) * (value1.Z - value2.Z)
			);
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The squared distance between two vectors as an output parameter.</param>
		public static void DistanceSquared(
			ref Vector4 value1,
			ref Vector4 value2,
			out float result
		) {
			result = (
				(value1.W - value2.W) * (value1.W - value2.W) +
				(value1.X - value2.X) * (value1.X - value2.X) +
				(value1.Y - value2.Y) * (value1.Y - value2.Y) +
				(value1.Z - value2.Z) * (value1.Z - value2.Z)
			);
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Divisor <see cref="Vector4"/>.</param>
		/// <returns>The result of dividing the vectors.</returns>
		public static Vector4 Divide(Vector4 value1, Vector4 value2)
		{
			value1.W /= value2.W;
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector4"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <returns>The result of dividing a vector by a scalar.</returns>
		public static Vector4 Divide(Vector4 value1, float divider)
		{
			float factor = 1f / divider;
			value1.W *= factor;
			value1.X *= factor;
			value1.Y *= factor;
			value1.Z *= factor;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector4"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
		public static void Divide(ref Vector4 value1, float divider, out Vector4 result)
		{
			float factor = 1f / divider;
			result.W = value1.W * factor;
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
			result.Z = value1.Z * factor;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Divisor <see cref="Vector4"/>.</param>
		/// <param name="result">The result of dividing the vectors as an output parameter.</param>
		public static void Divide(
			ref Vector4 value1,
			ref Vector4 value2,
			out Vector4 result
		) {
			result.W = value1.W / value2.W;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The dot product of two vectors.</returns>
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return (
				vector1.X * vector2.X +
				vector1.Y * vector2.Y +
				vector1.Z * vector2.Z +
				vector1.W * vector2.W
			);
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The dot product of two vectors as an output parameter.</param>
		public static void Dot(ref Vector4 vector1, ref Vector4 vector2, out float result)
		{
			result = (
				(vector1.X * vector2.X) +
				(vector1.Y * vector2.Y) +
				(vector1.Z * vector2.Z) +
				(vector1.W * vector2.W)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains hermite spline interpolation.
		/// </summary>
		/// <param name="value1">The first position vector.</param>
		/// <param name="tangent1">The first tangent vector.</param>
		/// <param name="value2">The second position vector.</param>
		/// <param name="tangent2">The second tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <returns>The hermite spline interpolation vector.</returns>
		public static Vector4 Hermite(
			Vector4 value1,
			Vector4 tangent1,
			Vector4 value2,
			Vector4 tangent2,
			float amount
		) {
			return new Vector4(
				MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
				MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount),
				MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount),
				MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains hermite spline interpolation.
		/// </summary>
		/// <param name="value1">The first position vector.</param>
		/// <param name="tangent1">The first tangent vector.</param>
		/// <param name="value2">The second position vector.</param>
		/// <param name="tangent2">The second tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
		public static void Hermite(
			ref Vector4 value1,
			ref Vector4 tangent1,
			ref Vector4 value2,
			ref Vector4 tangent2,
			float amount,
			out Vector4 result
		) {
			result.W = MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
			result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
			result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		/// <returns>The result of linear interpolation of the specified vectors.</returns>
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(
				MathHelper.Lerp(value1.X, value2.X, amount),
				MathHelper.Lerp(value1.Y, value2.Y, amount),
				MathHelper.Lerp(value1.Z, value2.Z, amount),
				MathHelper.Lerp(value1.W, value2.W, amount)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		/// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
		public static void Lerp(
			ref Vector4 value1,
			ref Vector4 value2,
			float amount,
			out Vector4 result
		) {
			result.X = MathHelper.Lerp(value1.X, value2.X, amount);
			result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
			result.Z = MathHelper.Lerp(value1.Z, value2.Z, amount);
			result.W = MathHelper.Lerp(value1.W, value2.W, amount);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Vector4"/> with maximal values from the two vectors.</returns>
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4(
				MathHelper.Max(value1.X, value2.X),
				MathHelper.Max(value1.Y, value2.Y),
				MathHelper.Max(value1.Z, value2.Z),
				MathHelper.Max(value1.W, value2.W)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Vector4"/> with maximal values from the two vectors as an output parameter.</param>
		public static void Max(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = MathHelper.Max(value1.X, value2.X);
			result.Y = MathHelper.Max(value1.Y, value2.Y);
			result.Z = MathHelper.Max(value1.Z, value2.Z);
			result.W = MathHelper.Max(value1.W, value2.W);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Vector4"/> with minimal values from the two vectors.</returns>
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4(
				MathHelper.Min(value1.X, value2.X),
				MathHelper.Min(value1.Y, value2.Y),
				MathHelper.Min(value1.Z, value2.Z),
				MathHelper.Min(value1.W, value2.W)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Vector4"/> with minimal values from the two vectors as an output parameter.</param>
		public static void Min(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = MathHelper.Min(value1.X, value2.X);
			result.Y = MathHelper.Min(value1.Y, value2.Y);
			result.Z = MathHelper.Min(value1.Z, value2.Z);
			result.W = MathHelper.Min(value1.W, value2.W);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Source <see cref="Vector4"/>.</param>
		/// <returns>The result of the vector multiplication.</returns>
		public static Vector4 Multiply(Vector4 value1, Vector4 value2)
		{
			value1.W *= value2.W;
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a multiplication of <see cref="Vector4"/> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <returns>The result of the vector multiplication with a scalar.</returns>
		public static Vector4 Multiply(Vector4 value1, float scaleFactor)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a multiplication of <see cref="Vector4"/> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
		public static void Multiply(ref Vector4 value1, float scaleFactor, out Vector4 result)
		{
			result.W = value1.W * scaleFactor;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Source <see cref="Vector4"/>.</param>
		/// <param name="result">The result of the vector multiplication as an output parameter.</param>
		public static void Multiply(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.W = value1.W * value2.W;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains the specified vector inversion.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <returns>The result of the vector inversion.</returns>
		public static Vector4 Negate(Vector4 value)
		{
			value = new Vector4(-value.X, -value.Y, -value.Z, -value.W);
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains the specified vector inversion.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <param name="result">The result of the vector inversion as an output parameter.</param>
		public static void Negate(ref Vector4 value, out Vector4 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <returns>Unit vector.</returns>
		public static Vector4 Normalize(Vector4 vector)
		{
			float factor = 1.0f / (float) Math.Sqrt(
				(vector.X * vector.X) +
				(vector.Y * vector.Y) +
				(vector.Z * vector.Z) +
				(vector.W * vector.W)
			);
			return new Vector4(
				vector.X * factor,
				vector.Y * factor,
				vector.Z * factor,
				vector.W * factor
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <param name="result">Unit vector as an output parameter.</param>
		public static void Normalize(ref Vector4 vector, out Vector4 result)
		{
			float factor = 1.0f / (float) Math.Sqrt(
				(vector.X * vector.X) +
				(vector.Y * vector.Y) +
				(vector.Z * vector.Z) +
				(vector.W * vector.W)
			);
			result.X = vector.X * factor;
			result.Y = vector.Y * factor;
			result.Z = vector.Z * factor;
			result.W = vector.W * factor;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains cubic interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Source <see cref="Vector4"/>.</param>
		/// <param name="amount">Weighting value.</param>
		/// <returns>Cubic interpolation of the specified vectors.</returns>
		public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(
				MathHelper.SmoothStep(value1.X, value2.X, amount),
				MathHelper.SmoothStep(value1.Y, value2.Y, amount),
				MathHelper.SmoothStep(value1.Z, value2.Z, amount),
				MathHelper.SmoothStep(value1.W, value2.W, amount)
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains cubic interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Source <see cref="Vector4"/>.</param>
		/// <param name="amount">Weighting value.</param>
		/// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
		public static void SmoothStep(
			ref Vector4 value1,
			ref Vector4 value2,
			float amount,
			out Vector4 result
		) {
			result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
			result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
			result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
			result.W = MathHelper.SmoothStep(value1.W, value2.W, amount);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains subtraction of on <see cref="Vector4"/> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Source <see cref="Vector4"/>.</param>
		/// <returns>The result of the vector subtraction.</returns>
		public static Vector4 Subtract(Vector4 value1, Vector4 value2)
		{
			value1.W -= value2.W;
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains subtraction of on <see cref="Vector4"/> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector4"/>.</param>
		/// <param name="value2">Source <see cref="Vector4"/>.</param>
		/// <param name="result">The result of the vector subtraction as an output parameter.</param>
		public static void Subtract(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.W = value1.W - value2.W;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <returns>Transformed <see cref="Vector4"/>.</returns>
		public static Vector4 Transform(Vector2 position, Matrix matrix)
		{
			Vector4 result;
			Transform(ref position, ref matrix, out result);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <returns>Transformed <see cref="Vector4"/>.</returns>
		public static Vector4 Transform(Vector3 position, Matrix matrix)
		{
			Vector4 result;
			Transform(ref position, ref matrix, out result);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <returns>Transformed <see cref="Vector4"/>.</returns>
		public static Vector4 Transform(Vector4 vector, Matrix matrix)
		{
			Transform(ref vector, ref matrix, out vector);
			return vector;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
		public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector4 result)
		{
			result = new Vector4(
				(position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
				(position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42,
				(position.X * matrix.M13) + (position.Y * matrix.M23) + matrix.M43,
				(position.X * matrix.M14) + (position.Y * matrix.M24) + matrix.M44
			);
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
		public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector4 result)
		{
			float x = (
				(position.X * matrix.M11) +
				(position.Y * matrix.M21) +
				(position.Z * matrix.M31) +
				matrix.M41
			);
			float y = (
				(position.X * matrix.M12) +
				(position.Y * matrix.M22) +
				(position.Z * matrix.M32) +
				matrix.M42
			);
			float z = (
				(position.X * matrix.M13) +
				(position.Y * matrix.M23) +
				(position.Z * matrix.M33) +
				matrix.M43
			);
			float w = (
				(position.X * matrix.M14) +
				(position.Y * matrix.M24) +
				(position.Z * matrix.M34) +
				matrix.M44
			);
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Matrix"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
		public static void Transform(ref Vector4 vector, ref Matrix matrix, out Vector4 result)
		{
			float x = (
				(vector.X * matrix.M11) +
				(vector.Y * matrix.M21) +
				(vector.Z * matrix.M31) +
				(vector.W * matrix.M41)
			);
			float y = (
				(vector.X * matrix.M12) +
				(vector.Y * matrix.M22) +
				(vector.Z * matrix.M32) +
				(vector.W * matrix.M42)
			);
			float z = (
				(vector.X * matrix.M13) +
				(vector.Y * matrix.M23) +
				(vector.Z * matrix.M33) +
				(vector.W * matrix.M43)
			);
			float w = (
				(vector.X * matrix.M14) +
				(vector.Y * matrix.M24) +
				(vector.Z * matrix.M34) +
				(vector.W * matrix.M44)
			);
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}

		/// <summary>
		/// Apply transformation on all vectors within array of <see cref="Vector4"/> by the specified <see cref="Matrix"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void Transform(
			Vector4[] sourceArray,
			ref Matrix matrix,
			Vector4[] destinationArray
		) {
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (destinationArray.Length < sourceArray.Length)
			{
				throw new ArgumentException(
					"destinationArray is too small to contain the result."
				);
			}
			for (int i = 0; i < sourceArray.Length; i += 1)
			{
				Transform(
					ref sourceArray[i],
					ref matrix,
					out destinationArray[i]
				);
			}
		}

		/// <summary>
		/// Apply transformation on vectors within array of <see cref="Vector4"/> by the specified <see cref="Matrix"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector4"/> should be written.</param>
		/// <param name="length">The number of vectors to be transformed.</param>
		public static void Transform(
			Vector4[] sourceArray,
			int sourceIndex,
			ref Matrix matrix,
			Vector4[] destinationArray,
			int destinationIndex,
			int length
		) {
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (destinationIndex + length > destinationArray.Length)
			{
				throw new ArgumentException(
					"destinationArray is too small to contain the result."
				);
			}
			if (sourceIndex + length > sourceArray.Length)
			{
				throw new ArgumentException(
					"The combination of sourceIndex and length was greater than sourceArray.Length."
				);
			}
			for (int i = 0; i < length; i += 1)
			{
				Transform(
					ref sourceArray[i + sourceIndex],
					ref matrix,
					out destinationArray[i + destinationIndex]
				);
			}
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <returns>Transformed <see cref="Vector4"/>.</returns>
		public static Vector4 Transform(Vector2 value, Quaternion rotation)
		{
			Vector4 result;
			Transform(ref value, ref rotation, out result);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <returns>Transformed <see cref="Vector4"/>.</returns>
		public static Vector4 Transform(Vector3 value, Quaternion rotation)
		{
			Vector4 result;
			Transform(ref value, ref rotation, out result);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <returns>Transformed <see cref="Vector4"/>.</returns>
		public static Vector4 Transform(Vector4 value, Quaternion rotation)
		{
			Vector4 result;
			Transform(ref value, ref rotation, out result);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector2"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
		public static void Transform(
			ref Vector2 value,
			ref Quaternion rotation,
			out Vector4 result
		) {
			double xx = rotation.X + rotation.X;
			double yy = rotation.Y + rotation.Y;
			double zz = rotation.Z + rotation.Z;
			double wxx = rotation.W * xx;
			double wyy = rotation.W * yy;
			double wzz = rotation.W * zz;
			double xxx = rotation.X * xx;
			double xyy = rotation.X * yy;
			double xzz = rotation.X * zz;
			double yyy = rotation.Y * yy;
			double yzz = rotation.Y * zz;
			double zzz = rotation.Z * zz;
			result.X = (float) (
				(double) value.X * (1.0 - yyy - zzz) +
				(double) value.Y * (xyy - wzz)
			);
			result.Y = (float) (
				(double) value.X * (xyy + wzz) +
				(double) value.Y * (1.0 - xxx - zzz)
			);
			result.Z = (float) (
				(double) value.X * (xzz - wyy) +
				(double) value.Y * (yzz + wxx)
			);
			result.W = 1.0f;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
		public static void Transform(
			ref Vector3 value,
			ref Quaternion rotation,
			out Vector4 result
		) {
			double xx = rotation.X + rotation.X;
			double yy = rotation.Y + rotation.Y;
			double zz = rotation.Z + rotation.Z;
			double wxx = rotation.W * xx;
			double wyy = rotation.W * yy;
			double wzz = rotation.W * zz;
			double xxx = rotation.X * xx;
			double xyy = rotation.X * yy;
			double xzz = rotation.X * zz;
			double yyy = rotation.Y * yy;
			double yzz = rotation.Y * zz;
			double zzz = rotation.Z * zz;
			result.X = (float) (
				(double) value.X * (1.0 - yyy - zzz) +
				(double) value.Y * (xyy - wzz) +
				(double) value.Z * (xzz + wyy)
			);
			result.Y = (float) (
				(double) value.X * (xyy + wzz) +
				(double) value.Y * (1.0 - xxx - zzz) +
				(double) value.Z * (yzz - wxx)
			);
			result.Z = (float) (
				(double) value.X * (xzz - wyy) +
				(double) value.Y * (yzz + wxx) +
				(double) value.Z * (1.0 - xxx - yyy)
			);
			result.W = 1.0f;
		}

		/// <summary>
		/// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Vector4"/>.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
		public static void Transform(
			ref Vector4 value,
			ref Quaternion rotation,
			out Vector4 result
		) {
			double xx = rotation.X + rotation.X;
			double yy = rotation.Y + rotation.Y;
			double zz = rotation.Z + rotation.Z;
			double wxx = rotation.W * xx;
			double wyy = rotation.W * yy;
			double wzz = rotation.W * zz;
			double xxx = rotation.X * xx;
			double xyy = rotation.X * yy;
			double xzz = rotation.X * zz;
			double yyy = rotation.Y * yy;
			double yzz = rotation.Y * zz;
			double zzz = rotation.Z * zz;
			result.X = (float) (
				(double) value.X * (1.0 - yyy - zzz) +
				(double) value.Y * (xyy - wzz) +
				(double) value.Z * (xzz + wyy)
			);
			result.Y = (float) (
				(double) value.X * (xyy + wzz) +
				(double) value.Y * (1.0 - xxx - zzz) +
				(double) value.Z * (yzz - wxx)
			);
			result.Z = (float) (
				(double) value.X * (xzz - wyy) +
				(double) value.Y * (yzz + wxx) +
				(double) value.Z * (1.0 - xxx - yyy)
			);
			result.W = value.W;
		}

		/// <summary>
		/// Apply transformation on all vectors within array of <see cref="Vector4"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void Transform(
			Vector4[] sourceArray,
			ref Quaternion rotation,
			Vector4[] destinationArray
		) {
			if (sourceArray == null)
			{
				throw new ArgumentException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentException("destinationArray");
			}
			if (destinationArray.Length < sourceArray.Length)
			{
				throw new ArgumentException(
					"destinationArray is too small to contain the result."
				);
			}
			for (int i = 0; i < sourceArray.Length; i += 1)
			{
				Transform(
					ref sourceArray[i],
					ref rotation,
					out destinationArray[i]
				);
			}
		}

		/// <summary>
		/// Apply transformation on vectors within array of <see cref="Vector4"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector4"/> should be written.</param>
		/// <param name="length">The number of vectors to be transformed.</param>
		public static void Transform(
			Vector4[] sourceArray,
			int sourceIndex,
			ref Quaternion rotation,
			Vector4[] destinationArray,
			int destinationIndex,
			int length
		) {
			if (sourceArray == null)
			{
				throw new ArgumentException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentException("destinationArray");
			}
			if (destinationIndex + length > destinationArray.Length)
			{
				throw new ArgumentException(
					"destinationArray is too small to contain the result."
				);
			}
			if (sourceIndex + length > sourceArray.Length)
			{
				throw new ArgumentException(
					"The combination of sourceIndex and length was greater than sourceArray.Length."
				);
			}
			for (int i = 0; i < length; i += 1)
			{
				Transform(
					ref sourceArray[i + sourceIndex],
					ref rotation,
					out destinationArray[i + destinationIndex]
				);
			}
		}

		#endregion

		#region Public Static Operators

		public static Vector4 operator -(Vector4 value)
		{
			return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
		}

		public static bool operator ==(Vector4 value1, Vector4 value2)
		{
			return (	value1.X == value2.X &&
					value1.Y == value2.Y &&
					value1.Z == value2.Z &&
					value1.W == value2.W	);
		}

		public static bool operator !=(Vector4 value1, Vector4 value2)
		{
			return !(value1 == value2);
		}

		public static Vector4 operator +(Vector4 value1, Vector4 value2)
		{
			value1.W += value2.W;
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}

		public static Vector4 operator -(Vector4 value1, Vector4 value2)
		{
			value1.W -= value2.W;
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}

		public static Vector4 operator *(Vector4 value1, Vector4 value2)
		{
			value1.W *= value2.W;
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}

		public static Vector4 operator *(Vector4 value1, float scaleFactor)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

		public static Vector4 operator *(float scaleFactor, Vector4 value1)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

		public static Vector4 operator /(Vector4 value1, Vector4 value2)
		{
			value1.W /= value2.W;
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}

		public static Vector4 operator /(Vector4 value1, float divider)
		{
			float factor = 1f / divider;
			value1.W *= factor;
			value1.X *= factor;
			value1.Y *= factor;
			value1.Z *= factor;
			return value1;
		}

		#endregion
	}
}
