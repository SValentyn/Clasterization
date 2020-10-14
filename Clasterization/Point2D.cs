using System;

namespace Clasterization {
    struct Point2D {
        public double X { get; set; }

        public double Y { get; set; }

        public Point2D(double x, double y) {
            this.X = x;
            this.Y = y;
        }

        public static double DistanceBetween(Point2D one, Point2D two) {
            return Math.Sqrt((one.X - two.X) * (one.X - two.X) + (one.Y - two.Y) * (one.Y - two.Y));
        }

        public static bool IsEqual(Point2D one, Point2D two) {
            if (one.X.Equals(two.X) && one.Y.Equals(two.Y)) {
                return true;
            }

            return false;
        }
    }
}