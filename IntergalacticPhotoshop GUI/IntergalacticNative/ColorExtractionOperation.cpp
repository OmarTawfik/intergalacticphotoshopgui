#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void ColorExtractionOperationExecute(ImageData source, bool keepRed, bool keepGreen, bool keepBlue)
{
	ImageData* src = &source;
	Pixel* p;
	int i, j;

#pragma omp parallel for shared(src, keepRed, keepGreen, keepBlue) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			p->R = (keepRed) ? p->R : 0;
			p->G = (keepGreen) ? p->G : 0;
			p->B = (keepBlue) ? p->B : 0;
		}
	}
}            
