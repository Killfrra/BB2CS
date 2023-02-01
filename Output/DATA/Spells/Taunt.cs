#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Taunt : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Taunt",
            BuffTextureName = "GSB_taunt.dds",
            PopupMessage = new[]{ "game_floatingtext_Taunted", },
        };
        bool removePart;
        Particle part;
        public override void OnActivate()
        {
            string attackerName; // UNUSED
            TeamId teamID;
            Particle part2; // UNUSED
            StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.StunnedOrSilencedOrTaunted);
            SetTaunted(owner, true);
            if(attacker is Champion)
            {
                ApplyAssistMarker(attacker, owner, 10);
            }
            attackerName = GetUnitSkinName(attacker);
            teamID = GetTeamID(attacker);
            this.removePart = false;
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.GalioIdolOfDurandMarker)) > 0)
            {
                SpellEffectCreate(out this.part, out _, "galio_taunt_unit_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, default, default, false, false);
                this.removePart = true;
            }
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ShenShadowDashCooldown)) > 0)
            {
                SpellEffectCreate(out this.part, out _, "Global_Taunt_multi_unit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out part2, out _, "shen_shadowDash_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                this.removePart = true;
            }
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.PuncturingTauntArmorDebuff)) > 0)
            {
                SpellEffectCreate(out this.part, out _, "global_taunt.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, false, default, default, false, false);
                this.removePart = true;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetTaunted(owner, false);
            if(this.removePart)
            {
                SpellEffectRemove(this.part);
            }
        }
        public override void OnUpdateStats()
        {
            if(attacker.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                SetTaunted(owner, true);
            }
        }
    }
}