#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KatsudionsGlueGun : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.Slow)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.Slow), attacker);
                DebugSay(owner, "DISPELL SLOW !!");
            }
            else
            {
                nextBuffVars_MoveSpeedMod = -0.5f;
                DebugSay(owner, "TARGET SLOWED 50% !!");
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false);
            }
        }
    }
}