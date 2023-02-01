#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FeastMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Particle particle;
        float lastTimeExecuted;
        int[] effect0 = {0, 0, 0};
        int[] effect1 = {300, 475, 650};
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "feast_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            int count;
            int level;
            float healthPerStack;
            float feastBase;
            float bonusFeastHealth;
            float feastHealth;
            float targetHealth;
            float time;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                count = GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.Feast));
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                healthPerStack = this.effect0[level];
                feastBase = this.effect1[level];
                bonusFeastHealth = healthPerStack * count;
                feastHealth = bonusFeastHealth + feastBase;
                targetHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if(feastHealth < targetHealth)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    time = GetSlotSpellCooldownTime(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(time > 0)
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
    }
}