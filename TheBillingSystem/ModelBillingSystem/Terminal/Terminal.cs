using ContourBillingSystem;
using ContourBillingSystem.ComponentStation;
using ContourBillingSystem.Terminal;
using System;

namespace ModelBillingSystem.Terminal
{
    public class Terminal : ITerminal
    {
        public ITerminalModel Model { get; }
        public ISubscriberNumber SubscriberNumber { get; set; }
        public Port PortUsed { get; set; }
        public event EventHandler<Port> CallingPort;
        public event EventHandler<Port> TakingPort;
        public event EventHandler<Port> UntyingPort;
        public event EventHandler<Port> TalkingPort;


        public Terminal(ITerminalModel model, ISubscriberNumber subscriberNumber)
        {
            Model = model;
            SubscriberNumber = subscriberNumber;
            Port? PortUsed = null;
        }


        public void RegisterEventHandlersForPort(Port port)
        {
            port.StatusChanged += (sender, state) =>
            {
                if (state == PortStatus.Free)
                {
                    OnTakePort(sender, port);
                }
            };
        }

        public void TakePort(Port port)
        {
            port.Status = PortStatus.Busy;
            PortUsed = port;
            OnTakePort(this, port);
        }

        protected virtual void OnTakePort(object sender, Port port)
        {
            TakingPort?.Invoke(sender, port);
        }

        public void UntiePort(Port port)
        {
            port.Status = PortStatus.Free;
            PortUsed = port;
            OnUntiePort(this, port);
        }

        protected virtual void OnUntiePort(object sender, Port port)
        {
            UntyingPort?.Invoke(sender, port);
        }

        public void CallPort(Port port)
        {
            port.Status = PortStatus.Call;
            PortUsed = port;
            OnCallPort(this, port);
        }

        protected virtual void OnCallPort(object sender, Port port)
        {
            CallingPort?.Invoke(sender, port);
        }

        public void TalkPort(Port port)
        {
            port.Status = PortStatus.Talk;
            PortUsed = port;
            OnTalkPort(this, port);
        }

        protected virtual void OnTalkPort(object sender, Port port)
        {
            TalkingPort?.Invoke(sender, port);
        }


        public override string ToString()
        {
            return string.Join(" ", Model.ToString(), "port used:", PortUsed.Id);
        }


        public string MessageConnect()
        {
            return string.Join(string.Empty, "Terminal connected to the station. Station number ", SubscriberNumber.Number, " port ", PortUsed.Id, " used.");
        }

        public string MessageDisconnect()
        {
            return string.Join(string.Empty, "Terminal disconnected to the station. Station number ", SubscriberNumber.Number, " does't use port.");
        }

        public string MessageCall()
        {
            return "The terminal is in a call state.";
        }

        public string MessageTalk()
        {
            return "The terminal is in a talk state.";
        }
    }
}
