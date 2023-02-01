#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinDebacleCloak : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "bansheesveil_buf.troy", },
            BuffName = "BansheesVeil",
            BuffTextureName = "066_Sindoran_Shielding_Amulet.dds",
        };
        bool willRemove;
        public OdinDebacleCloak(bool willRemove = default)
        {
            this.willRemove = willRemove;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            float durationMod;
            Particle ar; // UNUSED
            if(this.willRemove)
            {
                durationMod = 0.5f;
                if(owner.Team != attacker.Team)
                {
                    if(type == BuffType.SNARE)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.SLOW)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.FEAR)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.CHARM)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.SLEEP)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.STUN)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.TAUNT)
                    {
                        duration *= durationMod;
                    }
                }
                else
                {
                    returnValue = true;
                }
            }
            else if(duration == 37037)
            {
                SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
                this.willRemove = true;
                if(owner.Team != attacker.Team)
                {
                    if(type == BuffType.SNARE)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.SLOW)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.FEAR)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.CHARM)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.SLEEP)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.STUN)
                    {
                        duration *= durationMod;
                    }
                    if(type == BuffType.TAUNT)
                    {
                        duration *= durationMod;
                    }
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
        }
        public override void OnUpdateStats()
        {
            if(this.willRemove)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinDebacleTimer(), 1, 1, 45, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.OdinDebacleCloak), (ObjAIBase)owner, 0);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.willRemove)
            {
                damageAmount *= 0.5f;
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
                        SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
                    }
                    else if(spellVars.DoesntBreakShields)
                    {
                    }
                    else if(!spellVars.DoesntTriggerSpellCasts)
                    {
                        this.willRemove = true;
                        SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
                    }
                }
            }
        }
    }
}