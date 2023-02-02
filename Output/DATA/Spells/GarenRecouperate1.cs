#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenRecouperate1 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "GarenRecouperate1",
            BuffTextureName = "Garen_Perseverance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle part;
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.part, out _, "garen_heal.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            SpellBuffRemove(owner, nameof(Buffs.GarenRecouperateOn), (ObjAIBase)owner, 0);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.part);
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecouperateOn(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                float healthPercent;
                healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if(healthPercent < 1)
                {
                    float maxHealth;
                    float healthToInc;
                    maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                    healthToInc = maxHealth * 0.005f;
                    IncHealth(owner, healthToInc, owner);
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}