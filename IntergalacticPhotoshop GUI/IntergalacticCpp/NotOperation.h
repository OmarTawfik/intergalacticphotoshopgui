#include "ImageData.h"
using namespace IntergalacticCpp::Data;

namespace IntergalacticCpp
{
	namespace PixelOperations
	{
		/// Not operation
		public ref class NotCLROp
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
						p->R = 255 - p->R;
						p->G = 255 - p->G;
						p->B = 255 - p->B;
					}
				}
			}
		};
	}
}