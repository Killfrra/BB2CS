#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniWintersClawDmg : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "NetherBladeArmorPen",
            BuffTextureName = "Voidwalker_NullBlade.dds",
            SpellToggleSlot = 2,
        };
        float baseDamage;
        Particle chargedBladeEffect;
        int[] effect0 = {30, 45, 60, 75, 90};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.baseDamage = this.effect0[level];
            SpellEffectCreate(out this.chargedBladeEffect, out _, "enrage_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false, false, false, false, false);
            OverrideAnimation("Attack1", "Crit", owner);
            OverrideAnimation("Attack2", "Crit", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.chargedBladeEffect);
            ClearOverrideAnimation("Attack1", owner);
            ClearOverrideAnimation("Attack2", owner);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID;
            Particle temp; // UNUSED
            int count;
            if(target is not BaseTurret)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    if(hitResult != HitResult.HIT_Miss)
                    {
                        teamID = GetTeamID(attacker);
                        SpellEffectCreate(out temp, out _, "Leona_ShieldOfDaybreak_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.25f, 0, false, false, attacker);
                        SpellBuffRemoveStacks(owner, owner, nameof(Buffs.SejuaniWintersClawBuff), 1);
                        count = GetBuffCountFromAll(owner, nameof(Buffs.SejuaniWintersClawBuff));
                        if(count <= 0)
                        {
                            SpellBuffRemove(owner, nameof(Buffs.SejuaniWintersClawBuff), (ObjAIBase)owner, 0);
                            SpellBuffRemove(owner, nameof(Buffs.SejuaniWintersClawDmg), (ObjAIBase)owner, 0);
                        }
                    }
                }
            }
        }
    }
}