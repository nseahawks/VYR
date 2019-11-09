using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VYRMobile.ViewModels
{
    public partial class ReportViewModel
    {
        [JsonProperty("reports")]
        public Report[] Reports { get; set; }
    }

    public partial class Report
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("resolveDate")]
        public DateTimeOffset ResolveDate { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }

    public partial class ReportViewModel
    {
        public static ReportViewModel FromJson(string json) => JsonConvert.DeserializeObject<ReportViewModel>(json, VYRMobile.ViewModels.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ReportViewModel self) => JsonConvert.SerializeObject(self, VYRMobile.ViewModels.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}