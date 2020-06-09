using System.Windows;
using TPUM.Client.GUI.ViewModel;

namespace TPUM.Client.GUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (DataContext as MainViewModel)!.DoClose;
        }
    }
}
