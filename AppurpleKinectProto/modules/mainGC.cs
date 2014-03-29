using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppurpleKinectProto.modules
{
    public class mainGC
    {
        public string classifyPosition(int XVal, int YVal)
        {
            if ((XVal >= 100) && (XVal <= 280) && (YVal >= 110) && (YVal <= 345))
            {
                return "men";
            }

            if ((XVal >= 660) && (XVal <= 790) && (YVal >= 80) && (YVal <= 240))
            {
                return "women";
            }

            return null;
        }
    }
}
