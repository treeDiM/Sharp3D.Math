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

namespace Sharp3D.Math.Analysis
{
	/// <summary>
	/// Approximates integrals of functions over an interval using the Simpson integration method.
	/// </summary>
	public class SimpsonIntegral : ICloneable, IFloatIntegrator, IDoubleIntegrator
	{
		#region Private Fields
		private int _stepCount = 100;
		#endregion

		#region Constructors
		/// <summary>
		/// Initialize a new instance of the <see cref="SimpsonIntegral"/> class using default integration steps number.
		/// </summary>
		/// <remarks>
		/// The default integration steps number is 100.
		/// </remarks>
		public SimpsonIntegral()
		{
		}
		/// <summary>
		/// Initialize a new instance of the <see cref="SimpsonIntegral"/> class.
		/// </summary>
		/// <param name="steps">The number of steps to use for the integration.</param>
		public SimpsonIntegral(int steps)
		{
			_stepCount = steps;
		}
		/// <summary>
		/// Initialize a new instance of the <see cref="SimpsonIntegral"/> class using values from another <see cref="SimpsonIntegral"/> instance.
		/// </summary>
		/// <param name="simpson">A <see cref="SimpsonIntegral"/> instance.</param>
		public SimpsonIntegral(SimpsonIntegral simpson)
		{
			_stepCount = simpson._stepCount;
		}


		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating the number of steps used for the integration.
		/// </summary>
		/// <value></value>
		public int Steps
		{
			get { return _stepCount; }
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="SimpsonIntegral"/> object.
		/// </summary>
		/// <returns>The <see cref="SimpsonIntegral"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new SimpsonIntegral(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="SimpsonIntegral"/> object.
		/// </summary>
		/// <returns>The <see cref="SimpsonIntegral"/> object this method creates.</returns>
		public SimpsonIntegral Clone()
		{
			return new SimpsonIntegral(this);
		}
		#endregion

		#region IFloatIntegrator Members
		/// <summary>
		/// Integrates a given function within the given integral.
		/// </summary>
		/// <param name="f">The function to integrate.</param>
		/// <param name="a">The lower limit.</param>
		/// <param name="b">The higher limit.</param>
		/// <returns>
		/// The integral of <paramref name="function"/> over the interval from <paramref name="a"/> to <paramref name="b"/>
		/// </returns>
        public float Integrate(Sharp3D.Math.Core.MathFunctions.UnaryFunction<float> f, float a, float b)
		{
			if (a > b) return -Integrate(f, b, a);

			float sum = 0;
			float stepSize = (float)((b - a) / _stepCount);
			float stepSizeDiv3 = stepSize / 3.0f;
			for (int i = 0; i < _stepCount; i = i + 2)
			{
				sum += (f(a + i * stepSize) + 4.0f * f(a + (i + 1) * stepSize) + f(a + (i + 2) * stepSize)) * stepSizeDiv3;
			}

			return sum;
		}
		#endregion

		#region IDoubleIntegrator Members
		/// <summary>
		/// Integrates a given function within the given integral.
		/// </summary>
		/// <param name="f">The function to integrate.</param>
		/// <param name="a">The lower limit.</param>
		/// <param name="b">The higher limit.</param>
		/// <returns>
		/// The integral of <paramref name="function"/> over the interval from <paramref name="a"/> to <paramref name="b"/>
		/// </returns>
        public double Integrate(Sharp3D.Math.Core.MathFunctions.UnaryFunction<double> f, double a, double b)
		{
			if (a > b) return -Integrate(f, b, a);

			double sum = 0;
			double stepSize = (b - a) / _stepCount;
			double stepSizeDiv3 = stepSize / 3;
			for (int i = 0; i < _stepCount; i = i + 2)
			{
				sum += (f(a + i * stepSize) + 4 * f(a + (i + 1) * stepSize) + f(a + (i + 2) * stepSize)) * stepSizeDiv3;
			}

			return sum;
		}
		#endregion
	}
}
