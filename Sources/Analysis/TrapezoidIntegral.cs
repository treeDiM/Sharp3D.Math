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
	/// Approximates integrals of functions over an interval using the Trapezoid rule.
	/// </summary>
	public class TrapezoidIntegral : ICloneable, IDoubleIntegrator
	{
		public enum Method { Default, MidPoint }

		#region Private Fields
		private double _accuracy;
		private int _maxSteps;
		private Method _method;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TrapezoidIntegral"/> class using default values.
		/// </summary>
		/// <remarks>
		/// The default accuracy is 1.0e-4. 
		/// The default max iterations value is set to <see cref="int.MaxValue"/>.
		/// The default integration method is set to <see cref="TrapezoidIntegral.Method.Default"/>.
		/// </remarks>
		public TrapezoidIntegral()
		{
			_accuracy = 1.0e-4;
			_maxSteps = int.MaxValue;
			_method = Method.Default;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TrapezoidIntegral"/> class using the given values.
		/// </summary>
		/// <param name="accuracy">A double-precision floating-point number.</param>
		/// <param name="maxIterations">An integer representing the maximum number of iterations to use in the calculations.</param>
		/// <param name="method">A <see cref="TrapezoidIntegral.Method.Default"/> value.</param>
		public TrapezoidIntegral(double accuracy, int maxIterations, Method method)
		{
			_accuracy = accuracy;
			_maxSteps = maxIterations;
			_method = method;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TrapezoidIntegral"/> class using values from another instance.
		/// </summary>
		/// <param name="integrator">A <see cref="TrapezoidIntegral"/> instance.</param>
		public TrapezoidIntegral(TrapezoidIntegral integrator)
		{
			_accuracy = integrator._accuracy;
			_maxSteps = integrator._maxSteps;
			_method = integrator._method;
		}
		#endregion

		#region Public Properties
		public double Accuracy
		{
			get { return _accuracy; }
			set { _accuracy = value; }
		}
		public int MaxIterations
		{
			get { return _maxSteps; }
			set { _maxSteps = value; }
		}
		public Method IntegrationMethod
		{
			get { return _method; }
			set { _method = value; }
		}
		#endregion

		#region Private Helper Methods
		private double defaultIteration(MathFunctions.UnaryFunction<double> f, double a, double b, double estimate, int n)
		{
			double sum = 0;
			double stepSize = (b - a) / n;
			double x = a + (stepSize / 2);

			for (int i = 0; i < n; i++, x += stepSize)
				sum += f(x);

			return (estimate + (stepSize * sum)) / 2;
		}
		private double midPointIteration(MathFunctions.UnaryFunction<double> f, double a, double b, double estimate, int n)
		{
			double sum = 0;
			double stepSize = (b - a) / n;
			double stepSizeMid = stepSize * (2/3);
			double x = a + (stepSize / 6);

			for (int i = 0; i < n; i++, x += stepSize)
			{
				sum += f(x) + f(x + stepSizeMid);
			}

			return (estimate + (stepSize*sum))/3;
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="TrapezoidIntegral"/> object.
		/// </summary>
		/// <returns>The <see cref="TrapezoidIntegral"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new TrapezoidIntegral(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="TrapezoidIntegral"/> object.
		/// </summary>
		/// <returns>The <see cref="TrapezoidIntegral"/> object this method creates.</returns>
		public TrapezoidIntegral Clone()
		{
			return new TrapezoidIntegral(this);
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
			if (a == b)
				return 0;

			if (a > b)
				return -Integrate(f, b, a);

			// Start with the crudest estimate
			int n = 1;
			double estimate = ((f(a) + f(b)) * (b - a)) / 2.0;
			double newEstimate = 0;

			int i = 1;
			do
			{
				switch (_method)
				{
					case Method.Default:
						newEstimate = defaultIteration(f, a, b, estimate, n);
						n *= 2;
						break;
					case Method.MidPoint:
						newEstimate = midPointIteration(f, a, b, estimate, n);
						n *= 3;
						break;
				}

				// Check accuracy
				if (System.Math.Abs(newEstimate - estimate) <= _accuracy)
					return newEstimate;

				estimate = newEstimate;
				i++;
			}
			while (i < _maxSteps);

			throw new MathException("Max number of iterations reached.");
		}
		#endregion
	}
}
