#include "Stdafx.h"

#ifndef IMAGEDATA_H_
#define IMAGEDATA_H_

namespace IntergalacticCpp
{
	namespace Data
	{
		// Used to access any pixel in an image
		#define GETPIXEL(IMG,I,J) ((Pixel*)(IMG->Base + IMG->Stride*J + I*sizeof(Pixel)))

		// Holds the image data used in all operations
		public ref struct ImageData
		{
		public:
			unsigned char* Base;
			int Stride;
			int Width, Height;
		};

		// Pixel structure
		struct Pixel
		{
		public:
			unsigned char Red,Green,Blue;

			Pixel(unsigned char R, unsigned char G, unsigned char B)
			{
				this->Red = R;
				this->Green = G;
				this->Blue = B;
			}
		};
	}
}
#endif