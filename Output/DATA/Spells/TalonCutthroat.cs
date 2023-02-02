#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TalonCutthroat : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240};
        int[] effect1 = {1, 1, 1, 1, 1};
        int[] effect2 = {8, 12, 16, 20, 24};
        float[] effect3 = {1, 1.5f, 2, 2.5f, 3};
        float[] effect4 = {1.03f, 1.06f, 1.09f, 1.12f, 1.15f};
        public override void SelfExecute()
        {
            TeamId ownerTeam;
            Vector3 castPos;
            Particle p3; // UNUSED
            ownerTeam = GetTeamID(owner);
            castPos = GetUnitPosition(owner);
            SpellEffectCreate(out p3, out _, "talon_E_cast.troy", default, ownerTeam ?? TeamId.TEAM_NEUTRAL, 1, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float distance;
            float finalDistance;
            Vector3 targetPos;
            float damageVar;
            float silenceDur;
            float kIDamage;
            FaceDirection(owner, target.Position);
            distance = DistanceBetweenObjects("Owner", "Target");
            finalDistance = distance + 175;
            targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
            TeleportToPosition(owner, targetPos);
            damageVar = this.effect0[level];
            silenceDur = this.effect1[level];
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            kIDamage = this.effect2[level];
            damageVar += kIDamage;
            kIDamage = this.effect3[level];
            if(target.Team != owner.Team)
            {
                Particle pH; // UNUSED
                float nextBuffVars_AmpValue;
                SpellEffectCreate(out pH, out _, "talon_E_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, targetPos, target, default, default, true, false, false, false, false);
                ApplySilence(attacker, target, silenceDur);
                nextBuffVars_AmpValue = this.effect4[level];
                AddBuff(attacker, target, new Buffs.TalonDamageAmp(nextBuffVars_AmpValue), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                if(target is Champion)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
            }
        }
    }
}