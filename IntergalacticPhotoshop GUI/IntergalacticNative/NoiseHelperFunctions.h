#ifndef NOISEHELPERFUNCTIONS_H_
#define NOISEHELPERFUNCTIONS_H_

#include "ImageData.h"

int toBitMixed(Pixel* p);

void fromBitMixed(int bits, Pixel* p);

void getBitMixedArray(ImageData* src, int* ar);

void NormalizeIntegers(int* ar, int height, int width, int oldMin, int oldMax);

#endif