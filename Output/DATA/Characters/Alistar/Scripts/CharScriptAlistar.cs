#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptAlistar : BBCharScript
    {
        float[] effect0 = {0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f};
        public override void SetVarsByLevel()
        {
            charVars.BaseBlockAmount = this.effect0[level];
        }
        public override void OnNearbyDeath()
        {
            float cooldown;
            bool noRender;
            float newCooldown;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(cooldown > 0)
                {
                    noRender = GetNoRender(target);
                    if(!noRender)
                    {
                        newCooldown = cooldown - 2;
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.ColossalStrength(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
        }
    }
}