#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Sharp3D.Math.Core
{
    /// <summary>
    /// Represents a 3D transformation
    /// </summary>
    [Serializable]
    public class Transform3D : ICloneable
    {
        #region Private fields
        /// <summary>
        /// The <see cref="Matrix4D"/> 3D transformation matrix
        /// </summary>
        Matrix4D _mat = Matrix4D.Identity;
        #endregion

        #region Enums
        public enum Axis
        { 
            X
            , Y
            , Z
        }
        #endregion

        #region Constructors
        public Transform3D()
        { 
        }
        public Transform3D(Matrix4D mat)
        {
            _mat = mat.Clone();
        }
        public Transform3D(Transform3D transf)
        {
            _mat = transf._mat.Clone();
        }
        #endregion

        #region Transform3D members
        public Vector3D transform(Vector3D vec)
        {
            Vector4D vecTransf = _mat * (new Vector4D(vec.X, vec.Y, vec.Z, 1.0));
            return new Vector3D(vecTransf.X, vecTransf.Y, vecTransf.Z);
        }
        /// <summary>
        /// Transform vector using rotation part only of matrix
        /// </summary>
        /// <param name="vec">input vector</param>
        /// <returns></returns>
        public Vector3D transformRot(Vector3D vec)
        {
            Vector4D vecTransf = MatrixRot * (new Vector4D(vec.X, vec.Y, vec.Z, 1.0));
            return new Vector3D(vecTransf.X, vecTransf.Y, vecTransf.Z);
        }

        public Transform3D Inverse()
        {
            double m00 = _mat.M11;//m[0][0];
            double m01 = _mat.M12;//m[0][1];
            double m02 = _mat.M13;//m[0][2];
            double m03 = _mat.M14;//m[0][3];

            double m10 = _mat.M21; //m[1][0];
            double m11 = _mat.M22; //m[1][1];
            double m12 = _mat.M23; //m[1][2];
            double m13 = _mat.M24; //m[1][3];

            double m20 = _mat.M31; //m[2][0];
            double m21 = _mat.M32; //m[2][1];
            double m22 = _mat.M33; //m[2][2];
            double m23 = _mat.M34; //m[2][3];

            double t4 = m00 * m11;
            double t6 = m00 * m21;
            double t8 = m10 * m01;
            double t10 = m10 * m21;
            double t12 = m20 * m01;
            double t14 = m20 * m11;
            double d = (t4 * m22 - t6 * m12 - t8 * m22 + t10 * m02 + t12 * m12 - t14 * m02);

            if (d == 0.0)
                throw new Exception("Matrix determinant is 0.0");
            double t17 = 1 / d;

            double t20 = m21 * m02;
            double t23 = m01 * m12;
            double t24 = m11 * m02;
            double t43 = m20 * m02;
            double t46 = m00 * m12;
            double t47 = m10 * m02;
            double t51 = m00 * m13;
            double t54 = m10 * m03;
            double t57 = m20 * m03;
            double inv00 = (m11 * m22 - m21 * m12) * t17;
            double inv01 = -(m01 * m22 - t20) * t17;
            double inv02 = (t23 - t24) * t17;
            double inv03 =
              -(t23 * m23 - m01 * m13 * m22 - t24 * m23 + m11 * m03 * m22 + t20 * m13 - m21 * m03 * m12) * t17;
            double inv10 = -(m10 * m22 - m20 * m12) * t17;
            double inv11 = (m00 * m22 - t43) * t17;
            double inv12 = -(t46 - t47) * t17;
            double inv13 = (t46 * m23 - t51 * m22 - t47 * m23 + t54 * m22 + t43 * m13 - t57 * m12) * t17;
            double inv20 = (t10 - t14) * t17;
            double inv21 = -(t6 - t12) * t17;
            double inv22 = (t4 - t8) * t17;
            double inv23 = -(t4 * m23 - t51 * m21 - t8 * m23 + t54 * m21 + t12 * m13 - t57 * m11) * t17;

            return new Transform3D(new Matrix4D(inv00, inv01, inv02, inv03,
                     inv10, inv11, inv12, inv13,
                     inv20, inv21, inv22, inv23,
                     0, 0, 0, 1));
        }
        #endregion

        #region ICloneable members
        /// <summary>
        /// Creates an exact copy of this <see cref="MatrixTransform3D"/> object.
        /// </summary>
        /// <returns>The <see cref="Transform3D"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Transform3D(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="MatrixTransform3D"/> object.
        /// </summary>
        /// <returns>The <see cref="MatrixTransform3D"/> object this method creates.</returns>
        public Transform3D Clone()
        {
            return new Transform3D(this);
        }
        #endregion

        #region Public properties
        public Matrix4D Matrix
        {
            get { return _mat; }
            set { _mat = value; }
        }
        public Matrix4D MatrixRot
        {
            get
            {
                return new Matrix4D(
                    _mat.M11, _mat.M12, _mat.M13, 0.0
                    , _mat.M21, _mat.M22, _mat.M23, 0.0
                    , _mat.M31, _mat.M32, _mat.M33, 0.0
                    , _mat.M41, _mat.M42, _mat.M43, 1.0);
            }
        }
        public Vector3D Rotations
        {
            get
            {
                return new Vector3D(
                    System.Math.Atan2(_mat.M32, _mat.M33) * 180.0 / System.Math.PI,
                    System.Math.Asin(-_mat.M31) * 180.0 / System.Math.PI,
                    System.Math.Atan2(_mat.M21, _mat.M11) * 180.0 / System.Math.PI
                    );
            }
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Creates a translation transformation from a given <see cref="Vector3D"/>.
        /// </summary>
        /// <param name="vecTrans">A translation <see cref="Vector3D"/>.</param>
        /// <returns>A <see cref="Transform3D"/> object.</returns>
        public static Transform3D Translation(Vector3D vecTrans)
        {
            Matrix4D matrixTransf = Matrix4D.Identity;
            matrixTransf.M14 = vecTrans.X;
            matrixTransf.M24 = vecTrans.Y;
            matrixTransf.M34 = vecTrans.Z;
            return new Transform3D(matrixTransf);
        }
        /// <summary>
        /// Creates a rotation a rotation around X, Y or Z axis transformation 
        /// </summary>
        /// <param name="axis">Axis to rotate about</param>
        /// <param name="angle">A double angle value</param>
        /// <returns>A <see cref="Transform3D"/> object</returns>
        public static Transform3D Rotation(Axis axis, double angle)
        {
            double angleRad = angle * System.Math.PI / 180.0;
            double cosAngle = System.Math.Cos(angleRad);
            double sinAngle = System.Math.Sin(angleRad);

            Matrix4D matrix = Matrix4D.Identity;

            switch (axis)
            {
                case Axis.X:
                    matrix.M22 = matrix.M33 = cosAngle;
                    matrix.M32 = sinAngle;
                    matrix.M23 = -sinAngle;
                    break;
                case Axis.Y:
                    matrix.M11 = matrix.M33 = cosAngle;
                    matrix.M31 = sinAngle;
                    matrix.M13 = -sinAngle;
                    break;
                case Axis.Z:
                    matrix.M11 = matrix.M22 = cosAngle;
                    matrix.M21 = sinAngle;
                    matrix.M12 = -sinAngle;
                    break;
                default:
                    break;
            }
            return new Transform3D(matrix);
        }

        public static Transform3D RotationX(double angle)
        {
            double a = angle * System.Math.PI / 180.0;
            return new Transform3D(
                new Matrix4D(
                    1.0, 0.0, 0.0, 0.0,
                    0.0, System.Math.Cos(a), -System.Math.Sin(a), 0.0,
                    0.0, System.Math.Sin(a), System.Math.Cos(a), 0.0,
                    0.0, 0.0, 0.0, 1.0
                )
            );
        }

        public static Transform3D RotationY(double angle)
        {
            double a = angle * System.Math.PI / 180.0;
            return new Transform3D(
                new Matrix4D(
                     System.Math.Cos(a), 0.0, System.Math.Sin(a), 0.0,
                     0.0, 1.0, 0.0, 0.0,
                     -System.Math.Sin(a), 0.0, System.Math.Cos(a), 0.0,
                     0.0, 0.0, 0.0, 1.0
                )
            );
        }

        public static Transform3D RotationZ(double angle)
        {
            double a = angle * System.Math.PI / 180.0;
            return new Transform3D(
                new Matrix4D(
                     System.Math.Cos(a), -System.Math.Sin(a), 0.0, 0.0,
                     System.Math.Sin(a), System.Math.Cos(a), 0.0, 0.0,
                     0.0, 0.0, 1.0, 0.0,
                     0.0, 0.0, 0.0, 1.0
                )
            );
        }
        /// <summary>
        /// Creates a rotation around a given axis
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> that defines the rotation axis direction</param>
        /// <param name="angle">A double angle value in degree</param>
        /// <returns>A <see cref="Transform3D"/> object</returns>
        public static Transform3D Rotation(Vector3D u, double angle)
        {
            u.Normalize();
            double angleRad = angle * System.Math.PI;
            double c = System.Math.Cos(angleRad);
            double s = System.Math.Sin(angleRad);

            Matrix4D m = Matrix4D.Identity;

            m.M11 = u.X * u.X + (1.0 - u.X * u.X) * c;
            m.M12 = u.X * u.Y * (1.0 - c) - u.Z * s;
            m.M13 = u.X * u.Z * (1.0 - c) + u.Y * s;

            m.M21 = u.Y * u.X * (1.0 - c) + u.Z * s;
            m.M22 = u.Y * u.Y * (1.0 - u.Y * u.Y) * c;
            m.M21 = u.Y * u.Z * (1.0 - c) - u.X * s;

            m.M31 = u.Z * u.X * (1.0 - c) + u.Y * s;
            m.M32 = u.Z * u.Y * (1.0 - c) + u.X * s;
            m.M33 = u.Z * u.Z * (1.0 - u.Z * u.Z) * c;

            return new Transform3D(m);
        }
        /// <summary>
        /// Perspective
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Transform3D Perspective(double d)
        {
            Matrix4D m = Matrix4D.Identity;
            m.M43 = 1.0 / d;
            return new Transform3D(m);
        }
        #endregion

        #region Projections
        public static Transform3D OrthographicProjection(Vector3D viewportMin, Vector3D viewportMax, double[] sizeMin, double[] sizeMax)
        {
            Matrix4D m = Matrix4D.Identity;
            m.M11 = (sizeMax[0] - sizeMin[0]) / (viewportMax.X - viewportMin.X);
            m.M22 = (sizeMax[1] - sizeMin[1]) / (viewportMax.Y - viewportMin.Y);
            m.M33 = (sizeMax[2] - sizeMin[2]) / (viewportMax.Z - viewportMin.Z);
            m.M14 = (sizeMin[0] * viewportMax.X - sizeMax[0] * viewportMin.X) / (viewportMax.X - viewportMin.X);
            m.M24 = (sizeMin[1] * viewportMax.Y - sizeMax[1] * viewportMin.Y) / (viewportMax.Y - viewportMin.Y);
            m.M34 = (sizeMin[2] * viewportMax.Z - sizeMax[2] * viewportMin.Z) / (viewportMax.Z - viewportMin.Z);

            return new Transform3D(m);
        }
        #endregion

        #region Binary operators
        public static Transform3D operator *(Transform3D transf1, Transform3D transf2)
        {
            return new Transform3D(transf1._mat * transf2._mat);
        }
        #endregion

        #region Constants
        public static readonly Transform3D Identity = new Transform3D(Matrix4D.Identity);

        #endregion
    }
}
