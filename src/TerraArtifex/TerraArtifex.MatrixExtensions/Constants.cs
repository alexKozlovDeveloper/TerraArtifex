namespace TerraArtifex.MatrixExtensions
{
    public static class Constants
    {
        /// <summary>
        /// 
        ///  a[x][y]
        ///  
        ///       x++ ->
        ///        
        /// y++   a[-1][-1]  a[0][-1]  a[1][-1] 
        ///  |
        ///  V    a[-1][0]   a[0][0]   a[1][0]
        ///  
        ///       a[-1][1]   a[0][1]   a[1][1]
        ///       
        /// </summary>
        public static IEnumerable<(int x, int y)> ImmediatePointOffsets 
        { 
            get
            {
                return new List<(int x, int y)>
                {
                    (1, 0), // right
                    (0, 1), // bottom
                    (-1, 0), // left
                    (0, -1), // top
                };
            }
        }

        public static IEnumerable<(int x, int y)> FullImmediatePointOffsets
        {
            get
            {
                return new List<(int x, int y)>
                {
                    (1, 0), // right
                    (0, 1), // bottom
                    (-1, 0), // left
                    (0, -1), // top

                    (1, 1), // right-bottom
                    (-1, -1), // left-top
                    (1, -1), // right-top
                    (-1, 1), // left-bottom
                };
            }
        }
    }
}
