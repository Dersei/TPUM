namespace TPUM.GUI.Interfaces
{
    public interface IView
    {
        bool? ShowDialog();
        object DataContext { get; }
    }
}