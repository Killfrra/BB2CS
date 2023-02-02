#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaGhostSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "OrianaGhostSelf",
            BuffTextureName = "OriannaPassive.dds",
        };
        object selfParticle; // UNUSED
        int[] effect0 = {10, 15, 20, 25, 30};
        public override void OnActivate()
        {
            object selfParticle; // UNITIALIZED
            string myName;
            this.selfParticle = selfParticle;
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            myName = GetUnitSkinName(owner);
            if(myName == "Orianna")
            {
                if(!owner.IsDead)
                {
                    if(charVars.GhostInitialized)
                    {
                        PopCharacterData(owner, charVars.TempSkin);
                    }
                }
            }
            SpellBuffClear(owner, nameof(Buffs.OriannaBallTracker));
        }
        public override void OnDeactivate(bool expired)
        {
            string myName;
            myName = GetUnitSkinName(owner);
            charVars.GhostInitialized = true;
            if(!owner.IsDead)
            {
                if(myName == "Orianna")
                {
                    charVars.TempSkin = PushCharacterData("OriannaNoBall", owner, false);
                }
                if(myName == "orianna")
                {
                    charVars.TempSkin = PushCharacterData("OriannaNoBall", owner, false);
                }
            }
        }
        public override void OnUpdateStats()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                float defenseBonus;
                defenseBonus = this.effect0[level];
                IncFlatArmorMod(owner, defenseBonus);
                IncFlatSpellBlockMod(owner, defenseBonus);
            }
        }
    }
}