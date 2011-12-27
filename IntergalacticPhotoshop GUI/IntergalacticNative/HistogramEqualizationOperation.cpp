#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void HistogramEqualizationOperationExecute(ImageData source, int* imageRed, int* imageGreen, int* imageBlue)
{
	int red[256];
	int green[256];
	int blue[256];
	double imageRunningR = 0;
	double imageRunningG = 0;
	double imageRunningB = 0;
	int imageSum = source.Height * source.Width;
	int i, j;

	for (i = 0; i < 256; i++)
	{
		imageRunningR += imageRed[i];
		imageRunningG += imageGreen[i];
		imageRunningB += imageBlue[i];
		red[i] = (imageRunningR / imageSum) * 255;
		green[i] = (imageRunningG / imageSum) * 255;
		blue[i] = (imageRunningB / imageSum) * 255;
	}
	
	Pixel* p;
#pragma omp parallel for shared(source, red, green, blue) private(i, j, p)
	for (i = 0; i < source.Height; i++)
	{
		for (j = 0; j < source.Width ;j++)
		{
			p = GETPIXEL(&source,j,i);

			p->R = red[p->R];
			p->G = green[p->G];
			p->B = blue[p->B];
		}
	}
}            
