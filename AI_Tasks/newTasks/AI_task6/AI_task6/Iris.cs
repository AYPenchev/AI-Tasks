using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_task6
{
    public class Iris
    {
        private double sepalLength;
        private double sepalWidth;
        private double petalLength;
        private double petalWidth;
        private string name;

    public Iris(double sepalLength, double sepalWidth, double petalLength, double petalWidth, string name)
        {
            this.sepalLength = sepalLength;
            this.sepalWidth = sepalWidth;
            this.petalLength = petalLength;
            this.petalWidth = petalWidth;
            this.name = name;
        }

        public Iris(Iris iris)
        {
            this.sepalLength = iris.sepalLength;
            this.sepalWidth = iris.sepalWidth;
            this.petalLength = iris.petalLength;
            this.petalWidth = iris.petalWidth;
            this.name = iris.name;
        }

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
        public override string ToString()
        {
            return "Iris { " + "sepal_length = " + sepalLength + ", sepal_width = " + sepalWidth + ", petal_length = " +
                    petalLength + ", petal_width = " + petalWidth + ", species = '" + name + '\'' + " }";
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null)
            {
                return false;
            }
            Iris iris = (Iris)obj;
            return HasMinimalDifference(iris.sepalLength, sepalLength, 1) && HasMinimalDifference(iris.sepalWidth, sepalWidth, 1) &&
                   HasMinimalDifference(iris.petalLength, petalLength, 1) &&
                   HasMinimalDifference(iris.petalWidth, petalWidth, 1) &&
                    name.Equals(((Iris)obj).name);
        }

        public static bool HasMinimalDifference(double value1, double value2, int units)
        {
            long lValue1 = BitConverter.DoubleToInt64Bits(value1);
            long lValue2 = BitConverter.DoubleToInt64Bits(value2);

            // If the signs are different, return false except for +0 and -0.
            if ((lValue1 >> 63) != (lValue2 >> 63))
            {
                if (value1 == value2)
                    return true;

                return false;
            }

            long diff = Math.Abs(lValue1 - lValue2);

            if (diff <= (long)units)
                return true;

            return false;
        }
    }
}
