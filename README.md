                           An image processing project in which we implemented image filters using different sorting algorithms.   
FIRST: Alpha-trim filter
--------------------------------
The idea is to calculate the average of some neighboring pixels' values after trimming out (excluding) the smallest T pixels and largest T pixels. This can be done by repeating the following steps for each pixel in the image:   
1.	Store the values of the neighboring pixels in an array. The array is called the window, and it should be odd-sized.
2.	Sort the values in the window in ascending order.
3.	Exclude the first T values (smallest) and the last T values (largest) from the array.
4.	Calculate the average of the remaining values as the new pixel value and place it in the center of the window in the new image, see Figure 1.
This filter is usually used to remove both salt & pepper noise and random noise. See figure 2

**_Notes_** :

We work on gray-level images. So, each pixel has a value ranging from 0 to 255. Where 0 is the black pixel and 255 is the white pixel

**_The Sorting Algorithms Implemented:_**
1.	Counting Sort
2.	Selecting Kth smallest element in the array without sorting it (Textbook sec. 9.2). where K = T to exclude the smallest T values, then on the remaining array, apply the algorithm again to exclude the largest T values. Finally, calculate the average of the remaining values

ㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤFigure 2: Effect of the alpha-trim filter with window size = 5x5 and trim value T = 5 
![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/32980637-4cda-4291-9ea4-93297ec0dd44)

Image with uniform and salt & pepper noises


![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/483df510-b642-4e06-892e-e94c67775f2a)

Alpha-trim filter 5x5 and T = 5                             



ㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤFigure 3: Effect of the standard median filter with different window size
![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/e67c61ae-cd2e-4fe6-ad8c-896f0e78e858)![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/9640f421-aab8-49af-bee3-55d0d08345e2)
ㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤImage with salt & pepper noiseㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤMedian filter with window 3×3

![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/8d08343c-3dc0-4414-968e-becc739d6f5f)![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/3394e7da-9fcf-4382-8885-ce8e3b0bd6ab)

ㅤㅤㅤMedian filter with window 5×5ㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤMedian filter with window 7x7




SECOND: Adaptive Median Filter
--------------------------------------------
-The idea of the standard median filter is similar to the alpha-trim filter but instead, we calculate the median of neighboring pixels' values (middle value in the window array after sorting). 

-It's usually used to remove the salt and pepper noise, see Figure 3.
However, the standard median filter has the following drawbacks:
1.	It fails to remove salt and pepper noise with a large percentage (greater than 20%) without causing distortion in the original image.
2.	It usually has a side effect on the original image especially when it’s applied with a large mask size, see Figure 2 with window 7×7.

-The adaptive median filter is designed to handle these drawbacks by:
1.	Seeking a median value that’s not either salt or pepper noise by increasing the window size until reaching such median.
2.	Replace the noise pixels only. (i.e. if the pixel is not a salt or a pepper, then leave it).
This is clear in Figure 4.

Figure 4: Effect of adaptive vs. standard median filter on small percentage of salt and pepper noise
![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/b7d84917-74b8-490d-b937-f7ea51d420b7)
ㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤOriginal image corrupted with salt and pepper noise with percentage ≈ 30%

![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/1d52e905-25fe-42e2-aba7-45109c96874c)![image](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/9db8ac7c-31eb-435d-b126-1348a544792c)

Standard median filter with window 7×7 ㅤㅤㅤㅤㅤㅤㅤㅤㅤㅤAdaptive median with max window 7×7

_**The Sorting Algorithm Implemented:**_
1.	Quick Sort
2.	Counting Sort

_**Implementation steps of Adaptive Median Filter on Image:**_

The adaptive median filter has a variable window size WS, and the procedure for updating the pixel value is as follows:
For each pixel in the image:
Try window sizes ranging from 3×3 to WS × WS, where WS is the maximum window size entered by the user, as follows:
Step 0: Start with window size 3×3
Step 1: Chose a non-noise median value
Sort the current window, and denote the following:
1.	Zxy is the gray value of the current pixel value at location (x, y)
2.	Zmax is the maximum gray value in the window.
3.	Zmin is the minimum gray value in the window.
4.	Zmed is the median gray value in the window.
A1 = Zmed – Zmin
A2 = Zmax – Zmed
If A1 > 0 and A2 > 0 then we found a non-noise median
              Go to Step 2
Else
              Increase window size by 2
              If the new window size ≤ WS then
                             Repeat Step 1 again
              Else
                             NewPixelVal = Zmed                       
Step 2: Replace the center with the median value, or leave it
B1 = Zxy – Zmin
B2 = Zmax – Zxy
If B1 > 0 and B2 > 0 then
              NewPixelVal = Zxy //leave the center pixel as it is
Else
              NewPixelVal = Zmed //replace the center pixel with the median value

Step 3: repeat the process for the next pixel starting from step 0 again


The steps above summarize what’s done through adaptive median filter implementation. The meaning of these steps is as follows:
Step 1: Search for a true median
IF the current window has a true median (i.e. Zmed is different from Zmin and Zmax) THEN  
              //Execute Step 2 
ELSE 
               IF the current window size is not the maximum 
                               Increase it and repeat Step 1
               ELSE
                             Let the output pixel be Zmed and move to the next pixel.
                ENDIF
EndIF
Step 2: if we have a true median
IF (Zxy is different from Zmin and Zmax) (i.e. not noise)
THEN      
              Let the output pixel be Zxy (i.e. not changed) and move to the next pixel.
ELSE
               Let the output pixel be Zmed and move to the next pixel.


Functions Description:
---------------------
->SortHelper.cs:
• Quick Sort: we did a function called “Partitioning” that returns the index at which the array should be divided. In which all the values before the index should be smaller than the value of the index and all the values after the index should be larger than the value of the index. In the “partitioning” function, we pass to it the first and last index of the array that needs to be sorted and the array itself. After the partitioning, the index should be sent to the quick sort function so that it sorts the two arrays by calling itself two times in its body (recursion). 

• Counting Sort: To implement the counting sort idea, we made an array of size 256 to make an index for every value so that we can store how many times the value appeared in the unsorted array. Then, we counted how many times the values had appeared in the unsorted array and stored that in the counter array. Then, we sorted the array by going after each index in the counter array and placing the values depending on how much they occurred in the sorted array.

• Kth smallest/largest element: To implement Kth smallest/largest element, we decided to place the values of the unsorted array into a list so that we can remove more flexibly in case the smallest/largest element was in the middle or the end. Then, we determined the largest and smallest element each time depending on the trim value and removed them. Finally, we converted the sorted list into an array so that we can return the sorted array for further operations.

->AlphaTrimFilter.cs : 
• To implement this filter, we looped on the height and the width of the given image to get the neighboring pixels of each pixel in order to get the average of them and replace the new non-noise pixel. After getting the neighboring pixels whether they were inside or outside (by placing them as zeros) the range of the image, we sort the neighboring pixels array. If the selected algorithm was the “Counting sort” we trim the array by the given trim value and then get the average of the remaining values and place it in the new image array. If the selected algorithm was “Kth smallest/largest element”, we only get the average (since it’s already trimmed by the given trim value in the algorithm function itself) and place the new pixel in the new image array. 

->AdaptiveMedianFilter.cs : 
• To implement this filter, we looped on the height and the width of the given image to get the neighboring pixels of each pixel in order to get the average of them and replace the new non-noise pixel. After getting the neighboring pixels whether they were inside or outside (by placing them as zeros) the range of the image, we sort the neighboring pixels array. Then we sort the array either the “Counting sort ” or the “Quick sort”. Then, we loop on the given window size starting from size 3. After that, we get the minimum, maximum, and median from the sorted array and the gray value of the current pixel value at location (i, j). Step 1: We calculate the A1, A2 by: A1 = median – min A2 = max – median If A1 > 0 and A2 > 0 then we found a non-noise median. Then, we calculate B1 and B2: B1 = the current pixel at (i,j) – min B2 = max – the current pixel at (i,j) If B1 > 0 and B2 > 0 then NewPixelVal = the current pixel at (i,j) //leave the center pixel as it is Else NewPixelVal = median //replace the center pixel with the median value If A1 or A2 is not larger than 0, then Increase window size by 2 If new window size ≤ max window size then Repeat Step 1 again Else NewPixelVal = median


Results:
---
1-Alpha Trim Filter

![alpha1](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/4680d23b-70d9-482a-a0c5-11d092af8bbb)
![alpha2](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/d2098d3f-0c67-4abe-88cd-5ca5e86ddf07)
![alpha3](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/8c4553e7-d7b1-46c7-8986-4ad38ca64e80)


As you increase the wmax in Alpha Filter, the picture gets blurred

![bluralpha](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/e928a647-842b-4646-a554-2ce744827515)


2-Adaptive Median Filter

![adap1](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/5317661d-e162-48a4-8d05-d5beea16b641)
![adap2](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/45e2fe07-8a2a-477b-b1b0-e9020a93d2c8)
![adap3](https://github.com/MinnaMohammed/Image-Filters/assets/83884890/600c4550-8ca3-429f-ad55-d1adc11940b7)



