using System;
using System.Threading.Tasks;
using DG.Tweening;

namespace Infrastructure.Services.Hint
{ 
    public interface IHintService : IService
    {
        void ShowHint(string value);
        event Func<Task> OnHintHide;
        event Func<string, Task> OnHintShowed;
    }
}