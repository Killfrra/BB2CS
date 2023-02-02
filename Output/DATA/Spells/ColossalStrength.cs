#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ColossalStrength : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Colossal Strength",
            BuffTextureName = "Minotaur_ColossalStrength.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int lastF1; // UNUSED
        float lastTimeExecuted;
        int[] effect0 = {10, 11, 12, 12, 13, 14, 15, 15, 16, 17, 18, 18, 19, 20, 21, 21, 22, 23};
        public override void OnActivate()
        {
            this.lastF1 = 0;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, true))
            {
                int level;
                float damageByRank;
                float totalAttackDamage;
                float baseAttackDamage;
                float abilityPower;
                float bonusAttackDamage;
                float attackDamageToAdd;
                float abilityPowerToAdd;
                float damageToDeal;
                float currentDamage;
                level = GetLevel(owner);
                damageByRank = this.effect0[level];
                totalAttackDamage = GetTotalAttackDamage(owner);
                baseAttackDamage = GetBaseAttackDamage(owner);
                abilityPower = GetFlatMagicDamageMod(owner);
                bonusAttackDamage = totalAttackDamage - baseAttackDamage;
                attackDamageToAdd = bonusAttackDamage * 0;
                abilityPowerToAdd = abilityPower * 0.1f;
                damageToDeal = damageByRank + abilityPowerToAdd;
                currentDamage = damageToDeal + attackDamageToAdd;
                SetBuffToolTipVar(1, currentDamage);
                SetBuffToolTipVar(2, damageByRank);
                SetBuffToolTipVar(3, abilityPowerToAdd);
            }
        }
    }
}