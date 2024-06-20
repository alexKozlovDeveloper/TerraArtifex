namespace TerraArtifex.MatrixExtensions.Ranges
{
    public static class RangeHelper
    {
        public static int BringValueToRange(int val, int width)
        {
            var result = val;

            var count = Math.Abs(val / width);

            if (val < 0)
            {
                result += width * (count + 1);
            }
            else
            {
                result -= width * count;
            }

            return result;
        }
    }
}

