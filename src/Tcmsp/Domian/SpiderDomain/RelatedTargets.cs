using Newtonsoft.Json;

namespace Tcmsp.Domian.SpiderDomain;

public class RelatedTargets
{
    [JsonProperty("molecule_ID")]
    public string MoleculeID { get; set; }

    [JsonProperty("MOL_ID")]
    public string MolID { get; set; }

    [JsonProperty("molecule_name")]
    public string MoleculeName { get; set; }

    [JsonProperty("target_name")]
    public string TargetName { get; set; }

    [JsonProperty("target_ID")]
    public string TargetID { get; set; }

    [JsonProperty("drugbank_ID")]
    public string DrugbankID { get; set; }

    [JsonProperty("validated")]
    public string Validated { get; set; }

    [JsonProperty("SVM_score")]
    public string SVMscore { get; set; }

    [JsonProperty("RF_score")]
    public string RFscore { get; set; }
}