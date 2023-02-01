#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrailblazerApplicator : BBBuffScript
    {
        float moveSpeedMod;
        Region thisBubble;
        public TrailblazerApplicator(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetNoRender(owner, true);
            SetIgnoreCallForHelp(owner, true);
            this.thisBubble = AddPosPerceptionBubble(attacker, 300, owner.Position, 5000, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1);
            RemovePerceptionBubble(this.thisBubble);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_MoveSpeedMod;
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 150, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf))
            {
                AddBuff(attacker, unit, new Buffs.TrailblazerTarget(nextBuffVars_MoveSpeedMod), 1, 1, 2.1f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0);
            }
        }
    }
}