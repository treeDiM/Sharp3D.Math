#region Sharp3D.Math, Copyright(C) 2004 Eran Kampf, Licensed under LGPL.
//	Sharp3D.Math math library
//	Copyright (C) 2003-2004  
//	Eran Kampf
//	eran@ekampf.com
//	http://www.ekampf.com
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#endregion
#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;
#endregion

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents 3-Dimentional vector of double-precision floating point numbers.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[TypeConverter(typeof(Vector3DConverter))]
	public struct Vector3D : ICloneable
	{
		#region Private fields
		private double _x;
		private double _y;
		private double _z;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3D"/> class with the specified coordinates.
		/// </summary>
		/// <param name="x">The vector's X coordinate.</param>
		/// <param name="y">The vector's Y coordinate.</param>
		/// <param name="z">The vector's Z coordinate.</param>
		public Vector3D(double x, double y, double z)
		{
			_x = x;
			_y = y;
			_z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3D"/> class with the specified coordinates.
		/// </summary>
		/// <param name="coordinates">An array containing the coordinate parameters.</param>
		public Vector3D(double[] coordinates)
		{
			Debug.Assert(coordinates != null);
			Debug.Assert(coordinates.Length >= 3);

			_x = coordinates[0];
			_y = coordinates[1];
			_z = coordinates[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3D"/> class with the specified coordinates.
		/// </summary>
		/// <param name="coordinates">An array containing the coordinate parameters.</param>
		public Vector3D(List<double> coordinates)
		{
			Debug.Assert(coordinates != null);
			Debug.Assert(coordinates.Count >= 3);

			_x = coordinates[0];
			_y = coordinates[1];
			_z = coordinates[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3D"/> class using coordinates from a given <see cref="Vector3D"/> instance.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> to get the coordinates from.</param>
		public Vector3D(Vector3D vector)
		{
			_x = vector.X;
			_y = vector.Y;
			_z = vector.Z;
		}
		#endregion

		#region Constants
		/// <summary>
		/// 4-Dimentional double-precision floating point zero vector.
		/// </summary>
		public static readonly Vector3D Zero = new Vector3D(0.0f, 0.0f, 0.0f);
		/// <summary>
		/// 4-Dimentional double-precision floating point X-Axis vector.
		/// </summary>
		public static readonly Vector3D XAxis = new Vector3D(1.0f, 0.0f, 0.0f);
		/// <summary>
		/// 4-Dimentional double-precision floating point Y-Axis vector.
		/// </summary>
		public static readonly Vector3D YAxis = new Vector3D(0.0f, 1.0f, 0.0f);
		/// <summary>
		/// 4-Dimentional double-precision floating point Y-Axis vector.
		/// </summary>
		public static readonly Vector3D ZAxis = new Vector3D(0.0f, 0.0f, 1.0f);
		#endregion

		#region Public properties
		/// <summary>
		/// Gets or sets the x-coordinate of this vector.
		/// </summary>
		/// <value>The x-coordinate of this vector.</value>
		public double X
		{
			get { return _x; }
			set { _x = value; }
		}
		/// <summary>
		/// Gets or sets the y-coordinate of this vector.
		/// </summary>
		/// <value>The y-coordinate of this vector.</value>
		public double Y
		{
			get { return _y; }
			set { _y = value; }
		}
		/// <summary>
		/// Gets or sets the z-coordinate of this vector.
		/// </summary>
		/// <value>The z-coordinate of this vector.</value>
		public double Z
		{
			get { return _z; }
			set { _z = value; }
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Vector3D"/> object.
		/// </summary>
		/// <returns>The <see cref="Vector3D"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Vector3D(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Vector3D"/> object.
		/// </summary>
		/// <returns>The <see cref="Vector3D"/> object this method creates.</returns>
		public Vector3D Clone()
		{
			return new Vector3D(this);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="Vector3D"/> equivalent.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="Vector3D"/>.</param>
		/// <returns>A <see cref="Vector3D"/> that represents the vector specified by the <paramref name="value"/> parameter.</returns>
		public static Vector3D Parse(string value)
		{
			Regex r = new Regex(@"\((?<x>.*),(?<y>.*),(?<z>.*)\)", RegexOptions.Singleline);
			Match m = r.Match(value);
			if (m.Success)
			{
                NumberStyles style = NumberStyles.Number;
                CultureInfo culture = CultureInfo.InvariantCulture;
				return new Vector3D(
                    double.Parse(m.Result("${x}"), style, culture)
                    , double.Parse(m.Result("${y}"), style, culture)
                    , double.Parse(m.Result("${z}"), style, culture));
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		/// <summary>
		/// Converts the specified string to its <see cref="Vector3D"/> equivalent.
		/// A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="Vector3D"/>.</param>
		/// <param name="result">
		/// When this method returns, if the conversion succeeded,
		/// contains a <see cref="Vector3D"/> representing the vector specified by <paramref name="value"/>.
		/// </param>
		/// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
		public static bool TryParse(string value, out Vector3D result)
		{
			Regex r = new Regex(@"\((?<x>.*),(?<y>.*),(?<z>.*)\)", RegexOptions.Singleline);
			Match m = r.Match(value);
			if (m.Success)
			{
                NumberStyles style = NumberStyles.Number;
                CultureInfo culture = CultureInfo.InvariantCulture;
                result = new Vector3D(
                    double.Parse(m.Result("${x}"), style, culture)
                    , double.Parse(m.Result("${y}"), style, culture)
                    , double.Parse(m.Result("${z}"), style, culture));
				return true;
			}

			result = Vector3D.Zero;
			return false;
		}
		#endregion

		#region Public Static Vector Arithmetics
		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
		public static Vector3D Add(Vector3D left, Vector3D right)
		{
			return new Vector3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
		public static Vector3D Add(Vector3D vector, double scalar)
		{
			return new Vector3D(vector.X + scalar, vector.Y + scalar, vector.Z + scalar);
		}
		/// <summary>
		/// Adds two vectors and put the result in the third vector.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		public static void Add(Vector3D left, Vector3D right, ref Vector3D result)
		{
			result.X = left.X + right.X;
			result.Y = left.Y + right.Y;
			result.Z = left.Z + right.Z;
		}
		/// <summary>
		/// Adds a vector and a scalar and put the result into another vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		public static void Add(Vector3D vector, double scalar, ref Vector3D result)
		{
			result.X = vector.X + scalar;
			result.Y = vector.Y + scalar;
			result.Z = vector.Z + scalar;
		}
		/// <summary>
		/// Subtracts a vector from a vector.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
		/// <remarks>
		///	result[i] = left[i] - right[i].
		/// </remarks>
		public static Vector3D Subtract(Vector3D left, Vector3D right)
		{
			return new Vector3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Subtracts a scalar from a vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = vector[i] - scalar
		/// </remarks>
		public static Vector3D Subtract(Vector3D vector, double scalar)
		{
			return new Vector3D(vector.X - scalar, vector.Y - scalar, vector.Z - scalar);
		}
		/// <summary>
		/// Subtracts a vector from a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = scalar - vector[i]
		/// </remarks>
		public static Vector3D Subtract(double scalar, Vector3D vector)
		{
			return new Vector3D(scalar - vector.X, scalar - vector.Y, scalar - vector.Z);
		}
		/// <summary>
		/// Subtracts a vector from a second vector and puts the result into a third vector.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		/// <remarks>
		///	result[i] = left[i] - right[i].
		/// </remarks>
		public static void Subtract(Vector3D left, Vector3D right, ref Vector3D result)
		{
			result.X = left.X - right.X;
			result.Y = left.Y - right.Y;
			result.Z = left.Z - right.Z;
		}
		/// <summary>
		/// Subtracts a vector from a scalar and put the result into another vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		/// <remarks>
		/// result[i] = vector[i] - scalar
		/// </remarks>
		public static void Subtract(Vector3D vector, double scalar, ref Vector3D result)
		{
			result.X = vector.X - scalar;
			result.Y = vector.Y - scalar;
			result.Z = vector.Z - scalar;
		}
		/// <summary>
		/// Subtracts a scalar from a vector and put the result into another vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		/// <remarks>
		/// result[i] = scalar - vector[i]
		/// </remarks>
		public static void Subtract(double scalar, Vector3D vector, ref Vector3D result)
		{
			result.X = scalar - vector.X;
			result.Y = scalar - vector.Y;
			result.Z = scalar - vector.Z;
		}
		/// <summary>
		/// Divides a vector by another vector.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
		/// <remarks>
		///	result[i] = left[i] / right[i].
		/// </remarks>
		public static Vector3D Divide(Vector3D left, Vector3D right)
		{
			return new Vector3D(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A scalar</param>
		/// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = vector[i] / scalar;
		/// </remarks>
		public static Vector3D Divide(Vector3D vector, double scalar)
		{
			return new Vector3D(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A scalar</param>
		/// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = scalar / vector[i]
		/// </remarks>
		public static Vector3D Divide(double scalar, Vector3D vector)
		{
			return new Vector3D(scalar / vector.X, scalar / vector.Y, scalar / vector.Z);
		}
		/// <summary>
		/// Divides a vector by another vector.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		/// <remarks>
		/// result[i] = left[i] / right[i]
		/// </remarks>
		public static void Divide(Vector3D left, Vector3D right, ref Vector3D result)
		{
			result.X = left.X / right.X;
			result.Y = left.Y / right.Y;
			result.Z = left.Z / right.Z;
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A scalar</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		/// <remarks>
		/// result[i] = vector[i] / scalar
		/// </remarks>
		public static void Divide(Vector3D vector, double scalar, ref Vector3D result)
		{
			result.X = vector.X / scalar;
			result.Y = vector.Y / scalar;
			result.Z = vector.Z / scalar;
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A scalar</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		/// <remarks>
		/// result[i] = scalar / vector[i]
		/// </remarks>
		public static void Divide(double scalar, Vector3D vector, ref Vector3D result)
		{
			result.X = scalar / vector.X;
			result.Y = scalar / vector.Y;
			result.Z = scalar / vector.Z;
		}
		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> containing the result.</returns>
		public static Vector3D Multiply(Vector3D vector, double scalar)
		{
			return new Vector3D(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}
		/// <summary>
		/// Multiplies a vector by a scalar and put the result in another vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		public static void Multiply(Vector3D vector, double scalar, ref Vector3D result)
		{
			result.X = vector.X * scalar;
			result.Y = vector.Y * scalar;
			result.Z = vector.Z * scalar;
		}
		/// <summary>
		/// Calculates the dot product of two vectors.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns>The dot product value.</returns>
		public static double DotProduct(Vector3D left, Vector3D right)
		{
			return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
		}
		/// <summary>
		/// Calculates the cross product of two vectors.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> containing the cross product result.</returns>
		public static Vector3D CrossProduct(Vector3D left, Vector3D right)
		{
			return new Vector3D(
				left.Y * right.Z - left.Z * right.Y,
				left.Z * right.X - left.X * right.Z,
				left.X * right.Y - left.Y * right.X);
		}
		/// <summary>
		/// Calculates the cross product of two vectors.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the cross product result.</param>
		public static void CrossProduct(Vector3D left, Vector3D right, ref Vector3D result)
		{
			result.X = left.Y * right.Z - left.Z * right.Y;
			result.Y = left.Z * right.X - left.X * right.Z;
			result.Z = left.X * right.Y - left.Y * right.X;
		}
		/// <summary>
		/// Negates a vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the negated values.</returns>
		public static Vector3D Negate(Vector3D vector)
		{
			return new Vector3D(-vector.X, -vector.Y, -vector.Z);
		}
		/// <summary>
		/// Tests whether two vectors are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEqual(Vector3D left, Vector3D right)
		{
			return ApproxEqual(left, right, MathFunctions.EpsilonD);
		}
		/// <summary>
		/// Tests whether two vectors are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEqual(Vector3D left, Vector3D right, double tolerance)
		{
			return
				(
				(System.Math.Abs(left.X - right.X) <= tolerance) &&
				(System.Math.Abs(left.Y - right.Y) <= tolerance) &&
				(System.Math.Abs(left.Z - right.Z) <= tolerance)
				);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Scale the vector so that its length is 1.
		/// </summary>
		public void Normalize()
		{
			double length = GetLength();
			if (length == 0)
			{
				throw new DivideByZeroException("Trying to normalize a vector with length of zero.");
			}

			_x /= length;
			_y /= length;
			_z /= length;
		}
		/// <summary>
		/// Calculates the length of the vector.
		/// </summary>
		/// <returns>Returns the length of the vector. (Sqrt(X*X + Y*Y))</returns>
		public double GetLength()
		{
			return System.Math.Sqrt(_x * _x + _y * _y + _z * _z);
		}
		/// <summary>
		/// Calculates the squared length of the vector.
		/// </summary>
		/// <returns>Returns the squared length of the vector. (X*X + Y*Y)</returns>
		public double GetLengthSquared()
		{
			return (_x * _x + _y * _y + _z * _z);
		}
		/// <summary>
		/// Clamps vector values to zero using a given tolerance value.
		/// </summary>
		/// <param name="tolerance">The tolerance to use.</param>
		/// <remarks>
		/// The vector values that are close to zero within the given tolerance are set to zero.
		/// </remarks>
		public void ClampZero(double tolerance)
		{
			_x = MathFunctions.Clamp(_x, 0, tolerance);
			_y = MathFunctions.Clamp(_y, 0, tolerance);
			_z = MathFunctions.Clamp(_z, 0, tolerance);
		}
		/// <summary>
		/// Clamps vector values to zero using the default tolerance value.
		/// </summary>
		/// <remarks>
		/// The vector values that are close to zero within the given tolerance are set to zero.
		/// The tolerance value used is <see cref="MathFunctions.EpsilonD"/>
		/// </remarks>
		public void ClampZero()
		{
			_x = MathFunctions.Clamp(_x, 0);
			_y = MathFunctions.Clamp(_y, 0);
			_z = MathFunctions.Clamp(_z, 0);
		}
		#endregion

		#region System.Object Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Vector3D"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector3D)
			{
				Vector3D v = (Vector3D)obj;
				return (_x == v.X) && (_y == v.Y) && (_z == v.Z);
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0}, {1}, {2})", _x, _y, _z);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified vectors are equal.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns><see langword="true"/> if the two vectors are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(Vector3D left, Vector3D right)
		{
			return ValueType.Equals(left, right);
		}
		/// <summary>
		/// Tests whether two specified vectors are not equal.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns><see langword="true"/> if the two vectors are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(Vector3D left, Vector3D right)
		{
			return !ValueType.Equals(left, right);
		}

		/// <summary>
		/// Tests if a vector's components are greater than another vector's components.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns><see langword="true"/> if the left-hand vector's components are greater than the right-hand vector's component; otherwise, <see langword="false"/>.</returns>
		public static bool operator >(Vector3D left, Vector3D right)
		{
			return (
				(left._x > right._x) &&
				(left._y > right._y) &&
				(left._z > right._z));
		}
		/// <summary>
		/// Tests if a vector's components are smaller than another vector's components.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns><see langword="true"/> if the left-hand vector's components are smaller than the right-hand vector's component; otherwise, <see langword="false"/>.</returns>
		public static bool operator <(Vector3D left, Vector3D right)
		{
			return (
				(left._x < right._x) &&
				(left._y < right._y) &&
				(left._z < right._z));
		}
		/// <summary>
		/// Tests if a vector's components are greater or equal than another vector's components.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns><see langword="true"/> if the left-hand vector's components are greater or equal than the right-hand vector's component; otherwise, <see langword="false"/>.</returns>
		public static bool operator >=(Vector3D left, Vector3D right)
		{
			return (
				(left._x >= right._x) &&
				(left._y >= right._y) &&
				(left._z >= right._z));
		}
		/// <summary>
		/// Tests if a vector's components are smaller or equal than another vector's components.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns><see langword="true"/> if the left-hand vector's components are smaller or equal than the right-hand vector's component; otherwise, <see langword="false"/>.</returns>
		public static bool operator <=(Vector3D left, Vector3D right)
		{
			return (
				(left._x <= right._x) &&
				(left._y <= right._y) &&
				(left._z <= right._z));
		}
		#endregion

		#region Unary Operators
		/// <summary>
		/// Negates the values of the given vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the negated values.</returns>
		public static Vector3D operator -(Vector3D vector)
		{
			return Vector3D.Negate(vector);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
		public static Vector3D operator +(Vector3D left, Vector3D right)
		{
			return Vector3D.Add(left, right);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
		public static Vector3D operator +(Vector3D vector, double scalar)
		{
			return Vector3D.Add(vector, scalar);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
		public static Vector3D operator +(double scalar, Vector3D vector)
		{
			return Vector3D.Add(vector, scalar);
		}
		/// <summary>
		/// Subtracts a vector from a vector.
		/// </summary>
		/// <param name="left">A <see cref="Vector3D"/> instance.</param>
		/// <param name="right">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
		/// <remarks>
		///	result[i] = left[i] - right[i].
		/// </remarks>
		public static Vector3D operator -(Vector3D left, Vector3D right)
		{
			return Vector3D.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a scalar from a vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = vector[i] - scalar
		/// </remarks>
		public static Vector3D operator -(Vector3D vector, double scalar)
		{
			return Vector3D.Subtract(vector, scalar);
		}
		/// <summary>
		/// Subtracts a vector from a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = scalar - vector[i]
		/// </remarks>
		public static Vector3D operator -(double scalar, Vector3D vector)
		{
			return Vector3D.Subtract(scalar, vector);
		}
		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> containing the result.</returns>
		public static Vector3D operator *(Vector3D vector, double scalar)
		{
			return Vector3D.Multiply(vector, scalar);
		}
		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Vector3D"/> containing the result.</returns>
		public static Vector3D operator *(double scalar, Vector3D vector)
		{
			return Vector3D.Multiply(vector, scalar);
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A scalar</param>
		/// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = vector[i] / scalar;
		/// </remarks>
		public static Vector3D operator /(Vector3D vector, double scalar)
		{
			return Vector3D.Divide(vector, scalar);
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="scalar">A scalar</param>
		/// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = scalar / vector[i]
		/// </remarks>
		public static Vector3D operator /(double scalar, Vector3D vector)
		{
			return Vector3D.Divide(scalar, vector);
		}
		#endregion

		#region Array Indexing Operator
		/// <summary>
		/// Indexer ( [x, y, z] ).
		/// </summary>
		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return _x;
					case 1:
						return _y;
					case 2:
						return _z;
					default:
						throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						_x = value;
						break;
					case 1:
						_y = value;
						break;
					case 2:
						_z = value;
						break;
					default:
						throw new IndexOutOfRangeException();
				}
			}

		}
		#endregion

		#region Conversion Operators
		/// <summary>
		/// Converts the vector to an array of double-precision floating point values.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>An array of double-precision floating point values.</returns>
		public static explicit operator double[](Vector3D vector)
		{
			double[] array = new double[3];
			array[0] = vector.X;
			array[1] = vector.Y;
			array[2] = vector.Z;
			return array;
		}
		/// <summary>
		/// Converts the vector to a <see cref="System.Collections.Generic.List"/> of double-precision floating point values.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A <see cref="System.Collections.Generic.List"/> of double-precision floating point values.</returns>
		public static explicit operator List<double>(Vector3D vector)
		{
			List<double> list = new List<double>(3);
			list.Add(vector.X);
			list.Add(vector.Y);
			list.Add(vector.Z);

			return list;
		}
		/// <summary>
		/// Converts the vector to a <see cref="System.Collections.Generic.LinkedList"/> of double-precision floating point values.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A <see cref="System.Collections.Generic.LinkedList"/> of double-precision floating point values.</returns>
		public static explicit operator LinkedList<double>(Vector3D vector)
		{
			LinkedList<double> list = new LinkedList<double>();
			list.AddLast(vector.X);
			list.AddLast(vector.Y);
			list.AddLast(vector.Z);

			return list;
		}
		#endregion
	}

	#region Vector3DConverter class
	/// <summary>
	/// Converts a <see cref="Vector3D"/> to and from string representation.
	/// </summary>
	public class Vector3DConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
		/// <returns><b>true</b> if this converter can perform the conversion; otherwise, <b>false</b>.</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}
		/// <summary>
		/// Returns whether this converter can convert the object to the specified type, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
		/// <returns><b>true</b> if this converter can perform the conversion; otherwise, <b>false</b>.</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;

			return base.CanConvertTo(context, destinationType);
		}
		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> object. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <param name="destinationType">The Type to convert the <paramref name="value"/> parameter to.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if ((destinationType == typeof(string)) && (value is Vector3D))
			{
				Vector3D v = (Vector3D)value;
				return v.ToString();
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
		/// <summary>
		/// Converts the given object to the type of this converter, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture. </param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		/// <exception cref="ParseException">Failed parsing from string.</exception>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				return Vector3D.Parse((string)value);
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Returns whether this object supports a standard set of values that can be picked from a list.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <returns><b>true</b> if <see cref="GetStandardValues"/> should be called to find a common set of values the object supports; otherwise, <b>false</b>.</returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>
		/// Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be a null reference.</param>
		/// <returns>A <see cref="TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values, or a null reference (Nothing in Visual Basic) if the data type does not support a standard set of values.</returns>
		public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			StandardValuesCollection svc =
				new StandardValuesCollection(new object[4] { Vector3D.Zero, Vector3D.XAxis, Vector3D.YAxis, Vector3D.ZAxis });

			return svc;
		}
	}
	#endregion
}
