using System.Globalization;
using System.Xml.XPath;

namespace LysApp;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public static int SockMerchant(int [] sockArray)
    {
        // 1, 2, 2, 2
        var uniqueSockMap = new Dictionary<int, int>();
        for(var i = 0; i < sockArray.Length;i++)
        {
            var sock = sockArray[i];
            if(!uniqueSockMap.ContainsKey(sock)){
                uniqueSockMap.Add(sock, 0);
            }

            uniqueSockMap[sock] += 1;
        }

        //key: 1, val: 1 
        //key: 2, val: 3

        var pairs = 0; 
        foreach(var value in uniqueSockMap.Values)
            pairs += value / 2;


        int [] uniqueSocks = GetDistinctNumbers(sockArray);
        int [] pairs = new int[uniqueSocks.Count()];
        for (int i = 0; i< uniqueSocks.Count(); i++)
        {
            int sockCount = 0;
            int pairCount = 0;
            for (int j = 0; j < sockArray.Length; j++)
            {
                if (uniqueSocks[i] == sockArray[j])
                {
                    sockCount++;
                }
                if (sockCount==2)
                {
                    pairCount++;
                    sockCount = 0;
                }
            }
            pairs[i] = pairCount;
        }
        int nOfPairs = 0;
        foreach (int pair in pairs)
        {
            nOfPairs += pair;
        }
        return nOfPairs;
    }

    public static int[] GetDistinctNumbers(int [] someArray)
    {
        HashSet<int> distinctNumbers = new();

        for (int i = 0; i < someArray.Length; i++)
        {
            // HashSet adds numbers if unique
            distinctNumbers.Add(someArray[i]);
        }
        int [] newArray = new int[distinctNumbers.Count];
        distinctNumbers.CopyTo(newArray);
        return newArray;
    }
    public static int CandleBlower(int[] arr)
    {
        int highestCandle = arr.Max();
        int candleCount = 0;
        for (int i = 0; i< arr.Length;i++)
        {
            if (arr[i] == highestCandle){
                candleCount ++;
            }
        }
        return candleCount;
    }
}
