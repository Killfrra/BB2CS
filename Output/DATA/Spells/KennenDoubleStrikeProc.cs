#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenDoubleStrikeProc : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "KennenDoubleStrikeProc",
            BuffTextureName = "Kennen_ElectricalSurge.dds",
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is not BaseTurret)
                    {
                        if(target is ObjAIBase)
                        {
                            charVars.Count++;
                            AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeIndicator(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true);
                            if(charVars.Count >= 4)
                            {
                                AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeLive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
                                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KennenDoubleStrikeIndicator), 0);
                            }
                        }
                    }
                }
            }
        }
    }
}