using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LD
{
    public static class Extendedattribute
    {

        
        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type GetTypeByName(this string typeName)
        {
            Type type = null;
            Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            int assemblyArrayLength = assemblyArray.Length;
            for (int i = 0; i < assemblyArrayLength; ++i)
            {
                type = assemblyArray[i].GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            for (int i = 0; (i < assemblyArrayLength); ++i)
            {
                Type[] typeArray = assemblyArray[i].GetTypes();
                int typeArrayLength = typeArray.Length;
                for (int j = 0; j < typeArrayLength; ++j)
                {
                    if (typeArray[j].Name.Equals(typeName))
                    {
                        return typeArray[j];
                    }
                }
            }
            
            return type;
        }
    }


    public class Common
    {
        /// <summary>
        /// Plc离线
        /// </summary>
        public const bool IsPlcOffline = false;
        /// <summary>
        /// 
        /// </summary>
        public const bool IsUseMes = true;
        /// <summary>
        /// 使用mes
        /// </summary>
        public const bool IsScannerOffline = false;

        /// <summary>
        /// 图像捕获
        /// </summary>
        /// <param name="image"></param>
        //public delegate void GrabImageDelegate(Config.Cameradevice device, HalconDotNet.HObject image);

        //public delegate void GetHalCtrlDelegate(out HalROIs.HalROIsCtrl halCtrl);


        /// <summary>
        /// 刷新界面
        /// </summary>
        /// <param name="type">操作类型  load ,clear,update</param>
        /// <param name="obj">对象</param>
        /// <param name="gcItems">画笔</param>
        /// <param name="tag">标签</param>
        //public delegate void PaintImageDelegate(Config.VisionItem vision, Common.LoadType type, object obj = null, System.Collections.Hashtable gcItems = null, Object tag = null);
        

        /// <summary>
        /// halcon控件加载
        /// </summary>
        public enum LoadType
        {
            /// <summary>
            /// 清除
            /// </summary>
            clear = 0,
            /// <summary>
            /// 加载
            /// </summary>
            load = 1,
            /// <summary>
            /// 保存原图
            /// </summary>
  
            save_bmp,
            /// <summary>
            /// 保存结果图
            /// </summary>
            save_jpg,

            /// <summary>
            /// 更新
            /// </summary>
            add,

            /// <summary>
            /// 其他
            /// </summary>
            other,

        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName, string assemblyName)
        {
            string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
            Type o = Type.GetType(path);//加载类型
            object obj = Activator.CreateInstance(o, true);//根据类型创建实例
            return (T)obj;//类型转换并返回
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }


        /// <summary>
        /// 获取枚举值上的Description特性的说明
        /// </summary>
        /// <param name="obj">枚举值</param>
        /// <returns>特性的说明</returns>
        public string GetEnumDescription(object obj)
        {
            var type = obj.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, obj));
            DescriptionAttribute descAttr =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
            {
                return string.Empty;
            }
            return descAttr.Description;
        }


        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName"></param>
        public static void KillProcess(string processName)
        {
            try
            {
                string comand = string.Format("taskkill /im {0} /f ", processName);
                RunCmd(comand);
            }
            catch { }
        }

        /// <summary>
        /// 运行DOS CMD 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string RunCmd(string command)
        {
            //實例一個Process類，啟動一個獨立進程   
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：   

            p.StartInfo.FileName = "cmd.exe";           //設定程序名   
            p.StartInfo.Arguments = "/c " + command;    //設定程式執行參數   
            p.StartInfo.UseShellExecute = false;        //關閉Shell的使用   
            p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入   
            p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出   
            p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出   
            p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口   

            p.Start();   //啟動   

            //p.StandardInput.WriteLine(command);       //也可以用這種方式輸入要執行的命令   
            //p.StandardInput.WriteLine("exit");        //不過要記得加上Exit要不然下一行程式執行的時候會當機   

            return p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果   

        }

        /// <summary>
        /// 十六进制转换字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = "" + s.Trim ();
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// 字节数组转换十六进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            if (data == null)
                data = new byte[0];
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper().Trim();
        }           


        /// <summary>
        /// 序列化成XML串
        /// </summary>
        /// <param name="entity">要序列化的对象</param>
        /// <returns>XML串</returns>
        public static string SerializeXml(object entity)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = false;
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.GetEncoding("gb2312");

            XmlWriter writer = XmlWriter.Create(sb, settings);

            // writer.Settings.Encoding = System.Text.Encoding.GetEncoding("gb2312");

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            XmlSerializer ser = new XmlSerializer(entity.GetType());
            ser.Serialize(writer, entity, namespaces);
            writer.Close();
            return sb.ToString().Replace("utf-16", "gb2312");
        }

        /// <summary>
        /// 序列化成XML文件
        /// </summary>
        /// <param name="entity">要序列化的对象</param>
        /// <param name="path">路径</param>
        public static void SerializeXml(object entity, string path)
        {
            XmlSerializer ser = new XmlSerializer(entity.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = false;
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.GetEncoding("gb2312");

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);


            XmlWriter writer = XmlWriter.Create(path , settings);
            ser.Serialize(writer, entity, namespaces);
            writer.Close();

            //StreamWriter sw = new StreamWriter(path);
            //ser.Serialize(sw, entity);
            //sw.Close();
        }

        /// <summary>
        /// 返序列化
        /// </summary>
        /// <param name="xmlData">序列化后的xml串</param>
        /// <returns>返回对象</returns>
        public static T XmlDeserialize<T>(string xmlData)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            TextReader reader = new StringReader(xmlData);
            T entity = (T)ser.Deserialize(reader);
            reader.Close();
            return entity;
        }

        /// <summary>
        /// 返序列化
        /// </summary>
        /// <param name="path">xml文件</param>
        /// <returns>返回对象</returns>
        public static T XmlDeserializeFromFile<T>(string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            TextReader reader = new StreamReader(path,System.Text.Encoding.GetEncoding("gb2312"));
            T entity = (T)ser.Deserialize(reader);
            reader.Close();
            return entity;
        }

        /// <summary>
        /// 查子在父的起始位
        /// </summary>
        /// <param name="father"></param>
        /// <param name="son"></param>
        /// <returns></returns>
        public static int FindSonPositionFromBytes(byte[] father, byte[] son)
        {
            int l = son.Length;
            int z = father.Length - l + 1;
            for (int i = 0; i <= z; i++)
            {
                byte[] tmp = new byte[l];
                Array.Copy(father, i, tmp, 0, l);
                if (tmp == son)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 查子在父的起始位
        /// </summary>
        /// <param name="father"></param>
        /// <param name="son"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int FindSonPositionFromBytes(string father, string son,int start)
        {
            int position = father.IndexOf(son,start);
            if (position >= 0)
            {
                if ((position%2) == 0)
                {
                    return position/2;
                }
                else
                {
                    return FindSonPositionFromBytes(father, son, position + 1);
                }

            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 取byte[]中的段
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetSubData(byte[] data, int startIndex, int length)
        {
            byte[] ret = new byte[length];
            Array.Copy(data, startIndex, ret, 0, length);
            return ret;
        }

        /// <summary>
        /// 序列化成二进制
        /// </summary>
        /// <param name="entity">要序列化的对象</param>
        /// <value>返回二进制串</value>
        public static byte[] SerializeBinary(object entity)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, entity);
            return ms.ToArray();
        }

        /// <summary>
        /// 返序列化
        /// </summary>
        /// <param name="bytes">序列化后的二进制数组</param>
        /// <returns>返回对象</returns>
        public static object DeserializeBinary(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(ms);
        }



        /// <summary>  
        /// 是否能 Ping 通指定的主机  
        /// </summary>  
        /// <param name="ip">ip 地址或主机名或域名</param>  
        /// <returns>true 通，false 不通</returns>  
        public static  bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒  
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }  

        /// <summary>
        /// socket客户端接收
        /// </summary>
        /// <param name="device"></param>
        /// <param name="dataB"></param>
        /// <param name="dataS"></param>
        public delegate void SocketReceiveDelegate(string device,byte[] dataB,string dataS);

        /// <summary>
        /// Opc设备枚举
        /// </summary>
        public enum OpcDevice
        {
            /// <summary>
            /// 错误
            /// </summary>
            ERROR = -1,

            /// <summary>
            /// 空
            /// </summary>
            NULL = 0,

            #region PING PC

            /// <summary>
            /// 连接状态
            /// </summary>
            PING_PC_Status,
            #endregion



        }

        /// <summary>
        /// SOCKET
        /// </summary>
        public enum SocketDevice
        {
            TEST = -1,
            NULL = 0,
            /// <summary>
            /// MES服务
            /// </summary>
            MES,
            /// <summary>
            /// ABB robot 01
            /// </summary>
            ABB01,
            /// <summary>
            /// ABB robot 02
            /// </summary>
            ABB02,
            /// <summary>
            /// ABB robot 03
            /// </summary>
            ABB03,
            /// <summary>
            /// ABB robot 04
            /// </summary>
            ABB04,

            /// <summary>
            /// 扫码枪01
            /// </summary>
            SCANER01,
            /// <summary>
            /// 扫码枪02
            /// </summary>
            SCANER02,

            /// <summary>
            /// 康耐德感应器
            /// </summary>
            DANNAD01,

            /// <summary>
            /// 康耐德感应器
            /// </summary>
            DANNAD02,

            /// <summary>
            /// 视觉（无用）
            /// </summary>
            VISION,

            DIAG

        }

        /// <summary>
        /// 串口
        /// </summary>
        public enum SerialDevice
        {
            TEST = -1,
            NULL=0,

            /// <summary>
            /// 扫码枪01
            /// </summary>
            SCANER01,
            /// <summary>
            /// 扫码枪02
            /// </summary>
            SCANER02,
            /// <summary>
            /// 扫码枪03
            /// </summary>
            SCANER03,
            /// <summary>
            /// 扫码枪04
            /// </summary>
            SCANER04,
            /// <summary>
            /// 扫码枪05
            /// </summary>
            SCANER05,
            /// <summary>
            /// 扫码枪06
            /// </summary>
            SCANER06,
            /// <summary>
            /// 扫码枪07
            /// </summary>
            SCANER07,
            /// <summary>
            /// 扫码枪08
            /// </summary>
            SCANER08,
            /// <summary>
            /// 扫码枪09
            /// </summary>
            SCANER09,
            /// <summary>
            /// 扫码枪10
            /// </summary>
            SCANER10,
            /// <summary>
            /// 扫码枪11
            /// </summary>
            SCANER11,
            /// <summary>
            /// 扫码枪12
            /// </summary>
            SCANER12,
            /// <summary>
            /// 光源二通道
            /// </summary>
            LIGHTT201,
            /// <summary>
            /// 光源二通道
            /// </summary>
            LIGHTT202,
            /// <summary>
            /// 光源四通道
            /// </summary>
            LIGHTT401,
            /// <summary>
            /// 光源四通道
            /// </summary>
            LIGHTT402,
            /// <summary>
            /// 切换器01
            /// </summary>
            KVM01,
            /// <summary>
            /// 切换器02
            /// </summary>
            KVM02,
            /// <summary>
            /// 传感器
            /// </summary>
            SENSOR01,
            /// <summary>
            /// 传感器
            /// </summary>
            SENSOR02,
            /// <summary>
            /// 传感器
            /// </summary>
            SENSOR03,
            /// <summary>
            /// 传感器
            /// </summary>
            SENSOR04,
            /// <summary>
            /// 称重设备
            /// </summary>
            XJC608T,
        }

        /// <summary>
        /// 视觉设备（拍照点）
        /// </summary>
        public enum VisionDevice
        {
            TEST = -1,
            NULL = 0,

            /// <summary>
            /// 拍照点01视觉
            /// </summary>
            VISION01,
            /// <summary>
            ///  拍照点02视觉
            /// </summary>
            VISION02,
            /// <summary>
            ///  拍照点03视觉
            /// </summary>
            VISION03,
            /// <summary>
            ///  拍照点04视觉
            /// </summary>
            VISION04,
            /// <summary>
            ///  拍照点05视觉
            /// </summary>
            VISION05,
            /// <summary>
            ///  拍照点06视觉
            /// </summary>
            VISION06,
            /// <summary>
            ///  拍照点07视觉
            /// </summary>
            VISION07,
            /// <summary>
            ///  拍照点08视觉
            /// </summary>
            VISION08,
            VISION09,
            VISION10,
            VISION11,
            VISION12,
            VISION13,
            VISION14,
            VISION15,
            VISION16,
            VISION17,
            VISION18,
            VISION19,
            VISION20,
        }

        public enum MatchKind
        {
            NULL=0,
            SHAPE,
            NCC,

        
        }

        /// <summary>
        /// Opc设备枚举
        /// </summary>
        public enum PlcDevice
        {
            /// <summary>
            /// 错误
            /// </summary>
            ERROR = -1,

            /// <summary>
            /// 空
            /// </summary>
            NULL = 0,

            ECD010_RTU_01,


            #region

            /// <summary>
            /// 触发拍照
            /// </summary>
            V_01_TriggerGrab,
            /// <summary> 拍照完成 </summary>
            V_01_GrabFinish,
            /// <summary>
            /// 视觉OK
            /// </summary>
            V_01_Vision_OK,

            /// <summary>
            /// 视觉完成
            /// </summary>
            V_01_VisionFinish,
            /// <summary> 视觉拍照工位编号地址 </summary>
            V_01_GrabNum,

            /// <summary>
            /// 拍照 X 坐标
            /// </summary>
            V_01_Grab_X,

            /// <summary>
            /// 拍照 Y 坐标
            /// </summary>
            V_01_Grab_Y,

            /// <summary>
            /// 拍照 X 坐标
            /// </summary>
            V_01_Grab_Z,

            /// <summary>
            /// 拍照 Y 坐标
            /// </summary>
            V_01_Grab_R,

            /// <summary>
            /// 结果 X 坐标
            /// </summary>
            V_01_Result_X,

            /// <summary>
            /// 结果 Y 坐标
            /// </summary>
            V_01_Result_Y,

            /// <summary>
            /// 结果 R 角度
            /// </summary>
            V_01_Result_R,

            /// <summary>
            /// 结果 X 补偿
            /// </summary>
            V_01_Offset_X,

            /// <summary>
            /// 结果 Y 补偿
            /// </summary>
            V_01_Offset_Y,

            /// <summary>
            /// 结果 R 补偿
            /// </summary>
            V_01_Offset_R,

            /// <summary>
            /// 触发拍照
            /// </summary>
            V_02_TriggerGrab,

            /// <summary> 拍照完成 </summary>
            V_02_GrabFinish,
            /// <summary>
            /// 视觉OK
            /// </summary>
            V_02_Vision_OK,

            /// <summary>
            /// 视觉完成
            /// </summary>
            V_02_VisionFinish,


            /// <summary> 视觉拍照工位编号地址 </summary>
            V_02_GrabNum,
            /// <summary>
            /// 拍照 X 坐标
            /// </summary>
            V_02_Grab_X,

            /// <summary>
            /// 拍照 Y 坐标
            /// </summary>
            V_02_Grab_Y,


            /// <summary>
            /// 拍照 X 坐标
            /// </summary>
            V_02_Grab_Z,

            /// <summary>
            /// 拍照 Y 坐标
            /// </summary>
            V_02_Grab_R,


            /// <summary>
            /// 结果 X 坐标
            /// </summary>
            V_02_Result_X,

            /// <summary>
            /// 结果 Y 坐标
            /// </summary>
            V_02_Result_Y,

            /// <summary>
            /// 结果 R 角度
            /// </summary>
            V_02_Result_R,

            /// <summary>
            /// 结果 X 补偿
            /// </summary>
            V_02_Offset_X,

            /// <summary>
            /// 结果 Y 补偿
            /// </summary>
            V_02_Offset_Y,

            /// <summary>
            /// 结果 R 补偿
            /// </summary>
            V_02_Offset_R,

            /// <summary>
            /// 触发拍照
            /// </summary>
            V_03_TriggerGrab,

            /// <summary> 拍照完成 </summary>
            V_03_GrabFinish,
            /// <summary>
            /// 视觉OK
            /// </summary>
            V_03_Vision_OK,

            /// <summary>
            /// 视觉完成
            /// </summary>
            V_03_VisionFinish,


            /// <summary> 视觉拍照工位编号地址 </summary>
            V_03_GrabNum,
            /// <summary>
            /// 拍照 X 坐标
            /// </summary>
            V_03_Grab_X,

            /// <summary>
            /// 拍照 Y 坐标
            /// </summary>
            V_03_Grab_Y,

            /// <summary>
            /// 结果 X 坐标
            /// </summary>
            V_03_Result_X,

            /// <summary>
            /// 结果 Y 坐标
            /// </summary>
            V_03_Result_Y,

            /// <summary>
            /// 结果 R 角度
            /// </summary>
            V_03_Result_R,

            /// <summary> 结果 X 补偿 </summary>
            V_03_Offset_X,
            /// <summary> 结果 Y 补偿 </summary>
            V_03_Offset_Y,
            /// <summary> 结果 R 补偿 </summary>
            V_03_Offset_R,


            /// <summary> 触发拍照 </summary>
            V_04_TriggerGrab,
            /// <summary> 拍照完成 </summary>
            V_04_GrabFinish,
            /// <summary> 视觉OK </summary>
            V_04_Vision_OK,
            /// <summary>视觉完成</summary>
            V_04_VisionFinish,
            /// <summary> 视觉拍照工位编号地址 </summary>
            V_04_GrabNum,
            /// <summary> 拍照 X 坐标</summary>
            V_04_Grab_X,
            /// <summary> 拍照 Y 坐标 </summary>
            V_04_Grab_Y,
            /// <summary> 结果 X 坐标 </summary>
            V_04_Result_X,
            /// <summary> 结果 Y 坐标 /// </summary>
            V_04_Result_Y,
            /// <summary> 结果 R 角度 </summary>
            V_04_Result_R,
            /// <summary> 结果 X 补偿 </summary>
            V_04_Offset_X,
            /// <summary> 结果 Y 补偿 </summary>
            V_04_Offset_Y,
            /// <summary> 结果 R 补偿 </summary>
            V_04_Offset_R,


            /// <summary> 触发拍照 </summary>
            V_05_TriggerGrab,
            /// <summary> 拍照完成 </summary>
            V_05_GrabFinish,
            /// <summary> 视觉OK </summary>
            V_05_Vision_OK,
            /// <summary>视觉完成</summary>
            V_05_VisionFinish,
            /// <summary> 视觉拍照工位编号地址 </summary>
            V_05_GrabNum,
            /// <summary> 拍照 X 坐标</summary>
            V_05_Grab_X,
            /// <summary> 拍照 Y 坐标 </summary>
            V_05_Grab_Y,
            /// <summary> 结果 X 坐标 </summary>
            V_05_Result_X,
            /// <summary> 结果 Y 坐标 /// </summary>
            V_05_Result_Y,
            /// <summary> 结果 R 角度 </summary>
            V_05_Result_R,
            /// <summary> 结果 X 补偿 </summary>
            V_05_Offset_X,
            /// <summary> 结果 Y 补偿 </summary>
            V_05_Offset_Y,
            /// <summary> 结果 R 补偿 </summary>
            V_05_Offset_R,

            /// <summary> 触发拍照 </summary>
            V_06_TriggerGrab,
            /// <summary> 拍照完成 </summary>
            V_06_GrabFinish,
            /// <summary> 视觉OK </summary>
            V_06_Vision_OK,
            /// <summary>视觉完成</summary>
            V_06_VisionFinish,
            /// <summary> 视觉拍照工位编号地址 </summary>
            V_06_GrabNum,
            /// <summary> 拍照 X 坐标</summary>
            V_06_Grab_X,
            /// <summary> 拍照 Y 坐标 </summary>
            V_06_Grab_Y,
            /// <summary> 结果 X 坐标 </summary>
            V_06_Result_X,
            /// <summary> 结果 Y 坐标 /// </summary>
            V_06_Result_Y,
            /// <summary> 结果 R 角度 </summary>
            V_06_Result_R,
            /// <summary> 结果 X 补偿 </summary>
            V_06_Offset_X,
            /// <summary> 结果 Y 补偿 </summary>
            V_06_Offset_Y,
            /// <summary> 结果 R 补偿 </summary>
            V_06_Offset_R,


            /// <summary> 触发拍照 </summary>
            V_07_TriggerGrab,
            /// <summary> 拍照完成 </summary>
            V_07_GrabFinish,
            /// <summary> 视觉OK </summary>
            V_07_Vision_OK,
            /// <summary>视觉完成</summary>
            V_07_VisionFinish,
            /// <summary> 视觉拍照工位编号地址 </summary>
            V_07_GrabNum,
            /// <summary> 拍照 X 坐标</summary>
            V_07_Grab_X,
            /// <summary> 拍照 Y 坐标 </summary>
            V_07_Grab_Y,
            /// <summary> 结果 X 坐标 </summary>
            V_07_Result_X,
            /// <summary> 结果 Y 坐标 /// </summary>
            V_07_Result_Y,
            /// <summary> 结果 R 角度 </summary>
            V_07_Result_R,
            /// <summary> 结果 X 补偿 </summary>
            V_07_Offset_X,
            /// <summary> 结果 Y 补偿 </summary>
            V_07_Offset_Y,
            /// <summary> 结果 R 补偿 </summary>
            V_07_Offset_R,

            /// <summary> 触发拍照 </summary>
            V_08_TriggerGrab,
            /// <summary> 拍照完成 </summary>
            V_08_GrabFinish,
            /// <summary> 视觉OK </summary>
            V_08_Vision_OK,
            /// <summary>视觉完成</summary>
            V_08_VisionFinish,
            /// <summary> 视觉拍照工位编号地址 </summary>
            V_08_GrabNum,
            /// <summary> 拍照 X 坐标</summary>
            V_08_Grab_X,
            /// <summary> 拍照 Y 坐标 </summary>
            V_08_Grab_Y,
            /// <summary> 结果 X 坐标 </summary>
            V_08_Result_X,
            /// <summary> 结果 Y 坐标 /// </summary>
            V_08_Result_Y,
            /// <summary> 结果 R 角度 </summary>
            V_08_Result_R,
            /// <summary> 结果 X 补偿 </summary>
            V_08_Offset_X,
            /// <summary> 结果 Y 补偿 </summary>
            V_08_Offset_Y,
            /// <summary> 结果 R 补偿 </summary>
            V_08_Offset_R,

            /// <summary> 触发拍照 </summary>
            V_09_TriggerGrab,
            /// <summary> 拍照完成 </summary>
            V_09_GrabFinish,
            /// <summary> 视觉OK </summary>
            V_09_Vision_OK,
            /// <summary>视觉完成</summary>
            V_09_VisionFinish,
            /// <summary> 视觉拍照工位编号地址 </summary>
            V_09_GrabNum,
            /// <summary> 拍照 X 坐标</summary>
            V_09_Grab_X,
            /// <summary> 拍照 Y 坐标 </summary>
            V_09_Grab_Y,
            /// <summary> 结果 X 坐标 </summary>
            V_09_Result_X,
            /// <summary> 结果 Y 坐标 /// </summary>
            V_09_Result_Y,
            /// <summary> 结果 R 角度 </summary>
            V_09_Result_R,
            /// <summary> 结果 X 补偿 </summary>
            V_09_Offset_X,
            /// <summary> 结果 Y 补偿 </summary>
            V_09_Offset_Y,
            /// <summary> 结果 R 补偿 </summary>
            V_09_Offset_R,


            #endregion
            /// <summary> 产品名字 </summary>
            ProductName,
            /// <summary>产品的编号</summary>
            ProductNum,

            /// <summary>
            ///链接器编号
            /// </summary>
            LinkerNum,
            MaxDistance,
            OpenLight,//1030zxh

            /// <summary> Art测试相机开始拍照 </summary>
            ArtTestCameraStart,
            /// <summary> Art测试相机拍照完成 </summary>
            ArtTestCameraGrabed,
            /// <summary> Art相机拍照定位结果</summary>
            ArtTestCamLocalResult,
            /// <summary> Art测试台编号 </summary>
            ArtTestStageNumRead,
            /// <summary> Art测试台拍照位编号</summary> 
            ArtTestGrabNum,
            /// <summary> 示教产品移到相机视野中，定位计算出的坐标X </summary>
            ArtTeachProductX,
            /// <summary> 示教产品移到相机视野中，定位计算出的坐标Y  </summary>
            ArtTeachProductY,
            /// <summary> 示教产品移到相机视野中，定位计算出的坐标Theta </summary>
            ArtTeachProductTheta,
            /// <summary> 定位后X轴补偿值</summary>
            ArtTestAddX,
            /// <summary> 定位后Y轴补偿值 </summary>
            ArtTestAddY,
            /// <summary> 定位后Y轴补偿值 </summary>
            ArtTestAddTheta,
            /// <summary> 定位后测试台编号写入值</summary>
            ArtTestStageNumWrite,

            /// <summary>  开始偏移检测 </summary>
            FOF_start_insp,
            /// <summary> 拍照完成</summary>
            FOF_Grabed_ok,
            /// <summary>拍照结果 </summary>
            FOF_Grabed_result,
            /// <summary>检测结果</summary>
            FOF_inspect_result,
            /// <summary> FOF_X偏移量 </summary>
            FOF_offset_x,
            /// <summary> FOF_Y偏移量 </summary>
            FOF_offset_y,
            /// <summary> FOF_Z偏移量 </summary>
            FOF_offset_z,

            /// <summary> 坐标系0的X轴</summary>
            Coordi0_X ,
            /// <summary> 坐标系0的Y轴</summary>
            Coordi0_Y,
            /// <summary> 坐标系0的Z轴</summary>
            Coordi0_Z,
            /// <summary> 坐标系0的Theta轴</summary>
            Coordi0_Theta,

            /// <summary> 设置坐标系0的X轴的坐标</summary>
            Coordi0_X1,
            /// <summary> 设置坐标系0的Y轴的坐标<</summary>
            Coordi0_Y1,
            /// <summary> 设置坐标系0的Z轴的坐标<</summary>
            Coordi0_Z1,
            /// <summary> 设置坐标系0的Theta轴的坐标<</summary>
            Coordi0_Theta1,

            /// <summary> 坐标系1的X轴</summary>
            Coordi1_X,
            /// <summary> 坐标系1的Y轴</summary>
            Coordi1_Y,
            /// <summary> 坐标系1的Z轴</summary>
            Coordi1_Z,
            /// <summary> 坐标系1的Theta轴</summary>
            Coordi1_Theta,

            /// <summary> 坐标系0的X轴</summary>
            Coordi1_X1,
            /// <summary> 坐标系0的Y轴</summary>
            Coordi1_Y1,
            /// <summary> 坐标系0的Z轴</summary>
            Coordi1_Z1,
            /// <summary> 坐标系0的Theta轴</summary>
            Coordi1_Theta1,

            /// <summary> 坐标系2的X轴</summary>
            Coordi2_X,
            /// <summary> 坐标系2的Y轴</summary>
            Coordi2_Y,
            /// <summary> 坐标系2的Z轴</summary>
            Coordi2_Z,
            /// <summary> 坐标系2的Theta轴</summary>
            Coordi2_Theta,

            /// <summary> 坐标系0的X轴</summary>
            Coordi2_X1,
            /// <summary> 坐标系0的Y轴</summary>
            Coordi2_Y1,
            /// <summary> 坐标系0的Z轴</summary>
            Coordi2_Z1,
            /// <summary> 坐标系0的Theta轴</summary>
            Coordi2_Theta1,

            /// <summary> 坐标系3的X轴</summary>
            Coordi3_X,
            /// <summary> 坐标系3的Y轴</summary>
            Coordi3_Y,
            /// <summary> 坐标系3的Z轴</summary>
            Coordi3_Z,
            /// <summary> 坐标系3的Theta轴</summary>
            Coordi3_Theta,
            /// <summary> 设置坐标系3的X轴的坐标</summary>
            Coordi3_X1,
            /// <summary> 设置坐标系3的Y轴的坐标<</summary>
            Coordi3_Y1,
            /// <summary> 设置坐标系3的Z轴的坐标<</summary>
            Coordi3_Z1,
            /// <summary> 设置坐标系3的Theta轴的坐标<</summary>
            Coordi3_Theta1,



            /// <summary> 坐标系4的X轴</summary>
            Coordi4_X,
            /// <summary> 坐标系4的Y轴</summary>
            Coordi4_Y,
            /// <summary> 坐标系4的Z轴</summary>
            Coordi4_Z,
            /// <summary> 坐标系4的Theta轴</summary>
            Coordi4_Theta,
            /// <summary> 设置坐标系4的X轴的坐标</summary>
            Coordi4_X1,
            /// <summary> 设置坐标系4的Y轴的坐标<</summary>
            Coordi4_Y1,
            /// <summary> 设置坐标系4的Z轴的坐标<</summary>
            Coordi4_Z1,
            /// <summary> 设置坐标系4的Theta轴的坐标<</summary>
            Coordi4_Theta1,


            /// <summary> 坐标系5的X轴</summary>
            Coordi5_X,
            /// <summary> 坐标系5的Y轴</summary>
            Coordi5_Y,
            /// <summary> 坐标系5的Z轴</summary>
            Coordi5_Z,
            /// <summary> 坐标系5的Theta轴</summary>
            Coordi5_Theta,
            /// <summary> 设置坐标系5的X轴的坐标</summary>
            Coordi5_X1,
            /// <summary> 设置坐标系5的Y轴的坐标<</summary>
            Coordi5_Y1,
            /// <summary> 设置坐标系5的Z轴的坐标<</summary>
            Coordi5_Z1,
            /// <summary> 设置坐标系5的Theta轴的坐标<</summary>
            Coordi5_Theta1,


            /// <summary> 坐标系6的X轴</summary>
            Coordi6_X,
            /// <summary> 坐标系6的Y轴</summary>
            Coordi6_Y,
            /// <summary> 坐标系6的Z轴</summary>
            Coordi6_Z,
            /// <summary> 坐标系6的Theta轴</summary>
            Coordi6_Theta,
            /// <summary> 设置坐标系6的X轴的坐标</summary>
            Coordi6_X1,
            /// <summary> 设置坐标系6的Y轴的坐标<</summary>
            Coordi6_Y1,
            /// <summary> 设置坐标系6的Z轴的坐标<</summary>
            Coordi6_Z1,
            /// <summary> 设置坐标系6的Theta轴的坐标<</summary>
            Coordi6_Theta1,


            /// <summary> 坐标系7的X轴</summary>
            Coordi7_X,
            /// <summary> 坐标系7的Y轴</summary>
            Coordi7_Y,
            /// <summary> 坐标系7的Z轴</summary>
            Coordi7_Z,
            /// <summary> 坐标系7的Theta轴</summary>
            Coordi7_Theta,
            /// <summary> 设置坐标系7的X轴的坐标</summary>
            Coordi7_X1,
            /// <summary> 设置坐标系7的Y轴的坐标<</summary>
            Coordi7_Y1,
            /// <summary> 设置坐标系7的Z轴的坐标<</summary>
            Coordi7_Z1,
            /// <summary> 设置坐标系7的Theta轴的坐标<</summary>
            Coordi7_Theta1,


            /// <summary> 坐标系8的X轴</summary>
            Coordi8_X,
            /// <summary> 坐标系8的Y轴</summary>
            Coordi8_Y,
            /// <summary> 坐标系8的Z轴</summary>
            Coordi8_Z,
            /// <summary> 坐标系8的Theta轴</summary>
            Coordi8_Theta,
            /// <summary> 设置坐标系8的X轴的坐标</summary>
            Coordi8_X1,
            /// <summary> 设置坐标系8的Y轴的坐标<</summary>
            Coordi8_Y1,
            /// <summary> 设置坐标系8的Z轴的坐标<</summary>
            Coordi8_Z1,
            /// <summary> 设置坐标系8的Theta轴的坐标<</summary>
            Coordi8_Theta1,

            /// <summary> 坐标系9的X轴</summary>
            Coordi9_X,
            /// <summary> 坐标系9的Y轴</summary>
            Coordi9_Y,
            /// <summary> 坐标系9的Z轴</summary>
            Coordi9_Z,
            /// <summary> 坐标系9的Theta轴</summary>
            Coordi9_Theta,
            /// <summary> 设置坐标系9的X轴的坐标</summary>
            Coordi9_X1,
            /// <summary> 设置坐标系9的Y轴的坐标<</summary>
            Coordi9_Y1,
            /// <summary> 设置坐标系9的Z轴的坐标<</summary>
            Coordi9_Z1,
            /// <summary> 设置坐标系9的Theta轴的坐标<</summary>
            Coordi9_Theta1,

            /// <summary>
            /// pin针的编号
            /// </summary>
            Pin1,
            Pin2,
            Pin3,
            Pin4,
            Pin5,
            Pin6,
            Pin7,
            Pin8,
            Pin9,
            Pin10,
            Pin11,
            Pin12,
            Pin13,
            Pin14,
            Pin15,
            Pin16,
            Pin17,
            Pin18,
            Pin19,
            Pin20,
            Pin21,
            Pin22,
            Pin23,
            Pin24,
            Pin25,
            Pin26,
            Pin27,
            Pin28,
            Pin29,
            Pin30,
            Pin31,
            Pin32,
            Pin33,
            Pin34,
            Pin35,
            Pin36,
            Pin37,
            Pin38,
            Pin39,
            Pin40,
            Pin41,
            Pin42,
            Pin43,
            Pin44,
            Pin45,
            Pin46,
            Pin47,
            Pin48,
            Pin49,
            Pin50,


        }







        //数据类型
        public enum DataTypes : short
        {
            Bool = 0,
            Byte = 1,
            Short = 2,
            Ushort = 3,
            Int = 4,
            UInt = 5,
            Long = 6,
            ULong = 7,
            Float = 8,
            Double = 9,
            String = 10,
            Coil,
            Discrete
        }

        //设备类型
        public enum DeviceType
        {
            S1200 = 1,
            S300 = 2,
            S400 = 3,
            S1500 = 4,
            S200Smart = 5,
            S200 = 6,
            ModbusTcp = 7,
            ModbusRtu = 8,
            Qseries = 9,
            Keyence = 10,
        }


    }
}
