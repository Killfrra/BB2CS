#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NetherBladeBuff : BBBuffScript
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
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.baseDamage = this.effect0[level];
            SpellEffectCreate(out this.chargedBladeEffect, out _, "Kassadin_Netherblade.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.chargedBladeEffect);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is not BaseTurret)
            {
                ApplyDamage(attacker, target, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.15f, 0, false, false, attacker);
            }
        }
    }
}