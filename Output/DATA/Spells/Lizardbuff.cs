#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Lizardbuff : BBBuffScript
    {
        public override void OnDeath()
        {
            AddBuff((ObjAIBase)owner, attacker, new Buffs.BlessingoftheLizardElder(), 1, 1, 180, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            SpellEffectCreate(out _, out _, "NeutralMonster_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false);
        }
    }
}