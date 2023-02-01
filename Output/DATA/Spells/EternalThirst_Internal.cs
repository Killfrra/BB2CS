#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EternalThirst_Internal : BBBuffScript
    {
        float lifeStealAmount;
        public EternalThirst_Internal(float lifeStealAmount = default)
        {
            this.lifeStealAmount = lifeStealAmount;
        }
        public override void OnActivate()
        {
            int count;
            float lifeStealToHeal;
            //RequireVar(this.lifeStealAmount);
            count = GetBuffCountFromAll(owner, nameof(Buffs.EternalThirst));
            lifeStealToHeal = this.lifeStealAmount * count;
            IncHealth(attacker, lifeStealToHeal, attacker);
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
        }
    }
}