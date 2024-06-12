using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie2.distance;

namespace Zadanie2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var data1 = new Dane();
            data1.ReadData("iris.data");

            double[] vector = { 1, 1, 1, 1 }; //Testowany wektor

            //data1.Nor();
            //Console.WriteLine("Metoda CV");
            //Walidator wal1 = new Walidator(data1, 10);
            //Console.WriteLine(wal1.CV(10));


            Console.WriteLine("\nMetoda LOO");
            IDistance distance = new Euklides();

            var data2 = new Dane();
            data2.ReadData("iris.data");
            data2.Nor();
            KNN knn = new KNN(data2, 10, distance);
            Walidator wal2 = new Walidator(data2, knn);

            Console.WriteLine(wal2.LOO());
            Console.WriteLine(knn.Classify(vector));

            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
