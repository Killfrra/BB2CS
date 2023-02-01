#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliSmokeBomb : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle particle2;
        Particle particle;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId casterID;
            casterID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle2, out this.particle, "akali_smoke_bomb_tar_team_green.troy", "akali_smoke_bomb_tar_team_red.troy", casterID, 250, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_InitialTime;
            float nextBuffVars_TimeLastHit;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(unit == attacker)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) == 0)
                        {
                            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AkaliHoldStealth)) == 0)
                            {
                                if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AkaliSBStealth)) == 0)
                                {
                                    nextBuffVars_InitialTime = GetTime();
                                    nextBuffVars_TimeLastHit = GetTime();
                                    AddBuff(attacker, attacker, new Buffs.AkaliSmokeBombInternal(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                }
                                else
                                {
                                    AddBuff(attacker, attacker, new Buffs.AkaliSBStealth(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
                                }
                            }
                        }
                        AddBuff(attacker, attacker, new Buffs.AkaliSBBuff(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                    if(unit.Team != attacker.Team)
                    {
                        AddBuff(attacker, unit, new Buffs.AkaliSBDebuff(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class AkaliSmokeBomb : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {8, 8, 8, 8, 8};
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Minion other3;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.AkaliSmokeBomb(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}