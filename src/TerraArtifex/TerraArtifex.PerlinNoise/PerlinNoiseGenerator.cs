using TerraArtifex.MatrixExtensions;
using TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes;
using TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes.Factories;

namespace TerraArtifex.PerlinNoise
{
    public class PerlinNoiseGenerator
    {
        private readonly Random _random;

        private readonly int _maxValue;

        public PerlinNoiseGenerator(int seed = -1, int maxValue = 255)
        {
            _random = new Random(seed);

            _maxValue = maxValue;
        }

        /// <summary>
        /// Return a perlin noise int matrix with setted dimension (dimension must be a multiple of two)
        /// </summary>
        /// <param name="dimension">Dimension, must be a multiple of two</param>
        /// <returns></returns>
        public Matrix<int> GetPerlinNoiseMatrix(int dimension, int smoothingSize)
        {
            if (MathHelper.IsMultipleOfTwo(dimension) == false)
            {
                throw new NotSupportedException("Dimension must be a multiple of two [2, 4, 8, 16, 32, 64 ...].");
            }

            var matrixes = new List<Matrix<int>>();

            for (int i = 2; i < dimension; i *= 2)
            {
                var matrix = MatrixFactory.GetRandomMatrix(_random, i, i, _maxValue);

                matrix = matrix.IncreaseOctave(dimension / i);

                matrixes.Add(matrix);
            }

            var sum = matrixes.Average();

            sum.StretchOnMaximumAndMinimumValue(0, _maxValue);
            sum.Smoothing(smoothingSize);

            return sum;
        }
    }
}
