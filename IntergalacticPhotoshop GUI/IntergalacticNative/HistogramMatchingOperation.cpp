#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void HistogramMatchingOperationExecute(ImageData source, int* imageRed, int* imageGreen, int* imageBlue, int* otherRed, int* otherGreen, int* otherBlue, int imageSum, int otherSum)
{
	int Red1 [256];
	int Green1 [256];
	int Blue1 [256];

	int Red2 [256];
	int Green2 [256];
	int Blue2 [256];
	
	int Red [256];
	int Green [256];
	int Blue [256];

	double imageRunningR = 0;
	double imageRunningG = 0;
	double imageRunningB = 0;
	double otherRunningR = 0;
	double otherRunningG = 0;
	double otherRunningB = 0;
	int i, j;

	for (i = 0; i < 256; i++)
	{
		imageRunningR += imageRed[i];
		imageRunningG += imageGreen[i];
		imageRunningB += imageBlue[i];

		otherRunningR += otherRed[i];
		otherRunningG += otherGreen[i];
		otherRunningB += otherBlue[i];
		
		Red1[i] = (imageRunningR / imageSum) * 255;
		Green1[i] = (imageRunningG / imageSum) * 255;
		Blue1[i] = (imageRunningB / imageSum) * 255;
		
		Red2[i] = (otherRunningR / otherSum) * 255;
		Green2[i] = (otherRunningG / otherSum) * 255;
		Blue2[i] = (otherRunningB / otherSum) * 255;
	}
	
	int finalValueR, differenceR, currentDifferenceR;
	int finalValueG, differenceG, currentDifferenceG;
	int finalValueB, differenceB, currentDifferenceB;
#pragma omp parallel for shared(Red, Red1, Red2, Green, Green1, Green2, Blue, Blue1, Blue2) private(i, j, currentDifferenceR, finalValueR, differenceR, finalValueG, differenceG, currentDifferenceG, finalValueB, differenceB, currentDifferenceB)
	for (i = 0; i < 256; i++)
	{
		finalValueR = 0;
		finalValueG = 0;
		finalValueB = 0;
		
		differenceR = 255;
		differenceG = 255;
		differenceB = 255;

		for (j = 0; j < 256; j++)
		{
			currentDifferenceR = abs(Red1[i] - Red2[j]);
			currentDifferenceG = abs(Green1[i] - Green2[j]);
			currentDifferenceB = abs(Blue1[i] - Blue2[j]);

			if (differenceR > currentDifferenceR)
			{
				differenceR = currentDifferenceR;
				finalValueR = j;
			}

			if (differenceG > currentDifferenceG)
			{
				differenceG = currentDifferenceG;
				finalValueG = j;
			}

			if (differenceB > currentDifferenceB)
			{
				differenceB = currentDifferenceB;
				finalValueB = j;
			}
		}
		
		Red[i] = finalValueR;
		Green[i] = finalValueG;
		Blue[i] = finalValueB;
	}

	Pixel* p;
	ImageData* src = &source;
#pragma omp parallel for shared(src, Red, Green, Blue) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width ;j++)
		{
			p = GETPIXEL(src,j,i);

			p->R = Red[p->R];
			p->G = Green[p->G];
			p->B = Blue[p->B];
		}
	}
}            
