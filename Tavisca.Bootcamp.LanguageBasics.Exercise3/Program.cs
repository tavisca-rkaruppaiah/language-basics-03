using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

       int[] caloriesArray, pro, car, fa;
        int mincount = 0, maxcount=0;

        public int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            pro = protein;
            car = carbs;
            fa = fat;

            caloriesArray = new int[protein.Length];

            for(int i=0; i< caloriesArray.Length; i++)
            {
                caloriesArray[i] = protein[i] * 5 + carbs[i] * 5 + fat[i] * 9;
            }

            /*for(int i=0; i<caloriesArray.Length; i++)
            {
                Console.WriteLine(caloriesArray[i]);
            }*/

            int size = dietPlans.Length;
            int[] result = new int[size];


           for(int i=0; i< dietPlans.Length; i++)
            {
                int stringlength = dietPlans[i].Length;
                //Console.WriteLine("String Length" + stringlength);
                if(stringlength == 1)
                {
                    char c = char.Parse(dietPlans[i]);

                    result[i] = findIndex(c, protein, carbs, fat);
                    //Console.WriteLine("the result index is " + result[i]);
                }
                else
                {
                    //Console.WriteLine("Two Strings");
                    char[] ch = dietPlans[i].ToCharArray();

                    for(int j=0; j< ch.Length; j++)
                    {
                       if(ch[j].Equals('t'))
                        {
                            //Console.WriteLine("small t");

                            if(UniqueMinCalorieSize('t'))
                            {
                                result[i] = findIndex(ch[j], protein, carbs, fat);
                            }
                            else
                            {
                                continue;
                            }
                            
                            break;
                        }
                       else if(ch[j].Equals('T'))
                        {
                            if (UniqueMinCalorieSize('T'))
                            {
                                result[i] = findIndex(ch[j], protein, carbs, fat);
                            }
                            else
                            {
                                continue;
                            }
                            break;
                        }
                       else
                        {
                            result[i] = findIndex(ch[j], protein, carbs, fat);
                            break;
                        }
                    }
                }
            }





            return result;
        }

        public int findIndex(char ch, int[] p, int[] c, int[] f)
        {
            int index=-1;
            

            switch(ch)
            {
                case 'P':
                    index = FindMaximumArray(p);
                   // Console.WriteLine("the index " + index);
                    break;
                case 'p':
                    index = FindMinimumArray(p);
                   // Console.WriteLine("the index" + index);
                    break;
                case 'C':
                    index = FindMaximumArray(c);
                    break;
                case 'c':
                    index = FindMinimumArray(c);
                    break;
                case 'F':
                    index = FindMaximumArray(f);
                    break;
                case 'f':
                    index = FindMinimumArray(f);
                    break;
                case 'T':
                    index = MaximumCalorie();
                    break;
                case 't':
                    index = MinimumCalorie();
                    break;
            }


            return index;
        }

        public int FindMaximumArray(int[] array)
        {
            int max = array[0];
            int maxIndex = 0;

            for(int i=0; i<array.Length; i++)
            {
                if(max<array[i])
                {
                    max = array[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        public int FindMinimumArray(int[] array)
        {
            int MinIndex = 0;
            int min = array[0];

            for(int i=0; i<array.Length; i++)
            {
                if (min > array[i])
                {
                    min = array[i];
                    MinIndex = i;
                }
            }
            return MinIndex;
        }

        public int MaximumCalorie()
        {
            int mca = caloriesArray[0], i;
            bool flag = false;

            int mc = 0;
            for (i = 0; i < caloriesArray.Length; i++)
            {

                if (mca <= caloriesArray[i])
                {
                    mca = caloriesArray[i];
                    mc = i;
                }
            }

            return mc;
        }

        public int MinimumCalorie()
        {
            int mca = caloriesArray[0], i;
            

            int mc = 0;
            for (i = 0; i < caloriesArray.Length; i++)
            {
                if (mca >= caloriesArray[i])
                {
                    mca = caloriesArray[i];
                    mc = i;
                }
            }

            return mc;
        }

        public bool UniqueMinCalorieSize(char c)
        {
            int min, max;
            

            if(c.Equals('t'))
            {

                min=caloriesArray[MinimumCalorie()];
                for (int i = 0; i < caloriesArray.Length; i++)
                {
                   
                       if(i !=MinimumCalorie())
                    {
                        if (min == caloriesArray[i])
                        {
                            mincount = mincount++;

                        }
                    }

                    
                }

                if(mincount == 0)
                {
                    return true;
                }
            }
            else if(c.Equals('T'))
            {
                max = caloriesArray[MaximumCalorie()];
                for (int i = 0; i < caloriesArray.Length; i++)
                {

                    if (i != MaximumCalorie())
                    {
                        if (max == caloriesArray[i])
                        {
                            maxcount = maxcount++;

                        }
                    }


                }

                if(maxcount == 0)
                {
                    return true;
                }
            }

            


            return false;
        }
    }
