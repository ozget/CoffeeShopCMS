using CoffeeShopCMS.Service.Provider;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CoffeeShopCMS.Infrastructure
{
    public class MernisService : IVerificationOperationsService
    {
        private const string baseUrl = "https://tckimlik.nvi.gov.tr/service/kpspublic.asmx";

        public MernisService()
        {
        }

        public bool IsCorrectTcFullNameBirtYear(long identityNo, string firstName, string lastName, int birthYear)
        {
            var xmlDocument = CreateXmlDocument(identityNo, firstName, lastName, birthYear);

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(baseUrl, new StringContent(xmlDocument.ToString(), Encoding.UTF8, "text/xml")).Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                return bool.Parse(System.Text.RegularExpressions.Regex.Replace(result.Result, "<.*?>", String.Empty));
            }
        }

        private string CreateXmlDocument(long tcNo, string firstName, string lastName, int birthYear)
        {
            var requestXml = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ws=""http://tckimlik.nvi.gov.tr/WS"">";

            requestXml += @"<soapenv:Header/>";

            requestXml += @"<soapenv:Body>";

            requestXml += @"<ws:TCKimlikNoDogrula>";

            requestXml += @"<ws:TCKimlikNo>" + tcNo + "</ws:TCKimlikNo>";

            requestXml += @"<ws:Ad>" + firstName + "</ws:Ad>";

            requestXml += @"<ws:Soyad>" + lastName + "</ws:Soyad>";

            requestXml += @"<ws:DogumYili>" + birthYear + "</ws:DogumYili>";

            requestXml += @"</ws:TCKimlikNoDogrula>";

            requestXml += @"</soapenv:Body>";

            requestXml += @"</soapenv:Envelope>";

            return requestXml;
        }
    }
}