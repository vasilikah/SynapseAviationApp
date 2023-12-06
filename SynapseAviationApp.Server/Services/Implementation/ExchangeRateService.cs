using SynapseAviationApp.Server.Models;
using SynapseAviationApp.Server.Services.Interfaces;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Xml;

namespace SynapseAviationApp.Server.Services.Implementation
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IFileSystemService _fileSystemService;
        public ExchangeRateService(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public async Task<List<Kurs>> GetExcangeRates()
        {
            // Date range for exchange rates, that later we add into xml exchange model
            var startDate = "12.02.2010";
            var endDate = "15.02.2010";

            var fileName = "GetExchangeRateRequestModel.xml";
            var fullFilePath = await _fileSystemService.GetFileContentAsync(fileName);
            var fileAsString = File.ReadAllText(fullFilePath);

            fileAsString = fileAsString.Replace("{[StartDateValue]}", startDate);
            fileAsString = fileAsString.Replace("{[EndDateValue]}", endDate);

            // Creating an instance of HttpClient to send an HTTP request(for external url)
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            //this is the url for the HTTP request
            var uri = new Uri("https://www.nbrm.mk/klservice/kurs.asmx?op=GetExchangeRate");
            var requestHttpMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            // Set the content of the HTTP request with the XML content
            requestHttpMessage.Content = new StringContent(fileAsString, Encoding.UTF8, "text/xml");

            var responseHttpMessage = await client.SendAsync(requestHttpMessage);
            var kursList = new List<Kurs>();

            // Check if the HTTP response is successful
            if (responseHttpMessage == null || !responseHttpMessage.IsSuccessStatusCode)
            {
                
                throw new HttpRequestException("HTTP Request failed");
            }
            else
            {
                //success and  extract content from the HTTP response
                var contentAsString = await responseHttpMessage.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(contentAsString))
                {
                    // XML to JSON conversion
                    XmlDocument contentResponseDoc = new XmlDocument();
                    contentResponseDoc.LoadXml(contentAsString);
                    var childNodes = contentResponseDoc.ChildNodes;

                    var contentText = childNodes[1]?.InnerText;

                    XmlDocument contentDoc = new XmlDocument();
                    contentDoc.LoadXml(contentText);

                    var childs = contentDoc.FirstChild.SelectNodes("KursZbir");


                    foreach (XmlNode child in childs)
                    {
                        var kurs = new Kurs
                        {
                            RBr = int.Parse(child?.SelectSingleNode("RBr")?.InnerText),
                            Datum = DateTime.Parse(child?.SelectSingleNode("Datum")?.InnerText),
                            Valuta = double.Parse(child?.SelectSingleNode("Valuta")?.InnerText),
                            Oznaka = child?.SelectSingleNode("Oznaka")?.InnerText,
                            Drzava = child?.SelectSingleNode("Drzava")?.InnerText,
                            Nomin = double.Parse(child?.SelectSingleNode("Nomin")?.InnerText),
                            Sreden = double.Parse(child?.SelectSingleNode("Sreden")?.InnerText),
                            DrzavaAng = child?.SelectSingleNode("DrzavaAng")?.InnerText,
                            DrzavaAl = child?.SelectSingleNode("DrzavaAl")?.InnerText,
                            NazivMak = child?.SelectSingleNode("NazivMak")?.InnerText,
                            NazivAng = child?.SelectSingleNode("NazivAng")?.InnerText,
                            ValutaNaziv_AL = child?.SelectSingleNode("ValutaNaziv_AL")?.InnerText,
                            Datum_f = DateTime.Parse(child?.SelectSingleNode("Datum_f")?.InnerText)
                        };

                        kursList.Add(kurs);
                    }
                }
            }
            return kursList;
        }

    }
}
