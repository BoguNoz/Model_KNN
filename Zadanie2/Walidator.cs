using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Zadanie2.distance;

namespace Zadanie2
{
    public class Walidator
    {
        IMachine knn = null;
        Dane data = null;

        Random rand = new Random();
        //IDistance dist;

        public int[,] confMatrix;
        public Dictionary<string, int> classes;


        public Walidator(Dane data, IMachine metod)
        {
            this.data = data;
            this.knn = metod;
        }

        public float LOO()
        {
            float good = 0;
            float bad = 0;

            //Utworzenie Macierzy Niepewności
            classes = new Dictionary<string, int>();
            var classTable = data.Ends.GroupBy(g => g).Select(g => g.First()).ToArray();
            for (int i = 0; i < classTable.Length; i++)
                classes.Add(classTable[i], i);

            classes.Add("inpas",classes.Count);

            confMatrix = new int[classTable.Length + 1, classTable.Length + 1];

            for (int i = 0; i < classTable.Length; i++)
                for (int j = 0; j < classTable.Length; j++)
                    confMatrix[i, j] = 0;


            for (int i = 0; i < data.index.Count; i++)
            {
                
                //Przygotowanie listy do walidacji
                var testVector = data[i];
                data.index.Remove(i);

                //Walidacja
                var temp = knn.Classify(testVector);
                if (temp == data.Ends[i]) good++;
                else bad++;
                //Console.WriteLine($"Orginal: {i+1}, {data.Ends[i]} | Walidacja: {temp} Liczba błędów: {bad}"); //Test line

                //Dodanie do Macierzy Niepewności
                AddToMatrix(data.Ends[i], temp);

                //Przywrucenie pierwotnej listy
                data.index.Insert(i, i);
    
            }

            var check = confMatrix;

            return good / (bad + good); //Zwrócenie wyniku 
        }

        public float CV(int n)
        {
            if (n == 1) throw new Exception("Your n = 1 so, you should use LOO method");

            //Polosowanie tablicy indeksów
            data.index = data.index.OrderBy(i => rand.Next()).ToList();

            float result = 0; //Wynnik

            //Ustalenie wielkości klasy testowej
            int startIndex = 0;
            int lenghtOfRange = (data.index.Count / n);

            for (int i = 0; i < n; i++)
            {
                //Zmienne temporalne
                float good = 0;
                float bad = 0;
                int j = 0;

                //Tworzenie tablic przechowujących testową klase
                var indexs = data.index.GetRange(startIndex, lenghtOfRange);
                
                //Usuwanie danych z klasy testowych z puli testowanych 
                data.index.RemoveRange(startIndex, lenghtOfRange);

                //Walidacja
                foreach (var vector in indexs)
                {
                    var temp = knn.Classify(data.Tab[vector]);
                    if (temp == data.Ends[indexs[j]]) good++;
                    else bad++;
                    j++;
                }

                //Przywracanie pierwotnego stanu
                data.index.InsertRange(startIndex, indexs);

                startIndex += lenghtOfRange;
                result += good / (bad + good);
                good = 0;
                bad = 0;
            }

            return result / n ; //Zwrócenie wyniku 
        }

        public void AddToMatrix(string c1, string c2)
        {
            int d1 = classes[c1];
            int d2 = classes[c2];
            confMatrix[d1, d2] += 1;
        }
    }
}
