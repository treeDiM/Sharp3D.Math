#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using Sharp3D.Math.Core;
#endregion

namespace Sharp3D.Math.Geometry2D
{
    public class VerticalDistance
    {
        public static bool PointToAboveSegment(Vector2D p, Segment seg, ref double distance)
        {
            double xMin = System.Math.Min(seg.P0.X, seg.P1.X);
            double xMax = System.Math.Max(seg.P0.X, seg.P1.X);

            if (p.X < xMin - MathFunctions.EpsilonF || p.X > xMax + MathFunctions.EpsilonF)
                return false;
            else
            {
                double yMin = System.Math.Min(seg.P0.Y, seg.P1.Y);
                double yMax = System.Math.Max(seg.P0.Y, seg.P1.Y);

                if (System.Math.Abs(seg.P1.X - seg.P0.X) < MathFunctions.EpsilonF)
                {
                    if (p.Y < yMin)
                        distance = yMin - p.Y;
                    else if (p.Y > yMax)
                        distance = yMax - p.Y;
                    else
                        distance = 0.0f;
                }
                else
                    distance = seg.P0.Y + (p.X - seg.P0.X) * (seg.P1.Y - seg.P0.Y) / (seg.P1.X - seg.P0.X) - p.Y;
                return true;
            }
        }

        public static bool PointToAboveArc(Vector2D p, Arc arc, ref double distance)
        {
            bool success = false;
            distance = double.MaxValue;
            IList<Segment> list = arc.Explode(20);
            foreach (Segment seg in list)
            {
                double dist = 0.0f;
                if (PointToAboveSegment(p, seg, ref dist) && dist > 0)
                {
                    distance = System.Math.Min(dist, distance);
                    success = true;
                }
            }
            return success;
        }

        public static bool SegmentToAboveSegment(Segment seg, Segment segAbove, ref double distance)
        {
            bool success = false;
            distance = double.MaxValue;
            double dist = 0.0;
            if (PointToAboveSegment(seg.P0, segAbove, ref dist) && dist >= 0.0)
            {
                distance = System.Math.Min(distance, dist);
                success = true;
            }
            if (PointToAboveSegment(seg.P1, segAbove, ref dist) && dist >= 0.0)
            {
                distance = System.Math.Min(distance, dist);
                success = true;
            }
            if (PointToAboveSegment(segAbove.P0, seg, ref dist) && dist <= 0.0)
            {
                distance = System.Math.Min(distance, -dist);
                success = true;
            }
            if (PointToAboveSegment(segAbove.P1, seg, ref dist) && dist <= 0.0)
            {
                distance = System.Math.Min(distance, -dist);
                success = true;
            }
            return success;
        }

        public static bool SegmentToAboveArc(Segment seg, Arc arcAbove, ref double distance)
        {
            bool success = false;
            distance = double.MaxValue;
            List<Segment> listArc = arcAbove.Explode(20);

            foreach (Segment segArc in listArc)
            {
                double dist = 0.0f;
                if (PointToAboveSegment(seg.P0, segArc, ref dist) && dist > 0)
                {
                    distance = System.Math.Min(distance, dist);
                    success = true;
                }
                if (PointToAboveSegment(seg.P1, segArc, ref dist) && dist > 0)
                {
                    distance = System.Math.Min(distance, dist);
                    success = true;
                }
                if (PointToAboveSegment(segArc.P0, seg, ref dist) && dist < 0)
                {
                    distance = System.Math.Min(distance, -dist);
                    success = true;
                }
                if (PointToAboveSegment(segArc.P1, seg, ref dist) && dist < 0)
                {
                    distance = System.Math.Min(distance, -dist);
                    success = true;
                }
            }
            return success;
        }

        public static bool ArcToAboveSegment(Arc arc, Segment segAbove, ref double distance)
        {
            bool success = false;
            distance = double.MaxValue;
            List<Segment> listArc = arc.Explode(20);

            foreach (Segment segArc in listArc)
            {
                double dist = double.MaxValue;
                if (PointToAboveSegment(segArc.P0, segAbove, ref dist) && dist > 0)
                {
                    distance = System.Math.Min(distance, dist);
                    success = true;
                }
                if (PointToAboveSegment(segArc.P1, segAbove, ref dist) && dist > 0)
                {
                    distance = System.Math.Min(distance, dist);
                    success = true;
                }
                if (PointToAboveSegment(segAbove.P0, segArc, ref dist) && dist < 0)
                {
                    distance = System.Math.Min(distance, -dist);
                    success = true;
                }
                if (PointToAboveSegment(segAbove.P1, segArc, ref dist) && dist < 0)
                {
                    distance = System.Math.Min(distance, -dist);
                    success = true;
                }
            }
            return success;
        }

        public static bool ArcToAboveArc(Arc arc1, Arc arc2, ref double distance)
        {
            bool success = false;
            distance = double.MaxValue;
            List<Segment> listArc1 = arc1.Explode(20);
            List<Segment> listArc2 = arc2.Explode(20);

            foreach (Segment seg1 in listArc1)
                foreach (Segment seg2 in listArc2)
                {
                    double dist = double.MaxValue;
                    if (VerticalDistance.SegmentToAboveSegment(seg1, seg2, ref dist) && dist > 0)
                    {
                        distance = System.Math.Min(distance, dist);
                        success = true;
                    }
                }
            return success;
        }
    }
}
