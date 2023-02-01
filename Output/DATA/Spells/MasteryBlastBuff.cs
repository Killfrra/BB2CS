#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MasteryBlastBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float percentMod;
        float aP;
        public MasteryBlastBuff(float percentMod = default)
        {
            this.percentMod = percentMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.percentMod);
            this.aP = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.aP);
        }
        public override void OnUpdateActions()
        {
            float abilityPowerStart;
            abilityPowerStart = GetFlatMagicDamageMod(owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ZhonyasRing)) > 0)
            {
                abilityPowerStart /= 1.3f;
            }
            abilityPowerStart -= this.aP;
            this.aP = abilityPowerStart * this.percentMod;
        }
    }
}