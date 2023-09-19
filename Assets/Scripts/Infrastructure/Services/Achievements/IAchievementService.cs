using System.Threading.Tasks;
using Data;

namespace Infrastructure.Services.Achievements
{
    public interface IAchievementService : IService
    {
        public Task Achieve(AchievementType type);
    }
}