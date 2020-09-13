namespace TPUM.Client.Data.Interfaces
{
    public interface IDataContextFiller
    {
        DataContext Fill();

        static bool WasUsed;
    }
}