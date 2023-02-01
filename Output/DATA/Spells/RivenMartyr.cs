#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenMartyr : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BlindMonkSafeguard",
            IsDeathRecapSource = true,
        };
        Particle temp;
        int[] effect0 = {50, 80, 110, 140, 170};
        int[] effect1 = {50, 80, 110, 140, 170};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.RivenFengShuiEngine)) > 0)
            {
                SpellEffectCreate(out this.temp, out _, "exile_W_cast_ult_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.temp, out _, "exile_W_cast_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.temp, out _, "exile_W_weapon_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.temp, out _, "exile_W_cast_ult_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 360, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    if(!unit.IsDead)
                    {
                        ApplyStun(attacker, unit, 0.75f);
                    }
                }
            }
            else
            {
                SpellEffectCreate(out this.temp, out _, "exile_W_cast_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.temp, out _, "exile_W_cast_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.temp, out _, "exile_W_weapon_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false, false, false, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.effect1[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    if(!unit.IsDead)
                    {
                        ApplyStun(attacker, unit, 0.75f);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.temp);
        }
    }
}
namespace Spells
{
    public class RivenMartyr : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}