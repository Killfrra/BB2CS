#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UdyrTurtleStance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 100, 140, 180, 220};
        public override void SelfExecute()
        {
            float cooldownPerc;
            float currentCD;
            float nextBuffVars_ShieldAmount;
            float aPAmount;
            float shieldAmount;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrBearStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrBearStance), (ObjAIBase)owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerStance), (ObjAIBase)owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrPhoenixStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrPhoenixStance), (ObjAIBase)owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTurtleActivation)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTurtleActivation), (ObjAIBase)owner);
            }
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 1.5f;
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrTurtleStance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            aPAmount = GetFlatMagicDamageMod(owner);
            aPAmount *= 0.5f;
            shieldAmount = this.effect0[level];
            shieldAmount += aPAmount;
            nextBuffVars_ShieldAmount = shieldAmount;
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrTurtleActivation(nextBuffVars_ShieldAmount), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class UdyrTurtleStance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTurtleStance",
            BuffTextureName = "Udyr_TurtleStance.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
        int casterID; // UNUSED
        Particle turtle;
        Particle turtleparticle;
        public override void OnActivate()
        {
            this.casterID = PushCharacterData("UdyrTurtle", owner, false);
            SpellEffectCreate(out this.turtle, out _, "turtlepelt.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            SpellEffectCreate(out this.turtleparticle, out _, "TurtleStance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.turtle);
            SpellEffectRemove(this.turtleparticle);
            RemoveOverrideAutoAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            float critMod;
            critMod = GetFlatCritChanceMod(owner);
            critMod *= -1;
            critMod += charVars.BaseCritChance;
            IncFlatCritChanceMod(owner, critMod);
        }
    }
}