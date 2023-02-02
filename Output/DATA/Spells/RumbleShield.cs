#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RumbleShield : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {50, 80, 110, 140, 170};
        int[] effect1 = {2, 2, 2, 2, 2};
        float[] effect2 = {0.1f, 0.15f, 0.2f, 0.25f, 0.3f};
        int[] effect3 = {20, 20, 20, 20, 20};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float par;
            float nextBuffVars_DamageBlock;
            float baseDamageBlock;
            float abilityPower;
            float bonusHealth;
            float damageBlock;
            float baseHeatCost;
            float nextBuffVars_SpeedBoost;
            par = GetPAR(target, PrimaryAbilityResourceType.Other);
            if(par >= 80)
            {
                AddBuff(attacker, attacker, new Buffs.RumbleOverheat(), 1, 1, 5.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleShield)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.RumbleShield), (ObjAIBase)owner);
            }
            baseDamageBlock = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusHealth = abilityPower * 0.4f;
            damageBlock = baseDamageBlock + bonusHealth;
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.RumbleDangerZone)) > 0)
            {
                damageBlock *= 1.3f;
            }
            nextBuffVars_DamageBlock = damageBlock;
            AddBuff(attacker, target, new Buffs.RumbleShield(nextBuffVars_DamageBlock), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.RumbleHeatDelay(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            nextBuffVars_SpeedBoost = this.effect2[level];
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.RumbleDangerZone)) > 0)
            {
                nextBuffVars_SpeedBoost *= 1.3f;
            }
            baseHeatCost = this.effect3[level];
            AddBuff(attacker, target, new Buffs.RumbleShieldBuff(nextBuffVars_SpeedBoost), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncPAR(owner, baseHeatCost, PrimaryAbilityResourceType.Other);
        }
    }
}
namespace Buffs
{
    public class RumbleShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "robot_root", },
            AutoBuffActivateEffect = new[]{ "rumble_shield_01.troy", },
            BuffName = "RumbleShield",
            BuffTextureName = "Rumble_Scrap Shield.dds",
            OnPreDamagePriority = 3,
        };
        float damageBlock;
        bool willRemove;
        float oldArmorAmount;
        public RumbleShield(float damageBlock = default, bool willRemove = default)
        {
            this.damageBlock = damageBlock;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            PlayAnimation("Spell2", 0, owner, false, false, false);
            IncreaseShield(owner, this.damageBlock, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if(!this.willRemove)
            {
                Particle ar; // UNUSED
                SpellEffectCreate(out ar, out _, "shen_Feint_self_deactivate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
            if(this.damageBlock > 0)
            {
                RemoveShield(owner, this.damageBlock, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.damageBlock;
            if(this.damageBlock >= damageAmount)
            {
                this.damageBlock -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.damageBlock;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                TeamId teamID; // UNUSED
                teamID = GetTeamID(owner);
                damageAmount -= this.damageBlock;
                this.damageBlock = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}