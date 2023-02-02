#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTrundle : BBCharScript
    {
        float[] effect0 = {0.8f, 0.9f, 1, 1.1f, 1.2f};
        float[] effect1 = {0.02f, 0.02f, 0.02f, 0.02f, 0.03f, 0.03f, 0.03f, 0.03f, 0.04f, 0.04f, 0.04f, 0.05f, 0.05f, 0.05f, 0.06f, 0.06f, 0.06f, 0.06f};
        float[] effect2 = {0.02f, 0.02f, 0.02f, 0.02f, 0.03f, 0.03f, 0.03f, 0.03f, 0.04f, 0.04f, 0.04f, 0.05f, 0.05f, 0.05f, 0.06f, 0.06f, 0.06f, 0.06f};
        public override void OnUpdateActions()
        {
            float scaling;
            float damagess;
            float tTVar;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            scaling = this.effect0[level];
            damagess = GetTotalAttackDamage(owner);
            tTVar = damagess * scaling;
            SetSpellToolTipVar(tTVar, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
        }
        public override void OnNearbyDeath()
        {
            if(owner.Team != target.Team)
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        bool noRender;
                        noRender = GetNoRender(target);
                        if(!noRender)
                        {
                            float hPPre;
                            float healVar;
                            hPPre = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                            healVar = hPPre * charVars.RegenValue;
                            IncHealth(owner, healVar, owner);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            level = GetLevel(owner);
            charVars.RegenValue = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.TrundleDiseaseOverseer(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            charVars.DiseaseCounter = 0;
        }
        public override void OnLevelUp()
        {
            level = GetLevel(owner);
            charVars.RegenValue = this.effect2[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.TrundleDiseaseOverseer(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}