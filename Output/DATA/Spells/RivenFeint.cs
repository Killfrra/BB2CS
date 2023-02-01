#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenFeint : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "exile_E_shield_01.troy", },
            BuffName = "RivenFeint",
            BuffTextureName = "RivenPathoftheExile.dds",
            OnPreDamagePriority = 3,
        };
        float damageBlock;
        float oldArmorAmount;
        public RivenFeint(float damageBlock = default)
        {
            this.damageBlock = damageBlock;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            IncreaseShield(owner, this.damageBlock, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.damageBlock > 0)
            {
                RemoveShield(owner, this.damageBlock, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID;
            Particle ar; // UNUSED
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
                teamID = GetTeamID(owner);
                damageAmount -= this.damageBlock;
                this.damageBlock = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellEffectCreate(out ar, out _, "exile_E_interupt.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenTriCleave)) == 0)
            {
                UnlockAnimation(owner, true);
            }
        }
    }
}
namespace Spells
{
    public class RivenFeint : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        Particle temp_; // UNUSED
        int[] effect0 = {60, 90, 120, 150, 180};
        float[] effect1 = {2.5f, 2.5f, 2.5f, 2.5f, 2.5f};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            float distance; // UNUSED
            Vector3 pos;
            float baseMS;
            float bonusMS;
            float baseDamageBlock;
            float totalAD;
            float baseAD;
            float bonusAD;
            float bonusHealth;
            float damageBlock;
            float nextBuffVars_DamageBlock;
            targetPos = GetCastSpellTargetPos();
            FaceDirection(owner, targetPos);
            distance = DistanceBetweenObjectAndPoint(owner, targetPos);
            pos = GetPointByUnitFacingOffset(owner, 250, 0);
            baseMS = GetFlatMovementSpeedMod(owner);
            bonusMS = baseMS + 650;
            PlayAnimation("Spell3", 0, owner, false, true, false);
            Move(owner, pos, 900 + bonusMS, 0, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.CANCEL_ORDER, 325, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            baseDamageBlock = this.effect0[level];
            totalAD = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            bonusHealth = bonusAD * 1;
            damageBlock = baseDamageBlock + bonusHealth;
            nextBuffVars_DamageBlock = damageBlock;
            AddBuff((ObjAIBase)owner, owner, new Buffs.RivenFeint(nextBuffVars_DamageBlock), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out this.temp_, out _, "exile_E_mis.troy  ", "exile_E_mis.troy  ", TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
    }
}