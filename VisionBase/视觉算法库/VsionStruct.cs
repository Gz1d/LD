using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionBase
{
    public enum En_ImageProcessResult
    {
        OK = 0,
        未知NG,
        复检NG,
        相机采集超时,
        图片处理超时,
        模板匹配失败,
        模板匹配度低与设定值,       
        检测有胶带,
        找线失败,
        胶带供料位置超出限定范围,
        直线夹角过大,
        线数量和示教不一致,
        找线Rake1返回false,
        直线定位出错坐标非数字,
        直线拟合失败,
        圆半径和理论值偏差过大,
        圆半径超出限定值,
        圆的数量少于示教的数量,
        找圆定位出错坐标非数字,
        找圆失败,
        产品三号位置弯曲,

    }

   public struct VectorAngle
   {
       public double X;
       public double Y;
       public double Angle;
       public VectorAngle(bool isInit = true)
       {
           X = 0;
           Y = 0;
           Angle = 0;
       }
       public VectorAngle(double X0,double Y0,double Th0  )
       {
           X = X0;
           Y = Y0;
           Angle = Th0;
       }
   }

  public struct Point2Db
  {
      public double  Row;
      public double  Col;
      public Point2Db (bool isInit = true)
      {
          Row = 0;
          Col = 0;
      }

      public Point2Db(double ColIn,double RowIn )
      {
          Row = RowIn;
          Col = ColIn;
      
      
      }
      public static Point2Db operator +(Point2Db Pt2D1, Point2Db Pt2D2)
      {
          Point2Db aPt2D = new Point2Db(true);
          aPt2D.Col = Pt2D1.Col + Pt2D2.Col;
          aPt2D.Row = Pt2D1.Row + Pt2D2.Row;
          return aPt2D;
      }
      public static Point2Db operator -(Point2Db Pt2D1, Point2Db Pt2D2)
      {
          Point2Db aPt2D = new Point2Db(true);
          aPt2D.Col = Pt2D1.Col - Pt2D2.Col;
          aPt2D.Row = Pt2D1.Row - Pt2D2.Row;
          return aPt2D;
      }
  }

  public struct Point3Db
  {
      public Point3Db(double ptX, double ptY, double ptZ)
      {
          x = ptX;
          y = ptY;
          angle = ptZ;
      }

      public Point2Db Pt2D
      {
          get { return new Point2Db(x, y); }
      }

      public double x;
      public double y;
      public double angle;

      public static Point3Db operator +(Point3Db pt1, Point3Db pt2)
      {
          Point3Db pt3 = new Point3Db();

          pt3.x = pt1.x + pt2.x;
          pt3.y = pt1.y + pt2.y;
          pt3.angle = pt1.angle + pt2.angle;

          return pt3;
      }

      public static Point3Db operator -(Point3Db pt1, Point3Db pt2)
      {
          Point3Db pt3 = new Point3Db();

          pt3.x = pt1.x - pt2.x;
          pt3.y = pt1.y - pt2.y;
          pt3.angle = pt1.angle - pt2.angle;

          return pt3;
      }

      public static bool Equal(Point3Db pt1, Point3Db pt2)
      {
          return pt1.x == pt2.x && pt1.y == pt2.y && pt1.angle == pt2.angle;
      }
  }

  public class St_Position
  {
      public double X;
      public double Y;
      public double CCDX;
      public double Z;
      public double R;
      public const int AxisCount = 5;

      public St_Position()
      {
          X = 0;
          Y = 0;
          CCDX = 0;
          R = 0;
          Z = 0;
      }
  }

  public struct ST_UpCamMarkPos
  {
      public Point2Db  MarkPt2D1;
      public Point2Db  MarkPt2D2;

      public ST_UpCamMarkPos(bool isInit = true)
      {
          MarkPt2D1 = new Point2Db();
          MarkPt2D2 = new Point2Db();

      }

      public ST_UpCamMarkPos(Point2Db Pt1, Point2Db Pt2)
      {
          MarkPt2D1 = Pt1;
          MarkPt2D2 = Pt2;
      }

  }

  public struct ST_MarkPara
  {
      public double MarkR;
      public double MaxGray;
      public double MinGray;
      public int  ExposValue;
      public ST_MarkPara(bool isInit = true)
      {
          MarkR = 0;
          MaxGray = 0;
          MinGray = 0;
          ExposValue = 0;
      }
      public ST_MarkPara(double MarkR0,double  MinGray0,double  MaxGray0,int ExposValue0  )
      {
          MarkR = MarkR0;
          MaxGray = MaxGray0;
          MinGray = MinGray0;
          ExposValue = ExposValue0;
      } 
  }


    public struct ST_OffSet
    {
        public double X;
        public double Y;
        public double Theta;
        public ST_OffSet(bool isInit =true)
        {
            X=0;
            Y=0;
            Theta =0;           
        }
        public  ST_OffSet(double OffSetX,double OffSetY,double OffSetTheta)
        {
           X= OffSetX;
           Y =OffSetY;
           Theta = OffSetTheta;
        }
    }

  public struct MyHomMat2D
  {
      public double c00;
      public double c01;
      public double c02;
      public double c10;
      public double c11;
      public double c12;
      public MyHomMat2D(bool isInit = true)
      {
          c00 = 1;
          c01 = 0;
          c02 = 0;
          c10 = 0;
          c11 = 1;
          c12 = 0;
      }
        public override string ToString()
        {
            string str = "";
            str = c00.ToString("f4") + "   " + c01.ToString("f4") + "   " + c02.ToString("f4") + "   " +
                  c10.ToString("f4") + "   " + c11.ToString("f4") + "   " + c12.ToString("f4") + "   ";
            return str;                           
        }

  }
    [Serializable]
  public struct St_VectorAngle
  {
      public double   Row;
      public double   Col;
      public double   Angle;
      public St_VectorAngle(bool isInit = true)
      {
          Row = 0;
          Col = 0;
          Angle = 0;     
      }
      public St_VectorAngle(double row,double col,double angle)    
      {
          Row = row;
          Col = col;
          Angle = angle;
      }
  
  }

  public struct St_Rectangle2
  {
     public  double Row;
     public double Col;
     public double Angle;
     public double Width;
     public double Height;
     public St_Rectangle2(bool isInit =true )
     {
         Row = 0;
         Col = 0;
         Angle = 0;
         Width = 0;
         Height = 0;     
     }
     public St_Rectangle2( double row,double col,double angle,double width,double height  )
     {
         Row = row;
         Col = col;
         Angle =angle;
         Width = width;
         Height = height;      
     }


  }

}
