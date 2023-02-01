#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathLocusOfPower : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XerathLocusOfPower",
            BuffTextureName = "Xerath_LocusOfPower.dds",
            SpellToggleSlot = 2,
        };
        float magicPen;
        Particle particle;
        Particle particlea;
        Particle particleb;
        Particle particlec;
        int[] effect0 = {20, 16, 12, 8, 4};
        public XerathLocusOfPower(float magicPen = default)
        {
            this.magicPen = magicPen;
        }
        public override void OnActivate()
        {
            float cooldown;
            float cooldown2;
            float cooldown3;
            TeamId teamOfOwner; // UNITIALIZED
            string flashCheck;
            //RequireVar(this.magicPen);
            IncPercentMagicPenetrationMod(owner, this.magicPen);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathLocusOfPowerToggle));
            SetSlotSpellCooldownTimeVer2(0.5f, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcanopulseExtended));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathMageChainsExtended));
            SetSlotSpellCooldownTimeVer2(cooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcaneBarrageWrapperExt));
            SetSlotSpellCooldownTimeVer2(cooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectCreate(out this.particle, out _, "Xerath_LocusOfPower_buf.troy", default, teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particlea, out _, "Xerath_LocusOfPower_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out this.particleb, out _, "Xerath_LocusOfPower_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_CHANNEL_2", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out this.particlec, out _, "Xerath_LocusOfPower_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_CHANNEL_3", default, owner, "spine", default, false, false, false, false, false);
            SetCanMove(owner, false);
            OverrideAnimation("Idle1", "Spell2_chan", owner);
            OverrideAnimation("Idle2", "Spell2_chan", owner);
            OverrideAnimation("Idle3", "Spell2_chan", owner);
            OverrideAnimation("Idle4", "Spell2_chan", owner);
            flashCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float cooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            float cooldown2;
            string flashCheck;
            SetCanMove(owner, true);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathLocusOfPower));
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldown = this.effect0[level];
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = cooldown * multiplier;
            SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particlea);
            SpellEffectRemove(this.particleb);
            SpellEffectRemove(this.particlec);
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcanopulse));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathMageChains));
            SetSlotSpellCooldownTimeVer2(cooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.XerathArcaneBarrageWrapper));
            SetSlotSpellCooldownTimeVer2(cooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            ClearOverrideAnimation("Idle1", owner);
            ClearOverrideAnimation("Idle2", owner);
            ClearOverrideAnimation("Idle3", owner);
            ClearOverrideAnimation("Idle4", owner);
            flashCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.XerathEnergize(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMagicPenetrationMod(owner, this.magicPen);
        }
        public override void OnUpdateActions()
        {
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class XerathLocusOfPower : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}