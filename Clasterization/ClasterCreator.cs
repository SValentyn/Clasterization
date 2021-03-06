﻿using System;
using System.Collections.Generic;

namespace Clasterization {
    internal class ClasterCreator2D {
        private List<Point2D> centersOfClasters;
        private List<Claster> clasters;
        private List<Point2D> points;
        private Point2D minPoint, maxPoint;

        public List<Point2D> GetCentersOfClasters => new List<Point2D>(centersOfClasters);
        public List<Claster> GetClasters => new List<Claster>(clasters);
        public List<Point2D> GetPoints => new List<Point2D>(points);
        public int CountOfClasters { get; }
        public Point2D MinPoint2D => minPoint;
        public Point2D MaxPoint2D => maxPoint;
        public bool IsAlreadyClastered { get; private set; }

        public ClasterCreator2D(List<Point2D> points, int countOfClasters) {
            if (points == null || points.Count <= 0 || countOfClasters <= 0) throw new Exception();

            this.points = new List<Point2D>(points);
            CountOfClasters = countOfClasters;

            _SetMinAndMax();
            _CreateRandomCenterOfClasters();
            _DoOneStepCreateClaster();
        }

        public ClasterCreator2D(List<Point2D> points, int countOfClasters, List<Point2D> centersOfClasters) {
            if (points == null || points.Count <= 0 || this.centersOfClasters == null || countOfClasters <= 0)
                throw new Exception();
            if (countOfClasters != centersOfClasters.Count) throw new Exception();

            this.points = new List<Point2D>(points);
            this.centersOfClasters = new List<Point2D>(centersOfClasters);
            CountOfClasters = countOfClasters;

            _SetMinAndMax();
            DoOneStepCreateClaster();
        }

        private void _SetMinAndMax() {
            double maxX, maxY, minX, minY, x, y;

            maxX = points[0].X;
            minX = points[0].X;

            maxY = points[0].Y;
            minY = points[0].Y;

            for (var i = 1; i < points.Count; i++) {
                x = points[i].X;
                y = points[i].Y;

                if (x > maxX) maxX = x;
                if (x < minX) minX = x;

                if (y > maxY) maxY = y;
                if (y < minY) minY = y;
            }

            maxPoint = new Point2D((int) maxX + 1, (int) maxY + 1);
            minPoint = new Point2D((int) minX - 1, (int) minY - 1);
        }

        private void _CreateRandomCenterOfClasters() {
            centersOfClasters = new List<Point2D>();

            var random = new Random();

            for (var i = 0; i < CountOfClasters; i++) {
                double x = random.Next((int) minPoint.X, (int) maxPoint.X);
                double y = random.Next((int) minPoint.Y, (int) maxPoint.Y);
                centersOfClasters.Add(new Point2D(x, y));
            }
        }

        public void DoOneStepCreateClaster() {
            if (!IsAlreadyClastered) {
                RecalculateCenterClasters();
                _DoOneStepCreateClaster();
            }
        }

        private void RecalculateCenterClasters() {
            List<Point2D> points;
            double sumX, sumY;
            double averageX, averageY;
            var isChanged = false;
            Point2D newCenter;

            for (var i = 0; i < clasters.Count; i++) {
                points = clasters[i].GetPoints;
                sumX = 0;
                sumY = 0;

                foreach (var point in points) {
                    sumX += point.X;
                    sumY += point.Y;
                }

                averageX = sumX / points.Count;
                averageY = sumY / points.Count;
                newCenter = new Point2D(averageX, averageY);

                if (!Point2D.IsEqual(newCenter, centersOfClasters[i])) isChanged = true;

                centersOfClasters[i] = newCenter;
            }

            IsAlreadyClastered = !isChanged;
        }

        private void _DoOneStepCreateClaster() {
            var clasters = new List<Point2D>[CountOfClasters];
            for (var i = 0; i < clasters.Length; i++) clasters[i] = new List<Point2D>();

            double minLength = 0;
            var minSizePoint = 0;

            foreach (var point in points) {
                var isFirst = true;
                for (var j = 0; j < CountOfClasters; j++) {
                    var length = Point2D.DistanceBetween(point, centersOfClasters[j]);

                    if (isFirst) {
                        minLength = length;
                        minSizePoint = j;
                        isFirst = false;
                    }
                    else if (minLength > length) {
                        minLength = length;
                        minSizePoint = j;
                    }
                }

                clasters[minSizePoint].Add(point);
            }

            this.clasters = new List<Claster>();
            for (var i = 0; i < clasters.Length; i++) {
                this.clasters.Add(new Claster(centersOfClasters[i], clasters[i]));
            }
        }
    }
}