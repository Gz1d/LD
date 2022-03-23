using System;
using HalconDotNet;
using System.Collections;
using System.Collections.Generic;


namespace ViewROI
{
    [Serializable]
    public class ROIPolygon2 : ROI
    {
        private double row0;
        private double col0;
        private HTuple row1;//¾ØÐÎ
        private HTuple col1;
        private HTuple row2;//Ô²
        private HTuple col2;
        private double minDistance;
        private double midR, midC;   // midpoint 

        /// <summary>Constructor</summary>
        public ROIPolygon2()
        {
            RoiName = "Polygon";
            this.activeHandleIdx = 0;
            this.row1 = new HTuple();
            this.col1 = new HTuple();
            this.row2 = new HTuple();
            this.col2 = new HTuple();
            this.minDistance = 0.0;
        }

        public void GetPolygon2(out double Outrow0, out double Outcol0 , out List<double> Outrow1,out List<double>  Outcol1 ,
            List<double> Outrow2 ,List<double> Outcol2  )
        {
            Outrow0 = this.row0;
            Outcol0 = this.col0;
            Outrow1 = new List<double>();
            Outcol1 = new List<double>();
            Outrow2 = new List<double>();
            Outcol2 = new List<double>();
            try
            {
                for (int i = 0; i < this.col1.TupleLength(); i++)
                {
                    Outcol1.Add(this.col1[i].D);
                    Outrow1.Add(this.row1[i].D);
                    Outcol2.Add(this.col2[i].D);
                    Outrow2.Add(this.row2[i].D);
                }                    
            }
            catch
            { }
        }

        /// <summary>Creates a new ROI instance at the mouse position</summary>
        /// <param name="midX">
        /// x (=column) coordinate for interactive ROI
        /// </param>
        /// <param name="midY">
        /// y (=row) coordinate for interactive ROI
        /// </param>
        public override void createROI(double midX, double midY)
        {
            midR = midY;
            midC = midX;
            this.row1 = null;
            this.col1 = null;
            this.row1 = new HTuple();
            this.col1 = new HTuple();
            this.row1 = this.row1.TupleInsert(0, midY - 100.0);
            this.row1 = this.row1.TupleInsert(0, midY - 100.0);
            this.row1 = this.row1.TupleInsert(0, midY + 100.0);
            this.row1 = this.row1.TupleInsert(0, midY + 100.0);
            this.col1 = this.col1.TupleInsert(0, midX - 100.0);
            this.col1 = this.col1.TupleInsert(0, midX + 100.0);
            this.col1 = this.col1.TupleInsert(0, midX + 100.0);
            this.col1 = this.col1.TupleInsert(0, midX - 100.0);
            this.updateRow2();
        }

        /// <summary>Paints the ROI into the supplied window</summary>
        /// <param name="window">HALCON window</param>
        public override void draw(HalconDotNet.HWindow window)
        {
            window.SetDraw("margin");
            double num = 5.0;
            for (int i = 0; i < this.row1.TupleLength(); i++) {
                if (i < this.row1.TupleLength() - 1){
                    window.DispLine(this.row1[i].D, this.col1[i].D, this.row1[i + 1].D, this.col1[i + 1].D);
                }
                else {
                    window.DispLine(this.row1[i].D, this.col1[i].D, this.row1[0].D, this.col1[0].D);
                }
                window.DispRectangle2(this.row1[i].D, this.col1[i].D, 0.0, num, num);
            }
            for (int j = 0; j < this.row2.TupleLength(); j++){
                window.DispCircle(this.row2[j].D, this.col2[j].D, num);
            }
            window.DispRectangle2(midR, midC, 0, 5, 5);
        }
        public void updateRow1(HTuple mRow, HTuple mCol)
        {
            if (mRow.TupleLength() == mCol.TupleLength() && mRow.TupleLength() > 2) {
                this.row1 = null;
                this.col1 = null;
                this.row1 = new HTuple();
                this.col1 = new HTuple();
                this.row1 = mRow.Clone();
                this.col1 = mCol.Clone();
                this.updateRow2();
            }
        }
        private void updateRow2()
        {
            if (this.row1.TupleLength() > 2 && this.col1.TupleLength() > 2 && this.row1.TupleLength() == this.col1.TupleLength()) {
                this.row2 = null;
                this.col2 = null;
                this.row2 = new HTuple();
                this.col2 = new HTuple();
                for (int i = 0; i < this.row1.TupleLength() - 1; i++) {
                    this.row2 = this.row2.TupleConcat((this.row1[i] + this.row1[i + 1]) / 2);
                    this.col2 = this.col2.TupleConcat((this.col1[i] + this.col1[i + 1]) / 2);
                }
                this.row2 = this.row2.TupleConcat((this.row1[0] + this.row1[this.row1.TupleLength() - 1]) / 2);
                this.col2 = this.col2.TupleConcat((this.col1[0] + this.col1[this.col1.TupleLength() - 1]) / 2);
            }
        }


        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y)
        /// </summary>
        /// <param name="x">x (=column) coordinate</param>
        /// <param name="y">y (=row) coordinate</param>
        /// <returns> 
        /// Distance of the closest ROI handle.
        /// </returns>
        public override double distToClosestHandle(double x, double y)
        {
            this.row0 = y;
            this.col0 = x;
            this.minDistance = 0.0;
            double[] array = new double[this.row1.TupleLength()];
            double[] array2 = new double[this.row2.TupleLength()];
            for (int i = 0; i < this.row1.TupleLength(); i++){
                array[i] = HMisc.DistancePp(y, x, this.row1[i].D, this.col1[i].D);
            }
            for (int j = 0; j < this.row2.TupleLength(); j++){
                array2[j] = HMisc.DistancePp(y, x, this.row2[j].D, this.col2[j].D);
            }
            double num = HMisc.DistancePp(this.row1[0].D, this.col1[0].D, this.row1[1].D, this.col1[1].D);
            for (int k = 1; k < this.row1.TupleLength() - 1; k++){
                double num2 = HMisc.DistancePp(this.row1[k].D, this.col1[k].D, this.row1[k + 1].D, this.col1[k + 1].D);
                if (num > num2){
                    num = num2;
                }
            }
            double num3 = (num / 10.0 < 3.0) ? 3.0 : (num / 10.0);
            double result=20000;
            for (int l = 0; l < array.Length; l++) {
                if (array[l] < num3){
                    num3 = array[l];
                    this.activeHandleIdx = l;
                }
            }
            for (int m = 0; m < array2.Length; m++) {
                if (array2[m] < num3) {
                    num3 = array2[m];
                    this.activeHandleIdx = this.row1.TupleLength() + m;
                }
            }
            double num4 = HMisc.DistancePp(y, x, midR, midC);
            if (num4 < num3){
                num3 = num4;
                this.activeHandleIdx = this.row1.TupleLength() + this.row2.TupleLength();
            }
            if (this.activeHandleIdx < this.row1.TupleLength()) {
                result = array[this.activeHandleIdx];
            }
            else if (this.activeHandleIdx < this.row1.TupleLength() + this.row2.TupleLength())
            {
                result = array2[this.activeHandleIdx - this.row1.TupleLength()];
            }
            else if (this.activeHandleIdx == this.row1.TupleLength() + this.row2.TupleLength())
            {
                result = num4;
            }
            return result;
        }

        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window
        /// </summary>
        /// <param name="window">HALCON window</param>
        public override void displayActive(HalconDotNet.HWindow window)
        {
            window.SetDraw("margin");
            double num = 5.0;
            if (this.activeHandleIdx < this.row1.TupleLength()) {
                window.DispRectangle2(this.row1[this.activeHandleIdx].D, this.col1[this.activeHandleIdx].D, 0.0, num, num);
            }
            else if (this.activeHandleIdx < this.row1.TupleLength() + this.row2.TupleLength()){
                window.DispCircle(this.row2[this.activeHandleIdx - this.row1.TupleLength()].D, 
                    this.col2[this.activeHandleIdx - this.row1.TupleLength()].D, num);
            }
            else if (this.activeHandleIdx == this.row1.TupleLength() + this.row2.TupleLength()){
                window.DispRectangle2(midR, midC, 0, num, num);
            }
            else{
                for (int i = 0; i < this.row1.TupleLength() - 1; i++){
                    window.DispLine(this.row1[i].D, this.col1[i].D, this.row1[i + 1].D, this.col1[i + 1].D);
                }
                window.DispLine(this.row1[0].D, this.col1[0].D, this.row1[this.row1.TupleLength() - 1].D,
                    this.col1[this.row1.TupleLength() - 1].D);
            }
        }

        public override ROI Clone()
        {
            ROIPolygon2 roi = new ROIPolygon2();
            roi.row0 = this.row0;
            roi.col0 = this.col0;
            roi.row1 = this.row1.Clone();
            roi.col1 = this.col1.Clone();
            roi.row2 = this.row2.Clone();
            roi.col2 = this.col2.Clone();
            roi.midR = this.midR;
            roi.midC = this.midC;
            return roi;
        }

        /// <summary>Gets the HALCON region described by the ROI</summary>
        public override HRegion getRegion()
        {
            HObject hObject;
            HOperatorSet.GenEmptyObj(out hObject);
            HObject hObject2;
            HOperatorSet.GenEmptyObj(out hObject2);
            hObject.Dispose();
            HTuple row = this.row1.TupleConcat(this.row1[0]);
            HTuple col = this.col1.TupleConcat(this.col1[0]);
            HOperatorSet.GenContourPolygonXld(out hObject, row, col);
            hObject2.Dispose();
            HOperatorSet.GenRegionContourXld(hObject, out hObject2, "filled");
            HRegion result = new HRegion(hObject2);
            hObject.Dispose();
            hObject2.Dispose();
            return result;
        }

       
        /// <summary>
        /// Gets the model information described by 
        /// the interactive ROI
        /// </summary> 
        public override HTuple getModelData()
        {
            HTuple result;
            if (this.row1 != null && this.col1 != null){
                result = this.row1.TupleConcat(this.col1);
            }
            else{
                result = null;
            }
            return result;
        }

        //public  HXLDCont GetXLDCont()
        //{
        //    HTuple row = this.row1.TupleConcat(this.row1[0]);
        //    HTuple col = this.col1.TupleConcat(this.col1[0]);
        //    HXLDCont hXLDCont = new HXLDCont();
        //    hXLDCont.GenEmptyObj();
        //    hXLDCont.GenContourPolygonXld(row, col);
        //    return hXLDCont;
        //}
        /// <summary> 
        /// Recalculates the shape of the ROI instance. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y)
        /// </summary>
        /// <param name="newX">x mouse coordinate</param>
        /// <param name="newY">y mouse coordinate</param>
        public override void moveByHandle(double newX, double newY)
        {
            if (this.minDistance >= 0.0){
                if (this.activeHandleIdx < this.row1.TupleLength()){
                    double num;
                    double d;
                    if (this.activeHandleIdx != 0 && this.activeHandleIdx != this.row1.TupleLength() - 1) {
                        num = HMisc.DistancePl(newY, newX, this.row1[this.activeHandleIdx - 1].D, this.col1[this.activeHandleIdx - 1].D,
                            this.row1[this.activeHandleIdx + 1].D, this.col1[this.activeHandleIdx + 1].D);
                        d = Math.Abs(HMisc.AngleLl(newY, newX, this.row1[this.activeHandleIdx - 1].D, 
                            this.col1[this.activeHandleIdx - 1].D, newY, newX, this.row1[this.activeHandleIdx + 1].D, 
                            this.col1[this.activeHandleIdx + 1].D));
                    }
                    else if (this.activeHandleIdx == 0){
                        num = HMisc.DistancePl(newY, newX, this.row1[this.row1.TupleLength() - 1].D, this.col1[this.col1.TupleLength()
                            - 1].D, this.row1[this.activeHandleIdx + 1].D, this.col1[this.activeHandleIdx + 1].D);
                        d = Math.Abs(HMisc.AngleLl(newY, newX, this.row1[this.row1.TupleLength() - 1].D, 
                            this.col1[this.col1.TupleLength() - 1].D, newY, newX, this.row1[this.activeHandleIdx + 1].D,
                            this.col1[this.activeHandleIdx + 1].D));
                    }
                    else {
                        num = HMisc.DistancePl(newY, newX, this.row1[this.activeHandleIdx - 1].D,
                            this.col1[this.activeHandleIdx - 1].D, this.row1[0].D, this.col1[0].D);
                        d = Math.Abs(HMisc.AngleLl(newY, newX, this.row1[this.activeHandleIdx - 1].D, 
                            this.col1[this.activeHandleIdx - 1].D, newY, newX, this.row1[0].D, this.col1[0].D));
                    }
                    if (this.minDistance == 0.0){
                        this.row1[this.activeHandleIdx] = newY;
                        this.col1[this.activeHandleIdx] = newX;
                        this.minDistance = num;
                    }
                    else if (num >= this.minDistance){
                        this.row1[this.activeHandleIdx] = newY;
                        this.col1[this.activeHandleIdx] = newX;
                        this.minDistance = num;
                    }
                    else {
                        HTuple hTuple = null;
                        HOperatorSet.TupleDeg(d, out hTuple);
                        if (Math.Abs(hTuple.D) > 179.5 && this.row1.TupleLength() > 2){
                            this.row1 = this.row1.TupleRemove(this.activeHandleIdx);
                            this.col1 = this.col1.TupleRemove(this.activeHandleIdx);
                            this.minDistance = -1.0;
                        }
                        else {
                            this.row1[this.activeHandleIdx] = newY;
                            this.col1[this.activeHandleIdx] = newX;
                            this.minDistance = num;
                        }
                    }
                }
                else if (this.activeHandleIdx < this.row1.TupleLength() + this.row2.TupleLength()){
                    midR = newY;
                    midC = newX;
                    int num2 = this.activeHandleIdx - this.row1.TupleLength() + 1;
                    this.row1 = this.row1.TupleInsert(num2, this.row2[num2 - 1].D);
                    this.col1 = this.col1.TupleInsert(num2, this.col2[num2 - 1].D);
                    this.activeHandleIdx = num2;
                }
                else if (this.activeHandleIdx == this.row1.TupleLength() + this.row2.TupleLength()) {
                    double t = this.row0 - newY;
                    double t2 = this.col0 - newX;
                    this.row1 -= t;
                    this.row2 -= t;
                    this.col1 -= t2;
                    this.col2 -= t2;
                }
                else {
                    double t = this.row0 - newY;
                    double t2 = this.col0 - newX;
                    this.row1 -= t;
                    this.row2 -= t;
                    this.col1 -= t2;
                    this.col2 -= t2;
                }
                midR = (row1.TupleMean() + row2.TupleMean()) / 2;
                midC = (col1.TupleMean() + col2.TupleMean()) / 2;
                this.updateRow2();
                this.row0 = newY;
                this.col0 = newX;
            }
        }//end of class
    }
}//end of namespace
