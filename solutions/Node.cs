namespace AOC2023
{
    public class Node
    {
        public long x;
        public long y;
        public long cost;
        public char move;
        public long consecutiveMoves;
        public Node prev;

        public Node(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public Node(long x, long y, long cost, char move, long consecutiveMoves)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
            this.move = move;
            this.consecutiveMoves = consecutiveMoves;
        }

        public Node(long x, long y, long cost, char move, long consecutiveMoves, Node prev)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
            this.move = move;
            this.consecutiveMoves = consecutiveMoves;
            this.prev = prev;
        }

        protected bool Equals(Node other)
        {
            return x == other.x && y == other.y && move == other.move && consecutiveMoves == other.consecutiveMoves;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ move.GetHashCode();
                hashCode = (hashCode * 397) ^ consecutiveMoves.GetHashCode();
                return hashCode;
            }
        }
    }
}