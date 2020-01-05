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

    public Iris(double sepalLength, double sepalWidth, double petalLength, double petalWidth, String name)
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
            if (obj == null || this.getType() != obj.GetType()) return false;
            Iris iris = (Iris)obj;
            return double.Compare(iris.sepalLength, sepalLength) == 0 && double.compare(iris.sepalWidth, sepalWidth) == 0 &&
                    double.compare(iris.petalLength, petalLength) == 0 &&
                    double.compare(iris.petalWidth, petalWidth) == 0 &&
                    name.Equals(((Iris)obj).name);
        }
    }
}
