#include "ImageData.h"
#include "ConvolutionHelpers.h"
#include <iostream>
using namespace std;

extern "C" DllExport void LaplacianPointEdgeDetectionOperationExecute(ImageData src, ImageData dest)
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
					
                    p = GETLOCATION(&src, newX, newY);
                    red -= p->R;
                    green -= p->G;
                    blue -= p->B;
                }
            }

            center = GETPIXEL(&src, j, i);
            red += 9 * center->R;
            green += 9 * center->G;
            blue += 9 * center->B;
			
			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

