#include "ImageData.h"
using namespace IntergalacticCpp::Data;

namespace IntergalacticCpp
{
	namespace PixelOperations
	{
		public ref class CLRBinarizationOperation
		{
		public:
			static void Execute(ImageData^ image)
			{
				int totalGray = 0;
				for (int i = 0; i < image->Height; i++)
				{
					for (int j = 0; j < image->Width; j++)
					{
						Pixel* p = GETPIXEL(image,j,i);
						totalGray += (p->Red + p->Green +p->Blue)/3;
					}
				}

				int threshold = totalGray / (image->Height * image->Width);
				for (int i = 0; i < image->Height; i++)
				{
					for (int j = 0; j < image->Width; j++)
					{
						Pixel* p = GETPIXEL(image,j,i);
						int gray = (p->Red + p->Green +p->Blue)/3;

						if (gray < threshold)
						{
							p->Red = 0;
							p->Green = 0;
							p->Blue = 0;
						}
						else if (gray >= threshold)
						{
							p->Red = 255;
							p->Green = 255;
							p->Blue = 255;
						}
					}
				}
			}
		};
	}
}