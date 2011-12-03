namespace IntergalacticControls.PopupUI.Notifications
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
    using IntergalacticControls.PopupUI.Notifications.Base;
    using IntergalacticControls.SpecialUI;

    /// <summary>
    /// Interaction logic for MaskSizeNotification.xaml
    /// </summary>
    public partial class MaskSizeNotification : NotificationView
    {
        /// <summary>
        /// The input view to update
        /// </summary>
        private OperationInputView inputView;
        
        /// <summary>
        /// Initializes a new instance of the MaskSizeNotification class
        /// </summary>
        /// <param name="operationView">The current OperationInputView</param>
        public MaskSizeNotification(OperationInputView operationView)
        {
            InitializeComponent();
            this.BlocksUI = false;
            this.AnimationType = NotificationAnimationType.Fade;
            this.DisplayTimeout = null;
            this.inputView = operationView;
            this.btnChangeMaskSize.Click += new RoutedEventHandler(this.BtnChangeMaskSize_Click);

            Binding bind = new Binding();
            bind.Source = sliderMaskSize;
            bind.Path = new PropertyPath(Slider.ValueProperty);
            BindingOperations.SetBinding(lblValue, Label.ContentProperty, bind);
        }

        /// <summary>
        /// Closes the notification and applies changes to the input view
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void BtnChangeMaskSize_Click(object sender, RoutedEventArgs e)
        {
            this.inputView.DefaultMaskSize = (int)sliderMaskSize.Value;
            this.inputView.ReConstructUI();
            this.CloseNotification();
        }
    }
}
