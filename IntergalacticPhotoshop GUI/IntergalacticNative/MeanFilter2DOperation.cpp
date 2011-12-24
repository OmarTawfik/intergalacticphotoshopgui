#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void MeanFilter2DOperationExecute(ImageData src, ImageData dest, int maskSize)
{
	int i, j, newX, newY, a, b, red, green ,blue;
	Pixel *p;

	int side = (int)maskSize / 2;
	int maskSizeSq = maskSize * maskSize;

	#pragma omp parallel for shared(src, dest, side, maskSize, maskSizeSq) private(i, j, newX, newY, a, b, red, green, blue, p) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
            red = 0;
			green = 0;
			blue = 0;
            for (a = i - side; a < i + side; a++)
            {
                for (b = j - side; b < j + side; b++)
                {
					newX = b;
					newY = a;
							
					if (newX < 0) newX = 0;
					if (newY < 0) newY = 0;
					if (newX >= src.Width) newX = src.Width - 1;
					if (newY >= src.Height) newY = src.Height - 1;

					p = GETPIXEL(&src, newX, newY);
					
                    red += p->R;
                    green += p->G;
                    blue += p->B;
                }
            }
	
			p = GETPIXEL(&dest, j, i);
			CUTOFFTOP(p, (int)(red / maskSizeSq), (int)(green / maskSizeSq), (int)(blue / maskSizeSq));
        }
    }
}            

