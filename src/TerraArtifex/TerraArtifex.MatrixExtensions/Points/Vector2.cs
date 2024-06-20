namespace TerraArtifex.MatrixExtensions.Points
{
    public class Vector2 
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{X}:{Y}]";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object? other)
        {
            var otherVector2 = other as Vector2;

            if (this.X == otherVector2.X && this.Y == otherVector2.Y) 
            {
                return true;
            }

            return false;
        }
    }
}
