#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VladimirTransfusionHeal : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 0f, 0f, 0f, 0f, 0f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {15, 25, 35, 45, 55};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseHeal;
            float abilityPower;
            float abilityPowerMod;
            float totalHeal;
            Particle ar; // UNUSED
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseHeal = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(attacker);
            abilityPowerMod = abilityPower * 0.25f;
            totalHeal = abilityPowerMod + baseHeal;
            IncHealth(target, totalHeal, attacker);
            SpellEffectCreate(out ar, out _, "VampHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
        }
    }
}