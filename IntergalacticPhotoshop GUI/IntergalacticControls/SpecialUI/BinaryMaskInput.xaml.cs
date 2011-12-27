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
    /// Interaction logic for BinaryMaskInput.xaml
    /// </summary>
    public partial class BinaryMaskInputControl : UserControl
    {
        /// <summary>
        /// Default width and height of textBoxes
        /// </summary>
        private int textBoxWidth = 35, textBoxHeight = 23;

        /// <summary>
        /// Input Text Boxes
        /// </summary>
        private CheckBox[,] textBoxes;

        /// <summary>
        /// The mask size
        /// </summary>
        private int maskSize;

        /// <summary>
        /// Initializes a new instance of the BinaryMaskInputControl class
        /// </summary>
        /// <param name="maskSize">Mask size</param>
        public BinaryMaskInputControl(int maskSize)
        {
            InitializeComponent();

            if (maskSize % 2 == 0)
            {
                throw new Exception("Mask size must be an odd number.");
            }

            this.maskSize = maskSize;
            this.textBoxes = new CheckBox[maskSize, maskSize];

            for (int i = 0; i < maskSize; i++)
            {
                for (int j = 0; j < maskSize; j++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    checkBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    checkBox.Margin = new Thickness(3 + (j * this.textBoxWidth), 3 + (i * this.textBoxHeight), 0, 0);
                    checkBox.Width = 35;
                    checkBox.Height = this.textBoxHeight;
                    checkBox.IsChecked = false;

                    this.mainGrid.Children.Add(checkBox);
                    this.textBoxes[i, j] = checkBox;
                }
            }

            this.Width = 3 + (maskSize * this.textBoxWidth);
            this.Height = 3 + (maskSize * this.textBoxHeight);
        }

        /// <summary>
        /// Returns the mask entered from the user
        /// </summary>
        /// <returns>The mask</returns>
        public BinaryMask GetMask()
        {
            BinaryMask mask = new BinaryMask(this.maskSize, this.maskSize);

            for (int i = 0; i < this.maskSize; i++)
            {
                for (int j = 0; j < this.maskSize; j++)
                {
                    try
                    {
                        mask.Data[i, j] = (bool)this.textBoxes[i, j].IsChecked;
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
