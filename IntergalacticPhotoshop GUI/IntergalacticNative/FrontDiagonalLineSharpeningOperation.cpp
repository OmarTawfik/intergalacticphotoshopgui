#include "ImageData.h"
#include "ConvolutionHelpers.h"
#include <iostream>
using namespace std;

extern "C" DllExport void FrontDiagonalLineSharpeningOperationExecute(ImageData src, ImageData dest)
{
	int i, j, red, green, blue;
	Pixel *p, *rightDown, *leftUp;
	
#pragma omp parallel for shared(src, dest) private(i, j, p, rightDown, leftUp, red, green, blue) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
			p = GETPIXEL(&src, j, i);

			leftUp = GETLOCATION(&src, j - 1, i - 1);
			rightDown = GETLOCATION(&src, j + 1, i + 1);
			
			red = p->R + leftUp->R - rightDown->R;
			green = p->G + leftUp->G - rightDown->G;
			blue = p->B + leftUp->B - rightDown->B; 
			
			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

