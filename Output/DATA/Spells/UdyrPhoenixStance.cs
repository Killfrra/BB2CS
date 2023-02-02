#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UdyrPhoenixStance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            float cooldownPerc;
            float currentCD;
            Particle phoenixparticle; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrBearStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrBearStance), (ObjAIBase)owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerStance), (ObjAIBase)owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTurtleStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTurtleStance), (ObjAIBase)owner);
            }
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 1.5f;
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
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
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrPhoenixStance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            SpellEffectCreate(out phoenixparticle, out _, "PhoenixStance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrPhoenixActivation(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class UdyrPhoenixStance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrPhoenixStance",
            BuffTextureName = "Udyr_PhoenixStance.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        int casterID; // UNUSED
        Particle phoenix;
        public override void OnActivate()
        {
            this.casterID = PushCharacterData("UdyrPhoenix", owner, false);
            SpellEffectCreate(out this.phoenix, out _, "phoenixpelt.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            charVars.Count = 0;
            OverrideAutoAttack(4, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.phoenix);
            RemoveOverrideAutoAttack(owner, true);
        }
    }
}