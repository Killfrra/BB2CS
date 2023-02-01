#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaSongofDiscordMissile : BBSpellScript
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
        float[] effect0 = {0.06f, 0.08f, 0.1f, 0.12f, 0.14f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            ApplyAssistMarker(attacker, target, 10);
            AddBuff(attacker, target, new Buffs.SonaSongofDiscordHaste(nextBuffVars_MoveSpeedMod), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false);
        }
    }
}