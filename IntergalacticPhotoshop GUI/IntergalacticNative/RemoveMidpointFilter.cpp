#include "ImageData.h"
#include <iostream>
#include <math.h>
#include "NoiseHelperFunctions.h"
using namespace std;

extern "C" DllExport void RemoveMidpointFilterExecute(ImageData source, ImageData result, int* ar, int maskSize)
{
	ImageData* src = &source;
	ImageData* res = &result;
	getBitMixedArray(src,ar);
	int i, j, a, b, minimum, maximum, side = maskSize / 2;
	Pixel* p1;
	Pixel* p2;

#pragma omp parallel for shared(src, res, ar, side) private(i, j, minimum, maximum, a, b, p1, p2)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			minimum = INT_MAX;
			maximum = INT_MIN;
			for ( a = i - side ; a <= i + side; a ++ )
			{
				for ( b = j - side ; b <= j + side ; b ++ )
				{
					minimum = min(minimum, GET2D(ar,src->Width,
						min(src->Width-1,max(0,b)),
						min(src->Height-1,max(0,a))));
					maximum = max(maximum, GET2D(ar,src->Width,
						min(src->Width-1,max(0,b)),
						min(src->Height-1,max(0,a))));
				}
			}

			fromBitMixed(minimum,p1);
			fromBitMixed(maximum,p2);

			SETPIXELRGB(GETPIXEL(res,j,i),
				((int)p1->R + (int)p2->R) / 2,
				((int)p1->G + (int)p2->G) / 2,
				((int)p1->B + (int)p2->B) / 2);
		}
	}
}
