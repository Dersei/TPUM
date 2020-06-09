using System.Windows;
using TPUM.Client.GUI.Interfaces;

namespace TPUM.Client.GUI.View
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
