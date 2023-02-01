#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VorpalSpikes : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Vorpal Spikes",
            BuffTextureName = "GreenTerror_ChitinousExoplates.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
    }
}
namespace Spells
{
    public class VorpalSpikes : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VorpalSpikes)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.VorpalSpikes), (ObjAIBase)owner);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.VorpalSpikes(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            }
        }
    }
}