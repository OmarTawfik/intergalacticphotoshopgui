#include "ImageData.h"
#include <iostream>
#include <math.h>
#include <algorithm>
#include <vector>
#include "NoiseHelperFunctions.h"
using namespace std;

extern "C" DllExport void RemoveMedianFilterExecute(ImageData source, ImageData result, int* ar, int maskSize)
{
	ImageData* src = &source;
	ImageData* res = &result;
	getBitMixedArray(src,ar);
	int i, j, a, b, side = maskSize / 2;
	vector<int> mask;

#pragma omp parallel for shared(src, res, ar, side) private(i, j, a, b, mask)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			mask.clear();
			for ( a = i - side ; a <= i + side; a ++ )
			{
				for ( b = j - side ; b <= j + side ; b ++ )
				{
					mask.push_back(GET2D(ar,src->Width,
						min(src->Width-1,max(0,b)),
						min(src->Height-1,max(0,a))));
				}
			}

			sort(mask.begin(),mask.end());
			fromBitMixed(mask[mask.size()/2],GETPIXEL(res,j,i));
		}
	}
}
