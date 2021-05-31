using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API_Unit_Test.Helper
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        public string delimiter = SampleUnitTestVariablesAndMethods.GenericInfoTest.delimiter;

        public dynamic _result;
        public HttpStatusCode _httpStatusCode;

        public List<string> _listResults;
        public MockHttpMessageHandler(List<string> resultList,dynamic result, HttpStatusCode httpStatusCode)
        {
            _listResults = resultList;
            _result = result;
            _httpStatusCode = httpStatusCode;
        }
        public MockHttpMessageHandler(dynamic result, HttpStatusCode httpStatusCode)
        {
            _result = result;
            _httpStatusCode = httpStatusCode;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_listResults != null)
            {
                if (_listResults.Count > 0)
                {
                    string resultToSplit = _listResults.FirstOrDefault(x => x.Contains(request.RequestUri.AbsoluteUri));
                    _result = resultToSplit.Substring(0, resultToSplit.IndexOf(delimiter));
                }
            }
            else
            {
                try
                {
                    if (_result.Contains(request.RequestUri.AbsoluteUri))
                    {
                        _result = _result.Substring(0, _result.IndexOf(delimiter));
                    }
                }
                catch
                {
                    _result = null;
                }
            }

            var responseMessage = new HttpResponseMessage(_httpStatusCode);

            try
            {
                responseMessage.Content = new StringContent(_result);
            }
            catch
            {
                responseMessage.Content = _result;
            }

            return await Task.FromResult(responseMessage);
        }
    }
}

