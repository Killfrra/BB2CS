#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KriggersBarrier : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.BlackShield)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.BlackShield), attacker);
                DebugSay(owner, "DISPELL BlackShield");
            }
            else
            {
                float nextBuffVars_ShieldHealth;
                nextBuffVars_ShieldHealth = 1000;
                DebugSay(owner, "ADD BlackShield 1000 Health");
                AddBuff(attacker, target, new Buffs.BlackShield(nextBuffVars_ShieldHealth), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.SPELL_IMMUNITY, 0, true, false);
            }
        }
    }
}