using TerraArtifex.MatrixExtensions.MultidimensionalArrays.Factories;
using TerraArtifex.MatrixExtensions.Points;

namespace TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes
{
    public class Matrix<T>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Size { get { return new Vector2(Width, Height); } }

        private T[][] _d2Array;

        public Matrix(int width, int height)
        {
            Width = width;
            Height = height;

            _d2Array = D2ArrayFactory.CreateEmptyD2Array<T>(Width, Height);
        }

        public Matrix(Vector2 size)
        {
            Width = size.X;
            Height = size.Y;

            _d2Array = D2ArrayFactory.CreateEmptyD2Array<T>(Width, Height);
        }

        public Matrix(T[][] src)
        {
            Width = src.GetWidth();
            Height = src.GetHeight();

            _d2Array = src.NormalizeToD2Array();
        }

        public T[][] GetAsArray()
        {
            return _d2Array.Copy();
        }

        public T this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < Width)
                {
                    if (y >= 0 && y < Height)
                    {
                        return _d2Array[x][y];
                    }
                }

                throw new ArgumentOutOfRangeException($"Indexes x:{x} y:{y} is out of range.");
            }
            set
            {
                if (x >= 0 && x < Width)
                {
                    if (y >= 0 && y < Height)
                    {
                        _d2Array[x][y] = value;
                    }
                }
            }
        }

        public T this[Vector2 point]
        {
            get
            {
                return this[point.X, point.Y];
            }
            set
            {
                this[point.X, point.Y] = value;
            }
        }

        public T GetMax()
        {
            return _d2Array.GetMax();
        }

        public T GetMin()
        {
            return _d2Array.GetMin();
        }

        public void ForEachItem(Func<int, int, T, T> func)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _d2Array[x][y] = func(x, y, _d2Array[x][y]);
                }
            }
        }

        public void ForEachItem(Func<int, int, T> func)
        {
            ForEachItem((x, y, a) => func(x, y));
        }

        public void ForEachItem(Func<T> func)
        {
            ForEachItem((x, y, a) => func());
        }

        public Matrix<T> Copy(Func<int, int, T, T> func) 
        {
            var result = new Matrix<T>(Size);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    result[x, y] = func(x, y, _d2Array[x][y]);
                }
            }

            return result;
        }

        public Matrix<T> Copy() 
        {
            return Copy((x, y, a) => a);
        }
    }
}
