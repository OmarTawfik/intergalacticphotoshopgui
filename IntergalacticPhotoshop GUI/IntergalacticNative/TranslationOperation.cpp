#include "ImageData.h"

extern "C" DllExport void TranslationOperationExecute(ImageData src, ImageData dest, int displacementX, int displacementY)
{
	int i, j, newX, newY;
	Pixel *newPixel;

	while (displacementX < src.Width)
    {
        displacementX += src.Width;
    }

    while (displacementY < src.Height)
    {
        displacementY += src.Height;
    }

#pragma omp parallel for shared(src, dest) private(i, j, newPixel, newX, newY) 
    for (i = 0; i < dest.Height; i++)
    {
        for (j = 0; j < dest.Width; j++)
        {
            newX = (j + displacementX) % dest.Width;
            newY = (i + displacementY) % dest.Height;

            newPixel = GETPIXEL(&src, j, i);
            SETPIXEL(GETPIXEL(&dest, newX, newY), newPixel);
        }
    }
}            

