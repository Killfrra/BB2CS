#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdinQuestBuff : BBCharScript
    {
        public override void OnActivate()
        {
            SetNoRender(owner, false);
            SetCanMove(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                float newDuration;
                newDuration = 50;
                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                {
                    newDuration *= 1.2f;
                }
                AddBuff((ObjAIBase)unit, unit, new Buffs.OdinQuestBuff(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUseable, nameof(Buffs.OdinGuardianBuff), true))
            {
                AddBuff((ObjAIBase)unit, unit, new Buffs.OdinQuestBuffParticle(), 1, 1, 50, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
        }
    }
}