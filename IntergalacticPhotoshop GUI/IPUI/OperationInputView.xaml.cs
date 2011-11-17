
namespace IPUI
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
    using IntergalacticCore2;
    using IntergalacticCore2.Data;

    /// <summary>
    /// Interaction logic for OperationInputView.xaml
    /// </summary>
    public partial class OperationInputView : UserControl
    {
        private BaseOperation operation;
        private List<OperationInputInfo> inputInfoList;
        private List<object> inputSourceList;
        private List<Label> labelList;
        private Dictionary<Button, Image> imageInputDictionary;

        public OperationInputView()
        {
            InitializeComponent();
        }

        public void SetInputTarget(BaseOperation operation)
        {
            this.inputInfoList = operation.GetInputTypes();

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
            this.imageInputDictionary = new Dictionary<Button, Image>();

            int shiftY = 30;
            for (int i = 0; i < this.inputInfoList.Count; i++)
            {
                switch (this.inputInfoList[i].Type)
                {
                    case IntergalacticCore2.Data.InputType.Bool:
                        this.AddBooleanInputControl(this.inputInfoList[i], ref shiftY);
                        break;
                    case IntergalacticCore2.Data.InputType.Color:
                        this.AddColorInputControls(this.inputInfoList[i], ref shiftY);
                        break;
                    case IntergalacticCore2.Data.InputType.Mask:
                        throw new InvalidOperationException("Masks input are not supported");
                    case IntergalacticCore2.Data.InputType.Image:
                        this.AddImageInputControls(this.inputInfoList[i], ref shiftY);
                        break;
                    default:
                        this.AddNumericInputControls(this.inputInfoList[i], ref shiftY);
                        break;
                }
            }

            this.Height = shiftY + 80;
        }

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

        private void AddNumericInputControls(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            Slider numericSlider = this.CreateSliderWithLabel(ref startY);

            switch (info.Type)
            {
                case IntergalacticCore2.Data.InputType.Byte:
                    numericSlider.Minimum = (byte)info.From;
                    numericSlider.Maximum = (byte)info.To;
                    numericSlider.SmallChange = 1;
                    numericSlider.LargeChange = 1;
                    numericSlider.TickFrequency = 1;
                    numericSlider.IsSnapToTickEnabled = true;
                    break;
                case IntergalacticCore2.Data.InputType.Int:
                    numericSlider.Minimum = (int)info.From;
                    numericSlider.Maximum = (int)info.To;
                    numericSlider.SmallChange = 1;
                    numericSlider.LargeChange = 1;
                    numericSlider.TickFrequency = 1;
                    numericSlider.IsSnapToTickEnabled = true;
                    break;
                case IntergalacticCore2.Data.InputType.Float:
                    numericSlider.Minimum = (float)info.From;
                    numericSlider.Maximum = (float)info.To;
                    numericSlider.SmallChange = 0.05;
                    numericSlider.LargeChange = 1;
                    break;
                case IntergalacticCore2.Data.InputType.Double:
                    numericSlider.Minimum = (double)info.From;
                    numericSlider.Maximum = (double)info.To;
                    numericSlider.SmallChange = 0.05;
                    numericSlider.LargeChange = 1;
                    break;
                default:
                    break;
            }

            this.inputSourceList.Add(numericSlider);

            startY += 10;
        }

        private void AddColorInputControls(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            SolidColorBrush brush = new SolidColorBrush(Colors.Black);

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
            rect.Fill = brush;

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

            this.inputSourceList.Add(brush);

            startY += 15;
        }

        private void AddImageInputControls(OperationInputInfo info, ref int startY)
        {
            this.AddLabel(info.Title, startY);

            DropShadowEffect rectShadow = new DropShadowEffect();
            rectShadow.BlurRadius = 15;
            rectShadow.Direction = 270;
            rectShadow.Color = Colors.Black;
            rectShadow.ShadowDepth = 2;
            rectShadow.Opacity = 0.2;

            DropShadowEffect imgShadow = new DropShadowEffect();
            imgShadow.BlurRadius = 5;
            imgShadow.Direction = 270;
            imgShadow.Color = Colors.Black;
            imgShadow.ShadowDepth = 2;
            imgShadow.Opacity = 0.5;

            Rectangle rect = new Rectangle();
            rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            rect.Margin = new Thickness(-125, startY + 10, 0, 0);
            rect.Width = 118;
            rect.Height = 78;
            rect.RadiusX = 10;
            rect.RadiusY = 10;
            rect.Fill = Brushes.LightGray;
            rect.Stroke = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));
            rect.Effect = rectShadow;

            Label label = new Label();
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            label.Margin = new Thickness(-125, startY + 10, 0, 0);
            label.Width = 118;
            label.Height = 78;
            label.Content = "Browse an image";
            label.FontSize = 11;
            label.Foreground = Brushes.Gray;

            Button browseButton = new Button();
            browseButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            browseButton.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            browseButton.Margin = new Thickness(130, startY + 37, 0, 0);
            browseButton.Width = 110;
            browseButton.Height = 23;
            browseButton.Content = "Browse an image";
            browseButton.Click += new RoutedEventHandler(this.BrowseButton_Click);

            Image img = new Image();
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            img.Margin = new Thickness(-121, startY + 14, 0, 0);
            img.Width = 110;
            img.Height = 70;
            img.Stretch = Stretch.Uniform;
            img.Effect = imgShadow;

            this.mainGrid.Children.Add(rect);
            this.mainGrid.Children.Add(label);
            this.mainGrid.Children.Add(img);
            this.mainGrid.Children.Add(browseButton);

            this.inputSourceList.Add(img);
            this.imageInputDictionary.Add(browseButton, img);

            startY += 110;
        }

        void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.ShowDialog();

            if (dialog.FileName != string.Empty)
            {
                this.imageInputDictionary[(Button)sender].Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

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

        private object GetInputFromUIElement(int index)
        {
            object result = null;

            switch (this.inputInfoList[index].Type)
            {
                case IntergalacticCore2.Data.InputType.Bool:
                    result = ((CheckBox)this.inputSourceList[index]).IsChecked == true;
                    break;
                case IntergalacticCore2.Data.InputType.Byte:
                    result = (byte)((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticCore2.Data.InputType.Int:
                    result = (int)((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticCore2.Data.InputType.Float:
                    result = (float)((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticCore2.Data.InputType.Double:
                    result = ((Slider)this.inputSourceList[index]).Value;
                    break;
                case IntergalacticCore2.Data.InputType.Color:
                    Color color = ((SolidColorBrush)this.inputSourceList[index]).Color;
                    result = new Pixel(color.R, color.G, color.B);
                    break;
                case IntergalacticCore2.Data.InputType.Mask:
                    break;
                case IntergalacticCore2.Data.InputType.Image:
                    result = new WPFBitmap((BitmapSource)((Image)this.inputSourceList[index]).Source);
                    break;
                default:
                    break;
            }

            return result;
        }

        private void ApplyOperation_MouseClick(object sender, RoutedEventArgs e)
        {
            List<object> inputList = new List<object>();

            for (int i = 0; i < this.inputInfoList.Count; i++)
            {
                inputList.Add(this.GetInputFromUIElement(i));
            }

            this.operation.GetInput(inputList);
            ////TODO: Call the operation manager
        }

        private void CancelOperation_Click(object sender, RoutedEventArgs e)
        {
            PopupViewManager.CurrentPopupManager.CloseOperationInputView();
        }
    }
}
