#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void HistogramMatchingOperationExecute(ImageData source, int* imageGray, int* otherGray, int imageSum, int otherSum)
{
	int gray1 [256];
	int gray2 [256];
	int gray [256];
	double imageRunning = 0, otherRunning =0;
	int i, j;

	for (i = 0; i < 256; i++)
	{
		imageRunning += imageGray[i];
		otherRunning += otherGray[i];

		gray1[i] = (imageRunning / imageSum) * 255;
		gray2[i] = (otherRunning / otherSum) * 255;
	}

	int finalValue, difference, currentDifference;
#pragma omp parallel for shared(gray, gray1, gray2) private(i, j, currentDifference, finalValue, difference)
	for (i = 0; i < 256; i++)
	{
		finalValue = 0;
		difference = 255;

		for (j = 0; j < 256; j++)
		{
			currentDifference = abs(gray1[i] - gray2[j]);
			if (difference > currentDifference)
			{
				difference = currentDifference;
				finalValue = j;
			}
		}

		gray[i] = finalValue;
	}

	Pixel* p;
	ImageData* src = &source;
#pragma omp parallel for shared(src, gray) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width ;j++)
		{
			p = GETPIXEL(src,j,i);

			p->R = gray[p->R];
			p->G = gray[p->G];
			p->B = gray[p->B];
		}
	}
}            
