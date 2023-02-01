#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickDecayed : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonDecayed)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickSummonDecayed));
            }
            targetPos = GetCastSpellTargetPos();
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
        }
    }
}