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
using System.Globalization;

#endregion

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents a complex single-precision floating point number.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ComplexF : ICloneable
	{
		#region Private Fields
		private float _real;
		private float _image;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ComplexF"/> class using given real and imaginary values.
		/// </summary>
		/// <param name="real">Real value.</param>
		/// <param name="imaginary">Imaginary value.</param>
		public ComplexF(float real, float imaginary)
		{
			_real = real;
			_image = imaginary;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ComplexF"/> class using values from a given complex instance.
		/// </summary>
		/// <param name="c">A complex number to get values from.</param>
		public ComplexF(ComplexF c)
		{
			_real = c.Real;
			_image = c.Imaginary;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the real value of the complex number.
		/// </summary>
		/// <value>The real value of this complex number.</value>
		public float Real
		{
			get { return _real; }
			set { _real = value; }
		}
		/// <summary>
		/// Gets or sets the imaginary value of the complex number.
		/// </summary>
		/// <value>The imaginary value of this complex number.</value>
		public float Imaginary
		{
			get { return _image; }
			set { _image = value; }
		}
		/// <summary>
		/// Gets a value indicating the complex number is real (Its imaginary value is zero).
		/// </summary>
		/// <value>A <see cref="bool"/>.</value>
		public bool IsReal
		{
			get
			{
				return (_image == 0.0);
			}
		}
		/// <summary>
		/// Gets a value indicating the complex number is imaginary (Its real value is zero).
		/// </summary>
		/// <value>A <see cref="bool"/>.</value>
		public bool IsImaginary
		{
			get
			{
				return (_real == 0.0);
			}
		}
		/// <summary>
		/// Gets the the modulus of the complex number.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		/// <remarks>See http://mathworld.wolfram.com/ComplexModulus.html for further details.</remarks>
		public float Modulus
		{
			get
			{
				return (float)System.Math.Sqrt(_real * _real + _image * _image);
			}
		}
		/// <summary>
		/// Gets the squared modulus of the complex number.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		/// <remarks>See http://mathworld.wolfram.com/ComplexModulus.html for further details.</remarks>
		public float ModulusSquared
		{
			get
			{
				return ((_real * _real) + (_image * _image));
			}
		}
		/// <summary>
		/// Gets or sets the argument of the complex number.
		/// </summary>
		/// <value>A single-precision floating-point number.</value>
		/// <remarks>See http://mathworld.wolfram.com/ComplexArgument.html for further details.</remarks>
		public float Argument
		{
			get
			{
				if ((_image == 0.0) && (_real < 0.0))
				{
					return (float)System.Math.PI;
				}

				if ((_image == 0.0) &&  (_real >= 0.0))
				{
					return 0.0f;
				}

				return (float)System.Math.Atan2(_image, _real);
			}
			set
			{
				float modulus = this.Modulus;
				_real = (float)System.Math.Cos(value) * modulus;
				_image = (float)System.Math.Sin(value) * modulus;
			}
		}
		/// <summary>
		/// Gets or sets the conjugate of the complex number. 
		/// </summary>
		/// <value>A <see cref="ComplexF"/> instance.</value>
		public ComplexF Conjugate
		{
			get
			{
				return new ComplexF(_real, -_image);
			}
			set
			{
				this = value.Conjugate;
			}
		}
		#endregion

		#region Constants
		/// <summary>
		///  A single-precision complex number that represents zero.
		/// </summary>
		public static readonly ComplexF Zero = new ComplexF(0, 0);
		/// <summary>
		///  A single-precision complex number that represents one.
		/// </summary>
		public static readonly ComplexF One = new ComplexF(1, 0);
		/// <summary>
		///  A single-precision complex number that represents the squere root of (-1).
		/// </summary>
		public static readonly ComplexF I = new ComplexF(0, 1);
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="ComplexF"/> object.
		/// </summary>
		/// <returns>The <see cref="ComplexF"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new ComplexF(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="ComplexF"/> object.
		/// </summary>
		/// <returns>The <see cref="ComplexF"/> object this method creates.</returns>
		public ComplexF Clone()
		{
			return new ComplexF(this);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="ComplexF"/> equivalent.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="ComplexF"/>.</param>
		/// <returns>A <see cref="ComplexF"/> that represents the complex number specified by the <paramref name="s"/> parameter.</returns>
		public static ComplexF Parse(string value)
		{
			Regex r = new Regex(@"\((?<real>.*),(?<imaginary>.*)\)", RegexOptions.None);
			Match m = r.Match(value);
			if (m.Success)
			{
				return new ComplexF(
					float.Parse(m.Result("${real}")),
					float.Parse(m.Result("${imaginary}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		/// <summary>
		/// Converts the specified string to its <see cref="ComplexF"/> equivalent.
		/// A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="ComplexF"/>.</param>
		/// <param name="result">
		/// When this method returns, if the conversion succeeded,
		/// contains a <see cref="ComplexF"/> representing the complex number specified by <paramref name="value"/>.
		/// </param>
		/// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
		public static bool TryParse(string value, out ComplexF result)
		{
			Regex r = new Regex(@"\((?<real>.*),(?<imaginary>.*)\)", RegexOptions.None);
			Match m = r.Match(value);
			if (m.Success)
			{
				result = new ComplexF(
					float.Parse(m.Result("${real}")),
					float.Parse(m.Result("${imaginary}"))
					);

				return true;
			}

			result = ComplexF.Zero;
			return false;
		}
		#endregion

		#region Public Static Complex Arithmetics
		/// <summary>
		/// Adds two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexAddition.html for further details.</remarks>
		public static ComplexF Add(ComplexF left, ComplexF right)
		{
			return new ComplexF(left.Real + right.Real, left.Imaginary + right.Imaginary);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF Add(ComplexF complex, float scalar)
		{
			return new ComplexF(complex.Real + scalar, complex.Imaginary);
		}
		/// <summary>
		/// Adds two complex numbers and put the result in the third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexAddition.html for further details.</remarks>
		public static void Add(ComplexF left, ComplexF right, ref ComplexF result)
		{
			result.Real = left.Real + right.Real;
			result.Imaginary = left.Imaginary + right.Imaginary;
		}
		/// <summary>
		/// Adds a complex number and a scalar and put the result into another complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Add(ComplexF complex, float scalar, ref ComplexF result)
		{
			result.Real = complex.Real + scalar;
			result.Imaginary = complex.Imaginary;
		}

		/// <summary>
		/// Subtracts a complex from a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexSubtraction.html for further details.</remarks>
		public static ComplexF Subtract(ComplexF left, ComplexF right)
		{
			return new ComplexF(left.Real - right.Real, left.Imaginary - right.Imaginary);
		}
		/// <summary>
		/// Subtracts a scalar from a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF Subtract(ComplexF complex, float scalar)
		{
			return new ComplexF(complex.Real - scalar, complex.Imaginary);
		}
		/// <summary>
		/// Subtracts a complex from a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF Subtract(float scalar, ComplexF complex)
		{
			return new ComplexF(scalar - complex.Real, complex.Imaginary);
		}
		/// <summary>
		/// Subtracts a complex from a complex and put the result in the third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexSubtraction.html for further details.</remarks>
		public static void Subtract(ComplexF left, ComplexF right, ref ComplexF result)
		{
			result.Real = left.Real - right.Real;
			result.Imaginary = left.Imaginary - right.Imaginary;
		}
		/// <summary>
		/// Subtracts a scalar from a complex and put the result into another complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Subtract(ComplexF complex, float scalar, ref ComplexF result)
		{
			result.Real = complex.Real - scalar;
			result.Imaginary = complex.Imaginary;
		}
		/// <summary>
		/// Subtracts a complex from a scalar and put the result into another complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Subtract(float scalar, ComplexF complex, ref ComplexF result)
		{
			result.Real = scalar - complex.Real;
			result.Imaginary = complex.Imaginary;
		}

		/// <summary>
		/// Multiplies two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexMultiplication.html for further details.</remarks>
		public static ComplexF Multiply(ComplexF left, ComplexF right)
		{
			// (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
			float x = left.Real, y = left.Imaginary;
			float u = right.Real, v = right.Imaginary;

			return new ComplexF(x * u - y * v, x * v + y * u);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Multiply(ComplexF complex, float scalar)
		{
			return new ComplexF(complex.Real * scalar, complex.Imaginary * scalar);
		}
		/// <summary>
		/// Multiplies two complex numbers and put the result in a third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexMultiplication.html for further details.</remarks>
		public static void Multiply(ComplexF left, ComplexF right, ref ComplexF result)
		{
			// (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
			float x = left.Real, y = left.Imaginary;
			float u = right.Real, v = right.Imaginary;

			result.Real = x * u - y * v;
			result.Imaginary = x * v + y * u;
		}
		/// <summary>
		/// Multiplies a complex by a scalar and put the result into another complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Multiply(ComplexF complex, float scalar, ref ComplexF result)
		{
			result.Real = complex.Real * scalar;
			result.Imaginary = complex.Imaginary * scalar;
		}
		
		/// <summary>
		/// Divides a complex by a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexFivision.html for further details.</remarks>
		public static ComplexF Divide(ComplexF left, ComplexF right)
		{
			float x = left.Real, y = left.Imaginary;
			float u = right.Real, v = right.Imaginary;
			float modulusSquared = u * u + v * v;

			if (modulusSquared == 0)
			{
				throw new DivideByZeroException();
			}

			float invModulusSquared = 1 / modulusSquared;

			return new ComplexF(
				(x * u + y * v) * invModulusSquared,
				(y * u - x * v) * invModulusSquared);
		}
		/// <summary>
		/// Divides a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Divide(ComplexF complex, float scalar)
		{
			if (scalar == 0)
			{
				throw new DivideByZeroException();
			}

			return new ComplexF(
				complex.Real / scalar,
				complex.Imaginary / scalar);
		}
		/// <summary>
		/// Divides a scalar by a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Divide(float scalar, ComplexF complex)
		{
			if ((complex.Real == 0) || (complex.Imaginary == 0))
			{
				throw new DivideByZeroException();
			}

			return new ComplexF(
				scalar / complex.Real,
				scalar / complex.Imaginary);
		}
		/// <summary>
		/// Divides a complex by a complex and put the result in a third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexFivision.html for further details.</remarks>
		public static void Divide(ComplexF left, ComplexF right, ref ComplexF result)
		{
			float x = left.Real, y = left.Imaginary;
			float u = right.Real, v = right.Imaginary;
			float modulusSquared = u * u + v * v;

			if (modulusSquared == 0)
			{
				throw new DivideByZeroException();
			}

			float invModulusSquared = 1 / modulusSquared;

			result.Real = (x * u + y * v) * invModulusSquared;
			result.Imaginary = (y * u - x * v) * invModulusSquared;
		}
		/// <summary>
		/// Divides a complex by a scalar and put the result into another complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Divide(ComplexF complex, float scalar, ref ComplexF result)
		{
			if (scalar == 0)
			{
				throw new DivideByZeroException();
			}

			result.Real = complex.Real / scalar;
			result.Imaginary = complex.Imaginary / scalar;
		}
		/// <summary>
		/// Divides a scalar by a complex and put the result into another complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A single-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Divide(float scalar, ComplexF complex, ref ComplexF result)
		{
			if ((complex.Real == 0) || (complex.Imaginary == 0))
			{
				throw new DivideByZeroException();
			}

			result.Real = scalar / complex.Real;
			result.Imaginary = scalar / complex.Imaginary;
		}

		/// <summary>
		/// Negates a complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the negated values.</returns>
		public static ComplexF Negate(ComplexF complex)
		{
			return new ComplexF(-complex.Real, -complex.Imaginary);
		}

		/// <summary>
		/// Tests whether two complex numbers are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEqual(ComplexF left, ComplexF right)
		{
			return ApproxEqual(left, right, MathFunctions.EpsilonF);
		}
		/// <summary>
		/// Tests whether two complex numbers are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEqual(ComplexF left, ComplexF right, float tolerance)
		{
			return
				(
				(System.Math.Abs(left.Real - right.Real) <= tolerance) &&
				(System.Math.Abs(left.Imaginary - right.Imaginary) <= tolerance)
				);
		}
		#endregion

		#region Public Static Complex Special Functions
		/// <summary>
		/// Calculates the square root of a complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The square root of the complex number given in <paramref name="complex"/>.</returns>
		/// <remarks>See http://mathworld.wolfram.com/SquareRoot.html for further details.</remarks>
		public static ComplexF Sqrt(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if ((complex.Real == 0.0f) && (complex.Imaginary == 0.0f))
			{
				return result;
			}
			else if (complex.IsReal)
			{
				result.Real = (complex.Real > 0) ? (float)System.Math.Sqrt(complex.Real) : (float)System.Math.Sqrt(-complex.Real);
				result.Imaginary = 0.0f;
			}
			else
			{
				float modulus = complex.Modulus;

				result.Real = (float)System.Math.Sqrt(0.5f * (modulus + complex.Real));
				result.Imaginary = (float)System.Math.Sqrt(0.5f * (modulus - complex.Real));
				if (complex.Imaginary < 0.0f)
					result.Imaginary = -result.Imaginary;
			}

			return result;
		}
		/// <summary>
		/// Calculates the logarithm of a specified complex number.
		/// Calculates the natural (base e) logarithm of a specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The natural (base e) logarithm of the complex number given in <paramref name="complex"/>.</returns>
		public static ComplexF Log(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if ((complex.Real > 0.0) && (complex.Imaginary == 0.0))
			{
				result.Real = (float)System.Math.Log(complex.Real);
				result.Imaginary = 0.0f;
			}
			else if (complex.Real == 0.0f)
			{
				if (complex.Imaginary > 0.0f)
				{
					result.Real = (float)System.Math.Log(complex.Imaginary);
					result.Imaginary = (float)MathFunctions.HalfPI;
				}
				else
				{
					result.Real = (float)System.Math.Log(-(complex.Imaginary));
					result.Imaginary = -(float)MathFunctions.HalfPI;
				}
			}
			else
			{
				result.Real = (float)System.Math.Log(complex.Modulus);
				result.Imaginary = (float)System.Math.Atan2(complex.Imaginary, complex.Real);
			}

			return result;
		}
		/// <summary>
		/// Calculates the exponential of a specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The exponential of the complex number given in <paramref name="complex"/>.</returns>
		public static ComplexF Exp(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;
			float r = (float)System.Math.Exp(complex.Real);
			result.Real = r * (float)System.Math.Cos(complex.Imaginary);
			result.Imaginary = r * (float)System.Math.Sin(complex.Imaginary);

			return result;
		}
		/// <summary>
		/// Calculates a specified complex number raised by a specified power.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> representing the number to raise.</param>
		/// <param name="power">A <see cref="ComplexF"/> representing the power.</param>
		/// <returns>The complex <paramref name="complex"/> raised by <paramref name="power"/>.</returns>
		public static ComplexF Pow(ComplexF complex, ComplexF power)
		{
			return Exp(power * Log(complex));
		}
		/// <summary>
		/// Calculates the square of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The square of the given complex number.</returns>
		public static ComplexF Square(ComplexF complex)
		{
			if (complex.IsReal)
			{
				return new ComplexF(complex.Real * complex.Real, 0.0f);
			}

			float real = complex.Real;
			float imag = complex.Imaginary;
			return new ComplexF(real * real - imag * imag, 2.0f * real * imag);
		}
		#endregion

		#region Public Static Trigonometric Functions
		/// <summary>
		/// Calculates the sine of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The sine of <paramref name="complex"/>.</returns>
		public static ComplexF Sin(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if (complex.IsReal)
			{
				result.Real = (float)System.Math.Sin(complex.Real);
				result.Imaginary = 0.0f;
			}
			else
			{
				result.Real = (float)(System.Math.Sin(complex.Real) * System.Math.Cosh(complex.Imaginary));
				result.Imaginary = (float)(System.Math.Cos(complex.Real) * System.Math.Sinh(complex.Imaginary));
			}

			return result;
		}
		/// <summary>
		/// Calculates the cosine of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The cosine of <paramref name="complex"/>.</returns>
		public static ComplexF Cos(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if (complex.IsReal)
			{
				result.Real = (float)System.Math.Cos(complex.Real);
				result.Imaginary = 0.0f;
			}
			else
			{
				result.Real = (float)(System.Math.Cos(complex.Real) * System.Math.Cosh(complex.Imaginary));
				result.Imaginary = (float)(-System.Math.Sin(complex.Real) * System.Math.Sinh(complex.Imaginary));
			}

			return result;
		}
		/// <summary>
		/// Calculates the tangent of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The tangent of <paramref name="complex"/>.</returns>
		public static ComplexF Tan(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if (complex.IsReal)
			{
				result.Real = (float)System.Math.Tan(complex.Real);
				result.Imaginary = 0.0f;
			}
			else
			{
				float cosr = (float)System.Math.Cos(complex.Real);
				float sinhi = (float)System.Math.Sinh(complex.Imaginary);
				float denom = cosr * cosr + sinhi * sinhi;

				result.Real = (float)System.Math.Sin(complex.Real) * cosr / denom;
				result.Imaginary = sinhi * (float)System.Math.Cosh(complex.Imaginary) / denom;
			}

			return result;
		}
		/// <summary>
		/// Calculates the cotangent of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The cotangent of <paramref name="complex"/>.</returns>
		public static ComplexF Cot(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if (complex.IsReal)
			{
				result.Real = (float)MathFunctions.Cot(complex.Real);
			}
			else
			{
				float sinr = (float)System.Math.Sin(complex.Real);
				float sinhi = (float)System.Math.Sinh(complex.Imaginary);
				float denom = sinr * sinr + sinhi * sinhi;

				result.Real = (sinr * (float)System.Math.Cos(complex.Real)) / denom;
				result.Imaginary = (-sinhi * (float)System.Math.Cosh(complex.Imaginary)) / denom;
			}

			return result;
		}
		/// <summary>
		/// Calculates the secant of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The secant of <paramref name="complex"/>.</returns>
		public static ComplexF Sec(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if (complex.IsReal)
			{
				result.Real = MathFunctions.Sec(complex.Real);
			}
			else
			{
				float cosr = (float)MathFunctions.Cos(complex.Real);
				float sinhi = (float)MathFunctions.Sinh(complex.Imaginary);
				float denom = cosr * cosr + sinhi * sinhi;
				result.Real = (cosr * (float)MathFunctions.Cosh(complex.Imaginary)) / denom;
				result.Imaginary = ((float)MathFunctions.Sin(complex.Real) * sinhi) / denom;
			}

			return result;
		}
		/// <summary>
		/// Calculates the cosecant of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The cosecant of <paramref name="complex"/>.</returns>
		public static ComplexF Csc(ComplexF complex)
		{
			ComplexF result = ComplexF.Zero;

			if (complex.IsReal)
			{
				result.Real = (float)MathFunctions.Csc(complex.Real);
			}
			else
			{
				float sinr = (float)MathFunctions.Sin(complex.Real);
				float sinhi = (float)MathFunctions.Sinh(complex.Imaginary);
				float denom = sinr * sinr + sinhi * sinhi;
				result.Real = (sinr * (float)MathFunctions.Cosh(complex.Imaginary)) / denom;
				result.Imaginary = (-(float)MathFunctions.Cos(complex.Real) * sinhi) / denom;
			}

			return result;
		}
		#endregion

		#region Public Static Trigonometric Arcus Functions
		public static ComplexF Asin(ComplexF complex)
		{
			ComplexF result = 1 - ComplexF.Square(complex);
			result = ComplexF.Sqrt(result);
			result = result + (ComplexF.I * complex);
			result = ComplexF.Log(result);
			result = -ComplexF.I * result;

			return result;
		}
		public static ComplexF Acos(ComplexF complex)
		{
			ComplexF result = 1 - ComplexF.Square(complex);
			result = ComplexF.Sqrt(result);
			result = ComplexF.I * result;
			result = complex + result;
			result = ComplexF.Log(result);
			result = -ComplexF.I * result;
			return result;
		}
		public static ComplexF Atan(ComplexF complex)
		{
			ComplexF tmp = new ComplexF(-complex.Imaginary, complex.Real);
			return (new ComplexF(0.0f, 0.5f)) * (ComplexF.Log(1.0f - tmp) - ComplexF.Log(1.0f + tmp));
		}
		public static ComplexF Acot(ComplexF complex)
		{
			ComplexF tmp = new ComplexF(-complex.Imaginary, complex.Real);
			return (new ComplexF(0.0f, 0.5f)) * (ComplexF.Log(1.0f + tmp) - ComplexF.Log(1.0f - tmp)) + (float)MathFunctions.HalfPI;
		}
		public static ComplexF Asec(ComplexF complex)
		{
			ComplexF inverse = 1 / complex;
			return (-ComplexF.I) * ComplexF.Log(inverse + ComplexF.I * ComplexF.Sqrt(1 - ComplexF.Square(inverse)));
		}
		public static ComplexF Acsc(ComplexF complex)
		{
			ComplexF inverse = 1 / complex;
			return (-ComplexF.I) * ComplexF.Log(ComplexF.I * inverse + ComplexF.Sqrt(1 - ComplexF.Square(inverse)));
		}
		#endregion

		#region Public Static Trigonometric Hyperbolic Functions
		/// <summary>
		/// Calculates the hyperbolic sine of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>Returns the hyperbolic sine of the specified complex number.</returns>
		public static ComplexF Sinh(ComplexF complex)
		{
			if (complex.IsReal)
			{
				return new ComplexF((float)System.Math.Sinh(complex.Real), 0.0f);
			}

			return new ComplexF(
				(float)(System.Math.Sinh(complex.Real) * System.Math.Cos(complex.Imaginary)),
				(float)(System.Math.Cosh(complex.Real) * System.Math.Sin(complex.Imaginary))
				);
		}
		/// <summary>
		/// Calculates the hyperbolic cosine of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>Returns the hyperbolic cosine of the specified complex number.</returns>
		public static ComplexF Cosh(ComplexF complex)
		{
			if (complex.IsReal)
			{
				return new ComplexF((float)System.Math.Cosh(complex.Real), 0.0f);
			}

			return new ComplexF(
				(float)(System.Math.Cosh(complex.Real) * System.Math.Cos(complex.Imaginary)),
				(float)(System.Math.Sinh(complex.Real) * System.Math.Sin(complex.Imaginary))
				);
		}
		/// <summary>
		/// Calculates the hyperbolic tangent of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>Returns the hyperbolic tangent of the specified complex number.</returns>
		public static ComplexF Tanh(ComplexF complex)
		{
			if (complex.IsReal)
			{
				return new ComplexF((float)System.Math.Tanh(complex.Real), 0.0f);
			}

			float cosi = (float)System.Math.Cos(complex.Imaginary);
			float sinhr = (float)System.Math.Sinh(complex.Real);
			float denom = (cosi * cosi) + (sinhr * sinhr);

			return new ComplexF(
				(sinhr * (float)System.Math.Cosh(complex.Real)) / denom,
				(cosi * (float)System.Math.Sin(complex.Imaginary)) / denom
				);
		}
		/// <summary>
		/// Calculates the hyperbolic cotangent of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>Returns the hyperbolic cotangent of the specified complex number.</returns>
		public static ComplexF Coth(ComplexF complex)
		{
			if (complex.IsReal)
			{
				return new ComplexF((float)MathFunctions.Coth(complex.Real), 0.0f);
			}

			//return ComplexF.Divide(Cosh(complex), Sinh(complex));
			float sini = -(float)System.Math.Sin(complex.Imaginary);
			float sinhr = (float)System.Math.Sinh(complex.Real);
			float denom = (sini * sini) + (sinhr * sinhr);

			return new ComplexF(
				(sinhr * (float)System.Math.Cosh(complex.Real)) / denom,
				(sini * (float)System.Math.Cos(complex.Imaginary)) / denom
				);
		}
		/// <summary>
		/// Calculates the hyperbolic secant of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>Returns the hyperbolic secant of the specified complex number.</returns>
		public static ComplexF Sech(ComplexF complex)
		{
			if (complex.IsReal)
			{
				return new ComplexF((float)MathFunctions.Sech(complex.Real), 0.0f);
			}

			ComplexF exp = ComplexF.Exp(complex);
			return (2 * exp) / (ComplexF.Square(exp) + 1);
		}
		/// <summary>
		/// Calculates the hyperbolic cosecant of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>Returns the hyperbolic cosecant of the specified complex number.</returns>
		public static ComplexF Csch(ComplexF complex)
		{
			if (complex.IsReal)
			{
				return new ComplexF((float)MathFunctions.Csch((double)complex.Real), 0.0f);
			}

			ComplexF exp = ComplexF.Exp(complex);
			return (2 * exp) / (ComplexF.Square(exp) - 1);
		}
		#endregion

		#region Public Static Trigonometric Hyperbolic Area Functions
		public static ComplexF Asinh(ComplexF complex)
		{
			ComplexF result = ComplexF.Sqrt(ComplexF.Square(complex) + 1);
			result = ComplexF.Log(complex + result);
			return result;
		}
		public static ComplexF Acosh(ComplexF complex)
		{
			ComplexF result = ComplexF.Sqrt(complex - 1) * ComplexF.Sqrt(complex + 1);
			result = complex + result;
			result = ComplexF.Log(result);
			return result;
		}
		public static ComplexF Atanh(ComplexF complex)
		{
			return 0.5f * (ComplexF.Log(1 + complex) - ComplexF.Log(1 - complex));
		}
		public static ComplexF Acoth(ComplexF complex)
		{
			return 0.5f * (ComplexF.Log(complex + 1) - ComplexF.Log(complex - 1));
		}
		public static ComplexF Asech(ComplexF complex)
		{
			ComplexF inverse = 1 / complex;
			ComplexF temp = inverse + ComplexF.Sqrt(inverse - 1) * ComplexF.Sqrt(inverse + 1);
			return ComplexF.Log(temp);
		}
		public static ComplexF Acsch(ComplexF complex)
		{
			ComplexF inverse = 1 / complex;
			ComplexF temp = inverse + ComplexF.Square(inverse - 1) * ComplexF.Square(inverse + 1);
			return ComplexF.Log(temp);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Scale the complex number to modulus 1.
		/// </summary>
		public void Normalize()
		{
			float modulus = this.Modulus;
			if (modulus == 0)
			{
				throw new DivideByZeroException("Can not normalize a complex number that is zero.");
			}
			_real = (float)(_real / modulus);
			_image = (float)(_image / modulus);
		}
		#endregion

		#region System.Object Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _real.GetHashCode() ^ _image.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="ComplexF"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is ComplexF)
			{
				ComplexF c = (ComplexF)obj;
				return (this.Real == c.Real) && (this.Imaginary == c.Imaginary);
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0}, {1})", _real, _image);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified complex numbers are equal.
		/// </summary>
		/// <param name="left">The left-hand complex number.</param>
		/// <param name="right">The right-hand complex number.</param>
		/// <returns><see langword="true"/> if the two complex numbers are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(ComplexF left, ComplexF right)
		{
			return ValueType.Equals(left, right);
		}
		/// <summary>
		/// Tests whether two specified complex numbers are not equal.
		/// </summary>
		/// <param name="left">The left-hand complex number.</param>
		/// <param name="right">The right-hand complex number.</param>
		/// <returns><see langword="true"/> if the two complex numbers are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(ComplexF left, ComplexF right)
		{
			return !ValueType.Equals(left, right);
		}
		#endregion

		#region Unary Operators
		/// <summary>
		/// Negates the complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the negated values.</returns>
		public static ComplexF operator -(ComplexF complex)
		{
			return ComplexF.Negate(complex);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF operator +(ComplexF left, ComplexF right)
		{
			return ComplexF.Add(left, right);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF operator +(ComplexF complex, float scalar)
		{
			return ComplexF.Add(complex, scalar);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF operator +(float scalar, ComplexF complex)
		{
			return ComplexF.Add(complex, scalar);
		}
		/// <summary>
		/// Subtracts a complex from a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF operator -(ComplexF left, ComplexF right)
		{
			return ComplexF.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a scalar from a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF operator -(ComplexF complex, float scalar)
		{
			return ComplexF.Subtract(complex, scalar);
		}
		/// <summary>
		/// Subtracts a complex from a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF operator -(float scalar, ComplexF complex)
		{
			return ComplexF.Subtract(scalar, complex);
		}

		/// <summary>
		/// Multiplies two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator *(ComplexF left, ComplexF right)
		{
			return ComplexF.Multiply(left, right);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator *(float scalar, ComplexF complex)
		{
			return ComplexF.Multiply(complex, scalar);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator *(ComplexF complex, float scalar)
		{
			return ComplexF.Multiply(complex, scalar);
		}
		/// <summary>
		/// Divides a complex by a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexF"/> instance.</param>
		/// <param name="right">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator /(ComplexF left, ComplexF right)
		{
			return ComplexF.Divide(left, right);
		}
		/// <summary>
		/// Divides a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator /(ComplexF complex, float scalar)
		{
			return ComplexF.Divide(complex, scalar);
		}
		/// <summary>
		/// Divides a scalar by a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexF"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator /(float scalar, ComplexF complex)
		{
			return ComplexF.Divide(scalar, complex);
		}
		#endregion

	}
}
