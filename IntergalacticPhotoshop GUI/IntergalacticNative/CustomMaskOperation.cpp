#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void CustomMaskOperationExecute(ImageData src, ImageData dest, double* mask,int maskSize)
{
	int i, j, newX, newY, a, b, x, y;
	double red, green ,blue, tmp;
	Pixel *p;

	int side = (int)maskSize / 2;

#pragma omp parallel for shared(src, dest, side, mask, maskSize) private(i, j, newX, newY, a, b, x, y, red, green, blue, tmp, p) 
	for (i = 0; i < src.Height; i++)
	{
		for (j = 0; j < src.Width; j++)
		{
			red = 0;
			green = 0;
			blue = 0;
			for (y = 0, a = i - side; a < i + side; y++, a++)
			{
				for (x = 0, b = j - side; b < j + side; x++, b++)
				{
					newX = b;
					newY = a;

					if (newX < 0) newX = 0;
					if (newY < 0) newY = 0;
					if (newX >= src.Width) newX = src.Width - 1;
					if (newY >= src.Height) newY = src.Height - 1;

					p = GETPIXEL(&src, newX, newY);

					tmp = GET2D(mask, maskSize, x, y);

					red += (double)p->R * tmp;
					green += (double)p->G * tmp;
					blue += (double)p->B * tmp;
				}
			}

			p = GETPIXEL(&dest, j, i);
			CUTOFFTOP(p, (int)(red), (int)(green), (int)(blue));
		}
	}
}            
