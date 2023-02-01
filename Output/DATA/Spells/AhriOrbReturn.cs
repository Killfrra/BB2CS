#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AhriOrbReturn : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_OrbofDeceptionIsActive;
            if(target != attacker)
            {
                nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
                AddBuff(attacker, target, new Buffs.AhriOrbDamageSilence(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                DestroyMissile(missileNetworkID);
                if(charVars.OrbofDeceptionIsActive == 1)
                {
                    charVars.OrbofDeceptionIsActive = 0;
                }
            }
        }
    }
}