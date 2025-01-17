using System;
using QuaternionObjects.Serialization;

namespace QuaternionObjects
{
    /// <summary>
    /// Summary description for Quaternion.
    /// </summary>
    public struct Quaternion_big : IParsable
    {
        const double Epsilon = 1e-03f;

        public double W, X, Y, Z;

        private static readonly Quaternion_big IdentityQuaternion = new Quaternion_big(1.0f, 0.0f, 0.0f, 0.0f);
        private static readonly Quaternion_big ZeroQuaternion = new Quaternion_big(0.0f, 0.0f, 0.0f, 0.0f);
        private static readonly int[] Next = new int[] { 1, 2, 0 };

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion_big"/> struct.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Quaternion_big(double w, double x, double y, double z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }
        private Quaternion_big(string parsableText)
        {
            if (parsableText == null)
                throw new ArgumentException("Text cannot be null.");
            try
            {
                if (parsableText[0] == '[') //[pitch,yaw,roll], Rotation in euler angles in degrees
                {
                    string[] vals = parsableText.TrimStart('[').TrimEnd(']').Split(',');
                    Quaternion_big q = FromEulerAnglesInDegrees(double.Parse(vals[0]), double.Parse(vals[1]), double.Parse(vals[2]));
                    W = q.W;
                    X = q.X;
                    Y = q.Y;
                    Z = q.Z;
                }
                else
                {
                    string[] vals = parsableText.TrimStart('(').TrimEnd(')').Split(',');
                    W = double.Parse(vals[0]);
                    X = double.Parse(vals[1]);
                    Y = double.Parse(vals[2]);
                    Z = double.Parse(vals[3]);
                }
            }
            catch (Exception e)
            {
                throw new FormatException(string.Format("Could not parse Quaternion from '{0}'. " + e.Message, parsableText));
            }
        }
        /// <summary>
        /// Overload for creating from parsable text
        /// </summary>
        /// <remarks>The struct is used to prevent confusion with another overload of a class's constructor that may accept a string</remarks>
        /// <param name="data"></param>
        public Quaternion_big(ParsedData data)
            : this(data.Text)
        {
        }


        #region Operator Overloads + CLS compliant method equivalents

        /// <summary>
        /// Used to multiply 2 Quaternions together.
        /// </summary>
        /// <remarks>
        ///		Quaternion multiplication is not communative in most cases.
        ///		i.e. p*q != q*p
        /// </remarks>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion_big Multiply(Quaternion_big left, Quaternion_big right)
        {
            return left * right;
        }

        /// <summary>
        /// Used to multiply 2 Quaternions together.
        /// </summary>
        /// <remarks>
        ///		Quaternion multiplication is not communative in most cases.
        ///		i.e. p*q != q*p
        /// </remarks>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion_big operator *(Quaternion_big left, Quaternion_big right)
        {
            Quaternion_big q = new Quaternion_big();

            q.W = left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z;
            q.X = left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y;
            q.Y = left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z;
            q.Z = left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X;

            /*
            return new Quaternion
                (
                left.w * right.w - left.X * right.X - left.y * right.y - left.z * right.z,
                left.w * right.X + left.X * right.w + left.y * right.z - left.z * right.y,
                left.w * right.y + left.y * right.w + left.z * right.X - left.X * right.z,
                left.w * right.z + left.z * right.w + left.X * right.y - left.y * right.X
                ); */

            return q;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="quat"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Multiply(Quaternion_big quat, Vector3 vector)
        {
            return quat * vector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quat"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 operator *(Quaternion_big quat, Vector3 vector)
        {
            // nVidia SDK implementation
            Vector3 uv, uuv;
            Vector3 qvec = new Vector3(quat.X, quat.Y, quat.Z);

            uv = qvec.Cross(vector);
            uuv = qvec.Cross(uv);
            uv *= (2.0f * quat.W);
            uuv *= 2.0f;

            return vector + uv + uuv;

            // get the rotation matrix of the Quaternion and multiply it times the vector
            //return quat.ToRotationMatrix() * vector;
        }

        /// <summary>
        /// Used when a double value is multiplied by a Quaternion.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion_big Multiply(double scalar, Quaternion_big right)
        {
            return scalar * right;
        }

        /// <summary>
        /// Used when a double value is multiplied by a Quaternion.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion_big operator *(double scalar, Quaternion_big right)
        {
            return new Quaternion_big(scalar * right.W, scalar * right.X, scalar * right.Y, scalar * right.Z);
        }

        /// <summary>
        /// Used when a Quaternion is multiplied by a double value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Quaternion_big Multiply(Quaternion_big left, double scalar)
        {
            return left * scalar;
        }

        /// <summary>
        /// Used when a Quaternion is multiplied by a double value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Quaternion_big operator *(Quaternion_big left, double scalar)
        {
            return new Quaternion_big(scalar * left.W, scalar * left.X, scalar * left.Y, scalar * left.Z);
        }

        /// <summary>
        /// Used when a Quaternion is added to another Quaternion.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion_big Add(Quaternion_big left, Quaternion_big right)
        {
            return left + right;
        }

        /// <summary>
        /// Used when a Quaternion is added to another Quaternion.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion_big operator +(Quaternion_big left, Quaternion_big right)
        {
            return new Quaternion_big(left.W + right.W, left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        ///     Negates a Quaternion, which simply returns a new Quaternion
        ///     with all components negated.
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion_big operator -(Quaternion_big right)
        {
            return new Quaternion_big(-right.W, -right.X, -right.Y, -right.Z);
        }

        public static bool operator ==(Quaternion_big left, Quaternion_big right)
        {
            return (left.W == right.W && left.X == right.X && left.Y == right.Y && left.Z == right.Z);
        }

        public static bool operator !=(Quaternion_big left, Quaternion_big right)
        {
            return (left.W != right.W || left.X != right.X || left.Y != right.Y || left.Z != right.Z);
        }

        #endregion

        #region Properties

        /// <summary>
        ///    An Identity Quaternion.
        /// </summary>
        public static Quaternion_big Identity
        {
            get
            {
                return IdentityQuaternion;
            }
        }

        /// <summary>
        ///    A Quaternion with all elements set to 0.0f;
        /// </summary>
        public static Quaternion_big Zero
        {
            get
            {
                return ZeroQuaternion;
            }
        }

        /// <summary>
        ///		Squared 'length' of this quaternion.
        /// </summary>
        public double Norm
        {
            get
            {
                return X * X + Y * Y + Z * Z + W * W;
            }
        }

        /// <summary>
        ///    Local X-axis portion of this rotation.
        /// </summary>
        public Vector3 XAxis
        {
            get
            {
                double fTx = 2.0f * X;
                double fTy = 2.0f * Y;
                double fTz = 2.0f * Z;
                double fTwy = fTy * W;
                double fTwz = fTz * W;
                double fTxy = fTy * X;
                double fTxz = fTz * X;
                double fTyy = fTy * Y;
                double fTzz = fTz * Z;

                return new Vector3(1.0f - (fTyy + fTzz), fTxy + fTwz, fTxz - fTwy);
            }
        }

        /// <summary>
        ///    Local Y-axis portion of this rotation.
        /// </summary>
        public Vector3 YAxis
        {
            get
            {
                double fTx = 2.0f * X;
                double fTy = 2.0f * Y;
                double fTz = 2.0f * Z;
                double fTwx = fTx * W;
                double fTwz = fTz * W;
                double fTxx = fTx * X;
                double fTxy = fTy * X;
                double fTyz = fTz * Y;
                double fTzz = fTz * Z;

                return new Vector3(fTxy - fTwz, 1.0f - (fTxx + fTzz), fTyz + fTwx);
            }
        }

        /// <summary>
        ///    Local Z-axis portion of this rotation.
        /// </summary>
        public Vector3 ZAxis
        {
            get
            {
                double fTx = 2.0f * X;
                double fTy = 2.0f * Y;
                double fTz = 2.0f * Z;
                double fTwx = fTx * W;
                double fTwy = fTy * W;
                double fTxx = fTx * X;
                double fTxz = fTz * X;
                double fTyy = fTy * Y;
                double fTyz = fTz * Y;

                return new Vector3(fTxz + fTwy, fTyz - fTwx, 1.0f - (fTxx + fTyy));
            }
        }
        public double PitchInDegrees { get { return Functions.RadiansToDegrees(Pitch); } set { Pitch = Functions.DegreesToRadians(value); } }
        public double YawInDegrees { get { return Functions.RadiansToDegrees(Yaw); } set { Yaw = Functions.DegreesToRadians(value); } }
        public double RollInDegrees { get { return Functions.RadiansToDegrees(Roll); } set { Roll = Functions.DegreesToRadians(value); } }

        public double Pitch
        {
            set
            {
                double pitch, yaw, roll;
                ToEulerAngles(out pitch, out yaw, out roll);
                FromEulerAngles(value, yaw, roll);
            }
            get
            {

                double test = X * Y + Z * W;
                if (Math.Abs(test) > 0.499f) // singularity at north and south pole
                    return 0f;
                return (double)Math.Atan2(2 * X * W - 2 * Y * Z, 1 - 2 * X * X - 2 * Z * Z);
            }
        }


        public double Yaw
        {
            set
            {
                double pitch, yaw, roll;
                ToEulerAngles(out pitch, out yaw, out roll);
                FromEulerAngles(pitch, value, roll);
            }
            get
            {
                double test = X * Y + Z * W;
                if (Math.Abs(test) > 0.499f) // singularity at north and south pole
                    return Math.Sign(test) * 2 * (double)Math.Atan2(X, W);
                return (double)Math.Atan2(2 * Y * W - 2 * X * Z, 1 - 2 * Y * Y - 2 * Z * Z);
            }
        }
        public double Roll
        {
            set
            {

                double pitch, yaw, roll;
                ToEulerAngles(out pitch, out yaw, out roll);
                FromEulerAngles(pitch, yaw, value);
            }
            get
            {
                double test = X * Y + Z * W;
                if (Math.Abs(test) > 0.499f) // singularity at north and south pole
                    return Math.Sign(test) * Functions.PI / 2;
                return (double)Math.Asin(2 * test);
            }
        }


        #endregion

        #region Static methods

        public static Quaternion_big Parse(string text)
        {
            return new Quaternion_big(text);
        }

        public static Quaternion_big Slerp(double time, Quaternion_big quatA, Quaternion_big quatB)
        {
            return Slerp(time, quatA, quatB, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="quatA"></param>
        /// <param name="quatB"></param>
        /// <param name="useShortestPath"></param>
        /// <returns></returns>
        public static Quaternion_big Slerp(double time, Quaternion_big quatA, Quaternion_big quatB, bool useShortestPath)
        {
            double cos = quatA.Dot(quatB);

            double angle = Functions.ACos(cos);

            if (Functions.Abs(angle) < Epsilon)
            {
                return quatA;
            }

            double sin = Functions.Sin(angle);
            double inverseSin = 1.0f / sin;
            double coeff0 = Functions.Sin((1.0f - time) * angle) * inverseSin;
            double coeff1 = Functions.Sin(time * angle) * inverseSin;

            Quaternion_big result;

            if (cos < 0.0f && useShortestPath)
            {
                coeff0 = -coeff0;
                // taking the complement requires renormalisation
                Quaternion_big t = coeff0 * quatA + coeff1 * quatB;
                t.Normalize();
                result = t;
            }
            else
            {
                result = (coeff0 * quatA + coeff1 * quatB);
            }

            return result;
        }

        /// <summary>
        /// Creates a Quaternion from a supplied angle and axis.
        /// </summary>
        /// <param name="angle">Value of an angle in radians.</param>
        /// <param name="axis">Arbitrary axis vector.</param>
        /// <returns></returns>
        public static Quaternion_big FromAngleAxis(double angle, Vector3 axis)
        {
            Quaternion_big quat = new Quaternion_big();

            double halfAngle = 0.5f * angle;
            double sin = Functions.Sin(halfAngle);

            quat.W = Functions.Cos(halfAngle);
            quat.X = sin * axis.X;
            quat.Y = sin * axis.Y;
            quat.Z = sin * axis.Z;

            return quat;
        }

        public static Quaternion_big Squad(double t, Quaternion_big p, Quaternion_big a, Quaternion_big b, Quaternion_big q)
        {
            return Squad(t, p, a, b, q, false);
        }

        /// <summary>
        ///		Performs spherical quadratic interpolation.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion_big Squad(double t, Quaternion_big p, Quaternion_big a, Quaternion_big b, Quaternion_big q, bool useShortestPath)
        {
            double slerpT = 2.0f * t * (1.0f - t);

            // use spherical linear interpolation
            Quaternion_big slerpP = Slerp(t, p, q, useShortestPath);
            Quaternion_big slerpQ = Slerp(t, a, b);

            // run another Slerp on the results of the first 2, and return the results
            return Slerp(slerpT, slerpP, slerpQ);
        }

        #endregion

        #region Public methods

        #region Euler Angles
        public Vector3 ToEulerAnglesInDegrees()
        {
            double pitch, yaw, roll;
            ToEulerAngles(out pitch, out yaw, out roll);
            return new Vector3(Functions.RadiansToDegrees(pitch), Functions.RadiansToDegrees(yaw), Functions.RadiansToDegrees(roll));
        }
        public Vector3 ToEulerAngles()
        {
            double pitch, yaw, roll;
            ToEulerAngles(out pitch, out yaw, out roll);
            return new Vector3(pitch, yaw, roll);
        }
        public void ToEulerAnglesInDegrees(out double pitch, out double yaw, out double roll)
        {
            ToEulerAngles(out pitch, out yaw, out roll);
            pitch = Functions.RadiansToDegrees(pitch);
            yaw = Functions.RadiansToDegrees(yaw);
            roll = Functions.RadiansToDegrees(roll);
        }
        public void ToEulerAngles(out double pitch, out double yaw, out double roll)
        {

            double halfPi = (double)Math.PI / 2;
            double test = X * Y + Z * W;
            if (test > 0.499f)
            { // singularity at north pole
                yaw = 2 * (double)Math.Atan2(X, W);
                roll = halfPi;
                pitch = 0;
            }
            else if (test < -0.499f)
            { // singularity at south pole
                yaw = -2 * (double)Math.Atan2(X, W);
                roll = -halfPi;
                pitch = 0;
            }
            else
            {
                double sqx = X * X;
                double sqy = Y * Y;
                double sqz = Z * Z;
                yaw = (double)Math.Atan2(2 * Y * W - 2 * X * Z, 1 - 2 * sqy - 2 * sqz);
                roll = (double)Math.Asin(2 * test);
                pitch = (double)Math.Atan2(2 * X * W - 2 * Y * Z, 1 - 2 * sqx - 2 * sqz);
            }

            if (pitch <= double.Epsilon)
                pitch = 0f;
            if (yaw <= double.Epsilon)
                yaw = 0f;
            if (roll <= double.Epsilon)
                roll = 0f;
        }
        public static Quaternion_big FromEulerAnglesInDegrees(double pitch, double yaw, double roll)
        {
            return FromEulerAngles(Functions.DegreesToRadians(pitch), Functions.DegreesToRadians(yaw), Functions.DegreesToRadians(roll));
        }

        /// <summary>
        /// Combines the euler angles in the order yaw, pitch, roll to create a rotation quaternion
        /// </summary>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <param name="roll"></param>
        /// <returns></returns>
        public static Quaternion_big FromEulerAngles(double pitch, double yaw, double roll)
        {
            return FromAngleAxis(yaw, Vector3.UnitY)
                * FromAngleAxis(pitch, Vector3.UnitX)
                * FromAngleAxis(roll, Vector3.UnitZ);

            /*TODO: Debug
            //Equation from http://www.euclideanspace.com/maths/geometry/rotations/conversions/eulerToQuaternion/index.htm
            //heading
			
            double c1 = (double)Math.Cos(yaw/2);
            double s1 = (double)Math.Sin(yaw/2);
            //attitude
            double c2 = (double)Math.Cos(roll/2);
            double s2 = (double)Math.Sin(roll/2);
            //bank
            double c3 = (double)Math.Cos(pitch/2);
            double s3 = (double)Math.Sin(pitch/2);
            double c1c2 = c1*c2;
            double s1s2 = s1*s2;

            double w =c1c2*c3 - s1s2*s3;
            double x =c1c2*s3 + s1s2*c3;
            double y =s1*c2*c3 + c1*s2*s3;
            double z =c1*s2*c3 - s1*c2*s3;
            return new Quaternion(w,x,y,z);*/
        }

        #endregion

        /// <summary>
        /// Performs a Dot Product operation on 2 Quaternions.
        /// </summary>
        /// <param name="quat"></param>
        /// <returns></returns>
        public double Dot(Quaternion_big quat)
        {
            return W * quat.W + X * quat.X + Y * quat.Y + Z * quat.Z;
        }

        /// <summary>
        ///		Normalizes elements of this quaterion to the range [0,1].
        /// </summary>
        public void Normalize()
        {
            double factor = 1.0f / Functions.Sqrt(Norm);

            W = W * factor;
            X = X * factor;
            Y = Y * factor;
            Z = Z * factor;
        }

        /// <summary>
        ///    
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public void ToAngleAxis(ref double angle, ref Vector3 axis)
        {
            // The quaternion representing the rotation is
            //   q = cos(A/2)+sin(A/2)*(x*i+y*j+z*k)

            double sqrLength = X * X + Y * Y + Z * Z;

            if (sqrLength > 0.0f)
            {
                angle = 2.0f * Functions.ACos(W);
                double invLength = Functions.InvSqrt(sqrLength);
                axis.X = X * invLength;
                axis.Y = Y * invLength;
                axis.Z = Z * invLength;
            }
            else
            {
                angle = 0.0f;
                axis.X = 1.0f;
                axis.Y = 0.0f;
                axis.Z = 0.0f;
            }
        }

        /// <summary>
        /// Gets a 3x3 rotation matrix from this Quaternion.
        /// </summary>
        /// <returns></returns>
        public Matrix3 ToRotationMatrix()
        {
            Matrix3 rotation = new Matrix3();

            double tx = 2.0f * X;
            double ty = 2.0f * Y;
            double tz = 2.0f * Z;
            double twx = tx * W;
            double twy = ty * W;
            double twz = tz * W;
            double txx = tx * X;
            double txy = ty * X;
            double txz = tz * X;
            double tyy = ty * Y;
            double tyz = tz * Y;
            double tzz = tz * Z;

            rotation.m00 = 1.0f - (tyy + tzz);
            rotation.m01 = txy - twz;
            rotation.m02 = txz + twy;
            rotation.m10 = txy + twz;
            rotation.m11 = 1.0f - (txx + tzz);
            rotation.m12 = tyz - twx;
            rotation.m20 = txz - twy;
            rotation.m21 = tyz + twx;
            rotation.m22 = 1.0f - (txx + tyy);

            return rotation;
        }

        /// <summary>
        /// Computes the inverse of a Quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion_big Inverse()
        {
            double norm = W * W + X * X + Y * Y + Z * Z;
            if (norm > 0.0f)
            {
                double inverseNorm = 1.0f / norm;
                return new Quaternion_big(W * inverseNorm, -X * inverseNorm, -Y * inverseNorm, -Z * inverseNorm);
            }
            else
            {
                // return an invalid result to flag the error
                return Zero;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        public void ToAxes(out Vector3 xAxis, out Vector3 yAxis, out Vector3 zAxis)
        {
            xAxis = new Vector3();
            yAxis = new Vector3();
            zAxis = new Vector3();

            Matrix3 rotation = ToRotationMatrix();

            xAxis.X = rotation.m00;
            xAxis.Y = rotation.m10;
            xAxis.Z = rotation.m20;

            yAxis.X = rotation.m01;
            yAxis.Y = rotation.m11;
            yAxis.Z = rotation.m21;

            zAxis.X = rotation.m02;
            zAxis.Y = rotation.m12;
            zAxis.Z = rotation.m22;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        public void FromAxes(Vector3 xAxis, Vector3 yAxis, Vector3 zAxis)
        {
            Matrix3 rotation = new Matrix3();

            rotation.m00 = xAxis.X;
            rotation.m10 = xAxis.Y;
            rotation.m20 = xAxis.Z;

            rotation.m01 = yAxis.X;
            rotation.m11 = yAxis.Y;
            rotation.m21 = yAxis.Z;

            rotation.m02 = zAxis.X;
            rotation.m12 = zAxis.Y;
            rotation.m22 = zAxis.Z;

            // set this quaternions values from the rotation matrix built
            FromRotationMatrix(rotation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        public void FromRotationMatrix(Matrix3 matrix)
        {
            // Algorithm in Ken Shoemake's article in 1987 SIGGRAPH course notes
            // article "Quaternion Calculus and Fast Animation".

            double trace = matrix.m00 + matrix.m11 + matrix.m22;

            double root = 0.0f;

            if (trace > 0.0f)
            {
                // |this.w| > 1/2, may as well choose this.w > 1/2
                root = Functions.Sqrt(trace + 1.0f);  // 2w
                W = 0.5f * root;

                root = 0.5f / root;  // 1/(4w)

                X = (matrix.m21 - matrix.m12) * root;
                Y = (matrix.m02 - matrix.m20) * root;
                Z = (matrix.m10 - matrix.m01) * root;
            }
            else
            {
                // |this.w| <= 1/2

                int i = 0;
                if (matrix.m11 > matrix.m00)
                    i = 1;
                if (matrix.m22 > matrix[i, i])
                    i = 2;

                int j = Next[i];
                int k = Next[j];

                root = Functions.Sqrt(matrix[i, i] - matrix[j, j] - matrix[k, k] + 1.0f);

                unsafe
                {
                    fixed (double* apkQuat = &X)
                    {
                        apkQuat[i] = 0.5f * root;
                        root = 0.5f / root;

                        W = (matrix[k, j] - matrix[j, k]) * root;

                        apkQuat[j] = (matrix[j, i] + matrix[i, j]) * root;
                        apkQuat[k] = (matrix[k, i] + matrix[i, k]) * root;
                    }
                }
            }
        }

        /// <summary>
        ///		Calculates the logarithm of a Quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion_big Log()
        {
            // BLACKBOX: Learn this
            // If q = cos(A)+sin(A)*(x*i+y*j+z*k) where (x,y,z) is unit length, then
            // log(q) = A*(x*i+y*j+z*k).  If sin(A) is near zero, use log(q) =
            // sin(A)*(x*i+y*j+z*k) since sin(A)/A has limit 1.

            // start off with a zero quat
            Quaternion_big result = Zero;

            if (Functions.Abs(W) < 1.0f)
            {
                double angle = Functions.ACos(W);
                double sin = Functions.Sin(angle);

                if (Functions.Abs(sin) >= Epsilon)
                {
                    double coeff = angle / sin;
                    result.X = coeff * X;
                    result.Y = coeff * Y;
                    result.Z = coeff * Z;
                }
                else
                {
                    result.X = X;
                    result.Y = Y;
                    result.Z = Z;
                }
            }

            return result;
        }

        /// <summary>
        ///		Calculates the Exponent of a Quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion_big Exp()
        {
            // If q = A*(x*i+y*j+z*k) where (x,y,z) is unit length, then
            // exp(q) = cos(A)+sin(A)*(x*i+y*j+z*k).  If sin(A) is near zero,
            // use exp(q) = cos(A)+A*(x*i+y*j+z*k) since A/sin(A) has limit 1.

            double angle = Functions.Sqrt(X * X + Y * Y + Z * Z);
            double sin = Functions.Sin(angle);

            // start off with a zero quat
            Quaternion_big result = Zero;

            result.W = Functions.Cos(angle);

            if (Functions.Abs(sin) >= Epsilon)
            {
                double coeff = sin / angle;

                result.X = coeff * X;
                result.Y = coeff * Y;
                result.Z = coeff * Z;
            }
            else
            {
                result.X = X;
                result.Y = Y;
                result.Z = Z;
            }

            return result;
        }

        #endregion

        #region Object overloads


        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Vector3.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})", X, Y, Z, W);
        }

        public string ToParsableText()
        {
            return ToString();
        }


        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Vector3.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public string ToIntegerString()
        {
            return string.Format("({0}, {1}, {2}, {3})", (int)X, (int)Y, (int)Z, (int)W);
        }
        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Vector3.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public string ToString(bool shortenDecmialPlaces)
        {
            if (shortenDecmialPlaces)
                return string.Format("({0:0.##}, {1:0.##} ,{2:0.##}, {3:0.##})", X, Y, Z, W);
            return ToString();
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is Quaternion_big && this == (Quaternion_big)obj;
        }


        #endregion
    }
}
