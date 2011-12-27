#include "ImageData.h"
#include <iostream>
#include <math.h>
#include <fstream>
#include "NoiseHelperFunctions.h"
using namespace std;

extern "C" DllExport void AddPeriodicNoiseExecute(ImageData source, int* red, int* green, int* blue, double percentage, double amplitude, double frequencyX, double frequencyY, double shiftX, double shiftY)
{
	ImageData* src = &source;
	Pixel* p;
	int x, y, i, j, count;
	int remaining = src->Height * src->Width;
	int redMax = 0, greenMax = 0, blueMax = 0;
	double dist, part1, part2;

	ofstream fout ("lol.txt");

#pragma omp parallel for shared(fout, red, green, blue, percentage, src, remaining, amplitude, frequencyX, frequencyY, shiftX, shiftY, redMax, greenMax, blueMax) private(p, i, x, y, count, dist, part1, part2)
	for ( i = 0 ; i < 256; i ++)
	{
		part1 = (2 * PI * frequencyX * j) / (src->Width + shiftX);
		part2 = (2 * PI * frequencyY * i) / (src->Height + shiftY);
		dist = amplitude * abs(sin(part1 + part2));
		count = floor(percentage * dist * src->Height * src->Width);

		continue;

		fout << "at i = " << i << "\t dist = " << dist << "\tcount = " << count << endl;

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
