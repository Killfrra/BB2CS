#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IreliaHitenStyleCharged : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "BUFFBONE_GLB_WEAPON_1", "BUFFBONE_GLB_WEAPON_1", "", "", },
            AutoBuffActivateEffect = new[]{ "irelia_hitenStyle_activate.troy", "irelia_hitenStlye_active_glow.troy", "", "", },
            BuffName = "IreliaHitenStyleCharged",
            BuffTextureName = "Irelia_HitenStyle.dds",
        };
        int[] effect0 = {15, 30, 45, 60, 75};
        int[] effect1 = {10, 14, 18, 22, 26};
        public override void OnActivate()
        {
            OverrideAnimation("Attack1", "Attack1c", owner);
            OverrideAnimation("Attack2", "Attack2c", owner);
            OverrideAnimation("Crit", "Critc", owner);
            OverrideAnimation("Idle1", "Idle1c", owner);
            OverrideAnimation("Run", "Runc", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaHitenStyle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float trueDamage;
            float healthRestoration;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            trueDamage = this.effect0[level];
            healthRestoration = this.effect1[level];
            IncHealth(owner, healthRestoration, owner);
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    ApplyDamage(attacker, target, trueDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}