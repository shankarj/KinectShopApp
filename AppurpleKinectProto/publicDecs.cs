/*
 *COPYRIGHTS APPURPLE 2013
 *AUTHOR : SHANKAR J
 *VERSION : 1.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Kinect;

namespace AppurpleKinectProto
{
    public class publicDecs : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int xval;
        public int X { get { return xval; } set { xval = value; if (this.PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("X")); } } }

        int yval;
        public int Y { get { return yval; } set { yval = value; if (this.PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Y")); } } }
    }

    public static class objects
    {
        public static publicDecs VAL = new publicDecs();
    }
}
