#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TeemoMushrooms : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Mushroom Stored",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateAmmo()
        {
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.TeemoMushrooms));
            if(count >= 3)
            {
                AddBuff(attacker, owner, new Buffs.TeemoMushrooms(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.TeemoMushrooms(), 4, 1, charVars.MushroomCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
    }
}