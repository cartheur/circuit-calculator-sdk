using System;
using System.Numerics;

namespace Equations.Expressions
{
    public class ImaginaryQuaternion
    {
        public Complex I { get; set; }
        public Complex J { get; set; }
        public Complex K { get; set; }

        public ImaginaryQuaternion(Complex i, Complex j, Complex k)
        {
            I = i;
            J = j;
            K = k;
        }

        public override string ToString()
        {
            return $"({I}, {J}, {K})";
        }

        public static ImaginaryQuaternion operator +(ImaginaryQuaternion a, ImaginaryQuaternion b)
        {
            return new ImaginaryQuaternion(a.I + b.I, a.J + b.J, a.K + b.K);
        }

        public static ImaginaryQuaternion operator -(ImaginaryQuaternion a, ImaginaryQuaternion b)
        {
            return new ImaginaryQuaternion(a.I - b.I, a.J - b.J, a.K - b.K);
        }

        public static ImaginaryQuaternion operator *(ImaginaryQuaternion a, ImaginaryQuaternion b)
        {
            return new ImaginaryQuaternion(a.I * b.I, a.J * b.J, a.K * b.K);
        }

        public static ImaginaryQuaternion operator /(ImaginaryQuaternion a, ImaginaryQuaternion b)
        {
            return new ImaginaryQuaternion(a.I / b.I, a.J / b.J, a.K / b.K);
        }
    }
}