#include "NoiseHelperFunctions.h"
#include "ImageData.h"

int toBitMixed(Pixel* p)
{
	int total = 0, i;

	for (i = 0; i < 8; i++)
	{
		total |= (p->R & (1 << i)) << ((i + 1) * 2);
		total |= (p->G & (1 << i)) << ((i * 2) + 1);
		total |= (p->B & (1 << i)) << (i * 2);
	}

	return total;
}

void fromBitMixed(int bits, Pixel* p)
{
	p->R = p->G = p->B = 0;
	int i;

	for (i = 0; i < 8; i++)
	{
		p->R |= ((bits & (1 << ((i * 3) + 2))) >> ((i * 2) + 2));
		p->G |= ((bits & (1 << ((i * 3) + 1))) >> ((i * 2) + 1));
		p->B |= ((bits & (1 << (i * 3))) >> (i * 2));
	}
}

int** getBitMixedArray(ImageData* src)
{
	int** ar = new int* [src->Height];
	int i, j;

	for ( i = 0 ; i < src->Height ; i ++ )
	{
		ar[i] = new int[src->Width];
		for ( j = 0 ; j < src->Width ; j ++ )
		{
			ar[i][j] = toBitMixed(GETPIXEL(src,j,i));
		}
	}

	return ar;
}

void NormalizeIntegers(int* ar, int height, int width, int oldMin, int oldMax)
{
	int i, j;
	double oldMultiplier = (oldMax - oldMin) / 255.0;

#pragma omp parallel for shared(ar, height, width, oldMultiplier) private(i, j)
	for (i = 0; i < height; i++)
	{
		for (j = 0; j < width; j++)
		{
			GET2D(ar,width,j,i) = (GET2D(ar,width,j,i) - oldMin) / oldMultiplier;
		}
	}
}