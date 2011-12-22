#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void PixelationOperationExecute(ImageData source, int pixelSize)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j, pixelCount = src->Height * src->Width;

#pragma omp parallel for shared(src, pixelSize, pixelCount) private(i, j, p)
	for (int i = 0; i < src->Height; i += pixelSize)
	{
		for (int j = 0; j < src->Width; j += pixelSize)
		{
			int totalRed = 0, totalGreen = 0, totalBlue = 0;
			for (int a = i; a < i + pixelSize && a < src->Height; a++)
			{
				for (int b = j; b < j + pixelSize && b < src->Width; b++)
				{
					p = GETPIXEL(src,j,i);
					totalRed += p->R;
					totalGreen += p->G;
					totalBlue += p->B;
				}
			}

			totalRed /= pixelCount;
			totalGreen /= pixelCount;
			totalBlue /= pixelCount;
			
			for (int a = i; a < i + pixelSize && a < src->Height; a++)
			{
				for (int b = j; b < j + pixelSize && b < src->Width; b++)
				{
					p = GETPIXEL(src,j,i);
					SETPIXELRGB(p, totalRed, totalGreen, totalBlue);
				}
			}
		}
	}
}            
