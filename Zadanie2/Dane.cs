using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using Zadanie2.distance;

namespace Zadanie2
{
    public class Dane
    {
        private double[][] tab;
        //private string[] ends;
        public List<string> Ends = new List<string>();

        public List<int> index { get; set; }   

        public double[] this[int i] { get => tab[index[i]]; }

        public double[][] Tab { get => tab; }
        //public string[] Ends { get => ends; }

        public Dane()
        {

        }

        //Konstruktor kopiujący
        public Dane(Dane data)
        {
            if(data == null) throw new ArgumentNullException("data is null");

            tab = new double[data.tab.Length][];
            for (int i = 0; i < tab.Length; i++)
                tab[i] = new double[data.tab[0].Length];

            for (int i = 0; i < data.tab.Length; i++)
                for (int j = 0; j < data.tab[0].Length; j++)
                    tab[i][j] = data.tab[i][j];

        }

        public void ReadData(string file)
        {
            //Stworzenie z zawartości pliku tablice 2D stringów by ułatwić konwersje  
            StreamReader sr = new StreamReader(file);
            string[][] temp = sr.ReadToEnd().Split('\n').Select(d => d.Split(',').ToArray()).Where(d => d.Length > 1).ToArray();

            //Znalezienie wielkości tablicy
            int width = temp[0].Length - 1; //zakładamy, że ostatnia kolumna jest etykietką
            int lenght = temp.GetLength(0);

            //ends = new string[lenght];

            //Utworzenie tablicy tab w odpowiednim rozmiarze
            tab = new double[lenght][];
            for (int i = 0; i < lenght; i++)
                tab[i] = new double[width];

            //Zapełnienie tablicy wartośianmi
            for (int i = 0; i < lenght; i++)
                for (int j = 0; j < width + 1; j++) {
                    double result;
                    //Sprawdzenie czy wartość jest konwerowalna
                    if (Double.TryParse(temp[i][j], NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                        tab[i][j] = result;
                    //else ends[i] = temp[i][j];
                    else Ends.Add(temp[i][j]);
                }

            index = Enumerable.Range(0, tab.Length).ToList();

        }

        public void StanData()
        {
            double[] label = new double[tab.Length];
            for (int i = 0; i < tab.Length; i++)
            {
                var mean  = tab[i].Aggregate((a,b) => a + b) / tab[i].Length; // Obliczenie średniej z wiersza
                var sigma = Math.Sqrt(tab[i].Aggregate((a, b) => a += Math.Pow((b - mean),2)) / (tab[i].Length - 1)); // Obliczenie odchylenia standardowego
                label[i] = Math.Round(tab[i].Aggregate((a, b) => a += ((b - mean) / sigma)),3); // Podstawienie danych do wzoru xi - mean / sigma, zaokrąglenie do 3 miejsc po przecinku
            }

            for (int i = 0; i < tab.Length; i++)
            {
                Array.Resize(ref tab[i], tab[i].Length + 1); // Dodanie nowego miejsca w tablicy 
                tab[i][tab[i].Length - 1] = label[i]; 
            }

        }

        //Normalizacja danych
        public void Nor()
        {
            double[] label = new double[tab[0].Length];
            for (int j = 0; j < tab[0].Length; j++)
            {
                var mean = Enumerable.Range(0, tab.Length).Aggregate(0.0, (a, i) => a + tab[i][j]) / tab.Length; // Obliczenie średniej z kolumny
                var sigma = Math.Sqrt(Enumerable.Range(0, tab.Length).Aggregate(0.0, (a, i) => a + Math.Pow((tab[i][j] - mean), 2)) / (tab.Length - 1)); // Obliczenie odchylenia standardowego
                for (int i = 0; i < tab.Length; i++)
                    tab[i][j] = (tab[i][j] - mean) / sigma; // Podstawienie danych do wzoru xi - mean / sigma
            } 
        }

        public void ListData()
        {
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[0].Length; j++)
                {
                    if (tab[i][j].ToString().Length > 1) Console.Write($"{tab[i][j]} ");
                    else Console.Write($"{tab[i][j]},0 ");
                }
                Console.WriteLine();
            }
        }

    }
}
