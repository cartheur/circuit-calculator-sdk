using System;

namespace QuaternionObjects
{
    public static class Operations
    {
        /// <summary>
        /// Computes the tensor of the quaternion.
        /// </summary>
        /// <param name="input">The input quaternion.</param>
        /// <returns></returns>
        public static Quaternion Tensor(this Quaternion input)
        {
            double w = Math.Pow(input.W, 2);
            double x = Math.Pow(input.X, 2);
            double y = Math.Pow(input.Y, 2);
            double z = Math.Pow(input.Z, 2);

            return new Quaternion(w, x, y, z);
        }
        /// <summary>
        /// IS IT REALLY NOW?
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUnitVector(this Quaternion input)
        {
            Vector vec = new Vector(input.X, input.Y, input.Z);
            return vec.Magnitude == 1;
        }
        /// <summary>
        /// NEEDS TO BE VALIDATED.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Quaternion UnitVector(this Quaternion input)
        {
            return new Quaternion(0, 1, 0, 0);
        }
        /// <summary>
        /// NEEDS TO BE WRITTEN.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Quaternion Differential(this Quaternion input)
        {
            return new Quaternion();
        }
        /// <summary>
        /// Computes the norm of the quaternion.
        /// </summary>
        /// <param name="input">The input quaternion.</param>
        /// <returns></returns>
        public static Quaternion Norm(this Quaternion input)
        {
            var output = input.Copy();
            double m = output.W * output.W + output.X * output.X + output.Y * output.Y + output.Z * output.Z;
            if (m > 0.001)
            {
                m = Math.Sqrt(m);
                output.W /= m;
                output.X /= m;
                output.Y /= m;
                output.Z /= m;
            }
            else
            {
                output.W = 1; output.X = 0; output.Y = 0; output.Z = 0;
            }
            return output;
        }
        /// <summary>
        /// Returns the conjugate of the quaterion.
        /// </summary>
        /// <param name="input">The input quaternion.</param>
        /// <returns></returns>
        public static Quaternion Conjugate(this Quaternion input)
        {
            var output = input.Copy();
            output.X = -(input.X);
            output.Y = -(input.Y);
            output.Z = -(input.Z);

            return new Quaternion(input.W, output.X, output.Y, output.Z);
        }
        public static Quaternion Multiply(this Quaternion first, Quaternion second)
        {
            var q = first *= second;
            return q;
        }

        //                  -1
        // V'=q*V*q     ,
        public static Quaternion Rotate(this Quaternion input, Scalar pt)
        {
            input.Norm();
            Quaternion q1 = input.Copy();
            q1.Conjugate();

            Quaternion qNode = new Quaternion(0, pt.X, pt.Y, pt.Z);
            qNode = input * qNode * q1;
            pt.X = qNode.X;
            pt.Y = qNode.Y;
            pt.Z = qNode.Z;

            return qNode;
        }
        public static Quaternion Rotate(this Quaternion input, Scalar[] nodes)
        {
            input.Norm();
            Quaternion q1 = input.Copy();
            q1.Conjugate();
            Quaternion qNode = new Quaternion();

            for (int i = 0; i < nodes.Length; i++)
            {
                qNode = new Quaternion(0, nodes[i].X, nodes[i].Y, nodes[i].Z);
                qNode = input * qNode * q1;
                nodes[i].X = qNode.X;
                nodes[i].Y = qNode.Y;
                nodes[i].Z = qNode.Z;
            }

            return qNode;
        }
    }
}
