using AddressValidation.Settings;
using AdressValidation.Model;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Xml.Linq;

namespace AdressValidation.Service
{
    public class USPSClient : IUSPSClient
    {
        private readonly string _userId;
        private readonly string _baseUrl;
        private const string ApiAction = "Verify";

        public USPSClient(IOptions<Settings> settings)
        {
            _userId = settings.Value.USPSUserId;
            _baseUrl = settings.Value.USPSBaseUrl;
        }

        public bool ValidateAddress(AddressModel address)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("", Method.Get);
            request.AddParameter("API", ApiAction);
            request.AddParameter("XML", BuildRequestXml(address));

            var response = client.Execute(request);
            return IsValidResponse(response.Content);
        }

        private string BuildRequestXml(AddressModel address)
        {
            return $"<AddressValidateRequest USERID=\"{_userId}\"><Address><Address1></Address1><Address2>{address.Address}</Address2><City>{address.City}</City><State>{address.State}</State><Zip5>{address.Zip}</Zip5><Zip4></Zip4></Address></AddressValidateRequest>";
        }

        private bool IsValidResponse(string responseContent)
        {
            var xml = XDocument.Parse(responseContent);
            var error = xml.Descendants("Error").FirstOrDefault();
            return error == null;
        }
    }
}
