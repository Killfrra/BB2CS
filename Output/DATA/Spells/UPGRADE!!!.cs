#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UPGRADE___ : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, nameof(Buffs.H28GEvolutionTurret), true))
            {
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.H28GEvolutionTurret)) > 0)
                {
                    float maxHP;
                    AddBuff(attacker, unit, new Buffs.UpgradeSlow(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    maxHP = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    IncHealth(unit, maxHP, attacker);
                }
            }
            AddBuff(attacker, attacker, new Buffs.UpgradeBuff(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class UPGRADE___ : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UPGRADE!!!",
            BuffTextureName = "Heimerdinger_UPGRADE.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float cooldownBonus;
        float[] effect0 = {-0.1f, -0.15f, -0.2f};
        float[] effect1 = {-0.1f, -0.15f, -0.2f};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.cooldownBonus = this.effect0[level];
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, this.cooldownBonus);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 3)
            {
                int level;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.cooldownBonus = this.effect1[level];
            }
        }
    }
}