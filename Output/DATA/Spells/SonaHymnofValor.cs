#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaHymnofValor : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Region bubbleID; // UNUSED
        public override void SelfExecute()
        {
            float cooldownPerc;
            float currentCD;
            TeamId casterID;
            float availChamps;
            bool result;
            AddBuff((ObjAIBase)owner, owner, new Buffs.SonaHymnofValor(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SonaHymnofValorCheck(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 2;
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            casterID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            availChamps = 0;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                availChamps++;
            }
            if(availChamps == 1)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            if(availChamps >= 2)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 2, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            if(availChamps == 0)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 850, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 2, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.SonaHymnofValorAura(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell1", 1, owner, false, true, true);
        }
    }
}
namespace Buffs
{
    public class SonaHymnofValor : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            BuffName = "",
            BuffTextureName = "Sona_HymnofValorGold.dds",
            PersistsThroughDeath = true,
            SpellFXOverrideSkins = new[]{ "GuqinSona", },
            SpellToggleSlot = 4,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.SonaAriaofPerseverance), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscord), (ObjAIBase)owner, 0);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) == 0)
            {
                OverrideAutoAttack(4, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
    }
}