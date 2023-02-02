#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MissileBarrage : BBSpellScript
    {
        public override bool CanCast()
        {
            bool returnValue = true;
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.MissileBarrage));
            if(count <= 1)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 pos;
            int count;
            Vector3 ownerPos;
            float distance;
            int barrageCount;
            pos = GetCastSpellTargetPos();
            count = GetBuffCountFromAll(owner, nameof(Buffs.MissileBarrage));
            if(count >= 8)
            {
                SpellBuffRemove(owner, nameof(Buffs.MissileBarrage), (ObjAIBase)owner, charVars.ChargeCooldown);
            }
            else
            {
                SpellBuffRemove(owner, default, (ObjAIBase)owner, 0);
            }
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, pos);
            FaceDirection(owner, pos);
            if(distance > 1200)
            {
                pos = GetPointByUnitFacingOffset(owner, 1150, 0);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MBCheck2)) > 0)
            {
                SpellCast((ObjAIBase)owner, default, pos, pos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.MBCheck2), (ObjAIBase)owner, 0);
            }
            else
            {
                SpellCast((ObjAIBase)owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
            barrageCount = GetBuffCountFromAll(owner, nameof(Buffs.CorkiMissileBarrageNC));
            if(barrageCount == 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MBCheck2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.CorkiMissileBarrageNC), 3);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CorkiMissileBarrageNC(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class MissileBarrage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissileBarrageCheck",
            BuffTextureName = "Corki_MissileBarrage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateAmmo()
        {
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.MissileBarrage));
            if(count == 7)
            {
                AddBuff(attacker, owner, new Buffs.MissileBarrage(), 8, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.MissileBarrage(), 8, 1, charVars.ChargeCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
    }
}