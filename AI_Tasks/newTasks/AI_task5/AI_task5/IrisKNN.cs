using System;
using System.Collections.Generic;

namespace AI_task5
{
    public class IrisKNN
    {
        private List<Neighbour> kNeighbours = new List<Neighbour>();
        private List<Iris> train;
        private List<Iris> test;
        private int k;

        public IrisKNN(List<Iris> train, List<Iris> test, int k)
        {
            this.train = train;
            this.test = test;
            this.k = k;
        }

        public void classify()
        {
            for (int i = 0, n = test.Count; i < n; ++i)
            {
                for (int j = 0, t = train.Count; j < t; ++j)
                {
                    double distance = euclideanDistance(j, i);
                    if (j < k)
                    {
                        kNeighbours.Add(new Neighbour(distance, train[j].getType()));
                    }
                    else
                    {
                        for (int inn = 0; inn < k; ++inn)
                        {
                            if (distance < kNeighbours[inn].getDistance())
                            {
                                kNeighbours[inn].setDistance(distance);
                                kNeighbours[inn].setType(train[j].getType());
                                break;
                            }
                        }
                    }
                }
                vote(i);
                foreach(Iris ir in test)
                {
                    ir.testClassification();
                }
                kNeighbours.Clear();
            }
        }

        public double euclideanDistance(int idxTrain, int idxTest)
        {
            return Math.Sqrt(Math.Pow(test[idxTest].getNormSepalLength() - train[idxTrain].getNormSepalLength(), 2)
                + Math.Pow(test[idxTest].getNormSepalWidth() - train[idxTrain].getNormSepalWidth(), 2)
                + Math.Pow(test[idxTest].getNormPetalLength() - train[idxTrain].getNormPetalLength(), 2)
                + Math.Pow(test[idxTest].getNormPetalWidth() - train[idxTrain].getNormPetalWidth(), 2));
        }

        private void vote(int idx)
        {
            Dictionary<string, int> voteMap = new Dictionary<string, int>();
            for (int i = 0; i < k; ++i)
            {
                if (voteMap.ContainsKey(kNeighbours[i].getType()))
                {
                    voteMap.Add(kNeighbours[i].getType(), voteMap[kNeighbours[i].getType()] + 1);
                }
                else
                {
                    voteMap.Add(kNeighbours[i].getType(), 1);
                }
            }
            int maxVotes = 0;
            String typeName = "";
            foreach(KeyValuePair<string, int> pair in voteMap)
            {
                if (maxVotes < pair.Value)
                {
                    maxVotes = pair.Value;
                    typeName = pair.Key;
                }
            }
            test[idx].setClassificationType(typeName);
        }
    }
}
