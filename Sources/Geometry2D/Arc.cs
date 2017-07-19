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
    /// Represents a arc in 2D space.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SegmentConverter))]
    public struct Arc : ICloneable
    {
        #region Private fields
        private Vector2D _pCenter;
        private double _radius, _angle0, _angle1;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        /// <param name="pCenter">A <see cref="Vector2D"/> instance marking the <see cref="Arc"/>'s center point.</param>
        /// <param name="radius">The <see cref="Arc"/>'s radius.</param>
        /// <param name="angle0">The <see cref="Arc"/>'s starting angle.</param>
        /// <param name="angle1">The <see cref="Arc"/>'s ending angle.</param>
        public Arc(Vector2D pCenter, float radius, float angle0, float angle1)
        {
            _pCenter = pCenter;
            _radius = radius;
            _angle0 = angle0;
            _angle1 = angle1;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        /// <param name="arc">A <see cref="Arc"/> instance.</param>
        public Arc(Arc arc)
        {
            _pCenter = arc._pCenter;
            _radius = arc._radius;
            _angle0 = arc._angle0;
            _angle1 = arc._angle1;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the <see cref="Center"/>'s center point.
        /// </summary>
        public Vector2D Center
        {
            get { return _pCenter; }
            set { _pCenter = value; }
        }
        /// <summary>
        /// Gets or sets the <see cref="Arc"/>'s radius
        /// </summary>
        public double Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        /// <summary>
        /// Gets or sets the <see cref="Arc"/>'s starting angle.
        /// </summary>
        public double Angle0
        {
            get { return _angle0; }
            set { _angle0 = value; }
        }
        /// <summary>
        /// Gets or sets the <see cref="Arc"/>'s ending angle.
        /// </summary>
        public double Angle1
        {
            get { return _angle0; }
            set { _angle0 = value; }
        }
        /// <summary>
        /// Gets the <see cref="Arc"/>'s starting point.
        /// </summary>
        public Vector2D P0
        {
            get { return PointAtAngle(_angle0); }
        }
        /// <summary>
        /// Gets the <see cref="Arc"/>'s ending point.
        /// </summary>
        public Vector2D P1
        {
            get { return PointAtAngle(_angle1); }
        }
        #endregion

        #region Transform
        /// <summary>
        /// Transform
        /// </summary>
        /// <param name="transf"><see cref="Transform2D"/> used</param>
        public void Transform(Transform2D transf)
        {
            Vector2D p0Transf = transf.transform(P0);
            Vector2D p1Transf = transf.transform(P1);
            _pCenter = transf.transform(_pCenter);
            _angle0 = AngleAtPoint(p0Transf);
            _angle1 = AngleAtPoint(p1Transf);
        }
        #endregion

        #region Specific methods
        /// <summary>
        /// Returns the arc point for a given angle
        /// </summary>
        /// <param name="angle">Angle in degree</param>
        /// <returns><see cref="Vector2D"/> marking intermediate point</returns>
        public Vector2D PointAtAngle(double angle)
        {
            double angleRad = angle * System.Math.PI / 180.0;
            return _pCenter + _radius * (new Vector2D(System.Math.Cos(angleRad), System.Math.Sin(angleRad)));
        }

        public double AngleAtPoint(Vector2D p)
        {
            Vector2D pO = p - _pCenter;
            if (pO.X > 0)
                return -180.0 * (System.Math.Asin(pO.Y/pO.GetLength()) / System.Math.PI);
            else
            {
                if (pO.Y > 0)
                    return 180.0 * ((System.Math.PI + System.Math.Asin(pO.Y/pO.GetLength())) / System.Math.PI);
                else
                    return 180.0 * ((System.Math.PI - System.Math.Acos(-pO.Y/pO.GetLength())) / System.Math.PI);            
            }
        }

        public List<Segment> Explode(int iStepNumber)
        {
            List<Segment> list = new List<Segment>();
            double stepAngle = (_angle1 - _angle0) / iStepNumber;
            for (int iStep = 0; iStep < iStepNumber; ++iStep)
                list.Add( new Segment(PointAtAngle(_angle0 + iStep * stepAngle), PointAtAngle(_angle0 + (iStep+1) * stepAngle)) );
            return list;
        }
        #endregion

        #region ICloneable members
        /// <summary>
        /// Creates an exact copy of this <see cref="Arc"/> object.
        /// </summary>
        /// <returns>The <see cref="Arc"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Arc(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Arc"/> object.
        /// </summary>
        /// <returns>The <see cref="Arc"/> object this method creates.</returns>
        public Arc Clone()
        {
            return new Arc(this);
        }
        #endregion

        #region Public Static Parse Methods
        /// <summary>
        /// Converts the specified string to its <see cref="Arc"/> equivalent.
        /// </summary>
        /// <param name="value">A string representation of a <see cref="Arc"/></param>
        /// <returns>A <see cref="Arc"/> that represents the vector specified by the <paramref name="value"/> parameter.</returns>
        public static Arc Parse(string value)
        {
            Regex r = new Regex(@"\((?<pC>\([^\)]*\)), (?<r>\([^\)]*\)), (?<a0>\([^\)]*\)), ((?<a1>\([^\)]*\))\)", RegexOptions.None);
            Match m = r.Match(value);
            if (m.Success)
            {
                return new Arc(
                    Vector2D.Parse(m.Result("${pC}")),
                    float.Parse(m.Result("${r}")),
                    float.Parse(m.Result("${a0}")),
                    float.Parse(m.Result("${a1}"))
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
            return _pCenter.GetHashCode() ^ _radius.GetHashCode() ^ _angle0.GetHashCode() ^ _angle1.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to
        /// the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Segment"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Arc)
            {
                Arc a = (Arc)obj;
                return ((_pCenter == a._pCenter) && (_radius == a._radius) && (_angle0 == a._angle0) && (_angle1 == a._angle1));
            }
            return false;
        }
        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return string.Format("Arc(Center={0}, Radius={1}, Angle0={2}, Angle1={3})", _pCenter, _angle0, _angle1);
        }
        #endregion

        #region Comparison Operators
        /// <summary>
        /// Tests whether two specified arcs are equal.
        /// </summary>
        /// <param name="left">A <see cref="Arc"/> instance.</param>
        /// <param name="right">A <see cref="Arc"/> instance.</param>
        /// <returns><see langword="true"/> if the two segments are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Arc left, Arc right)
        {
            return ValueType.Equals(left, right);
        }
        /// <summary>
        /// Tests whether two specified arcs are not equal.
        /// </summary>
        /// <param name="left">A <see cref="Arc"/> instance.</param>
        /// <param name="right">A <see cref="Arc"/> instance.</param>
        /// <returns><see langword="true"/> if the two segments are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Arc left, Arc right)
        {
            return !ValueType.Equals(left, right);
        }
        #endregion
    }

    #region ArcConverter class
    /// <summary>
    /// Converts a <see cref="Arc"/> to and from string representation.
    /// </summary>
    public class ArcConverter : ExpandableObjectConverter
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
                Arc l = (Arc)value;
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
                return Arc.Parse((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
    #endregion
}
