using System.Drawing;

namespace QuaternionObjects
{
    /// <summary>
    ///  A versor is a quaternion of norm one (a unit quaternion).
    ///  <remarks>https://en.wikipedia.org/wiki/Versor</remarks>
    /// </summary>
    public struct Versor
    {
        public double X, Y, Z; // coordinate system follows right-hand rule
        /// <summary>
        /// True if the verser is right-handed, left otherwise.
        /// </summary>
        public bool Right;

        public Versor(bool right,double x, double y, double z)
        {
            Right = right; X = x; Y = y; Z = z;
        }

        public Versor(Vector v)
        {
            X = v.X; Y = v.Y; Z = v.Z;
        }
        public Versor(Quaternion q)
        {
            X = q.X; Y = q.Y; Z = q.Z;
        }

        public Versor Copy()
        {
            return new Versor(Right, this.X, this.Y, this.Z);
        }

        public Versor ToVector()
        {
            return new Versor(false, X, Y, Z);//  new Scalar(W);
        }

        public void Offset(double x, double y, double z)
        {
            this.X += x;
            this.Y += y;
            this.Z += z;
        }

        public static Versor[] Copy(Versor[] pts)
        {
            Versor[] copy = new Versor[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                copy[i] = pts[i].Copy();
            }
            return copy;
        }

        public static void Offset(Versor[] pts, double offsetX, double offsetY, double offsetZ)
        {
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].Offset(offsetX, offsetY, offsetZ);
            }
        }

        public PointF GetProjectedPoint(double d /* project distance: from eye to screen*/)
        {
            return new PointF((float)(this.X * d / (d + this.Z)), (float)(this.Y * d / (d + this.Z)));
        }

        public static PointF[] Project(Tensor[] pts, double d /* project distance: from eye to screen*/)
        {
            PointF[] pt2ds = new PointF[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pt2ds[i] = pts[i].GetProjectedPoint(d);
            }
            return pt2ds;
        }
    }
}
