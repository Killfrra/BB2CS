#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HeimerdingerTurretCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            int level;
            int count;
            float baseCooldown;
            float cooldownMod;
            float newCooldown;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HeimerdingerTurretTimer)) == 0)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(level >= 3)
                        {
                            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.HeimerdingerTurretReady));
                            if(count < 2)
                            {
                                baseCooldown = 25;
                                cooldownMod = GetPercentCooldownMod(owner);
                                cooldownMod++;
                                newCooldown = baseCooldown * cooldownMod;
                                AddBuff((ObjAIBase)owner, owner, new Buffs.HeimerdingerTurretTimer(), 1, 1, newCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, false, false, false);
                            }
                        }
                        else if(level >= 1)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HeimerdingerTurretReady)) == 0)
                            {
                                baseCooldown = 25;
                                cooldownMod = GetPercentCooldownMod(owner);
                                cooldownMod++;
                                newCooldown = baseCooldown * cooldownMod;
                                AddBuff((ObjAIBase)owner, owner, new Buffs.HeimerdingerTurretTimer(), 1, 1, newCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, false, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}