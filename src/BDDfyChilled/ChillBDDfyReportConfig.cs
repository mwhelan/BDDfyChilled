using TestStack.BDDfy.Reporters.Html;

namespace BDDfyChilled
{
    public class ChillBDDfyReportConfig : DefaultHtmlReportConfiguration
    {
        public override string ReportHeader { get { return "BDDfy Chilled!"; }}
    }
}
