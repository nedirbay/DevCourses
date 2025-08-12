using DevCourses.DataAccess.Repository;
using DevCourses.Domain.Entities;
using YourNamespace.Helpers;

namespace DevCourses.Business.Services
{
    public interface ILogService
    {
        public Task<PaginatedList<Log>> GetLogs(int page, int size, CancellationToken token);
    }
    public class LogService : ILogService
    {
        private readonly IlogRepo _logRepo;

        public LogService(IlogRepo logRepo)
        {
            _logRepo = logRepo;
        }

        public async Task<PaginatedList<Log>> GetLogs(int page, int size, CancellationToken token)
        {
            return await _logRepo.GetAllLogs(page,size,token);
        }
    }
}
