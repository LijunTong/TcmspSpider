using Newtonsoft.Json;

namespace Tcmsp.Domain.SpiderDomain;

public class Ingredients
{
    [JsonProperty("molecule_ID")]
    public string MoleculeID { get; set; }

    [JsonProperty("MOL_ID")]
    public string MolID { get; set; }

    [JsonProperty("file_ID")]
    public string FileID { get; set; }

    [JsonProperty("molecule_name")]
    public string MoleculeName { get; set; }

    [JsonProperty("ob")]
    public string Ob { get; set; }

    [JsonProperty("mw")]
    public string Mw { get; set; }

    [JsonProperty("alogp")]
    public string Alogp { get; set; }

    [JsonProperty("caco2")]
    public string Caco2 { get; set; }

    [JsonProperty("bbb")]
    public string Bbb { get; set; }

    [JsonProperty("halflife")]
    public string HalfLife { get; set; }

    [JsonProperty("hdon")]
    public string Hdon { get; set; }

    [JsonProperty("hacc")]
    public string Hacc { get; set; }

    [JsonProperty("dl")]
    public string Dl { get; set; }

    [JsonProperty("FASA")]
    public string Fasa { get; set; }
}