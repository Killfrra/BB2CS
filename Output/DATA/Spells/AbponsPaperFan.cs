#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AbponsPaperFan : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_ShieldHealth;
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.Stun)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.Stun), attacker);
                DebugSay(owner, "DISPELL STUN !!");
            }
            else
            {
                nextBuffVars_ShieldHealth = 1000;
                DebugSay(owner, "TWHAP!  Target STUNNED !!");
                AddBuff(attacker, target, new Buffs.Stun(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false);
            }
        }
    }
}