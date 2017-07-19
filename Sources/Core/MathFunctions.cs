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

#endregion

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Provides standard mathematical functions for the library types.
	/// </summary>
	public static class MathFunctions
	{
		#region Delegates
		public delegate T VoidFunction<T>();
		public delegate T UnaryFunction<T>(T param);
		public delegate T BinaryFunction<T>(T param1, T param2);
		public delegate T TenaryFunction<T>(T param1, T param2, T param3);
		#endregion

		#region Constants
		/// <summary>
		/// The value of PI.
		/// </summary>
		public const double PI = System.Math.PI;
		/// <summary>
		/// The value of (2 * PI).
		/// </summary>
		public const double TwoPI = 2 * System.Math.PI;
		/// <summary>
		/// The value of (PI*PI).
		/// </summary>
		public const double SquaredPI = System.Math.PI * System.Math.PI;
		/// <summary>
		/// The value of PI/2.
		/// </summary>
		public const double HalfPI = System.Math.PI / 2.0;

		/// <summary>
		/// Epsilon, a fairly small value for a single precision floating point
		/// </summary>
		public const float EpsilonF = 4.76837158203125E-7f;
		/// <summary>
		/// Epsilon, a fairly small value for a double precision floating point
		/// </summary>
		public const double EpsilonD = 8.8817841970012523233891E-16;
		#endregion

		#region Function Definitions

		#region Trigonometric Functions
		public static readonly UnaryFunction<double> DoubleSinFunction = new UnaryFunction<double>(MathFunctions.Sin);
		public static readonly UnaryFunction<double> DoubleCosFunction = new UnaryFunction<double>(MathFunctions.Cos);
		public static readonly UnaryFunction<double> DoubleTanFunction = new UnaryFunction<double>(MathFunctions.Tan);
		public static readonly UnaryFunction<double> DoubleCotFunction = new UnaryFunction<double>(MathFunctions.Cot);
		public static readonly UnaryFunction<double> DoubleSecFunction = new UnaryFunction<double>(MathFunctions.Sec);
		public static readonly UnaryFunction<double> DoubleCscFunction = new UnaryFunction<double>(MathFunctions.Csc);

		public static readonly UnaryFunction<ComplexD> ComplexDSinFunction = new UnaryFunction<ComplexD>(MathFunctions.Sin);
		public static readonly UnaryFunction<ComplexD> ComplexDCosFunction = new UnaryFunction<ComplexD>(MathFunctions.Cos);
		public static readonly UnaryFunction<ComplexD> ComplexDTanFunction = new UnaryFunction<ComplexD>(MathFunctions.Tan);
		public static readonly UnaryFunction<ComplexD> ComplexDCotFunction = new UnaryFunction<ComplexD>(MathFunctions.Cot);
		public static readonly UnaryFunction<ComplexD> ComplexDSecFunction = new UnaryFunction<ComplexD>(MathFunctions.Sec);
		public static readonly UnaryFunction<ComplexD> ComplexDCscFunction = new UnaryFunction<ComplexD>(MathFunctions.Csc);
		#endregion

		#region Trigonometric Arcus Functions
		public static readonly UnaryFunction<double> DoubleAsinFunction = new UnaryFunction<double>(MathFunctions.Asin);
		public static readonly UnaryFunction<double> DoubleAcosFunction = new UnaryFunction<double>(MathFunctions.Acos);
		public static readonly UnaryFunction<double> DoubleAtanFunction = new UnaryFunction<double>(MathFunctions.Atan);
		public static readonly UnaryFunction<double> DoubleAcotFunction = new UnaryFunction<double>(MathFunctions.Acot);
		public static readonly UnaryFunction<double> DoubleAsecFunction = new UnaryFunction<double>(MathFunctions.Asec);
		public static readonly UnaryFunction<double> DoubleAcscFunction = new UnaryFunction<double>(MathFunctions.Acsc);

		public static readonly UnaryFunction<ComplexD> ComplexDAsinFunction = new UnaryFunction<ComplexD>(MathFunctions.Asin);
		public static readonly UnaryFunction<ComplexD> ComplexDAcosFunction = new UnaryFunction<ComplexD>(MathFunctions.Acos);
		public static readonly UnaryFunction<ComplexD> ComplexDAtanFunction = new UnaryFunction<ComplexD>(MathFunctions.Atan);
		public static readonly UnaryFunction<ComplexD> ComplexDAcotFunction = new UnaryFunction<ComplexD>(MathFunctions.Acot);
		public static readonly UnaryFunction<ComplexD> ComplexDAsecFunction = new UnaryFunction<ComplexD>(MathFunctions.Asec);
		public static readonly UnaryFunction<ComplexD> ComplexDAcscFunction = new UnaryFunction<ComplexD>(MathFunctions.Acsc);
		#endregion

		#region Hyperbolic Functions
		public static readonly UnaryFunction<double> DoubleSinhFunction = new UnaryFunction<double>(MathFunctions.Sinh);
		public static readonly UnaryFunction<double> DoubleCoshFunction = new UnaryFunction<double>(MathFunctions.Cosh);
		public static readonly UnaryFunction<double> DoubleTanhFunction = new UnaryFunction<double>(MathFunctions.Tanh);
		public static readonly UnaryFunction<double> DoubleCothFunction = new UnaryFunction<double>(MathFunctions.Coth);
		public static readonly UnaryFunction<double> DoubleSechFunction = new UnaryFunction<double>(MathFunctions.Sech);
		public static readonly UnaryFunction<double> DoubleCschFunction = new UnaryFunction<double>(MathFunctions.Csch);

		public static readonly UnaryFunction<ComplexD> ComplexDSinhFunction = new UnaryFunction<ComplexD>(MathFunctions.Sinh);
		public static readonly UnaryFunction<ComplexD> ComplexDCoshFunction = new UnaryFunction<ComplexD>(MathFunctions.Cosh);
		public static readonly UnaryFunction<ComplexD> ComplexDTanhFunction = new UnaryFunction<ComplexD>(MathFunctions.Tanh);
		public static readonly UnaryFunction<ComplexD> ComplexDCothFunction = new UnaryFunction<ComplexD>(MathFunctions.Coth);
		public static readonly UnaryFunction<ComplexD> ComplexDSechFunction = new UnaryFunction<ComplexD>(MathFunctions.Sech);
		public static readonly UnaryFunction<ComplexD> ComplexDCschFunction = new UnaryFunction<ComplexD>(MathFunctions.Csch);
		#endregion

		#region Hyperbolic Area Functions
		public static readonly UnaryFunction<double> DoubleAsinhFunction = new UnaryFunction<double>(MathFunctions.Asinh);
		public static readonly UnaryFunction<double> DoubleAcoshFunction = new UnaryFunction<double>(MathFunctions.Acosh);
		public static readonly UnaryFunction<double> DoubleAtanhFunction = new UnaryFunction<double>(MathFunctions.Atanh);
		public static readonly UnaryFunction<double> DoubleAcothFunction = new UnaryFunction<double>(MathFunctions.Acoth);
		public static readonly UnaryFunction<double> DoubleAsechFunction = new UnaryFunction<double>(MathFunctions.Asech);
		public static readonly UnaryFunction<double> DoubleAcschFunction = new UnaryFunction<double>(MathFunctions.Acsch);

		public static readonly UnaryFunction<ComplexD> ComplexDAsinhFunction = new UnaryFunction<ComplexD>(MathFunctions.Asinh);
		public static readonly UnaryFunction<ComplexD> ComplexDAcoshFunction = new UnaryFunction<ComplexD>(MathFunctions.Acosh);
		public static readonly UnaryFunction<ComplexD> ComplexDAtanhFunction = new UnaryFunction<ComplexD>(MathFunctions.Atanh);
		public static readonly UnaryFunction<ComplexD> ComplexDAcothFunction = new UnaryFunction<ComplexD>(MathFunctions.Acoth);
		public static readonly UnaryFunction<ComplexD> ComplexDAsechFunction = new UnaryFunction<ComplexD>(MathFunctions.Asech);
		public static readonly UnaryFunction<ComplexD> ComplexDAcschFunction = new UnaryFunction<ComplexD>(MathFunctions.Acsch);
		#endregion

		#region Abs Functions
		/// <summary>
		/// Absolute value function for single-precision floating point numbers.
		/// </summary>
		public static readonly UnaryFunction<float> FloatAbsFunction = new UnaryFunction<float>(System.Math.Abs);
		/// <summary>
		/// Absolute value function for double-precision floating point numbers.
		/// </summary>
		public static readonly UnaryFunction<double> DoubleAbsFunction = new UnaryFunction<double>(System.Math.Abs);
		/// <summary>
		/// Absolute value function for integers.
		/// </summary>
		public static readonly UnaryFunction<int> IntAbsFunction = new UnaryFunction<int>(System.Math.Abs);
		#endregion



		public static readonly UnaryFunction<double> DoubleSqrtFunction = new UnaryFunction<double>(System.Math.Sqrt);
		#endregion

		#region Abs
		/// <summary>
		/// Calculates the absolute value of an integer.
		/// </summary>
		/// <param name="x">An integer.</param>
		/// <returns>The absolute value of <paramref name="x"/>.</returns>
		public static int Abs(int x)
		{
			return System.Math.Abs(x);
		}
		/// <summary>
		/// Calculates the absolute value of a single-precision floating point number.
		/// </summary>
		/// <param name="x">A single-precision floating point number.</param>
		/// <returns>The absolute value of <paramref name="x"/>.</returns>
		public static float Abs(float x)
		{
			return System.Math.Abs(x);
		}
		/// <summary>
		/// Calculates the absolute value of a double-precision floating point number.
		/// </summary>
		/// <param name="x">A double-precision floating point number.</param>
		/// <returns>The absolute value of <paramref name="x"/>.</returns>
		public static double Abs(double x)
		{
			return System.Math.Abs(x);
		}
		/// <summary>
		/// Creates a new array of integers whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given integers array.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>A new array of integers whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static int[] Abs(int[] array)
		{
			int[] result = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				result[i] = Abs(array[i]);
			}
			return result;
		}		
		/// <summary>
		/// Creates a new <see cref="float[]"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given floats array.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>A new <see cref="FloatArrayList"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static float[] Abs(float[] array)
		{
			float[] result = new float[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				result[i] = Abs(array[i]);
			}
			return result;
		}
		/// <summary>
		/// Creates a new <see cref="double[]"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given doubles array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>A new <see cref="double[]"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static double[] Abs(double[] array)
		{
			double[] result = new double[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				result[i] = Abs(array[i]);
			}

			return result;
		}
		#endregion

		#region Clamp
		/// <summary>
		/// Clamp a <paramref name="value"/> to <paramref name="calmpedValue"/> if it is withon the <paramref name="tolerance"/> range.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="calmpedValue">The clamped value.</param>
		/// <param name="tolerance">The tolerance value.</param>
		/// <returns>
		/// Returns the clamped value.
		/// result = (tolerance > Abs(value-calmpedValue)) ? calmpedValue : value;
		/// </returns>
		public static double Clamp(double value, double calmpedValue, double tolerance)
		{
			return (tolerance > Abs(value - calmpedValue)) ? calmpedValue : value;
		}
		/// <summary>
		/// Clamp a <paramref name="value"/> to <paramref name="calmpedValue"/> using the default tolerance value.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="calmpedValue">The clamped value.</param>
		/// <returns>
		/// Returns the clamped value.
		/// result = (EpsilonD > Abs(value-calmpedValue)) ? calmpedValue : value;
		/// </returns>
		/// <remarks><see cref="MathFunctions.EpsilonD"/> is used for tolerance.</remarks>
		public static double Clamp(double value, double calmpedValue)
		{
			return (EpsilonD > Abs(value - calmpedValue)) ? calmpedValue : value;
		}
		/// <summary>
		/// Clamp a <paramref name="value"/> to <paramref name="calmpedValue"/> if it is withon the <paramref name="tolerance"/> range.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="calmpedValue">The clamped value.</param>
		/// <param name="tolerance">The tolerance value.</param>
		/// <returns>
		/// Returns the clamped value.
		/// result = (tolerance > Abs(value-calmpedValue)) ? calmpedValue : value;
		/// </returns>
		public static float Clamp(float value, float calmpedValue, float tolerance)
		{
			return (tolerance > Abs(value - calmpedValue)) ? calmpedValue : value;
		}
		/// <summary>
		/// Clamp a <paramref name="value"/> to <paramref name="calmpedValue"/> using the default tolerance value.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="calmpedValue">The clamped value.</param>
		/// <returns>
		/// Returns the clamped value.
		/// result = (EpsilonF > Abs(value-calmpedValue)) ? calmpedValue : value;
		/// </returns>
		/// <remarks><see cref="MathFunctions.EpsilonF"/> is used for tolerance.</remarks>
		public static float Clamp(float value, float calmpedValue)
		{
			return (EpsilonF > Abs(value - calmpedValue)) ? calmpedValue : value;
		}
		#endregion

		#region Trigonometric Functions
		public static double Sin(double value)
		{
			return System.Math.Sin(value);
		}
		public static float Sin(float value)
		{
			return (float)System.Math.Sin(value);
		}
		public static ComplexD Sin(ComplexD value)
		{
			return ComplexD.Sin(value);
		}

		public static double Cos(double value)
		{
			return System.Math.Cos(value);
		}
		public static float Cos(float value)
		{
			return (float)System.Math.Cos(value);
		}
		public static ComplexD Cos(ComplexD value)
		{
			return ComplexD.Cos(value);
		}

		public static double Tan(double value)
		{
			return System.Math.Tan(value);
		}
		public static float Tan(float value)
		{
			return (float)System.Math.Tan(value);
		}
		public static ComplexD Tan(ComplexD value)
		{
			return ComplexD.Tan(value);
		}

		public static double Cot(double value)
		{
			return 1 / System.Math.Tan(value);
		}
		public static float Cot(float value)
		{
			return (float)(1 / System.Math.Tan(value));
		}
		public static ComplexD Cot(ComplexD value)
		{
			return ComplexD.Cot(value);
		}

		public static double Sec(double value)
		{
			return 1 / System.Math.Cos(value);
		}
		public static float Sec(float value)
		{
			return (float)(1 / System.Math.Cos(value));
		}
		public static ComplexD Sec(ComplexD value)
		{
			return ComplexD.Sec(value);
		}

		public static double Csc(double value)
		{
			return 1 / System.Math.Sin(value);
		}
		public static float Csc(float value)
		{
			return (float)(1 / System.Math.Sin(value));
		}
		public static ComplexD Csc(ComplexD value)
		{
			return ComplexD.Csc(value);
		}
		#endregion

		#region Trigonometric Arcus Functions
		public static double Asin(double value)
		{
			return System.Math.Asin(value);
		}
		public static ComplexD Asin(ComplexD value)
		{
			return ComplexD.Asin(value);
		}

		public static double Acos(double value)
		{
			return System.Math.Acos(value);
		}
		public static ComplexD Acos(ComplexD value)
		{
			return ComplexD.Acos(value);
		}

		public static double Atan(double value)
		{
			return System.Math.Atan(value);
		}
		public static ComplexD Atan(ComplexD value)
		{
			return ComplexD.Atan(value);
		}

		public static double Acot(double value)
		{
			return System.Math.Atan(1 / value);
		}
		public static ComplexD Acot(ComplexD value)
		{
			return ComplexD.Acot(value);
		}

		public static double Asec(double value)
		{
			return System.Math.Acos(1 / value);
		}
		public static ComplexD Asec(ComplexD value)
		{
			return ComplexD.Asec(value);
		}

		public static double Acsc(double value)
		{
			return System.Math.Asin(1 / value);
		}
		public static ComplexD Acsc(ComplexD value)
		{
			return ComplexD.Acsc(value);
		}
		#endregion

		#region Hyperbolic Functions
		public static double Sinh(double value) 
		{
			return System.Math.Sinh(value);
		}
		public static ComplexD Sinh(ComplexD value)
		{
			return ComplexD.Sinh(value);
		}

		public static double Cosh(double value) 
		{
			return System.Math.Cosh(value);
		}
		public static ComplexD Cosh(ComplexD value)
		{
			return ComplexD.Cosh(value);
		}

		public static double Tanh(double value) 
		{
			return System.Math.Tanh(value);
		}
		public static ComplexD Tanh(ComplexD value)
		{
			return ComplexD.Tanh(value);
		}

		public static double Coth(double value) 
		{
			return 1/System.Math.Tanh(value);
		}
		public static ComplexD Coth(ComplexD value)
		{
			return ComplexD.Coth(value);
		}

		public static double Sech(double value) 
		{
			return 1/Cosh(value);
		}
		public static ComplexD Sech(ComplexD value)
		{
			return ComplexD.Sech(value);
		}

		public static double Csch(double value) 
		{
			return 1/Sinh(value);
		}
		public static ComplexD Csch(ComplexD value)
		{
			return ComplexD.Csch(value);
		}
		#endregion

		#region Hyperbolic Area Functions
		public static double Asinh(double value)
		{
			return System.Math.Log(value + System.Math.Sqrt(value * value + 1), System.Math.E);
		}
		public static ComplexD Asinh(ComplexD value)
		{
			return ComplexD.Asinh(value);
		}

		public static double Acosh(double value)
		{
			return System.Math.Log(value + System.Math.Sqrt(value - 1) * System.Math.Sqrt(value + 1), System.Math.E);
		}
		public static ComplexD Acosh(ComplexD value)
		{
			return ComplexD.Acosh(value);
		}

		public static double Atanh(double value)
		{
			return 0.5 * System.Math.Log((1 + value) / (1 - value), System.Math.E);
		}
		public static ComplexD Atanh(ComplexD value)
		{
			return ComplexD.Atanh(value);
		}

		public static double Acoth(double value)
		{
			return 0.5 * System.Math.Log((value + 1) / (value - 1), System.Math.E);
		}
		public static ComplexD Acoth(ComplexD value)
		{
			return ComplexD.Acoth(value);
		}

		public static double Asech(double value)
		{
			return Acosh(1 / value);
		}
		public static ComplexD Asech(ComplexD value)
		{
			return ComplexD.Asech(value);
		}

		public static double Acsch(double value)
		{
			return Asinh(1 / value);
		}
		public static ComplexD Acsch(ComplexD value)
		{
			return ComplexD.Acsch(value);
		}
		#endregion

		#region ApproxEquals
		/// <summary>
		/// Tests whether two single-precision floating point numbers are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="a">A single-precision floating point number.</param>
		/// <param name="b">A single-precision floating point number.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEquals(float a, float b)
		{
			return (System.Math.Abs(a - b) <= EpsilonF);
		}
		/// <summary>
		/// Tests whether two single-precision floating point numbers are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="a">A single-precision floating point number.</param>
		/// <param name="b">A single-precision floating point number.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEquals(float a, float b, float tolerance)
		{
			return (System.Math.Abs(a - b) <= tolerance);
		}
		/// <summary>
		/// Tests whether two double-precision floating point numbers are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="a">A double-precision floating point number.</param>
		/// <param name="b">A double-precision floating point number.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEquals(double a, double b)
		{
			return (System.Math.Abs(a - b) <= EpsilonD);
		}
		/// <summary>
		/// Tests whether two double-precision floating point numbers are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="a">A double-precision floating point number.</param>
		/// <param name="b">A double-precision floating point number.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns><see langword="true"/> if the two vectors are approximately equal; otherwise, <see langword="false"/>.</returns>
		public static bool ApproxEquals(double a, double b, double tolerance)
		{
			return (System.Math.Abs(a - b) <= tolerance);
		}
		#endregion

		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T temp = lhs;
			lhs = rhs;
			rhs = temp;
		}
	}
}
