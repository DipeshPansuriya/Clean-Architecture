using Application_Core.Background;
using Hangfire;
using System;
using System.Linq.Expressions;

namespace Application_Infrastructure.Background
{
    public class BackgroundJob : IBackgroundJob
    {
        private readonly IBackgroundJobClient _backgroundClient;

        public BackgroundJob(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundClient = backgroundJobClient;
        }

        public string AddEnque(Expression<Action> methodCall)
        {
            return _backgroundClient.Enqueue(methodCall);
        }

        public string AddEnque<T>(Expression<Action<T>> methodCall)
        {
            return _backgroundClient.Enqueue<T>(methodCall);
        }

        public string AddContinuations(Expression<Action> methodCall, string jobid)
        {
            return _backgroundClient.ContinueJobWith(jobid, methodCall);
        }

        public string AddContinuations<T>(Expression<Action<T>> methodCall, string jobid)
        {
            return _backgroundClient.ContinueJobWith<T>(jobid, methodCall);
        }

        public string AddSchedule(Expression<Action> methodCall, RecuringTime recuringTime, double time)
        {
            switch (recuringTime)
            {
                case RecuringTime.Milliseconds:
                    return _backgroundClient.Schedule(methodCall, TimeSpan.FromMilliseconds(time));

                case RecuringTime.Seconds:
                    return _backgroundClient.Schedule(methodCall, TimeSpan.FromSeconds(time));

                case RecuringTime.Minutes:
                    return _backgroundClient.Schedule(methodCall, TimeSpan.FromMinutes(time));

                case RecuringTime.Hours:
                    return _backgroundClient.Schedule(methodCall, TimeSpan.FromHours(time));

                case RecuringTime.Day:
                    return _backgroundClient.Schedule(methodCall, TimeSpan.FromDays(time));

                default:
                    return _backgroundClient.Schedule(methodCall, TimeSpan.FromMinutes(time));
            }
        }

        public string AddSchedule<T>(Expression<Action<T>> methodCall, RecuringTime recuringTime, double time)
        {
            switch (recuringTime)
            {
                case RecuringTime.Milliseconds:
                    return _backgroundClient.Schedule<T>(methodCall, TimeSpan.FromMilliseconds(time));

                case RecuringTime.Seconds:
                    return _backgroundClient.Schedule<T>(methodCall, TimeSpan.FromSeconds(time));

                case RecuringTime.Minutes:
                    return _backgroundClient.Schedule<T>(methodCall, TimeSpan.FromMinutes(time));

                case RecuringTime.Hours:
                    return _backgroundClient.Schedule<T>(methodCall, TimeSpan.FromHours(time));

                case RecuringTime.Day:
                    return _backgroundClient.Schedule<T>(methodCall, TimeSpan.FromDays(time));

                default:
                    return _backgroundClient.Schedule<T>(methodCall, TimeSpan.FromMinutes(time));
            }
        }
    }
}