using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcmsp
{
    public class RelatedTargets
    {
        /// <summary>
        /// 
        /// </summary>
        public string molecule_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MOL_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string molecule_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string target_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string target_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string drugbank_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string validated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SVM_score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RF_score { get; set; }
    }
}
