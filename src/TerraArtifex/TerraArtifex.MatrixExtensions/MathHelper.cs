namespace TerraArtifex.MatrixExtensions
{
    public static class MathHelper
    {
        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }

        public static bool IsMultipleOfTwo(int val)
        {
            return val != 0 && (val & (val - 1)) == 0;
        }

        public static void GetIntegerAndFractionalPart(double num, out int integer, out double fractional)
        {
            integer = (int)num;
            fractional = num - (double)integer;
        }

        public static double GetWeightedArithmeticMean(IEnumerable<(double value, double weight)> items)
        {
            var weightSum = items.Select(a => a.weight).Sum();

            var multiplyingValuesAndWeights = items.Select(a => a.value * a.weight).Sum();

            var result = multiplyingValuesAndWeights / weightSum;

            return result;
        }

        public static double GetSquareTransitionValue(double a1, double a2, double b1, double b2, double xPercent, double yPercent)
        {
            var r = Math.Sqrt(2);

            var a1Impact = r - Math.Sqrt(Math.Pow(xPercent, 2) + Math.Pow(yPercent, 2));
            var a2Impact = r - Math.Sqrt(Math.Pow(1 - xPercent, 2) + Math.Pow(yPercent, 2));
            var b1Impact = r - Math.Sqrt(Math.Pow(xPercent, 2) + Math.Pow(1 - yPercent, 2));
            var b2Impact = r - Math.Sqrt(Math.Pow(1 - xPercent, 2) + Math.Pow(1 - yPercent, 2));

            var pairs = new[] 
            { 
                (a1, a1Impact), 
                (a2, a2Impact), 
                (b1, b1Impact), 
                (b2, b2Impact) 
            };

            return GetWeightedArithmeticMean(pairs);
        }

        public static double GetSquareLerp(double a1, double a2, double b1, double b2, double xt, double yt)
        {
            var topLerp = Lerp(a1, a2, xt);
            var bottomLerp = Lerp(b1, b2, xt);

            var resultLerp = Lerp(topLerp, bottomLerp, yt);

            return resultLerp;
        }
    }
}
