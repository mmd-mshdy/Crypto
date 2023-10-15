using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;


await RunInBackground(TimeSpan.FromSeconds(4), () => currency());

async Task RunInBackground(TimeSpan time, Action period)
{
    var periodicTimer = new PeriodicTimer(time);
    while (await periodicTimer.WaitForNextTickAsync())
    {
        period();
    }
}
async Task currency()
{
    HttpClient Apiclient = new HttpClient();
    string url = "https://api.wallex.ir/v1/currencies/stats";
    HttpResponseMessage respons = await Apiclient.GetAsync(url);
    if (respons.IsSuccessStatusCode)
    {
        string apirespons = await respons.Content.ReadAsStringAsync();
        Console.WriteLine(apirespons);
        Console.WriteLine();
        Root? Deserialize = JsonConvert.DeserializeObject<Root>(apirespons);
        List<Data>? data = Deserialize.data;
        foreach (var item in data)
        {
            Console.WriteLine($"key:{item.key}");
            Console.WriteLine($"name:{item.name}");
            Console.WriteLine($"price:{item.price}");
            Console.WriteLine($"daily_high_price:{item.daily_high_price}");
            Console.WriteLine($"daily_low_price:{item.daily_low_price}");
            Console.WriteLine($"percent_change_1h:{item.percent_change_1h}");
            Console.WriteLine($"percent_change_24h:{item.percent_change_24h}");
            Console.WriteLine($" price_change_200d:{item.price_change_200d}");

            double? crypto = item.price + item.percent_change_1h;
            double? crypto2 = item.price - item.percent_change_1h;
            crypto = item.daily_high_price;
            crypto2 = item.daily_low_price;
            item.price_change_30d = (item.price_change_7d + item.daily_high_price) * 5;
            item.price_change_200d = item.percent_change_200d * item.price;
            Console.WriteLine(crypto);
            Console.WriteLine(crypto2);
            Console.WriteLine(item.price_change_60d);
            Console.WriteLine(item.price_change_200d);


        }

    }
}

public class Data
{
    public string? key { get; set; }
    public string? name { get; set; }
    public string? name_en { get; set; }
    public int? rank { get; set; }
    public double? dominance { get; set; }
    public double? volume_24h { get; set; }
    public double? market_cap { get; set; }
    public double? ath { get; set; }
    public double? atl { get; set; }
    public double? ath_change_percentage { get; set; }
    public DateTime? ath_date { get; set; }
    public double price { get; set; }
    public double? daily_high_price { get; set; }
    public double? daily_low_price { get; set; }
    public double? weekly_high_price { get; set; }
    public double? monthly_high_price { get; set; }
    public double? yearly_high_price { get; set; }
    public double? weekly_low_price { get; set; }
    public double? monthly_low_price { get; set; }
    public double? yearly_low_price { get; set; }
    public double? percent_change_1h { get; set; }
    public double? percent_change_24h { get; set; }
    public double? percent_change_7d { get; set; }
    public double? percent_change_14d { get; set; }
    public double? percent_change_30d { get; set; }
    public double? percent_change_60d { get; set; }
    public double? percent_change_200d { get; set; }
    public double? percent_change_1y { get; set; }
    public double? price_change_24h { get; set; }
    public double? price_change_7d { get; set; }
    public double? price_change_14d { get; set; }
    public double? price_change_30d { get; set; }
    public double? price_change_60d { get; set; }
    public double? price_change_200d { get; set; }
    public double? price_change_1y { get; set; }
    public double? max_supply { get; set; }
    public double? total_supply { get; set; }
    public double? circulating_supply { get; set; }
    public string? type { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}

public class ResultInfo
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int count { get; set; }
    public int total_count { get; set; }
}

public class Root
{
    public List<Data>? data { get; set; }
    public string? message { get; set; }
    public bool success { get; set; }
    public string? provider { get; set; }
    public ResultInfo? result_info { get; set; }
}