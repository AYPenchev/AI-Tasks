using System;
using System.Collections.Generic;
using System.IO;

namespace AI_task5
{
    public class IrisDataParser
    {

        private List<Iris> trainList = new List<Iris>();
        private List<Iris> testList = new List<Iris>();

        public static void Main()
        {
            IrisDataParser dataParser = new IrisDataParser();
            dataParser.parse();
            IrisKNN kNN = new IrisKNN(dataParser.trainList, dataParser.testList, 3);
            kNN.classify();
            Console.WriteLine("==========================TEST RESULTS===========================");
            int countWrong = 0, countRight = 0;
            foreach (Iris ir in dataParser.testList)
            {
                int i = ir.IsClassificationRight() ? ++countRight : ++countWrong;
                Console.WriteLine(ir);
            }
            Console.WriteLine("==========================RESULTS===========================");
            Console.WriteLine("% of right types: " + (countRight * 100 / dataParser.testList.Count)
                    + "% " + "% of wrong types: " + (countWrong * 100 / dataParser.testList.Count) + "%");
        }

        private void parse()
        {
            HashSet<int> idxTestSet = new HashSet<int>();  // indexes for trainList
            Random random = new Random();

            // random numbers [0, 149]
            do
            {
                idxTestSet.Add(random.Next(149));
            } while (idxTestSet.Count != 50);

            // parse data
            StreamReader reader = new StreamReader("../../../iris.txt");
            try
            {
                int idx = -1;
                string str;
                string[] array;

                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                    ++idx;
                    array = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (idxTestSet.Contains(idx))
                    {
                        testList.Add(new Iris(double.Parse(array[0]), double.Parse(array[1]),
                                double.Parse(array[2]), double.Parse(array[3]), array[4]));
                    }
                    else
                    {
                        trainList.Add(new Iris(double.Parse(array[0]), double.Parse(array[1]),
                                double.Parse(array[2]), double.Parse(array[3]), array[4]));
                    }
                }

                Console.WriteLine("==========================INPUT TRAIN DATA===========================");
                foreach (Iris ir in trainList)
                {
                    Console.WriteLine(ir);
                }
                Console.WriteLine("==========================INPUT TEST DATA===========================");
                foreach (Iris ir in testList)
                {
                    Console.WriteLine(ir);
                }

                // normalize data
                normalize();

            } catch (IOException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void normalize()
        {
            double minSL = 100, maxSL = -1, minSW = 100, maxSW = -1, minPL = 100, maxPL = -1, minPW = 100, maxPW = -1;

            foreach (Iris ir in trainList)
            {
                minSL = minSL > ir.getSepalLength() ? ir.getSepalLength() : minSL;
                maxSL = maxSL < ir.getSepalLength() ? ir.getSepalLength() : maxSL;

                minSW = minSW > ir.getSepalWidth() ? ir.getSepalWidth() : minSW;
                maxSW = maxSW < ir.getSepalWidth() ? ir.getSepalWidth() : maxSW;

                minPL = minPL > ir.getPetalLength() ? ir.getPetalLength() : minPL;
                maxPL = maxPL < ir.getPetalLength() ? ir.getPetalLength() : maxPL;

                minPW = minPW > ir.getPetalWidth() ? ir.getPetalWidth() : minPW;
                maxPW = maxPW < ir.getPetalWidth() ? ir.getPetalWidth() : maxPW;
            }

            foreach (Iris ir in testList)
            {
                minSL = minSL > ir.getSepalLength() ? ir.getSepalLength() : minSL;
                maxSL = maxSL < ir.getSepalLength() ? ir.getSepalLength() : maxSL;

                minSW = minSW > ir.getSepalWidth() ? ir.getSepalWidth() : minSW;
                maxSW = maxSW < ir.getSepalWidth() ? ir.getSepalWidth() : maxSW;

                minPL = minPL > ir.getPetalLength() ? ir.getPetalLength() : minPL;
                maxPL = maxPL < ir.getPetalLength() ? ir.getPetalLength() : maxPL;

                minPW = minPW > ir.getPetalWidth() ? ir.getPetalWidth() : minPW;
                maxPW = maxPW < ir.getPetalWidth() ? ir.getPetalWidth() : maxPW;
            }

            foreach (Iris ir in trainList)
            {
                ir.normalize(minSL, maxSL, minSW, maxSW, minPL, maxPL, minPW, maxPW);
            }

            foreach (Iris ir in testList)
            {
                ir.normalize(minSL, maxSL, minSW, maxSW, minPL, maxPL, minPW, maxPW);
            }

            Console.WriteLine("==========================NORM TRAIN DATA===========================");
            foreach (Iris ir in trainList)
            {
                Console.WriteLine(ir.normToString());
            }
            Console.WriteLine("==========================NORM TEST DATA===========================");
            foreach (Iris ir in testList)
            {
                Console.WriteLine(ir.normToString());
            }
        }

        public List<Iris> getTrainList()
        {
            return trainList;
        }

        public List<Iris> getTestList()
        {
            return testList;
        }
    }
}
