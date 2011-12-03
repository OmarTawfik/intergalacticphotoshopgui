namespace IntergalacticControls
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
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticControls.Classes;
    using IntergalacticControls.PopupUI.Notifications;
    using IntergalacticControls.SpecialUI;
    using IntergalacticCore;
    using IntergalacticCore.Data;

    /// <summary>
    /// Interaction logic for OperationInputView.xaml
    /// </summary>
    public partial class OperationInputView : UserControl
    {
        /// <summary>
        /// Shows whether the view is shown
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// The targeted operation
        /// </summary>
        private BaseOperation operation;

        /// <summary>
        /// List of operation input information
        /// </summary>
        private List<OperationInputInfo> inputInfoList;

        /// <summary>
        /// List of objects that hold the input from the UI
        /// </summary>
        private List<object> inputSourceList;

        /// <summary>
        /// List of UI input labels
        /// </summary>
        private List<Label> labelList;

        /// <summary>
        /// Buttons if the image input
        /// </summary>
        private List<ComboBox> imageInputComboBoxes;

        /// <summary>
        /// Buttons if the image input
        /// </summary>
        private List<Rectangle> imageInputRects;

        /// <summary>
        /// Default mask size for mask input
        /// </summary>
        private int defaultMaskSize = 3;

        /// <summary>
        /// Initializes a new instance of the OperationInputView class
        /// </summary>
        public OperationInputView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the default mask size for mask operations
        /// </summary>
        internal int DefaultMaskSize
        {
            get { return this.defaultMaskSize; }
            set { this.defaultMaskSize = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this view is shown or not.
        /// </summary>
        internal bool IsShown
        {
            get { return this.isShown; }
            set { this.isShown = value; }
        }

        /// <summary>
        /// Sets the targeted operation
        /// </summary>
        /// <param name="operation">The operation</param>
        public void SetInputTarget(BaseOperation operation)
        {
            this.inputInfoList = new List<OperationInputInfo>();
            this.ParseInputInfo(this.inputInfoList, operation.GetInput());

            if (this.inputInfoList == null)
            {
                throw new InvalidOperationException("This view only accepts oparations with input.");
            }

            if (this.inputInfoList.Count < 1)
            {
                throw new InvalidOperationException("Operation must have at least one input.");
            }

            this.operation = operation;

            this.mainGrid.Children.Clear();
            this.mainGrid.Children.Add(btnApply);
            this.mainGrid.Children.Add(btnCancel);

            this.inputSourceList = new List<object>();
            this.labelList = new List<Label>();
            this.imageInputComboBoxes = new List<ComboBox>();
            this.imageInputRects = new List<Rectangle>();

            int shiftY = 30;
            for (int i = 0; i < this.inputInfoList.Count; i++)
            {
                switch (this.inputInfoList[i].Type)
                {
                    case IntergalacticControls.InputType.Bool:
                        this.AddBooleanInputControl(this.inputInfoList[i], ref shiftY);
                        break;
                    case IntergalacticControls.InputType.Color:
                        this.AddColorInputControls(this.inputInfoList[i], ref shiftY);
                        break;
                    case IntergalacticControls.InputType.Mask:
                        this.AddMaskInputControl(this.inputInfoList[i], ref shiftY);
                        break;
                    case IntergalacticControls.InputType.Image:
                        this.AddImageInputControls(this.inputInfoList[i], ref shiftY);
                        break;
                    default:
                        this.AddNumericInputControls(this.inputInfoList[i], ref shiftY);
                        break;
                }
            }

            this.Height = shiftY + 80;
        }

        /// <summary>
        /// Updates the UI with the current operation
        /// </summary>
        public void ReConstructUI()
        {
            this.SetInputTarget(this.operation);
        }

        /// <summary>
        /// Adds boolean input controls to the UI
        /// </summary>
        /// <param name="info">Input information</param>
        /// <param name="startY">The Y position to start with</param>
        private void AddBooleanInputControl(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            CheckBox checkBox = new CheckBox();
            checkBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            checkBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            checkBox.Margin = new Thickness(15, startY + 7, 0, 0);
            checkBox.Width = 265;
            checkBox.Height = 23;
            checkBox.Content = string.Empty;

            this.mainGrid.Children.Add(checkBox);
            this.inputSourceList.Add(checkBox);

            startY += 35;
        }

        /// <summary>
        /// Adds numeric input controls to the UI
        /// </summary>
        /// <param name="info">Input information</param>
        /// <param name="startY">The Y position to start with</param>
        private void AddNumericInputControls(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            Slider numericSlider = this.CreateSliderWithLabel(ref startY);

            switch (info.Type)
            {
                case IntergalacticControls.InputType.Byte:
                    numericSlider.Minimum = (byte)info.From;
                    numericSlider.Maximum = (byte)info.To;
                    numericSlider.SmallChange = 1;
                    numericSlider.LargeChange = 1;
                    numericSlider.TickFrequency = 1;
                    numericSlider.IsSnapToTickEnabled = true;
                    break;
                case IntergalacticControls.InputType.Int:
                    numericSlider.Minimum = (int)info.From;
                    numericSlider.Maximum = (int)info.To;
                    numericSlider.SmallChange = 1;
                    numericSlider.LargeChange = 1;
                    numericSlider.TickFrequency = 1;
                    numericSlider.IsSnapToTickEnabled = true;
                    break;
                case IntergalacticControls.InputType.Float:
                    numericSlider.Minimum = (float)info.From;
                    numericSlider.Maximum = (float)info.To;
                    numericSlider.SmallChange = 0.05;
                    numericSlider.LargeChange = 1;
                    numericSlider.TickFrequency = 0.001;
                    numericSlider.IsSnapToTickEnabled = true;
                    break;
                case IntergalacticControls.InputType.Double:
                    numericSlider.Minimum = (double)info.From;
                    numericSlider.Maximum = (double)info.To;
                    numericSlider.SmallChange = 0.05;
                    numericSlider.LargeChange = 1;
                    numericSlider.TickFrequency = 0.001;
                    numericSlider.IsSnapToTickEnabled = true;
                    break;
                default:
                    break;
            }

            this.inputSourceList.Add(numericSlider);

            startY += 10;
        }

        /// <summary>
        /// Adds color input controls to the UI
        /// </summary>
        /// <param name="info">Input information</param>
        /// <param name="startY">The Y position to start with</param>
        private void AddColorInputControls(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            DropShadowEffect shadow = new DropShadowEffect();
            shadow.BlurRadius = 15;
            shadow.Direction = 270;
            shadow.Color = Colors.Black;
            shadow.ShadowDepth = 2;
            shadow.Opacity = 0.2;

            Rectangle rect = new Rectangle();
            rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            rect.Margin = new Thickness(430, startY + 5, 0, 0);
            rect.Width = 40;
            rect.Height = 75;
            rect.RadiusX = 10;
            rect.RadiusY = 10;
            rect.Effect = shadow;
            rect.Stroke = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));

            this.mainGrid.Children.Add(rect);

            Slider redSlider = this.CreateSliderWithLabel(ref startY);
            Slider greenSlider = this.CreateSliderWithLabel(ref startY);
            Slider blueSlider = this.CreateSliderWithLabel(ref startY);

            redSlider.Minimum = 0;
            redSlider.Maximum = 255;
            redSlider.SmallChange = 1;
            redSlider.LargeChange = 1;
            redSlider.TickFrequency = 1;
            redSlider.IsSnapToTickEnabled = true;

            greenSlider.Minimum = 0;
            greenSlider.Maximum = 255;
            greenSlider.SmallChange = 1;
            greenSlider.LargeChange = 1;
            greenSlider.TickFrequency = 1;
            greenSlider.IsSnapToTickEnabled = true;

            blueSlider.Minimum = 0;
            blueSlider.Maximum = 255;
            blueSlider.SmallChange = 1;
            blueSlider.LargeChange = 1;
            blueSlider.TickFrequency = 1;
            blueSlider.IsSnapToTickEnabled = true;

            Binding redBind = new Binding();
            redBind.Source = redSlider;
            redBind.Path = new PropertyPath(Slider.ValueProperty);

            Binding greenBind = new Binding();
            greenBind.Source = greenSlider;
            greenBind.Path = new PropertyPath(Slider.ValueProperty);

            Binding blueBind = new Binding();
            blueBind.Source = blueSlider;
            blueBind.Path = new PropertyPath(Slider.ValueProperty);

            MultiBinding brushBind = new MultiBinding();
            brushBind.Converter = new BrushConverter();
            brushBind.Bindings.Add(redBind);
            brushBind.Bindings.Add(greenBind);
            brushBind.Bindings.Add(blueBind);
            BindingOperations.SetBinding(rect, Rectangle.FillProperty, brushBind);

            this.inputSourceList.Add(rect);

            startY += 15;
        }

        /// <summary>
        /// Adds image input controls to the UI
        /// </summary>
        /// <param name="info">Input information</param>
        /// <param name="startY">The Y position to start with</param>
        private void AddImageInputControls(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            ImageBrush rectFill = new ImageBrush();

            DropShadowEffect rectShadow = new DropShadowEffect();
            rectShadow.BlurRadius = 15;
            rectShadow.Direction = 270;
            rectShadow.Color = Colors.Black;
            rectShadow.ShadowDepth = 2;
            rectShadow.Opacity = 0.4;

            Rectangle rect = new Rectangle();
            rect.Margin = new Thickness(-125, startY + 10, 0, 0);
            rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            rect.Width = 118;
            rect.Height = 78;
            rect.RadiusX = 10;
            rect.RadiusY = 10;
            rect.Stroke = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));
            rect.Fill = rectFill;
            rect.Effect = rectShadow;

            ComboBox tabsComboBox = new ComboBox();
            tabsComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            tabsComboBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            tabsComboBox.Margin = new Thickness(130, startY + 37, 0, 0);
            tabsComboBox.Width = 110;
            tabsComboBox.Height = 23;
            List<string> tabNames = Manager.Instance.GetTabNames();
            for (int i = 0; i < tabNames.Count; i++)
            {
                tabsComboBox.Items.Add(tabNames[i]);
            }

            tabsComboBox.Items.Add("Image from file");
            tabsComboBox.SelectionChanged += new SelectionChangedEventHandler(this.TabsComboBox_SelectionChanged);

            this.imageInputComboBoxes.Add(tabsComboBox);
            this.imageInputRects.Add(rect);

            tabsComboBox.SelectedIndex = 0;

            this.mainGrid.Children.Add(rect);
            this.mainGrid.Children.Add(tabsComboBox);

            this.inputSourceList.Add(rectFill);

            startY += 110;
        }

        /// <summary>
        /// Adds mask input control to the UI
        /// </summary>
        /// <param name="info">Input information</param>
        /// <param name="startY">The Y position to start with</param>
        private void AddMaskInputControl(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            MaskInputControl maskControl = new MaskInputControl(this.defaultMaskSize);
            maskControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            maskControl.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            maskControl.Margin = new Thickness(-(this.Width - maskControl.Width) + (this.Width * 0.6), startY + 5, 0, 0);

            Button changeButton = new Button();
            changeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            changeButton.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            changeButton.Width = 100;
            changeButton.Height = 25;
            changeButton.Margin = new Thickness(-(this.Width - maskControl.Width) + (this.Width * 0.6) + maskControl.Width + changeButton.Width + 20, startY + (maskControl.Height / 2), 0, 0);
            changeButton.Content = "Change Size";
            changeButton.Click += new RoutedEventHandler(this.ChangeMaskSizeButton_Click);

            this.mainGrid.Children.Add(maskControl);
            this.mainGrid.Children.Add(changeButton);
            this.inputSourceList.Add(maskControl);
            
            startY += (int)(maskControl.Height + 20);
        }

        /// <summary>
        /// SizeChanged function to switch images
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void TabsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedIndex == combo.Items.Count - 1)
            {
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.ShowDialog();

                if (dialog.FileName != string.Empty)
                {
                    int index = 0;
                    while (this.imageInputComboBoxes[index] != combo)
                    {
                        index++;
                    }

                    ImageBrush brush = (ImageBrush)this.imageInputRects[index].Fill;
                    brush.ImageSource = new BitmapImage(new Uri(dialog.FileName));
                    brush.Stretch = Stretch.UniformToFill;
                }
                else
                {
                    combo.SelectedIndex = 0;
                }
            }
            else
            {
                int index = 0;
                while (this.imageInputComboBoxes[index] != combo)
                {
                    index++;
                }

                ImageBrush brush = (ImageBrush)this.imageInputRects[index].Fill;
                brush.ImageSource = ((WPFBitmap)Manager.Instance.GetTab((string)combo.SelectedItem).Image).GetImageSource();
                brush.Stretch = Stretch.UniformToFill;
            }
        }

        /// <summary>
        /// SizeChanged function to switch images
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void ChangeMaskSizeButton_Click(object sender, RoutedEventArgs e)
        {
            MaskSizeNotification view = new MaskSizeNotification(this);
            view.ShowNotification();
        }

        /// <summary>
        /// Creates a slider for numeric input
        /// </summary>
        /// <param name="startY">The Y position to start with</param>
        /// <returns>The Slider</returns>
        private Slider CreateSliderWithLabel(ref int startY)
        {
            Slider newSlider = new Slider();
            newSlider.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            newSlider.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newSlider.Margin = new Thickness(15, startY + 5, 0, 0);
            newSlider.Width = 265;
            newSlider.Height = 23;

            Label sliderValueLabel = new Label();
            sliderValueLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            sliderValueLabel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            sliderValueLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            sliderValueLabel.Margin = new Thickness(345, startY, 0, 0);
            sliderValueLabel.Width = 55;
            sliderValueLabel.Height = 25;

            Binding bind = new Binding();
            bind.Source = newSlider;
            bind.Path = new PropertyPath(Slider.ValueProperty);
            BindingOperations.SetBinding(sliderValueLabel, Label.ContentProperty, bind);

            mainGrid.Children.Add(newSlider);
            mainGrid.Children.Add(sliderValueLabel);

            startY += 30;

            return newSlider;
        }

        /// <summary>
        /// Adds a label to the input controls
        /// </summary>
        /// <param name="text">Label text</param>
        /// <param name="startY">The Y position to start with</param>
        private void AddLabel(string text, int startY)
        {
            Label newLabel = new Label();
            newLabel.Content = text;
            newLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            newLabel.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            newLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            newLabel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newLabel.Margin = new Thickness(-380, startY, 0, 0);
            newLabel.Width = 100;
            newLabel.Height = 30;

            this.labelList.Add(newLabel);
            this.mainGrid.Children.Add(newLabel);
        }

        /// <summary>
        /// Gets input from the input source list
        /// </summary>
        /// <param name="index">The index of the input</param>
        /// <returns>Input object</returns>
        private object GetInputFromSourceList(int index)
        {
            object result = null;

            switch (this.inputInfoList[index].Type)
            {
                case IntergalacticControls.InputType.Bool:
                    result = ((CheckBox)this.inputSourceList[index]).IsChecked == true;
                    break;
                case IntergalacticControls.InputType.Byte:
                    result = (byte)((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticControls.InputType.Int:
                    result = (int)((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticControls.InputType.Float:
                    result = (float)((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticControls.InputType.Double:
                    result = ((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticControls.InputType.Color:
                    Color color = ((SolidColorBrush)((Rectangle)this.inputSourceList[index]).Fill).Color;
                    result = new Pixel(color.R, color.G, color.B);
                    break;
                case IntergalacticControls.InputType.Mask:
                    result = ((MaskInputControl)this.inputSourceList[index]).GetMask();
                    break;
                case IntergalacticControls.InputType.Image:
                    result = new WPFBitmap((BitmapSource)((ImageBrush)this.inputSourceList[index]).ImageSource);
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Click function for the Apply button
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void ApplyOperation_MouseClick(object sender, RoutedEventArgs e)
        {
            object[] inputList = new object[this.inputInfoList.Count];

            try
            {
                for (int i = 0; i < this.inputInfoList.Count; i++)
                {
                    inputList[i] = this.GetInputFromSourceList(i);
                }

                this.operation.SetInput(inputList);
                Manager.Instance.DoOperation(this.operation);
                UIManager.CurrentUIManager.CloseOperationInputView();
            }
            catch (Exception ex)
            {
                SideNotification notification = new SideNotification();
                notification.SetTitle("Error: " + ex.Message);
                notification.ShowNotification();
            }
        }

        /// <summary>
        /// Click function for the Cancel button
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void CancelOperation_Click(object sender, RoutedEventArgs e)
        {
            UIManager.CurrentUIManager.CloseOperationInputView();
        }

        /// <summary>
        /// Parses the input string from the target operation
        /// </summary>
        /// <param name="targetList">The list to add the input information to</param>
        /// <param name="inputString">The input string to parse</param>
        private void ParseInputInfo(List<OperationInputInfo> targetList, string inputString)
        {
            string[] inputList = inputString.Split('|');

            for (int i = 0; i < inputList.Length; i++)
            {
                string[] parameters = inputList[i].Split(',');
                string title = parameters[0];
                InputType type = this.GetInputType(parameters[1]);
                object from = null;
                object to = null;

                if (parameters.Length > 2)
                {
                    from = this.ConvertParameterToObject(type, parameters[2]);
                    to = this.ConvertParameterToObject(type, parameters[3]);
                }

                targetList.Add(new OperationInputInfo(title, type, from, to));
            }
        }

        /// <summary>
        /// Gets the input type form string
        /// </summary>
        /// <param name="parameter">The string</param>
        /// <returns>Input type</returns>
        private InputType GetInputType(string parameter)
        {
            InputType result = InputType.Bool;
            switch (parameter)
            {
                case "bool":
                    result = InputType.Bool;
                    break;
                case "byte_slider":
                    result = InputType.Byte;
                    break;
                case "int":
                    result = InputType.Int;
                    break;
                case "float":
                    result = InputType.Float;
                    break;
                case "double":
                    result = InputType.Double;
                    break;
                case "color":
                    result = InputType.Color;
                    break;
                case "image":
                    result = InputType.Image;
                    break;
                case "mask":
                    result = InputType.Mask;
                    break;
                default:
                    throw new InvalidOperationException("Type was not identified.");
            }

            return result;
        }

        /// <summary>
        /// Converts parameter to its equivalant object from a string
        /// </summary>
        /// <param name="type">The input type</param>
        /// <param name="parameter">The string</param>
        /// <returns>The converted object</returns>
        private object ConvertParameterToObject(InputType type, string parameter)
        {
            object result = 0;

            switch (type)
            {
                case InputType.Byte:
                    if (parameter == "-inf")
                    {
                        result = (byte)0;
                        break;
                    }
                    else if (parameter == "inf")
                    {
                        result = (byte)255;
                        break;
                    }
                    else
                    {
                        result = Convert.ToByte(parameter);
                    }

                    break;
                case InputType.Int:
                    if (parameter == "-inf")
                    {
                        result = int.MinValue;
                        break;
                    }
                    else if (parameter == "inf")
                    {
                        result = 10000;
                        break;
                    }
                    else
                    {
                        result = Convert.ToInt32(parameter);
                    }

                    break;
                case InputType.Float:
                    if (parameter == "-inf")
                    {
                        result = -1000000;
                        break;
                    }
                    else if (parameter == "inf")
                    {
                        result = 1000000;
                        break;
                    }
                    else
                    {
                        result = (float)Convert.ToDouble(parameter);
                    }

                    break;
                case InputType.Double:
                    if (parameter == "-inf")
                    {
                        result = double.MinValue;
                        break;
                    }
                    else if (parameter == "inf")
                    {
                        result = double.MaxValue;
                        break;
                    }
                    else
                    {
                        result = Convert.ToDouble(parameter);
                    }

                    break;
                default:
                    throw new InvalidOperationException("Sent parameter is not accepted as a numeric value");
            }

            return result;
        }
    }
}
