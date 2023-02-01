#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HunterSpeed : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "HunterSpeedBuff",
            BuffTextureName = "Sivir_Sprint.dds",
        };
        float movementSpeedMod;
        float lastTimeExecuted;
        int[] effect0 = {30, 40, 50, 60, 70};
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.COMBAT_DEHANCER)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.DAMAGE)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.FEAR)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.CHARM)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.POLYMORPH)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.SILENCE)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.SLEEP)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.SNARE)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.STUN)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if(type == BuffType.SLOW)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
        }
        public override void OnUpdateStats()
        {
            int level;
            if(ExecutePeriodically(8, ref this.lastTimeExecuted, false))
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.movementSpeedMod = this.effect0[level];
            }
            IncFlatMovementSpeedMod(owner, this.movementSpeedMod);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}