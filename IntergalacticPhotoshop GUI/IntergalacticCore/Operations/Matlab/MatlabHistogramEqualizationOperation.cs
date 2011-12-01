// -----------------------------------------------------------------------
// <copyright file="MatlabHistogramEqualizationOperation.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace IntergalacticCore.Operations.Matlab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MathWorks.MATLAB.NET.Arrays;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MatlabHistogramEqualizationOperation : MatlabOperation
    {
        protected override void BeforeOperate()
        {
            Byte[,] red, green, blue;
            this.ImageToBytes(this.Image, out red,out green, out blue);

            IPmw.TestCls cls = new IPmw.TestCls();
            MWArray [] result = cls.HistEq(3, (MWNumericArray)red, (MWNumericArray)green, (MWNumericArray)blue);

            this.BytesToImage(this.Image, (byte[,])result[0].ToArray(), (byte[,])result[1].ToArray(), (byte[,])result[2].ToArray());
        }

        public override string ToString()
        {
            return "Matlab Hist";
        }
    }
}
