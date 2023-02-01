#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptKarthus : BBCharScript
    {
        float lastTimeExecuted;
        int[] effect0 = {20, 27, 34, 41, 48};
        public override void OnKill()
        {
            float manaToInc;
            Particle particle; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Defile)) > 0)
            {
            }
            else
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    manaToInc = this.effect0[level];
                    IncPAR(owner, manaToInc, PrimaryAbilityResourceType.MANA);
                    SpellEffectCreate(out particle, out _, "NeutralMonster_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnUpdateActions()
        {
            float _1; // UNITIALIZED
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, true, _1))
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRAZombie)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickReviveAllySelf)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
    }
}