#include "ImageData.h"
#include <iostream>
#include <math.h>
#include "NoiseHelperFunctions.h"
using namespace std;

extern "C" DllExport void RemoveGeometricMeanFilterExecute(ImageData source, ImageData result, int maskSize)
{
	ImageData* src = &source;
	ImageData* res = &result;
	int i, j, a, b, side = maskSize / 2;
	double power = 1.0 / (maskSize * maskSize), red, green, blue;
	Pixel* p;

#pragma omp parallel for shared(src, res, side, power) private(i, j, a, b, red, green, blue, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			red = 1, green = 1, blue = 1;
			for (a = i - side; a <= i + side; a++)
			{
				for (b = j - side; b <= j + side; b++)
				{
					p = GETPIXEL(src,
						min(src->Width-1,max(0,b)),
						min(src->Height-1,max(0,a)));

					red *= pow(p->R, power);
					green *= pow(p->G, power);
					blue *= pow(p->B, power);
				}
			}

			SETPIXELRGB(GETPIXEL(res,j,i),red,green,blue);
		}
	}
}
