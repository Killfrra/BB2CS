#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorDeathRayDOT : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Viktor_Laserhit.troy", "", },
            BuffName = "ViktorDeathRayBurning",
            BuffTextureName = "ViktorDeathRayAUG.dds",
        };
        float damageForDot;
        float lastTimeExecuted;
        public ViktorDeathRayDOT(float damageForDot = default)
        {
            this.damageForDot = damageForDot;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageForDot);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(caster, owner, this.damageForDot, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}