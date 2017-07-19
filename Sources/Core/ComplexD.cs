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

#endregion

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents a complex double-precision doubleing point number.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ComplexD : ICloneable
	{
		#region Private Fields
		private double _real;
		private double _image;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ComplexD"/> class using given real and imaginary values.
		/// </summary>
		/// <param name="real">Real value.</param>
		/// <param name="imaginary">Imaginary value.</param>
		public ComplexD(double real, double imaginary)
		{
			_real = real;
			_image = imaginary;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ComplexD"/> class using values from a given complex instance.
		/// </summary>
		/// <param name="c">A complex number to get values from.</param>
		public ComplexD(ComplexD c)
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
		public double Real
		{
			get { return _real; }
			set { _real = value; }
		}
		/// <summary>
		/// Gets or sets the imaginary value of the complex number.
		/// </summary>
		/// <value>The imaginary value of this complex number.</value>
		public double Imaginary
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
		/// <value>A double-precision floating-point number.</value>
		/// <remarks>See http://mathworld.wolfram.com/ComplexModulus.html for further details.</remarks>
		public double Modulus
		{
			get
			{
				return System.Math.Sqrt(_real * _real + _image * _image);
			}
		}
		/// <summary>
		/// Gets the squared modulus of the complex number.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		/// <remarks>See http://mathworld.wolfram.com/ComplexModulus.html for further details.</remarks>
		public double ModulusSquared
		{
			get
			{
				return ((_real * _real) + (_image * _image));
			}
		}
		/// <summary>
		/// Gets or sets the argument of the complex number.
		/// </summary>
		/// <value>A double-precision floating-point number.</value>
		/// <remarks>See http://mathworld.wolfram.com/ComplexArgument.html for further details.</remarks>
		public double Argument
		{
			get
			{
				if ((_image == 0.0) && (_real < 0.0))
				{
					return System.Math.PI;
				}

				if ((_image == 0.0) &&  (_real >= 0.0))
				{
					return 0.0;
				}

				return System.Math.Atan2(_image, _real);
			}
			set
			{
				double modulus = this.Modulus;
				_real = System.Math.Cos(value) * modulus;
				_image = System.Math.Sin(value) * modulus;
			}
		}
		/// <summary>
		/// Gets or sets the conjugate of the complex number. 
		/// </summary>
		/// <value>A <see cref="ComplexD"/> instance.</value>
		public ComplexD Conjugate
		{
			get
			{
				return new ComplexD(_real, -_image);
			}
			set
			{
				this = value.Conjugate;
			}
		}
		#endregion

		#region Constants
		/// <summary>
		///  A double-precision complex number that represents zero.
		/// </summary>
		public static readonly ComplexD Zero = new ComplexD(0, 0);
		/// <summary>
		///  A double-precision complex number that represents one.
		/// </summary>
		public static readonly ComplexD One = new ComplexD(1, 0);
		/// <summary>
		///  A double-precision complex number that represents the squere root of (-1).
		/// </summary>
		public static readonly ComplexD I = new ComplexD(0, 1);
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="ComplexD"/> object.
		/// </summary>
		/// <returns>The <see cref="ComplexD"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new ComplexD(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="ComplexD"/> object.
		/// </summary>
		/// <returns>The <see cref="ComplexD"/> object this method creates.</returns>
		public ComplexD Clone()
		{
			return new ComplexD(this);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="ComplexD"/> equivalent.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="ComplexD"/>.</param>
		/// <returns>A <see cref="ComplexD"/> that represents the complex number specified by the <paramref name="s"/> parameter.</returns>
		public static ComplexD Parse(string value)
		{
			Regex r = new Regex(@"\((?<real>.*),(?<imaginary>.*)\)", RegexOptions.None);
			Match m = r.Match(value);
			if (m.Success)
			{
				return new ComplexD(
					double.Parse(m.Result("${real}")),
					double.Parse(m.Result("${imaginary}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		/// <summary>
		/// Converts the specified string to its <see cref="ComplexD"/> equivalent.
		/// A return value indicates whether the conversion succeeded or failed.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="ComplexD"/>.</param>
		/// <param name="result">
		/// When this method returns, if the conversion succeeded,
		/// contains a <see cref="ComplexD"/> representing the complex number specified by <paramref name="value"/>.
		/// </param>
		/// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
		public static bool TryParse(string value, out ComplexD result)
		{
			Regex r = new Regex(@"\((?<real>.*),(?<imaginary>.*)\)", RegexOptions.None);
			Match m = r.Match(value);
			if (m.Success)
			{
				result = new ComplexD(
					double.Parse(m.Result("${real}")),
					double.Parse(m.Result("${imaginary}"))
					);

				return true;
			}

			result = ComplexD.Zero;
			return false;
		}
		#endregion

		#region Public Static Complex Arithmetics
		/// <summary>
		/// Adds two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the sum.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexAddition.html for further details.</remarks>
		public static ComplexD Add(ComplexD left, ComplexD right)
		{
			return new ComplexD(left.Real + right.Real, left.Imaginary + right.Imaginary);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the sum.</returns>
		public static ComplexD Add(ComplexD complex, double scalar)
		{
			return new ComplexD(complex.Real + scalar, complex.Imaginary);
		}
		/// <summary>
		/// Adds two complex numbers and put the result in the third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexAddition.html for further details.</remarks>
		public static void Add(ComplexD left, ComplexD right, ref ComplexD result)
		{
			result.Real = left.Real + right.Real;
			result.Imaginary = left.Imaginary + right.Imaginary;
		}
		/// <summary>
		/// Adds a complex number and a scalar and put the result into another complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		public static void Add(ComplexD complex, double scalar, ref ComplexD result)
		{
			result.Real = complex.Real + scalar;
			result.Imaginary = complex.Imaginary;
		}

		/// <summary>
		/// Subtracts a complex from a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the difference.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexSubtraction.html for further details.</remarks>
		public static ComplexD Subtract(ComplexD left, ComplexD right)
		{
			return new ComplexD(left.Real - right.Real, left.Imaginary - right.Imaginary);
		}
		/// <summary>
		/// Subtracts a scalar from a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the difference.</returns>
		public static ComplexD Subtract(ComplexD complex, double scalar)
		{
			return new ComplexD(complex.Real - scalar, complex.Imaginary);
		}
		/// <summary>
		/// Subtracts a complex from a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the difference.</returns>
		public static ComplexD Subtract(double scalar, ComplexD complex)
		{
			return new ComplexD(scalar - complex.Real, complex.Imaginary);
		}
		/// <summary>
		/// Subtracts a complex from a complex and put the result in the third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexSubtraction.html for further details.</remarks>
		public static void Subtract(ComplexD left, ComplexD right, ref ComplexD result)
		{
			result.Real = left.Real - right.Real;
			result.Imaginary = left.Imaginary - right.Imaginary;
		}
		/// <summary>
		/// Subtracts a scalar from a complex and put the result into another complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		public static void Subtract(ComplexD complex, double scalar, ref ComplexD result)
		{
			result.Real = complex.Real - scalar;
			result.Imaginary = complex.Imaginary;
		}
		/// <summary>
		/// Subtracts a complex from a scalar and put the result into another complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		public static void Subtract(double scalar, ComplexD complex, ref ComplexD result)
		{
			result.Real = scalar - complex.Real;
			result.Imaginary = complex.Imaginary;
		}

		/// <summary>
		/// Multiplies two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexMultiplication.html for further details.</remarks>
		public static ComplexD Multiply(ComplexD left, ComplexD right)
		{
			// (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
			double x = left.Real, y = left.Imaginary;
			double u = right.Real, v = right.Imaginary;

			return new ComplexD(x * u - y * v, x * v + y * u);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD Multiply(ComplexD complex, double scalar)
		{
			return new ComplexD(complex.Real * scalar, complex.Imaginary * scalar);
		}
		/// <summary>
		/// Multiplies two complex numbers and put the result in a third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexMultiplication.html for further details.</remarks>
		public static void Multiply(ComplexD left, ComplexD right, ref ComplexD result)
		{
			// (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
			double x = left.Real, y = left.Imaginary;
			double u = right.Real, v = right.Imaginary;

			result.Real = x * u - y * v;
			result.Imaginary = x * v + y * u;
		}
		/// <summary>
		/// Multiplies a complex by a scalar and put the result into another complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		public static void Multiply(ComplexD complex, double scalar, ref ComplexD result)
		{
			result.Real = complex.Real * scalar;
			result.Imaginary = complex.Imaginary * scalar;
		}
		
		/// <summary>
		/// Divides a complex by a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		/// <remarks>See http://mathworld.wolfram.com/ComplexDivision.html for further details.</remarks>
		public static ComplexD Divide(ComplexD left, ComplexD right)
		{
			double x = left.Real, y = left.Imaginary;
			double u = right.Real, v = right.Imaginary;
			double modulusSquared = u * u + v * v;

			if (modulusSquared == 0)
			{
				throw new DivideByZeroException();
			}

			double invModulusSquared = 1 / modulusSquared;

			return new ComplexD(
				(x * u + y * v) * invModulusSquared,
				(y * u - x * v) * invModulusSquared);
		}
		/// <summary>
		/// Divides a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD Divide(ComplexD complex, double scalar)
		{
			if (scalar == 0)
			{
				throw new DivideByZeroException();
			}

			return new ComplexD(
				complex.Real / scalar,
				complex.Imaginary / scalar);
		}
		/// <summary>
		/// Divides a scalar by a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD Divide(double scalar, ComplexD complex)
		{
			if ((complex.Real == 0) || (complex.Imaginary == 0))
			{
				throw new DivideByZeroException();
			}

			return new ComplexD(
				scalar / complex.Real,
				scalar / complex.Imaginary);
		}
		/// <summary>
		/// Divides a complex by a complex and put the result in a third complex number.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		/// <remarks>See http://mathworld.wolfram.com/ComplexDivision.html for further details.</remarks>
		public static void Divide(ComplexD left, ComplexD right, ref ComplexD result)
		{
			double x = left.Real, y = left.Imaginary;
			double u = right.Real, v = right.Imaginary;
			double modulusSquared = u * u + v * v;

			if (modulusSquared == 0)
			{
				throw new DivideByZeroException();
			}

			double invModulusSquared = 1 / modulusSquared;

			result.Real = (x * u + y * v) * invModulusSquared;
			result.Imaginary = (y * u - x * v) * invModulusSquared;
		}
		/// <summary>
		/// Divides a complex by a scalar and put the result into another complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		public static void Divide(ComplexD complex, double scalar, ref ComplexD result)
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
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A double-precision floating-point value.</param>
		/// <param name="result">A <see cref="ComplexD"/> instance to hold the result.</param>
		public static void Divide(double scalar, ComplexD complex, ref ComplexD result)
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
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the negated values.</returns>
		public static ComplexD Negate(ComplexD complex)
		{
			return new ComplexD(-complex.Real, -complex.Imaginary);
		}

		/// <summary>
		/// Tests whether two complex numbers are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEqual(ComplexD left, ComplexD right)
		{
			return ApproxEqual(left, right, MathFunctions.EpsilonD);
		}
		/// <summary>
		/// Tests whether two complex numbers are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEqual(ComplexD left, ComplexD right, double tolerance)
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
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The square root of the complex number given in <paramref name="complex"/>.</returns>
		/// <remarks>See http://mathworld.wolfram.com/SquareRoot.html for further details.</remarks>
		public static ComplexD Sqrt(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if ((complex.Real == 0.0) && (complex.Imaginary == 0.0))
			{
				return result;
			}
			else if (complex.IsReal)
			{
				result.Real = (complex.Real > 0) ? System.Math.Sqrt(complex.Real) : System.Math.Sqrt(-complex.Real);
				result.Imaginary = 0.0;
			}
			else
			{
				double modulus = complex.Modulus;

				result.Real = System.Math.Sqrt(0.5 * (modulus + complex.Real));
				result.Imaginary = System.Math.Sqrt(0.5 * (modulus - complex.Real));
				if (complex.Imaginary < 0.0)
					result.Imaginary = -result.Imaginary;
			}

			return result;
		}
		/// <summary>
		/// Calculates the logarithm of a specified complex number.
		/// Calculates the natural (base e) logarithm of a specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The natural (base e) logarithm of the complex number given in <paramref name="complex"/>.</returns>
		public static ComplexD Log(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if ((complex.Real > 0.0) && (complex.Imaginary == 0.0))
			{
				result.Real = System.Math.Log(complex.Real);
				result.Imaginary = 0.0;
			}
			else if (complex.Real == 0.0)
			{
				if (complex.Imaginary > 0.0)
				{
					result.Real = System.Math.Log(complex.Imaginary);
					result.Imaginary = MathFunctions.HalfPI;
				}
				else
				{
					result.Real = System.Math.Log(-(complex.Imaginary));
					result.Imaginary = -MathFunctions.HalfPI;
				}
			}
			else
			{
				result.Real = System.Math.Log(complex.Modulus);
				result.Imaginary = System.Math.Atan2(complex.Imaginary, complex.Real);
			}

			return result;
		}
		/// <summary>
		/// Calculates the exponential of a specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The exponential of the complex number given in <paramref name="complex"/>.</returns>
		public static ComplexD Exp(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;
			double r = System.Math.Exp(complex.Real);
			result.Real = r * System.Math.Cos(complex.Imaginary);
			result.Imaginary = r * System.Math.Sin(complex.Imaginary);

			return result;
		}
		/// <summary>
		/// Calculates a specified complex number raised by a specified power.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> representing the number to raise.</param>
		/// <param name="power">A <see cref="ComplexD"/> representing the power.</param>
		/// <returns>The complex <paramref name="complex"/> raised by <paramref name="power"/>.</returns>
		public static ComplexD Pow(ComplexD complex, ComplexD power)
		{
			return Exp(power * Log(complex));
		}
		/// <summary>
		/// Calculates the square of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The square of the given complex number.</returns>
		public static ComplexD Square(ComplexD complex)
		{
			if (complex.IsReal)
			{
				return new ComplexD(complex.Real * complex.Real, 0.0);
			}

			double real = complex.Real;
			double imag = complex.Imaginary;
			return new ComplexD(real * real - imag * imag, 2 * real * imag);
		}
		#endregion

		#region Public Static Trigonometric Functions
		/// <summary>
		/// Calculates the sine of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The sine of <paramref name="complex"/>.</returns>
		public static ComplexD Sin(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if (complex.IsReal)
			{
				result.Real = System.Math.Sin(complex.Real);
				result.Imaginary = 0.0;
			}
			else
			{
				result.Real = System.Math.Sin(complex.Real) * System.Math.Cosh(complex.Imaginary);
				result.Imaginary = System.Math.Cos(complex.Real) * System.Math.Sinh(complex.Imaginary);
			}

			return result;
		}
		/// <summary>
		/// Calculates the cosine of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The cosine of <paramref name="complex"/>.</returns>
		public static ComplexD Cos(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if (complex.IsReal)
			{
				result.Real = System.Math.Cos(complex.Real);
				result.Imaginary = 0.0;
			}
			else
			{
				result.Real = System.Math.Cos(complex.Real) * System.Math.Cosh(complex.Imaginary);
				result.Imaginary = -System.Math.Sin(complex.Real) * System.Math.Sinh(complex.Imaginary);
			}

			return result;
		}
		/// <summary>
		/// Calculates the tangent of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The tangent of <paramref name="complex"/>.</returns>
		public static ComplexD Tan(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if (complex.IsReal)
			{
				result.Real = System.Math.Tan(complex.Real);
				result.Imaginary = 0.0;
			}
			else
			{
				double cosr = System.Math.Cos(complex.Real);
				double sinhi = System.Math.Sinh(complex.Imaginary);
				double denom = cosr * cosr + sinhi * sinhi;

				result.Real = System.Math.Sin(complex.Real) * cosr / denom;
				result.Imaginary = sinhi * System.Math.Cosh(complex.Imaginary) / denom;
			}

			return result;
		}
		/// <summary>
		/// Calculates the cotangent of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The cotangent of <paramref name="complex"/>.</returns>
		public static ComplexD Cot(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if (complex.IsReal)
			{
				result.Real = MathFunctions.Cot(complex.Real);
			}
			else
			{
				double sinr = System.Math.Sin(complex.Real);
				double sinhi = System.Math.Sinh(complex.Imaginary);
				double denom = sinr * sinr + sinhi * sinhi;

				result.Real = (sinr * System.Math.Cos(complex.Real)) / denom;
				result.Imaginary = (-sinhi * System.Math.Cosh(complex.Imaginary)) / denom;
			}

			return result;
		}
		/// <summary>
		/// Calculates the secant of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The secant of <paramref name="complex"/>.</returns>
		public static ComplexD Sec(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if (complex.IsReal)
			{
				result.Real = MathFunctions.Sec(complex.Real);
			}
			else
			{
				double cosr = MathFunctions.Cos(complex.Real);
				double sinhi = MathFunctions.Sinh(complex.Imaginary);
				double denom = cosr * cosr + sinhi * sinhi;
				result.Real = (cosr * MathFunctions.Cosh(complex.Imaginary)) / denom;
				result.Imaginary = (MathFunctions.Sin(complex.Real) * sinhi) / denom;
			}

			return result;
		}
		/// <summary>
		/// Calculates the cosecant of the specified complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The cosecant of <paramref name="complex"/>.</returns>
		public static ComplexD Csc(ComplexD complex)
		{
			ComplexD result = ComplexD.Zero;

			if (complex.IsReal)
			{
				result.Real = MathFunctions.Csc(complex.Real);
			}
			else
			{
				double sinr = MathFunctions.Sin(complex.Real);
				double sinhi = MathFunctions.Sinh(complex.Imaginary);
				double denom = sinr * sinr + sinhi * sinhi;
				result.Real = (sinr * MathFunctions.Cosh(complex.Imaginary)) / denom;
				result.Imaginary = (-MathFunctions.Cos(complex.Real) * sinhi) / denom;
			}

			return result;
		}
		#endregion

		#region Public Static Trigonometric Arcus Functions
		public static ComplexD Asin(ComplexD complex)
		{
			ComplexD result = 1 - ComplexD.Square(complex);
			result = ComplexD.Sqrt(result);
			result = result + (ComplexD.I * complex);
			result = ComplexD.Log(result);
			result = -ComplexD.I * result;

			return result;
		}
		public static ComplexD Acos(ComplexD complex)
		{
			ComplexD result = 1 - ComplexD.Square(complex);
			result = ComplexD.Sqrt(result);
			result = ComplexD.I * result;
			result = complex + result;
			result = ComplexD.Log(result);
			result = -ComplexD.I * result;
			return result;
		}
		public static ComplexD Atan(ComplexD complex)
		{
			ComplexD tmp = new ComplexD(-complex.Imaginary, complex.Real);
			return (new ComplexD(0, 0.5)) * (ComplexD.Log(1 - tmp) - ComplexD.Log(1 + tmp));
		}
		public static ComplexD Acot(ComplexD complex)
		{
			ComplexD tmp = new ComplexD(-complex.Imaginary, complex.Real);
			return (new ComplexD(0, 0.5)) * (ComplexD.Log(1 + tmp) - ComplexD.Log(1 - tmp)) + MathFunctions.HalfPI;
		}
		public static ComplexD Asec(ComplexD complex)
		{
			ComplexD inverse = 1 / complex;
			return (-ComplexD.I) * ComplexD.Log(inverse + ComplexD.I * ComplexD.Sqrt(1 - ComplexD.Square(inverse)));
		}
		public static ComplexD Acsc(ComplexD complex)
		{
			ComplexD inverse = 1 / complex;
			return (-ComplexD.I) * ComplexD.Log(ComplexD.I * inverse + ComplexD.Sqrt(1 - ComplexD.Square(inverse)));
		}
		#endregion

		#region Public Static Trigonometric Hyperbolic Functions
		/// <summary>
		/// Calculates the hyperbolic sine of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>Returns the hyperbolic sine of the specified complex number.</returns>
		public static ComplexD Sinh(ComplexD complex)
		{
			if (complex.IsReal)
			{
				return new ComplexD(System.Math.Sinh(complex.Real), 0.0);
			}

			return new ComplexD(
				System.Math.Sinh(complex.Real)*System.Math.Cos(complex.Imaginary),
				System.Math.Cosh(complex.Real)*System.Math.Sin(complex.Imaginary)
				);
		}
		/// <summary>
		/// Calculates the hyperbolic cosine of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>Returns the hyperbolic cosine of the specified complex number.</returns>
		public static ComplexD Cosh(ComplexD complex)
		{
			if (complex.IsReal)
			{
				return new ComplexD(System.Math.Cosh(complex.Real), 0.0);
			}

			return new ComplexD(
				System.Math.Cosh(complex.Real) * System.Math.Cos(complex.Imaginary),
				System.Math.Sinh(complex.Real) * System.Math.Sin(complex.Imaginary)
				);
		}
		/// <summary>
		/// Calculates the hyperbolic tangent of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>Returns the hyperbolic tangent of the specified complex number.</returns>
		public static ComplexD Tanh(ComplexD complex)
		{
			if (complex.IsReal)
			{
				return new ComplexD(System.Math.Tanh(complex.Real), 0.0);
			}

			double cosi = System.Math.Cos(complex.Imaginary);
			double sinhr = System.Math.Sinh(complex.Real);
			double denom = (cosi * cosi) + (sinhr * sinhr);

			return new ComplexD(
				(sinhr* System.Math.Cosh(complex.Real)) / denom,
				(cosi * System.Math.Sin(complex.Imaginary)) / denom
				);
		}
		/// <summary>
		/// Calculates the hyperbolic cotangent of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>Returns the hyperbolic cotangent of the specified complex number.</returns>
		public static ComplexD Coth(ComplexD complex)
		{
			if (complex.IsReal)
			{
				return new ComplexD(MathFunctions.Coth(complex.Real), 0.0);
			}

			//return ComplexD.Divide(Cosh(complex), Sinh(complex));
			double sini = -System.Math.Sin(complex.Imaginary);
			double sinhr = System.Math.Sinh(complex.Real);
			double denom = (sini * sini) + (sinhr * sinhr);

			return new ComplexD(
				(sinhr * System.Math.Cosh(complex.Real)) / denom,
				(sini * System.Math.Cos(complex.Imaginary)) / denom
				);
		}
		/// <summary>
		/// Calculates the hyperbolic secant of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>Returns the hyperbolic secant of the specified complex number.</returns>
		public static ComplexD Sech(ComplexD complex)
		{
			if (complex.IsReal)
			{
				return new ComplexD(MathFunctions.Sech(complex.Real), 0.0);
			}

			ComplexD exp = ComplexD.Exp(complex);
			return (2 * exp) / (ComplexD.Square(exp) + 1);
		}
		/// <summary>
		/// Calculates the hyperbolic cosecant of the specified complex number. 
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>Returns the hyperbolic cosecant of the specified complex number.</returns>
		public static ComplexD Csch(ComplexD complex)
		{
			if (complex.IsReal)
			{
				return new ComplexD(MathFunctions.Csch(complex.Real), 0.0);
			}

			ComplexD exp = ComplexD.Exp(complex);
			return (2 * exp) / (ComplexD.Square(exp) - 1);
		}
		#endregion

		#region Public Static Trigonometric Hyperbolic Area Functions
		public static ComplexD Asinh(ComplexD complex)
		{
			ComplexD result = ComplexD.Sqrt(ComplexD.Square(complex) + 1);
			result = ComplexD.Log(complex + result);
			return result;
		}
		public static ComplexD Acosh(ComplexD complex)
		{
			ComplexD result = ComplexD.Sqrt(complex - 1) * ComplexD.Sqrt(complex + 1);
			result = complex + result;
			result = ComplexD.Log(result);
			return result;
		}
		public static ComplexD Atanh(ComplexD complex)
		{
			return 0.5 * (ComplexD.Log(1 + complex) - ComplexD.Log(1 - complex));
		}
		public static ComplexD Acoth(ComplexD complex)
		{
			return 0.5 * (ComplexD.Log(complex + 1) - ComplexD.Log(complex - 1));
		}
		public static ComplexD Asech(ComplexD complex)
		{
			ComplexD inverse = 1 / complex;
			ComplexD temp = inverse + ComplexD.Sqrt(inverse - 1) * ComplexD.Sqrt(inverse + 1);
			return ComplexD.Log(temp);
		}
		public static ComplexD Acsch(ComplexD complex)
		{
			ComplexD inverse = 1 / complex;
			ComplexD temp = inverse + ComplexD.Square(inverse - 1) * ComplexD.Square(inverse + 1);
			return ComplexD.Log(temp);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Scale the complex number to modulus 1.
		/// </summary>
		public void Normalize()
		{
			double modulus = this.Modulus;
			if (modulus == 0)
			{
				throw new DivideByZeroException("Can not normalize a complex number that is zero.");
			}
			_real = (double)(_real / modulus);
			_image = (double)(_image / modulus);
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
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="ComplexD"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is ComplexD)
			{
				ComplexD c = (ComplexD)obj;
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
			return string.Format("({0}, {1})", _real, _image);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified complex numbers are equal.
		/// </summary>
		/// <param name="left">The left-hand complex number.</param>
		/// <param name="right">The right-hand complex number.</param>
		/// <returns><see langword="true"/> if the two complex numbers are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(ComplexD left, ComplexD right)
		{
			return ValueType.Equals(left, right);
		}
		/// <summary>
		/// Tests whether two specified complex numbers are not equal.
		/// </summary>
		/// <param name="left">The left-hand complex number.</param>
		/// <param name="right">The right-hand complex number.</param>
		/// <returns><see langword="true"/> if the two complex numbers are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(ComplexD left, ComplexD right)
		{
			return !ValueType.Equals(left, right);
		}
		#endregion

		#region Unary Operators
		/// <summary>
		/// Negates the complex number.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the negated values.</returns>
		public static ComplexD operator -(ComplexD complex)
		{
			return ComplexD.Negate(complex);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the sum.</returns>
		public static ComplexD operator +(ComplexD left, ComplexD right)
		{
			return ComplexD.Add(left, right);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the sum.</returns>
		public static ComplexD operator +(ComplexD complex, double scalar)
		{
			return ComplexD.Add(complex, scalar);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the sum.</returns>
		public static ComplexD operator +(double scalar, ComplexD complex)
		{
			return ComplexD.Add(complex, scalar);
		}
		/// <summary>
		/// Subtracts a complex from a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the difference.</returns>
		public static ComplexD operator -(ComplexD left, ComplexD right)
		{
			return ComplexD.Subtract(left, right);
		}
		/// <summary>
		/// Subtracts a scalar from a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the difference.</returns>
		public static ComplexD operator -(ComplexD complex, double scalar)
		{
			return ComplexD.Subtract(complex, scalar);
		}
		/// <summary>
		/// Subtracts a complex from a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the difference.</returns>
		public static ComplexD operator -(double scalar, ComplexD complex)
		{
			return ComplexD.Subtract(scalar, complex);
		}

		/// <summary>
		/// Multiplies two complex numbers.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD operator *(ComplexD left, ComplexD right)
		{
			return ComplexD.Multiply(left, right);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD operator *(double scalar, ComplexD complex)
		{
			return ComplexD.Multiply(complex, scalar);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD operator *(ComplexD complex, double scalar)
		{
			return ComplexD.Multiply(complex, scalar);
		}
		/// <summary>
		/// Divides a complex by a complex.
		/// </summary>
		/// <param name="left">A <see cref="ComplexD"/> instance.</param>
		/// <param name="right">A <see cref="ComplexD"/> instance.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD operator /(ComplexD left, ComplexD right)
		{
			return ComplexD.Divide(left, right);
		}
		/// <summary>
		/// Divides a complex by a scalar.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD operator /(ComplexD complex, double scalar)
		{
			return ComplexD.Divide(complex, scalar);
		}
		/// <summary>
		/// Divides a scalar by a complex.
		/// </summary>
		/// <param name="complex">A <see cref="ComplexD"/> instance.</param>
		/// <param name="scalar">A scalar.</param>
		/// <returns>A new <see cref="ComplexD"/> instance containing the result.</returns>
		public static ComplexD operator /(double scalar, ComplexD complex)
		{
			return ComplexD.Divide(scalar, complex);
		}
		#endregion

	}
}
