#include "ImageData.h"

extern "C" DllExport void TranslationOperationExecute(ImageData src, ImageData dest, int displacementX, int displacementY)
{
	int i, j, newX, newY;
	Pixel *oldPixel, *newPixel;

	while (displacementX < src.Width)
    {
        displacementX += src.Width;
    }

    while (displacementY < src.Height)
    {
        displacementY += src.Height;
    }

#pragma omp parallel for shared(src, dest) private(i, j, oldPixel, newPixel, newX, newY) 
    for (int i = 0; i < dest.Height; i++)
    {
        for (int j = 0; j < dest.Width; j++)
        {
            int newX = (j + displacementX) % dest.Width;
            int newY = (i + displacementY) % dest.Height;

            oldPixel = GETPIXEL(&src, j, i);
            SETPIXEL(GETPIXEL(&dest, newX, newY), oldPixel);
        }
    }
}            

