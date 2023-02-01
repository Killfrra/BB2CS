#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TeemoMushroomManager : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            int count;
            if(!owner.IsDead)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TeemoMushroomCounter)) == 0)
                {
                    count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.TeemoMushrooms));
                    if(count != 3)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.TeemoMushroomCounter(), 1, 1, charVars.MushroomCooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                }
            }
        }
    }
}