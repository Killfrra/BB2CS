#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptblueDragon : BBCharScript
    {
        public override void OnUpdateStats()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DragonApplicator)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.DragonApplicator(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 100000, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                float nextBuffVars_HPPerLevel;
                nextBuffVars_HPPerLevel = 200;
                AddBuff((ObjAIBase)owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ResistantSkinDragon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}