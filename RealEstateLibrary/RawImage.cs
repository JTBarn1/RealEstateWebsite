using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class RawImage
    {
        private byte[] imageData;
        private string imageType, imageName, imagePath,caption;

        public byte[] ImageData { get { return imageData; } set { imageData = value; } }
        public string ImageType { get { return imageType; } set { imageType = value; } }
        public string ImageName { get { return imageName; } set { imageName = value; } }
        public string ImagePath { get { return imagePath; } set { imagePath = value; } }
        public string Caption { get { return caption; } set { caption = value; } }

        public RawImage(byte[] imageData, string imageType, string imageName, string caption, string imagePath)
        {
            this.imageData = imageData;
            this.imageType = imageType;
            this.imageName = imageName;
            this.imagePath = imagePath;
            this.caption = caption;
        }

    }
}
