#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CaitlynAceintheHole : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChannelDuration = 1.25f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle particleID;
        public override void ChannelingStart()
        {
            FaceDirection(owner, target.Position);
            AddBuff(attacker, target, new Buffs.CaitlynAceintheHole(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            SpellEffectCreate(out this.particleID, out _, "caitlyn_laser_beam_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_5", default, target, "spine", default, false, false, false, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            bool isStealthed; // UNUSED
            TeamId team; // UNUSED
            isStealthed = GetStealthed(target);
            FaceDirection(owner, target.Position);
            team = GetTeamID(attacker);
            SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectRemove(this.particleID);
        }
        public override void ChannelingCancelStop()
        {
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 5);
            SpellEffectRemove(this.particleID);
            SpellBuffRemove(target, nameof(Buffs.CaitlynAceintheHole), (ObjAIBase)owner, 0);
        }
    }
}
namespace Buffs
{
    public class CaitlynAceintheHole : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", },
            AutoBuffActivateEffect = new[]{ "caitlyn_ace_target_indicator.troy", "caitlyn_ace_target_indicator_02.troy", },
            BuffName = "CaitlynAceintheHole",
            BuffTextureName = "Caitlyn_AceintheHole.dds",
        };
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId team;
            team = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(team, 50, owner, 4, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            Region nextBuffVars_BubbleID;
            nextBuffVars_BubbleID = this.bubbleID;
            AddBuff(attacker, owner, new Buffs.CaitlynAceintheHoleVisibility(nextBuffVars_BubbleID), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(3, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            bool zombie;
            zombie = GetIsZombie(owner);
            if(zombie)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
            }
        }
        public override void OnZombie()
        {
            StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
        }
    }
}