using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application_Core.Interfaces
{
    public interface IBackgroundJob
    {
        void AddEnque(Expression<Action> methodCall);

        void AddEnque<T>(Expression<Func<T, Task>> methodCall);

        void AddSchedule<T>(Expression<Func<T, Task>> methodCall, RecuringTime recuringTime, double time);

        void AddSchedule(Expression<Func<Task>> methodCall, RecuringTime recuringTime, double time);
    }

    public enum RecuringType
    {
        Daily,
        Minutely,
        Hourly,
        Weekly,
        Monthly,
        Yearly
    }

    public enum RecuringTime
    {
        Milliseconds,
        Seconds,
        Minutes,
        Hours,
        Day
    }
}