using System.Windows;
using TPUM.GUI.ViewModel;

namespace TPUM.GUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (DataContext as MainViewModel).DoClose;
        }
    }
}
