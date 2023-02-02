#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenRecouperate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "GarenRecouperate",
            BuffTextureName = "Garen_Perseverance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle part;
        float lastTimeExecuted;
        int[] effect0 = {8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25};
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if(type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out this.part, out _, "garen_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.part);
        }
        public override void OnUpdateStats()
        {
            int level;
            int hPRegen; // UNUSED
            level = GetLevel(owner);
            hPRegen = this.effect0[level];
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
                if(healthPercent >= 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}