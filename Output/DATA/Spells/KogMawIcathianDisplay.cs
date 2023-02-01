#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawIcathianDisplay : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "KogMawIcathianDisplay",
            BuffTextureName = "KogMaw_IcathianSurprise.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            int levelDamage;
            float bonusDamage;
            float totalDamage;
            levelDamage = GetLevel(owner);
            bonusDamage = levelDamage * 25;
            totalDamage = bonusDamage + 100;
            SetBuffToolTipVar(1, totalDamage);
        }
    }
}