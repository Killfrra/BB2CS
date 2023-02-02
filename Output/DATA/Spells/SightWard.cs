#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SightWard : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Minion other3;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            other3 = SpawnMinion("SightWard", "SightWard", "idle.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, true, true, false, false, false, false, 0, true, false, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.SharedWardBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.SightWard(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ItemPlacementMissile(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(avatarVars.Scout)
            {
                AddBuff(attacker, other3, new Buffs.MasteryScoutBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpell((ObjAIBase)owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ItemPlacementMissile));
            FaceDirection(owner, targetPos);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class SightWard : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ICU.troy", },
            BuffName = "SightWard",
            BuffTextureName = "1020_Glowing_Orb.dds",
        };
        Region bubbleID;
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if(type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if(type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if(type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if(type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if(type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if(type == BuffType.COMBAT_DEHANCER)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId casterID;
            casterID = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(casterID, 1100, owner, 180, default, default, false);
            SetForceRenderParticles(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            ApplyDamage((ObjAIBase)owner, owner, 600, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if(lifeTime >= 2)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Stealth(), 1, 1, 600, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0, true, false, true);
            }
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                IncPAR(owner, -1, PrimaryAbilityResourceType.Shield);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageAmount >= 1)
            {
                if(attacker is not BaseTurret)
                {
                    if(damageSource != default)
                    {
                        damageAmount = 0;
                    }
                    else
                    {
                        damageAmount = 1;
                    }
                }
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            returnValue = 0;
            return returnValue;
        }
    }
}