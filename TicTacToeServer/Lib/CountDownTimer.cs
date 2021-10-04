using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TicTacToeServer.Lib
{
    internal class CountDownTimer : IDisposable
    {
        internal Action OnTimeOut { get; set; }
        internal Double Duration { get; private set; }
        private Double _elapsed;
        private DateTime _startTime;
        private readonly Timer _timer;
        internal Boolean IsRunning { get; private set; }


        private Double _leftInterval => IsRunning ?
            Duration - (DateTime.Now - _startTime).TotalMilliseconds < 0 ?
            0 :
            Duration - (DateTime.Now - _startTime).TotalMilliseconds :
            Duration - _elapsed;

        private Int64 _gameId;

        public CountDownTimer(Double interval, Int64 gameId)
        {
            _timer = new Timer(interval);
            _gameId = gameId;
            IsRunning = false;
            Duration = interval;

            //_timer.Elapsed += OnTimeElapsed();

        }

    
        internal void Play()
        {
            lock(_timer)
            {
                if (IsRunning)
                {
                    throw new Exception("Timer Already Running");
                }

                IsRunning = true;
                _startTime = DateTime.Now;
                _timer.Start();
                _timer.Elapsed += TimerOnElapsed;
            }
        }

        internal void Pause()
        {
            lock (_timer)
            {
                IsRunning = false;
                _elapsed = (DateTime.Now - _startTime).TotalMilliseconds;
                _timer.Stop();
                _timer.Elapsed -= TimerOnElapsed;
            }
        }

        internal void Finish()
        {
            
        }

        private void TimerOnElapsed(Object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (_leftInterval <= 0)
            {
                Pause();
                OnTimeOut?.Invoke();
            }
            else
            {
                Console.WriteLine("Not Elapsed");
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
