namespace AOC2023
{
    public class Point2d
    {
        private int x;
        private int y;

        public Point2d(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        protected bool Equals(Point2d other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point2d)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }
    }
}