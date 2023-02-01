#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortuneStrut : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissFortuneStrut",
            BuffTextureName = "MissFortune_Strut.dds",
            PersistsThroughDeath = true,
        };
        float moveSpeedMod;
        bool willRemove;
        Particle running;
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            this.moveSpeedMod = 0;
            this.willRemove = false;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MissFortuneWaves)) == 0)
            {
                this.moveSpeedMod = 25;
                OverrideAnimation("Run", "Run2", owner);
                this.willRemove = true;
                SpellEffectCreate(out this.running, out _, "missFortune_passive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "root", default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
            if(this.willRemove)
            {
                SpellEffectRemove(this.running);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, this.moveSpeedMod);
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MissFortuneWaves)) == 0)
                {
                    this.moveSpeedMod += 3.93f;
                    this.moveSpeedMod = Math.Min(this.moveSpeedMod, 70);
                    if(!this.willRemove)
                    {
                        OverrideAnimation("Run", "Run2", owner);
                        this.willRemove = true;
                        SpellEffectCreate(out this.running, out _, "missFortune_passive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "root", default, owner, default, default, false);
                    }
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
        }
    }
}