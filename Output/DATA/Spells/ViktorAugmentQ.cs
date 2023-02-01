#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorAugmentQ : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId ownerTeam; // UNUSED
            SetSlotSpellIcon(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, 2);
            ownerTeam = GetTeamID(owner);
        }
        public override void OnUpdateStats()
        {
            float ownerMaxHealth;
            float healthtoAdd; // UNUSED
            ownerMaxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            healthtoAdd = ownerMaxHealth * 0.1f;
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float nextBuffVars_MoveSpeedMod;
            spellName = GetSpellName();
            nextBuffVars_MoveSpeedMod = 0.3f;
            if(spellName == nameof(Spells.ViktorPowerTransfer))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Haste(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else if(spellName == nameof(Spells.HexMageCatalyst))
            {
            }
            else if(spellName == nameof(Spells.HexMageChaosCharge))
            {
            }
            else if(spellName == nameof(Spells.HexMageChaoticStorm))
            {
            }
            else
            {
            }
        }
    }
}