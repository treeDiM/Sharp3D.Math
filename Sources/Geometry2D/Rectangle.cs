#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Sharp3D.Math.Core;
#endregion

namespace Sharp3D.Math.Geometry2D
{
    [Serializable]
    public struct Rectangle : ICloneable
    {
        #region Private Fields
        private Vector2D _ptOrigin;
        private Vector2D _dimensions;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(Vector2D ptOrigin, Vector2D dimensions)
        {
            _ptOrigin = ptOrigin;
            _dimensions = dimensions;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class using an existing <see cref="Segment"/> instance.
        /// </summary>
        public Rectangle(Rectangle r)
        {
            _ptOrigin = r._ptOrigin;
            _dimensions = r._dimensions;
        }
        #endregion

        #region Public properties
        public Segment[] Segments
        {
            get
            {
                // points
                Vector2D P0 = new Vector2D(_ptOrigin);
                Vector2D P1 = new Vector2D(_ptOrigin + _dimensions.X * Vector2D.XAxis);
                Vector2D P2 = new Vector2D(_ptOrigin + _dimensions);
                Vector2D P3 = new Vector2D(_ptOrigin + _dimensions.Y * Vector2D.YAxis);
                // segments
                Segment[] segments = new Segment[4];
                segments[0] = new Segment(P0, P1);
                segments[1] = new Segment(P1, P2);
                segments[2] = new Segment(P2, P3);
                segments[3] = new Segment(P3, P0);
                return segments; 
            }
        }
        public Vector2D Origin
        {
            get { return _ptOrigin; }
        }
        public Vector2D Dimensions
        {
            get { return _dimensions; }
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Rectangle"/> object.
        /// </summary>
        /// <returns>The <see cref="Rectangle"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Rectangle(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Rectangle"/> object.
        /// </summary>
        /// <returns>The <see cref="Rectangle"/> object this method creates.</returns>
        public Rectangle Clone()
        {
            return new Rectangle(this);
        }
        #endregion

        #region System.Object Overrides
        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _ptOrigin.GetHashCode() ^ _dimensions.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to
        /// the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Segment"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Rectangle)
            {
                Rectangle l = (Rectangle)obj;
                return ((_ptOrigin == l._ptOrigin) && (_dimensions == l._dimensions));
            }
            return false;
        }
        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Rectangle(Origin={0}, Dimensions={1})", _ptOrigin, _dimensions);
        }
        #endregion
    }

    public class Hatching
    {
        #region Private Fields
        private Rectangle _rOuter, _rHole;
        private bool _hasHole = false;
        #endregion

        #region Constructors
        public Hatching(Rectangle rOuter)
        {
            _rOuter = rOuter; 
        }
        public Hatching(Rectangle rOuter, Rectangle rHole)
        {
            _rOuter = rOuter;
            _rHole = rHole;
            _hasHole = true;
        }
        #endregion

        #region PointComparer
        public class PointComparer : IComparer<Vector2D>
        {
            #region Data members
            private Vector2D _ptOrigin, _direction;
            #endregion
            #region Constructor
            public PointComparer(Vector2D ptOrigin, Vector2D direction)
            {
                _ptOrigin = ptOrigin;
                _direction = direction;
            }
            #endregion
            #region IComparer<Box> implementation
            public int Compare(Vector2D pt1, Vector2D pt2)
            {
                return Vector2D.DotProduct(pt1 - _ptOrigin, _direction) < Vector2D.DotProduct(pt2 - _ptOrigin, _direction) ? -1 : 1;
            }
            #endregion
        }
        #endregion

        #region Public properties
        public Segment[] GetHatchingSegments(double angle, double spacing)
        {
            // direction
            double angleRad = angle * System.Math.PI / 180.0;
            Vector2D dir = new Vector2D(System.Math.Cos(angleRad), System.Math.Sin(angleRad));
            Vector2D dirOrtho = new Vector2D(-dir.Y, dir.X);
            // number of steps
            int noSteps = Convert.ToInt32( Vector2D.DotProduct(_rOuter.Dimensions, dir) / spacing );
            Segment[] sOuter = _rOuter.Segments;
            Segment[] sHole = _rHole.Segments;
            List<Segment> segments = new List<Segment>();

            // list of loop segments
            foreach (Segment s in sOuter)
                segments.Add(s);
            foreach (Segment s in sHole)
                segments.Add(s);
            // build hatching segments
            for (int i = 0; i < noSteps; ++i)
            { 
                // build ray
                Vector2D pt = _rOuter.Origin + i * spacing * dir;
                Ray ray = new Ray(pt, dirOrtho);

                List<Vector2D> listPoints = new List<Vector2D>();
                foreach (Segment s in sOuter)
                {
                    Intersection2D interObj;
                    if (IntersectMethods.Intersect(s, ray, out interObj))
                        listPoints.Add((Vector2D)interObj.Result );
                }

                if (_hasHole && listPoints.Count >= 2)
                {
                    Intersection2D interObjHole;
                    foreach (Segment sh in sHole)
                    {
                        if (IntersectMethods.Intersect(sh, ray, out interObjHole))
                            listPoints.Add((Vector2D)interObjHole.Result);
                    }
                }
                // sort points
                listPoints.Sort(new PointComparer(pt, dirOrtho));

                // add segments
                if (listPoints.Count >= 2)
                    segments.Add(new Segment(listPoints[0], listPoints[1]));
                if (listPoints.Count >= 4)
                    segments.Add(new Segment(listPoints[2], listPoints[3]));
            }
            return segments.ToArray();
        }
        #endregion
    }
}
