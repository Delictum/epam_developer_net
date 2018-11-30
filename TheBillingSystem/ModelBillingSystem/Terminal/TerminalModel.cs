using ContourBillingSystem.Terminal;
using System;

namespace ModelBillingSystem.Terminal
{
    public class TerminalModel : ITerminalModel
    {
        public string Name { get; }
        public string[] Localization { get; set; }
        public Tuple<double, double> ScreenSize { get; }
        public bool Speakerphone { get; }


        public TerminalModel(string name, string[] localization, Tuple<double, double> screenSize, bool speakerphone)
        {
            Name = name;
            Localization = localization;
            ScreenSize = screenSize;
            Speakerphone = speakerphone;
        }


        public override string ToString()
        {
            return string.Join(string.Empty, "Name: ", Name, "; localization: ", string.Join(", ", Localization), 
                "; screen size: ", ScreenSize, "; speakerphone: ", Speakerphone ? "yes" : "no");
        }
    }
}
