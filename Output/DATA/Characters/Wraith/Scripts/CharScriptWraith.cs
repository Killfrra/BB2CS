#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptWraith : BBCharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime;
            float nextBuffVars_healthPerMinute;
            float nextBuffVars_damagePerMinute;
            float nextBuffVars_areaDmgReduction;
            float nextBuffVars_goldPerMinute;
            float nextBuffVars_expPerMinute;
            bool nextBuffVars_upgradeTimer;
            AddBuff(attacker, owner, new Buffs.LifestealAttack(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_spawnTime = 101;
            nextBuffVars_healthPerMinute = 28;
            nextBuffVars_damagePerMinute = 0.5f;
            nextBuffVars_areaDmgReduction = 0.2f;
            nextBuffVars_goldPerMinute = 0.48f;
            nextBuffVars_expPerMinute = 1.4f;
            nextBuffVars_upgradeTimer = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RegenerationRuneAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPermanentExpReward(owner, 33);
            IncPermanentFlatPhysicalDamageMod(owner, 1);
        }
    }
}