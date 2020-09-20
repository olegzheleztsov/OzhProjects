using HtmlAgilityPack;
using NuclearWar.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NuclearWar.Services
{
    public class WebService : IWebService
    {
        public string CapitalsUrl { get; }
        public WebService(string capitalsUrl )
        {
            CapitalsUrl = capitalsUrl;
        }

        public async Task<IEnumerable<Capital>> GetCapitalsAsync()
        {
            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(CapitalsUrl).ConfigureAwait(false);
            var nodes = document.DocumentNode.SelectNodes("//table/tr").Where(node =>
            {
                return node.SelectNodes("td").All(tdNode => !tdNode.HasAttributes);
            });
            var converter = new StringToCoordinateConverter();

            var result = new List<Capital>();

            foreach (var node in nodes)
            {
                var tdNodes = node.SelectNodes("td");
                if (tdNodes.Count == 4)
                {
                    string country = tdNodes[0].InnerText;
                    string capital = tdNodes[1].InnerText;
                    double latitude = 0.0, longitude = 0.0;
                    bool convertSuccess = true;
                    try
                    {
                        (latitude, longitude) =
                            converter.Convert(tdNodes[2].InnerText.Trim(), tdNodes[3].InnerText.Trim());
                    }
                    catch
                    {
                        convertSuccess = false;
                    }

                    if (convertSuccess)
                    {
                        result.Add(new Capital(country, capital, latitude, longitude));
                    }
                }
            }

            return result;
        }


    }
}
