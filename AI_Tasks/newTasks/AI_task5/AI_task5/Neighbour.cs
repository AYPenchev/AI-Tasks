namespace AI_task5
{
    public class Neighbour
    {
        private double distance;
        private string type;

        public Neighbour(double distance, string type)
        {
            this.distance = distance;
            this.type = type;
        }

        public double getDistance()
        {
            return distance;
        }

        public void setDistance(double distance)
        {
            this.distance = distance;
        }

        public string getType()
        {
            return type;
        }

        public void setType(string type)
        {
            this.type = type;
        }
    }
}
