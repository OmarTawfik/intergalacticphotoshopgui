#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void BinarizationOperationExecute(ImageData source)
{
	ImageData* src = &source;
	int totalGray = 0, i, j;
	Pixel* p;

	#pragma omp parallel for shared(src, totalGray) private(i, j, p)
	for (int i = 0; i < src->Height; i++)
	{
		for (int j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			totalGray += (p->R + p->G + p->B) / 3;
		}
	}

	int threshold = totalGray / (src->Width * src->Height), gray;

	#pragma omp parallel for shared(src, threshold) private(i, j, p, gray)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			gray = (p->R+ p->G + p->B) / 3;

			if (gray < threshold)
			{
				p->R = p->G = p->B = 0;
			}
			else if (gray >= threshold)
			{
				p->R = p->G = p->B = 255;
			}
		}
	}
}            
