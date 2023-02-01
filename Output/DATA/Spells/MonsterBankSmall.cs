#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonsterBankSmall : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Monster Bank Small",
            BuffTextureName = "23.dds",
        };
        float lastTimeExecuted;
        float numberUpgrades;
        public override void OnUpdateActions()
        {
            float healthPercent;
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(healthPercent >= 0.99f)
            {
                if(ExecutePeriodically(5, ref this.lastTimeExecuted, false))
                {
                    if(this.numberUpgrades > 0)
                    {
                        IncPermanentExpReward(owner, 1.786f);
                        IncPermanentGoldReward(owner, 0.2667f);
                        this.numberUpgrades--;
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.MonsterBankBig(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        SpellBuffClear(owner, nameof(Buffs.MonsterBankSmall));
                    }
                }
            }
        }
        public override void OnActivate()
        {
            this.numberUpgrades = 14;
            IncPermanentExpReward(owner, 12.5f);
            IncPermanentGoldReward(owner, 2);
        }
    }
}