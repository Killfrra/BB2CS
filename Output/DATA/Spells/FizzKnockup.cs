#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzKnockup : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            Vector3 targetPos;
            targetPos = GetRandomPointInAreaPosition(owner.Position, 60, 60);
            Move(owner, targetPos, 125, 12, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.CANCEL_ORDER, 100, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            bool zombie;
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            zombie = GetIsZombie(owner);
            if(!zombie)
            {
                if(owner.IsDead)
                {
                    string name;
                    name = GetUnitSkinName(owner);
                    if(name == "Annie")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(name == "Annie")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(name == "Amumu")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(name == "Kennen")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(name == "Fizz")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(name == "Poppy")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(name == "Veigar")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if(name == "Tristana")
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSharkDissappear(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}