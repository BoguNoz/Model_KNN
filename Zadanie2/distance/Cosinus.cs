using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2.distance
{
    public class Cosinus :IDistance
    {
        public double CalcDist(double[] x, double[] y)
        {
            if (x.Length != y.Length) throw new Exception("Size dont match");

            var multiper = x.Zip(y, (a,b) => a * b).Sum();

            var den1 = Math.Sqrt(x.Sum(a => a * a));
            var den2 = Math.Sqrt(y.Sum(b => b * b));

            if (den1 == 0 || den2 == 0) throw new Exception("Lenght of one vectors is 0");

            return 1 - (multiper / (den1 * den2));
        }
    }
}

//Metoda clasify  w KNN , która zwraca string
//public string clasify(double[] x)
//dostajemy nowy wektor i liczymy dystans do wszystkich wektorów
//wynik == zwracamy który jest k najbliższe jeśli 