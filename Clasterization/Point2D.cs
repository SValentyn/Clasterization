using System;

namespace Clasterization {
    internal struct Point2D {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D(double x, double y) {
            X = x;
            Y = y;
        }

        public static double DistanceBetween(Point2D one, Point2D two) {
            return Math.Sqrt((one.X - two.X) * (one.X - two.X) + (one.Y - two.Y) * (one.Y - two.Y));
        }

        public static bool IsEqual(Point2D firstPoint, Point2D secondPoint) {
            return firstPoint.X.Equals(secondPoint.X) && firstPoint.Y.Equals(secondPoint.Y);
        }
    }
}