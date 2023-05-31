using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class AlphaTrimFilter
    {


        public static int GetTimeofAlpha(int t1, int t2)
        {
            int TimeAlpha = t2 - t1;
            return TimeAlpha;
        }
        public static byte[] NeighboursPixels(byte[,] image, int[] pixel, int windowSize, int height, int width) 
        {
            int PixelX = pixel[0];
            int PixelY = pixel[1];
            byte[] windowVal = new byte[windowSize * windowSize];
            int dimensions = (windowSize / 2);
            int index = 0;
            for (int i = PixelX - dimensions; i <= PixelX + dimensions; i++)
            {
                for (int j = PixelY - dimensions; j <= PixelY + dimensions; j++)
                {

                    if (i < 0 || j < 0 || i >= height || j >= width)
                    {
                        windowVal[index] = 0;
                        index++;
                    }
                    else
                    {
                        windowVal[index] = image[i, j];
                        index++;
                    }
                }
            }

            return windowVal;

        }
        public static byte Getaverage(byte[] Window, int trim, int size, int usedAlgo)
        {
            if ((trim * 2) > size)
            {
                trim = size;
            }
            
            int sum = 0;
            int count = 0;
            if (usedAlgo == 0)
            {
                for (int i = trim; i < Window.Length - trim; ++i)
                {
                    sum += Window[i];
                    ++count;
                }
            }
            else if (usedAlgo == 1)
            {
                for (int i = 0; i < Window.Length; ++i)
                {
                    sum += Window[i];
                    ++count;
                }
            }
            byte avg = Convert.ToByte(sum / count);
            return avg;
        }


        public static Byte[,] ApplyFilter(Byte[,] ImageMatrix, int MaxWindowSize, int UsedAlgorithm, int TrimValue)
        {
            //TODO: Implement alpha trim filter

            //Exception Handling
            if (MaxWindowSize == 1)
            {
                MaxWindowSize = 3;
            }
            int height = ImageOperations.GetHeight(ImageMatrix);
            int Width = ImageOperations.GetWidth(ImageMatrix);

            int[] pixel = new int[2];
            Byte[] PixelWindow = new Byte[MaxWindowSize * MaxWindowSize];
            Byte[] KthWindow = new Byte[] { };
            Byte[,] NewMatrix = new Byte[height, Width];

            for (int i = 0; i < height; i++)
            {
                pixel[0] = i;
                for (int j = 0; j < Width; j++)
                {
                    pixel[1] = j;
                    //Store the values of the neighboring pixels in an array
                    PixelWindow = NeighboursPixels(ImageMatrix, pixel, MaxWindowSize, height, Width);

                    
                    if (UsedAlgorithm == 0)
                    {
                        //Sort the values in the window in ascending order (Counting Sort)
                        PixelWindow = SortHelper.CountingSort(PixelWindow);
                        //Calculate the average of the remaining values as the new pixel value 
                        byte average = Getaverage(PixelWindow, TrimValue, MaxWindowSize, UsedAlgorithm);
                        //Place the new value in the center of the window in the new matrix
                        NewMatrix[i, j] = average;
                    }
                    else if (UsedAlgorithm == 1)
                    {
                        KthWindow = SortHelper.Kth_element(PixelWindow, TrimValue, MaxWindowSize);
                        //Calculate the average of the remaining values as the new pixel value 
                        byte average = Getaverage(KthWindow, TrimValue, MaxWindowSize, UsedAlgorithm);
                        //Place the new value in the center of the window in the new matrix
                        NewMatrix[i, j] = average;


                    }
                }
            }


            return NewMatrix;
            // Remove the next line
            //throw new NotImplementedException();
        }



    }

}