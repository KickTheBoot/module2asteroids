using System;

namespace TeamY.Utils
{
    //Static utility class for converting types to bytes and vice versa, called Pirate because it's an 'arr'ay utility class
    public static class Pirate
    {

        //Convert an array of 32 bit integers into an array of bytes 
        public static byte[] intArrToByteArr(int[] input)
        {
            byte[] output = new byte[input.Length*sizeof(int)];

            for(int i = 0; i < input.Length; i++)
            {
                byte[] current = new byte[sizeof(int)];
                current = BitConverter.GetBytes(input[i]);
                for(int j = 0; j < sizeof(int);j++)
                {
                    output[i * sizeof(int) + j] = current[j];
                }
            }
            return output;
        }

        //Convert an array of bytes into an array of 32 bit integers
        public static int[] byteArrToInt32arr(byte[] input)
        {
            int[] output = new int[input.Length/sizeof(int)];

            for(int i = 0; i < output.Length; i++)
            {
                byte[] current = new byte[sizeof(int)];
                for(int j = 0; j < sizeof(int); j++)
                {
                    current[j]= input[i*sizeof(int)+j];
                }
                output[i] = BitConverter.ToInt32(current);
            }

            return output;
        }

        //Descending insertion sort
        public static int[] InsertionSortDesc(int[] input)
        {
            int[] output = input;

            for (int i = 0; i < output.Length; i++)
        {
            int j = i;
            while (j > 0 && output[j - 1] < output[j])
            {
                //Swap the values
                int k = output[j - 1];
                output[j - 1] = output[j];
                output[j] = k;

                j--;
            }
        }

            return output;
        }

        //Ascending insertion sort
        public static int[] InsertionSortAsc(int[] input)
        {
            int[] output = input;

            for (int i = 0; i < output.Length; i++)
        {
            int j = i;
            while (j > 0 && output[j - 1] > output[j])
            {
                //Swap the values
                int k = output[j - 1];
                output[j - 1] = output[j];
                output[j] = k;

                j--;
            }
        }

            return output;
        }

        //returns the largest value in array
        public static int getLargest(int[] input)
        {
            int output = int.MinValue;

            for(int i = 0; i<input.Length; i++)
            {
                if(input[i] > output) output = input[i];
            }
            return output;

        }

        //returns the smallest value in the array
        public static int getSmallest(int[] input)
        {
            int output = int.MaxValue;

            for(int i = 0; i<input.Length; i++)
            {
                if(input[i] < output) output = input[i];
            }
            return output;
        }

        //returns the index of the largest value
        public static int getLargestIndex(int[] input)
        {
            int output = 0;
            for(int i = 0; i<input.Length; i++)
            {
                if(input[i] > output) output = i;
            }
            return output;
        }

        //returns the index of the smallest value
        public static int getSmallestIndex(int[] input)
        {
            int output = 0;
            for(int i = 0; i<input.Length; i++)
            {
                if(input[i] < output) output = i;
            }
            return output;

        }

        
    }
}