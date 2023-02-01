#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Ardor : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float percentMod;
        float aP;
        float aS;
        public Ardor(float percentMod = default)
        {
            this.percentMod = percentMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.percentMod);
            this.aP = 0;
            this.aS = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.aP);
            IncPercentAttackSpeedMod(owner, this.aS);
        }
        public override void OnUpdateActions()
        {
            float abilityPowerStart;
            float attackSpeedStart;
            abilityPowerStart = GetFlatMagicDamageMod(owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ZhonyasRing)) > 0)
            {
                abilityPowerStart /= 1.3f;
            }
            abilityPowerStart -= this.aP;
            this.aP = abilityPowerStart * this.percentMod;
            attackSpeedStart = GetPercentAttackSpeedMod(owner);
            attackSpeedStart -= this.aS;
            this.aS = attackSpeedStart * this.percentMod;
        }
    }
}