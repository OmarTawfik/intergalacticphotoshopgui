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
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p1 = GETPIXEL(src,j,i);
			p2 = GETPIXEL(other,j,i);
			CUTOFF(p1, abs(p1->R - p2->R), abs(p1->G - p2->G), abs(p1->B - p2->B));
		}
	}
}
