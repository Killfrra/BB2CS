#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptXerath : BBCharScript
    {
        int[] effect0 = {8, 8, 8, 8, 8};
        float[] effect1 = {0.1f, 0.15f, 0.2f, 0.25f, 0.3f};
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                int slot;
                slot = GetSpellSlot();
                if(slot == 1)
                {
                    float buffDuration;
                    float nextBuffVars_MagicPen;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    buffDuration = this.effect0[level];
                    nextBuffVars_MagicPen = this.effect1[level];
                    AddBuff((ObjAIBase)owner, owner, new Buffs.XerathLocusOfPower(nextBuffVars_MagicPen), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.XerathAscended(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.XerathParticleBuff(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}