using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace MobileManagementSystems.GlobalReferences
{
    public class PdfGeneratorData
    {
        public async Task<byte[]> GeneratePdfFromHtml(string htmlContent)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });

            await browser.CloseAsync();

            return pdfBytes;
        }
    }
}