#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OlafAxeExpirationTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle a; // UNUSED
        Particle particle1;
        Particle particle;
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(attacker);
            SpellEffectCreate(out this.a, out _, "olaf_axe_trigger.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, false);
            SpellEffectCreate(out this.particle1, out this.particle, "olaf_axe_totem_team_id_green.troy", "olaf_axe_totem_team_id_red.troy", teamOfOwner, 400, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, true);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle1);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OlafAxeExpirationTimer2(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -1);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
        }
        public override void OnUpdateActions()
        {
            float cooldown;
            TeamId teamID;
            Vector3 ownerPos;
            Particle a; // UNUSED
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, nameof(Buffs.OlafBerzerkerRage), true))
            {
                if(unit == attacker)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)unit, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    cooldown -= 6;
                    SetSlotSpellCooldownTime((ObjAIBase)unit, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldown);
                    teamID = GetTeamID(attacker);
                    ownerPos = GetUnitPosition(owner);
                    SpellEffectCreate(out a, out _, "olaf_axe_refresh_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                    if(teamID == TeamId.TEAM_BLUE)
                    {
                        SpellEffectCreate(out a, out _, "olaf_axe_trigger_02.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false, false);
                    }
                    else
                    {
                        SpellEffectCreate(out a, out _, "olaf_axe_trigger_02.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false, false);
                    }
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}