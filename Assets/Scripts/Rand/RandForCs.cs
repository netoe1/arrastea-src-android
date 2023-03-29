using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
                                            MADE BY ELY NETO 27/12/2022
 */
namespace RandForCs
{
    public class Rand
    {
        public List<int> drawnNumbers = new List<int>();
        public void clearList()
        {
            drawnNumbers.Clear();
        }

        public int generateNumbers(int limit)
        {
            System.Random random = new System.Random();
            int drawn;
            int cont = 0;
            
            do
            {
                drawn = random.Next(0,limit);
                cont++;
                if (cont == 500)
                {
                    UnityEngine.Debug.Log("A função está com problema, opções possiveis esgotadas!");
                }
            }
            while (drawnNumbers.Contains(drawn));

            drawnNumbers.Add(drawn);
            return drawn;
        }
    }
}
