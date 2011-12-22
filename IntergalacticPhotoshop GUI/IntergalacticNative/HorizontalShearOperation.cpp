#include "ImageData.h"

extern "C" DllExport void HorizontalShearOperationExecute(ImageData src, ImageData dest, double factor)
{
	int i, j, newX;
	Pixel *oldPixel, *newPixel;


#pragma omp parallel for shared(src, dest) private(i, j, oldPixel, newPixel, newX) 
    for (i = 0; i < dest.Height; i++)
    {
        for (j = 0; j < dest.Width; j++)
        {
            newX = ((int)(j + (factor * i))) % dest.Width;

            oldPixel = GETPIXEL(&src, j, i);
            SETPIXEL(GETPIXEL(&dest, newX, i), oldPixel);
        }
    }
}