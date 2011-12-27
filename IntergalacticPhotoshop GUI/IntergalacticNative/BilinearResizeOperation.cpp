#include "ImageData.h"

extern "C" DllExport void BilinearResizeOperationExecute(ImageData source, ImageData destination)
{
	ImageData* src = &source;
	ImageData* dest = &destination;

	int i, j, x1, x2, y1, y2;
	double widthRatio, heightRatio, oldX, oldY, xfraction, yfraction;
	Pixel *p1, *p2, *p3, *p4, *newPixel;

	widthRatio = (double)src->Width / (double)dest->Width;
	heightRatio = (double)src->Height / (double)dest->Height;

	double z1R, z1B, z1G, z2R, z2G, z2B;

#pragma omp parallel for shared(src, dest, widthRatio, heightRatio) private(i, j, x1, x2, y1, y2, p1, p2, p3, p4, z1R, z1B, z1G, z2R, z2G, z2B, newPixel, oldX, oldY, xfraction, yfraction) 
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
						
			z1R = (p1->R * (1.0 - xfraction)) + (p2->R * xfraction);
			z1G = (p1->G * (1.0 - xfraction)) + (p2->G * xfraction);
			z1B = (p1->B * (1.0 - xfraction)) + (p2->B * xfraction);

			z2R = (p3->R * (1.0 - xfraction)) + (p4->R * xfraction);
			z2G = (p3->G * (1.0 - xfraction)) + (p4->G * xfraction);
			z2B = (p3->B * (1.0 - xfraction)) + (p4->B * xfraction);

			newPixel = GETPIXEL(dest, j, i);

			newPixel->R = (z1R * (1.0 - yfraction)) + (z2R * yfraction);
			newPixel->G = (z1G * (1.0 - yfraction)) + (z2G * yfraction);
			newPixel->B = (z1B * (1.0 - yfraction)) + (z2B * yfraction);
		}
	}
}