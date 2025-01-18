namespace QuaternionObjects
{
    public class MagneticFieldQuaternion
    {
        public double W { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public MagneticFieldQuaternion(double w, double x, double y, double z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public MagneticFieldQuaternion(Vector magneticField)
        {
            W = 0;
            X = magneticField.X;
            Y = magneticField.Y;
            Z = magneticField.Z;
        }

        public MagneticFieldQuaternion Normalize()
        {
            double norm = Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
            return new MagneticFieldQuaternion(W / norm, X / norm, Y / norm, Z / norm);
        }

        public static MagneticFieldQuaternion operator +(MagneticFieldQuaternion a, MagneticFieldQuaternion b)
        {
            return new MagneticFieldQuaternion(a.W + b.W, a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static MagneticFieldQuaternion operator -(MagneticFieldQuaternion a, MagneticFieldQuaternion b)
        {
            return new MagneticFieldQuaternion(a.W - b.W, a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static MagneticFieldQuaternion operator *(MagneticFieldQuaternion a, double scalar)
        {
            return new MagneticFieldQuaternion(a.W * scalar, a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        public static MagneticFieldQuaternion operator /(MagneticFieldQuaternion a, double scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException("Scalar must not be zero.");
            }
            return new MagneticFieldQuaternion(a.W / scalar, a.X / scalar, a.Y / scalar, a.Z / scalar);
        }

        public Vector ToVector()
        {
            return new Vector(X, Y, Z);
        }
    }
}