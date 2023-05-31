using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class AdaptiveMedianFilter
    {


        public static int GetTimeofAdapMed(int starttime, int endtime)
        {
            //Total time
            int TimeMed = endtime - starttime;
            return TimeMed;
        }
        public static Byte[,] ApplyFilter(Byte[,] ImageMatrix, int MaxWindowSize, int UsedAlgorithm)
        {
            //TODO: Implement adaptive median filter
         
  
            //Exception Handling
            if (MaxWindowSize == 1)
            {
                MaxWindowSize = 3;
            }

            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);
            int initial_window;
            int[] pixel = new int[2];
            Byte[,] NewMatrix = new Byte[height, width];
            for (int i = 0; i < height; i++)
            {
                pixel[0] = i;
                for (int j = 0; j < width; j++)
                {

                   //Starting by window size 3×3
                    initial_window = 3;
                    Byte[] pixelWindow = new Byte[initial_window * initial_window];
                    pixel[1] = j;
                    int min = 0, max = 0, median = 0;
                    int A1 = 0, A2 = 0;
                    int B1 = 0, B2 = 0;
                    bool replaced = false;

                    //Store the values of the neighboring pixels in an array
                    pixelWindow = AlphaTrimFilter.NeighboursPixels(ImageMatrix, pixel, initial_window, height, width);

                    //Sort the values in the window in ascending order (Counting Sort or Quick Sort)
                    if (UsedAlgorithm == 0)  
                    {
                        SortHelper.QuickSort(0, pixelWindow.Length, ref pixelWindow);
                    }
                    else if (UsedAlgorithm == 1)
                    {
                        pixelWindow = SortHelper.CountingSort(pixelWindow);
                    }
                  
                    while (initial_window <= MaxWindowSize)
                    {

                        min = pixelWindow[0];
                        max = pixelWindow[pixelWindow.Length - 1];
                        median = pixelWindow[(pixelWindow.Length / 2) + 1];
                        A1 = median - min;
                        A2 = max - median;
                        //Choosing a non-noise median value (true median)
                        if (A1 > 0 && A2 > 0)   
                        {
                            B1 = ImageMatrix[i, j] - min;
                            B2 = max - ImageMatrix[i, j];
                            if (B1 > 0 && B2 > 0) 
                            {
                                //The pixel value is unchanged
                                NewMatrix[i, j] = ImageMatrix[i, j];
                                replaced = true;
                                break;
                            }
                            else   
                            {
                                NewMatrix[i, j] = (Byte)median;
                                replaced = true;
                                break;
                            }

                        }
                        else  
                        {
                            initial_window += 2;
                            pixelWindow = AlphaTrimFilter.NeighboursPixels(ImageMatrix, pixel, initial_window, height, width);

                        }

                    }
                    if (!replaced)
                    {
                        NewMatrix[i, j] =(Byte) median;
                    }




                }
            }

            return NewMatrix;

            // Remove the next line
            //throw new NotImplementedException();
        }
    }
}
