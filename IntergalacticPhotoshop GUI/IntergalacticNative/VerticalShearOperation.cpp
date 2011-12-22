#include "ImageData.h"

extern "C" DllExport void VerticalShearOperationExecute(ImageData src, ImageData dest, double factor)
{
	int i, j, newY;
	Pixel *oldPixel;


#pragma omp parallel for shared(src, dest) private(i, j, oldPixel, newY) 
    for (i = 0; i < dest.Height; i++)
    {
        for (j = 0; j < dest.Width; j++)
        {
			newY = ((int)(i + (factor * j))) % dest.Height;

            oldPixel = GETPIXEL(&src, j, i);
            SETPIXEL(GETPIXEL(&dest, j, newY), oldPixel);
        }
    }
}