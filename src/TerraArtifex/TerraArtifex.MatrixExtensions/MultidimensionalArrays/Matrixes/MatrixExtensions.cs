using TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes;
using TerraArtifex.MatrixExtensions.Points;

namespace TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes
{
    public static class MatrixExtensions
    {
        public static Matrix<M> Convert<T, M>(this Matrix<T> src, Func<T, M> convertFunc)
        {
            var result = new Matrix<M>(src.Size);

            result.ForEachItem((x, y) => convertFunc(src[x, y]));

            return result;
        }

        public static void ForEachItemRadial<T>(this Matrix<T> src, Func<T, double, T> func)
        {
            var center = new Vector2(src.Width / 2, src.Height / 2);
            var radius = center.GetLength();

            src.ForEachItem((x, y, a) => 
            {
                var bassedVector = new Vector2(x - center.X, y - center.Y);

                var lenghtToCenter = bassedVector.GetLength();

                var persent = lenghtToCenter / radius;

                return func(src[x, y], persent);
            });
        }

        public static void RadialDecrease(this Matrix<int> src, double decreaseSpeed = 2)
        {
            if (decreaseSpeed == 0) { decreaseSpeed = 1; }

            src.ForEachItemRadial((val, lenghtToCenter) =>
            {
                var newVal = (1 - lenghtToCenter / decreaseSpeed) * val;

                if (newVal < 0) { newVal = 0; }

                return (int)newVal;
            });
        }

        public static void RadialDecrease(this Matrix<double> src, double decreaseSpeed = 2)
        {
            if (decreaseSpeed == 0) { decreaseSpeed = 1; }

            src.ForEachItemRadial((val, lenghtToCenter) =>
            {
                var newVal = (1 - lenghtToCenter / decreaseSpeed) * val;

                if (newVal < 0) { newVal = 0; }

                return newVal;
            });
        }

        public static Matrix<double> ToDouble(this Matrix<int> src) 
        {
            return src.Convert(a => (double)a);        
        }

        public static Matrix<int> ToInt(this Matrix<double> src)
        {
            return src.Convert(a => (int)a);
        }

        public static bool IsOutOfMatrix<T>(this Matrix<T> matrix, Vector2 point)
        {
            return MatrixHelper.IsOutOf2dArray(matrix.Width, matrix.Height, point);
        }

        #region Get Immediate Points
        public static IEnumerable<Vector2> GetImmediatePoints<T>(this Matrix<T> matrix, Vector2 point)
        {
            return MatrixHelper.GetImmediatePoints(matrix.Width, matrix.Height, point);
        }

        public static IEnumerable<Vector2> GetFullImmediatePoints<T>(this Matrix<T> matrix, Vector2 point)
        {
            return MatrixHelper.GetFullImmediatePoints(matrix.Width, matrix.Height, point);
        }

        public static IEnumerable<Vector2> GetImmediatePointsMirror<T>(this Matrix<T> matrix, Vector2 point)
        {
            return MatrixHelper.GetImmediatePointsMirror(matrix.Width, matrix.Height, point);
        }

        public static IEnumerable<Vector2> GetFullImmediatePointsMirror<T>(this Matrix<T> matrix, Vector2 point)
        {
            return MatrixHelper.GetFullImmediatePointsMirror(matrix.Width, matrix.Height, point);
        }
        #endregion

        public static void StretchOnMaximumAndMinimumValue(this Matrix<int> src, int newMin, int newMax)
        {
            var maxValue = src.GetMax();
            var minValue = src.GetMin();

            src.ForEachItem((x, y) => 
            {
                double newValue = src[x, y];

                newValue -= minValue;

                double pos = newValue / (maxValue - minValue);

                newValue = pos * (newMax - newMin) + newMin;

                return (int)newValue;
            });
        }

        public static void StretchOnMaximumAndMinimumValue(this Matrix<double> src, int newMin, int newMax)
        {
            var maxValue = src.GetMax();
            var minValue = src.GetMin();

            src.ForEachItem((x, y) =>
            {
                double newValue = src[x, y];

                newValue -= minValue;

                double pos = newValue / (maxValue - minValue);

                newValue = pos * (newMax - newMin) + newMin;

                return newValue;
            });
        }

        public static Matrix<int> Sum(this Matrix<int> m1, Matrix<int> m2)
        {
            if (m1.Width != m2.Width || m1.Height != m2.Height) { throw new NotSupportedException("Size of matrix must be the same."); }

            var result = new Matrix<int>(m1.Size);

            result.ForEachItem((x, y) => 
            {
                return m1[x, y] + m2[x, y];
            });

            return result;
        }

        public static Matrix<int> Average(this Matrix<int> m1, Matrix<int> m2)
        {
            if (m1.Width != m2.Width || m1.Height != m2.Height) { throw new NotSupportedException("Size of matrix must be the same."); }

            var result = new Matrix<int>(m1.Size);

            result.ForEachItem((x, y) =>
            {
                return (m1[x, y] + m2[x, y]) / 2;
            });

            return result;
        }

        public static Matrix<int> Average(this IEnumerable<Matrix<int>> items)
        {
            if (items.Count() == 0) { return null; }

            var result = new Matrix<int>(items.First().Size);

            result.ForEachItem((x, y) =>
            {
                var sum = 0;

                foreach (var item in items)
                {
                    sum += item[x, y];
                }

                return sum / items.Count();
            });

            return result;
        }

        public static void Smoothing(this Matrix<int> matrix, int size = 1)
        {
            matrix.ForEachItem((x, y) =>
            {
                var points = GetAdjacentXY(x, y, size, matrix.Width, matrix.Height, true);

                var values = new List<int>();

                foreach (var point in points)
                {
                    values.Add(matrix[point.Key, point.Value]);
                }

                var value = (int)values.Average();

                return value;
            });
        }

        public static void Smoothing(this Matrix<double> matrix, int size = 1)
        {
            matrix.ForEachItem((x, y) =>
            {
                var points = GetAdjacentXY(x, y, size, matrix.Width, matrix.Height, true);

                var values = new List<double>();

                foreach (var point in points)
                {
                    values.Add(matrix[point.Key, point.Value]);
                }

                var value = values.Average();

                return value;
            });
        }

        private static IEnumerable<KeyValuePair<int, int>> GetAdjacentXY(int x, int y, int size, int maxX, int maxY, bool useLoopXY = false)
        {
            var points = new List<KeyValuePair<int, int>>();

            for (int i = x - size; i <= x + size; i++)
            {
                for (int j = y - size; j <= y + size; j++)
                {
                    if (useLoopXY == true)
                    {
                        var posX = i >= 0 && i < maxX ? i : i < 0 ? maxX + i : i - maxX;
                        var posY = j >= 0 && j < maxY ? j : j < 0 ? maxY + j : j - maxY;

                        points.Add(new KeyValuePair<int, int>(posX, posY));
                    }
                    else
                    {
                        if (i >= 0 && i < maxX && j >= 0 && j < maxY)
                        {
                            points.Add(new KeyValuePair<int, int>(i, j));
                        }
                    }
                }
            }

            return points;
        }

        public static void Exponentiation(this Matrix<int> src, int _maxValue)
        {
            src.ForEachItem((x, y) =>
            {
                return (int)(Math.Pow(src[x, y], 2) / _maxValue);
            });
        }

        public static void ClearTopValue(this Matrix<int> src, int topEdge)
        {
            src.ForEachItem((x, y) =>
            {
                return src[x, y] <= topEdge ? src[x, y] : 0;
            });
        }

        public static void ClearBottomValue(this Matrix<int> src, int bottomEdge)
        {
            src.ForEachItem((x, y) =>
            {
                return src[x, y] >= bottomEdge ? src[x, y] : 0;
            });
        }

        public static Matrix<int> IncreaseOctave(this Matrix<int> src, int multiplier)
        {
            int newWidth = src.Width * multiplier;
            int newHeight = src.Height * multiplier;

            var result = new Matrix<int>(newWidth, newHeight);

            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    for (int a = 0; a < multiplier; a++)
                    {
                        for (int b = 0; b < multiplier; b++)
                        {
                            if (a == 0 && b == 0)
                            {
                                result[x * multiplier, y * multiplier] = src[x, y];
                                continue;
                            }

                            var point00 = src[x, y];
                            var point01 = src[x, (y == src.Height - 1 ? -1 : y) + 1];
                            var point10 = src[(x == src.Width - 1 ? -1 : x) + 1, y];
                            var point11 = src[(x == src.Width - 1 ? -1 : x) + 1, (y == src.Height - 1 ? -1 : y) + 1];

                            var t00 = 1 / Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                            var t01 = 1 / Math.Sqrt(Math.Pow(a, 2) + Math.Pow(multiplier - b, 2));
                            var t10 = 1 / Math.Sqrt(Math.Pow(multiplier - a, 2) + Math.Pow(b, 2));
                            var t11 = 1 / Math.Sqrt(Math.Pow(multiplier - a, 2) + Math.Pow(multiplier - b, 2));

                            var value = (point00 * t00 + point01 * t01 + point10 * t10 + point11 * t11) / (t00 + t01 + t10 + t11);

                            result[x * multiplier + a, y * multiplier + b] = (int)value;
                        }
                    }
                }
            }

            return result;
        }

        public static Matrix<int> DecreaseOctave(this Matrix<int> src, int multiplier)
        {
            int newWidth = src.Width / multiplier;
            int newHeight = src.Height / multiplier;

            var result = new Matrix<int>(newWidth, newHeight);

            result.ForEachItem((x, y) =>
            {
                return src[x * multiplier, y * multiplier];
            });

            return result;
        }

        public static Matrix<double> ResizeMatrix(this Matrix<double> matrix, int newWidth, int newHeight)
        {
            double widthStepSize = (double)matrix.Width / (double)newWidth;
            double heightStepSize = (double)matrix.Height / (double)newHeight;

            var newMatrix = new Matrix<double>(newWidth, newHeight);

            for (int newX = 0; newX < newWidth; newX++)
            {
                var oldX = newX * widthStepSize;

                for (int newY = 0; newY < newHeight; newY++)
                {
                    var oldY = newY * heightStepSize;

                    MathHelper.GetIntegerAndFractionalPart(oldX, out int stepBaseX, out double percentX);
                    MathHelper.GetIntegerAndFractionalPart(oldY, out int stepBaseY, out double percentY);

                    var increasedStepBaseX = stepBaseX < matrix.Width - 1 ? stepBaseX + 1 : 0;
                    var increasedStepBaseY = stepBaseY < matrix.Height - 1 ? stepBaseY + 1 : 0;

                    var a1 = matrix[stepBaseX, stepBaseY];
                    var a2 = matrix[increasedStepBaseX, stepBaseY];
                    var b1 = matrix[stepBaseX, increasedStepBaseY];
                    var b2 = matrix[increasedStepBaseX, increasedStepBaseY];

                    var value = MathHelper.GetSquareLerp(a1, a2, b1, b2, percentX, percentY);

                    newMatrix[newX, newY] = value;
                }
            }

            return newMatrix;
        }
    }
}
