using Equations.Numbers;

namespace Circuit.Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this is the Circuit Calculator!");

            Console.WriteLine("An example of how to use the calculator to compute complex numbers.");
            Complex c1 = new Complex( 3, 9 );
            Complex c2 = new Complex( 8, 3 );
            // sum
            Complex s1 = Complex.Add( c1, c2 );
            Complex s2 = c1 + c2;
            Console.WriteLine( "Sum: " + s2 );
            Complex s3 = c1 + 5;
            Console.WriteLine( "Sum: " + s3 );
            // difference
            Complex d1 = Complex.Subtract( c1, c2 );
            Complex d2 = c1 - c2;
            Console.WriteLine( "Difference: " + d2 );
            Complex d3 = c1 - 2;
            Console.WriteLine( "Difference: " + d3 );

            Console.WriteLine("An example of how to use the calculator to compute the resonance frequency of a circuit.");
            Console.WriteLine("The result is: " + Equations.Expressions.ResonanceCalculator.CalculateResonance( 100, 68 ));
            

            //Console.WriteLine("Please enter the circuit you want to calculate:");
            //string circuit = Console.ReadLine();
            //Console.WriteLine("The result is: " + Calculate(circuit));
        }
    }
}
