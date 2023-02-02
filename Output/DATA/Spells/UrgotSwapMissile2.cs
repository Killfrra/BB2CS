#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UrgotSwapMissile2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            count = GetBuffCountFromAll(target, nameof(Buffs.UrgotSwapMarker));
            if(count != 0)
            {
                DestroyMissile(missileNetworkID);
            }
        }
    }
}
namespace Buffs
{
    public class UrgotSwapMissile2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            Vector3 ownerPos;
            ownerPos = GetUnitPosition(owner);
            SetCameraPosition((Champion)owner, ownerPos);
        }
    }
}