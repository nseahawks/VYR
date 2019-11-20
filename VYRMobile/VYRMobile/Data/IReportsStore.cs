using System.Collections.Generic;
using System.Threading.Tasks;

namespace VYRMobile.Data
{
    public interface IReportsStore<T>
    {
        Task<bool> AddReportAsync(T report);
        Task<bool> UpdateReportAsync(T report);
        Task<bool> DeleteReportAsync(string id);
        Task<T> GetReportAsync(string id);
        Task<IEnumerable<T>> GetReportsAsync(bool forceRefresh = false);
    }
}
