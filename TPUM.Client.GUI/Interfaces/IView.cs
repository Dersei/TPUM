namespace TPUM.Client.GUI.Interfaces
{
    public interface IView
    {
        bool? ShowDialog();
        void Close();
        object DataContext { get; }
    }
}