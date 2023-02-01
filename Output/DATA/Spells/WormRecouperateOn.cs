#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WormRecouperateOn : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "WormRecouperateOn",
            BuffTextureName = "3011_Dawnseeker.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WormRecoupDebuff)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecouperate1(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}