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
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Sharp3D.Math.Core;
#endregion

namespace Sharp3D.Math.Geometry2D
{
	/// <summary>
	/// Represents a circle in 2D space.
	/// </summary>
	/// <remarks>
	///	A circle is defined by a center point C and a radius r > 0.
	/// The <i>parametric form</i> is X(t) = C + r * f(t) where f(t) = (cost, sint) for t is in [0, 2PI).
	/// </remarks>
	[Serializable]
	[TypeConverter(typeof(CircleConverter))]
	public struct Circle : ICloneable
	{
		#region Private Fields
		private Vector2F _c;
		private float _r;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Circle"/> class using center and radius values.
		/// </summary>
		/// <param name="center">The circle's center point.</param>
		/// <param name="radius">The circle's radius.</param>
		public Circle(Vector2F center, float radius)
		{
			_c = center;
			_r = radius;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Circle"/> class using values from another circle instance.
		/// </summary>
		/// <param name="circle">A <see cref="Circle"/> instance to take values from.</param>
		public Circle(Circle circle)
		{
			_c = circle._c;
			_r = circle._r;
		}
		#endregion

		#region Constants
		/// <summary>
		/// Unit circle.
		/// </summary>
		public static readonly Circle UnitCircle = new Circle(new Vector2F(0.0f, 0.0f), 1.0f);
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		object ICloneable.Clone()
		{
			return new Circle(this);
		}
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public Circle Clone()
		{
			return new Circle(this);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="Circle"/> equivalent.
		/// </summary>
		/// <param name="s">A string representation of a <see cref="Circle"/></param>
		/// <returns>A <see cref="Circle"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
		public static Circle Parse(string s)
		{
			Regex r = new Regex(@"Circle\(Center=(?<center>\([^\)]*\)), Radius=(?<radius>.*)\)", RegexOptions.None);
			Match m = r.Match(s);
			if (m.Success)
			{
				return new Circle(
					Vector2F.Parse(m.Result("${center}")),
					float.Parse(m.Result("${radius}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		#endregion

		#region System.Object Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _c.GetHashCode() ^ _r.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Circle"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Circle)
			{
				Circle c = (Circle)obj;
				return
					(_c == c._c) && (_r == c._r);
			}
			return false;
		}
		/// <summary>
		/// Convert <see cref="Circle"/> to a string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("({0}, {1})", _c, _r);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The circle's center.
		/// </summary>
		public Vector2F Center
		{
			get
			{
				return _c;
			}
			set
			{
				_c = value;
			}
		}
		/// <summary>
		/// The circle's radius.
		/// </summary>
		public float Radius
		{
			get
			{
				return _r;
			}
			set
			{
				_r = value;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Calculates the area of the circle.
		/// </summary>
		/// <returns>Returns the area of the circle.</returns>
		/// <remarks>
		/// The area of a circle is PI*radius*radius.
		/// </remarks>
		public float GetArea()
		{
			return ((float)MathFunctions.PI) * _r * _r;
		}
		#endregion
	}

	#region CircleConverter class
	/// <summary>
	/// Converts a <see cref="Circle"/> to and from string representation.
	/// </summary>
	public class CircleConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
		/// <returns><b>true</b> if this converter can perform the conversion; otherwise, <b>false</b>.</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}
		/// <summary>
		/// Returns whether this converter can convert the object to the specified type, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
		/// <returns><b>true</b> if this converter can perform the conversion; otherwise, <b>false</b>.</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;

			return base.CanConvertTo(context, destinationType);
		}
		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> object. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <param name="destinationType">The Type to convert the <paramref name="value"/> parameter to.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if ((destinationType == typeof(string)) && (value is Circle))
			{
				Circle c = (Circle)value;
				return c.ToString();
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
		/// <summary>
		/// Converts the given object to the type of this converter, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture. </param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		/// <exception cref="ParseException">Failed parsing from string.</exception>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				return Circle.Parse((string)value);
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
	#endregion
}
