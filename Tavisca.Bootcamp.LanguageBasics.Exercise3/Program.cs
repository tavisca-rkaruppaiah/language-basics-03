﻿using System;
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

         

       int[] caloriesArray;
        List<int> indexes = new List<int>();

        public int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            
            int[] resultArray = new int[dietPlans.Length];

            caloriesArray = new int[protein.Length];

            for (int i = 0; i < caloriesArray.Length; i++)
            {
                caloriesArray[i] = protein[i] * 5 + carbs[i] * 5 + fat[i] * 9;
            }

            for (int i = 0; i < dietPlans.Length; i++)
            {
                if (dietPlans.Length == 0)
                {
                    resultArray[i] = 0;
                }
                else
                {
                    
                    for (int k = 0; k < protein.Length; k++)
                    {
                        indexes.Add(k);
                    }

                    resultArray[i] = GetUniqueIndex(dietPlans[i], protein, carbs, fat);
                    
                }
            }





            return resultArray;

        }

        public int GetUniqueIndex( string dietPlans, int[] protein, int[] carbs, int[] fat)
        {
            foreach (char singleLetter in dietPlans)
            {
                switch (singleLetter)
                {
                    case 'P':
                        indexes = FindIndex(protein, indexes, 1);
                        break;
                    case 'p':
                        indexes = FindIndex(protein, indexes, 0);
                        break;
                    case 'C':
                        indexes = FindIndex(carbs, indexes, 1);
                        break;
                    case 'c':
                        indexes = FindIndex(carbs, indexes, 0);
                        break;
                    case 'F':
                        indexes = FindIndex(fat, indexes, 1);
                        break;
                    case 'f':
                        indexes = FindIndex(fat, indexes, 0);
                        break;
                    case 'T':
                        indexes = FindIndex(caloriesArray, indexes, 1);
                        break;
                    case 't':
                        indexes = FindIndex(caloriesArray, indexes, 0);
                        break;
                }

                
            }


            return indexes[0];
        }



        public List<int> FindIndex(int[] array, List<int> arrayIndex, int input)
        {



            int minMaxValue = array[arrayIndex[0]];
       

            if (arrayIndex.Count > 0)
            {
                if (input == 1)
                {
                    for (int i = 0; i < arrayIndex.Count; i++)
                    {
                        if (minMaxValue < array[arrayIndex[i]])
                        {
                            minMaxValue = array[arrayIndex[i]];
                        }
                    }
                }
                else if (input == 0)
                {
                    for (int i = 0; i < arrayIndex.Count; i++)
                    {
                        if (minMaxValue > array[arrayIndex[i]])
                        {
                            minMaxValue = array[arrayIndex[i]];
                        }
                    }
                }
            }
            else
            {
                return arrayIndex;
            }

            List<int> indexArray = new List<int>();
            foreach (int i in arrayIndex)
            {
                if (array[i] == minMaxValue)
                {
                    indexArray.Add(i);
                }
            }



            return indexArray;
        }
    }
