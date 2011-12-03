namespace IntergalacticControls.SpecialUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticCore.Data;

    /// <summary>
    /// Interaction logic for MaskInputControl.xaml
    /// </summary>
    public partial class MaskInputControl : UserControl
    {
        /// <summary>
        /// Default width and height of textBoxes
        /// </summary>
        private int textBoxWidth = 35, textBoxHeight = 23;

        /// <summary>
        /// Input Text Boxes
        /// </summary>
        private TextBox[,] textBoxes;

        /// <summary>
        /// The mask size
        /// </summary>
        private int maskSize;

        /// <summary>
        /// Initializes a new instance of the MaskInputControl class
        /// </summary>
        /// <param name="maskSize">Mask size</param>
        public MaskInputControl(int maskSize)
        {
            InitializeComponent();

            if (maskSize % 2 == 0)
            {
                throw new Exception("Mask size must be an odd number.");
            }

            this.maskSize = maskSize;
            this.textBoxes = new TextBox[maskSize, maskSize];

            for (int i = 0; i < maskSize; i++)
            {
                for (int j = 0; j < maskSize; j++)
                {
                    TextBox text = new TextBox();
                    text.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    text.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    text.Margin = new Thickness(3 + (j * this.textBoxWidth), 3 + (i * this.textBoxHeight), 0, 0);
                    text.Width = 35;
                    text.Height = this.textBoxHeight;
                    text.Text = "0";

                    this.mainGrid.Children.Add(text);
                    this.textBoxes[i, j] = text;
                }
            }

            this.Width = 3 + (maskSize * this.textBoxWidth);
            this.Height = 3 + (maskSize * this.textBoxHeight);
        }

        /// <summary>
        /// Returns the mask entered from the user
        /// </summary>
        /// <returns>The mask</returns>
        public ConvolutionMask GetMask()
        {
            ConvolutionMask mask = new ConvolutionMask(this.maskSize, this.maskSize);

            for (int i = 0; i < this.maskSize; i++)
            {
                for (int j = 0; j < this.maskSize; j++)
                {
                    try
                    {
                        mask.Data[i, j] = Convert.ToDouble(this.textBoxes[i, j].Text);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Invalid mask value at (" + i + ", " + j + ").");
                    }
                }
            }

            return mask;
        }
    }
}
