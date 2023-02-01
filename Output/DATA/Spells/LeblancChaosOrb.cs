#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancChaosOrb : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancMarkOfSilence",
            BuffTextureName = "LeblancMarkOfSilence.dds",
            NonDispellable = true,
        };
        Particle b;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.b, out _, "leBlanc_displace_AOE_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.b);
        }
    }
}
namespace Spells
{
    public class LeblancChaosOrb : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 16f, 14f, 12f, 10f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {20, 40, 60, 80, 100};
        int[] effect1 = {22, 44, 66, 88, 110};
        int[] effect2 = {25, 50, 75, 100, 125};
        int[] effect3 = {28, 56, 84, 112, 140};
        int[] effect4 = {70, 110, 150, 190, 230};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrb)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrb), (ObjAIBase)owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
            }
            AddBuff(attacker, target, new Buffs.LeblancChaosOrb(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrbM)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrbM), (ObjAIBase)owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                }
                else if(level == 2)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                }
                else
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, this.effect3[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                }
            }
            ApplyDamage(attacker, target, this.effect4[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
        }
    }
}