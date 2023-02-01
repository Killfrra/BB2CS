#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VladimirTidesofBloodHeal : BBSpellScript
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
        float[] effect0 = {30, 57.5f, 85, 112.5f, 140};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float aPMod;
            Particle self; // UNUSED
            aPMod = GetFlatMagicDamageMod(attacker);
            aPMod *= 0.325f;
            IncHealth(target, aPMod + this.effect0[level], attacker);
            SpellEffectCreate(out self, out _, "BriefHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false);
            ApplyAssistMarker(attacker, target, 10);
        }
    }
}