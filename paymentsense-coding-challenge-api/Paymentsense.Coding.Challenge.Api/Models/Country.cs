using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class Country
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        //[JsonPropertyName("topLevelDomain")]
        //public List<string> TopLevelDomain { get; set; }

        [JsonPropertyName("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonPropertyName("alpha3Code")]
        public string Alpha3Code { get; set; }

        //[JsonPropertyName("callingCodes")]
        //public List<string> CallingCodes { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        //[JsonPropertyName("subregion")]
        //public string Subregion { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        //[JsonPropertyName("latlng")]
        //public List<double> Latlng { get; set; }

        //[JsonPropertyName("demonym")]
        //public string Demonym { get; set; }

        [JsonPropertyName("area")]
        public double? Area { get; set; }

        //[JsonPropertyName("gini")]
        //public double? Gini { get; set; }

        [JsonPropertyName("timezones")]
        public List<string> Timezones { get; set; }

        [JsonPropertyName("borders")]
        public List<string> Borders { get; set; }

        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }

        //[JsonPropertyName("numericCode")]
        //public string NumericCode { get; set; }

        [JsonPropertyName("currencies")]
        public List<Currency> Currencies { get; set; }

        [JsonPropertyName("languages")]
        public List<Language> Languages { get; set; }

        //[JsonPropertyName("translations")]
        //public Translations Translations { get; set; }

        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        //[JsonPropertyName("regionalBlocs")]
        //public List<RegionalBloc> RegionalBlocs { get; set; }

        //[JsonPropertyName("cioc")]
        //public string Cioc { get; set; }
    }
}
