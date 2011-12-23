#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void AddOperationExecute(ImageData source, ImageData otherImage, double factor)
{
	ImageData* src = &source;
	ImageData* other = &otherImage;

	Pixel* p1;
	Pixel* p2;
	int i, j;

#pragma omp parallel for shared(src, other, factor) private(i, j, p1, p2)
	for (int i = 0; i < src->Height; i++)
	{
		for (int j = 0; j < src->Width; j++)
		{
			p1 = GETPIXEL(src,j,i);
			p2 = GETPIXEL(other,j,i);
			
			p1->R = (p1->R * factor) + (p2->R * (1.0 - factor));
			p1->G = (p1->G * factor) + (p2->G * (1.0 - factor));
			p1->B = (p1->B * factor) + (p2->B * (1.0 - factor));
		}
	}
}            
