#include "ImageData.h"
using namespace IntergalacticCpp::Data;

namespace IntergalacticCpp
{
	namespace PixelOperations
	{
		public ref class CLRInverseOperation
		{
		public:
			static void Execute(ImageData^ image)
			{
				int i, j;
				Pixel* p;
				for (i = 0; i<image->Height; ++i)
				{
					for (j = 0; j<image->Width; ++j)
					{
						p = GETPIXEL(image,j, i);
						p->Red = 255 - p->Red;
						p->Green = 255 - p->Green;
						p->Blue = 255 - p->Blue;
					}
				}
			}
		};
	}
}