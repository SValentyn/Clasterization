using System.Collections.Generic;

namespace Clasterization {
    class Claster {
        private Point2D center;
        private List<Point2D> points;

        public Point2D GetCenter => center;
        public int CountElem => points.Count;
        public List<Point2D> GetPoints => new List<Point2D>(points);

        public Claster(Point2D center, List<Point2D> points) {
            this.center = center;
            this.points = points;
        }
    }
}