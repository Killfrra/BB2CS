#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CardmasterStack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "CardMasterStack",
            BuffTextureName = "Cardmaster_RapidToss.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusDamage;
        float cooldownBonus;
        float attackSpeedBonus;
        public CardmasterStack(float bonusDamage = default, float cooldownBonus = default, float attackSpeedBonus = default)
        {
            this.bonusDamage = bonusDamage;
            this.cooldownBonus = cooldownBonus;
            this.attackSpeedBonus = attackSpeedBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.cooldownBonus);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.attackSpeedBonus);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedBonus);
            IncPercentCooldownMod(owner, this.cooldownBonus);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CardmasterStackParticle)) > 0)
                    {
                        TeamId teamID;
                        Particle c; // UNUSED
                        SpellBuffRemove(owner, nameof(Buffs.CardmasterStackParticle), (ObjAIBase)owner);
                        teamID = GetTeamID(owner);
                        BreakSpellShields(target);
                        ApplyDamage(attacker, target, this.bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                        SpellEffectCreate(out c, out _, "CardmasterStackAttack_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.CardMasterStackHolder(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}