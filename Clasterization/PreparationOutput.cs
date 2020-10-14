using System.Collections.Generic;

namespace Clasterization {
    class PreparationOutput {
        public static List<Claster> ChangeClastersToOutput(Point2D formSize, Point2D minPointArr, Point2D maxPointArr,
            List<Claster> clasters) {
            List<Point2D> changedClasters;
            List<Claster> myClasters = new List<Claster>();

            Point2D viewArrSize = new Point2D(maxPointArr.X - minPointArr.X, maxPointArr.Y - minPointArr.Y);
            double xScale = formSize.X / viewArrSize.X;
            double yScale = formSize.Y / viewArrSize.Y;
            Point2D center;

            foreach (var claster in clasters) {
                changedClasters = new List<Point2D>();
                foreach (Point2D point in claster.GetPoints) {
                    changedClasters.Add(new Point2D((point.X - minPointArr.X) * xScale,
                        (point.Y - minPointArr.Y) * yScale));
                }

                center = new Point2D((claster.GetCenter.X - minPointArr.X) * xScale,
                    (claster.GetCenter.Y - minPointArr.Y) * yScale);
                myClasters.Add(new Claster(center, changedClasters));
            }

            return myClasters;
        }
    }
}