#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Drain : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 6f,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.DrainCheck(), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.DrainCheck)) > 0)
            {
                SpellCast(attacker, target, target.Position, target.Position, 0, SpellSlotType.ExtraSlots, level, true, false, false, true);
            }
        }
    }
}
namespace Buffs
{
    public class Drain : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Drain",
            BuffTextureName = "Fiddlesticks_ConjureScarecrow.dds",
        };
    }
}