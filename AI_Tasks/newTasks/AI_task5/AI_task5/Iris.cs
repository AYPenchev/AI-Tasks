using System;
using System.Collections.Generic;


namespace AI_task5
{
    public class Iris
    {
        // input data
        private double sepalLength;
        private double sepalWidth;
        private double petalLength;
        private double petalWidth;
        private string type;

        // normalize data
        private double normSepalLength;
        private double normSepalWidth;
        private double normPetalLength;
        private double normPetalWidth;

        // results data
        private string classificationType;
        private bool isClassificationRight;

        // constructor
        public Iris(double sepL, double sepW, double petL, double petW, string type)
        {
            sepalLength = sepL;
            sepalWidth = sepW;
            petalLength = petL;
            petalWidth = petW;
            this.type = type;
        }

        // Input data getters
        public double getSepalLength()
        {
            return sepalLength;
        }
        public double getSepalWidth()
        {
            return sepalWidth;
        }
        public double getPetalLength()
        {
            return petalLength;
        }
        public double getPetalWidth()
        {
            return petalWidth;
        }
        public string getType()
        {
            return type;
        }

        // normalize data getters
        public double getNormSepalLength()
        {
            return normSepalLength;
        }
        public double getNormSepalWidth()
        {
            return normSepalWidth;
        }
        public double getNormPetalLength()
        {
            return normPetalLength;
        }
        public double getNormPetalWidth()
        {
            return normPetalWidth;
        }


        // Getters for result data
        public string getClassificationType()
        {
            return classificationType;
        }
        public bool IsClassificationRight()
        {
            return this.isClassificationRight;
        }

        // Setters
        public void setClassificationType(string classificationType)
        {
            this.classificationType = classificationType;
        }

        // normalize method
        public void normalize(double minSL, double maxSL, double minSW, double maxSW, double minPL, double maxPL, double minPW, double maxPW)
        {
            normSepalLength = (double)Math.Round((sepalLength - minSL) / (maxSL - minSL) * 1000) / 1000;
            normSepalWidth = (double)Math.Round((sepalWidth - minSW) / (maxSW - minSW) * 1000) / 1000;
            normPetalLength = (double)Math.Round((petalLength - minPL) / (maxPL - minPL) * 1000) / 1000;
            normPetalWidth = (double)Math.Round((petalWidth - minPW) / (maxPW - minPW) * 1000) / 1000;
        }

        // testing result
        public void testClassification()
        {
            this.isClassificationRight = type.Equals(classificationType);
        }

        public override string ToString()
        {
            return "sepal length: " + sepalLength + " | sepal width: " + sepalWidth
                    + " | petal length: " + petalLength + " | petal width: " + petalWidth
                    + " | type: " + type + " | Classification Type: " + classificationType + " | Classification is "
                    + (IsClassificationRight() ? "right" : "wrong");
        }

        public string normToString()
        {
            return string.Format("sepal length: " + "%.2f | sepal width: " + "%.2f | petal length: " + "%.2f | petal width: "
                            + "%.2f | type: " + "%s | Classification Type: " + "%s | Classification: " + "%s"
                    , normSepalLength, normSepalWidth, normPetalLength, normPetalWidth, type, classificationType, isClassificationRight);
        }
    }
}
