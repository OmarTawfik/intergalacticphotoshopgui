#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void LaplacianSharpeningOperationExecute(ImageData src, ImageData dest)
{
	int i, j, red, green, blue;
	Pixel *p, *right, *left, *up, *down;
	
#pragma omp parallel for shared(src, dest) private(i, j, p, right, left, up, down, red, green, blue) 
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
			up = GETPIXEL(&src, j, i - 1);
			down = GETPIXEL(&src, j, i + 1);
			
			red = 5 * p->R - left->R - right->R - up->R - down->R;
			green = 5 * p->G - left->G - right->G - up->G - down->G;
			blue = 5 * p->B - left->B - right->B - up->B - down->B; 
			
			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

