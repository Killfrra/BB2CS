#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonDamageAmp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "talon_E_tar_dmg.troy", },
            BuffName = "TalonDamageAmp",
            BuffTextureName = "TalonCutthroat.dds",
        };
        float ampValue;
        public TalonDamageAmp(float ampValue = default)
        {
            this.ampValue = ampValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.ampValue);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(attacker == caster)
            {
                damageAmount *= this.ampValue;
            }
        }
    }
}