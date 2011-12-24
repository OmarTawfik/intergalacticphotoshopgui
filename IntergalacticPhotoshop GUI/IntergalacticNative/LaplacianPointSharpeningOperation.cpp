#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void LaplacianPointSharpeningOperationExecute(ImageData src, ImageData dest)
{
	int i, j, x, y, red, green, blue, newX, newY;
	Pixel *p, *center;
	
#pragma omp parallel for shared(src, dest) private(i, j, x, y, red, green, blue, newX, newY, p, center) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
			red = 0;
			green = 0;
			blue = 0;
            for (y = i - 1; y <= i + 1; y++)
            {
                for (x = j - 1; x <= j + 1; x++)
                {
					newX = x;
					newY = y;
					
					if (newX < 0) newX = 0;
					if (newX >= src.Width) newX = src.Width - 1;
					if (newY < 0) newY = 0;
					if (newY >= src.Height) newY = src.Height - 1;

                    p = GETPIXEL(&src, newX, newY);
                    red -= p->R;
                    green -= p->G;
                    blue -= p->B;
                }
            }

            center = GETPIXEL(&src, j, i);
            red += 10 * center->R;
            green += 10 * center->G;
            blue += 10 * center->B;
			
			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

