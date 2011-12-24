#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void HorizontalLineSharpeningOperationExecute(ImageData src, ImageData dest)
{
	int i, j, red, green, blue;
	Pixel *p, *down, *up;
	
#pragma omp parallel for shared(src, dest) private(i, j, p, down, up, red, green, blue) 
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
			up = GETPIXEL(&src, j, i - 1);
			down = GETPIXEL(&src, j, i + 1);
			
			red = p->R + up->R - down->R;
			green = p->G + up->G - down->G;
			blue = p->B + up->B - down->B; 
			
			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

