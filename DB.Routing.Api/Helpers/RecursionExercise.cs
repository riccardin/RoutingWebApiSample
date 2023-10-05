using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Routing.Api.Helpers
{

    /*
     ny program that can be written using assignment, the if-then-else statement and the while statement 
     can also be written using assignment, if-then-else and Recursion".

      Is important to find the  "satisfy condition" in order be able to undertand when the recursion will stop
         */
    public static class RecursionExercise
    {
        public static int recursiveSum(int parameter) {

            return (parameter == 0) ? 0 : recursiveSum(parameter-1)+parameter;
                                                         //10+5=15
                                                         //6+4=10
                                                         //3+3=6
                                                         //1+2=3
                                                         //0+1=1
                                                         //0+0=0

                                                         
        }

        public static int nonRecursiveSume(int parameter) {

            int sum = 0;
            for (int i = 0; i <= parameter; i++) {
                sum++;
                 }

            return sum;
        }


        public static int getFactorial(int parameter) {

            //0!=1
            //1!=1
            //2!=2*1!=2
            //3!=3*2!=6
            //n!=n*(n-1)!

            return parameter==0?1:parameter * getFactorial(parameter - 1);
        }

        public static long getFib(int n)
        {
            if (n == 0 || n == 1)//satisfaction condition
                return n;
            return getFib(n - 2) + getFib(n - 1);
        }

        //Recursive methods are useful for getting the last innerException
        public static Exception GetInnerException(Exception ex)
        {
            return (ex.InnerException == null) ? ex : GetInnerException(ex.InnerException);
        }




        //The method seems not to have any satisfy condition because it will be satisfied in each directory automatically, 
        //if it iterates all files and doesn't find any subfolder there.
        private static Dictionary<string, string> errors = new Dictionary<string, string>();
        private static List<string> result = new List<string>();

        private static void SearchForFiles(string path)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(path))//Gets all files in the current path
                {
                    result.Add(fileName);
                }

                foreach (string directory in Directory.GetDirectories(path))//Gets all folders in the current path
                {
                    SearchForFiles(directory);//The methods calls itself with a new parameter, here!
                }
            }
            catch (System.Exception ex)
            {
                errors.Add(path, ex.Message);//Stores Error Messages in a dictionary with path in key
            }
        }



        //A child is running up a staircase with n steps, and can hop either 1, 2, or 3 steps at a time.  Implement a method to count how many possible ways the child can run up the stairs.
        //Source: Cracking the Coding Interview p. 109
        //Answer will overflow integer datatype(over 4 billion) at 37 steps

        //Recursive solution
        public static int CombosRecursive(int numStairs)
        {
            if (numStairs > 36)
                throw new Exception("Int overflow");
            if (numStairs <= 0)
                return 0;
            if (numStairs == 1)
                return 1;
            if (numStairs == 2)
                return 2;
            if (numStairs == 3)
                return 4;
            return CombosRecursive(numStairs - 1) + CombosRecursive(numStairs - 2) + CombosRecursive(numStairs - 3);
        }

     

    }


}
