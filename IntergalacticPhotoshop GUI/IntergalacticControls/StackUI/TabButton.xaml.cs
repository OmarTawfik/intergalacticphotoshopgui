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
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticControls.Classes;
    using IntergalacticCore;

    /// <summary>
    /// Interaction logic for TabButton.xaml
    /// </summary>
    public partial class TabButton : StackButtonBase
    {
        /// <summary>
        /// Tab name
        /// </summary>
        private Tab tab;

        /// <summary>
        /// Initializes a new instance of the TabButton class
        /// </summary>
        /// <param name="tab">The tab</param>
        public TabButton(Tab tab)
        {
            InitializeComponent();
            this.tab = tab;
            this.SubViews.Add(new Image());
            this.SubViews[0].Width = 250;
            this.SubViews[0].Height = 150;
            ((Image)this.SubViews[0]).Stretch = Stretch.Uniform;
            this.UpdateSubview(tab);
            this.lblTitle.Content = tab.Name;

            if (tab.CanBeDeleted == false)
            {
                this.CloseBtn.Visibility = Visibility.Hidden;
            }

            Manager.Instance.OnOperationFinshed += this.UpdateThumbnail;
        }

        /// <summary>
        /// Gets or sets the tab name
        /// </summary>
        public Tab Tab
        {
            get { return this.tab; }
            set { this.tab = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this tab is selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.toggleRect.Visibility == System.Windows.Visibility.Visible;
            }

            set
            {
                if (value)
                {
                    this.toggleRect.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.toggleRect.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Updates the thumbnail after operations
        /// </summary>
        /// <param name="mng">The manager</param>
        /// <param name="operation">The operation</param>
        private void UpdateThumbnail(Manager mng, BaseOperation operation)
        {
            if (mng.CurrentTab.Name == this.tab.Name)
            {
                this.UpdateSubview(this.tab);
            }
        }

        /// <summary>
        /// Updates the image subview from the thumbnail of the given tab
        /// </summary>
        /// <param name="tab">The tab</param>
        private void UpdateSubview(Tab tab)
        {
            WriteableBitmap source = (WriteableBitmap)((WPFBitmap)tab.Thumbnails.Peek()).GetImageSource();
            this.SubViews[0].Width = source.PixelWidth;
            this.SubViews[0].Height = source.PixelHeight;
            ((Image)this.SubViews[0]).Source = source;
        }

        /// <summary>
        /// Gets called when the user presses close on the tab.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="e">Mouse Button Events Arguments.</param>
        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Manager.Instance.DeleteTab(this.tab.Name);
        }
    }
}
