using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class Dummy : IMachine
    {
        Dane caseBase = null;
        private int K;

        public Dummy(Dane dane, int k) 
        {
            caseBase = dane;
            K = k;
        }  

        public string Classify(double[] vector)
        {
            if (caseBase.Tab == null) throw new ArgumentNullException(nameof(caseBase));
            if (caseBase.Tab[0].Length != vector.Length) throw new Exception($"wrong format of argument vector: vector should be {caseBase.Tab[0].Length} lenght long");

            var most = caseBase.Ends.GroupBy(c => c).OrderByDescending(g => g.Count());

            //return most.Skip(1).Any(g => g.Count() == most.Max(q => q.Count())) ? "inpas" : most.First().Key;
            return most.First().Key.ToString();
        }
    }
}
