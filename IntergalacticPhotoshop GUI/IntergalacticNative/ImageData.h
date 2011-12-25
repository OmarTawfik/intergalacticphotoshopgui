#ifndef IMAGEDATA_H_
#define IMAGEDATA_H_

// Used to dll export the function
#define DllExport __declspec( dllexport )

// Used to access any pixel in an image
#define GETPIXEL(IMG,I,J) ((Pixel*)((IMG)->Base + (IMG)->Stride*(J) + (I)*sizeof(Pixel)))

// Used to set pixel data
#define SETPIXEL(PIXEL1, PIXEL2) {(PIXEL1)->R = (PIXEL2)->R; (PIXEL1)->G = (PIXEL2)->G; (PIXEL1)->B = (PIXEL2)->B; }
#define SETPIXELRGB(PIXEL,Red, Green, Blue) {(PIXEL)->R = (Red); (PIXEL)->G = (Green); (PIXEL)->B = (Blue);}
#define CUTOFF(PIXEL, Red, Green, Blue) { (PIXEL)->R = max(0,min(255,(Red))); (PIXEL)->G = max(0,min(255,(Green))); (PIXEL)->B = max(0,min(255,(Blue))); }
#define CUTOFFTOP(PIXEL, Red, Green, Blue) { (PIXEL)->R = min(255,(Red)); (PIXEL)->G = min(255,(Green)); (PIXEL)->B = min(255,(Blue)); }

#define PI 3.1415926535897932384626433832795
#define E 2.7182818284590452353602874713527

#define GET2D(ARR, stride, X, Y) (ARR[(Y)*(stride) + (X)])

// Holds the image data used in all operations
struct ImageData
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
	unsigned char R,G,B;

	Pixel()
	{
	}

	Pixel(unsigned char R, unsigned char G, unsigned char B)
	{
		this->R = R;
		this->G = G;
		this->B = B;
	}
};

#endif