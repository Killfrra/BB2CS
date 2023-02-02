#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ItemPlacementMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.ItemPlacementMissile)) > 0)
            {
                DestroyMissile(missileNetworkID);
            }
            SpellEffectCreate(out _, out _, "ItemPlacement_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
        }
    }
}
namespace Buffs
{
    public class ItemPlacementMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
    }
}