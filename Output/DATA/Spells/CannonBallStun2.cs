#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CannonBallStun2 : BBBuffScript
    {
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CannonBallStun)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.CannonBallStun), (ObjAIBase)owner);
            }
            ApplyStun(owner, owner, 0.75f);
        }
    }
}