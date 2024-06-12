using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace iris
{
    public class Data 
    {
        public double[][] vectorTable;
        int x;
        int y;
        public void ReadData(string fileLocation)
        {
            int j = 0;
            StreamReader file = new StreamReader(fileLocation);


            var line = file.ReadLine();

            x = line.Split(',').Length;
            y = 0;

            while (line != null)
            {
                y++;
                line = file.ReadLine();
            }

            file.BaseStream.Seek(0, SeekOrigin.Begin);

            string l = file.ReadLine();

            vectorTable = new double[y][];
            for (int i = 0; i < y; i++)
                vectorTable[i] = new double[x];

            while (l != null)
            {
                var toTable = l.Split(',');
                if (toTable.Length > 1)
                {
                    for (int i = 0; i < x - 1; i++)
                        vectorTable[j][i] = Double.Parse(toTable[i], CultureInfo.InvariantCulture);
                    j++;
                }
                l = file.ReadLine();               

            }

        }

        public void List()
        {

            for (int z = 0; z < 10; z++)
            {
                for (int i = 0; i < x - 1; i++) Console.Write($" {vectorTable[z][i]}");
                Console.WriteLine();
            }
        }
    }
}

//Zrobić standaryzacje danych 1 2 3 4 5 E(etykietka)