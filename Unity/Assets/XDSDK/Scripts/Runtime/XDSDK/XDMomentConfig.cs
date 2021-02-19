using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xdsdk
{
    public class XDMomentConfig
    {
       
        public static int ORIENTATION_DEFAULT = -1;
        public static int ORIENTATION_LANDSCAPE = 0;
        public static int ORIENTATION_PORTRAIT = 1;
        public static int ORIENTATION_SENSOR = 2;

        private int orientation = ORIENTATION_DEFAULT;

        public XDMomentConfig()
        {
        }

        public void SetOrientation(int orientation)
        {
            this.orientation = orientation;
            
        }

        public int GetOrientation()
        {
            return orientation;
        }

        public string GetConfigString()
        {
            return "{\"orientation\":" + orientation +"}";
        }
       
    }
}