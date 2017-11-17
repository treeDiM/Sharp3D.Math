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
	/// Represents a 3-dimentional double-precision floating point matrix.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public struct Matrix3D : ICloneable
	{
		#region Private Fields
		private double _m11, _m12, _m13;
		private double _m21, _m22, _m23;
		private double _m31, _m32, _m33;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix3D"/> structure with the specified values.
		/// </summary>
		public Matrix3D(
			double m11, double m12, double m13,
			double m21, double m22, double m23,
			double m31, double m32, double m33
			)
		{
			_m11 = m11; _m12 = m12; _m13 = m13;
			_m21 = m21; _m22 = m22; _m23 = m23;
			_m31 = m31; _m32 = m32; _m33 = m33;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix3D"/> structure with the specified values.
		/// </summary>
		/// <param name="elements">An array containing the matrix values in row-major order.</param>
		public Matrix3D(double[] elements)
		{
			Debug.Assert(elements != null);
			Debug.Assert(elements.Length >= 9);

			_m11 = elements[0]; _m12 = elements[1]; _m13 = elements[2];
			_m21 = elements[3]; _m22 = elements[4]; _m23 = elements[5];
			_m31 = elements[6]; _m32 = elements[7]; _m33 = elements[8];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4F"/> structure with the specified values.
		/// </summary>
		/// <param name="elements">An array containing the matrix values in row-major order.</param>
		public Matrix3D(List<double> elements)
		{
			Debug.Assert(elements != null);
			Debug.Assert(elements.Count >= 9);

			_m11 = elements[0]; _m12 = elements[1]; _m13 = elements[2];
			_m21 = elements[3]; _m22 = elements[4]; _m23 = elements[5];
			_m31 = elements[6]; _m32 = elements[7]; _m33 = elements[8];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix3D"/> structure with the specified values.
		/// </summary>
		/// <param name="column1">A <see cref="Vector3D"/> instance holding values for the first column.</param>
		/// <param name="column2">A <see cref="Vector3D"/> instance holding values for the second column.</param>
		/// <param name="column3">A <see cref="Vector3D"/> instance holding values for the third column.</param>
		public Matrix3D(Vector3D column1, Vector3D column2, Vector3D column3)
		{
			_m11 = column1.X; _m12 = column2.X; _m13 = column3.X; 
			_m21 = column1.Y; _m22 = column2.Y; _m23 = column3.Y; 
			_m31 = column1.Z; _m32 = column2.Z; _m33 = column3.Z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix3D"/> class using a given matrix.
		/// </summary>
		public Matrix3D(Matrix3D m)
		{
			_m11 = m.M11; _m12 = m.M12; _m13 = m.M13;
			_m21 = m.M21; _m22 = m.M22; _m23 = m.M23;
			_m31 = m.M31; _m32 = m.M32; _m33 = m.M33;
		}
		#endregion

		#region Constants
		/// <summary>
		/// 3-dimentional double-precision floating point zero matrix.
		/// </summary>
		public static readonly Matrix3D Zero = new Matrix3D(0, 0, 0, 0, 0, 0, 0, 0, 0);
		/// <summary>
		/// 3-dimentional double-precision floating point identity matrix.
		/// </summary>
		public static readonly Matrix3D Identity = new Matrix3D(
			1, 0, 0,
			0, 1, 0,
			0, 0, 1
			);
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the value of the [1,1] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M11
		{
			get { return _m11; }
			set { _m11 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [1,2] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M12
		{
			get { return _m12; }
			set { _m12 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [1,3] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M13
		{
			get { return _m13; }
			set { _m13 = value; }
		}


		/// <summary>
		/// Gets or sets the value of the [2,1] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M21
		{
			get { return _m21; }
			set { _m21 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [2,2] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M22
		{
			get { return _m22; }
			set { _m22 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [2,3] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M23
		{
			get { return _m23; }
			set { _m23 = value; }
		}


		/// <summary>
		/// Gets or sets the value of the [3,1] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M31
		{
			get { return _m31; }
			set { _m31 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [3,2] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M32
		{
			get { return _m32; }
			set { _m32 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [3,3] matrix element.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double M33
		{
			get { return _m33; }
			set { _m33 = value; }
		}

		/// <summary>
		/// Gets the matrix's trace value.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		public double Trace
		{
			get
			{
				return _m11 + _m22 + _m33;
			}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Matrix3D"/> object.
		/// </summary>
		/// <returns>The <see cref="Matrix3D"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Matrix3D(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Matrix3D"/> object.
		/// </summary>
		/// <returns>The <see cref="Matrix3D"/> object this method creates.</returns>
		public Matrix3D Clone()
		{
			return new Matrix3D(this);
		}
		#endregion

		#region Public Static Parse Methods
		private const string regularExp =
			@"3x3\s*\[(?<m11>.*),(?<m12>.*),(?<m13>.*),(?<m21>.*),(?<m22>.*),(?<m23>.*),(?<m31>.*),(?<m32>.*),(?<m33>.*)\]";
		/// <summary>
		/// Converts the specified string to its <see cref="Matrix3D"/> equivalent.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="Matrix3D"/>.</param>
		/// <returns>A <see cref="Matrix3D"/> that represents the vector specified by the <paramref name="value"/> parameter.</returns>
		/// <remarks>
		/// The string should be in the following form: "3x3..matrix elements..>".<br/>
		/// Exmaple : "3x3[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]"
		/// </remarks>
		public static Matrix3D Parse(string value)
		{
			Regex r = new Regex(regularExp, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Match m = r.Match(value);
			if (m.Success)
			{
				return new Matrix3D(
					double.Parse(m.Result("${m11}")),
					double.Parse(m.Result("${m12}")),
					double.Parse(m.Result("${m13}")),

					double.Parse(m.Result("${m21}")),
					double.Parse(m.Result("${m22}")),
					double.Parse(m.Result("${m23}")),

					double.Parse(m.Result("${m31}")),
					double.Parse(m.Result("${m32}")),
					double.Parse(m.Result("${m33}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		/// <summary>
		/// Converts the specified string to its <see cref="Matrix3D"/> equivalent.
		/// A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="Matrix3D"/>.</param>
		/// <param name="result">
		/// When this method returns, if the conversion succeeded,
		/// contains a <see cref="Matrix3D"/> representing the vector specified by <paramref name="value"/>.
		/// </param>
		/// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
		/// <remarks>
		/// The string should be in the following form: "3x3..matrix elements..>".<br/>
		/// Exmaple : "3x3[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]"
		/// </remarks>
		public static bool TryParse(string value, out Matrix3D result)
		{
			Regex r = new Regex(regularExp, RegexOptions.Singleline);
			Match m = r.Match(value);
			if (m.Success)
			{
				result = new Matrix3D(
					double.Parse(m.Result("${m11}")),
					double.Parse(m.Result("${m12}")),
					double.Parse(m.Result("${m13}")),

					double.Parse(m.Result("${m21}")),
					double.Parse(m.Result("${m22}")),
					double.Parse(m.Result("${m23}")),

					double.Parse(m.Result("${m31}")),
					double.Parse(m.Result("${m32}")),
					double.Parse(m.Result("${m33}"))
					);

				return true;
			}

			result = Matrix3D.Zero;
			return false;
		}
		#endregion

		#region Public Static Matrix Arithmetics
		/// <summary>
		/// Adds two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the sum.</returns>
		public static Matrix3D Add(Matrix3D left, Matrix3D right)
		{
			return new Matrix3D(
				left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, 
				left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23, 
				left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33
				);
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the sum.</returns>
		public static Matrix3D Add(Matrix3D matrix, double scalar)
		{
			return new Matrix3D(
				matrix.M11 + scalar, matrix.M12 + scalar, matrix.M13 + scalar, 
				matrix.M21 + scalar, matrix.M22 + scalar, matrix.M23 + scalar, 
				matrix.M31 + scalar, matrix.M32 + scalar, matrix.M33 + scalar
				);
		}
		/// <summary>
		/// Adds two matrices and put the result in a third matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="result">A <see cref="Matrix3D"/> instance to hold the result.</param>
		public static void Add(Matrix3D left, Matrix3D right, ref Matrix3D result)
		{
			result.M11 = left.M11 + right.M11;
			result.M12 = left.M12 + right.M12;
			result.M13 = left.M13 + right.M13;

			result.M21 = left.M21 + right.M21;
			result.M22 = left.M22 + right.M22;
			result.M23 = left.M23 + right.M23;

			result.M31 = left.M31 + right.M31;
			result.M32 = left.M32 + right.M32;
			result.M33 = left.M33 + right.M33;
		}
		/// <summary>
		/// Adds a matrix and a scalar and put the result in a third matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <param name="result">A <see cref="Matrix3D"/> instance to hold the result.</param>
		public static void Add(Matrix3D matrix, double scalar, ref Matrix3D result)
		{
			result.M11 = matrix.M11 + scalar;
			result.M12 = matrix.M12 + scalar;
			result.M13 = matrix.M13 + scalar;

			result.M21 = matrix.M21 + scalar;
			result.M22 = matrix.M22 + scalar;
			result.M23 = matrix.M23 + scalar;

			result.M31 = matrix.M31 + scalar;
			result.M32 = matrix.M32 + scalar;
			result.M33 = matrix.M33 + scalar;
		}
		/// <summary>
		/// Subtracts a matrix from a matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance to subtract from.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance to subtract.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the difference.</returns>
		/// <remarks>result[x][y] = left[x][y] - right[x][y]</remarks>
		public static Matrix3D Subtract(Matrix3D left, Matrix3D right)
		{
			return new Matrix3D(
				left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13,
				left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23,
				left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33
				);
		}
		/// <summary>
		/// Subtracts a scalar from a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the difference.</returns>
		public static Matrix3D Subtract(Matrix3D matrix, double scalar)
		{
			return new Matrix3D(
				matrix.M11 - scalar, matrix.M12 - scalar, matrix.M13 - scalar,
				matrix.M21 - scalar, matrix.M22 - scalar, matrix.M23 - scalar,
				matrix.M31 - scalar, matrix.M32 - scalar, matrix.M33 - scalar
				);
		}
		/// <summary>
		/// Subtracts a matrix from a matrix and put the result in a third matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance to subtract from.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance to subtract.</param>
		/// <param name="result">A <see cref="Matrix3D"/> instance to hold the result.</param>
		/// <remarks>result[x][y] = left[x][y] - right[x][y]</remarks>
		public static void Subtract(Matrix3D left, Matrix3D right, ref Matrix3D result)
		{
			result.M11 = left.M11 - right.M11;
			result.M12 = left.M12 - right.M12;
			result.M13 = left.M13 - right.M13;

			result.M21 = left.M21 - right.M21;
			result.M22 = left.M22 - right.M22;
			result.M23 = left.M23 - right.M23;

			result.M31 = left.M31 - right.M31;
			result.M32 = left.M32 - right.M32;
			result.M33 = left.M33 - right.M33;
		}
		/// <summary>
		/// Subtracts a scalar from a matrix and put the result in a third matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <param name="result">A <see cref="Matrix3D"/> instance to hold the result.</param>
		public static void Subtract(Matrix3D matrix, double scalar, ref Matrix3D result)
		{
			result.M11 = matrix.M11 - scalar;
			result.M12 = matrix.M12 - scalar;
			result.M13 = matrix.M13 - scalar;

			result.M21 = matrix.M21 - scalar;
			result.M22 = matrix.M22 - scalar;
			result.M23 = matrix.M23 - scalar;

			result.M31 = matrix.M31 - scalar;
			result.M32 = matrix.M32 - scalar;
			result.M33 = matrix.M33 - scalar;
		}
		/// <summary>
		/// Multiplies two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the result.</returns>
		public static Matrix3D Multiply(Matrix3D left, Matrix3D right)
		{
			return new Matrix3D(
				left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31,
				left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32,
				left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33,

				left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31,
				left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32,
				left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33,

				left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31,
				left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32,
				left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33
				);
		}
		/// <summary>
		/// Multiplies two matrices and put the result in a third matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="result">A <see cref="Matrix3D"/> instance to hold the result.</param>
		public static void Multiply(Matrix3D left, Matrix3D right, ref Matrix3D result)
		{
			result.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31;
			result.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32;
			result.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33;

			result.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31;
			result.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32;
			result.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33;

			result.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31;
			result.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32;
			result.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33;
		}
		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the result.</returns>
		public static Vector3D Transform(Matrix3D matrix, Vector3D vector)
		{
			return new Vector3D(
				(matrix.M11 * vector.X) + (matrix.M12 * vector.Y) + (matrix.M13 * vector.Z),
				(matrix.M21 * vector.X) + (matrix.M22 * vector.Y) + (matrix.M23 * vector.Z),
				(matrix.M31 * vector.X) + (matrix.M32 * vector.Y) + (matrix.M33 * vector.Z));
		}
		/// <summary>
		/// Transforms a given vector by a matrix and put the result in a vector.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <param name="result">A <see cref="Vector3D"/> instance to hold the result.</param>
		public static void Transform(Matrix3D matrix, Vector3D vector, ref Vector3D result)
		{
			result.X = (matrix.M11 * vector.X) + (matrix.M12 * vector.Y) + (matrix.M13 * vector.Z);
			result.Y = (matrix.M21 * vector.X) + (matrix.M22 * vector.Y) + (matrix.M23 * vector.Z);
			result.Z = (matrix.M31 * vector.X) + (matrix.M32 * vector.Y) + (matrix.M33 * vector.Z);
		}
		/// <summary>
		/// Transposes a matrix.
		/// </summary>
		/// <param name="m">A <see cref="Matrix3D"/> instance.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the transposed matrix.</returns>
		public static Matrix3D Transpose(Matrix3D m)
		{
			Matrix3D t = new Matrix3D(m);
			t.Transpose();
			return t;
		}

        public static Matrix3D Inverse(Matrix3D m)
        {
            double d = 1.0/m.GetDeterminant();
            
            return new Matrix3D(
                d * (m._m33 * m._m22 - m._m32 * m._m23)
                , -d * (m._m33 * m._m12 - m._m32 * m._m13)
                , d * (m._m23 * m._m12 - m._m22 * m._m13)

                , -d * (m._m33 * m._m21 - m._m31 * m._m23)
                , d * (m._m33 * m._m11 - m._m31 * m._m13)
                , -d * (m._m23 * m._m11 - m._m21 * m._m13)

                , d * (m._m32 * m._m21 - m._m31 * m._m22)
                , -d * (m._m32 * m._m11 - m._m31 * m._m12)
                , d * (m._m22 * m._m11 - m._m21 * m._m12));
        }
		#endregion

		#region System.Object overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return
				_m11.GetHashCode() ^ _m12.GetHashCode() ^ _m13.GetHashCode() ^
				_m21.GetHashCode() ^ _m22.GetHashCode() ^ _m23.GetHashCode() ^ 
				_m31.GetHashCode() ^ _m32.GetHashCode() ^ _m33.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Matrix3D"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix3D)
			{
				Matrix3D m = (Matrix3D)obj;
				return
					(_m11 == m.M11) && (_m12 == m.M12) && (_m13 == m.M13) &&
					(_m21 == m.M21) && (_m22 == m.M22) && (_m23 == m.M23) &&
					(_m31 == m.M31) && (_m32 == m.M32) && (_m33 == m.M33);
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "3x3[{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}]",
				_m11, _m12, _m13, _m21, _m22, _m23, _m31, _m32, _m33);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Calculates the determinant value of the matrix.
		/// </summary>
		/// <returns>The determinant value of the matrix.</returns>
		public double GetDeterminant()
		{
			// rule of Sarrus
			return 
				_m11 * _m22 * _m33 + _m12 * _m23 * _m31 + _m13 * _m21 * _m32 -
				_m13 * _m22 * _m31 - _m11 * _m23 * _m32 - _m12 * _m21 * _m33;
		}
		/// <summary>
		/// Transposes this matrix.
		/// </summary>
		public void Transpose()
		{
			MathFunctions.Swap<double>(ref _m12, ref _m21);
			MathFunctions.Swap<double>(ref _m13, ref _m31);
			MathFunctions.Swap<double>(ref _m23, ref _m32);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified matrices are equal.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <returns><see langword="true"/> if the two matrices are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(Matrix3D left, Matrix3D right)
		{
			return ValueType.Equals(left, right);
		}
		/// <summary>
		/// Tests whether two specified matrices are not equal.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <returns><see langword="true"/> if the two matrices are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(Matrix3D left, Matrix3D right)
		{
			return !ValueType.Equals(left, right);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the sum.</returns>
		public static Matrix3D operator +(Matrix3D left, Matrix3D right)
		{
			return Matrix3D.Add(left, right);;
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the sum.</returns>
		public static Matrix3D operator +(Matrix3D matrix, double scalar)
		{
			return Matrix3D.Add(matrix, scalar);
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the sum.</returns>
		public static Matrix3D operator +(double scalar, Matrix3D matrix)
		{
			return Matrix3D.Add(matrix, scalar);
		}
		/// <summary>
		/// Subtracts a matrix from a matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the difference.</returns>
		public static Matrix3D operator -(Matrix3D left, Matrix3D right)
		{
			return Matrix3D.Subtract(left, right);;
		}
		/// <summary>
		/// Subtracts a scalar from a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the difference.</returns>
		public static Matrix3D operator -(Matrix3D matrix, double scalar)
		{
			return Matrix3D.Subtract(matrix, scalar);
		}
		/// <summary>
		/// Multiplies two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="right">A <see cref="Matrix3D"/> instance.</param>
		/// <returns>A new <see cref="Matrix3D"/> instance containing the result.</returns>
		public static Matrix3D operator *(Matrix3D left, Matrix3D right)
		{
			return Matrix3D.Multiply(left, right);
		}
		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix3D"/> instance.</param>
		/// <param name="vector">A <see cref="Vector3D"/> instance.</param>
		/// <returns>A new <see cref="Vector3D"/> instance containing the result.</returns>
		public static Vector3D operator *(Matrix3D matrix, Vector3D vector)
		{
			return Matrix3D.Transform(matrix, vector);
		}
		#endregion

		#region Indexing Operators
		/// <summary>
		/// Indexer allowing to access the matrix elements by an index
		/// where index = 2*row + column.
		/// </summary>
		public unsafe double this[int index]
		{
			get
			{
				if (index < 0 || index >= 9)
					throw new IndexOutOfRangeException("Invalid matrix index!");

				fixed (double* f = &_m11)
				{
					return *(f + index);
				}
			}
			set
			{
				if (index < 0 || index >= 9)
					throw new IndexOutOfRangeException("Invalid matrix index!");

				fixed (double* f = &_m11)
				{
					*(f + index) = value;
				}
			}
		}
		/// <summary>
		/// Indexer allowing to access the matrix elements by row and column.
		/// </summary>
		public double this[int row, int column]
		{
			get
			{
				return this[(row-1) * 3 + (column-1)];
			}
			set
			{
				this[(row - 1) * 3 + (column - 1)] = value;
			}
		}
		#endregion
	}
}
