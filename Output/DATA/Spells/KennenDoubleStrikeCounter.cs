#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenDoubleStrikeCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KennenDoubleStrikeCounter",
            BuffTextureName = "Kennen_ElectricalSurge.dds",
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            charVars.Count++;
            if(charVars.Count >= 5)
            {
            }
            else
            {
                if(charVars.Count >= 4)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeProc(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KennenDoubleStrikeIndicator), 0);
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeIndicator(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true);
                }
            }
        }
    }
}