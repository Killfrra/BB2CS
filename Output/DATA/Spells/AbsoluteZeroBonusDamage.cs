#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AbsoluteZeroBonusDamage : BBBuffScript
    {
        public override void OnUpdateActions()
        {
            int level;
            float baseDamage;
            float secondDamage;
            float totalTime;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AbsoluteZeroBonusDamage2)) > 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                baseDamage = level * 250;
                secondDamage = baseDamage + 250;
                totalTime = 0.25f * lifeTime;
                SpellEffectCreate(out _, out _, "AbsoluteZero_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                {
                    ApplyDamage(attacker, unit, secondDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, totalTime, 3);
                }
                SpellBuffRemove(owner, nameof(Buffs.AbsoluteZeroBonusDamage), (ObjAIBase)owner);
            }
        }
    }
}