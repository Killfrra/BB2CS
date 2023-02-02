#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptGiantWolf : BBCharScript
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
            nextBuffVars_healthPerMinute = 31;
            nextBuffVars_damagePerMinute = 0.44f;
            nextBuffVars_areaDmgReduction = 0.2f;
            nextBuffVars_goldPerMinute = 0.43f;
            nextBuffVars_expPerMinute = 1.5f;
            nextBuffVars_upgradeTimer = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RegenerationRuneAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPermanentFlatPhysicalDamageMod(owner, 2);
            IncPermanentGoldReward(owner, 6);
            IncPermanentExpReward(owner, 14);
        }
    }
}