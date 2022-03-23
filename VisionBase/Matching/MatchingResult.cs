using System;
using HalconDotNet;


namespace VisionBase.Matching
{
    
    /// <summary>
    /// This class holds the result data from a model detection. For any new
    /// detection run, it also remembers the time needed
    /// for the model detection. 
    /// </summary>
    public class MatchingResult
	{
		
        /// <summary>
        /// Model contour applied for model detection
        /// </summary>
		public HXLDCont  mContour;
        /// <summary>
        /// All model contours detected
        /// </summary>
		public HXLDCont  mContResults;

        /// <summary>
        /// Row coordinate of the found instances of the model
        /// </summary>
		public HTuple mRow;
        /// <summary>
        /// Column coordinate of the found instances of the model
        /// </summary>
		public HTuple mCol;

        public double mImageScale;

        public double mError;
        /// <summary>
        /// Rotation angle of the found instances of the model
        /// </summary>
		public HTuple mAngle;
        /// <summary>
        /// Scale of the found instances of the model in the row direction
        /// </summary>
		public HTuple mScaleRow;
        /// <summary>
        /// Scale of the found instances of the model in the column direction
        /// </summary>
		public HTuple mScaleCol;
        /// <summary>
        /// Score of the found instances of the model
        /// </summary>
		public HTuple mScore;
        /// <summary>
        /// Time needed to detect <c>count</c> numbers of model instances
        /// </summary>
		public double mTime;
        /// <summary>
        /// Number of model instances found
        /// </summary>
		public int	  count;
        public bool isGrayModel;
        /// <summary>
        /// 2D homogeneous transformation matrix that can be used to transform
        /// data from the model into the test image.
        /// </summary>
		public HHomMat2D hmat;
        
        /// <summary>Constructor</summary>
		public MatchingResult()
		{
			hmat = new HHomMat2D();
			mContResults = new HXLDCont();
		}

        ~MatchingResult()
        {
            if (mContResults != null) mContResults.Dispose();
        }

        /// <summary>
        /// Gets the detected contour.
        /// </summary>
        /// <returns>Detected contour</returns>
		public HXLDCont getDetectionResults()
		{
			HXLDCont rContours = new HXLDCont();
            hmat.HomMat2dIdentity();

            if (mContResults != null) mContResults.Dispose();
			mContResults.GenEmptyObj();
            


            for (int i = 0; i<count; i++) 
			{
                if (mImageScale < 0.1 || mImageScale > 2) mImageScale = 1.0;
                hmat.VectorAngleToRigid(0, 0, 0, mRow[i].D* mImageScale, mCol[i].D* mImageScale, mAngle[i].D);
                //2020.11.7 gengxm修改 ，轮廓放大三倍
                hmat = hmat.HomMat2dScale(1.0000 / mImageScale, 1.0000 / mImageScale, 0, 0);

                rContours = hmat.AffineTransContourXld(mContour);
 
      


                mContResults =  mContResults.ConcatObj(rContours);
			}
            if (rContours != null) rContours.Dispose();

            if(true)
            {
                HTuple convexity;
                HTuple col1,col2,row1,row2;
                HOperatorSet.SmallestRectangle1Xld(mContResults,out row1,out col1,out row2,out col2);
                HOperatorSet.ConvexityXld(mContResults, out convexity);
               // wid = col2 - col1;
                //hei = row2 - row1;
            }

			return mContResults;
		}


        /// <summary>
        /// Resets the detection results and sets count to 0.
        /// </summary>
		public void reset()
		{
			count = 0;
		}

	}//end of class
}//end of namespace
