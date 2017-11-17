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
using System.Globalization;

using Sharp3D.Math.Core;
#endregion

namespace Sharp3D.Math.Geometry2D
{
	/// <summary>
	/// Represents a segment in 2D space.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(SegmentConverter))]
	public struct Segment : ICloneable
	{
		#region Private Fields
		private Vector2D _p0;
		private Vector2D _p1;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Segment"/> class.
		/// </summary>
		/// <param name="p0">A <see cref="Vector2D"/> instance marking the segment's starting point.</param>
		/// <param name="p1">A <see cref="Vector2D"/> instance marking the segment's ending point.</param>
		public Segment(Vector2D p0, Vector2D p1)
		{
			_p0 = p0;
			_p1 = p1;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Segment"/> class using an existing <see cref="Segment"/> instance.
		/// </summary>
		/// <param name="segment">A <see cref="Segment"/> instance.</param>
		public Segment(Segment segment)
		{
			_p0 = segment.P0;
			_p1 = segment.P1;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the segment's first point.
		/// </summary>
		public Vector2D P0
		{
			get { return _p0; }
			set { _p0 = value; }
		}
		/// <summary>
		/// Gets or sets the segment's second point.
		/// </summary>
		public Vector2D P1
		{
			get { return _p1; }
			set { _p1 = value; }
		}
		#endregion

        #region Transform
        /// <summary>
        /// Transform
        /// </summary>
        /// <param name="transf"><see cref="Transform2D"/> used</param>
        public void Transform(Transform2D transf)
        {
            _p0 = transf.transform(_p0);
            _p1 = transf.transform(_p1);
        }
        #endregion

        #region ICloneable Members
        /// <summary>
		/// Creates an exact copy of this <see cref="Segment"/> object.
		/// </summary>
		/// <returns>The <see cref="Segment"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Segment(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Segment"/> object.
		/// </summary>
		/// <returns>The <see cref="Segment"/> object this method creates.</returns>
		public Segment Clone()
		{
			return new Segment(this);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="Segment"/> equivalent.
		/// </summary>
		/// <param name="value">A string representation of a <see cref="Segment"/></param>
		/// <returns>A <see cref="Segment"/> that represents the vector specified by the <paramref name="value"/> parameter.</returns>
		public static Segment Parse(string value)
		{
			Regex r = new Regex(@"\((?<p0>\([^\)]*\)), (?<p1>\([^\)]*\))\)", RegexOptions.None);
			Match m = r.Match(value);
			if (m.Success)
			{
				return new Segment(
					Vector2D.Parse(m.Result("${p0}")),
					Vector2D.Parse(m.Result("${p1}"))
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
			return _p0.GetHashCode() ^ _p1.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Segment"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Segment)
			{
				Segment l = (Segment)obj;
				return ((_p0 == l._p0) && (_p1 == l._p1));
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Segment(P0={0}, P1={1})", _p0, _p1);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified segments are equal.
		/// </summary>
		/// <param name="left">A <see cref="Segment"/> instance.</param>
		/// <param name="right">A <see cref="Segment"/> instance.</param>
		/// <returns><see langword="true"/> if the two segments are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(Segment left, Segment right)
		{
			return ValueType.Equals(left, right);
		}
		/// <summary>
		/// Tests whether two specified segments are not equal.
		/// </summary>
		/// <param name="left">A <see cref="Segment"/> instance.</param>
		/// <param name="right">A <see cref="Segment"/> instance.</param>
		/// <returns><see langword="true"/> if the two segments are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(Segment left, Segment right)
		{
			return !ValueType.Equals(left, right);
		}
		#endregion
	}

	#region SegmentConverter class
	/// <summary>
	/// Converts a <see cref="Segment"/> to and from string representation.
	/// </summary>
	public class SegmentConverter : ExpandableObjectConverter
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
			if ((destinationType == typeof(string)) && (value is Segment))
			{
				Segment l = (Segment)value;
				return l.ToString();
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
				return Segment.Parse((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
	#endregion
}
