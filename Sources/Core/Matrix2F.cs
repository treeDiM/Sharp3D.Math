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
	/// Represents a 2-dimentional single-precision floating point matrix.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public struct Matrix2F : ICloneable
	{
		#region Private Fields
		private float _m11, _m12;
		private float _m21, _m22;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix2F"/> structure with the specified values.
		/// </summary>
		public Matrix2F(
			float m11, float m12, 
			float m21, float m22
			)
		{
			_m11 = m11; _m12 = m12;
			_m21 = m21; _m22 = m22; 
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix2F"/> structure with the specified values.
		/// </summary>
		/// <param name="elements">An array containing the matrix values in row-major order.</param>
		public Matrix2F(float[] elements)
		{
			Debug.Assert(elements != null);
			Debug.Assert(elements.Length >= 4);

			_m11 = elements[0]; _m12 = elements[1];
			_m21 = elements[2]; _m22 = elements[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4F"/> structure with the specified values.
		/// </summary>
		/// <param name="elements">An array containing the matrix values in row-major order.</param>
		public Matrix2F(List<float> elements)
		{
			Debug.Assert(elements != null);
			Debug.Assert(elements.Count >= 4);

			_m11 = elements[0]; _m12 = elements[1];
			_m21 = elements[2]; _m22 = elements[3];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix2F"/> structure with the specified values.
		/// </summary>
		/// <param name="column1">A <see cref="Vector2F"/> instance holding values for the first column.</param>
		/// <param name="column2">A <see cref="Vector2F"/> instance holding values for the second column.</param>
		public Matrix2F(Vector2F column1, Vector2F column2)
		{
			_m11 = column1.X; _m12 = column2.X;
			_m21 = column1.Y; _m22 = column2.Y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix2F"/> class using a given matrix.
		/// </summary>
		public Matrix2F(Matrix2F m)
		{
			_m11 = m.M11; _m12 = m.M12;
			_m21 = m.M21; _m22 = m.M22;
		}
		#endregion

		#region Constants
		/// <summary>
		/// 4-dimentional single-precision floating point zero matrix.
		/// </summary>
		public static readonly Matrix2F Zero = new Matrix2F(0, 0, 0, 0);
		/// <summary>
		/// 4-dimentional single-precision floating point identity matrix.
		/// </summary>
		public static readonly Matrix2F Identity = new Matrix2F(
			1, 0,
			0, 1
			);
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the value of the [1,1] matrix element.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		public float M11
		{
			get { return _m11; }
			set { _m11 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [1,2] matrix element.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		public float M12
		{
			get { return _m12; }
			set { _m12 = value; }
		}

		/// <summary>
		/// Gets or sets the value of the [2,1] matrix element.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		public float M21
		{
			get { return _m21; }
			set { _m21 = value; }
		}
		/// <summary>
		/// Gets or sets the value of the [2,2] matrix element.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		public float M22
		{
			get { return _m22; }
			set { _m22 = value; }
		}

		/// <summary>
		/// Gets the matrix's trace value.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		public float Trace
		{
			get
			{
				return _m11 + _m22;
			}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Matrix2F"/> object.
		/// </summary>
		/// <returns>The <see cref="Matrix2F"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Matrix2F(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Matrix2F"/> object.
		/// </summary>
		/// <returns>The <see cref="Matrix2F"/> object this method creates.</returns>
		public Matrix2F Clone()
		{
			return new Matrix2F(this);
		}
		#endregion

		#region Public Static Parse Methods
		private const string regularExp =
			@"2x2\s*\[(?<m11>.*),(?<m12>.*),(?<m21>.*),(?<m22>.*)\]";
		/// <summary>
		/// Converts the specified string to its <see cref="Matrix2F"/> equivalent.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="Matrix2F"/>.</param>
		/// <returns>A <see cref="Matrix2F"/> that represents the vector specified by the <paramref name="value"/> parameter.</returns>
		/// <remarks>
		/// The string should be in the following form: "2x2..matrix elements..>".<br/>
		/// Exmaple : "2x2[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]"
		/// </remarks>
		public static Matrix2F Parse(string value)
		{
			Regex r = new Regex(regularExp, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
			Match m = r.Match(value);
			if (m.Success)
			{
				return new Matrix2F(
					float.Parse(m.Result("${m11}")),
					float.Parse(m.Result("${m12}")),

					float.Parse(m.Result("${m21}")),
					float.Parse(m.Result("${m22}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		/// <summary>
		/// Converts the specified string to its <see cref="Matrix2F"/> equivalent.
		/// A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="Matrix2F"/>.</param>
		/// <param name="result">
		/// When this method returns, if the conversion succeeded,
		/// contains a <see cref="Matrix2F"/> representing the vector specified by <paramref name="value"/>.
		/// </param>
		/// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
		/// <remarks>
		/// The string should be in the following form: "2x2..matrix elements..>".<br/>
		/// Exmaple : "2x2[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]"
		/// </remarks>
		public static bool TryParse(string value, out Matrix2F result)
		{
			Regex r = new Regex(regularExp, RegexOptions.Singleline);
			Match m = r.Match(value);
			if (m.Success)
			{
				result = new Matrix2F(
					float.Parse(m.Result("${m11}")),
					float.Parse(m.Result("${m12}")),

					float.Parse(m.Result("${m21}")),
					float.Parse(m.Result("${m22}"))
					);

				return true;
			}

			result = Matrix2F.Zero;
			return false;
		}
		#endregion

		#region Public Static Matrix Arithmetics
		/// <summary>
		/// Adds two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the sum.</returns>
		public static Matrix2F Add(Matrix2F left, Matrix2F right)
		{
			return new Matrix2F(
				left.M11 + right.M11, left.M12 + right.M12, 
				left.M21 + right.M21, left.M22 + right.M22
				);
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the sum.</returns>
		public static Matrix2F Add(Matrix2F matrix, float scalar)
		{
			return new Matrix2F(
				matrix.M11 + scalar, matrix.M12 + scalar, 
				matrix.M21 + scalar, matrix.M22 + scalar 
				);
		}
		/// <summary>
		/// Adds two matrices and put the result in a third matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="result">A <see cref="Matrix2F"/> instance to hold the result.</param>
		public static void Add(Matrix2F left, Matrix2F right, ref Matrix2F result)
		{
			result.M11 = left.M11 + right.M11;
			result.M12 = left.M12 + right.M12;

			result.M21 = left.M21 + right.M21;
			result.M22 = left.M22 + right.M22;
		}
		/// <summary>
		/// Adds a matrix and a scalar and put the result in a third matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point number.</param>
		/// <param name="result">A <see cref="Matrix2F"/> instance to hold the result.</param>
		public static void Add(Matrix2F matrix, float scalar, ref Matrix2F result)
		{
			result.M11 = matrix.M11 + scalar;
			result.M12 = matrix.M12 + scalar;

			result.M21 = matrix.M21 + scalar;
			result.M22 = matrix.M22 + scalar;
		}
		/// <summary>
		/// Subtracts a matrix from a matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance to subtract from.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance to subtract.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the difference.</returns>
		/// <remarks>result[x][y] = left[x][y] - right[x][y]</remarks>
		public static Matrix2F Subtract(Matrix2F left, Matrix2F right)
		{
			return new Matrix2F(
				left.M11 - right.M11, left.M12 - right.M12, 
				left.M21 - right.M21, left.M22 - right.M22
				);
		}
		/// <summary>
		/// Subtracts a scalar from a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the difference.</returns>
		public static Matrix2F Subtract(Matrix2F matrix, float scalar)
		{
			return new Matrix2F(
				matrix.M11 - scalar, matrix.M12 - scalar,
				matrix.M21 - scalar, matrix.M22 - scalar
				);
		}
		/// <summary>
		/// Subtracts a matrix from a matrix and put the result in a third matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance to subtract from.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance to subtract.</param>
		/// <param name="result">A <see cref="Matrix2F"/> instance to hold the result.</param>
		/// <remarks>result[x][y] = left[x][y] - right[x][y]</remarks>
		public static void Subtract(Matrix2F left, Matrix2F right, ref Matrix2F result)
		{
			result.M11 = left.M11 - right.M11;
			result.M12 = left.M12 - right.M12;

			result.M21 = left.M21 - right.M21;
			result.M22 = left.M22 - right.M22;
		}
		/// <summary>
		/// Subtracts a scalar from a matrix and put the result in a third matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point number.</param>
		/// <param name="result">A <see cref="Matrix2F"/> instance to hold the result.</param>
		public static void Subtract(Matrix2F matrix, float scalar, ref Matrix2F result)
		{
			result.M11 = matrix.M11 - scalar;
			result.M12 = matrix.M12 - scalar;

			result.M21 = matrix.M21 - scalar;
			result.M22 = matrix.M22 - scalar;
		}
		/// <summary>
		/// Multiplies two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the result.</returns>
		public static Matrix2F Multiply(Matrix2F left, Matrix2F right)
		{
			return new Matrix2F(
				left.M11 * right.M11 + left.M12 * right.M21,
				left.M11 * right.M12 + left.M12 * right.M22,
				left.M21 * right.M11 + left.M22 * right.M21,
				left.M21 * right.M12 + left.M22 * right.M22
				);
		}
		/// <summary>
		/// Multiplies two matrices and put the result in a third matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="result">A <see cref="Matrix2F"/> instance to hold the result.</param>
		public static void Multiply(Matrix2F left, Matrix2F right, ref Matrix2F result)
		{
			result.M11 = left.M11 * right.M11 + left.M12 * right.M21;
			result.M12 = left.M11 * right.M12 + left.M12 * right.M22;

			result.M21 = left.M21 * right.M11 + left.M22 * right.M21;
			result.M22 = left.M21 * right.M12 + left.M22 * right.M22;
		}
		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="vector">A <see cref="Vector2F"/> instance.</param>
		/// <returns>A new <see cref="Vector2F"/> instance containing the result.</returns>
		public static Vector2F Transform(Matrix2F matrix, Vector2F vector)
		{
			return new Vector2F(
				(matrix.M11 * vector.X) + (matrix.M12 * vector.Y),
				(matrix.M21 * vector.X) + (matrix.M22 * vector.Y));
		}
		/// <summary>
		/// Transforms a given vector by a matrix and put the result in a vector.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="vector">A <see cref="Vector2F"/> instance.</param>
		/// <param name="result">A <see cref="Vector2F"/> instance to hold the result.</param>
		public static void Transform(Matrix2F matrix, Vector2F vector, ref Vector2F result)
		{
			result.X = (matrix.M11 * vector.X) + (matrix.M12 * vector.Y);
			result.Y = (matrix.M21 * vector.X) + (matrix.M22 * vector.Y);
		}
		/// <summary>
		/// Transposes a matrix.
		/// </summary>
		/// <param name="m">A <see cref="Matrix2F"/> instance.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the transposed matrix.</returns>
		public static Matrix2F Transpose(Matrix2F m)
		{
			Matrix2F t = new Matrix2F(m);
			t.Transpose();
			return t;
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
				_m11.GetHashCode() ^ _m12.GetHashCode() ^
				_m21.GetHashCode() ^ _m22.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Matrix2F"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix2F)
			{
				Matrix2F m = (Matrix2F)obj;
				return
					(_m11 == m.M11) && (_m12 == m.M12) &&
					(_m21 == m.M21) && (_m22 == m.M22);
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "2x2[{0}, {1}, {2}, {3}]",
				_m11, _m12, _m21, _m22);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Calculates the determinant value of the matrix.
		/// </summary>
		/// <returns>The determinant value of the matrix.</returns>
		public float GetDeterminant()
		{
			return (_m11 * _m22) - (_m12 * _m21);
		}
		/// <summary>
		/// Transposes this matrix.
		/// </summary>
		public void Transpose()
		{
			MathFunctions.Swap<float>(ref _m12, ref _m21);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified matrices are equal.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <returns><see langword="true"/> if the two matrices are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(Matrix2F left, Matrix2F right)
		{
			return ValueType.Equals(left, right);
		}
		/// <summary>
		/// Tests whether two specified matrices are not equal.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <returns><see langword="true"/> if the two matrices are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(Matrix2F left, Matrix2F right)
		{
			return !ValueType.Equals(left, right);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the sum.</returns>
		public static Matrix2F operator +(Matrix2F left, Matrix2F right)
		{
			return Matrix2F.Add(left, right);;
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the sum.</returns>
		public static Matrix2F operator +(Matrix2F matrix, float scalar)
		{
			return Matrix2F.Add(matrix, scalar);
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the sum.</returns>
		public static Matrix2F operator +(float scalar, Matrix2F matrix)
		{
			return Matrix2F.Add(matrix, scalar);
		}
		/// <summary>
		/// Subtracts a matrix from a matrix.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the difference.</returns>
		public static Matrix2F operator -(Matrix2F left, Matrix2F right)
		{
			return Matrix2F.Subtract(left, right);;
		}
		/// <summary>
		/// Subtracts a scalar from a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point number.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the difference.</returns>
		public static Matrix2F operator -(Matrix2F matrix, float scalar)
		{
			return Matrix2F.Subtract(matrix, scalar);
		}
		/// <summary>
		/// Multiplies two matrices.
		/// </summary>
		/// <param name="left">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="right">A <see cref="Matrix2F"/> instance.</param>
		/// <returns>A new <see cref="Matrix2F"/> instance containing the result.</returns>
		public static Matrix2F operator *(Matrix2F left, Matrix2F right)
		{
			return Matrix2F.Multiply(left, right);;
		}
		/// <summary>
		/// Transforms a given vector by a matrix.
		/// </summary>
		/// <param name="matrix">A <see cref="Matrix2F"/> instance.</param>
		/// <param name="vector">A <see cref="Vector2F"/> instance.</param>
		/// <returns>A new <see cref="Vector2F"/> instance containing the result.</returns>
		public static Vector2F operator *(Matrix2F matrix, Vector2F vector)
		{
			return Matrix2F.Transform(matrix, vector);
		}
		#endregion

		#region Indexing Operators
		/// <summary>
		/// Indexer allowing to access the matrix elements by an index
		/// where index = 2*row + column.
		/// </summary>
		public unsafe float this[int index]
		{
			get
			{
				if (index < 0 || index >= 4)
					throw new IndexOutOfRangeException("Invalid matrix index!");

				fixed (float* f = &_m11)
				{
					return *(f + index);
				}
			}
			set
			{
				if (index < 0 || index >= 4)
					throw new IndexOutOfRangeException("Invalid matrix index!");

				fixed (float* f = &_m11)
				{
					*(f + index) = value;
				}
			}
		}
		/// <summary>
		/// Indexer allowing to access the matrix elements by row and column.
		/// </summary>
		public float this[int row, int column]
		{
			get
			{
				return this[(row-1) * 2 + (column-1)];
			}
			set
			{
				this[(row-1) * 2 + (column-1)] = value;
			}
		}
		#endregion
	}
}
