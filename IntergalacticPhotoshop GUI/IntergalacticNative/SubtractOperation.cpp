#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void SubtractOperationExecute(ImageData source, ImageData otherImage)
{
	ImageData* src = &source;
	ImageData* other = &otherImage;

	Pixel* p1;
	Pixel* p2;
	int i, j;

#pragma omp parallel for shared(src, other) private(i, j, p1, p2)
	for (int i = 0; i < src->Height; i++)
	{
		for (int j = 0; j < src->Width; j++)
		{
			p1 = GETPIXEL(src,j,i);
			p2 = GETPIXEL(other,j,i);
			CUTOFF(p1, p1->R - p2->R, p1->G - p2->G, p1->B - p2->B);
		}
	}
}            
