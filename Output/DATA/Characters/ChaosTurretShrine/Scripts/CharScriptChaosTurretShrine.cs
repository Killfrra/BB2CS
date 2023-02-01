#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptChaosTurretShrine : BBCharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_BonusHealth;
            float nextBuffVars_BubbleSize;
            AddBuff((ObjAIBase)owner, owner, new Buffs.TurretBonus(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60);
            nextBuffVars_BonusHealth = 0;
            nextBuffVars_BubbleSize = 1600;
            AddBuff((ObjAIBase)owner, owner, new Buffs.TurretBonusHealth(nextBuffVars_BonusHealth, nextBuffVars_BubbleSize), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
        }
    }
}