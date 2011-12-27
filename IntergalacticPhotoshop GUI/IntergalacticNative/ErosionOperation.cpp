#include "ImageData.h"
#include "ConvolutionHelpers.h"
#include <iostream>
using namespace std;

extern "C" DllExport void ErosionOperationExecute(ImageData src, ImageData dest, bool * mask, int maskWidth, int maskHeight)
{
	bool resultBit, bit;
	int i, j, a, b;
	Pixel *p;
	
	int nx = maskWidth / 2;
	int ny = maskHeight / 2;

	#pragma omp parallel for shared(src, dest, mask, maskWidth, maskHeight) private(i, j, a, b, p, resultBit, bit) 
	for (i = 0; i < src.Height; i++)
	{
		for (j = 0; j < src.Width; j++)
		{
			resultBit = true;
			for (a = 0; a < maskHeight; a++)
			{
				for (b = 0; b < maskWidth; b++)
				{
					p = GETLOCATION(&src, j + b - nx, i + a - ny);
					bit = GET2D(mask, maskWidth, b, a);
					if (!bit)
						continue;

					if ((p->R != 0) != bit)
					{
						resultBit = false;
						goto RESULT;
					}
				}
			}
RESULT:
			a = resultBit? 255 : 0;
			p = GETPIXEL(&dest, j, i);
			p->R = a;
			p->G = a;
			p->B = a;
		}
	}
}            


