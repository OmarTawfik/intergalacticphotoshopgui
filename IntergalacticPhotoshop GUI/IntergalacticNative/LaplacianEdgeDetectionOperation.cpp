#include "ImageData.h"
#include "ConvolutionHelpers.h"
#include <iostream>
using namespace std;

extern "C" DllExport void LaplacianEdgeDetectionOperationExecute(ImageData src, ImageData dest)
{
	int i, j, red, green, blue;
	Pixel *p, *center, *up, *down, *left, *right;

	#pragma omp parallel for shared(src, dest) private(i, j, p, center, up, down, left, right, red, green, blue) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
			red = 0, green = 0, blue = 0;

			center = GETPIXEL(&src, j, i);
			up = GETLOCATION(&src, j, i - 1);
			left = GETLOCATION(&src, j - 1, i);
			right = GETLOCATION(&src, j + 1, i);
			down = GETLOCATION(&src, j, i + 1);
			
			red = -up->R - down->R - right->R - left->R + (4 * center->R);
            green = -up->G - down->G - right->G - left->G + (4 * center->G);
            blue = -up->B - down->B - right->B - left->B + (4 * center->B);


			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

