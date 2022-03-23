using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VisionBase.Config
{
    public  class TestPara
    {


        public TestPara()
        {
            this.MachineID = "01";
            this.StationID = "";
            this.DevName = "PLC01";
            this.ProjectVisionItem = ProjectVisionEnum.ProjectVision0;
            this.ProjectVisionName = "";
            this.CoordiEnumItem = CoordiEmum.Coordi0;
        }

        public string MachineID { set; get; }


        public string StationID
        {
            set;
            get;
        }


        public string DevName
        {
            set;
            get;
        }


        public ProjectVisionEnum ProjectVisionItem
        {
            set;
            get;

        }
        public string ProjectVisionName
        {
            set;
            get;

        }
        public CoordiEmum CoordiEnumItem
        {
            set;
            get;

        }

        public ST_OffSet MyOffSET
        {
            set;
            get;

        }

    }
}
