#include "ImageData.h"
#include <iostream>
using namespace std;

extern "C" DllExport void SaltandPepperNoiseAdditionOperationExecute(ImageData source, int* ar, double salt, double pepper)
{
	ImageData* src = &source;
	Pixel* p;
	int x, y, i, j;

	int count [] = { salt * src->Height * src->Width , pepper * src->Height * src->Width };
	int value [] = { 256 , 1 };

	if (count[0] + count[1] > src->Height * src->Width)
	{
		count[1] = (src->Height * src->Width) - count[0];
	}

#pragma omp parallel for shared(ar, count, value) private(i, x, y)
	for ( i = 0 ; i < 2 ; i ++)
	{
		while (count[i] > 0)
		{
			x = rand() % src->Width;
			y = rand() % src->Height;

			if ( GET2D(ar,src->Width,x,y) == 0 )
			{
				GET2D(ar,src->Width,x,y) = value[i];
				count[i] --;
			}
		}
	}

#pragma omp parallel for shared(src, ar) private(i, j, p)
	for (i = 0; i < src->Height; i++)
	{
		for (j = 0; j < src->Width; j++)
		{
			if ( GET2D(ar,src->Width,j,i) != 0)
			{
				p = GETPIXEL(src,j,i);
				p->R = p->G = p->B = GET2D(ar,src->Width,j,i) - 1;
			}
		}
	}
}
