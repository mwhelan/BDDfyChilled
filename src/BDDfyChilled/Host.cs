using NUnit.Framework;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace BDDfyChilled
{
    [SetUpFixture]
    public class Host
    {
        [SetUp]
        public void SetUp()
        {
            Configurator.Scanners.DefaultMethodNameStepScanner.Disable();
            Configurator.Scanners.Add(() => new ChillMethodNameStepScanner());

            Configurator.Scanners.StoryMetadataScanner = () => new ChillStoryMetadataScanner();

            Configurator.BatchProcessors.HtmlReport.Disable();
            Configurator.BatchProcessors.Add(new HtmlReporter(new ChillBDDfyReportConfig()));
        }
    }
}