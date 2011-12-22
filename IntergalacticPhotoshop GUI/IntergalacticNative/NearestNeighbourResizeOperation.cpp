#include "ImageData.h"

extern "C" DllExport void NearestNeighbourResizeOperationExecute(ImageData source, ImageData destination)
{
	ImageData* src = &source;
	ImageData* dest = &destination;

	int i, j;
	float widthRatio, heightRatio, oldX, oldY;
	Pixel *newPixel, *oldPixel;

	widthRatio = (float)src->Width / (float)dest->Width;
	heightRatio = (float)src->Height / (float)dest->Height;

#pragma omp parallel for shared(src, dest, widthRatio, heightRatio) private(i, j, newPixel, oldPixel, oldX, oldY) 
	for (i = 0; i < dest->Height; ++i)
	{
		for (j = 0; j < dest->Width; ++j)
		{
			oldX = ((float)j) * widthRatio;
            oldY = ((float)i) * heightRatio;

			oldPixel = GETPIXEL(src, (int)oldX, (int)oldY);
			newPixel = GETPIXEL(dest, j, i);

			newPixel->R = oldPixel->R;
			newPixel->G = oldPixel->G;
			newPixel->B = oldPixel->B;
		}
	}
}