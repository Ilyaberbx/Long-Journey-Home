using Data;

namespace Infrastructure.Services.GlobalProgress
{

    public interface IGlobalProgressService : IService
    {
        public GlobalPlayerProgress GlobalPlayerProgress { get; set; }
        
    }
}