#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void ContrastOperationExecute(ImageData source, int oldMin, int oldMax, int newMin, int newMax)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j, r, g, b;
	float oldMultiplier = (oldMax - oldMin) / 255.0f;
	float newMultiplier = (newMax - newMin) / 255.0f;

#pragma omp parallel for shared(src, oldMultiplier, newMultiplier) private(p, i, j, r, g, b)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);

			r = p->R - oldMin;
			g = p->G - oldMin;
			b = p->B - oldMin;

			r = (int)(((r / oldMultiplier) * newMultiplier) + newMin);
			g = (int)(((g / oldMultiplier) * newMultiplier) + newMin);
			b = (int)(((b / oldMultiplier) * newMultiplier) + newMin);

			CUTOFF(p, r, g, b);
		}
	}
}            
