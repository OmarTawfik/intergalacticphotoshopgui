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