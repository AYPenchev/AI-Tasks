using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_task6
{
    public class Iris
    {
        private decimal sepalLength;
        private decimal sepalWidth;
        private decimal petalLength;
        private decimal petalWidth;
        private string name;

    public Iris(decimal sepalLength, decimal sepalWidth, decimal petalLength, decimal petalWidth, string name)
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

        public decimal getSepalLength()
        {
            return sepalLength;
        }

        public decimal getSepalWidth()
        {
            return sepalWidth;
        }

        public decimal getPetalLength()
        {
            return petalLength;
        }

        public decimal getPetalWidth()
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
            if (obj == null || typeof(Iris) != typeof(object))
            {
                return false;
            }
            Iris iris = (Iris)obj;
            return (iris.sepalLength == sepalLength) && (iris.sepalWidth == sepalWidth) &&
                    (iris.petalLength == petalLength) &&
                    (iris.petalWidth == petalWidth) &&
                    name.Equals(((Iris)obj).name);
        }
    }
}
