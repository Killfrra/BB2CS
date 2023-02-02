#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShadowStep : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240};
        int[] effect1 = {8, 12, 16, 20, 24};
        float[] effect2 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        public override void SelfExecute()
        {
            Particle smokeBomb; // UNUSED
            Vector3 castPos;
            int ownerskinid;
            Particle p3; // UNUSED
            SpellEffectCreate(out smokeBomb, out _, "katarina_shadowStep_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            castPos = GetUnitPosition(owner);
            ownerskinid = GetSkinID(owner);
            if(ownerskinid == 6)
            {
                SpellEffectCreate(out p3, out _, "katarina_shadowStep_Sand_return.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out p3, out _, "katarina_shadowStep_return.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float distance;
            float finalDistance;
            Vector3 targetPos;
            bool temp;
            float damageVar;
            float kIDamage;
            float nextBuffVars_DamageReduction;
            FaceDirection(owner, target.Position);
            distance = DistanceBetweenObjects("Owner", "Target");
            finalDistance = distance + 0;
            finalDistance += 250;
            targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
            temp = IsPathable(targetPos);
            if(!temp)
            {
                finalDistance -= 200;
            }
            targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
            TeleportToPosition(owner, targetPos);
            damageVar = this.effect0[level];
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            kIDamage = this.effect1[level];
            damageVar += kIDamage;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinct)) > 0)
            {
                nextBuffVars_DamageReduction = this.effect2[level];
                AddBuff(attacker, owner, new Buffs.ShadowStepDodge(nextBuffVars_DamageReduction), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.KillerInstinct), (ObjAIBase)owner, 0);
            }
            if(target.Team != owner.Team)
            {
                Particle pH; // UNUSED
                SpellEffectCreate(out pH, out _, "katarina_shadowStep_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, targetPos, target, default, default, true, false, false, false, false);
                ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                if(target is Champion)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
            }
            else
            {
                if(GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.Destealth(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}