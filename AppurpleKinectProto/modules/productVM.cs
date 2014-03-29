using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace AppurpleKinectProto.modules
{
    public class productVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string pname;
        public string ProductName { get { return pname; } set { pname = value; if (this.PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("ProductName")); } } }

        string tag;
        public string TagLine { get { return tag; } set { tag = value; if (this.PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("TagLine")); } } }

        string desc;
        public string Description { get { return desc; } set { desc = value; if (this.PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Description")); } } }

        int val;
        public int Value { get { return val; } set { val = Convert.ToInt16(value); if (this.PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Value")); } } }

        BitmapImage imgsrc;
        public BitmapImage ImageSource { get { return imgsrc; } set { imgsrc = value; if (this.PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("ImageSource")); } } }

    }

    public static class productObject
    {
        public static productVM product = new productVM();
    }
}
