using System;

namespace ContourBillingSystem.Terminal
{
    public interface ITerminalModel
    {
        string Name { get; }
        string[] Localization { get; set; }
        Tuple<double, double> ScreenSize { get; }
        bool Speakerphone { get; }
    }
}
