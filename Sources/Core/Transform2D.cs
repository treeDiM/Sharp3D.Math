#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
#endregion

namespace Sharp3D.Math.Core
{
    /// <summary>
    /// Represents a 2D transformation
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Transform2D: ICloneable
    {
        #region Private fields
        /// <summary>
        /// The <see cref="Matrix3D"/> 2D transformation matrix
        /// </summary>
        Matrix3D _mat = Matrix3D.Identity;
        #endregion

        #region Constructors
        public Transform2D()
        {
        }
        public Transform2D(Matrix3D mat)
        {
            _mat = mat.Clone();
        }
        public Transform2D(Transform2D transf)
        {
            _mat = transf._mat.Clone();
        }
        #endregion

        #region Transform2D members
        /// <summary>
        /// Transforms a point
        /// </summary>
        /// <param name="vec">A <see cref="Vector2D"/> object defining a point to be transformed</param>
        /// <returns>A <see cref="Vector2D"/> point</returns>
        public Vector2D transform(Vector2D vec)
        { 
            Vector3D vecTranf = _mat * (new Vector3D(vec.X, vec.Y, 1.0));
            return new Vector2D(vecTranf.X, vecTranf.Y);
        }
        public double transform(double angle)
        {
            if (_mat.M21 > 0)
            {
                if (_mat.M11 > 0)
                    return angle + System.Math.Asin(_mat.M21) * 180.0 / PI;
                else
                    return angle + 180.0 - System.Math.Asin(_mat.M21) * 180.0 / PI;
            }
            else
            {
                if (_mat.M11 > 0)
                    return angle + System.Math.Asin(_mat.M21) * 180.0 / PI;
                else
                    return angle + 180.0 - System.Math.Asin(_mat.M21) * 180.0 / PI;
            }
        }
        public Transform2D Inverse()
        {
            return new Transform2D(Matrix3D.Inverse(_mat));
        }
        #endregion

        #region ICloneable members
        /// <summary>
        /// Creates an exact copy of this <see cref="MatrixTransform2D"/> object.
        /// </summary>
        /// <returns>The <see cref="Transform2D"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Transform2D(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="MatrixTransform2D"/> object.
        /// </summary>
        /// <returns>The <see cref="MatrixTransform2D"/> object this method creates.</returns>
        public Transform2D Clone()
        {
            return new Transform2D(this);
        }
        #endregion

        #region Public properties
        public Matrix3D Matrix
        {
            get { return _mat; }
            set { _mat = value; }
        }
        public bool HasReflection
        {
            get { return _mat.M11 * _mat.M22 * _mat.M33 < 0.0; }
        }
        public Vector2D TranslationVector
        {
            get {   return new Vector2D(_mat.M13, _mat.M23);    }
        }

        public double RotationAngle
        {
            get
            {
                double angle = System.Math.Acos(_mat.M11) * 180.0 / PI;
                if (System.Math.Sin(angle) * _mat.M21 < 0)
                    angle = 360.0 - angle;
                return angle;
            }
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Create a translation transformation from a given vector
        /// </summary>
        /// <param name="vecTrans">A translation <see cref="Vector2D"/>.</param>
        /// <returns>A <see cref="Transform2D"/> object</returns>
        public static Transform2D Translation(Vector2D vecTrans)
        {
            Matrix3D matrixTransf = Matrix3D.Identity;
            matrixTransf.M13 = vecTrans.X;
            matrixTransf.M23 = vecTrans.Y;
            return new Transform2D(matrixTransf);
        }
        /// <summary>
        /// Creates a rotation around the origin transformation 
        /// </summary>
        /// <param name="angle">A double rotation angle in degree</param>
        /// <returns>A <see cref="Transform2D"/> object</returns>
        public static Transform2D Rotation(double angle)
        {
            double angleRad = angle * System.Math.PI / 180.0;
            double cosAngle = System.Math.Cos(angleRad);
            double sinAngle = System.Math.Sin(angleRad);
            return new Transform2D(new Matrix3D(new Vector3D(cosAngle, sinAngle, 0.0), new Vector3D(-sinAngle, cosAngle, 0.0), Vector3D.ZAxis));
        }
        /// <summary>
        /// Creates a rotation around a point transformation
        /// </summary>
        /// <param name="pt">A <see cref="Vector2D"/> object</param>
        /// <param name="angle">A double angle value in degree</param>
        /// <returns>A <see cref="Transform2D"/> object</returns>
        public static Transform2D Rotation(Vector2D pt, double angle)
        {
            double angleRad = angle * System.Math.PI / 180.0;
            double cosAngle = System.Math.Cos(angleRad);
            double sinAngle = System.Math.Sin(angleRad);
            return new Transform2D(
                new Matrix3D(Vector3D.Zero, Vector3D.Zero, new Vector3D(pt.X, pt.Y, 0.0))
                * new Matrix3D(new Vector3D(cosAngle, sinAngle, 0.0), new Vector3D(-sinAngle, cosAngle, 0.0), Vector3D.Zero)
                * new Matrix3D(Vector3D.Zero, Vector3D.Zero, new Vector3D(-pt.X, -pt.Y, 0.0))
                );     
        }
        #endregion

        #region Binary operators
        /// <summary>
        /// Computes the composed transformation from 2 transformations
        /// </summary>
        /// <param name="transf1">A first <see cref="Transform2D"/> object</param>
        /// <param name="transf2">A second <see cref="Transform2D"/> object</param>
        /// <returns>The composed <see cref="Transform2D"/> object</returns>
        public static Transform2D operator *(Transform2D transf1, Transform2D transf2)
        {
            return new Transform2D(transf1._mat * transf2._mat);
        }
        #endregion

        #region Constants
        /// <summary>
        /// Indentity transformation
        /// </summary>
        public static readonly Transform2D Identity = new Transform2D(Matrix3D.Identity);
        /// <summary>
        /// Reflection about axis X
        /// </summary>
        public static readonly Transform2D ReflectionX = new Transform2D(new Matrix3D(new double[] { 1.0, 0.0, 0.0, 0.0, -1.0, 0.0, 0.0, 0.0, 1.0 }));
        /// <summary>
        /// Reflection about axis Y
        /// </summary>
        public static readonly Transform2D ReflectionY = new Transform2D(new Matrix3D(new double[] { -1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0 }));
        /// <summary>
        /// PI number (constant)
        /// </summary>
        private static readonly double PI = System.Math.PI;
        #endregion
    }
}
