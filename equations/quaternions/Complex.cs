using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace QuaternionObjects
{
    /// <summary>
    /// Complex number wrapper class.
    /// </summary>
    /// <remarks>
    /// <para>The class encapsulates complex number and provides set of different operators to manipulate it, like adding, subtraction, multiplication, and divisiion.</para>
    /// <para>Sample usage:</para>
    /// <code>
    /// // define two complex numbers
    /// Complex c1 = new Complex( 3, 9 );
    /// Complex c2 = new Complex( 8, 3 );
    /// // sum
    /// Complex s1 = Complex.Add( c1, c2 );
    /// Complex s2 = c1 + c2;
    /// Complex s3 = c1 + 5;
    /// // difference
    /// Complex d1 = Complex.Subtract( c1, c2 );
    /// Complex d2 = c1 - c2;
    /// Complex d3 = c1 - 2;
    /// </code>
    /// </remarks>
    public struct Complex : ICloneable, ISerializable
    {
        /// <summary>
        /// Real part of the complex number.
        /// </summary>
        public double Real;
        /// <summary>
        /// Imaginary part of the complex number.
        /// </summary>
        public double Imaginary;
        /// <summary>
        ///  A double-precision complex number that represents zero.
        /// </summary>
        public static readonly Complex Zero = new Complex(0, 0);
        /// <summary>
        ///  A double-precision complex number that represents one.
        /// </summary>
        public static readonly Complex One = new Complex(1, 0);
        /// <summary>
        ///  A double-precision complex number that represents the squere root of (-1).
        /// </summary>
        public static readonly Complex I = new Complex(0, 1);
        /// <summary>
        /// The tolerance for numeric comparisons.
        /// </summary>
        const double Tolerance = 1E-7;
        /// <summary>
        /// Magnitude value of the complex number.
        /// </summary>
        /// <remarks><para>Magnitude of the complex number, which equals to <b>Sqrt( Real * Real + Imaginary * Imaginary )</b>.</para></remarks>
        public double Magnitude
        {
            get { return Math.Sqrt(Real * Real + Imaginary * Imaginary); }
        }
        /// <summary>
        /// Phase value of the complex number.
        /// </summary>
        /// <remarks><para>Phase of the complex number, which equals to <b>Atan( Imaginary / Real )</b>.</para></remarks>
        public double Phase
        {
            get { return Math.Atan2(Imaginary, Real); }
        }
        /// <summary>
        /// Squared magnitude value of the complex number.
        /// </summary>
        public double SquaredMagnitude
        {
            get { return (Real * Real + Imaginary * Imaginary); }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Complex" /> class.
        /// </summary>
        /// <param name="real">Real part.</param>
        /// <param name="imaginary">Imaginary part.</param>
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="c">Source complex number.</param>
        public Complex(Complex c)
        {
            Real = c.Real;
            Imaginary = c.Imaginary;
        }
        /// <summary>
        /// Returns the absolute value of a complex number.
        /// </summary>
        /// <param name="c">The complex number</param>
        /// <returns></returns>
        public static double AbsComplex(Complex c)
        {
            var xabs = Math.Abs(c.Real);
            var yabs = Math.Abs(c.Imaginary);
            var w = xabs > yabs ? xabs : yabs;
            var v = xabs < yabs ? xabs : yabs;
            if (Math.Abs(v) < Tolerance)
                return w;
            var t = v / w;
            return w * Math.Sqrt(1 + t * t);
        }
        /// <summary>
        /// Adds two complex numbers.
        /// </summary>
        /// <param name="a">A <see cref="Complex"/> instance.</param>
        /// <param name="b">A <see cref="Complex"/> instance.</param>
        /// <returns>Returns new <see cref="Complex"/> instance containing the sum of specified complex numbers.</returns>
        public static Complex Add(Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }
        /// <summary>
        /// Adds scalar value to a complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex"/> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <returns>Returns new <see cref="Complex"/> instance containing the sum of specified complex number and scalar value.</returns>
        public static Complex Add(Complex a, double s)
        {
            return new Complex(a.Real + s, a.Imaginary);
        }
        /// <summary>
        /// Adds two complex numbers and puts the result into the third complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        public static void Add(Complex a, Complex b, ref Complex result)
        {
            result.Real = a.Real + b.Real;
            result.Imaginary = a.Imaginary + b.Imaginary;
        }
        /// <summary>
        /// Adds scalar value to a complex number and puts the result into another complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        public static void Add(Complex a, double s, ref Complex result)
        {
            result.Real = a.Real + s;
            result.Imaginary = a.Imaginary;
        }
        /// <summary>
        /// Subtracts one complex number from another.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance to subtract from.</param>
        /// <param name="b">A <see cref="Complex" /> instance to be subtracted.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the subtraction result (<b>a - b</b>).
        /// </returns>
        public static Complex Subtract(Complex a, Complex b)
        {
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }
        /// <summary>
        /// Subtracts a scalar from a complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance to subtract from.</param>
        /// <param name="s">A scalar value to be subtracted.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the subtraction result (<b>a - s</b>).
        /// </returns>
        public static Complex Subtract(Complex a, double s)
        {
            return new Complex(a.Real - s, a.Imaginary);
        }
        /// <summary>
        /// Subtracts a complex number from a scalar value.
        /// </summary>
        /// <param name="s">A scalar value to subtract from.</param>
        /// <param name="a">A <see cref="Complex" /> instance to be subtracted.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the subtraction result (<b>s - a</b>).
        /// </returns>
        public static Complex Subtract(double s, Complex a)
        {
            return new Complex(s - a.Real, a.Imaginary);
        }
        /// <summary>
        /// Subtracts one complex number from another and puts the result in the third complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance to subtract from.</param>
        /// <param name="b">A <see cref="Complex" /> instance to be subtracted.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        public static void Subtract(Complex a, Complex b, ref Complex result)
        {
            result.Real = a.Real - b.Real;
            result.Imaginary = a.Imaginary - b.Imaginary;
        }
        /// <summary>
        /// Subtracts a scalar value from a complex number and puts the result into another complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance to subtract from.</param>
        /// <param name="s">A scalar value to be subtracted.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        public static void Subtract(Complex a, double s, ref Complex result)
        {
            result.Real = a.Real - s;
            result.Imaginary = a.Imaginary;
        }
        /// <summary>
        /// Subtracts a complex number from a scalar value and puts the result into another complex number.
        /// </summary>
        /// <param name="s">A scalar value to subtract from.</param>
        /// <param name="a">A <see cref="Complex" /> instance to be subtracted.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        public static void Subtract(double s, Complex a, ref Complex result)
        {
            result.Real = s - a.Real;
            result.Imaginary = a.Imaginary;
        }
        /// <summary>
        /// Multiplies two complex numbers.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result of multiplication.
        /// </returns>
        public static Complex Multiply(Complex a, Complex b)
        {
            // (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
            double aRe = a.Real, aIm = a.Imaginary;
            double bRe = b.Real, bIm = b.Imaginary;

            return new Complex(aRe * bRe - aIm * bIm, aRe * bIm + aIm * bRe);
        }
        /// <summary>
        /// Multiplies a complex number by a scalar value.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result of multiplication.
        /// </returns>
        public static Complex Multiply(Complex a, double s)
        {
            return new Complex(a.Real * s, a.Imaginary * s);
        }
        /// <summary>
        /// Multiplies two complex numbers and puts the result in a third complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        public static void Multiply(Complex a, Complex b, ref Complex result)
        {
            // (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
            double aRe = a.Real, aIm = a.Imaginary;
            double bRe = b.Real, bIm = b.Imaginary;

            result.Real = aRe * bRe - aIm * bIm;
            result.Imaginary = aRe * bIm + aIm * bRe;
        }
        /// <summary>
        /// Multiplies a complex number by a scalar value and puts the result into another complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        public static void Multiply(Complex a, double s, ref Complex result)
        {
            result.Real = a.Real * s;
            result.Imaginary = a.Imaginary * s;
        }
        /// <summary>
        /// Divides one complex number by another complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result.
        /// </returns>
        /// <exception cref="System.DivideByZeroException">Can not divide by zero.</exception>
        /// <exception cref="DivideByZeroException">Can not divide by zero.</exception>
        public static Complex Divide(Complex a, Complex b)
        {
            var aRe = a.Real;
            var aIm = a.Imaginary;
            var bRe = b.Real;
            var bIm = b.Imaginary;
            var modulusSquared = bRe * bRe + bIm * bIm;

            if (Math.Abs(modulusSquared) < Tolerance)
            {
                throw new DivideByZeroException("Can not divide by zero.");// Unless 'Jimmy' is using this code! ;-)
            }

            var invModulusSquared = 1 / modulusSquared;

            return new Complex(
                (aRe * bRe + aIm * bIm) * invModulusSquared,
                (aIm * bRe - aRe * bIm) * invModulusSquared);
        }
        /// <summary>
        /// Divides a complex number by a scalar value.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result.
        /// </returns>
        /// <exception cref="System.DivideByZeroException">Can not divide by zero.</exception>
        /// <exception cref="DivideByZeroException">Can not divide by zero.</exception>
        public static Complex Divide(Complex a, double s)
        {
            if (Math.Abs(s) < Tolerance)
            {
                throw new DivideByZeroException("Can not divide by zero.");
            }

            return new Complex(a.Real / s, a.Imaginary / s);
        }
        /// <summary>
        /// Divides a scalar value by a complex number.
        /// </summary>
        /// <param name="s">A scalar value.</param>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result.
        /// </returns>
        /// <exception cref="System.DivideByZeroException">Can not divide by zero.</exception>
        /// <exception cref="DivideByZeroException">Can not divide by zero.</exception>
        public static Complex Divide(double s, Complex a)
        {
            if ((Math.Abs(a.Real) < Tolerance) || (Math.Abs(a.Imaginary) < Tolerance))
            {
                throw new DivideByZeroException("Can not divide by zero.");
            }
            return new Complex(s / a.Real, s / a.Imaginary);
        }
        /// <summary>
        /// Divides one complex number by another complex number and puts the result in a third complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        /// <exception cref="System.DivideByZeroException">Can not divide by zero.</exception>
        /// <exception cref="DivideByZeroException">Can not divide by zero.</exception>
        public static void Divide(Complex a, Complex b, ref Complex result)
        {
            double aRe = a.Real, aIm = a.Imaginary;
            double bRe = b.Real, bIm = b.Imaginary;
            var modulusSquared = bRe * bRe + bIm * bIm;

            if (Math.Abs(modulusSquared) < Tolerance)
            {
                throw new DivideByZeroException("Can not divide by zero.");
            }

            var invModulusSquared = 1 / modulusSquared;

            result.Real = (aRe * bRe + aIm * bIm) * invModulusSquared;
            result.Imaginary = (aIm * bRe - aRe * bIm) * invModulusSquared;
        }
        /// <summary>
        /// Divides a complex number by a scalar value and puts the result into another complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        /// <exception cref="System.DivideByZeroException">Can not divide by zero.</exception>
        /// <exception cref="DivideByZeroException">Can not divide by zero.</exception>
        public static void Divide(Complex a, double s, ref Complex result)
        {
            if (Math.Abs(s) < Tolerance)
            {
                throw new DivideByZeroException("Can not divide by zero.");
            }

            result.Real = a.Real / s;
            result.Imaginary = a.Imaginary / s;
        }
        /// <summary>
        /// Divides a scalar value by a complex number and puts the result into another complex number.
        /// </summary>
        /// <param name="s">A scalar value.</param>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="result">A <see cref="Complex" /> instance to hold the result.</param>
        /// <exception cref="System.DivideByZeroException">Can not divide by zero.</exception>
        /// <exception cref="DivideByZeroException">Can not divide by zero.</exception>
        public static void Divide(double s, Complex a, ref Complex result)
        {
            if ((Math.Abs(a.Real) < Tolerance) || (Math.Abs(a.Imaginary) < Tolerance))
            {
                throw new DivideByZeroException("Can not divide by zero.");
            }

            result.Real = s / a.Real;
            result.Imaginary = s / a.Imaginary;
        }
        /// <summary>
        /// Negates a complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing the negated values.
        /// </returns>
        public static Complex Negate(Complex a)
        {
            return new Complex(-a.Real, -a.Imaginary);
        }
        /// <summary>
        /// Tests whether two complex numbers are approximately equal using default tolerance value.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Return <see langword="true" /> if the two vectors are approximately equal or <see langword="false" /> otherwise.
        /// </returns>
        /// <remarks>
        /// The default tolerance value, which is used for the test, equals to 8.8817841970012523233891E-16.
        /// </remarks>
        public static bool ApproxEqual(Complex a, Complex b)
        {
            return ApproxEqual(a, b, 8.8817841970012523233891E-16);
        }
        /// <summary>
        /// Tests whether two complex numbers are approximately equal given a tolerance value.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <param name="tolerance">The tolerance value used to test approximate equality.</param>
        /// <returns></returns>
        /// <remarks>
        /// The default tolerance value, which is used for the test, equals to 8.8817841970012523233891E-16.
        /// </remarks>
        public static bool ApproxEqual(Complex a, Complex b, double tolerance)
        {
            return
                ((Math.Abs(a.Real - b.Real) <= tolerance) && (Math.Abs(a.Imaginary - b.Imaginary) <= tolerance));
        }

        #region Public Static Parse Methods
        /// <summary>
        /// Converts the specified string to its <see cref="Complex" /> equivalent.
        /// </summary>
        /// <param name="s">A string representation of a complex number.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance that represents the complex number specified by the <paramref name="s" /> parameter.
        /// </returns>
        /// <exception cref="System.FormatException">String representation of the complex number is not correctly formatted.</exception>
        /// <exception cref="FormatException">String representation of the complex number is not correctly formatted.</exception>
        public static Complex Parse(string s)
        {
            var r = new Regex(@"\((?<real>.*),(?<imaginary>.*)\)", RegexOptions.None);
            var m = r.Match(s);

            if (m.Success)
            {
                return new Complex(
                    double.Parse(m.Result("${real}")),
                    double.Parse(m.Result("${imaginary}"))
                    );
            }
            throw new FormatException("String representation of the complex number is not correctly formatted.");
        }
        /// <summary>
        /// Try to convert the specified string to its <see cref="Complex" /> equivalent.
        /// </summary>
        /// <param name="s">A string representation of a complex number.</param>
        /// <param name="result"><see cref="Complex" /> instance to output the result to.</param>
        /// <returns>
        /// Returns boolean value that indicates if the parse was successful or not.
        /// </returns>
        public static bool TryParse(string s, out Complex result)
        {
            try
            {
                var newComplex = Parse(s);
                result = newComplex;
                return true;

            }
            catch (FormatException)
            {
                result = new Complex();
                return false;
            }
        }

        #endregion

        #region Public Static Complex Special Functions

        /// <summary>
        /// Calculates square root of a complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing the square root of the specified complex number.
        /// </returns>
        public static Complex Sqrt(Complex a)
        {
            Complex result = Zero;

            if ((Math.Abs(a.Real) < Tolerance) && (Math.Abs(a.Imaginary) < Tolerance))
            {
                return result;
            }
            if (Math.Abs(a.Imaginary) < Tolerance)
            {
                result.Real = (a.Real > 0) ? Math.Sqrt(a.Real) : Math.Sqrt(-a.Real);
                result.Imaginary = 0.0;
            }
            else
            {
                var modulus = a.Magnitude;

                result.Real = Math.Sqrt(0.5 * (modulus + a.Real));
                result.Imaginary = Math.Sqrt(0.5 * (modulus - a.Real));
                if (a.Imaginary < 0.0)
                    result.Imaginary = -result.Imaginary;
            }

            return result;
        }
        /// <summary>
        /// Calculates natural (base <b>e</b>) logarithm of a complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing the natural logarithm of the specified complex number.
        /// </returns>
        public static Complex Log(Complex a)
        {
            var result = Zero;

            if ((a.Real > 0.0) && (Math.Abs(a.Imaginary) < Tolerance))
            {
                result.Real = Math.Log(a.Real);
                result.Imaginary = 0.0;
            }
            else if (Math.Abs(a.Real) < Tolerance)
            {
                if (a.Imaginary > 0.0)
                {
                    result.Real = Math.Log(a.Imaginary);
                    result.Imaginary = Math.PI / 2.0;
                }
                else
                {
                    result.Real = Math.Log(-(a.Imaginary));
                    result.Imaginary = -Math.PI / 2.0;
                }
            }
            else
            {
                result.Real = Math.Log(a.Magnitude);
                result.Imaginary = Math.Atan2(a.Imaginary, a.Real);
            }

            return result;
        }
        /// <summary>
        /// Calculates exponent (<b>e</b> raised to the specified power) of a complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing the exponent of the specified complex number.
        /// </returns>
        public static Complex Exp(Complex a)
        {
            var result = Zero;
            var r = Math.Exp(a.Real);
            result.Real = r * Math.Cos(a.Imaginary);
            result.Imaginary = r * Math.Sin(a.Imaginary);

            return result;
        }
        #endregion

        #region Public Static Complex Trigonometry

        /// <summary>
        /// Calculates Sine value of the complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing the Sine value of the specified complex number.
        /// </returns>
        public static Complex Sin(Complex a)
        {
            var result = Zero;

            if (Math.Abs(a.Imaginary) < Tolerance)
            {
                result.Real = Math.Sin(a.Real);
                result.Imaginary = 0.0;
            }
            else
            {
                result.Real = Math.Sin(a.Real) * Math.Cosh(a.Imaginary);
                result.Imaginary = Math.Cos(a.Real) * Math.Sinh(a.Imaginary);
            }

            return result;
        }
        /// <summary>
        /// Calculates Cosine value of the complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing the Cosine value of the specified complex number.
        /// </returns>
        public static Complex Cos(Complex a)
        {
            var result = Zero;

            if (Math.Abs(a.Imaginary) < Tolerance)
            {
                result.Real = Math.Cos(a.Real);
                result.Imaginary = 0.0;
            }
            else
            {
                result.Real = Math.Cos(a.Real) * Math.Cosh(a.Imaginary);
                result.Imaginary = -Math.Sin(a.Real) * Math.Sinh(a.Imaginary);
            }

            return result;
        }
        /// <summary>
        /// Calculates Tangent value of the complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing the Tangent value of the specified complex number.
        /// </returns>
        public static Complex Tan(Complex a)
        {
            var result = Zero;

            if (Math.Abs(a.Imaginary) < Tolerance)
            {
                result.Real = Math.Tan(a.Real);
                result.Imaginary = 0.0;
            }
            else
            {
                var real2 = 2 * a.Real;
                var imag2 = 2 * a.Imaginary;
                var denom = Math.Cos(real2) + Math.Cosh(real2);

                result.Real = Math.Sin(real2) / denom;
                result.Imaginary = Math.Sinh(imag2) / denom;
            }

            return result;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns>
        /// Returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="Complex" /> and has the same values as this instance or <see langword="false" /> otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (obj is Complex) && (this == (Complex)obj);
            //return (obj is Complex) ? (this == (Complex)obj) : false; -- original representation
        }
        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", Real, Imaginary);
        }
        #endregion

        #region Comparison Operators
        /// <summary>
        /// Tests whether two specified complex numbers are equal.
        /// </summary>
        /// <param name="u">The left-hand complex number.</param>
        /// <param name="v">The right-hand complex number.</param>
        /// <returns>
        /// Returns <see langword="true" /> if the two complex numbers are equal or <see langword="false" /> otherwise.
        /// </returns>
        public static bool operator ==(Complex u, Complex v)
        {
            return ((Math.Abs(u.Real - v.Real) < Tolerance) && (Math.Abs(u.Imaginary - v.Imaginary) < Tolerance));
        }
        /// <summary>
        /// Tests whether two specified complex numbers are not equal.
        /// </summary>
        /// <param name="u">The left-hand complex number.</param>
        /// <param name="v">The right-hand complex number.</param>
        /// <returns>
        /// Returns <see langword="true" /> if the two complex numbers are not equal or <see langword="false" /> otherwise.
        /// </returns>
        public static bool operator !=(Complex u, Complex v)
        {
            return !(u == v);
        }
        #endregion

        #region Unary Operators
        /// <summary>
        /// Negates the complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" />  instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the negated values.
        /// </returns>
        public static Complex operator -(Complex a)
        {
            return Negate(a);
        }
        #endregion

        #region Binary Operators
        /// <summary>
        /// Adds two complex numbers.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the sum.
        /// </returns>
        public static Complex operator +(Complex a, Complex b)
        {
            return Add(a, b);
        }
        /// <summary>
        /// Adds a complex number and a scalar value.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the sum.
        /// </returns>
        public static Complex operator +(Complex a, double s)
        {
            return Add(a, s);
        }
        /// <summary>
        /// Adds a complex number and a scalar value.
        /// </summary>
        /// <param name="s">A scalar value.</param>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the sum.
        /// </returns>
        public static Complex operator +(double s, Complex a)
        {
            return Add(a, s);
        }
        /// <summary>
        /// Subtracts one complex number from another complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the difference.
        /// </returns>
        public static Complex operator -(Complex a, Complex b)
        {
            return Subtract(a, b);
        }
        /// <summary>
        /// Subtracts a scalar value from a complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the difference.
        /// </returns>
        public static Complex operator -(Complex a, double s)
        {
            return Subtract(a, s);
        }
        /// <summary>
        /// Subtracts a complex number from a scalar value.
        /// </summary>
        /// <param name="s">A scalar value.</param>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the difference.
        /// </returns>
        public static Complex operator -(double s, Complex a)
        {
            return Subtract(s, a);
        }
        /// <summary>
        /// Multiplies two complex numbers.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result of multiplication.
        /// </returns>
        public static Complex operator *(Complex a, Complex b)
        {
            return Multiply(a, b);
        }
        /// <summary>
        /// Multiplies a complex number by a scalar value.
        /// </summary>
        /// <param name="s">A scalar value.</param>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result of multiplication.
        /// </returns>
        public static Complex operator *(double s, Complex a)
        {
            return Multiply(a, s);
        }
        /// <summary>
        /// Multiplies a complex number by a scalar value.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result of multiplication.
        /// </returns>
        public static Complex operator *(Complex a, double s)
        {
            return Multiply(a, s);
        }
        /// <summary>
        /// Divides one complex number by another complex number.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="b">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// A new Complex instance containing the result.
        /// </returns>
        public static Complex operator /(Complex a, Complex b)
        {
            return Divide(a, b);
        }
        /// <summary>
        /// Divides a complex number by a scalar value.
        /// </summary>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <param name="s">A scalar value.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result of division.
        /// </returns>
        public static Complex operator /(Complex a, double s)
        {
            return Divide(a, s);
        }
        /// <summary>
        /// Divides a scalar value by a complex number.
        /// </summary>
        /// <param name="s">A scalar value.</param>
        /// <param name="a">A <see cref="Complex" /> instance.</param>
        /// <returns>
        /// Returns new <see cref="Complex" /> instance containing the result of division.
        /// </returns>
        public static Complex operator /(double s, Complex a)
        {
            return Divide(s, a);
        }
        #endregion

        #region Conversion Operators
        /// <summary>
        /// Converts from a single-precision real number to a complex number.
        /// </summary>
        /// <param name="value">Single-precision real number to convert to complex number.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing complex number with real part initialized to the specified value.
        /// </returns>
        public static explicit operator Complex(float value)
        {
            return new Complex(value, 0);
        }
        /// <summary>
        /// Converts from a double-precision real number to a complex number.
        /// </summary>
        /// <param name="value">Double-precision real number to convert to complex number.</param>
        /// <returns>
        /// Returns a new <see cref="Complex" /> instance containing complex number with real part initialized to the specified value.
        /// </returns>
        public static explicit operator Complex(double value)
        {
            return new Complex(value, 0);
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Complex" /> object through the IClonable interface.
        /// </summary>
        /// <returns>
        /// Returns clone of the complex number.
        /// </returns>
        object ICloneable.Clone()
        {
            return new Complex(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Complex" /> object through the Complex struct.
        /// </summary>
        /// <returns>
        /// Returns clone of the complex number.
        /// </returns>
        public Complex Clone()
        {
            return new Complex(this);
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a <see cref="SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Real", Real);
            info.AddValue("Imaginary", Imaginary);
        }
        #endregion
    }
}
