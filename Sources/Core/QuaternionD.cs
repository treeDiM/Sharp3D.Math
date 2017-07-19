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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;

#endregion

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents a double-precision floating-point quaternion.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A quaternion can be thought of as a 4-Dimentional vector of form:
	/// q = [w, x, y, z] = w + xi + yj +zk.
	/// </para>
	/// <para>
	/// A Quaternion is often written as q = s + V where S represents
	/// the scalar part (w component) and V is a 3D vector representing
	/// the imaginery coefficients (x,y,z components).
	/// </para>
	/// <para>
	/// Check out http://mathworld.wolfram.com/Quaternion.html for further details.
	/// </para>
	/// </remarks>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct QuaternionD : ICloneable
	{
		#region Private Fields
		private double _w;
		private double _x;
		private double _y;
		private double _z;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="QuaternionD"/> class with the specified coordinates.
		/// </summary>
		/// <param name="w">The quaternions's W coordinate.</param>
		/// <param name="x">The quaternions's X coordinate.</param>
		/// <param name="y">The quaternions's Y coordinate.</param>
		/// <param name="z">The quaternions's Z coordinate.</param>
		public QuaternionD(double w, double x, double y, double z)
		{
			_w = w;
			_x = x;
			_y = y;
			_z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="QuaternionD"/> class with the specified coordinates.
		/// </summary>
		/// <param name="coordinates">An array containing the coordinate parameters.</param>
		public QuaternionD(double[] coordinates)
		{
			Debug.Assert(coordinates != null);
			Debug.Assert(coordinates.Length >= 4);

			_w = coordinates[0];
			_x = coordinates[1];
			_y = coordinates[2];
			_z = coordinates[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="QuaternionD"/> class using coordinates from a given <see cref="QuaternionD"/> instance.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance to copy the coordinates from.</param>
		public QuaternionD(QuaternionD quaternion)
		{
			_w = quaternion.W;
			_x = quaternion.X;
			_y = quaternion.Y;
			_z = quaternion.Z;
		}
		#endregion

		#region Constants
		/// <summary>
		/// Double-precision floating point zero quaternion.
		/// </summary>
		public static readonly QuaternionD Zero = new QuaternionD(0, 0, 0, 0);
		/// <summary>
		/// Double-precision floating point identity quaternion.
		/// </summary>
		public static readonly QuaternionD Identity = new QuaternionD(1, 0, 0, 0);
		/// <summary>
		/// Double-precision floating point X-Axis quaternion.
		/// </summary>
		public static readonly QuaternionD XAxis = new QuaternionD(0, 1, 0, 0);
		/// <summary>
		/// Double-precision floating point Y-Axis quaternion.
		/// </summary>
		public static readonly QuaternionD YAxis = new QuaternionD(0, 0, 1, 0);
		/// <summary>
		/// Double-precision floating point Z-Axis quaternion.
		/// </summary>
		public static readonly QuaternionD ZAxis = new QuaternionD(0, 0, 0, 1);
		/// <summary>
		/// Double-precision floating point W-Axis quaternion.
		/// </summary>
		public static readonly QuaternionD WAxis = new QuaternionD(1, 0, 0, 0);
		#endregion

		#region Public Properties
		/// <summery>
		/// Gets or sets the w-coordinate of this quaternion.
		/// </summery>
		/// <value>The w-coordinate of this quaternion.</value>
		public double W
		{
			get { return _w; }
			set { _w = value; }
		}
		/// <summery>
		/// Gets or sets the x-coordinate of this quaternion.
		/// </summery>
		/// <value>The x-coordinate of this quaternion.</value>
		public double X
		{
			get { return _x; }
			set { _x = value; }
		}
		/// <summery>
		/// Gets or sets the y-coordinate of this quaternion.
		/// </summery>
		/// <value>The y-coordinate of this quaternion.</value>
		public double Y
		{
			get { return _y; }
			set { _y = value; }
		}
		/// <summery>
		/// Gets or sets the z-coordinate of this quaternion.
		/// </summery>
		/// <value>The z-coordinate of this quaternion.</value>
		public double Z
		{
			get { return _z; }
			set { _z = value; }
		}

		/// <summary>
		/// Gets the the modulus of the quaternion.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double Modulus
		{
			get
			{
				return System.Math.Sqrt(_w * _w + _x * _x + _y * _y + _z * _z);
			}
		}
		/// <summary>
		/// Gets the the squared modulus of the quaternion.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double ModulusSquared
		{
			get
			{
				return (_w * _w + _x * _x + _y * _y + _z * _z);
			}
		}
		/// <summary>
		/// Gets or sets the conjugate of the quaternion.
		/// </summary>
		/// <value>A <see cref="QuaternionD"/> instance.</value>
		public QuaternionD Conjugate
		{
			get
			{
				return new QuaternionD(_w, -_x, -_y, -_z);
			}
			set
			{
				this = value.Conjugate;
			}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="QuaternionD"/> object.
		/// </summary>
		/// <returns>The <see cref="QuaternionD"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new QuaternionD(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="QuaternionD"/> object.
		/// </summary>
		/// <returns>The <see cref="QuaternionD"/> object this method creates.</returns>
		public QuaternionD Clone()
		{
			return new QuaternionD(this);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="QuaternionD"/> equivalent.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="QuaternionD"/></param>
		/// <returns>A <see cref="QuaternionD"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
		public static QuaternionD Parse(string value)
		{
			Regex r = new Regex(@"\((?<w>.*),(?<x>.*),(?<y>.*),(?<z>.*)\)", RegexOptions.None);
			Match m = r.Match(value);
			if (m.Success)
			{
				return new QuaternionD(
					double.Parse(m.Result("${w}")),
					double.Parse(m.Result("${x}")),
					double.Parse(m.Result("${y}")),
					double.Parse(m.Result("${z}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		/// <summary>
		/// Converts the specified string to its <see cref="QuaternionD"/> equivalent.
		/// A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="QuaternionD"/>.</param>
		/// <param name="result">
		/// When this method returns, if the conversion succeeded,
		/// contains a <see cref="QuaternionD"/> representing the vector specified by <paramref name="value"/>.
		/// </param>
		/// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
		public static bool TryParse(string value, out QuaternionD result)
		{
			Regex r = new Regex(@"\((?<w>.*),(?<x>.*),(?<y>.*),(?<z>.*)\)", RegexOptions.None);
			Match m = r.Match(value);
			if (m.Success)
			{
				result = new QuaternionD(
					double.Parse(m.Result("${x}")),
					double.Parse(m.Result("${y}")),
					double.Parse(m.Result("${z}")),
					double.Parse(m.Result("${w}"))
					);

				return true;
			}

			result = QuaternionD.Zero;
			return false;
		}
		#endregion

		#region Public Static Quaternion Arithmetics
		/// <summary>
		/// Adds two quaternions.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>A new <see cref="QuaternionD"/> instance containing the sum.</returns>
		public static QuaternionD Add(QuaternionD left, QuaternionD right)
		{
			return new QuaternionD(left.W + right.W, left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}
		/// <summary>
		/// Adds two quaternions and put the result in the third quaternion.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="result">A <see cref="QuaternionD"/> instance to hold the result.</param>
		public static void Add(QuaternionD left, QuaternionD right, ref QuaternionD result)
		{
			result.W = left.W + right.W;
			result.X = left.X + right.X;
			result.Y = left.Y + right.Y;
			result.Z = left.Z + right.Z;
		}

		/// <summary>
		/// Subtracts a quaternion from a quaternion.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>A new <see cref="QuaternionD"/> instance containing the difference.</returns>
		public static QuaternionD Subtract(QuaternionD left, QuaternionD right)
		{
			return new QuaternionD(left.W - right.W, left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}
		/// <summary>
		/// Subtracts a quaternion from a quaternion and puts the result into a third quaternion.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="result">A <see cref="QuaternionD"/> instance to hold the result.</param>
		public static void Subtract(QuaternionD left, QuaternionD right, ref QuaternionD result)
		{
			result.W = left.W - right.W;
			result.X = left.X - right.X;
			result.Y = left.Y - right.Y;
			result.Z = left.Z - right.Z;
		}

		/// <summary>
		/// Multiplies quaternion <paramref name="a"/> by quaternion <paramref name="b"/>.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>A new <see cref="QuaternionD"/> containing the result.</returns>
		public static QuaternionD Multiply(QuaternionD left, QuaternionD right)
		{
			QuaternionD result = new QuaternionD();
			result.W = left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z;
			result.X = left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y;
			result.Y = left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z;
			result.Z = left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X;

			return result;
		}
		/// <summary>
		/// Multiplies quaternion <paramref name="a"/> by quaternion <paramref name="b"/> and put the result in a third quaternion.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="result">A <see cref="QuaternionD"/> instance to hold the result.</param>
		public static void Multiply(QuaternionD left, QuaternionD right, ref QuaternionD result)
		{
			result.W = left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z;
			result.X = left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y;
			result.Y = left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z;
			result.Z = left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X;
		}
		/// <summary>
		/// Multiplies a quaternion by a scalar.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A <see cref="QuaternionD"/> instance to hold the result.</returns>
		public static QuaternionD Multiply(QuaternionD quaternion, double scalar)
		{
			QuaternionD result = new QuaternionD(quaternion);
			result.W *= scalar;
			result.X *= scalar;
			result.Y *= scalar;
			result.Z *= scalar;

			return result;
		}
		/// <summary>
		/// Multiplies a quaternion by a scalar and put the result in a third quaternion.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <param name="result">A <see cref="QuaternionD"/> instance to hold the result.</param>
		public static void Multiply(QuaternionD quaternion, double scalar, ref QuaternionD result)
		{
			result.W = quaternion.W * scalar;
			result.X = quaternion.X * scalar;
			result.Y = quaternion.Y * scalar;
			result.Z = quaternion.Z * scalar;
		}

		/// <summary>
		/// Divides a quaternion by a scalar.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A <see cref="QuaternionD"/> instance to hold the result.</returns>
		public static QuaternionD Divide(QuaternionD quaternion, double scalar)
		{
			if (scalar == 0)
			{
				throw new DivideByZeroException("Dividing quaternion by zero");
			}

			QuaternionD result = new QuaternionD(quaternion);

			result.W /= scalar;
			result.X /= scalar;
			result.Y /= scalar;
			result.Z /= scalar;

			return result;
		}
		/// <summary>
		/// Divides a quaternion by a scalar and put the result in a third quaternion.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <param name="result">A <see cref="QuaternionD"/> instance to hold the result.</param>
		public static void Divide(QuaternionD quaternion, double scalar, ref QuaternionD result)
		{
			if (scalar == 0)
			{
				throw new DivideByZeroException("Dividing quaternion by zero");
			}

			result.W = quaternion.W / scalar;
			result.X = quaternion.X / scalar;
			result.Y = quaternion.Y / scalar;
			result.Z = quaternion.Z / scalar;
		}

		/// <summary>
		/// Calculates the dot product of two quaternions.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>The dot product value.</returns>
		public static double DotProduct(QuaternionD left, QuaternionD right)
		{
			return left.W * right.W + left.X * right.X + left.Y * right.Y + left.Z * right.Z;
		}
		#endregion

		#region Public Static Complex Special Functions
		/// <summary>
		/// Calculates the logarithm of a given quaternion.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>The quaternion's logarithm.</returns>
		public static QuaternionD Log(QuaternionD quaternion)
		{
			QuaternionD result = new QuaternionD(0, 0, 0, 0);

			if (MathFunctions.Abs(quaternion.W) < 1.0)
			{
				double angle = System.Math.Acos(quaternion.W);
				double sin = System.Math.Sin(angle);

				if (MathFunctions.Abs(sin) >= 0)
				{
					double coeff = angle / sin;
					result.X = coeff * quaternion.X;
					result.Y = coeff * quaternion.Y;
					result.Z = coeff * quaternion.Z;
				}
				else
				{
					result.X = quaternion.X;
					result.Y = quaternion.Y;
					result.Z = quaternion.Z;
				}
			}

			return result;
		}
		/// <summary>
		/// Calculates the exponent of a quaternion.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>The quaternion's exponent.</returns>
		public QuaternionD Exp(QuaternionD quaternion)
		{
			QuaternionD result = new QuaternionD(0, 0, 0, 0);

			double angle = System.Math.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
			double sin = System.Math.Sin(angle);

			if (MathFunctions.Abs(sin) > 0)
			{
				double coeff = angle / sin;
				result.X = coeff * quaternion.X;
				result.Y = coeff * quaternion.Y;
				result.Z = coeff * quaternion.Z;
			}
			else
			{
				result.X = quaternion.X;
				result.Y = quaternion.Y;
				result.Z = quaternion.Z;
			}

			return result;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Inverts the quaternion.
		/// </summary>
		public void Inverse()
		{
			double norm = ModulusSquared;
			if (norm > 0)
			{
				double invNorm = 1.0 / norm;
				_w *= invNorm;
				_x *= -invNorm;
				_y *= -invNorm;
				_z *= -invNorm;
			}
			else
			{
				throw new QuaternionNotInvertibleException("Quaternion " + this.ToString() + " is not invertable");
			}
		}
		/// <summary>
		/// Normelizes the quaternion.
		/// </summary>
		public void Normalize()
		{
			double norm = Modulus;
			if (norm == 0)
			{
				throw new DivideByZeroException("Trying to normalize a quaternion with modulus of zero.");
			}

			_w /= norm;
			_x /= norm;
			_y /= norm;
			_z /= norm;
		}
		/// <summary>
		/// Clamps quaternion values to zero using a given tolerance value.
		/// </summary>
		/// <param name="tolerance">The tolerance to use.</param>
		/// <remarks>
		/// The quaternion values that are close to zero within the given tolerance are set to zero.
		/// </remarks>
		public void ClampZero(double tolerance)
		{
			_x = MathFunctions.Clamp(_x, 0, tolerance);
			_y = MathFunctions.Clamp(_y, 0, tolerance);
			_z = MathFunctions.Clamp(_z, 0, tolerance);
			_w = MathFunctions.Clamp(_w, 0, tolerance);
		}
		/// <summary>
		/// Clamps quaternion values to zero using the default tolerance value.
		/// </summary>
		/// <remarks>
		/// The quaternion values that are close to zero within the given tolerance are set to zero.
		/// The tolerance value used is <see cref="MathFunctions.EpsilonD"/>
		/// </remarks>
		public void ClampZero()
		{
			_x = MathFunctions.Clamp(_x, 0);
			_y = MathFunctions.Clamp(_y, 0);
			_z = MathFunctions.Clamp(_z, 0);
			_w = MathFunctions.Clamp(_w, 0);
		}
		#endregion

		#region System.Object Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _w.GetHashCode() ^ _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="QuaternionD"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is QuaternionD)
			{
				QuaternionD quaternion = (QuaternionD)obj;
				return (_w == quaternion.W) && (_x == quaternion.X) && (_y == quaternion.Y) && (_z == quaternion.Z);
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format("({0}, {1}, {2}, {3})", _w, _x, _y, _z);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified quaternions are equal.
		/// </summary>
		/// <param name="left">The left-hand quaternion.</param>
		/// <param name="right">The right-hand quaternion.</param>
		/// <returns><see langword="true"/> if the two quaternions are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(QuaternionD left, QuaternionD right)
		{
			return ValueType.Equals(left, right);
		}
		/// <summary>
		/// Tests whether two specified quaternions are not equal.
		/// </summary>
		/// <param name="left">The left-hand quaternion.</param>
		/// <param name="right">The right-hand quaternion.</param>
		/// <returns><see langword="true"/> if the two quaternions are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(QuaternionD left, QuaternionD right)
		{
			return !ValueType.Equals(left, right);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two quaternions.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>A new <see cref="QuaternionD"/> instance containing the sum.</returns>
		public static QuaternionD operator +(QuaternionD left, QuaternionD right)
		{
			return QuaternionD.Add(left, right);
		}
		/// <summary>
		/// Subtracts a quaternion from a quaternion.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>A new <see cref="QuaternionD"/> instance containing the difference.</returns>
		public static QuaternionD operator -(QuaternionD left, QuaternionD right)
		{
			return QuaternionD.Subtract(left, right);
		}
		/// <summary>
		/// Multiplies quaternion <paramref name="a"/> by quaternion <paramref name="b"/>.
		/// </summary>
		/// <param name="left">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="right">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>A new <see cref="QuaternionD"/> containing the result.</returns>
		public static QuaternionD operator *(QuaternionD left, QuaternionD right)
		{
			return QuaternionD.Multiply(left, right);
		}
		/// <summary>
		/// Multiplies a quaternion by a scalar.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A <see cref="QuaternionD"/> instance to hold the result.</returns>
		public static QuaternionD operator *(QuaternionD quaternion, double scalar)
		{
			return QuaternionD.Multiply(quaternion, scalar);
		}
		/// <summary>
		/// Multiplies a quaternion by a scalar.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A <see cref="QuaternionD"/> instance to hold the result.</returns>
		public static QuaternionD operator *(double scalar, QuaternionD quaternion)
		{
			return QuaternionD.Multiply(quaternion, scalar);
		}
		/// <summary>
		/// Divides a quaternion by a scalar.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A <see cref="QuaternionD"/> instance to hold the result.</returns>
		public static QuaternionD operator /(QuaternionD quaternion, double scalar)
		{
			return QuaternionD.Divide(quaternion, scalar);
		}
		/// <summary>
		/// Divides a scalar by a quaternion.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A <see cref="QuaternionD"/> instance to hold the result.</returns>
		public static QuaternionD operator /(double scalar, QuaternionD quaternion)
		{
			return QuaternionD.Multiply(quaternion, (1.0 / scalar));
		}
		#endregion

		#region Array Indexing Operator
		/// <summary>
		/// Indexer ( [w, x, y, z] ).
		/// </summary>
		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return _w;
					case 1:
						return _x;
					case 2:
						return _y;
					case 3:
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
						_w = value;
						break;
					case 1:
						_x = value;
						break;
					case 2:
						_y = value;
						break;
					case 3:
						_z = value;
						break;
					default:
						throw new IndexOutOfRangeException();
				}
				return;
			}
		}
		#endregion

		#region Conversion Operators
		/// <summary>
		/// Converts the quaternion to an array of double-precision floating point numbers.
		/// </summary>
		/// <param name="quaternion">A <see cref="QuaternionD"/> instance.</param>
		/// <returns>An array of double-precision floating point numbers.</returns>
		/// <remarks>The array is [w, x, y, z].</remarks>
		public static explicit operator double[](QuaternionD quaternion)
		{
			double[] doubles = new double[4];
			doubles[0] = quaternion.W;
			doubles[1] = quaternion.X;
			doubles[2] = quaternion.Y;
			doubles[3] = quaternion.Z;
			return doubles;
		}
		#endregion
	}
}
