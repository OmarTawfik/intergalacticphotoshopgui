#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void GrayOperationExecute(ImageData source)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j;

#pragma omp parallel for shared(src) private(i, j, p)
	for (int i = 0; i < src->Height; i++)
	{
		for (int j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			p->R = p->G = p->B = ( p->R + p->G + p->B ) / 3;
		}
	}
}            
