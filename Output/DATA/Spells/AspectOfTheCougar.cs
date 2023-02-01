#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AspectOfTheCougar : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AspectOfTheCougar",
            BuffTextureName = "Nidalee_AspectOfTheCougar.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        float armorMod;
        float cD0;
        float cD1;
        float cD2;
        int cougarID;
        int[] effect0 = {10, 20, 30};
        public AspectOfTheCougar(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            Particle particle; // UNUSED
            float cooldownStat;
            float multiplier;
            float newCooldown;
            //RequireVar(this.armorMod);
            this.cD0 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.cD1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.cD2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.cougarID = PushCharacterData("Nidalee_Cougar", owner, true);
            SpellEffectCreate(out particle, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * 4;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            Particle particle; // UNUSED
            float cooldownStat;
            float multiplier;
            float newCooldown;
            float cD0;
            float cD1;
            float cD2;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Takedown)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Takedown), (ObjAIBase)owner, 0);
            }
            PopCharacterData(owner, this.cougarID);
            SpellEffectCreate(out particle, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * 4;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cD0 = this.cD0 - lifeTime;
            cD1 = this.cD1 - lifeTime;
            cD2 = this.cD2 - lifeTime;
            SetSlotSpellCooldownTimeVer2(cD0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(cD1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(cD2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorMod);
            IncFlatSpellBlockMod(owner, this.armorMod);
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level;
            if(slot == 3)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.armorMod = this.effect0[level];
            }
        }
    }
}
namespace Spells
{
    public class AspectOfTheCougar : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {10, 20, 30};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_armorMod;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AspectOfTheCougar)) > 0)
            {
                SpellBuffRemove(owner, default, (ObjAIBase)owner, 0);
            }
            else
            {
                nextBuffVars_armorMod = this.effect0[level];
                AddBuff(attacker, owner, new Buffs.AspectOfTheCougar(nextBuffVars_armorMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}