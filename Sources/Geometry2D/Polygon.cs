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
using System.Xml.Serialization;
using System.ComponentModel;

using Sharp3D.Math.Core;

#endregion

namespace Sharp3D.Math.Geometry2D
{
	/// <summary>
	/// Represents a polygon in 2D space.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	class Polygon : ICloneable
	{
		#region Private fields
		private List<Vector2F> _points = new List<Vector2F>();
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon"/> class.
		/// </summary>
		public Polygon()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon"/> class.
		/// </summary>
		/// <param name="points">An array of <see cref="Vector2F"/> instances.</param>
		public Polygon(Vector2F[] points)
		{
			_points.AddRange(points);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon"/> class.
		/// </summary>
		/// <param name="points">A list containing <see cref="Vector2F"/> instances.</param>
		public Polygon(List<Vector2F> points)
		{
			_points.AddRange(points);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Polygon"/> classu sing coordinates from another instance.
		/// </summary>
		/// <param name="polygon">A <see cref="Polygon"/> instance.</param>
		public Polygon(Polygon polygon)
		{
			_points.AddRange(polygon._points);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the polygon's list of points.
		/// </summary>
		[XmlArrayItem(Type = typeof(Vector2F))]
		public List<Vector2F> Points
		{
			get { return _points; }
		}
		/// <summary>
		/// Gets the number of vertices in the polygon.
		/// </summary>
		public int Count
		{
			get
			{
				return _points.Count;
			}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Polygon"/> object.
		/// </summary>
		/// <returns>The <see cref="Polygon"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Polygon(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Polygon"/> object.
		/// </summary>
		/// <returns>The <see cref="Polygon"/> object this method creates.</returns>
		public Polygon Clone()
		{
			return new Polygon(this);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Flips the polygon.
		/// </summary>
		public void Flip()
		{
			_points.Reverse();
		}
		#endregion
	}
}
