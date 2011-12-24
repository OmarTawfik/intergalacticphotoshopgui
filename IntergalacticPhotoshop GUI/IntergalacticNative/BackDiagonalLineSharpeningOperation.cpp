#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void BackDiagonalLineSharpeningOperationExecute(ImageData src, ImageData dest)
{
	int i, j, red, green, blue;
	Pixel *p, *rightUp, *leftDown;

	#pragma omp parallel for shared(src, dest) private(i, j, p, rightUp, leftDown, red, green, blue) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
			if (i == 0 || i == 0 || i == src.Height - 1 || j == src.Width)
			{
				SETPIXEL(GETPIXEL(&dest, j, i), GETPIXEL(&src, j, i));
				continue;
			}

			p = GETPIXEL(&src, j, i);
			leftDown = GETPIXEL(&src, j - 1, i + 1);
			rightUp = GETPIXEL(&src, j + 1, i - 1);
			
			red = p->R - leftDown->R + rightUp->R;
			green = p->G - leftDown->G + rightUp->G;
			blue = p->B - leftDown->B + rightUp->B; 

			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

