using System;

namespace AI_task6
{
    class AI_task6
    {
        static void Main()
        {
            CSVLoader csvLoader = new CSVLoader();
            IrisKMeans kMeansIris = new IrisKMeans(csvLoader.load("../iris.txt"), 3);
            kMeansIris.cluster();
            kMeansIris.printClusters();
        }
    }
}
