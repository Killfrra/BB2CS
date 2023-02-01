#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KarmaSpiritBondEnemyTooltip : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaSpiritBondEnemy",
            BuffTextureName = "KarmaSpiritBond.dds",
        };
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.KarmaSpiritBondC)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}