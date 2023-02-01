#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BansheesVeil : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "BansheesVeil",
            BuffTextureName = "066_Sindoran_Shielding_Amulet.dds",
        };
        bool willRemove;
        Particle a;
        public BansheesVeil(bool willRemove = default)
        {
            this.willRemove = willRemove;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            Particle ar; // UNUSED
            if(this.willRemove)
            {
                if(owner.Team != attacker.Team)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else if(duration == 37037)
            {
                SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                this.willRemove = true;
                returnValue = false;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            //RequireVar(this.willRemove);
            teamID = GetTeamID(owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.XerathAscended)) > 0)
            {
                SpellEffectCreate(out this.a, out _, "bansheesveil_buf_tempXerath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, false, true, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.a, out _, "bansheesveil_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, true, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
        }
        public override void OnUpdateStats()
        {
            if(this.willRemove)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BansheesVeilTimer(), 1, 1, 45, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.BansheesVeil), (ObjAIBase)owner, 0);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.willRemove)
            {
                damageAmount = 0;
            }
        }
        public override void OnBeingSpellHit(SpellScriptMetaData spellVars)
        {
            bool isAttack;
            Particle ar; // UNUSED
            SetTriggerUnit(attacker);
            owner = SetBuffCasterUnit();
            if(owner.Team != attacker.Team)
            {
                isAttack = GetIsAttackOverride();
                if(!isAttack)
                {
                    if(!spellVars.DoesntBreakShields)
                    {
                        this.willRemove = true;
                        SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    }
                    else if(spellVars.DoesntBreakShields)
                    {
                    }
                    else if(!spellVars.DoesntTriggerSpellCasts)
                    {
                        this.willRemove = true;
                        SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    }
                }
            }
        }
    }
}