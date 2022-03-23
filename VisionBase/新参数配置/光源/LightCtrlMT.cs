using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionBase
{
   public  class LightCtrlMT:LightCtrlBase
    {
        private MENTO_CONTROLLER.Mento_controller mento_Controller;
        public override void DoInit()
        {
            port = new System.IO.Ports.SerialPort();
            mento_Controller = new MENTO_CONTROLLER.Mento_controller();
            Panels = new LightPanelEnum[] { LightPanelEnum.Panel0, LightPanelEnum.Panel1, LightPanelEnum.Panel2, LightPanelEnum.Panel3 };//初始化通道数
            if (lightCtrlParaItem != null) {
                port.PortName = lightCtrlParaItem.PortName.ToString();
                port.BaudRate = 115200;
                port.Handshake = System.IO.Ports.Handshake.None;
                port.Parity = System.IO.Ports.Parity.None;
                port.DataBits = 8;
                port.StopBits = System.IO.Ports.StopBits.One;
            }
        }


        public override void SetLightValue(int Channel, int Value)
        {
            if (Channel < 0 || Channel > 3) return;
            if (Value < 0 || Value > 255) return;
            byte LightValue = Convert.ToByte(Value);
            byte LightChannel = Convert.ToByte(Channel);
            bool rlt = mento_Controller.Set_lum(port, LightChannel, LightValue);


        }

        public override void OpenLightChannel(int Channel)
        {
            if (Channel < 0 || Channel > 3) return;
            Byte Ch = Convert.ToByte(Channel);
            bool State = false;
            bool rlt = true;
            rlt =mento_Controller.Get_state(port, Ch, ref State);
            if (!rlt) return;
            if (State) return;
            rlt = mento_Controller.Set_state(port, Ch, true);
            
        }

        public override void CloseLightChannel(int Channel)
        {
            if (Channel < 0 || Channel > 3) return;
            Byte Ch = Convert.ToByte(Channel);
            bool State = false;
            bool rlt = true;
            rlt = mento_Controller.Get_state(port, Ch, ref State);
            if (!rlt) return;
            if (State) return;
            rlt = mento_Controller.Set_state(port, Ch, false);
        }



    }
}
