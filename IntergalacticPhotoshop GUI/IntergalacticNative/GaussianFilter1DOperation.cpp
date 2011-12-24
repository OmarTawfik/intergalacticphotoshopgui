#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void GaussianFilter1DOperationExecute(ImageData src, ImageData dest, double sigma)
{
	int i, j, newX, newY, a, b, x, y;
	double red, green, blue;
	Pixel *p;

	int n = (int)((3.7 * sigma) - 0.5);
    double bracket = 1.0 / sqrt(2 * PI * sigma * sigma);

    int maskSize = (2 * n) + 1;
    double* horizontalMask = new double[maskSize];
    double* verticalMask = new double[maskSize];

	for (int x = 0, a = -n; x < maskSize; x++, a++)
    {
        double power = -(a * a) / (2 * sigma * sigma);
        horizontalMask[x] = bracket * pow(E, power);
    }

    for (int y = 0, b = -n; y < maskSize; y++, b++)
    {
        double power = -(b * b) / (2 * sigma * sigma);
        verticalMask[y] = bracket * pow(E, power);
    }

	#pragma omp parallel for shared(src, dest, n, maskSize, horizontalMask, verticalMask) private(i, j, newX, newY, b, x, red, green, blue, p) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
			red = 0;
			green = 0;
			blue = 0;
            for (x = j - n, b = 0; x <= j + n; x++, b++)
            {
				newX = x;
				newY = i;
							
				if (newX < 0) newX = 0;
				if (newX >= src.Width) newX = src.Width - 1;

                p = GETPIXEL(&src, newX, newY);
                red += p->R * horizontalMask[b];
                green += p->G * horizontalMask[b];
                blue += p->B * horizontalMask[b];
            }

			p = GETPIXEL(&dest, j, i);
			CUTOFFTOP(p, (int)red, (int)green, (int)blue);
        }
    }

    #pragma omp parallel for shared(src, dest, maskSize, horizontalMask, verticalMask) private(i, j, newX, newY, a, y, red, green, blue, p) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
            red = 0;
			green = 0;
			blue = 0;
            for (y = i - n, a = 0; y <= i + n; y++, a++)
            {
				newX = j;
				newY = y;

				if (newY < 0) newY = 0;
				if (newY >= src.Height) newY = src.Height - 1;

				p = GETPIXEL(&dest, newX, newY);

                red += p->R * verticalMask[a];
                green += p->G * verticalMask[a];
                blue += p->B * verticalMask[a];
            }

			p = GETPIXEL(&src, j, i);
			CUTOFFTOP(p, (int)(red), (int)(green), (int)(blue));
        }
    }

	delete [] horizontalMask;
	delete [] verticalMask;
}            

