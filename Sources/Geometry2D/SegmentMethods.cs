#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sharp3D.Math.Core;
#endregion

namespace Sharp3D.Math.Geometry2D
{
    public class SegmentMethods
    {
        public static bool AreSegmentsColinear(Segment s0, Segment s1, double epsilon)
        {
            return IsPointCollinear(s0, s1.P0, epsilon) && IsPointCollinear(s0, s1.P1, epsilon);
        }
        public static bool IsPointCollinear(Segment s, Vector2D p, double epsilon)
        {
            return System.Math.Abs(GetSignedTriangleArea2(s, p, epsilon)) <= epsilon;
        }
        public static double GetSignedTriangleArea2(Segment s, Vector2D p, double epsilon)
        {
            Vector2D u = p - s.P0;
            double uLength = u.GetLength();
            if (uLength < epsilon) return 0.0;
            u = u * (1.0 / uLength);
            Vector2D v = s.P1 - s.P0;
            double vLength = v.GetLength();
            if (vLength < epsilon) return 0.0;
            v = v * (1.0 / vLength);
            return u.X * v.Y - u.Y * v.X;
        }
    }
}
