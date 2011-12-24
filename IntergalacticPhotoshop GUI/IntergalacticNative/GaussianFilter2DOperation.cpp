#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void GaussianFilter2DOperationExecute(ImageData src, ImageData dest, double sigma)
{
	int i, j, newX, newY, a, b, x, y;
	double red, green, blue;
	Pixel *p;

	int n = (int)(((3.7 * sigma) - 0.5));
	double bracket = 1.0 / (2 * PI * sigma * sigma);

	int maskSize = (2 * n) + 1;

	double** mask = new double*[maskSize];
	for (i=0; i<maskSize; i++)
	{
		mask[i] = new double[maskSize];
	}

	for (int y = -n, a = 0; y <= n; y++, a++)
	{
		for (int x = -n, b = 0; x <= n; x++, b++)
		{
			double power = -((x * x) + (y * y)) / (2 * sigma * sigma);
			mask[a][b] = bracket * pow(E, power);
		}
	}


	#pragma omp parallel for shared(src, dest, n, maskSize) private(i, j, newX, newY, a, b, x, y, red, green, blue, p) 
	for (i = 0; i < src.Height; i++)
	{
		for (j = 0; j < src.Width; j++)
		{
			red = 0;
			green = 0;
			blue = 0;
			for (y = i - n, a = 0; y <= i + n; y++, a++)
			{
				for (x = j - n, b = 0; x <= j + n; x++, b++)
				{
					newX = x; newY = y;
					if (newX < 0) newX = 0;
					if (newX >= src.Width) newX = src.Width - 1;
					
					if (newY < 0) newY = 0;
					if (newY >= src.Height) newY = src.Height - 1;

					p = GETPIXEL(&src, newX, newY);

					red += p->R * mask[a][b];
					green += p->G * mask[a][b];
					blue += p->B * mask[a][b];
				}
			}

			p = GETPIXEL(&dest, j, i);
			CUTOFFTOP(p, (int)red, (int)green, (int)blue);
		}
	}

	for (i=0; i<maskSize; i++)
	{
		delete [] mask[i];
	}

	delete [] mask;
}            

