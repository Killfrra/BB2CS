#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptRammusDBC : BBCharScript
    {
        int[] effect0 = {6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4};
        int[] effect1 = {1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3};
        public override void SetVarsByLevel()
        {
            charVars.KillsPerArmor = this.effect0[level];
            charVars.ArmorPerChampionKill = this.effect1[level];
        }
        public override void OnKill()
        {
            bool nextBuffVars_IsChampion;
            if(target is ObjAIBase)
            {
                if(target is Champion)
                {
                    nextBuffVars_IsChampion = true;
                }
                else
                {
                    nextBuffVars_IsChampion = false;
                }
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ScavengeArmor)) > 0)
                {
                    charVars.NumMinionsKilled++;
                }
                else
                {
                    charVars.NumMinionsKilled = 1;
                    charVars.ScavengeArmorTotal = 0;
                }
                AddBuff(attacker, owner, new Buffs.ScavengeArmor(), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false);
        }
    }
}