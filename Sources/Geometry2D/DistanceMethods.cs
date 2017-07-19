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
	/// Provides methods for computing diatnces between geometric primitives
	/// in 2D space.
	/// </summary>
	public static class DistanceMethods
	{
		#region Point-Point
		/// <summary>
		/// Calculates the squared distance between two points.
		/// </summary>
		/// <param name="p0">A <see cref="Vector2D"/> instance.</param>
		/// <param name="p1">A <see cref="Vector2D"/> instance.</param>
		/// <returns>The squared distance between the two points.</returns>
		public static double SquaredDistance(Vector2D p0, Vector2D p1)
		{
			Vector2D diff = p0 - p1;
			return diff.GetLengthSquared();
		}
		/// <summary>
		/// Calculates the distance between two points.
		/// </summary>
		/// <param name="p0">A <see cref="Vector2D"/> instance.</param>
		/// <param name="p1">A <see cref="Vector2D"/> instance.</param>
		/// <returns>The distance between the two points.</returns>
		public static double Distance(Vector2D p0, Vector2D p1)
		{
			Vector2D diff = p0 - p1;
			return diff.GetLength();
		}
		#endregion

		#region Point-Segment
		/// <summary>
		/// Calculates the squared distance between a point and a segment.
		/// </summary>
		/// <param name="point">A <see cref="Vector2D"/> instance.</param>
		/// <param name="segment">A <see cref="Segment"/> instance.</param>
		/// <returns>The squared distance between the point and the segment.</returns>
		public static double SquaredDistance(Vector2D point, Segment segment)
		{
            Vector2D D = segment.P1 - segment.P0;
            Vector2D diffPointP0 = point - segment.P0;
            double t = Vector2D.DotProduct(D, diffPointP0);

			if (t <= 0)
			{
				// segment.P0 is the closest to point.
                return Vector2D.DotProduct(diffPointP0, diffPointP0);
			}

            double DdD = Vector2D.DotProduct(D, D);
			if (t >= DdD)
			{
				// segment.P1 is the closest to point.
                Vector2D diffPointP1 = point - segment.P1;
                return Vector2D.DotProduct(diffPointP1, diffPointP1);
			}
			
			// Closest point is inside the segment.
            return Vector2D.DotProduct(diffPointP0, diffPointP0) - t * t / DdD;
		}
		/// <summary>
		/// Calculates the squared distance between a point and a segment.
		/// </summary>
		/// <param name="point">A <see cref="Vector2D"/> instance.</param>
		/// <param name="segment">A <see cref="Segment"/> instance.</param>
		/// <returns>The squared distance between the point and the segment.</returns>
		public static double Distance(Vector2D point, Segment segment)
		{
			return System.Math.Sqrt(SquaredDistance(point, segment));
		}
		#endregion

		#region Point-Ray
		/// <summary>
		/// Calculates the squared distance between a given point and a given ray.
		/// </summary>
		/// <param name="point">A <see cref="Vector2D"/> instance.</param>
		/// <param name="ray">A <see cref="Ray"/> instance.</param>
		/// <returns>The squared distance between the point and the ray.</returns>
		public static double SquaredDistance(Vector2D point, Ray ray)
		{
			Vector2D diff = point - ray.Origin;
            double t = Vector2D.DotProduct(diff, ray.Direction);

			if (t <= 0.0f)
			{
				return diff.GetLengthSquared();
			}
			else
			{
				t = (t * t) / ray.Direction.GetLengthSquared();
				return diff.GetLengthSquared() - t;
			}
		}
		/// <summary>
		/// Calculates the distance between a given point and a given ray.
		/// </summary>
		/// <param name="point">A <see cref="Vector2D"/> instance.</param>
		/// <param name="ray">A <see cref="Ray"/> instance.</param>
		/// <returns>The distance between the point and the ray.</returns>
		public static float Distance(Vector2D point, Ray ray)
		{
			return (float)System.Math.Sqrt(SquaredDistance(point, ray));
		}
		#endregion

		#region Point-Oriented Box
		/// <summary>
		/// Calculates the squared distance between a point and an oriented box.
		/// </summary>
		/// <param name="point">A <see cref="Vector2D"/> instance.</param>
		/// <param name="box">An <see cref="OrientedBox"/> instance.</param>
		/// <returns>The squared distance between the point and the oriented box.</returns>
		public static float SquaredDistance(Vector2D point, OrientedBox box)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Calculates the distance between a point and an oriented box.
		/// </summary>
		/// <param name="point">A <see cref="Vector2D"/> instance.</param>
		/// <param name="box">An <see cref="OrientedBox"/> instance.</param>
		/// <returns>The distance between the point and the oriented box.</returns>
		public static float Distance(Vector2D point, OrientedBox box)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Ray-Ray
		/// <summary>
		/// Calculates the squared distance between two rays.
		/// </summary>
		/// <param name="r0">A <see cref="Ray"/> instance.</param>
		/// <param name="r1">A <see cref="Ray"/> instance.</param>
		/// <returns>Returns the squared distance between two rays.</returns>
		public static float SquaredDistance(Ray r0, Ray r1)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Calculates the distance between two rays.
		/// </summary>
		/// <param name="r0">A <see cref="Ray"/> instance.</param>
		/// <param name="r1">A <see cref="Ray"/> instance.</param>
		/// <returns>Returns the distance between two rays.</returns>
		public static float Distance(Ray r0, Ray r1)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Segment-Segment
		/// <summary>
		/// Calculates the squared distance between two segments.
		/// </summary>
		/// <param name="s0">A <see cref="Segment"/> instance.</param>
		/// <param name="s1">A <see cref="Segment"/> instance.</param>
		/// <returns>Returns the squared distance between two segments.</returns>
		public static float SquaredDistance(Segment s0, Segment s1)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Calculates the distance between two segments.
		/// </summary>
		/// <param name="s0">A <see cref="Segment"/> instance.</param>
		/// <param name="s1">A <see cref="Segment"/> instance.</param>
		/// <returns>Returns the distance between two segments.</returns>
		public static float Distance(Segment s0, Segment s1)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
