using ContourBillingSystem;
using ContourBillingSystem.Contracts;
using ContourBillingSystem.Terminal;
using System;
using System.Text;

namespace ModelBillingSystem.Contracts
{
    public class TerminalContract : Contract, ITerminalContract
    {
        public ITerminal Terminal { get; set; }
        public event EventHandler<ITerminal> OnContract;


        public TerminalContract(ITerminal terminal, Tuple<IBillingCompany, IClient> parties) : base(parties)
        {
            Terminal = terminal;
            OnContract?.Invoke(this, terminal);
        }

        public TerminalContract(ITerminal terminal, Tuple<IBillingCompany, IClient> parties, DateTime dateOfSigning) : base(parties, dateOfSigning)
        {
            Terminal = terminal;
            OnContract(this, terminal);
        }


        public override string ToString()
        {
            StringBuilder tempBuilder = new StringBuilder();
            tempBuilder.Append(base.ToString()).Append(", issued terminal: ").Append(Terminal).Append(", number: ").Append(Terminal.SubscriberNumber.Number).
                Append(", rate: ").Append(Terminal.SubscriberNumber.Rate.Name).Append(" (").Append(Terminal.SubscriberNumber.Rate.SubscriptionFee).Append(" rub/min)");
            return tempBuilder.ToString();
        }
    }
}
