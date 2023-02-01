#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OracleElixirSight : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "ElixirSight_aura_02.troy", },
            BuffName = "OracleElixir",
            BuffTextureName = "2026_Arcane_Protection_Potion.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Region thisBubble;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            this.thisBubble = AddUnitPerceptionBubble(teamID, 750, owner, 25000, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.thisBubble);
        }
    }
}
namespace Spells
{
    public class OracleElixirSight : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)target, target, new Buffs.OracleElixirSight(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}