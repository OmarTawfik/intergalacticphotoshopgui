#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void GammaAdjustmentOperationExecute(ImageData source, double gamma)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j;

#pragma omp parallel for shared(src, gamma) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			p->R = pow(p->R / 255.0, gamma) * 255;
			p->G = pow(p->G / 255.0, gamma) * 255;
			p->B = pow(p->B / 255.0, gamma) * 255;
		}
	}
}            
