#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void HistogramCalculatorExecute(ImageData source, int* red, int* green, int* blue, int* gray)
{
	ImageData* src = &source;

	Pixel* p;
	int i, j;

#pragma omp parallel for shared(src, red, green, blue, gray) private(i, j, p)
	for (int i = 0; i < src->Height; i++)
	{
		for (int j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);

			red[p->R]++;
			green[p->G]++;
			blue[p->B]++;

			gray[(p->R+p->G+p->B)/3]++;
		}
	}
}            
