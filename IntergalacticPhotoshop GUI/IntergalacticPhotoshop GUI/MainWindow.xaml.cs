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
using IntergalacticCore2.Operations.PixelOperations;
using IPUI;


namespace IntergalacticPhotoshop_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new PopupViewManager(mainGrid);

            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button();
                btn.Content = i * 10;
                btn.Width = 150;
                btn.Height = 100;
                PanelButton tmp = new PanelButton();
                tmp.SubView = btn;

                verticalStackController1.AddPanelButton(tmp);
            }

            OperationCategory category = new OperationCategory("Pixel Ops", null);
            category.AddOperation(new BrightnessOperation());
            category.AddOperation(new BrightnessOperation());
            category.AddOperation(new BrightnessOperation());

            PanelButton po = new PanelButton();
            po.Category = category;

            verticalStackController1.AddPanelButton(po);
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            TestWindow p = new TestWindow();
            p.Show();
        }

        private void mainGrid_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PopupViewManager.CurrentPopupManager.ViewOperationInputView(new BrightnessOperation());
        }
    }
}
