#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CassiopeiaTwinFang : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {60, 95, 130, 165, 200};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(HasBuffOfType(target, BuffType.POISON))
            {
                TeamId teamID;
                Particle particle; // UNUSED
                teamID = GetTeamID(attacker);
                SetSlotSpellCooldownTimeVer2(0.5f, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                SpellEffectCreate(out particle, out _, "CassioTwinFang_refreshsound.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            }
            BreakSpellShields(target);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.55f, 1, false, false, attacker);
        }
    }
}