using System.Drawing;
using System.Text;
using TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes;

namespace TerraArtifex.Exporting
{
    public static class MatrixExportExtensions
    {
        public static Bitmap ToBitmap(this Matrix<int> matrix, int multiplier = 1)
        {
            var colorMatrix = matrix.Convert(a => Color.FromArgb(a, a, a));

            return colorMatrix.ToBitmap(multiplier);
        }

        public static Bitmap ToBitmap(this Matrix<double> matrix, int multiplier = 1)
        {
            var colorMatrix = matrix.Convert(a => Color.FromArgb((int)a, (int)a, (int)a));

            return colorMatrix.ToBitmap(multiplier);
        }

        public static Bitmap ToBitmap(this Matrix<Color> matrix, int multiplier = 1)
        {
            var image = new Bitmap(matrix.Width * multiplier, matrix.Height * multiplier);

            for (int x = 0; x < matrix.Width; x++)
            {
                for (int y = 0; y < matrix.Height; y++)
                {
                    var color = matrix[x, y];

                    for (int i = 0; i < multiplier; i++)
                    {
                        for (int j = 0; j < multiplier; j++)
                        {
                            image.SetPixel(x * multiplier + i, y * multiplier + j, color);
                        }
                    }
                }
            }

            return image;
        }
    }
}
