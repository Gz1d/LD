using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LD.Config
{
  public  class ConfigPinReport : Configuration
    {

        private static string ConfigName = string.Format(@"config\{0}","PinReport.xml");

        private static string ConfigName1 = string.Format(@"config\{0}", "InspectReport.xml");
        public void Save(string posNumObj, string linkNumObj, string maxDists)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigName);
            XmlNode root = xmlDoc.SelectSingleNode("pinreport");//查找<bookstore>
            XmlElement xe1 = xmlDoc.CreateElement("InspectResult");//创建一个<book>节点
            //xe1.SetAttribute("genre", "产品");//设置该节点genre属性
            xe1.SetAttribute("时间", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));//设置该节点ISBN属性

            XmlElement xesub1 = xmlDoc.CreateElement("工艺");
            xesub1.InnerText = "Pin针偏移检测";//设置文本节点
            xe1.AppendChild(xesub1);//添加到<book>节点中


            XmlElement xesub2 = xmlDoc.CreateElement("产品编号");
            xesub2.InnerText = posNumObj;
            xe1.AppendChild(xesub2);

            XmlElement xesub3 = xmlDoc.CreateElement("产品工位");
            xesub3.InnerText = linkNumObj;
            xe1.AppendChild(xesub3);

            XmlElement xesub4 = xmlDoc.CreateElement("最大偏移量");
            xesub4.InnerText = maxDists;
            xe1.AppendChild(xesub4);

            root.AppendChild(xe1);//添加到<bookstore>节点中
            xmlDoc.Save(ConfigName);

        }


        public void Save1(string ProductName, string linkNumObj, string maxDists)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigName);
            XmlNode root = xmlDoc.SelectSingleNode("pinreport");//查找<bookstore>
            XmlElement xe1 = xmlDoc.CreateElement("InspectResult");//创建一个<book>节点
            //xe1.SetAttribute("genre", "产品");//设置该节点genre属性
            xe1.SetAttribute("时间", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));//设置该节点ISBN属性

            XmlElement xesub1 = xmlDoc.CreateElement("工艺");
            xesub1.InnerText = "Pin针偏移检测";//设置文本节点
            xe1.AppendChild(xesub1);//添加到<book>节点中


            XmlElement xesub2 = xmlDoc.CreateElement("产品名称");
            xesub2.InnerText = ProductName;
            xe1.AppendChild(xesub2);

            XmlElement xesub3 = xmlDoc.CreateElement("产品工位");
            xesub3.InnerText = linkNumObj;
            xe1.AppendChild(xesub3);

            XmlElement xesub4 = xmlDoc.CreateElement("最大偏移量");
            xesub4.InnerText = maxDists;
            xe1.AppendChild(xesub4);

            root.AppendChild(xe1);//添加到<bookstore>节点中
            xmlDoc.Save(ConfigName);
        }



        public void SemiDataSave(string ProductName, string OffSetX, string OffSetY, string OffSetAngle, string GlueArea, string GlueWidth, 
            string GlueHight)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigName1);

            XmlNode root = xmlDoc.SelectSingleNode("SemiMsgReport");//查找<bookstore>

            XmlElement xe1 = xmlDoc.CreateElement("InspectResult");//创建一个<book>节点
            xe1.SetAttribute("时间", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));//设置该节点ISBN属性
                                                                                 //xe1.SetAttribute("genre", "产品");//设置该节点genre属
            XmlElement xesub1 = xmlDoc.CreateElement("工艺");
            xesub1.InnerText = "芯片绑定工艺检测";//设置文本节点
            xe1.AppendChild(xesub1);//添加到<book>节点中

            XmlElement xesub2 = xmlDoc.CreateElement("产品名称");
            xesub2.InnerText = ProductName;
            xe1.AppendChild(xesub2);

            XmlElement xesub4 = xmlDoc.CreateElement("X偏移");
            xesub4.InnerText = OffSetX;
            xe1.AppendChild(xesub4);

            XmlElement xesub5 = xmlDoc.CreateElement("Y偏移");
            xesub5.InnerText = OffSetY;
            xe1.AppendChild(xesub5);


            XmlElement xesub6 = xmlDoc.CreateElement("角度偏移");
            xesub6.InnerText = OffSetAngle;
            xe1.AppendChild(xesub6);

            XmlElement xesub7 = xmlDoc.CreateElement("胶水面积");
            xesub7.InnerText = GlueArea;
            xe1.AppendChild(xesub7);

            XmlElement xesub8 = xmlDoc.CreateElement("胶水宽");
            xesub8.InnerText = GlueWidth;
            xe1.AppendChild(xesub8);

            XmlElement xesub9 = xmlDoc.CreateElement("胶水高");
            xesub9.InnerText = GlueHight;
            xe1.AppendChild(xesub9);

            root.AppendChild(xe1);//添加到<bookstore>节点中
            xmlDoc.Save(ConfigName1);
        }
        //public int ClassNumberMax
        //{
        //    set;
        //    get;
        //}

        //public string Class
        //{
        //    set;
        //    get;
        //}

        //public int AcquTime
        //{
        //    set;
        //    get;
        //}
        ///// <summary>
        ///// 报表列表
        ///// </summary>
        //public BindingList<Config.report> ReportItems
        //{
        //    set;
        //    get;
        //}


        ///// <summary>
        ///// 保存设置
        ///// </summary>
        //public void Save()
        //{
        //    try
        //    {
        //        Serializition.SaveToFile(this, ConfigName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //}

        ///// <summary>
        ///// 加载配置
        ///// </summary>
        ///// <returns></returns>
        //public static ConfigReport Load()
        //{
        //    try
        //    {
        //        ConfigReport obj = (ConfigReport)Serializition.LoadFromFile(typeof(ConfigReport), ConfigName);
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new LoadException(ConfigName, ex.Message);
        //    }
        //}

    }
}
