using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace MobileManagementSystems.GlobalReferences
{
    public class PdfGeneratorData
    {
        public async Task<byte[]> GeneratePdfAsync(string htmlContent)
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);

            var launchOptions = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultRevision)
            };

            using (var browser = await Puppeteer.LaunchAsync(launchOptions))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetContentAsync(htmlContent);

                var pdfBytes = await page.PdfDataAsync(new PdfOptions
                {
                    Format = PaperFormat.A4,
                    PrintBackground = true
                });

                return pdfBytes;
            }
        }
    }
}