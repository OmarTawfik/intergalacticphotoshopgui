#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void HorizontalFlipOperationExecute(ImageData source)
{
	ImageData* src = &source;

	int i, j;
	Pixel *leftPixel, *rightPixel, tmp;

	#pragma omp parallel for shared(src) private(i, j, leftPixel, rightPixel, tmp) 
	for (i = 0; i < src->Height; i++)
    {
        for (j = 0; j < src->Width / 2; j++)
        {
            leftPixel = GETPIXEL(src, j, i);
            rightPixel = GETPIXEL(src, src->Width - j - 1, i);
			
			tmp.R = leftPixel->R;
			tmp.G = leftPixel->G;
			tmp.B = leftPixel->B;

			SETPIXEL(leftPixel, rightPixel);
			SETPIXEL(rightPixel, &tmp);
        }
    }
}            

