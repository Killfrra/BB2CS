#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdinOrderTurretShrine : BBCharScript
    {
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            int count;
            float multiplier;
            count = GetBuffCountFromAll(target, nameof(Buffs.OdinTurretDamage));
            if(count > 0)
            {
                multiplier = count * 0.4f;
                multiplier++;
                damageAmount *= multiplier;
            }
            AddBuff((ObjAIBase)owner, target, new Buffs.OdinTurretDamage(), 8, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnActivate()
        {
            float nextBuffVars_BonusHealth;
            float nextBuffVars_BubbleSize;
            float range; // UNUSED
            Vector3 ownerPosition;
            TeamId myTeam;
            Region perceptionBubble; // UNUSED
            TeamId enemyTeam;
            Region perceptionBubble2; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.TurretBonus(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60, true, false, false);
            nextBuffVars_BonusHealth = 0;
            nextBuffVars_BubbleSize = 1600;
            AddBuff((ObjAIBase)owner, owner, new Buffs.TurretBonusHealth(nextBuffVars_BonusHealth, nextBuffVars_BubbleSize), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.CallForHelpManager(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            range = GetFlatAttackRangeMod(owner);
            ownerPosition = GetUnitPosition(owner);
            myTeam = GetTeamID(owner);
            SetTargetable(owner, false);
            ownerPosition = GetUnitPosition(owner);
            myTeam = GetTeamID(owner);
            perceptionBubble = AddPosPerceptionBubble(myTeam, 1600, ownerPosition, 25000, owner, true);
            enemyTeam = TeamId.TEAM_PURPLE;
            perceptionBubble2 = AddPosPerceptionBubble(enemyTeam, 50, ownerPosition, 25000, default, false);
            SetDodgePiercing(owner, true);
        }
    }
}