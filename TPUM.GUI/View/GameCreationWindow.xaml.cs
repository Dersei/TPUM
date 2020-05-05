using System.Windows;
using TPUM.GUI.Interfaces;

namespace TPUM.GUI.View
{
    /// <summary>
    /// Interaction logic for GameCreationWindow.xaml
    /// </summary>
    public partial class GameCreationWindow : IView
    {
        public GameCreationWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
