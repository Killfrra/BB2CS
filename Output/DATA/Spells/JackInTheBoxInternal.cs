#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JackInTheBoxInternal : BBBuffScript
    {
        Vector3 targetPos;
        int bonusHealth;
        float fearDuration;
        public JackInTheBoxInternal(Vector3 targetPos = default, int bonusHealth = default, float fearDuration = default)
        {
            this.targetPos = targetPos;
            this.bonusHealth = bonusHealth;
            this.fearDuration = fearDuration;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.fearDuration);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Vector3 targetPos;
            int nextBuffVars_BonusHealth; // UNUSED
            Minion other3;
            float nextBuffVars_FearDuration;
            teamID = GetTeamID(owner);
            targetPos = this.targetPos;
            nextBuffVars_BonusHealth = this.bonusHealth;
            other3 = SpawnMinion("Jack In The Box", "ShacoBox", "turret.lua", targetPos, teamID ?? TeamId.TEAM_CASTER, false, false, true, false, false, false, 0, false, false, (Champion)attacker);
            nextBuffVars_FearDuration = this.fearDuration;
            AddBuff(attacker, other3, new Buffs.JackInTheBox(nextBuffVars_FearDuration), 1, 1, 60, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}