using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Routing.Api.Helpers
{
    public static class Excercise
    {
        //Swap numbers without Using temp variable

        public static int  swapNumbers() {

            int first = 100;
            int second = 200;
            int swap = 100 + 200;
             first = swap - first;
            second = swap - second;

            return first;

        }

        public static bool IsArmstrongNumber()
        {
            int number = 407;
            double  remainder, sum = 0;
            for (int i = number; i > 0; i = i / 10)
            {
                remainder = i % 10;
                //  sum = sum + remainder * remainder * remainder;
                sum=sum+Math.Pow(remainder, 3);

            }


            //var result = Math.Pow(4, 3) + Math.Pow(0, 3) + Math.Pow(7, 3);
            if (sum == number)
                return true;

            return false;


        }

        //Find majority element in an unsorted array
        //Ex. {1,2,3,4,5,2,2,2,2}, 2 is the majority element because it accounts for more than 50% of the array
        public static int GetMajorityElement()
        {
            int[] x = new int[9] { 1, 2, 3, 4, 5, 2, 2, 2, 2 };
            Dictionary<int, int> d = new Dictionary<int, int>();
            int majority = x.Length / 2;

            //Stores the number of occcurences of each item in the passed array in a dictionary
            foreach (int i in x)
                if (d.ContainsKey(i))
                {
                    d[i]++;
                    //Checks if element just added is the majority element
                    if (d[i] > majority)
                        return i;
                }
                else
                    d.Add(i, 1);
            //No majority element
            throw new Exception("No majority element in array");
        }

        


    }
}
