using ContourBillingSystem;

namespace ModelBillingSystem
{
    public class Rate : IRate
    {
        public string Name { get; }
        public double SubscriptionFee { get; }


        public Rate(string name, double subscriptionFee)
        {
            Name = name;
            SubscriptionFee = subscriptionFee;
        }
    }
}
