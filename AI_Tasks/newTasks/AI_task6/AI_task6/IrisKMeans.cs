using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_task6
{
    public class IrisKMeans
    {
        private List<Iris> irises;
        private List<Iris> centroids = new List<Iris>();
        private List<List<Iris>> clusters = new List<List<Iris>>();
        private List<Iris> previousCentroids = new List<Iris>();


        private static readonly object syncObject;
        private static readonly Random generator = new Random();

        public static int Next(int maxValue)
        {
            int x = generator.Next();
            if (x < maxValue)
            {
                lock (syncObject) return x;
            }
            return Next(maxValue);
        }

    public IrisKMeans(List<Iris> irises, int k)
        {
            this.irises = irises;
            for (int i = 0; i < k; ++i)
            {
                centroids.Add(new Iris(irises[Next(irises.Count / k) * (i + 1)])); //Forgy Initialization Method.
                previousCentroids.Add(new Iris(centroids[i]));
                clusters.Add(new List<Iris>());
            }
        }

        public void cluster()
        {
            bool isOver = false;
            int counter = 0;
            while (!isOver)
            {
                clearClusters();
                addPointsToClusters();
                moveCentroids();
                ++counter;
                isOver = !haveCentroidsMoved();
            }
            Console.WriteLine("It took " + counter + " iterations to cluster.");
        }

        public void printClusters()
        {
            for (int i = 0; i < clusters.Count; ++i)
            {
                Console.WriteLine("Cluster " + (i + 1));
                for (int j = 0; j < clusters[i].Count; ++j)
                {
                    Console.WriteLine(clusters[i][j]);
                }
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            }
        }

        private bool haveCentroidsMoved()
        {
            for (int i = 0; i < centroids.Count; ++i)
            {
                if (!centroids[i].Equals(previousCentroids[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private void clearClusters()
        {
            for (int i = 0; i < clusters.Count; ++i)
            {
                clusters[i].Clear();
            }
        }

        private void addPointsToClusters()
        {
            for (int i = 0; i < irises.Count; ++i)
            {
                clusters[findClosestCentroid(irises[i])].Add(irises[i]);
            }
        }

        private void moveCentroids()
        {
            decimal x = 0.0m;
            decimal y = 0.0m;
            decimal z = 0.0m;
            decimal w = 0.0m;
            for (int i = 0; i < clusters.Count; ++i)
            {
                for (int j = 0; j < clusters[i].Count; ++j)
                {
                    x += clusters[i][j].getSepalLength();
                    y += clusters[i][j].getSepalWidth();
                    z += clusters[i][j].getPetalLength();
                    w += clusters[i][j].getPetalWidth();
                }
                x /= clusters[i].Count;
                y /= clusters[i].Count;
                z /= clusters[i].Count;
                w /= clusters[i].Count;
                previousCentroids[i] = new Iris(centroids[i]);
                centroids[i] = new Iris(x, y, z, w, "Centroid");
            }
        }

        public decimal Distance(Iris a, Iris b)
        {
            return (decimal)Math.Sqrt((double)((a.getSepalLength() - b.getSepalLength()) * (a.getSepalLength() - b.getSepalLength()) +
                    (a.getSepalWidth() - b.getSepalWidth()) * (a.getSepalWidth() - b.getSepalWidth()) +
                    (a.getPetalLength() - b.getPetalLength()) * (a.getPetalLength() - b.getPetalLength()) +
                    (a.getPetalWidth() - b.getPetalWidth()) * (a.getPetalWidth() - b.getPetalWidth())));
        }

        private int findClosestCentroid(Iris a)
        {
            decimal minDistance = Distance(a, centroids[0]);
            decimal distance;
            int closestCentroid = 0;
            for (int i = 1; i < centroids.Count; ++i)
            {
                distance = Distance(a, centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCentroid = i;
                }
            }
            return closestCentroid;
        }
    }

}
