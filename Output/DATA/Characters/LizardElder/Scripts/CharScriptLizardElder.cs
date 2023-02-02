#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptLizardElder : BBCharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime;
            float nextBuffVars_areaDmgReduction; // UNUSED
            float nextBuffVars_healthPerMinute;
            float nextBuffVars_damagePerMinute;
            float nextBuffVars_goldPerMinute;
            float nextBuffVars_expPerMinute;
            bool nextBuffVars_upgradeTimer;
            nextBuffVars_spawnTime = 101;
            nextBuffVars_areaDmgReduction = 0.2f;
            nextBuffVars_healthPerMinute = 90;
            nextBuffVars_damagePerMinute = 1.825f;
            nextBuffVars_goldPerMinute = 0.342f;
            nextBuffVars_expPerMinute = 2.5f;
            nextBuffVars_upgradeTimer = true;
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.BlessingoftheLizardElder(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 100000, true, false, false);
            IncPermanentFlatHPPoolMod(owner, 100);
        }
    }
}