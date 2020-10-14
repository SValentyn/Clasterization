using System.Collections.Generic;

namespace Clasterization {
    internal class Claster {
        private readonly List<Point2D> points;

        public Claster(Point2D center, List<Point2D> points) {
            GetCenter = center;
            this.points = points;
        }

        public Point2D GetCenter { get; }

        public int CountElem => points.Count;
        public List<Point2D> GetPoints => new List<Point2D>(points);
    }
}