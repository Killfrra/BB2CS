#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PinkCard : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        bool willRemove;
        Particle effectID;
        int[] effect0 = {50, 100, 150, 200, 250};
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
            SpellEffectCreate(out this.effectID, out _, "Card_Yellow_Tag.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SpellEffectRemove(this.effectID);
            baseCooldown = 4;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(target is ObjAIBase)
            {
                Particle ar; // UNUSED
                SpellEffectCreate(out ar, out _, "DeathsCaress_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
            {
                BreakSpellShields(unit);
                ApplyStun(attacker, unit, 1.25f);
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0);
            }
            this.willRemove = true;
        }
    }
}