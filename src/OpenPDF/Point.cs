using System;

namespace OpenPDF
{
    public class Point : IEquatable<Point>
    {
        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }

        public float Y { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return other != null &&
                   this.X == other.X &&
                   this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + this.X.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Y.GetHashCode();
            return hashCode;
        }
    }
}
