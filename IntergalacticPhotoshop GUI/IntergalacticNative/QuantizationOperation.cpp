#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void QuantizationOperationExecute(ImageData source, int bitsPerChannel)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j, mask = ~((1 << (8 - bitsPerChannel)) - 1);

#pragma omp parallel for shared(src, mask) private(i, j, p)
	for (int i = 0; i < src->Height; i ++)
	{
		for (int j = 0; j < src->Width; j ++)
		{
			p = GETPIXEL(src,j,i);
			p->R &= mask;
			p->G &= mask;
			p->B &= mask;
		}
	}
}            
