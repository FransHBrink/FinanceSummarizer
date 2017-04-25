
using System.ComponentModel;

namespace FinanceSummarizer.Enums
{
    public enum TransactionTypes
    {
        [Description("TFR IN")]
        TfrIn = 0,

        [Description("EFTPOS")]
        Eftpos = 1,

        [Description("TFR OUT")]
        TfrOut = 2,

        [Description("D/C")]
        Dc = 3,

        [Description("D/D")]
        Dd = 4,

        [Description("DEBIT")]
        Debit = 5,

        [Description("CREDIT")]
        Credit = 6
    }
}
