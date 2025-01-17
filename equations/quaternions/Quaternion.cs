using System;

namespace QuaternionObjects
{
    public struct Quaternion
    {
        public double W, X, Y, Z;

        public Quaternion(double w, double x, double y, double z)
        {
            W = w; X = x; Y = y; Z = z;
        }

        public Quaternion(double w, Vector v)
        {
            W = w; X = v.X; Y = v.Y; Z = v.Z;
        }

        public Vector V
        {
            set { X = value.X; Y = value.Y; Z = value.Z; }
            get { return new Vector(X, Y, Z); }
        }
        public void FromAxisAngle(Vector axis, double angleRadian)
        {
            double m = axis.Magnitude;
            if (m > 0.0001)
            {
                double ca = Math.Cos(angleRadian / 2);
                double sa = Math.Sin(angleRadian / 2);
                X = axis.X / m * sa;
                Y = axis.Y / m * sa;
                Z = axis.Z / m * sa;
                W = ca;
            }
            else
            {
                W = 1; X = 0; Y = 0; Z = 0;
            }
        }

        public Quaternion Copy()
        {
            return new Quaternion(W, X, Y, Z);
        }

        public void Multiplier(Quaternion q)
        {
            this *= q;
        }

        // Multiplying q1 with q2 is meaning of doing q2 firstly then q1
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            double nw = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
            double nx = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
            double ny = q1.W * q2.Y + q1.Y * q2.W + q1.Z * q2.X - q1.X * q2.Z;
            double nz = q1.W * q2.Z + q1.Z * q2.W + q1.X * q2.Y - q1.Y * q2.X;
            return new Quaternion(nw, nx, ny, nz);
        }
    }
}
