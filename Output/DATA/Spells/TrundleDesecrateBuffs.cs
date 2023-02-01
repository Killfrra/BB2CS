#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleDesecrateBuffs : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleDesecrateBuffs",
            BuffTextureName = "Trundle_Contaminate.dds",
        };
        float selfASMod;
        float selfMSMod;
        float cCReduc;
        public TrundleDesecrateBuffs(float selfASMod = default, float selfMSMod = default, float cCReduc = default)
        {
            this.selfASMod = selfASMod;
            this.selfMSMod = selfMSMod;
            this.cCReduc = cCReduc;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            int level; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.SNARE)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.SLOW)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.FEAR)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.CHARM)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.SLEEP)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.STUN)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.TAUNT)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.SILENCE)
                {
                    duration *= this.cCReduc;
                }
                if(type == BuffType.BLIND)
                {
                    duration *= this.cCReduc;
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.selfASMod);
            //RequireVar(this.selfMSMod);
            //RequireVar(this.cCReduc);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.selfMSMod);
            IncPercentAttackSpeedMod(owner, this.selfASMod);
        }
    }
}