namespace Equations.Expressions
{
    public class ResonanceCalculator
    {
        readonly Random _randomNumber = new Random(1);
        public static string CapacitanceFactor = @"nF";
        public static string InductanceFactor = @"uH";
        private const double SpeedOfLight = 2.99792458E8;
        private const double PlancksConstant = 6.62607015E-34;
        private const double Pi = 3.14159265359;
        private const double BoltzmannConstant = 1.380649E-23;
        static double CapacitanceValue { get; set; }
        static double InductanceValue { get; set; }
        public static double ResonanceFrequency { get; set; }
        public static double Wavelength { get; set; }
        public static string WavelengthFactor { get; set; }
        /// <summary>
        /// Calculates the resonance frequency of a circuit.
        /// </summary>
        /// <param name="capacitance">The value in nF</param>
        /// <param name="inductance">The value in uH</param>
        /// <returns>The value plus computes the wavelength, filling this property.</returns>
        public static double CalculateResonance(double capacitance, double inductance)
        {
            CapacitanceValue = capacitance;
            InductanceValue = inductance;
            double tempCapacitance = CapacitanceValue;
            double tempInductance = InductanceValue;

            if (CapacitanceFactor == @"uF")
            {
                capacitance = tempCapacitance * 1E-6;
            }
            if (CapacitanceFactor == @"nF")
            {
                capacitance = tempCapacitance * 1E-9;
            }
            if (InductanceFactor == @"uH")
            {
                inductance = tempInductance * 1E-6;
            }

            // Perform the calculation.
            double sqrt = Math.Sqrt(inductance * capacitance);
            double result = 1 / (2 * Math.PI * sqrt);

            //decimal result = Decimal.Parse(result, NumberStyles.AllowExponent); // Returns the resonance frequency in exponent (Hertz).
            ResonanceFrequency = result;
            CalculateWavelength();
            return result;
        }
        /// <summary>
        /// Calculates the wavelength.
        /// </summary>
        public static double CalculateWavelength()
        {
            double frequency = Convert.ToDouble(ResonanceFrequency);
            double wavelength = SpeedOfLight / frequency;
            Wavelength = wavelength;
            WavelengthFactor = @"m";
            return wavelength;
        }

        public static double Calculate(double L, double C)
        {
            return 1 / Math.Sqrt(L * C);
        }
    }
}