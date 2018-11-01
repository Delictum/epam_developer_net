namespace NewYearsGift.Model
{
    public interface ISweetnessWithFilling
    {
        System.Enum Filling { get; }

        void AlterFilling(System.Enum newFilling);
    }
}