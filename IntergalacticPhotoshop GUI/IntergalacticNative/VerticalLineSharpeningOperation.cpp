#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void VerticalLineSharpeningOperationExecute(ImageData src, ImageData dest)
{
	int i, j, red, green, blue;
	Pixel *p, *right, *left;
	
#pragma omp parallel for shared(src, dest) private(i, j, p, right, left, red, green, blue) 
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
			left = GETPIXEL(&src, j - 1, i);
			right = GETPIXEL(&src, j + 1, i);
			
			red = p->R + left->R - right->R;
			green = p->G + left->G - right->G;
			blue = p->B + left->B - right->B; 
			
			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

