using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.Hint
{
    public class HintService : IHintService
    {
        public event Func<string, Task> OnHintShowed;
        public event Func<Task> OnHintHide;

        private const int HintShowDuration = 4 * 1000;
        private Queue<string> _queue;

        private bool _isDisplaying;


        public HintService()
            => _queue = new Queue<string>();

        public async void ShowHint(string value)
        {
            AddToQueue(value);

            if (_isDisplaying)
                return;

            _isDisplaying = true;
            await DisplayHint(_queue.Dequeue());
        }

        private async Task DisplayHint(string value)
        {
            await OnHintShowed?.Invoke(value)!;
            await Task.Delay(HintShowDuration);
            await OnHintHide?.Invoke()!;

            if (IsQueueEnded())
            {
                _isDisplaying = false;
                return;
            }

            string hint = _queue.Dequeue();
            await DisplayHint(hint);
        }

        private bool IsQueueEnded()
            => _queue.Count <= 0;

        private void AddToQueue(string value) 
            => _queue.Enqueue(value);
    }
}