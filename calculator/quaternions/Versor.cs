using System.Drawing;

namespace QuaternionObjects
{
    public class Versor
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public bool Right { get; private set; } // True if the versor is right-handed, false otherwise

        public Versor(bool right, double x, double y, double z)
        {
            Right = right;
            X = x;
            Y = y;
            Z = z;
        }

        public Versor(Vector v, bool right = true)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            Right = right;
        }

        public Versor(Quaternion q, bool right = true)
        {
            X = q.X;
            Y = q.Y;
            Z = q.Z;
            Right = right;
        }

        public Versor Copy()
        {
            return new Versor(Right, X, Y, Z);
        }

        public Vector ToVector()
        {
            return new Vector(X, Y, Z);
        }

        public void Offset(double offsetX, double offsetY, double offsetZ)
        {
            X += offsetX;
            Y += offsetY;
            Z += offsetZ;
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
            return new PointF((float)(X * d / (d + Z)), (float)(Y * d / (d + Z)));
        }

        public static PointF[] Project(Versor[] pts, double d /* project distance: from eye to screen*/)
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
