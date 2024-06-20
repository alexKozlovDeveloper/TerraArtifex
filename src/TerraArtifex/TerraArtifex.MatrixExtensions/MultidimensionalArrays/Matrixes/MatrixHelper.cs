using TerraArtifex.MatrixExtensions.Points;

namespace TerraArtifex.MatrixExtensions.MultidimensionalArrays.Matrixes
{
    public static class MatrixHelper
    {
        public static bool IsOutOf2dArray(int width, int height, Vector2 point)
        {
            if (point.X >= 0 && point.X < width)
            {
                if (point.Y >= 0 && point.Y < height)
                {
                    return false;
                }
            }

            return true;
        }

        #region Get Immediate Points
        public static IEnumerable<Vector2> GetImmediatePoints(int width, int height, Vector2 point)
        {
            return Constants.ImmediatePointOffsets
                .Select(a => point.NewRelativePoint(a.x, a.y))
                .Where(a => IsOutOf2dArray(width, height, a) == false);
        }

        public static IEnumerable<Vector2> GetFullImmediatePoints(int width, int height, Vector2 point)
        {
            return Constants.FullImmediatePointOffsets
                .Select(a => point.NewRelativePoint(a.x, a.y))
                .Where(a => IsOutOf2dArray(width, height, a) == false);
        }

        public static IEnumerable<Vector2> GetImmediatePointsMirror(int width, int height, Vector2 point)
        {
            return Constants.ImmediatePointOffsets
                .Select(a => point.NewRelativePointMirror(a.x, a.y, width, height));
        }

        public static IEnumerable<Vector2> GetFullImmediatePointsMirror(int width, int height, Vector2 point)
        {
            return Constants.FullImmediatePointOffsets
                .Select(a => point.NewRelativePointMirror(a.x, a.y, width, height));
        }
        #endregion        
    }
}
