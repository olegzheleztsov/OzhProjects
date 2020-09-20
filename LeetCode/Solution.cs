using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace LeetCode
{
    public class Solution
    {

        public int FindMaxConsecutiveOnes(int[] nums)
        {
            if((nums?.Length ?? 0) == 0)
            {
                return 0;
            }
            int maxCounter = 0;
            int curCounter = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if(nums[i] == 1)
                {
                    curCounter++;
                } else
                {
                    if(curCounter > maxCounter)
                    {
                        maxCounter = curCounter;
                        
                    }
                    curCounter = 0;
                }
            }
            if(curCounter > maxCounter)
            {
                maxCounter = curCounter;
                curCounter = 0;
            }

            return maxCounter;
        }

        public int FindNumbers(int[] nums)
        {
            if((nums?.Length ?? 0) == 0)
            {
                return 0;
            }
            int countOfEven = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if(GetNumberCount(nums[i]) % 2 == 0)
                {
                    countOfEven++;
                }
            }
            return countOfEven;
        }

        private int GetNumberCount(int val )
        {
            int numbers = 0;
            do
            {
                numbers++;
                val /= 10;

            } while (val != 0);
            return numbers;
        }

        public int[] SortedSquares(int[] A)
        {
            if((A?.Length ?? 0) == 0)
            {
                return new int[] { };
            } 

            if(A.Length == 1)
            {
                return new int[] { A[0] * A[0] };
            }

            int edgeIndex = -1;
            for(int i = 0; i < A.Length; i++ )
            {
                if(A[i] >= 0)
                {
                    edgeIndex = i;
                    break;
                }
            }
            int leftIndex = edgeIndex - 1;
            int rightIndex = edgeIndex;
            int newIndex = 0;
            int[] newA = new int[A.Length];
            while(leftIndex >= 0 && rightIndex < A.Length)
            {
                if(Math.Abs(A[leftIndex]) <= A[rightIndex])
                {
                    newA[newIndex++] = A[leftIndex] * A[leftIndex];
                    leftIndex--;
                } else
                {
                    newA[newIndex++] = A[rightIndex] * A[rightIndex];
                    rightIndex++;
                }
            }

            if(leftIndex > -1)
            {
                for(int i = leftIndex; i > -1; i--)
                {
                    newA[newIndex++] = A[i] * A[i];
                }
            }
            if(rightIndex < A.Length)
            {
                for(int i = rightIndex; i < A.Length; i++ )
                {
                    newA[newIndex++] = A[i] * A[i];
                }
            }
            return newA;
        }

        public void DuplicateZeros(int[] arr)
        {
            for(int cursor = arr.Length - 1; cursor >= 0; cursor--)
            {
                if(arr[cursor] == 0)
                {
                    DuplicateAt(cursor, arr);
                }
            }
        }

        private void DuplicateAt(int index, int[] arr)
        {
            for(int cur = arr.Length - 1; cur > index; cur--)
            {
                arr[cur] = arr[cur - 1];
            }
        }

        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int[] result = new int[m + n];
            int i1 = 0, i2 = 0, cursor = 0;
            while(i1< m && i2 < n)
            {
                if(nums1[i1] <= nums2[i2])
                {
                    result[cursor++] = nums1[i1];
                    i1++;
                } else
                {
                    result[cursor++] = nums2[i2];
                    i2++;
                }
            }
            if(i1 < m)
            {
                for(; i1 < m; i1++)
                {
                    result[cursor++] = nums1[i1];
                }
            }
            if(i2 < n)
            {
                for(; i2 < n; i2++)
                {
                    result[cursor++] = nums2[i2];
                }
            }
            Array.Copy(result, nums1, n + m);
        }

        public int RemoveElement(int[] nums, int val)
        {
            if((nums?.Length ?? 0) == 0)
            {
                return 0;
            }
            if(nums.Length == 1)
            {
                if(nums[0] == val)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            int resultLen = nums.Length;
            int endCursor = nums.Length - 1;
            int cursor = 0;
            while (endCursor >= 0 && nums[endCursor] == val)
            {
                endCursor--;
                resultLen--;
            }
            if(endCursor < 0 )
            {
                return 0;
            }
            
            for(; cursor <= endCursor; )
            {
                if(nums[cursor] == val)
                {
                    nums[cursor] = nums[endCursor];
                    nums[endCursor] = val;
                    endCursor--;
                    resultLen--;
                } else
                {
                    cursor++;
                }
            }
            return resultLen;
        }

        public int RemoveDuplicates(int[] nums)
        {
            const int SENTINEL = int.MinValue;
            int currentIndex = 0, lookIndex = 1;
            int len = nums.Length;
            int removed = 0;

            for(; lookIndex < len; )
            {
                while(nums[lookIndex] == nums[currentIndex])
                {
                    nums[lookIndex] = SENTINEL;
                    removed++;
                    lookIndex++;
                    if(lookIndex >= len)
                    {
                        break;
                    }
                }
                currentIndex = lookIndex;
                lookIndex++;
            }

            currentIndex = 0;
            for(int i = 0; i < len; i++ )
            {
                if(nums[i] != SENTINEL)
                {
                    nums[currentIndex] = nums[i];
                    currentIndex++;
                }
            }
            return len - removed;
        }

        public bool CheckIfExist(int[] arr)
        {
            Dictionary<int, bool> existance = new Dictionary<int, bool>();
            for(int i = 0; i < arr.Length; i++ )
            {
                if(existance.ContainsKey(arr[i] / 2) && arr[i] % 2 == 0)
                {
                    return true;
                } else if(existance.ContainsKey(arr[i] * 2))
                {
                    return true;
                }
                existance[arr[i]] = true;
            }
            return false;
        }

        public bool ValidMountainArray(int[] A)
        {
            int endEdge = A.Length - 1;
            int startIndex = 0, endIndex = endEdge;
            bool startIsGoing = true, endIsGoing = true;

            if (A == null || A.Length < 3)
            {
                return false;
            }

            if(A.Length == 3)
            {
                return A[1] > A[0] && A[1] > A[2];
            }
            while(startIndex <= endIndex && (startIsGoing || endIsGoing))
            {
                if(A[startIndex + 1] <= A[startIndex])
                {
                    startIsGoing = false;
                    if(startIndex == 0)
                    {
                        break;
                    }
                } else
                {
                    startIndex++;
                }
                if(A[endIndex - 1] <= A[endIndex])
                {
                    endIsGoing = false;
                    if(endIndex == endEdge)
                    {
                        break;
                    }
                } else
                {
                    endIndex--;
                }
            }

            return startIndex == endIndex && startIndex != 0 && startIndex != A.Length-1;
        }

        public int[] ReplaceElements(int[] arr)
        {
            int replaceIndex = 0;
            int maxIndex = -1;
            int curMax = 0;

            if(arr.Length == 1)
            {
                arr[0] = -1;
                return arr;
            }

            if(arr.Length == 2)
            {
                arr[0] = arr[1];
                arr[1] = -1;
                return arr;
            }
            if(arr.Length == 3)
            {
                arr[0] = Math.Max(arr[1], arr[2]);
                arr[1] = arr[2];
                arr[2] = -1;
                return arr;
            }

            while(replaceIndex < arr.Length)
            {
                for(int i = arr.Length-1; i > replaceIndex; i--)
                {
                    if(arr[i] > curMax )
                    {
                        curMax = arr[i];
                        maxIndex = i;
                    }
                }
                for(int k = replaceIndex; k < maxIndex; k++)
                {
                    arr[k] = curMax;
                }
                replaceIndex = maxIndex;
                curMax = 0;
                if(replaceIndex == arr.Length - 1)
                {
                    arr[replaceIndex] = -1;
                    break;
                }
            }
            return arr;
        }
    }
}
