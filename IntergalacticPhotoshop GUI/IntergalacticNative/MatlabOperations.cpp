#include "ImageData.h"
#include <iostream>
using namespace std;

void DoubleNormalizeExecute(double* ar, int height, int width)
{
	double minimum = DBL_MAX, maximum = -DBL_MAX;
	int i, j;

#pragma omp parallel for shared(ar, height, width, minimum, maximum) private(i, j)
	for (i = 0; i < height; i++)
	{
		for (j = 0; j < width; j++)
		{
			minimum = min(minimum, GET2D(ar,width,j,i));
			maximum = max(maximum, GET2D(ar,width,j,i));
		}
	}

	double ratio = (maximum - minimum) / 255.0;

#pragma omp parallel for shared(ar, height, width, minimum, ratio) private(i, j)
	for (i = 0; i < height; i++)
	{
		for (j = 0; j < width; j++)
		{
			GET2D(ar,width,j,i) = (GET2D(ar,width,j,i) - minimum) / ratio;
		}
	}
}            

extern "C" DllExport void DoubleToImageExecute(ImageData source, double* red, double* green, double* blue)
{
	DoubleNormalizeExecute(red, source.Height, source.Width);
	DoubleNormalizeExecute(green, source.Height, source.Width);
	DoubleNormalizeExecute(blue, source.Height, source.Width);

	ImageData* src = &source;
	int i, j;
	Pixel* p;

#pragma omp parallel for shared(src, red, green, blue) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			p->R = GET2D(red,src->Width,j,i);
			p->G = GET2D(green,src->Width,j,i);
			p->B = GET2D(blue,src->Width,j,i);
		}
	}
}    

extern "C" DllExport void ImageToDoubleExecute(ImageData source, double* red, double* green, double* blue)
{
	ImageData* src = &source;
	int i, j;
	Pixel* p;

#pragma omp parallel for shared(src, red, green, blue) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			p = GETPIXEL(src,j,i);
			GET2D(red,src->Width,j,i) = p->R;
			GET2D(green,src->Width,j,i) = p->G;
			GET2D(blue,src->Width,j,i) = p->B;
		}
	}
} 