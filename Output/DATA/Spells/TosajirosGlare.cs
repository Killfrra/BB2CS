#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TosajirosGlare : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.LuxLightBinding)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.LuxLightBinding), attacker);
                DebugSay(owner, "DISPELL ROOT !!");
            }
            else
            {
                nextBuffVars_MoveSpeedMod = -0.5f;
                DebugSay(owner, "TARGET BINDED !!");
                AddBuff(attacker, target, new Buffs.LuxLightBinding(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
            }
        }
    }
}