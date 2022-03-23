using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
    /// <summary>
    /// 沃德普光源控制的通讯
    /// </summary>
  public  class LightCtrlWordp :LightCtrlBase
  {

        public override void DoInit()
        {
            base.DoInit();
            Panels = new LightPanelEnum[] { LightPanelEnum.Panel0, LightPanelEnum.Panel1, LightPanelEnum.Panel2, LightPanelEnum.Panel3 };//初始化通道数
        }

        public override void SetLightValue(int Channel,int Value)
        {
            string m = Value.ToString("X2");
            string mn = m.Substring(0, 1);//取第一位
            int mnn = Convert.ToInt32(mn, 16) + 6;
            string mnnn = mnn.ToString("X2").Substring(1, 1);
            string mm = m.Substring(m.Length - 1, 1);//取最后一位
            string mCode = "";
            int mm1 =0;
            string mm11 ="";
            switch (Channel) {
                case 0:
                    mCode = "40 05 01 00 1A 00" + " " + m + " " + mnnn + mm;
                    break;
                case 1:
                    if (mm == "F"){
                        mnn = mnn + 1;
                        mnnn = mnn.ToString("X2").Substring(1, 1);
                    }
                    mm1 = Convert.ToInt32(mm, 16) + 1;
                    mm11 = mm1.ToString("X2").Substring(1, 1);
                     mCode = "40 05 01 00 1A 01" + " " + m + " " + mnnn + mm11;
                    break;
                case 2:
                    if (mm == "E" | mm == "F")
                    {
                        mnn = mnn + 1;
                        mnnn = mnn.ToString("X2").Substring(1, 1);
                    }
                     mm1 = Convert.ToInt32(mm, 16) + 2;
                     mm11 = mm1.ToString("X2").Substring(1, 1);
                     mCode = "40 05 01 00 1A 02" + " " + m + " " + mnnn + mm11;
                    break;
                case 3:
                    if (mm == "D" | mm == "E" | mm == "F")
                    {
                        mnn = mnn + 1;
                        mnnn = mnn.ToString("X2").Substring(1, 1);
                    }
                    mm1 = Convert.ToInt32(mm, 16) + 3;
                    mm11 = mm1.ToString("X2").Substring(1, 1);
                     mCode = "40 05 01 00 1A 03" + " " + m + " " + mnnn + mm11;
                    break;                
            }
            Write(mCode);

        }

        public override void OpenLightChannel(int Channel)
        {
            string mCode = "40 05 01 00 2A 00 01 71";
            switch (Channel)
            {
                case 0:
                    mCode = "40 05 01 00 2A 00 01 71";//开
                    break;
                case 1:
                    mCode = "40 05 01 00 2A 01 01 72";//开
                    break;
                case 2:
                    mCode = "40 05 01 00 2A 02 01 73";//开
                    break;
                case 3:
                    mCode = "40 05 01 00 2A 03 01 74";//开
                    break;          
            }
            Write(mCode);
        }

        public override void CloseLightChannel(int Channel)
        {
            string mCode = "40 05 01 00 2A 00 01 71";
            switch (Channel)
            {
                case 0:
                    mCode = "40 05 01 00 2A 00 00 70";//关
                    break;
                case 1:
                    mCode = "40 05 01 00 2A 01 00 71";//关
                    break;
                case 2:
                    mCode = "40 05 01 00 2A 02 00 72";//关
                    break;
                case 3:
                    mCode = "40 05 01 00 2A 03 00 73";//关
                    break;
            }
            Write(mCode);
        }

         

    }
}
