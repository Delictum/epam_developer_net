namespace ContourBillingSystem
{
    public interface IRate
    {
        string Name { get; }
        double SubscriptionFee { get; }
    }
}