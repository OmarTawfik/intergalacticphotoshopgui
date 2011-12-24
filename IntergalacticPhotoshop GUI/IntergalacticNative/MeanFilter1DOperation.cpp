#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void MeanFilter1DOperationExecute(ImageData src, ImageData dest, int maskSize)
{
	int i, j, newX, newY, a, b, red, green ,blue;
	Pixel *p;

	int side = (int)maskSize / 2;

	#pragma omp parallel for shared(src, dest, side, maskSize) private(i, j, newX, newY, b, red, green, blue, p) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
            red = 0;
			green = 0;
			blue = 0;
            for (b = j - side; b <= j + side; b++)
            {
				newX = b;
				newY = i;
							
				if (newX < 0) newX = 0;
				if (newX >= src.Width) newX = src.Width - 1;

                p = GETPIXEL(&src, newX, newY);
                red += p->R;
                green += p->G;
                blue += p->B;
            }

			p = GETPIXEL(&dest, j, i);
			CUTOFFTOP(p, (red / maskSize), (green / maskSize), (blue / maskSize));
        }
    }

    #pragma omp parallel for shared(src, dest, side, maskSize) private(i, j, newX, newY, a, red, green, blue, p) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
            red = 0;
			green = 0;
			blue = 0;
            for (a = i - side; a <= i + side; a++)
            {
				newX = j;
				newY = a;

				if (newY < 0) newY = 0;
				if (newY >= src.Height) newY = src.Height - 1;

				p = GETPIXEL(&dest, newX, newY);

                red += p->R;
                green += p->G;
                blue += p->B;
            }

			p = GETPIXEL(&src, j, i);
			CUTOFFTOP(p, (int)(red / maskSize), (int)(green / maskSize), (int)(blue / maskSize));
        }
    }
}            

