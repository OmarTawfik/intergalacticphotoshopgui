#include "ImageData.h"

extern "C" DllExport void BilinearResizeOperationExecute(ImageData source, ImageData destination)
{
	ImageData* src = &source;
	ImageData* dest = &destination;

	int i, j, x1, x2, y1, y2;
	float widthRatio, heightRatio, oldX, oldY, xfraction, yfraction;
	Pixel *p1, *p2, *p3, *p4, z1, z2, *newPixel;

	widthRatio = (float)src->Width / (float)dest->Width;
	heightRatio = (float)src->Height / (float)dest->Height;

#pragma omp parallel for shared(src, dest, widthRatio, heightRatio) private(i, j, x1, x2, y1, y2, p1, p2, p3, p4, z1, z2, newPixel, oldX, oldY, xfraction, yfraction) 
	for (i = 0; i < dest->Height; ++i)
	{
		for (j = 0; j < dest->Width; ++j)
		{
			oldX = widthRatio * j;
			oldY = heightRatio * i;

			x1 = (int)oldX;
			x2 = (x1 + 1 < src->Width) ? x1 + 1 : x1;
			y1 = (int)oldY;
			y2 = (y1 + 1 < src->Height) ? y1 + 1 : y1;

			p1 = GETPIXEL(src, x1, y1);
			p2 = GETPIXEL(src, x2, y1);
			p3 = GETPIXEL(src, x1, y2);
			p4 = GETPIXEL(src, x2, y2);

			xfraction = oldX - x1;
			yfraction = oldY - y1;
						
			z1.R = (p1->R * (1.0f - xfraction)) + (p2->R * xfraction);
			z1.G = (p1->G * (1.0f - xfraction)) + (p2->G * xfraction);
			z1.B = (p1->B * (1.0f - xfraction)) + (p2->B * xfraction);

			z2.R = (p3->R * (1.0f - xfraction)) + (p4->R * xfraction);
			z2.G = (p3->G * (1.0f - xfraction)) + (p4->G * xfraction);
			z2.B = (p3->B * (1.0f - xfraction)) + (p4->B * xfraction);

			newPixel = GETPIXEL(dest, j, i);

			newPixel->R = (z1.R * (1 - yfraction)) + (z2.R * yfraction);
			newPixel->G = (z1.G * (1 - yfraction)) + (z2.G * yfraction);
			newPixel->B = (z1.B * (1 - yfraction)) + (z2.B * yfraction);
		}
	}
}