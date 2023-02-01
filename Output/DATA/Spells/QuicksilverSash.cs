#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class QuicksilverSash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle castParticle; // UNUSED
            float slotCheck;
            string name;
            SpellEffectCreate(out castParticle, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            DispellNegativeBuffs(owner);
            slotCheck = 0;
            while(slotCheck <= 5)
            {
                name = GetSlotSpellName((ObjAIBase)owner, slotCheck, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
                if(name == nameof(Spells.QuicksilverSash))
                {
                    SetSlotSpellCooldownTimeVer2(90, slotCheck, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                }
                slotCheck++;
            }
        }
    }
}