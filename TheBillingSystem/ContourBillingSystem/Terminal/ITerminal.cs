using ContourBillingSystem.ComponentStation;
using System;

namespace ContourBillingSystem.Terminal
{
    public interface ITerminal
    {
        ITerminalModel Model { get; }
        ISubscriberNumber SubscriberNumber { get; set; }
        Port PortUsed { get; set; }

        event EventHandler<Port> CallingPort;
        event EventHandler<Port> TakingPort;
        event EventHandler<Port> UntyingPort;
        event EventHandler<Port> TalkingPort;

        void RegisterEventHandlersForPort(Port port);
        void TakePort(Port port);
        void UntiePort(Port port);
        void CallPort(Port port);
        void TalkPort(Port port);

        string MessageConnect();
        string MessageDisconnect();
        string MessageCall();
        string MessageTalk();
    }
}