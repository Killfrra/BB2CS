#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinQuestParticleRemover : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = false,
        };
        public override void OnDeath()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUseable, nameof(Buffs.OdinGuardianBuff), true))
            {
                SpellBuffClear(unit, nameof(Buffs.OdinQuestIndicator));
            }
        }
    }
}