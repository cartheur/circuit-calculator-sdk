using System;

namespace QuaternionObjects
{
    public struct Vector
    {
        public double X, Y, Z;

        public Vector(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector(Scalar pt)
        {
            X = pt.X; Y = pt.Y; Z = pt.Z;
        }

        public Vector(Scalar startScalar, Scalar endScalar)
        {
            X = endScalar.X - startScalar.X;
            Y = endScalar.Y - startScalar.Y;
            Z = endScalar.Z - startScalar.Z;
        }

        public double Magnitude
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        public void Normalize()
        {
            double m = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (m > 0.001)
            {
                X /= m; Y /= m; Z /= m;
            }
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        // A x B = |A|*|B|*sin(angle), direction follow right-hand rule
        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            return new Vector(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        public static double DotProduct(Vector v1, Vector v2) // A . B = |A|*|B|*cos(angle)
        {
            return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }

        public Vector CrossProduct(Vector v)
        {
            return CrossProduct(this, v);
        }

        public double DotProduct(Vector v)
        {
            return DotProduct(this, v);
        }

        public static bool IsForeFace(Scalar pt1, Scalar pt2, Scalar pt3) // pts on a plane
        {
            Vector v1 = new Vector(pt2, pt1);
            Vector v2 = new Vector(pt2, pt3);
            Vector v = v1.CrossProduct(v2);
            return v.DotProduct(new Vector(0, 0, 1)) < 0;
        }

        public static bool IsBackFace(Scalar pt1, Scalar pt2, Scalar pt3)
        {
            Vector v1 = new Vector(pt2, pt1);
            Vector v2 = new Vector(pt2, pt3);
            Vector v = v1.CrossProduct(v2);
            return v.DotProduct(new Vector(0, 0, 1)) > 0;
        }
    }
}