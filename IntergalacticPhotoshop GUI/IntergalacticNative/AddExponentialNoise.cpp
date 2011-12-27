#include "ImageData.h"
#include <iostream>
#include <math.h>
#include "NoiseHelperFunctions.h"
using namespace std;

extern "C" DllExport void AddExponentialNoiseExecute(ImageData source, int* red, int* green, int* blue, double percentage, double mean)
{
	ImageData* src = &source;
	Pixel* p;
	int x, y, i, j, count;
	int remaining = src->Height * src->Width;
	int redMax = 0, greenMax = 0, blueMax = 0;
	double dist;

#pragma omp parallel for shared(red, green, blue, percentage, src, remaining, mean, redMax, greenMax, blueMax) private(p, i, x, y, count, dist)
	for ( i = 0 ; i < 256; i ++)
	{
		dist =  mean * exp(-mean * i);
		count = floor(percentage * dist * src->Height * src->Width);

		if ( count > remaining )
			count = remaining;
		remaining -= count;

		while (count > 0)
		{
			x = rand() % src->Width;
			y = rand() % src->Height;

			if ( GET2D(red,src->Width,x,y) == 0 )
			{
				p = GETPIXEL(src,x,y);

				GET2D(red,src->Width,x,y) = p->R + i + 1;
				GET2D(green,src->Width,x,y) = p->G + i + 1;
				GET2D(blue,src->Width,x,y) = p->B + i + 1;

				redMax = max(redMax, p->R + i);
				greenMax = max(greenMax, p->G + i);
				blueMax = max(blueMax, p->B + i);

				count --;
			}
		}
	}

	if (redMax > 255)
		NormalizeIntegers(red,src->Height,src->Width,0, redMax);
	if (greenMax > 255)
		NormalizeIntegers(green,src->Height,src->Width,0, greenMax);
	if (blueMax > 255)
		NormalizeIntegers(blue,src->Height,src->Width,0, blueMax);

#pragma omp parallel for shared(src, red, green, blue) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			if ( GET2D(red,src->Width,j,i) != 0)
			{
				p = GETPIXEL(src,j,i);
				p->R = GET2D(red,src->Width,j,i) - 1;
				p->G = GET2D(green,src->Width,j,i) - 1;
				p->B = GET2D(blue,src->Width,j,i) - 1;
			}
		}
	}
}
