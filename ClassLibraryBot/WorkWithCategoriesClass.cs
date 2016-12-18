using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBot
{
   public class WorkWithCategoriesClass
    {
        public void Circle(Event a, string s, out string[] mas1, out string[] mas2, out string[] mas3, out int k)
        {
            mas1 = new string[25];
            mas2 = new string[25];
            mas3 = new string[25];
            k = 0;
            for (int i = 0; i < 25; i++)
            {
                if (a.values[i].categories[0].name == s)
                {
                    mas1[k] = a.values[i].name;
                    mas2[k] = a.values[i].categories[0].name;
                    mas3[k] = a.values[i].url;
                    k = k + 1;

                }
            }
        }
        public void CircleForFilms(string s1, string s2, string[] a, string[] b, string[] c, out string[] mas1, out string[] mas2, out string[] mas3, out int q)
        {
            mas1 = new string[101];
            mas2 = new string[101];
            mas3 = new string[101];
            q = 0;
            for (int i = 1; i < 101; i++)
            {
                if (a[i].Contains(s1) == true || a[i].Contains(s2) == true)
                {
                    mas1[q] = c[i];
                    mas3[q] = a[i];
                    mas2[q] = b[i];
                    q = q + 1;
                }

            }
        }
    }
}
