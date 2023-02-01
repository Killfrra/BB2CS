#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RemoveScurvy : BBSpellScript
    {
        int[] effect0 = {80, 150, 220, 290, 360};
        public override void SelfExecute()
        {
            float healLevel;
            float abilityPower;
            float healAmount;
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            healLevel = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            abilityPower *= 1;
            healAmount = healLevel + abilityPower;
            IncHealth(owner, healAmount, owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PirateScurvy)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.PirateScurvy), (ObjAIBase)owner);
            }
        }
    }
}