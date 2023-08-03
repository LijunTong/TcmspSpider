using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcmsp.Domain.SpiderDomain;

namespace Tcmsp.Core.Interface
{
    public interface ISpider
    {
        /// <summary>
        /// 通过药品名字获取对应的药品成品以及对应靶点
        /// </summary>
        /// <param name="name">药品名字</param>
        /// <param name="ob">ob</param>
        /// <param name="dl">dl</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        (List<Ingredients> Ingredients, List<RelatedTargets> RelatedTargets) GetIngredientsAndTargets(string name, decimal ob, decimal dl, string token = "");
    }
}
