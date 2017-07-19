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

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Analysis
{
	/// <summary>
	/// Defines an interface for classes that performs integration of a single-precision floating-point function over an interval.
	/// </summary>
	public interface IFloatIntegrator
	{
		/// <summary>
		/// Integrates a given function within the given integral.
		/// </summary>
		/// <param name="f">The function to integrate.</param>
		/// <param name="a">The lower limit.</param>
		/// <param name="b">The higher limit.</param>
		/// <returns>
		/// The integral of <paramref name="function"/> over the interval from <paramref name="a"/> to <paramref name="b"/>
		/// </returns>
		float Integrate(MathFunctions.UnaryFunction<float> f, float a, float b);
	}

	/// <summary>
	/// Defines an interface for classes that performs integration of a double-precision floating-point function over an interval.
	/// </summary>
	public interface IDoubleIntegrator
	{
		/// <summary>
		/// Integrates a given function within the given integral.
		/// </summary>
		/// <param name="f">The function to integrate.</param>
		/// <param name="a">The lower limit.</param>
		/// <param name="b">The higher limit.</param>
		/// <returns>
		/// The integral of <paramref name="function"/> over the interval from <paramref name="a"/> to <paramref name="b"/>
		/// </returns>
		double Integrate(MathFunctions.UnaryFunction<double> f, double a, double b);
	}

	/// <summary>
	/// Defines an interface for classes that perform differentiation of a single-precision floating-point function at a point.
	/// </summary>
	public interface IFloatDifferentiator
	{
		/// <summary>
		/// Differentiates the given function at a given point.
		/// </summary>
		/// <param name="f">The function to differentiate.</param>
		/// <param name="x">The point to differentiate at.</param>
		/// <returns>The derivative of function at <paramref name="x"/>.</returns>
		double Differentiate(MathFunctions.UnaryFunction<float> f, float x);
	}

	/// <summary>
	/// Defines an interface for classes that perform differentiation of a double-precision floating-point function at a point.
	/// </summary>
	public interface IDoubleDifferentiator
	{
		/// <summary>
		/// Differentiates the given function at a given point.
		/// </summary>
		/// <param name="f">The function to differentiate.</param>
		/// <param name="x">The point to differentiate at.</param>
		/// <returns>The derivative of function at <paramref name="x"/>.</returns>
		double Differentiate(MathFunctions.UnaryFunction<double> f, double x);
	}

}
