#include "ImageData.h"
#include <iostream>
#include <math.h>
#include "NoiseHelperFunctions.h"
using namespace std;

extern "C" DllExport void RemoveMaxFilterExecute(ImageData source, ImageData result, int* ar, int maskSize)
{
	ImageData* src = &source;
	ImageData* res = &result;
	getBitMixedArray(src,ar);
	int i, j, a, b, maximum, side = maskSize / 2;

#pragma omp parallel for shared(src, res, ar, side) private(i, j, maximum, a, b)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			maximum = INT_MIN;
			for ( a = i - side ; a <= i + side; a ++ )
			{
				for ( b = j - side ; b <= j + side ; b ++ )
				{
					maximum = max(maximum, GET2D(ar,src->Width,
						min(src->Width-1,max(0,b)),
						min(src->Height-1,max(0,a))));
				}
			}
			fromBitMixed(maximum,GETPIXEL(res,j,i));
		}
	}
}
