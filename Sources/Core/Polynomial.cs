#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents a polynomial.
	/// </summary>
	/// <remarks>See http://mathworld.wolfram.com/Polynomial.html for further details.</remarks>
	[Serializable]
	public class Polynomial : IComparable, ICloneable
	{
		#region Private Fields
		private double[] _coefficients;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Polynomial"/> class with a specified order.
		/// </summary>
		/// <param name="order">The polynomial's order.</param>
		public Polynomial(int order)
		{
			_coefficients = new double[order + 1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polynomial"/> class with the given coefficients.
		/// </summary>
		/// <param name="coefficients">The polynomial's coefficients.</param>
		public Polynomial(double[] coefficients)
		{
			_coefficients = new double[coefficients.Length];
			for (int i = 0; i < coefficients.Length; i++)
			{
				_coefficients[i] = coefficients[i];
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polynomial"/> class with the given coefficients.
		/// </summary>
		/// <param name="coefficients">The polynomial's coefficients.</param>
		public Polynomial(List<double> coefficients)
		{
			_coefficients = new double[coefficients.Count];
			for (int i = 0; i < coefficients.Count; i++)
			{
				_coefficients[i] = coefficients[i];
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polynomial"/> class using values from a given <see cref="Polynomial"/> instance.
		/// </summary>
		/// <param name="polynomial">A <see cref="Polynomial"/> instance.</param>
		public Polynomial(Polynomial polynomial)
		{
			_coefficients = new double[polynomial._coefficients.Length];
			for (int i = 0; i < _coefficients.Length; i++)
			{
				_coefficients[i] = polynomial._coefficients[i];
			}
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the polynomial's order.
		/// </summary>
		/// <value>An <see cref="int"/> value.</value>
		public int Order
		{
			get
			{
				return _coefficients.Length - 1;
			}
			set
			{
				if (value == _coefficients.Length - 1)
					return;

				double[] temp = new double[value + 1];
				_coefficients.CopyTo(temp, 0);
				_coefficients = temp;
			}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Polynomial"/> object.
		/// </summary>
		/// <returns>The <see cref="Polynomial"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Polynomial(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Polynomial"/> object.
		/// </summary>
		/// <returns>The <see cref="Polynomial"/> object this method creates.</returns>
		public Polynomial Clone()
		{
			return new Polynomial(this);
		}
		#endregion

		#region IComparable Members
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
				throw new ArgumentException("Parameter cannot be null.", "obj");

			Polynomial p = obj as Polynomial;
			if (p == null)
				throw new ArgumentException("Type mismatch: polynomial expected.", "obj");

			return CompareTo(p);
		}
		public int CompareTo(Polynomial p)
		{
			int i = this._coefficients.Length - 1;
			int j = p._coefficients.Length - 1;

			while (i != j)
			{
				if (i > j)
				{
					if (_coefficients[i--] != 0)
						return 1;
				}
				else
				{
					if (p._coefficients[j--] != 0)
						return -1;
				}
			}

			while (i >= 0)
			{
				if (_coefficients[i] > p._coefficients[i])
					return 1;
				if (_coefficients[i] < p._coefficients[i])
					return -1;
				i--;
			}

			return 0;
		}
		#endregion

		#region Public Methods
		public Polynomial GetInverse()
		{
			int order = this.Order;
			Polynomial result = new Polynomial(order);
			for (int i = 0; i < _coefficients.Length; i++)
			{
				result._coefficients[i] = _coefficients[order - i];
			}

			return result;
		}
		public Polynomial GetDerivative()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Reduces the degree of the polynomial by eliminating all the coefficients that are close to zero
		/// and by making the leading coefficient one.
		/// </summary>
		/// <param name="tolerance">The threshold for testing coefficients against zero.</param>
		public void Compress(double tolerance)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Public Static Polynomial Arithmetics
		public static Polynomial Add(Polynomial left, Polynomial right)
		{
			Polynomial largeOrder, smallOrder;
			if (left.Order >= right.Order)
			{
				largeOrder = left;
				smallOrder = right;
			}
			else
			{
				largeOrder = right;
				smallOrder = left;
			}

			Polynomial result = new Polynomial(largeOrder);

			for (int i = 0; i < smallOrder._coefficients.Length; i++)
			{
				result._coefficients[i] += smallOrder._coefficients[i];
			}

			return result;
		}
		public static Polynomial Add(Polynomial p, double scalar)
		{
			throw new NotImplementedException();
		}
		public static void Add(Polynomial left, Polynomial right, Polynomial result)
		{
			Polynomial largeOrder, smallOrder;
			if (left.Order >= right.Order)
			{
				largeOrder = left;
				smallOrder = right;
			}
			else
			{
				largeOrder = right;
				smallOrder = left;
			}

			result.Order = largeOrder.Order;
			for (int i = 0; i < smallOrder._coefficients.Length; i++)
			{
				result._coefficients[i] = largeOrder._coefficients[i] + smallOrder._coefficients[i];
			}
			for (int i = smallOrder._coefficients.Length; i < largeOrder._coefficients.Length; i++)
			{
				result._coefficients[i] = largeOrder._coefficients[i];
			}
		}
		public static void Add(Polynomial p, double scalar, Polynomial result)
		{
			throw new NotImplementedException();
		}

		public static Polynomial Subtract(Polynomial left, Polynomial right)
		{
			throw new NotImplementedException();
		}
		public static Polynomial Subtract(Polynomial p, double scalar)
		{
			throw new NotImplementedException();
		}
		public static Polynomial Subtract(double scalar, Polynomial p)
		{
			throw new NotImplementedException();
		}
		public static void Subtract(Polynomial left, Polynomial right, Polynomial result)
		{
			Polynomial largeOrder, smallOrder;
			if (left.Order >= right.Order)
			{
				largeOrder = left;
				smallOrder = right;
			}
			else
			{
				largeOrder = right;
				smallOrder = left;
			}

			result.Order = largeOrder.Order;
			for (int i = 0; i < smallOrder._coefficients.Length; i++)
			{
				result._coefficients[i] = largeOrder._coefficients[i] + smallOrder._coefficients[i];
			}
			for (int i = smallOrder._coefficients.Length; i < largeOrder._coefficients.Length; i++)
			{
				result._coefficients[i] = largeOrder._coefficients[i];
			}
		}
		public static void Subtract(Polynomial p, double scalar, Polynomial result)
		{
			throw new NotImplementedException();
		}
		public static void Subtract(double scalar, Polynomial p, Polynomial result)
		{
			throw new NotImplementedException();
		}

		public static Polynomial Multiply(Polynomial left, Polynomial right)
		{
			throw new NotImplementedException();
		}
		public static Polynomial Multiply(Polynomial p, double scalar)
		{
			throw new NotImplementedException();
		}
		public static void Multiply(Polynomial left, Polynomial right, Polynomial result)
		{
			throw new NotImplementedException();
		}
		public static void Multiply(Polynomial p, double scalar, Polynomial result)
		{
			throw new NotImplementedException();
		}

		public static Polynomial Divide(Polynomial p, double scalar)
		{
			throw new NotImplementedException();
		}
		public static Polynomial Divide(double scalar, Polynomial p)
		{
			throw new NotImplementedException();
		}
		public static void Divide(Polynomial p, double scalar, Polynomial result)
		{
			throw new NotImplementedException();
		}
		public static void Divide(double scalar, Polynomial p, Polynomial result)
		{
			throw new NotImplementedException();
		}

		public static Polynomial Negate(Polynomial p)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region System.Object Overrides
		/// <summary>
		/// Returns a value indicating whether this instance is equal to the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Polynomial"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			Polynomial target = obj as Polynomial;
			if (obj == null)
				return false;

			return (CompareTo(target) == 0);
		}
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _coefficients.GetHashCode();
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < _coefficients.Length; i++)
			{
				if (_coefficients[i] == 0)
					continue;

				sb.Append((_coefficients[i] < 0) ? "-" : "+");

				sb.Append(_coefficients[i].ToString());
				sb.Append("x");

				if (i > 1)
				{
					sb.Append("^");
					sb.Append(i.ToString());
				}
			}

			return sb.ToString();
		}
		#endregion

		#region Comparison Operators
		public static bool operator ==(Polynomial left, Polynomial right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(Polynomial left, Polynomial right)
		{
			return !left.Equals(right);
		}
		public static bool operator >(Polynomial left, Polynomial right)
		{
			return left.CompareTo(right) == 1;
		}
		public static bool operator <(Polynomial left, Polynomial right)
		{
			return left.CompareTo(right) == -1;
		}
		public static bool operator >=(Polynomial left, Polynomial right)
		{
			return (left.CompareTo(right) >= 0);
		}
		public static bool operator <=(Polynomial left, Polynomial right)
		{
			return (left.CompareTo(right) <= 0); ;
		}
		#endregion

		#region Unary Operators
		public static Polynomial operator -(Polynomial polynomial)
		{
			return Polynomial.Negate(polynomial);
		}
		#endregion

		#region Binary Operators
		public static Polynomial operator +(Polynomial left, Polynomial right)
		{
			return Polynomial.Add(left, right);
		}
		public static Polynomial operator +(Polynomial polynomial, double scalar)
		{
			return Polynomial.Add(polynomial, scalar);
		}
		public static Polynomial operator +(double scalar, Polynomial polynomial)
		{
			return Polynomial.Add(polynomial, scalar);
		}

		public static Polynomial operator -(Polynomial left, Polynomial right)
		{
			return Polynomial.Subtract(left, right);
		}
		public static Polynomial operator -(Polynomial polynomial, double scalar)
		{
			return Polynomial.Subtract(polynomial, scalar);
		}
		public static Polynomial operator -(double scalar, Polynomial polynomial)
		{
			return Polynomial.Subtract(scalar, polynomial);
		}

		public static Polynomial operator *(Polynomial left, Polynomial right)
		{
			return Polynomial.Multiply(left, right);
		}
		public static Polynomial operator *(Polynomial polynomial, double scalar)
		{
			return Polynomial.Multiply(polynomial, scalar);
		}
		public static Polynomial operator *(double scalar, Polynomial polynomial)
		{
			return Polynomial.Multiply(polynomial, scalar);
		}

		public static Polynomial operator /(Polynomial polynomial, double scalar)
		{
			return Polynomial.Divide(polynomial, scalar);
		}
		public static Polynomial operator /(double scalar, Polynomial polynomial)
		{
			return Polynomial.Divide(scalar, polynomial);
		}
		#endregion

		#region Array indexing operator
		public double this[int index]
		{
			get
			{
				return _coefficients[index];
			}
			set
			{
				_coefficients[index] = value;
			}
		}
		#endregion
	}
}
