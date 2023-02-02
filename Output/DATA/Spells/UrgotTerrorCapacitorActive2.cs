#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UrgotTerrorCapacitorActive2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 140, 200, 260, 320};
        public override void SelfExecute()
        {
            float shieldAmount;
            float abilityPower;
            float bonusShield;
            float shield;
            float nextBuffVars_Shield;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            shieldAmount = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusShield = abilityPower * 0.8f;
            shield = shieldAmount + bonusShield;
            nextBuffVars_Shield = shield;
            AddBuff(attacker, attacker, new Buffs.UrgotTerrorCapacitorActive2(nextBuffVars_Shield), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class UrgotTerrorCapacitorActive2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UrgotTerrorCapacitor",
            BuffTextureName = "UrgotTerrorCapacitor.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
            SpellToggleSlot = 2,
        };
        float shield;
        Particle particle1;
        float oldArmorAmount;
        int[] effect0 = {20, 25, 30, 35, 40};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public UrgotTerrorCapacitorActive2(float shield = default)
        {
            this.shield = shield;
        }
        public override void OnActivate()
        {
            int level;
            float slowPercent;
            //RequireVar(this.shield);
            SpellEffectCreate(out this.particle1, out _, "UrgotTerrorCapacitor_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            slowPercent = this.effect0[level];
            SetBuffToolTipVar(1, this.shield);
            SetBuffToolTipVar(2, slowPercent);
            IncreaseShield(owner, this.shield, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
            if(this.shield > 0)
            {
                RemoveShield(owner, this.shield, true, true);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(!target.IsDead)
                    {
                        if(hitResult != HitResult.HIT_Miss)
                        {
                            int level;
                            float nextBuffVars_MoveSpeedMod; // UNUSED
                            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            nextBuffVars_MoveSpeedMod = this.effect1[level];
                            AddBuff(attacker, target, new Buffs.UrgotSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.shield;
            if(this.shield >= damageAmount)
            {
                this.shield -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.shield;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.shield;
                this.shield = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
            SetBuffToolTipVar(1, this.shield);
        }
    }
}