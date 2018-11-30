using ContourBillingSystem;
using ContourBillingSystem.ComponentStation;
using ContourBillingSystem.Terminal;
using ServiceBillingSystem.CustomExceptions;
using System;
using System.Linq;
using ModelBillingSystem;

namespace ServiceBillingSystem.ManagementDecisions
{
    public static class TerminalManagement
    {
        public delegate string DelegateMessage();
        public static event DelegateMessage EventMessage;

        private static readonly object someLock = new object();

        public static void AutoConnectTerminal(IStation station, ITerminal terminal)
        {
            if (terminal.PortUsed.Id == null)
            {
                lock (someLock)
                {
                    var firstFree = station.Capacity.FirstOrDefault(status => status.Status == PortStatus.Free);
                    if (firstFree.Id != 0)
                    {
                        TakePort(firstFree, terminal);
                        EventMessage += terminal.MessageConnect;
                    }
                    else
                    {
                        throw new FreePortNotFoundException();
                    }
                }
            }
            else
            {
                throw new ConnectTerminalException();
            }
        }

        public static void ConnectTerminal(IStation station, ITerminal terminal, int id)
        {
            if (terminal.PortUsed.Id == null)
            {
                foreach (var port in station.Capacity)
                {
                    if (port.Id == id && port.Status == PortStatus.Free)
                    {
                        lock (someLock)
                        {
                            var destinationPort = port;
                            TakePort(destinationPort, terminal);
                            EventMessage += terminal.MessageConnect;
                            return;
                        }
                    }
                }
                throw new PortArgumentOutOfRangeException(id);
            }
            throw new ConnectTerminalException();
        }

        public static void DisconnectTerminal(IStation station, ITerminal terminal)
        {
            if (terminal.PortUsed.Id != null)
            {
                lock (someLock)
                {
                    UntiePort(terminal.PortUsed, terminal);
                    EventMessage -= terminal.MessageConnect;
                    EventMessage += terminal.MessageDisconnect;
                }
            }
            else
            {
                throw new DisconnectTerminalException();
            }
        }

        public static void CallToSubscriber(IStation station, ITerminal callingTerminal, ITerminal calledTerminal)
        {
            if (callingTerminal.PortUsed.Id != null && calledTerminal.PortUsed.Id != null)
            {
                if (callingTerminal.PortUsed.Status == PortStatus.Busy &&
                    calledTerminal.PortUsed.Status == PortStatus.Busy)
                {
                    lock (someLock)
                    {
                        CallPort(callingTerminal.PortUsed, callingTerminal);
                        EventMessage += callingTerminal.MessageCall;
                    }
                    lock (someLock)
                    {
                        CallPort(calledTerminal.PortUsed, calledTerminal);
                        EventMessage += calledTerminal.MessageCall;
                    }
                }
                else
                {
                    throw new GeneralizedException("One or both terminals are not available for a call.");
                }
            }
            else
            {
                throw new DisconnectTerminalException();
            }
        }

        public static void AnswerTheCall(IStation station, ITerminal callingTerminal, ITerminal calledTerminal)
        {
            if (callingTerminal.PortUsed.Id != null && calledTerminal.PortUsed.Id != null)
            {
                if (callingTerminal.PortUsed.Status == PortStatus.Call &&
                    calledTerminal.PortUsed.Status == PortStatus.Call)
                {
                    lock (someLock)
                    {
                        callingTerminal.SubscriberNumber.CallLog.Add(new CallLog(callingTerminal.SubscriberNumber));
                        TalkPort(callingTerminal.PortUsed, callingTerminal);
                        EventMessage += callingTerminal.MessageTalk;
                    }
                    lock (someLock)
                    {
                        TalkPort(calledTerminal.PortUsed, calledTerminal);
                        EventMessage += calledTerminal.MessageTalk;
                    }
                }
                else
                {
                    throw new GeneralizedException("One or both terminals are not available to answer the call.");
                }
            }
            else
            {
                throw new DisconnectTerminalException();
            }
        }

        public static void CompleteCall(IStation station, ITerminal callingTerminal, ITerminal calledTerminal)
        {
            if (callingTerminal.PortUsed.Id != null && calledTerminal.PortUsed.Id != null)
            {
                if (callingTerminal.PortUsed.Status == PortStatus.Talk &&
                    calledTerminal.PortUsed.Status == PortStatus.Talk)
                {
                    lock (someLock)
                    {
                        TakePort(callingTerminal.PortUsed, callingTerminal);
                        EventMessage -= callingTerminal.MessageCall;
                        EventMessage -= calledTerminal.MessageTalk;
                    }
                    lock (someLock)
                    {
                        TakePort(calledTerminal.PortUsed, calledTerminal);
                        EventMessage -= calledTerminal.MessageCall;
                        EventMessage -= calledTerminal.MessageTalk;
                    }

                    TimeSpan duration = DateTime.Now - callingTerminal.SubscriberNumber
                                            .CallLog[callingTerminal.SubscriberNumber.CallLog.Count - 1].StartedAt;
                    double price = (duration.Minutes + 1) * callingTerminal.SubscriberNumber.Rate.SubscriptionFee;
                    callingTerminal.SubscriberNumber.CallLog[callingTerminal.SubscriberNumber.CallLog.Count - 1]
                        .FinalInit(duration, price);
                }
                else
                {
                    throw new GeneralizedException("One or both terminals are not available to complete the call.");
                }
            }
            else
            {
                throw new DisconnectTerminalException();
            }
        }

        private static void TakePort(Port port, ITerminal terminal)
        {
            terminal.TakePort(port);
            ProgramLog.Info(terminal.MessageConnect());
        }

        private static void UntiePort(Port port, ITerminal terminal)
        {
            terminal.UntiePort(port);
            ProgramLog.Info(terminal.MessageDisconnect());
        }

        private static void CallPort(Port port, ITerminal terminal)
        {
            terminal.CallPort(port);
            ProgramLog.Info(terminal.MessageCall());
        }

        private static void TalkPort(Port port, ITerminal terminal)
        {

            terminal.TalkPort(port);
            ProgramLog.Info(terminal.MessageTalk());
        }


        public static void OnEventMessage() => EventMessage?.Invoke();
    }
}
