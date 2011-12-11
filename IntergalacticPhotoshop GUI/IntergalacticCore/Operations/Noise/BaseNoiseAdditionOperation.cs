namespace IntergalacticCore.Operations.Noise
{
    using System;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Acts as a base to all noise addition operations.
    /// </summary>
    public class BaseNoiseAdditionOperation : BaseOperation
    {
        /// <summary>
        /// Gets called after the operation ends.
        /// </summary>
        protected override void AfterOperate()
        {
            NormalizationOperation operation = new NormalizationOperation();
            this.Image = operation.Execute(this.Image);
        }

        /// <summary>
        /// Gets the random table for this noise.
        /// </summary>
        /// <param name="percentage">Count of true pixels.</param>
        /// <returns>Generated table.</returns>
        protected bool[,] GetRandomTable(double percentage)
        {
            bool[,] table = new bool[this.Image.Height, this.Image.Width];
            Random rand = new Random();

            int count = (int)(percentage * this.Image.Width * this.Image.Height);
            while (count > 0)
            {
                int x = rand.Next(this.Image.Width);
                int y = rand.Next(this.Image.Height);

                if (table[y, x] == false)
                {
                    table[y, x] = true;
                    count--;
                }
            }

            return table;
        }
    }
}
