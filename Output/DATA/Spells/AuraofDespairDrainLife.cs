#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AuraofDespairDrainLife : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "Despair_tar.troy", },
            BuffName = "AuraofDespairDamage",
            BuffTextureName = "SadMummy_AuraofDespair.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lifeLossPercent;
        float lastTimeExecuted;
        public AuraofDespairDrainLife(float lifeLossPercent = default)
        {
            this.lifeLossPercent = lifeLossPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifeLossPercent);
        }
        public override void OnUpdateActions()
        {
            float temp1;
            float percentDamage;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                temp1 = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                percentDamage = temp1 * this.lifeLossPercent;
                ApplyDamage(attacker, owner, percentDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, default, false, false);
            }
        }
    }
}