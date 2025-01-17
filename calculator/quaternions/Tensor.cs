using System.Drawing;

namespace QuaternionObjects
{
    public struct Tensor
    {
        public double W, X, Y, Z; // coordinate system follows right-hand rule

        public Tensor(double w,double x, double y, double z)
        {
            W = w; X = x; Y = y; Z = z;
        }

        public Tensor(Vector v)
        {
            X = v.X; Y = v.Y; Z = v.Z;
        }
        public Tensor(Quaternion q)
        {
            W = q.W; X = q.X; Y = q.Y; Z = q.Z;
        }

        public Tensor Copy()
        {
            return new Tensor(this. W, this.X, this.Y, this.Z);
        }

        public Vector ToVector()
        {
            return new Vector(X, Y, Z);//  new Scalar(W);
        }

        public void Offset(double x, double y, double z)
        {
            this.X += x;
            this.Y += y;
            this.Z += z;
        }

        public static Tensor[] Copy(Tensor[] pts)
        {
            Tensor[] copy = new Tensor[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                copy[i] = pts[i].Copy();
            }
            return copy;
        }

        public static void Offset(Tensor[] pts, double offsetX, double offsetY, double offsetZ)
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

