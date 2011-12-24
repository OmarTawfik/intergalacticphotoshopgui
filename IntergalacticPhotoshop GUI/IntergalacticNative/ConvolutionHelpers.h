#ifndef CONVOLUTIONHELPERS_H_
#define CONVOLUTIONHELPERS_H_

#include "ImageData.h"

#define GETLOCATION(IMG, X, Y) (GETPIXEL((IMG), max(0, min((IMG)->Width - 1, (X))), max(0, min((IMG)->Height - 1, (Y)))))

#endif