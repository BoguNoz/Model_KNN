using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zadanie2.distance;

namespace Zadanie2
{
    public class KNN :IMachine
    {
        //public delegate double FuncDelegate(double[] v1, double[] v2);

        Euklides euk = new Euklides();
        Cosinus cos = new Cosinus();
        private int K;

        Dane caseBase = null;
        IDistance dist = null;

        ////Funkcje obsługujące delegata
        //public double Euk(double[] v1, double[] v2) => euk.CalcDist(v1, v2);
        //public double Cos(double[] v1, double[] v2) => cos.CalcDist(v1, v2);

        public KNN(Dane dane, int k, IDistance dis)
        {
            this.caseBase = dane;

            if (k > caseBase?.index.Count) throw new Exception("K out of range");
            K = k;

            dist = dis;
        }

        public string Classify(double[] vector)
        {
            if (caseBase.Tab == null) throw new ArgumentNullException(nameof(caseBase));
            if (caseBase.Tab[0].Length != vector.Length) throw new Exception($"wrong format of argument vector: vector should be {caseBase.Tab[0].Length} lenght long");

            double[] distance = new double[caseBase.index.Count];

            for (int i = 0; i < caseBase.index.Count; i++)
                distance[i] = dist.CalcDist(caseBase[i],vector);

            var temp = caseBase.index.Select(i => caseBase.Ends[i]).ToArray();
            Array.Sort(distance, temp);
            var segregate = temp.Take(K).GroupBy(g => g).OrderByDescending(g => g.Count());

            //Cudo:
            //var segregate = distance.Zip(caseBase.Ends, (first, second) => first + ":" + second)
            //    .OrderBy(m => m.Split(':')[0]).Take(K).Select(m => m.Split(':')[1]).ToArray()
            //    .GroupBy(g => g).OrderByDescending(g => g.Count());

            return segregate.Skip(1).Any(g => g.Count() == segregate.Max(q => q.Count())) ? "inpas" : segregate.First().Key;
        }

    }
}
