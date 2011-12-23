#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void HistogramEqualizationOperationExecute(ImageData source, int* imageGray)
{
	int gray [256];
	double imageRunning = 0;
	int imageSum = source.Height * source.Width;
	int i, j;

#pragma omp parallel for shared(imageGray, gray, imageRunning, imageSum) private(i)
	for (i = 0; i < 256; i++)
	{
		imageRunning += imageGray[i];
		gray[i] = (imageRunning / imageSum) * 255;
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
