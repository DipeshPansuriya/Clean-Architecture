using Application_Core.Interfaces;
using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application_Infrastructure.BackgroundTask
{
    public class BackgroundJob : IBackgroundJob
    {
        private IBackgroundJobClient _backgroundClient;

        public BackgroundJob(IBackgroundJobClient backgroundJobClient)
        {
            this._backgroundClient = backgroundJobClient;
        }

        public void AddEnque(Expression<Action> methodCall)
        {
            this._backgroundClient.Enqueue(methodCall);
        }

        public void AddEnque<T>(Expression<Func<T, Task>> methodCall)
        {
            this._backgroundClient.Enqueue<T>(methodCall);
        }

        public void AddSchedule<T>(Expression<Func<T, Task>> methodCall, RecuringTime recuringTime, double time)
        {
            switch (recuringTime)
            {
                case RecuringTime.Milliseconds:
                    this._backgroundClient.Schedule<T>(methodCall, TimeSpan.FromMilliseconds(time));
                    break;

                case RecuringTime.Seconds:
                    this._backgroundClient.Schedule<T>(methodCall, TimeSpan.FromSeconds(time));
                    break;

                case RecuringTime.Minutes:
                    this._backgroundClient.Schedule<T>(methodCall, TimeSpan.FromMinutes(time));
                    break;

                case RecuringTime.Hours:
                    this._backgroundClient.Schedule<T>(methodCall, TimeSpan.FromHours(time));
                    break;

                case RecuringTime.Day:
                    this._backgroundClient.Schedule<T>(methodCall, TimeSpan.FromDays(time));
                    break;

                default:
                    this._backgroundClient.Schedule<T>(methodCall, TimeSpan.FromMinutes(time));
                    break;
            }
        }

        public void AddSchedule(Expression<Func<Task>> methodCall, RecuringTime recuringTime, double time)
        {
            switch (recuringTime)
            {
                case RecuringTime.Milliseconds:
                    this._backgroundClient.Schedule(methodCall, TimeSpan.FromMilliseconds(time));
                    break;

                case RecuringTime.Seconds:
                    this._backgroundClient.Schedule(methodCall, TimeSpan.FromSeconds(time));
                    break;

                case RecuringTime.Minutes:
                    this._backgroundClient.Schedule(methodCall, TimeSpan.FromMinutes(time));
                    break;

                case RecuringTime.Hours:
                    this._backgroundClient.Schedule(methodCall, TimeSpan.FromHours(time));
                    break;

                case RecuringTime.Day:
                    this._backgroundClient.Schedule(methodCall, TimeSpan.FromDays(time));
                    break;

                default:
                    this._backgroundClient.Schedule(methodCall, TimeSpan.FromMinutes(time));
                    break;
            }
        }
    }
}