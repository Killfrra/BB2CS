#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SowTheWindCastMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SowTheWindCastMarker",
        };
        public override void OnActivate()
        {
            SetTargetingType(1, SpellSlotType.SpellSlots, default, TargetingType.Self, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetingType(1, SpellSlotType.SpellSlots, default, TargetingType.Target, owner);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.SowTheWind))
            {
                ObjAIBase caster;
                caster = SetBuffCasterUnit();
                SpellBuffRemove(caster, nameof(Buffs.SowTheWind), (ObjAIBase)owner);
                SpellBuffRemove(owner, nameof(Buffs.SowTheWindCastMarker), caster);
            }
        }
    }
}