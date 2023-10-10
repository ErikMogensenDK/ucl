using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAndSorting
{
    public class IntArrayHelper
    {
        public void SortAscending(int[] intArray)
        {
            int temp;
            for (int i = 0; i <= intArray.Length-1; i++)
            {
                for (int j = i+1; j < intArray.Length; j++)
                {
                    if (intArray[i]>intArray[j])                    
                    {
                        temp = intArray[i];
                        intArray[i] = intArray[j];
                        intArray[j] = temp;
                    }
                }
            }
        }
        public void SortAscendingAndReverse(int[] intArray)
        {
            SortAscending(intArray);
            Reverse(intArray);
        }
        public void Reverse(int[] intArray)
        {
            int[] newArray = new int[intArray.Length];
            for (int i = 0; i < intArray.Length; i++)
            {
                int reversedIndex = intArray.Length -i-1;
                newArray[i] = intArray[reversedIndex];
            }
            //for (int i = 0; i < intArray.Length; i++)
            //{
            //    intArray[i] = newArray[i];
            //}
            intArray = newArray;
        }
    }
}