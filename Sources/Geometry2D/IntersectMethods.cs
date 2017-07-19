using System;
using System.Collections.Generic;
using System.Text;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry2D
{
	#region Intersection2D
	public class Intersection2D
    {
        #region Enums
        public enum IntersectionType
		{
			I2D_NONE
			, I2D_POINT
			, I2D_SEGMENT
			, I2D_ARC
		};
        #endregion

        #region Private fields
        IntersectionType _iType;
		Vector2D _vec;
		Segment _seg;
		#endregion

		#region Setting intersection
		// no intersection
		public Intersection2D()
		{
			_iType = IntersectionType.I2D_NONE;
		}
		// point intersection
		public Intersection2D(Vector2D vec)
		{
			_iType = IntersectionType.I2D_POINT;
			_vec = vec;
		}
		// segment intersection
		public Intersection2D(Segment seg)
		{
			_iType = IntersectionType.I2D_SEGMENT;
			_seg = seg;
		}
		#endregion

		#region Accessing intersection data
		public IntersectionType Type
		{
			get { return _iType; }
		}
		public object Result
		{
			get
			{
				switch (_iType)
				{
					case IntersectionType.I2D_NONE: throw new Exception("No intersection!");
					case IntersectionType.I2D_POINT: return _vec;
					case IntersectionType.I2D_SEGMENT: return _seg;
					default: throw new NotImplementedException();
				}
			}
		}
		#endregion
	}
	#endregion

	public class IntersectMethods
	{
		#region Segment-Segment
        public static bool IntersectLines(Segment s0, Segment s1, out Intersection2D interObj)
        {
            //    (Ay-Cy)(Dx-Cx)-(Ax-Cx)(Dy-Cy)
            //r = -----------------------------  (eqn 1)
            //    (Bx-Ax)(Dy-Cy)-(By-Ay)(Dx-Cx)

            //    (Ay-Cy)(Bx-Ax)-(Ax-Cx)(By-Ay)
            //s = -----------------------------  (eqn 2)
            //    (Bx-Ax)(Dy-Cy)-(By-Ay)(Dx-Cx)

            double den = (s0.P1.X - s0.P0.X) * (s1.P1.Y - s1.P0.Y) - (s0.P1.Y - s0.P0.Y) * (s1.P1.X - s1.P0.X);
            double r = (s0.P0.Y - s1.P0.Y) * (s1.P1.X - s1.P0.X) - (s0.P0.X - s1.P0.X) * (s1.P1.Y - s1.P0.Y);
            double s = (s0.P0.Y - s1.P0.Y) * (s0.P1.X - s0.P0.X) - (s0.P0.X - s1.P0.X) * (s0.P1.Y - s0.P0.Y);

            // If the denominator in eqn 1 is zero, AB & CD are parallel
            if (System.Math.Abs(den) > MathFunctions.EpsilonF)
            {
                r /= den;
                s /= den;
                // Let P be the position vector of the intersection point, then
                // P=A+r(B-A)
                interObj = new Intersection2D(new Vector2D(s0.P0 + r * (s0.P1 - s0.P0)));
                return true;
            }
            else
            {
                // If the numerator in eqn 1 is also zero, AB & CD are collinear.
                if (System.Math.Abs(r) < MathFunctions.EpsilonF)
                {
                }
                else
                {
                }
            }
            interObj = new Intersection2D();
            return false;
        }

		public static bool Intersect(Segment s0, Segment s1, out Intersection2D interObj)
		{ 
		    //    (Ay-Cy)(Dx-Cx)-(Ax-Cx)(Dy-Cy)
		    //r = -----------------------------  (eqn 1)
		    //    (Bx-Ax)(Dy-Cy)-(By-Ay)(Dx-Cx)

		    //    (Ay-Cy)(Bx-Ax)-(Ax-Cx)(By-Ay)
		    //s = -----------------------------  (eqn 2)
		    //    (Bx-Ax)(Dy-Cy)-(By-Ay)(Dx-Cx)

            double den = (s0.P1.X - s0.P0.X) * (s1.P1.Y - s1.P0.Y) - (s0.P1.Y - s0.P0.Y) * (s1.P1.X - s1.P0.X);
            double r = (s0.P0.Y - s1.P0.Y) * (s1.P1.X - s1.P0.X) - (s0.P0.X - s1.P0.X) * (s1.P1.Y - s1.P0.Y);
            double s = (s0.P0.Y - s1.P0.Y) * (s0.P1.X - s0.P0.X) - (s0.P0.X - s1.P0.X) * (s0.P1.Y - s0.P0.Y);

			// If the denominator in eqn 1 is zero, AB & CD are parallel
			if (System.Math.Abs(den) < MathFunctions.EpsilonF)
			{
				// If the numerator in eqn 1 is also zero, AB & CD are collinear.
				if (System.Math.Abs(r) < MathFunctions.EpsilonF)
				{
					Interval i0 = new Interval(Interval.Type.Closed, System.Math.Min(s0.P0.X, s0.P1.X), System.Math.Max(s0.P0.X, s0.P1.X));
					Interval i1 = new Interval(Interval.Type.Closed, System.Math.Min(s1.P0.X, s1.P1.X), System.Math.Max(s1.P0.X, s1.P1.X));
					
					// check if interval overlaps
					if ((i0.Max < i1.Min) || (i1.Max < i0.Min))
					{
						interObj = new Intersection2D();
						return false;
					}
					else
					{
						Interval i2 = new Interval(Interval.Type.Closed, System.Math.Max(i0.Min, i1.Min), System.Math.Min(i0.Max, i1.Max));
						interObj = new Intersection2D(new Segment());
						return true;
					}
				}
				else
				{
					interObj = new Intersection2D();
					return false;
				}
			}
			else
			{ 
				r /= den;
				s /= den;
				// Let P be the position vector of the intersection point, then
				// P=A+r(B-A)
				if (0 <= r && r <= 1 && 0 <= s && s <= 1)
				{
					interObj = new Intersection2D(new Vector2D(s0.P0+r*(s0.P1-s0.P0)));
					return true;
				}
			}
			interObj = new Intersection2D();
			return false;
		}

        public static bool Intersect(Segment seg, Ray ray, out Intersection2D interObj)
        {
            //    (Ay-Cy)(Dx-Cx)-(Ax-Cx)(Dy-Cy)
            //r = -----------------------------  (eqn 1)
            //    (Bx-Ax)(Dy-Cy)-(By-Ay)(Dx-Cx)

            //    (Ay-Cy)(Bx-Ax)-(Ax-Cx)(By-Ay)
            //s = -----------------------------  (eqn 2)
            //    (Bx-Ax)(Dy-Cy)-(By-Ay)(Dx-Cx)

            double den = (seg.P1.X - seg.P0.X) * ray.Direction.Y - (seg.P1.Y - seg.P0.Y) * ray.Direction.X;
            double r = (seg.P0.Y - ray.Origin.Y) * ((ray.Origin + ray.Direction).X - ray.Origin.X) - (seg.P0.X - ray.Origin.X) * ((ray.Origin + ray.Direction).Y - ray.Origin.Y);

            // If the denominator in eqn 1 is zero, AB & CD are parallel
            if (System.Math.Abs(den) < MathFunctions.EpsilonF)
            {
                // If the numerator in eqn 1 is also zero, AB & CD are collinear.
                if (System.Math.Abs(r) < MathFunctions.EpsilonF)
                {
                    interObj = new Intersection2D(new Segment(seg));
                    return true;
                }
                else
                {
                    interObj = new Intersection2D();
                    return false;
                }
            }
            else
            {
                r /= den;
                // Let P be the position vector of the intersection point, then
                // P=A+r(B-A)
                if (0 <= r && r <= 1)
                {
                    interObj = new Intersection2D(new Vector2D(seg.P0 + r * (seg.P1 - seg.P0)));
                    return true;
                }
            }
            interObj = new Intersection2D();
            return false;
        }
		#endregion
	}
}
