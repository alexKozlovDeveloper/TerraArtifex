namespace TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes.Factories
{
    public static class MatrixFactory
    {
        public static Matrix<int> GetRandomMatrix(Random random, int width, int height, int maxValue)
        {
            var result = new Matrix<int>(width, height);

            result.ForEachItem(() => random.Next(0, maxValue));

            return result;
        }
    }
}
