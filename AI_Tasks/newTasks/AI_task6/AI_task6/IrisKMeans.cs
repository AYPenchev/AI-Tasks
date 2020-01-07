using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_task6
{
    public static class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public static int Next()
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next();
        }
    }
    public class IrisKMeans
    {
        private List<Iris> irises;
        private List<Iris> centroids = new List<Iris>();
        private List<List<Iris>> clusters = new List<List<Iris>>();
        private List<Iris> previousCentroids = new List<Iris>();

    public IrisKMeans(List<Iris> irises, int k)
        {
            this.irises = irises;
            for (int i = 0; i < k; ++i)
            {
                int x;
                while (true)
                {
                    x = ThreadSafeRandom.Next();
                    if (x < irises.Count / k)
                    {
                        break;
                    }
                }

                centroids.Add(new Iris(irises[x * (i + 1)])); //Forgy Initialization Method.
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
            double x = 0.0d;
            double y = 0.0d;
            double z = 0.0d;
            double w = 0.0d;
            for (int i = 0; i < clusters.Count; ++i)
            {
                for (int j = 0; j < clusters[i].Count; ++j)
                {
                    x += clusters[i][j].getSepalLength();
                    y += clusters[i][j].getSepalWidth();
                    z += clusters[i][j].getPetalLength();
                    w += clusters[i][j].getPetalWidth();
                }

                if (clusters[i].Count != 0)
                {
                    x /= clusters[i].Count;
                    y /= clusters[i].Count;
                    z /= clusters[i].Count;
                    w /= clusters[i].Count;
                }

                previousCentroids[i] = new Iris(centroids[i]);
                centroids[i] = new Iris(x, y, z, w, "Centroid");
            }
        }

        public double Distance(Iris a, Iris b)
        {
            return Math.Sqrt((a.getSepalLength() - b.getSepalLength()) * (a.getSepalLength() - b.getSepalLength()) +
                    (a.getSepalWidth() - b.getSepalWidth()) * (a.getSepalWidth() - b.getSepalWidth()) +
                    (a.getPetalLength() - b.getPetalLength()) * (a.getPetalLength() - b.getPetalLength()) +
                    (a.getPetalWidth() - b.getPetalWidth()) * (a.getPetalWidth() - b.getPetalWidth()));
        }

        private int findClosestCentroid(Iris a)
        {
            double minDistance = Distance(a, centroids[0]);
            double distance;
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
