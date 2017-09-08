using Processing.OpenTk.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing.OpenTk.Core.Extensions
{
    public static class PolygonExtensions
    {
        /// <summary>
        /// Calculates the center of an arbitrary closed polygon
        /// </summary>        
        public static PVector Centroid(this IEnumerable<PVector> polygon)
        {            
            var points = polygon.ToArray();
            if (points.Length < 3)
                throw new InvalidOperationException();

            PVector c = PVector.O;
            for (int i = 0; i < points.Length - 1; i++)
            {
                double x = (points[i].X + points[i + 1].X) * (points[i].X * points[i + 1].Y - points[i + 1].X * points[i].Y);
                double y = (points[i].Y + points[i + 1].Y) * (points[i].X * points[i + 1].Y - points[i + 1].X * points[i].Y);
                c += (x, y);
            }

            return c / (6 * points.Area());
        }

        /// <summary>
        /// Calculates the area of an arbitrary closed polygon
        /// </summary>    
        public static double Area(this IEnumerable<PVector> polygon)
        {
            var points = polygon.ToArray();
            if (points.Length < 3)
                throw new InvalidOperationException();

            double total = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                total += (points[i].X * points[i + 1].Y) - (points[i + 1].X * points[i].Y);
            }

            return total / 2;
        }
    }
}
