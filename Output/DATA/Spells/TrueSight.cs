#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TrueSight : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)target, target, new Buffs.TrueSight(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class TrueSight : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Magical Sight",
            BuffTextureName = "2026_Arcane_Protection_Potion.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Region thisBubble;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            this.thisBubble = AddUnitPerceptionBubble(teamID, 700, owner, 25000, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.thisBubble);
        }
    }
}