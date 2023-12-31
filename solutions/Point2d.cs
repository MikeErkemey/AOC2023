﻿using System;

namespace AOC2023
{
    public class Point2d
    {
        public long x;
        public long y;
        public char move;

        public Point2d(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public Point2d(long x, long y, char move)
        {
            this.x = x;
            this.y = y;
            this.move = move;
        }


        public long DistanceRoute(Point2d other)
        {
            return Math.Abs(other.x - x) + Math.Abs(other.y - y);
        }

        protected bool Equals(Point2d other)
        {
            return x == other.x && y == other.y;
        }

        public override string ToString()
        {
            return this.x + " - " + this.y;
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
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }
    }
}