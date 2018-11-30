using System;
using System.Linq;
using System.Text;
using ContourBillingSystem;
using ServiceBillingSystem.CustomExceptions;

namespace ServiceBillingSystem.ManagementDecisions
{
    public static class CallManagement
    {
        private const int MaxDay = 30;
        private const double MinCost = 0;


        public static string ViewCallLog(ISubscriberNumber subscriberNumber, int numberOfDays)
        {
            if (numberOfDays > MaxDay)
                throw new CallLogArgumentException(numberOfDays);

            StringBuilder tempBuilder = new StringBuilder();

            foreach (var log in subscriberNumber.CallLog.Where(log =>
                log.StartedAt > DateTime.Now - TimeSpan.FromDays(numberOfDays)))
            {
                tempBuilder.Append("Call to number ").Append(log.Called.Number).Append(", conversation duration ")
                    .Append(log.Duration.Minutes).Append(":").Append(log.Duration.Seconds).Append(" minutes, cost - ")
                    .Append(log.Price).Append(" rub.");
            }
            return CheckCallLogForEmpty(tempBuilder.ToString());
        }

        public static string ViewCallLogForTheDate(ISubscriberNumber subscriberNumber, DateTime date)
        {
            if ((DateTime.Now - date).Days > MaxDay)
                throw new CallLogArgumentException((DateTime.Now - date).Days);
            if (DateTime.Now < date)
                throw new CallLogArgumentException(date);

            StringBuilder tempBuilder = new StringBuilder();

            foreach (var log in subscriberNumber.CallLog.Where(log => log.StartedAt == date))
            {
                tempBuilder.Append("Call to number ").Append(log.Called.Number).Append(", conversation duration ")
                    .Append(log.Duration.Minutes).Append(":").Append(log.Duration.Seconds).Append(" minutes, cost - ")
                    .Append(log.Price).Append(" rub.");
            }
            return CheckCallLogForEmpty(tempBuilder.ToString());
        }

        public static string ViewCallLogForTheCost(ISubscriberNumber subscriberNumber, double cost)
        {
            if (cost < MinCost)
                throw new CallLogArgumentException(cost);

            StringBuilder tempBuilder = new StringBuilder();

            foreach (var log in subscriberNumber.CallLog.Where(log => Math.Abs(log.Price - cost) < 1e-10))
            {
                tempBuilder.Append("Call to number ").Append(log.Called.Number).Append(", conversation duration ")
                    .Append(log.Duration.Minutes).Append(":").Append(log.Duration.Seconds).Append(" minutes, cost - ")
                    .Append(log.Price).Append(" rub.");
            }
            return CheckCallLogForEmpty(tempBuilder.ToString());
        }

        public static string ViewCallLogForTheLessCost(ISubscriberNumber subscriberNumber, double cost)
        {
            if (cost < MinCost)
                throw new CallLogArgumentException(cost);

            StringBuilder tempBuilder = new StringBuilder();

            foreach (var log in subscriberNumber.CallLog.Where(log => log.Price < cost))
            {
                tempBuilder.Append("Call to number ").Append(log.Called.Number).Append(", conversation duration ")
                    .Append(log.Duration.Minutes).Append(":").Append(log.Duration.Seconds).Append(" minutes, cost - ")
                    .Append(log.Price).Append(" rub.");
            }
            return CheckCallLogForEmpty(tempBuilder.ToString());
        }

        public static string ViewCallLogForTheMuchCost(ISubscriberNumber subscriberNumber, double cost)
        {
            if (cost < MinCost)
                throw new CallLogArgumentException(cost);

            StringBuilder tempBuilder = new StringBuilder();

            foreach (var log in subscriberNumber.CallLog.Where(log => log.Price > cost))
            {
                tempBuilder.Append("Call to number ").Append(log.Called.Number).Append(", conversation duration ")
                    .Append(log.Duration.Minutes).Append(":").Append(log.Duration.Seconds).Append(" minutes, cost - ")
                    .Append(log.Price).Append(" rub.");
            }
            return CheckCallLogForEmpty(tempBuilder.ToString());
        }

        public static string ViewCallLogForTheRangeCost(ISubscriberNumber subscriberNumber, double fromCost, double toCost)
        {
            if (fromCost < MinCost)
                throw new CallLogArgumentException(fromCost);
            if (toCost < MinCost)
                throw new CallLogArgumentException(toCost);

            if (toCost < fromCost)
            {
                double temp = toCost;
                toCost = fromCost;
                fromCost = temp;
            }

            StringBuilder tempBuilder = new StringBuilder();

            foreach (var log in subscriberNumber.CallLog.Where(log => log.Price > fromCost && log.Price < toCost))
            {
                tempBuilder.Append("Call to number ").Append(log.Called.Number).Append(", conversation duration ")
                    .Append(log.Duration.Minutes).Append(":").Append(log.Duration.Seconds).Append(" minutes, cost - ")
                    .Append(log.Price).Append(" rub.");
            }
            return CheckCallLogForEmpty(tempBuilder.ToString());
        }

        public static string ViewCallLogBySubscriber(ISubscriberNumber subscriberNumber, int calledSubscriberNumber)
        {
            StringBuilder tempBuilder = new StringBuilder();

            foreach (var log in subscriberNumber.CallLog.Where(log => log.Called.Number == calledSubscriberNumber))
            {
                tempBuilder.Append("Call to number ").Append(log.Called.Number).Append(", conversation duration ")
                    .Append(log.Duration.Minutes).Append(":").Append(log.Duration.Seconds).Append(" minutes, cost - ")
                    .Append(log.Price).Append(" rub.");
            }
            return CheckCallLogForEmpty(tempBuilder.ToString());
        }

        private static string CheckCallLogForEmpty(string callLog)
        {
            if (callLog == string.Empty)
                callLog = "No calls";
            return callLog;
        }
    }
}
