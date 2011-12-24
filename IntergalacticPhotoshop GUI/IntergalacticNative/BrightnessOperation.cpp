#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void BrightnessOperationExecute(ImageData source, int brightness)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j;

#pragma omp parallel for shared(src, brightness) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			CUTOFF(p, p->R + brightness, p->G + brightness, p->B + brightness);
		}
	}
}            
