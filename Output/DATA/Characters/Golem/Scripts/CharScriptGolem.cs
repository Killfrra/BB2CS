#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptGolem : BBCharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime;
            float nextBuffVars_healthPerMinute;
            float nextBuffVars_damagePerMinute;
            float nextBuffVars_areaDmgReduction; // UNUSED
            float nextBuffVars_goldPerMinute;
            float nextBuffVars_expPerMinute;
            bool nextBuffVars_upgradeTimer;
            nextBuffVars_spawnTime = 101;
            nextBuffVars_healthPerMinute = 37;
            nextBuffVars_damagePerMinute = 1.05f;
            nextBuffVars_areaDmgReduction = 0.2f;
            nextBuffVars_goldPerMinute = 0.38f;
            nextBuffVars_expPerMinute = 1.55f;
            nextBuffVars_upgradeTimer = false;
            AddBuff(attacker, attacker, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RegenerationRuneAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPermanentFlatHPPoolMod(owner, 150);
            IncPermanentFlatPhysicalDamageMod(owner, 3);
            IncPermanentGoldReward(owner, 8);
            IncPermanentExpReward(owner, 24);
        }
    }
}