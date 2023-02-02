#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptAncientGolem : BBCharScript
    {
        public override void OnActivate()
        {
            int nextBuffVars_spawnTime;
            int nextBuffVars_healthPerMinute;
            float nextBuffVars_damagePerMinute;
            float nextBuffVars_areaDmgReduction; // UNUSED
            float nextBuffVars_goldPerMinute;
            float nextBuffVars_expPerMinute;
            bool nextBuffVars_upgradeTimer;
            nextBuffVars_spawnTime = 101;
            nextBuffVars_healthPerMinute = 90;
            nextBuffVars_damagePerMinute = 1.825f;
            nextBuffVars_areaDmgReduction = 0.2f;
            nextBuffVars_goldPerMinute = 0.324f;
            nextBuffVars_expPerMinute = 2.5f;
            nextBuffVars_upgradeTimer = true;
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.CrestoftheAncientGolem(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncPermanentFlatHPPoolMod(owner, 100);
        }
    }
}