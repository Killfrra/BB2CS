#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptBlitzcrank : BBCharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            float cooldown;
            float blitzAP;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(!owner.IsDead)
            {
                if(level > 0)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown <= 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.StaticField)) > 0)
                        {
                        }
                        else
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.StaticField(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                blitzAP = GetFlatMagicDamageMod(owner);
                SetSpellToolTipVar(blitzAP, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.RocketGrab))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Root(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, target, new Buffs.ManaBarrierIcon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}