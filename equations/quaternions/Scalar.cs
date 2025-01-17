using System.Drawing;

namespace QuaternionObjects
{
    public struct Scalar
    {
        public double X, Y, Z; // coordinate system follows right-hand rule

        public Scalar(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public Scalar(Vector v)
        {
            X = v.X; Y = v.Y; Z = v.Z;
        }

        public Scalar Copy()
        {
            return new Scalar(this.X, this.Y, this.Z);
        }

        public Vector ToVector()
        {
            return new Vector(X, Y, Z);
        }

        public void Offset(double x, double y, double z)
        {
            this.X += x;
            this.Y += y;
            this.Z += z;
        }

        public static Scalar[] Copy(Scalar[] pts)
        {
            Scalar[] copy = new Scalar[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                copy[i] = pts[i].Copy();
            }
            return copy;
        }

        public static void Offset(Scalar[] pts, double offsetX, double offsetY, double offsetZ)
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

        public static PointF[] Project(Scalar[] pts, double d /* project distance: from eye to screen*/)
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

