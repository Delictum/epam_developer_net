using System;
using ContourBillingSystem.Terminal;

namespace ContourBillingSystem.Contracts
{
    public interface ITerminalContract
    {
        ITerminal Terminal { get; set; }
        event EventHandler<ITerminal> OnContract;
    }
}
