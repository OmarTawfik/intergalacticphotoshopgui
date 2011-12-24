#include "ImageData.h"
#include "ConvolutionHelpers.h"
#include <iostream>
using namespace std;

extern "C" DllExport void FrontDiagonalEdgeDetectionOperationExecute(ImageData src, ImageData dest)
{
	int i, j, red, green, blue;
	Pixel *p, *rightUp, *leftDown, *rightDown, *leftUp, *up, *down, *left, *right;

	#pragma omp parallel for shared(src, dest) private(i, j, p, rightUp, leftDown, rightDown, leftUp, up, down, left, right, red, green, blue) 
    for (i = 0; i < src.Height; i++)
    {
        for (j = 0; j < src.Width; j++)
        {
			red = 0, green = 0, blue = 0;

			leftUp = GETLOCATION(&src, j - 1, i - 1);
			up = GETLOCATION(&src, j, i - 1);
			rightUp = GETLOCATION(&src, j + 1, i - 1);
			left = GETLOCATION(&src, j - 1, i);
			right = GETLOCATION(&src, j + 1, i);
			leftDown = GETLOCATION(&src, j - 1, i + 1);
			down = GETLOCATION(&src, j, i + 1);
			rightDown = GETLOCATION(&src, j + 1, i + 1);

			red += (leftUp->R * 5) + (up->R * 5) + (left->R * 5);
            green += (leftUp->G * 5) + (up->G * 5) + (left->G * 5);
            blue += (leftUp->B * 5) + (up->B * 5) + (left->B * 5);

            red += -(rightUp->R * 3) - (right->R * 3) - (leftDown->R * 3) - (down->R * 3) - (rightDown->R * 3);
            green += -(rightUp->G * 3) - (right->G * 3) - (leftDown->G * 3) - (down->G * 3) - (rightDown->G * 3);
            blue += -(rightUp->B * 3) - (right->B * 3) - (leftDown->B * 3) - (down->B * 3) - (rightDown->B * 3);

			p = GETPIXEL(&dest, j, i);
			CUTOFF(p, red, green, blue);
        }
    }
}            

