namespace TPUM.Data.Interfaces
{
    public interface IDataContextFiller
    {
        DataContext Fill();

        static bool WasUsed;
    }
}