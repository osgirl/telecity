using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace telecity
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var responseString = http.GetStringAsync(new Uri("http://elba-build-1.dev.kontur.ru" +
                                                         "/guestAuth/app/rest/buildQueue?locator=buildType:ROCK_Run"))
          .Result;
        var response = JsonConvert.DeserializeObject<BuildQueueResponse>(responseString);
        Console.Out.WriteLine(response.Count);
      }
    }
  }

  class BuildQueueResponse
  {
    public int Count { get; set; }
  }
}