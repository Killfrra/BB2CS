#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class DeathsCaressFull : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {100, 150, 200, 250, 300};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float abilityPower;
            float armorAmount;
            float bonusHealth;
            float totalArmorAmount;
            float nextBuffVars_TotalArmorAmount;
            float nextBuffVars_FinalArmorAmount;
            float nextBuffVars_Ticktimer;
            abilityPower = GetFlatMagicDamageMod(target);
            armorAmount = this.effect0[level];
            bonusHealth = abilityPower * 0.9f;
            totalArmorAmount = bonusHealth + armorAmount;
            nextBuffVars_TotalArmorAmount = totalArmorAmount;
            nextBuffVars_FinalArmorAmount = totalArmorAmount;
            nextBuffVars_Ticktimer = 10;
            AddBuff(attacker, target, new Buffs.DeathsCaress(nextBuffVars_TotalArmorAmount, nextBuffVars_FinalArmorAmount, nextBuffVars_Ticktimer), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}