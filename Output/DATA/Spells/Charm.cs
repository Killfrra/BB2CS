#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Charm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Charm_tar.troy", },
            BuffName = "Charm",
            BuffTextureName = "48thSlave_Pacify.dds",
        };
        public override void OnActivate()
        {
            SetCharmed(owner, true);
            RedirectGold(owner, attacker);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCharmed(owner, false);
            RedirectGold(owner);
        }
        public override void OnUpdateStats()
        {
            SetCharmed(owner, true);
            IncPercentPhysicalDamageMod(owner, -0.3f);
        }
    }
}