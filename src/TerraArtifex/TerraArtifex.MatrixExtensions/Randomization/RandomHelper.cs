namespace TerraArtifex.MatrixExtensions.Randomization
{
    public static class RandomHelper
    {
        public static T GetRandomItem<T>(this IList<T> items, Random random)
        {
            var index = random.Next(items.Count);

            return items[index];
        }
    }
}
