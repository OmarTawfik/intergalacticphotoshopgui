#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void InverseOperationExecute(ImageData source)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j;

#pragma omp parallel for shared(src) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			p->R = 255 - p->R;
			p->G = 255 - p->G;
			p->B = 255 - p->B;
		}
	}
}            
