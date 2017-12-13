using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2Var12
{
    class Program
    {
        static public int ArrMax(int[] arr)
        {
            int res = arr[0];
            foreach (int elem in arr)
            {
                if (elem > res)
                {
                    res = elem;
                }
            }
            return res;
        }

        static public int Digits(int num)
        {
            int counter = 0;
            if (num == 0)
            {
                return 1;
            }
            while (num != 0)
            {
                num /= 10;
                counter++;
            }
            return counter;
        }

        static public void BucketSort(int[] arr)
        {
            int order = Digits(ArrMax(arr));
            ArrayList[] bucket = new ArrayList[10];
            for (int i = 0; i < 10; i++)
            {
                bucket[i] = new ArrayList();
            }
            for (int count = 1; count <= order; count++)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    bucket[(arr[i] % Convert.ToInt32(Math.Pow(10, count))) / Convert.ToInt32(Math.Pow(10, count - 1))].Add(arr[i]);
                }
                for (int i = 0, arrIndex = 0; i < 10 && arrIndex < arr.Length; i++)
                {
                    foreach (int el in bucket[i])
                    {
                        arr[arrIndex] = el;
                        arrIndex++;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    bucket[i].Clear();
                }
            }
        }

        static public void insertionSort(int[] arr)
        {
            int tmp = 0;
            for(int i = 1; i < arr.Length; i++)
            {
                tmp = arr[i];
                for(int j = i - 1; j >= 0 && arr[j] > tmp; j--)
                {
                    arr[j + 1] = arr[j];
                    arr[j] = tmp;
                }
            }
        }

        static public ArrayList diffLength(int arrSize)
        {
            ArrayList res = new ArrayList();
            for (int i = 0; ; i++)
            {
                if (i % 2 == 0)
                {
                    res.Add(9 * Math.Pow(2, i) - 9 * Math.Pow(2, i / 2.0) + 1);
                }
                else
                {
                    res.Add(8 * Math.Pow(2, i) - 6 * Math.Pow(2, (i + 1) / 2.0) + 1);
                }
                if(3 * Convert.ToInt32(res[i]) > arrSize)
                {
                    break;
                }
            }
            res.RemoveAt(res.Count - 1);
            return res;
        }

        static public void ShellSort(int[] arr)
        {
            ArrayList diffArr = diffLength(arr.Length);
            int tmp = 0;
            for(int indexDiff = diffArr.Count - 1; indexDiff >= 0 && Convert.ToInt32(diffArr[indexDiff]) > 0; indexDiff--)
            { 
                for (int i = Convert.ToInt32(diffArr[indexDiff]); i < arr.Length; i++)
                {
                    tmp = arr[i];
                    for (int j = i - Convert.ToInt32(diffArr[indexDiff]); j >= 0 && arr[j] > tmp; j-= Convert.ToInt32(diffArr[indexDiff]))
                    {
                        arr[j + Convert.ToInt32(diffArr[indexDiff])] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }
        }

        static public void QuickSort(int[] arr, int Left = 0, int Right = -123)
        {
            if (Right == -123)
            {
                Right = arr.Length - 1;
            }
            if (Left < Right)
            {
                int last = arr[Right], tmp = 0, i = Left;
                for (int j = Left; j < Right; j++)
                {
                    if (arr[j] <= last)
                    {
                        tmp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = tmp;
                        i++;
                    }

                }
                tmp = arr[i];
                arr[i] = arr[Right];
                arr[Right] = tmp;
                QuickSort(arr, Left, i - 1);
                QuickSort(arr, i + 1, Right);
            }
        }

        static public void CountingSort(int [] arr)
        {
            int[] TmpArr = new int[ArrMax(arr) + 1];
            for (int i = 0; i < TmpArr.Length; i++)
                TmpArr[i] = 0;
            for(int i = 0; i < arr.Length; i++)
                TmpArr[arr[i]]++;
            for(int i = 0, indexArr = 0; i < TmpArr.Length; i++)
            {
                for(int j = 0; j < TmpArr[i]; j++)
                {
                    arr[indexArr] = i;
                    indexArr++;
                }
            }
        }

        static public void MergeSort(int[] arr, int Left = 0, int Right = -1)
        {
            if(Right == -1)
            {
                Right = arr.Length - 1;
            }
            if(Left >= Right)
            {
                return;
            }
            else
            {
                int mid = (Left + Right) / 2;
                MergeSort(arr, Left, mid);
                MergeSort(arr, mid + 1, Right);
                int[] TmpArr = new int[Right - Left + 1];
                for (int i = Left, j = mid + 1, indexTmp = 0; i <= mid && j <= Right && indexTmp <= Right - Left;)
                {
                    if(arr[i] > arr[j])
                    {
                        TmpArr[indexTmp] = arr[j];
                        indexTmp++;
                        j++;
                    }
                    else
                    {
                        TmpArr[indexTmp] = arr[i];
                        indexTmp++;
                        i++;
                    }
                    while(i > mid && j <= Right)
                    {
                        TmpArr[indexTmp] = arr[j];
                        j++;
                        indexTmp++;
                    }
                    while (j > Right && i <= mid)
                    {
                        TmpArr[indexTmp] = arr[i];
                        i++;
                        indexTmp++;
                    }
                }
                for (int i = Left, indexTmp = 0; i <= Right && indexTmp <= Right - Left; i++, indexTmp++)
                {
                    arr[i] = TmpArr[indexTmp];
                }
            }
        }

        static public void BubbleSort(int[] arr)
        {
            bool isSorted = true;
            int tmp = 0;
            do
            {
                isSorted = true;
                for(int i = 0; i < arr.Length - 1; i++)
                {
                    if(arr[i] > arr[i + 1])
                    {
                        tmp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = tmp;
                        isSorted = false;
                    }
                }
            } while (!isSorted);
        }

        static public void SelectionSort(int [] arr)
        {
            int indexMin = 0, tmp = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                indexMin = i;
                for(int j = i + 1; j < arr.Length; j++)
                {
                    if(arr[indexMin] > arr[j])
                    {
                        indexMin = j;
                    }
                }
                if(indexMin != i)
                {
                    tmp = arr[indexMin];
                    arr[indexMin] = arr[i];
                    arr[i] = tmp;
                }
            }
        }

        static void Main(string[] args)
        {
            Random rand = new Random();
            int size = 20;
            int[] ForSortArr = new int[size];
            int[] TmpArr = new int[size];
            System.Diagnostics.Stopwatch firstWatches = new System.Diagnostics.Stopwatch(), secondWatches = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Массив, заполненный случайным образом:");
            for (int i = 0; i < ForSortArr.Length; i++)
            {
                TmpArr[i] = ForSortArr[i] = rand.Next(0, 10000);
                Console.Write("{0}  ", ForSortArr[i]);
            }
            //firstWatches.Start();
            MergeSort(ForSortArr);
            //firstWatches.Stop();
            Console.WriteLine("\nОтсортированный: ");
            for (int i = 0; i < ForSortArr.Length; i++)
            {
                Console.Write("{0}  ", ForSortArr[i]);
            }
            //secondWatches.Start();
            //QuickSort(TmpArr);
            //secondWatches.Stop();
            ////Console.WriteLine("\nОтсортированный: ");
            ////for (int i = 0; i < TmpArr.Length; i++)
            ////{
            ////    Console.Write("{0}  ", TmpArr[i]);
            ////}
            //Console.Write("Bucket: {0} ms. Shell: {1} ms.", firstWatches.Elapsed, secondWatches.Elapsed);
            Console.ReadKey();
        }
    }
}
