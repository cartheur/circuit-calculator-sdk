namespace Circuit.Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this is the Circuit Calculator!");
            Console.WriteLine("Please enter the circuit you want to calculate:");
            string circuit = Console.ReadLine();
            Console.WriteLine("The result is: " + Calculate(circuit));
        }
    }
}
