using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.BonusDtos
{
    public class GetBonusDto
    {
        public int BonusAverage { get; }
        public int OneStarCount { get; }
        public int TwoStarCount { get; }
        public int ThreeStarCount { get; }
        public int FourStarCount { get; }
        public int FiveStarCount { get; }

    }
}
