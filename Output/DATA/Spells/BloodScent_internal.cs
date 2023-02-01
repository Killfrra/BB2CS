#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BloodScent_internal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Blood Awareness",
            BuffTextureName = "20.dds",
            PersistsThroughDeath = true,
            SpellVOOverrideSkins = new[]{ "HyenaWarwick", },
            SpellToggleSlot = 3,
        };
        float lastTimeExecuted;
        float[] effect0 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void OnDeactivate(bool expired)
        {
            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 4);
        }
        public override void OnUpdateActions()
        {
            int level;
            float baseRange;
            float range;
            float maxHealth;
            float health;
            float healthPercent;
            float nextBuffVars_MoveSpeedBuff;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                if(owner.IsDead)
                {
                }
                else
                {
                    if(ExecutePeriodically(2, ref this.lastTimeExecuted, false))
                    {
                        baseRange = level * 800;
                        range = baseRange + 700;
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, range, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                        {
                            maxHealth = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                            health = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                            healthPercent = health / maxHealth;
                            if(healthPercent <= 0.5f)
                            {
                                AddBuff(attacker, unit, new Buffs.BloodScent_target(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                                nextBuffVars_MoveSpeedBuff = this.effect0[level];
                                AddBuff(attacker, attacker, new Buffs.BloodScent(nextBuffVars_MoveSpeedBuff), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}