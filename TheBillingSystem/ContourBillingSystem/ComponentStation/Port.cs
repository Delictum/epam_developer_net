using System;

namespace ContourBillingSystem.ComponentStation
{
    public struct Port
    {
        public int? Id { get; set; }
        public PortStatus Status { get; set; }
        public event EventHandler<PortStatus> StatusChanging;
        public event EventHandler<PortStatus> StatusChanged;

        public Port(int id)
        {
            Id = id;
            Status = PortStatus.Free;
            StatusChanging = null;
            StatusChanged = null;
        }

        public void OnStateChanging(object sender, PortStatus newStatus)
        {
            StatusChanging?.Invoke(sender, newStatus);
        }

        public void OnStateChanged(object sender, PortStatus status)
        {
            StatusChanged?.Invoke(sender, status);
        }

        public override string ToString()
        {
            return string.Join("-", Id, Status);
        }
    }
}
