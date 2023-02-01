#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RegenerationRune : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        bool bountyActive; // UNUSED
        public override void OnActivate()
        {
            this.bountyActive = false;
        }
        public override void OnUpdateStats()
        {
            float healthPercent;
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(healthPercent >= 0.99f)
            {
                if(lifeTime >= 45)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonsterBankSmall)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonsterBankBig)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.MonsterBankSmall(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}