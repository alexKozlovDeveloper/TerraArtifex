using TerraArtifex.MatrixExtensions.MultidimensionalArrays.Factories;

namespace TerraArtifex.MatrixExtensions.MultidimensionalArrays
{
    public static class D2ArrayExtensions
    {
        public static int GetWidth<T>(this T[][] src)
        {
            var width = src.Length;

            return width;
        }

        public static int GetHeight<T>(this T[][] src)
        {
            var height = src.Select(a => a.Length).Max();

            return height;
        }

        public static T GetMax<T>(this T[][] src) 
        {
            return src.Max(a => a.Max());
        }

        public static T GetMin<T>(this T[][] src)
        {
            return src.Min(a => a.Min());
        }

        public static T[][] NormalizeToD2Array<T>(this T[][] src)
        {
            var width = src.GetWidth();
            var height = src.GetHeight();

            var result = D2ArrayFactory.CreateEmptyD2Array<T>(width, height);

            for (int x = 0; x < src.Length; x++)
            {
                for (int y = 0; y < src[x].Length; y++)
                {
                    result[x][y] = src[x][y];
                }
            }

            return result;
        }

        public static T[][] Copy<T>(this T[][] src)
        {
            var result = new T[src.Length][];

            for (int x = 0; x < src.Length; x++)
            {
                result[x] = new T[src[x].Length];

                for (int y = 0; y < src[x].Length; y++)
                {
                    result[x][y] = src[x][y];
                }
            }

            return result;
        }
    }
}
