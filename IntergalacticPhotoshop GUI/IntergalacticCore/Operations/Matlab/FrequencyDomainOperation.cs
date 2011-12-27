namespace IntergalacticCore.Operations.Matlab
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticMatlab;
    using MathWorks.MATLAB.NET.Arrays;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FrequencyDomainOperation : MatlabOperation
    {
        /// <summary>
        /// Resulting Red image from this operation.
        /// </summary>
        private ImageBase redImage;

        /// <summary>
        /// Resulting Green image from this operation.
        /// </summary>
        private ImageBase greenImage;

        /// <summary>
        /// Resulting Blue image from this operation
        /// </summary>
        private ImageBase blueImage;
        
        /// <summary>
        /// Resulting frequency domain image from this operation.
        /// </summary>
        private ImageBase frequencyDomainImage;

        /// <summary>
        /// The Matlab Class Handle.
        /// </summary>
        private FrequencyDomain matlabCls = new FrequencyDomain();

        /// <summary>
        /// Gets the result red image.
        /// </summary>
        public ImageBase RedImage
        {
            get { return this.redImage; }
        }

        /// <summary>
        /// Gets the result green image.
        /// </summary>
        public ImageBase GreenImage
        {
            get { return this.greenImage; }
        }

        /// <summary>
        /// Gets the result blue image.
        /// </summary>
        public ImageBase BlueImage
        {
            get { return this.blueImage; }
        }

        /// <summary>
        /// Gets the result frequency domain image.
        /// </summary>
        public ImageBase FrequencyDomainImage
        {
            get { return this.frequencyDomainImage; }
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Frequency Domain Components";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            double[,] sourceRed, sourceGreen, sourceBlue;
            this.ImageToDoubles(this.Image, out sourceRed, out sourceGreen, out sourceBlue);

            MWArray[] frequencyComponents = this.matlabCls.ConvertToFrequencyDomain(
                6,
                (MWNumericArray)sourceRed,
                (MWNumericArray)sourceGreen,
                (MWNumericArray)sourceBlue);

            double[,] logMagnitudeRed = this.ApplyLogMagnitude(
                (double[,])frequencyComponents[0].ToArray(),
                (double[,])frequencyComponents[1].ToArray());

            double[,] logMagnitudeGreen = this.ApplyLogMagnitude(
                (double[,])frequencyComponents[2].ToArray(),
                (double[,])frequencyComponents[3].ToArray());

            double[,] logMagnitudeBlue = this.ApplyLogMagnitude(
                (double[,])frequencyComponents[4].ToArray(),
                (double[,])frequencyComponents[5].ToArray());

            this.redImage = this.Image.CreateEmptyClone(this.Image.Width, this.Image.Height);
            this.greenImage = this.Image.CreateEmptyClone(this.Image.Width, this.Image.Height);
            this.blueImage = this.Image.CreateEmptyClone(this.Image.Width, this.Image.Height);
            this.frequencyDomainImage = this.Image.CreateEmptyClone(this.Image.Width, this.Image.Height);

            double[,] zeroes = new double[this.Image.Height, this.Image.Width];
            
            this.DoublesToImage(this.redImage, logMagnitudeRed, zeroes, zeroes);
            this.DoublesToImage(this.greenImage, zeroes, logMagnitudeGreen, zeroes);
            this.DoublesToImage(this.blueImage, zeroes, zeroes, logMagnitudeBlue);

            this.DoublesToImage(
                this.frequencyDomainImage,
                logMagnitudeRed,
                logMagnitudeGreen,
                logMagnitudeBlue);
        }

        /// <summary>
        /// Applies log magnitude calculation.
        /// </summary>
        /// <param name="imag">Imaginary part.</param>
        /// <param name="real">Real part.</param>
        /// <returns>Resulting calculation.</returns>
        private double[,] ApplyLogMagnitude(double[,] imag, double[,] real)
        {
            double[,] logMagnitude = new double[imag.GetLength(0), imag.GetLength(1)];

            for (int i = 0; i < imag.GetLength(0); i++)
            {
                for (int j = 0; j < imag.GetLength(1); j++)
                {
                    double magnitude = Math.Sqrt((real[i, j] * real[i, j]) + (imag[i, j] * imag[i, j]));
                    logMagnitude[i, j] = Math.Log(magnitude + 1.0);
                }
            }

            return logMagnitude;
        }
    }
}
