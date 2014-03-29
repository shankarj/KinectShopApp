using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;

namespace AppurpleKinectProto.modules
{
    public static class productBase
    {
        public static productLoader tempLoader = new productLoader();
    }

    public class productLoader
    {
        int position = -1;
        
        List<string> prodName = new List<string>();
        List<string> tagline = new List<string>();
        List<string> desc = new List<string>();
        List<string> val = new List<string>();
        List<string> imagename = new List<string>();

        StreamReader myReader = new StreamReader(Environment.CurrentDirectory + "\\shop.txt");
        int lc = 0;

        public productLoader()
        {
            while (!myReader.EndOfStream)
            {
                prodName.Add(myReader.ReadLine());
                tagline.Add(myReader.ReadLine());
                desc.Add(myReader.ReadLine());
                val.Add(myReader.ReadLine());
                imagename.Add(myReader.ReadLine());
                lc += 1;
            }
        }

        public void LoadNextProduct()
        {
            
            if (position < lc - 1)
            {
                position += 1;

                productObject.product.ProductName = prodName[position];
                productObject.product.TagLine = tagline[position];
                productObject.product.Description = desc[position];
                productObject.product.Value = Convert.ToInt16(val[position]);
                productObject.product.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\images\\" + imagename[position], UriKind.Absolute));

                
            }
        }

        public void LoadPreviousProduct()
        {
            if (position > 0)
            {
                position -= 1;

                productObject.product.ProductName = prodName[position];
                productObject.product.TagLine = tagline[position];
                productObject.product.Description = desc[position];
                productObject.product.Value = Convert.ToInt16(val[position]);
                productObject.product.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\images\\" + imagename[position], UriKind.Absolute));
            }
        }
    }
}
