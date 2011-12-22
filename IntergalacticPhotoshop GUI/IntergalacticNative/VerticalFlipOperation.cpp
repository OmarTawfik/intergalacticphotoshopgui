#include "ImageData.h"

extern "C" DllExport void VerticalFlipOperationExecute(ImageData source)
{
	ImageData* src = &source;

	int i, j;
	Pixel *topPixel, *bottomPixel, tmp;

#pragma omp parallel for shared(src) private(i, j, topPixel, bottomPixel, tmp) 
	for (i = 0; i < src->Height / 2; i++)
    {
        for (j = 0; j < src->Width; j++)
        {
            topPixel = GETPIXEL(src, j, i);
            bottomPixel = GETPIXEL(src, j, src->Height - i - 1);
			
			tmp.R = topPixel->R;
			tmp.G = topPixel->G;
			tmp.B = topPixel->B;

			SETPIXEL(topPixel, bottomPixel);
			SETPIXEL(bottomPixel, &tmp);
        }
    }
}            

