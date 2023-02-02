#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaAriaofPerseverance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {40, 60, 80, 100, 120};
        int[] effect1 = {8, 11, 14, 17, 20};
        public override void SelfExecute()
        {
            float nextBuffVars_DefenseBonus;
            float cooldownPerc;
            float currentCD;
            TeamId casterID; // UNUSED
            string jumpTarget;
            float jumpTargetHealth_;
            float aPMod;
            Particle self; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.SonaAriaofPerseverance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SonaAriaofPerseveranceCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 2;
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
            casterID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            jumpTarget = NoTargetYet;
            jumpTargetHealth_ = 1;
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 999, default, true))
            {
                float unitHealth_;
                if(jumpTarget == NoValidTarget)
                {
                    jumpTarget = unit;
                }
                unitHealth_ = GetHealthPercent(unit, PrimaryAbilityResourceType.MANA);
                if(unitHealth_ < jumpTargetHealth_)
                {
                    jumpTarget = unit;
                    jumpTargetHealth_ = unitHealth_;
                }
            }
            if(jumpTarget != NoValidTarget)
            {
                string other1;
                other1 = jumpTarget;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    if(unit == other1)
                    {
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            aPMod = GetFlatMagicDamageMod(attacker);
            aPMod *= 0.25f;
            IncHealth(owner, aPMod + this.effect0[level], attacker);
            SpellEffectCreate(out self, out _, "Global_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.SonaAriaofPerseveranceAura(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell2", 1, owner, false, true, true);
            nextBuffVars_DefenseBonus = this.effect1[level];
            AddBuff(attacker, attacker, new Buffs.SonaAriaShield(nextBuffVars_DefenseBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SonaAriaofPerseverance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            BuffName = "",
            BuffTextureName = "Sona_AriaofPerseveranceGold.dds",
            PersistsThroughDeath = true,
            SpellFXOverrideSkins = new[]{ "GuqinSona", },
            SpellToggleSlot = 4,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.SonaHymnofValor), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscord), (ObjAIBase)owner, 0);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) == 0)
            {
                OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
    }
}