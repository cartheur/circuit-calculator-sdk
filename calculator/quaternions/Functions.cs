namespace QuaternionObjects
{

    /// <summary>
    /// This is a class which exposes static methods for various common math functions.  Currently,
    /// the methods simply wrap the methods of the System.Math class (with the exception of a few added extras).
    /// This is in case the implementation needs to be swapped out with a faster C++ implementation, if
    /// deemed that the System.Math methods are not up to far speed wise.
    /// </summary>
    /// TODO: Add overloads for all methods for all instrinsic data types (i.e. double, short, etc).
    public static class Functions
    {
        static Random random = new Random();

        #region Constant

        public const double PI = (double)Math.PI;
        public const double TWO_PI = (double)Math.PI * 2.0f;
        public const double RADIANS_PER_DEGREE = PI / 180.0f;
        public const double DEGREES_PER_RADIAN = 180.0f / PI;

        #endregion

        #region From MathUtilities class

        //public static System.Random RndObject = new System.Random(System.DateTime.Now.Millisecond);
        public static Random RndObject = new Random(DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60 * 1000 * DateTime.Now.Minute);

        public const double MachineEpsilon = 5E-16;
        public const double MaxRealNumber = 1E300;
        public const double MinRealNumber = 1E-300;

        public static bool IsFinite(double d)
        {
            return !Double.IsNaN(d) && !Double.IsInfinity(d);
        }

        public static double RandomReal()
        {
            double r = 0;
            lock (RndObject) { r = RndObject.NextDouble(); }
            return r;
        }
        public static int RandomInteger(int N)
        {
            int r = 0;
            lock (RndObject) { r = RndObject.Next(N); }
            return r;
        }
        public static double sqr(double X)
        {
            return X * X;
        }

        //public static alglib.complex conj(alglib.complex z)
        //{
        //    return new complex(z.X, -z.y);
        //}
        //public static complex csqr(complex z)
        //{
        //    return new complex(z.X * z.X - z.y * z.y, 2 * z.X * z.y);
        //}

        #endregion

        #region Static Methods

        /// <summary>
        ///		Converts degrees to radians.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * RADIANS_PER_DEGREE;
        }
        /// <summary>
        ///		Converts radians to degrees.
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static double RadiansToDegrees(double radians)
        {
            return radians * DEGREES_PER_RADIAN;
        }
        /// <summary>
        ///		Returns the sine of the angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double Sin(double angle)
        {
            return (double)System.Math.Sin(angle);
        }
        /// <summary>
        ///		Calculate a face normal, including the w component which is the offset from the origin.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector4 CalculateFaceNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 normal = CalculateBasicFaceNormal(v1, v2, v3);

            // Now set up the w (distance of tri from origin
            return new Vector4(normal.X, normal.Y, normal.Z, -(normal.Dot(v1)));
        }
        /// <summary>
        ///		Calculate a face normal, no w-information.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector3 CalculateBasicFaceNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 normal = (v2 - v1).Cross(v3 - v1);
            normal.Normalize();

            return normal;
        }
        /// <summary>
        ///    Calculates the tangent space vector for a given set of positions / texture coords.
        /// </summary>
        /// <remarks>
        ///    Adapted from bump mapping tutorials at:
        ///    http://www.paulsprojects.net/tutorials/simplebump/simplebump.html
        ///    author : paul.baker@univ.ox.ac.uk
        /// </remarks>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <param name="position3"></param>
        /// <param name="u1"></param>
        /// <param name="v1"></param>
        /// <param name="u2"></param>
        /// <param name="v2"></param>
        /// <param name="u3"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector3 CalculateTangentSpaceVector(
            Vector3 position1, Vector3 position2, Vector3 position3, double u1, double v1, double u2, double v2, double u3, double v3)
        {

            // side0 is the vector along one side of the triangle of vertices passed in, 
            // and side1 is the vector along another side. Taking the cross product of these returns the normal.
            Vector3 side0 = position1 - position2;
            Vector3 side1 = position3 - position1;
            // Calculate face normal
            Vector3 normal = side1.Cross(side0);
            normal.Normalize();

            // Now we use a formula to calculate the tangent. 
            double deltaV0 = v1 - v2;
            double deltaV1 = v3 - v1;
            Vector3 tangent = deltaV1 * side0 - deltaV0 * side1;
            tangent.Normalize();

            // Calculate binormal
            double deltaU0 = u1 - u2;
            double deltaU1 = u3 - u1;
            Vector3 binormal = deltaU1 * side0 - deltaU0 * side1;
            binormal.Normalize();

            // Now, we take the cross product of the tangents to get a vector which 
            // should point in the same direction as our normal calculated above. 
            // If it points in the opposite direction (the dot product between the normals is less than zero), 
            // then we need to reverse the s and t tangents. 
            // This is because the triangle has been mirrored when going from tangent space to object space.
            // reverse tangents if necessary.
            Vector3 tangentCross = tangent.Cross(binormal);
            if (tangentCross.Dot(normal) < 0.0f)
            {
                tangent = -tangent;
                binormal = -binormal;
            }

            return tangent;
        }
        /// <summary>
        ///		Returns the cosine of the angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double Cos(double angle)
        {
            return (double)System.Math.Cos(angle);
        }
        /// <summary>
        ///		Returns the arc cosine of the angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double ACos(double angle)
        {

            // HACK: Ok, this needs to be looked at.  The decimal precision of double values can sometimes be 
            // *slightly* off from what is loaded from .skeleton files.  In some scenarios when we end up having 
            // a cos value calculated above that is just over 1 (i.e. 1.000000012), which the ACos of is Nan, thus 
            // completly throwing off node transformations and rotations associated with an animation.
            if (angle > 1)
            {
                angle = 1.0f;
            }

            return (double)System.Math.Acos(angle);
        }
        /// <summary>
        ///		Returns the arc sine of the angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double ASin(double angle)
        {
            return (double)System.Math.Asin(angle);
        }
        /// <summary>
        ///    Inverse square root.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double InvSqrt(double number)
        {
            return 1 / Sqrt(number);
        }
        /// <summary>
        ///		Returns the square root of a number.
        /// </summary>
        /// <remarks>This is one of the more expensive math operations.  Avoid when possible.</remarks>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double Sqrt(double number)
        {
            return (double)System.Math.Sqrt(number);
        }
        /// <summary>
        ///		Returns the absolute value of the supplied number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double Abs(double number)
        {
            return System.Math.Abs(number);
        }

        public static bool FloatEqual(double a, double b)
        {
            return FloatEqual(a, b, .00001f);
        }

        public static bool FloatEqualTolerant(double a, double b)
        {
            return FloatEqual(a, b, .0001f);
        }
        /// <summary>
        ///     Compares double values for equality, taking into consideration
        ///     that floating point values should never be directly compared using
        ///     ==.  2 floats could be conceptually equal, but vary by a 
        ///     .000001 which would fail in a direct comparison.  To circumvent that,
        ///     a tolerance value is used to see if the difference between the 2 floats
        ///     is less than the desired amount of accuracy.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool FloatEqual(double a, double b, double tolerance)
        {
            return Math.Abs(b - a) <= tolerance;
        }

        /// <summary>
        ///		Returns the tangent of the angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double Tan(double angle)
        {
            return Math.Tan(angle);
        }

        /// <summary>
        ///		Used to quickly determine the greater value between two values.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static double Max(double value1, double value2)
        {
            if (double.IsNaN(value1) || double.IsNaN(value2))
                return double.NaN;
            return (value1 > value2) ? value1 : value2;
        }

        public static double Max(double value1, double value2, double value3)
        {
            if (double.IsNaN(value1) || double.IsNaN(value2) || double.IsNaN(value3))
                return double.NaN;
            double max12 = (value1 > value2) ? value1 : value2;
            return (max12 > value3) ? max12 : value3;
        }
        public static double Min(double value1, double value2, double value3)
        {
            if (double.IsNaN(value1) || double.IsNaN(value2) || double.IsNaN(value3))
                return double.NaN;
            double min12 = (value1 < value2) ? value1 : value2;
            return (min12 < value3) ? min12 : value3;
        }

        public static double Max(params double[] vals)
        {
            if (vals.Length == 0)
                throw new ArgumentException("There must be at least one value to compare");
            double max = vals[0];
            for (int i = 1; i < vals.Length; i++)
            {
                double val = vals[i];
                if (double.IsNaN(val))
                    return double.NaN;
                if (val > max)
                    max = val;
            }
            return max;
        }

        public static double Min(params double[] vals)
        {
            if (vals.Length == 0)
                throw new ArgumentException("There must be at least one value to compare");
            double min = vals[0];
            for (int i = 1; i < vals.Length; i++)
            {
                double val = vals[i];
                if (double.IsNaN(val))
                    return double.NaN;
                if (val < min)
                    min = val;
            }
            return min;
        }
        /// <summary>
        ///		Used to quickly determine the lesser value between two values.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static double Min(double value1, double value2)
        {
            return (value1 < value2 || double.IsNaN(value1)) ? value1 : value2;
        }
        /// <summary>
        ///    Returns a random value between the specified min and max values.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>A random value in the range [min,max].</returns>
        public static double RangeRandom(double min, double max)
        {
            return (max - min) * UnitRandom() + min;
        }
        /// <summary>
        ///    
        /// </summary>
        /// <returns></returns>
        public static double UnitRandom()
        {
            return (double)random.Next(Int32.MaxValue) / (double)Int32.MaxValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static double SymmetricRandom()
        {
            return 2.0f * UnitRandom() - 1.0f;
        }
        /// <summary>
        ///		Checks wether a given point is inside a triangle, in a
        ///		2-dimensional (Cartesian) space.
        /// </summary>
        /// <remarks>
        ///		The vertices of the triangle must be given in either
        ///		trigonometrical (anticlockwise) or inverse trigonometrical
        ///		(clockwise) order.
        /// </remarks>
        /// <param name="px">
        ///    The X-coordinate of the point.
        /// </param>
        /// <param name="py">
        ///    The Y-coordinate of the point.
        /// </param>
        /// <param name="ax">
        ///    The X-coordinate of the triangle's first vertex.
        /// </param>
        /// <param name="ay">
        ///    The Y-coordinate of the triangle's first vertex.
        /// </param>
        /// <param name="bx">
        ///    The X-coordinate of the triangle's second vertex.
        /// </param>
        /// <param name="by">
        ///    The Y-coordinate of the triangle's second vertex.
        /// </param>
        /// <param name="cx">
        ///    The X-coordinate of the triangle's third vertex.
        /// </param>
        /// <param name="cy">
        ///    The Y-coordinate of the triangle's third vertex.
        /// </param>
        /// <returns>
        ///    <list type="bullet">
        ///        <item>
        ///            <description><b>true</b> - the point resides in the triangle.</description>
        ///        </item>
        ///        <item>
        ///            <description><b>false</b> - the point is outside the triangle</description>
        ///         </item>
        ///     </list>
        /// </returns>
        public static bool PointInTri2D(double px, double py, double ax, double ay, double bx, double by, double cx, double cy)
        {
            double v1x, v2x, v1y, v2y;
            bool bClockwise;

            v1x = bx - ax;
            v1y = by - ay;

            v2x = px - bx;
            v2y = py - by;

            bClockwise = (v1x * v2y - v1y * v2x >= 0.0);

            v1x = cx - bx;
            v1y = cy - by;

            v2x = px - cx;
            v2y = py - cy;

            if ((v1x * v2y - v1y * v2x >= 0.0) != bClockwise)
                return false;

            v1x = ax - cx;
            v1y = ay - cy;

            v2x = px - ax;
            v2y = py - ay;

            if ((v1x * v2y - v1y * v2x >= 0.0) != bClockwise)
                return false;

            return true;
        }

        #endregion Static Methods

 }

}
