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
    }
}