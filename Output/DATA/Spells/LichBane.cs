#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class LichBane : BBCharScript
    {
        public override void OnLaunchAttack()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.4f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
    }
}
namespace Buffs
{
    public class LichBane : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[]{ "bluehands_buf.troy", "bluehands_buf.troy", },
            BuffName = "LichBane",
            BuffTextureName = "126_Zeal_and_Sheen.dds",
        };
        float abilityPower;
        public LichBane(float abilityPower = default)
        {
            this.abilityPower = abilityPower;
        }
        public override void OnActivate()
        {
            //RequireVar(this.abilityPower);
        }
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Sheen)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Sheen), (ObjAIBase)owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    damageAmount += this.abilityPower;
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.SheenDelay(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                    SpellBuffClear(owner, nameof(Buffs.LichBane));
                }
            }
        }
    }
}