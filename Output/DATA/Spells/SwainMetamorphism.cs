#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SwainMetamorphism : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {5, 7, 9};
        int[] effect1 = {30, 32, 34};
        int[] effect2 = {0, 0, 0};
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainMetamorphism)) > 0)
            {
            }
            else
            {
                returnValue = 0.5f;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainMetamorphism)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.SwainMetamorphism), (ObjAIBase)owner, 0);
            }
            else
            {
                float nextBuffVars_ManaCostInc;
                float nextBuffVars_ManaCost;
                nextBuffVars_ManaCostInc = this.effect0[level];
                nextBuffVars_ManaCost = this.effect1[level];
                AddBuff(attacker, attacker, new Buffs.SwainMetamorphism(nextBuffVars_ManaCost, nextBuffVars_ManaCostInc), 1, 1, 25000 + this.effect2[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class SwainMetamorphism : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainMetamorphism",
            BuffTextureName = "SwainRavenousFlock.dds",
            NonDispellable = true,
            SpellToggleSlot = 4,
        };
        float manaCost;
        float manaCostInc;
        int ravenID; // UNUSED
        Particle particle2;
        Particle particle3;
        float lastTimeExecuted;
        public SwainMetamorphism(float manaCost = default, float manaCostInc = default)
        {
            this.manaCost = manaCost;
            this.manaCostInc = manaCostInc;
        }
        public override void OnActivate()
        {
            Particle particle; // UNUSED
            int level;
            float count;
            float maxMissiles;
            bool result;
            //RequireVar(this.manaCost);
            //RequireVar(this.manaCostInc);
            this.ravenID = PushCharacterData("SwainRaven", owner, false);
            SpellEffectCreate(out particle, out _, "swain_metamorph.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.particle2, out _, "swain_metamorph_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.particle3, out _, "swain_demonForm_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            count = 0;
            maxMissiles = 3;
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                result = CanSeeTarget(owner, unit);
                if(result)
                {
                    if(count < maxMissiles)
                    {
                        count++;
                        SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                    }
                }
            }
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 3, default, true))
            {
                result = CanSeeTarget(owner, unit);
                if(result)
                {
                    if(count < maxMissiles)
                    {
                        count++;
                        SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            Particle particle; // UNUSED
            float buffCheck;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
            SpellEffectCreate(out particle, out _, "swain_metamorph.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            buffCheck = 0;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SwainBeamSelf)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SwainBeamTransition(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                buffCheck++;
            }
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = 10 * multiplier;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            if(buffCheck == 0)
            {
                PopAllCharacterData(owner);
            }
        }
        public override void OnUpdateActions()
        {
            float count;
            float maxMissiles;
            count = 0;
            maxMissiles = 3;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                float curMana;
                int level;
                bool result;
                curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if(this.manaCost > curMana)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    float negMana;
                    negMana = this.manaCost * -1;
                    IncPAR(owner, negMana, PrimaryAbilityResourceType.MANA);
                }
                this.manaCost += this.manaCostInc;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        if(count < maxMissiles)
                        {
                            count++;
                            SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                        }
                    }
                }
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 625, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 3, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        if(count < maxMissiles)
                        {
                            count++;
                            SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
                        }
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string spellCastName;
            spellCastName = GetSpellName();
            if(spellCastName == nameof(Spells.SwainBeam))
            {
                PlayAnimation("Spell1", 0, owner, false, true, false);
            }
        }
    }
}