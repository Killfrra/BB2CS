#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ManiacalCloak : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Maniacal Cloak",
            BuffTextureName = "Jester_ManiacalCloak2.dds",
            SpellToggleSlot = 4,
        };
        bool buffAdded;
        bool willFade;
        float timeLastHit;
        float stealthDelay;
        float totalCostPerTick;
        Fade iD; // UNUSED
        Particle particle; // UNUSED
        float lastTimeExecuted;
        public ManiacalCloak(bool buffAdded = default, bool willFade = default, float timeLastHit = default, float stealthDelay = default, float totalCostPerTick = default)
        {
            this.buffAdded = buffAdded;
            this.willFade = willFade;
            this.timeLastHit = timeLastHit;
            this.stealthDelay = stealthDelay;
            this.totalCostPerTick = totalCostPerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.buffAdded);
            //RequireVar(this.willFade);
            //RequireVar(this.timeLastHit);
            //RequireVar(this.stealthDelay);
            //RequireVar(this.totalCostPerTick);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
            this.iD = PushCharacterFade(owner, 0.2f, this.stealthDelay);
            SpellEffectCreate(out this.particle, out _, "ShadowWalk_buf.troy", default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target);
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            baseCooldown = 5;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            this.iD = PushCharacterFade(owner, 1, 1);
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            if(this.buffAdded)
            {
                SetStealthed(owner, true);
            }
        }
        public override void OnUpdateActions()
        {
            float curTime;
            float timeSinceLastHit;
            float curMana;
            bool tempStealthed;
            float manaCost;
            if(!this.buffAdded)
            {
                curTime = GetTime();
                timeSinceLastHit = curTime - this.timeLastHit;
                if(timeSinceLastHit > this.stealthDelay)
                {
                    curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                    if(curMana >= this.totalCostPerTick)
                    {
                        SetStealthed(owner, true);
                        this.buffAdded = true;
                    }
                    else
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
                if(this.willFade)
                {
                    this.iD = PushCharacterFade(owner, 0.2f, this.stealthDelay);
                    this.willFade = false;
                    SpellEffectCreate(out this.particle, out _, "ShadowWalk_buf.troy", default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target);
                }
            }
            else
            {
                tempStealthed = GetStealthed(owner);
                if(!tempStealthed)
                {
                    this.buffAdded = false;
                    this.timeLastHit = GetTime();
                }
            }
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(this.buffAdded)
                {
                    curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                    if(curMana >= this.totalCostPerTick)
                    {
                        manaCost = this.totalCostPerTick * -1;
                        IncPAR(owner, manaCost);
                    }
                    else
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.CastingBreaksStealth)
            {
                this.timeLastHit = GetTime();
                this.iD = PushCharacterFade(owner, 1, 0);
                this.willFade = true;
                this.buffAdded = false;
                SetStealthed(owner, false);
            }
            else if(!spellVars.DoesntTriggerSpellCasts)
            {
                this.timeLastHit = GetTime();
                this.iD = PushCharacterFade(owner, 1, 0);
                this.willFade = true;
                this.buffAdded = false;
                SetStealthed(owner, false);
            }
        }
        public override void OnPreAttack()
        {
            this.timeLastHit = GetTime();
            this.iD = PushCharacterFade(owner, 1, 0);
            this.willFade = true;
            this.buffAdded = false;
        }
    }
}
namespace Spells
{
    public class ManiacalCloak : BBSpellScript
    {
        int[] effect0 = {10, 10, 10};
        float[] effect1 = {2.25f, 1.75f, 1.25f};
        public override void SelfExecute()
        {
            float nextBuffVars_TimeLastHit;
            bool nextBuffVars_BuffAdded;
            bool nextBuffVars_WillFade;
            float nextBuffVars_TotalCostPerTick;
            float nextBuffVars_StealthDelay;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ManiacalCloak)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.ManiacalCloak), (ObjAIBase)owner);
            }
            else
            {
                nextBuffVars_TimeLastHit = GetTime();
                nextBuffVars_BuffAdded = false;
                nextBuffVars_WillFade = false;
                nextBuffVars_TotalCostPerTick = this.effect0[level];
                nextBuffVars_StealthDelay = this.effect1[level];
                AddBuff(attacker, owner, new Buffs.ManiacalCloak(nextBuffVars_BuffAdded, nextBuffVars_WillFade, nextBuffVars_TimeLastHit, nextBuffVars_StealthDelay, nextBuffVars_TotalCostPerTick), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0);
            }
        }
    }
}