#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WillRevive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "spine", },
            AutoBuffActivateEffect = new[]{ "rebirthready.troy", },
            BuffName = "Guardian Angel",
            BuffTextureName = "3026_Guardian_Angel.dds",
            NonDispellable = true,
            OnPreDamagePriority = 6,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            float guardianAngelCount;
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            guardianAngelCount = 0;
            if(name == nameof(Spells.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if(name1 == nameof(Spells.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if(name2 == nameof(Spells.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if(name3 == nameof(Spells.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if(name4 == nameof(Spells.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if(name5 == nameof(Spells.GuardianAngel))
            {
                guardianAngelCount++;
            }
            if(guardianAngelCount == 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.WillRevive), (ObjAIBase)owner, 0);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HasBeenRevived)) == 0)
            {
                float curHealth;
                curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if(curHealth <= damageAmount)
                {
                    if(damageSource != default)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.GuardianAngel)) == 0)
                        {
                            if(owner is Champion)
                            {
                                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombie)) == 0)
                                {
                                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieLich)) == 0)
                                    {
                                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieKogMaw)) == 0)
                                        {
                                            damageAmount = curHealth - 1;
                                            AddBuff((ObjAIBase)owner, owner, new Buffs.GuardianAngel(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}