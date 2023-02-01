#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriSoulCrusher3 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "PotionofElusiveness_itm.troy", "PotionofBrilliance_itm.troy", "PotionofGiantStrength_itm.troy", },
            BuffName = "AhriSoulCrusher",
            BuffTextureName = "3017_Egitai_Twinsoul.dds",
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnDeactivate(bool expired)
        {
            AddBuff(attacker, attacker, new Buffs.AhriSoulCrusher4(), 1, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0.25f, true, false, false);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriFoxFire)) > 0)
                {
                }
                else
                {
                    SpellBuffClear(owner, nameof(Buffs.AhriSoulCrusher3));
                }
            }
        }
    }
}