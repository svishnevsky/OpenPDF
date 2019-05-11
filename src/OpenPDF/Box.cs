using System;
using System.Collections.Generic;

namespace OpenPDF
{
    public sealed class Box : IEquatable<Box>
    {
        public Box(Point topLeft, Point bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        public Point TopLeft { get; }

        public Point BottomRight { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Box);
        }

        public bool Equals(Box other)
        {
            return other != null &&
                   EqualityComparer<Point>.Default.Equals(
                       this.TopLeft, other.TopLeft) &&
                   EqualityComparer<Point>.Default.Equals(
                       this.BottomRight, other.BottomRight);
        }

        public override int GetHashCode()
        {
            int hashCode = -807028467;
            hashCode = hashCode * -1521134295 + 
                EqualityComparer<Point>.Default.GetHashCode(this.TopLeft);
            hashCode = hashCode * -1521134295 + 
                EqualityComparer<Point>.Default.GetHashCode(this.BottomRight);
            return hashCode;
        }
    }
}
