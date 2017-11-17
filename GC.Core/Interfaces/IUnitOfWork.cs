using System.Threading.Tasks;

namespace GC.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        int Complete();
    }
}
