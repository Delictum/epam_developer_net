using ContourBillingSystem;
using ContourBillingSystem.ComponentStation;
using ModelBillingSystem;
using ServiceBillingSystem.CustomExceptions;
using System.Collections.Generic;

namespace ServiceBillingSystem.ManagementDecisions
{
    public static class StationManagment
    {
        public static void CreateNewStation(IBillingCompany billingCompany, CodecType codecType)
        {
            billingCompany.Stations.Add(new Station(new List<Port>(), codecType));
        }

        public static void AddPort(IStation station, int id)
        {
            foreach (var port in station.Capacity)
            {
                if (port.Id == id || id < 1)
                {
                    throw new PortArgumentOutOfRangeException(id);
                }
            }
            station.Capacity.Add(new Port(id));
        }

        public static void AddPortRange(IStation station, int initialEntry, int finalEntry)
        {
            foreach (var port in station.Capacity)
            {
                if (port.Id >= initialEntry || port.Id <= finalEntry || initialEntry < 1)
                {
                    throw new PortArgumentOutOfRangeException(initialEntry, finalEntry);
                }
            }

            while (initialEntry <= finalEntry)
            {
                station.Capacity.Add(new Port(initialEntry++));
            }
        }

        public static string OutputAllPorts(IStation station)
        {
            return string.Join("\n", station.Capacity);
        }
    }
}
