#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VoidStoneAttackSpeedBoost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VoidStoneEmpowered",
            BuffTextureName = "Voidwalker_Netherburn.dds",
        };
        float attackSpeedBoost;
        public override void OnActivate()
        {
            this.attackSpeedBoost = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            this.attackSpeedBoost = 0;
        }
        public override void OnUpdateStats()
        {
            float tooltipAttackSpeed;
            IncPercentAttackSpeedMod(owner, this.attackSpeedBoost);
            tooltipAttackSpeed = this.attackSpeedBoost * 100;
            SetBuffToolTipVar(1, tooltipAttackSpeed);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float attackSpeedIncrement;
            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                attackSpeedIncrement = damageAmount * 0.0015f;
                this.attackSpeedBoost += attackSpeedIncrement;
            }
        }
    }
}