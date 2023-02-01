#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaAriaofPerseveranceMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {40, 60, 80, 100, 120};
        int[] effect1 = {8, 11, 14, 17, 20};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float aPMod;
            Particle self; // UNUSED
            float nextBuffVars_DefenseBonus;
            aPMod = GetFlatMagicDamageMod(attacker);
            aPMod *= 0.25f;
            IncHealth(target, aPMod + this.effect0[level], attacker);
            SpellEffectCreate(out self, out _, "Global_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false, false, false, false, false);
            ApplyAssistMarker(attacker, target, 10);
            nextBuffVars_DefenseBonus = this.effect1[level];
            AddBuff(attacker, target, new Buffs.SonaAriaShield(nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}