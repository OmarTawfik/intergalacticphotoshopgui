#include "ImageData.h"
#include <iostream>
#include <math.h>
#include <algorithm>
#include <vector>
#include "NoiseHelperFunctions.h"
using namespace std;

extern "C" DllExport void RemoveAdpativeMedianFilterExecute(ImageData source, ImageData result, int* ar, int maskSize)
{
	ImageData* src = &source;
	ImageData* res = &result;
	getBitMixedArray(src,ar);
	int i, j, a, b, side, current;
	vector<int> mask;
	int minimum, maximum, original, median;

#pragma omp parallel for shared(src, res, ar, maskSize) private(i, j, a, b, mask, current, side, minimum, maximum, original, median)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			for ( current = 3 ; current <= maskSize ; current += 2)
			{
				side = current / 2;
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

				minimum = mask[0];
				maximum = mask[mask.size()-1];
				original = GET2D(ar,src->Width,j,i);
				median = mask[mask.size()/2];

				if (median != minimum && median != maximum)
				{
					if (original != minimum && original != maximum)
					{
						fromBitMixed(original,GETPIXEL(res,j,i));
					}
					else
					{
						fromBitMixed(median,GETPIXEL(res,j,i));
					}

					break;
				}
				else
				{
					if (current == maskSize)
					{
						fromBitMixed(median,GETPIXEL(res,j,i));
						break;
					}
				}
			}
		}
	}
}
