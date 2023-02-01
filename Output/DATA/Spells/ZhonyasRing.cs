#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ZhonyasRing : BBBuffScript
    {
        float abilityPower;
        public override void OnActivate()
        {
            float aP;
            aP = GetFlatMagicDamageMod(owner);
            this.abilityPower = aP * 0.3f;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.abilityPower);
        }
        public override void OnUpdateActions()
        {
            float aP;
            aP = GetFlatMagicDamageMod(owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MasteryBlastBuff)) > 0)
            {
                aP /= 1.04f;
            }
            aP -= this.abilityPower;
            this.abilityPower = aP * 0.3f;
        }
    }
}