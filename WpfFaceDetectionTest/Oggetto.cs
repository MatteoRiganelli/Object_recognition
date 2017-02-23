using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfFaceDetectionTest
{
    class Oggetto
    {

        private int x = 0;
        private int y = 0;
        private Boolean visible;

        private string caratt;

        //costruttore
        public Oggetto()
        {
            this.x = 0;
            this.y = 0;
            visible = true;
        }

        //set X value
        public void set_X_Value(int valoreX) { this.x = valoreX; }
        //get X value
        public int get_X_Value() { return this.x; }

        //set Y value
        public void set_Y_Value(int valoreY) { this.y = valoreY; }
        //get Y value
        public int get_Y_Value() { return this.y; }

        //set visible TRUE
        public void set_visible_true() { this.visible = true; }
        //set visible FALSE
        public void set_visible_false() { this.visible = false; }
        //get visible
        public Boolean get_Visible() { return this.visible; }


        public void set_All_Zero() { this.x = 0; this.y = 0;}
    }
}
