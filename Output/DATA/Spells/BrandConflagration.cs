#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BrandConflagration : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {70, 105, 140, 175, 210};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int brandSkinID;
            TeamId teamID;
            Particle varA; // UNUSED
            brandSkinID = GetSkinID(attacker);
            teamID = GetTeamID(attacker);
            if(brandSkinID == 3)
            {
                SpellEffectCreate(out varA, out _, "BrandConflagration_buf_frost.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out varA, out _, "BrandConflagration_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.BrandAblaze)) > 0)
            {
                AddBuff(attacker, target, new Buffs.BrandConflagration(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.55f, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class BrandConflagration : BBBuffScript
    {
        public override void OnActivate()
        {
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(unit != target)
                {
                    Vector3 targetPos;
                    int level;
                    targetPos = GetUnitPosition(target);
                    level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SpellCast(attacker, unit, default, default, 2, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, targetPos);
                }
            }
        }
    }
}