#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CassiopeiaPetrifyingGaze : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            targetPos = GetCastSpellTargetPos();
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CassiopeiaPetrifyingGaze : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        Particle turntostone;
        public override void OnActivate()
        {
            int level; // UNUSED
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
            PauseAnimation(owner, true);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellEffectCreate(out this.turntostone, out _, "TurnToStone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            Particle asfd; // UNUSED
            SetStunned(owner, false);
            SpellEffectCreate(out asfd, out _, "TurnBack.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            PauseAnimation(owner, false);
            SpellEffectRemove(this.turntostone);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
        }
    }
}