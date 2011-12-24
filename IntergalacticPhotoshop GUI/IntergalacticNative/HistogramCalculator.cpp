#include "ImageData.h"
#include <iostream>
#include <math.h>
using namespace std;

extern "C" DllExport void HistogramCalculatorExecute(ImageData source, int* red, int* green, int* blue, int* gray)
{
	ImageData* src = &source;

	Pixel* p;
	int i, j;

	for (int i = 0; i < 256; i++)
    {
        red[i] = 0;
        green[i] = 0;
        blue[i] = 0;
        gray[i] = 0;
    }

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
