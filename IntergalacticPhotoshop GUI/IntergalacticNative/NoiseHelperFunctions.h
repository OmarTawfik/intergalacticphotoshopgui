#ifndef NOISEHELPERFUNCTIONS_H_
#define NOISEHELPERFUNCTIONS_H_

#include "ImageData.h"

int toBitMixed(Pixel* p);

void fromBitMixed(int bits, Pixel* p);

int** getBitMixedArray(ImageData* src);

void NormalizeIntegers(int* ar, int height, int width, int oldMin, int oldMax);

#endif