using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_task6
{
    public class CSVLoader
    {
        public List<Iris> load(string fileName)
        {
            string line;
            List<Iris> iris = new List<Iris>();
            try
            {
                StreamReader reader = new StreamReader(fileName);
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] data = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    iris.Add(new Iris(double.Parse(data[0]), double.Parse(data[1]), double.Parse(data[2]), double.Parse(data[3]), data[4]));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return iris;
        }
    }
}
