#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RumbleCarpetBombMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
        };
        Particle particle; // UNUSED
        Particle particle1; // UNUSED
        int[] effect0 = {150, 225, 300};
        int[] effect1 = {150, 225, 300};
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            TeamId teamOfOwner;
            Vector3 groundHeight;
            Vector3 nextBuffVars_MissilePosition;
            int count;
            Minion other1;
            int rumbleSkinID;
            teamOfOwner = GetTeamID(attacker);
            groundHeight = GetGroundHeight(missilePosition);
            groundHeight = ModifyPosition(0, 10, 0);
            nextBuffVars_MissilePosition = groundHeight;
            AddBuff(attacker, attacker, new Buffs.RumbleCarpetBombMissile(nextBuffVars_MissilePosition), 5, 1, 0.25f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
            AddBuff(attacker, attacker, new Buffs.RumbleCarpetBomb(), 1, 1, 4.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
            AddBuff(attacker, attacker, new Buffs.RumbleCarpetBombCounter(), 6, 1, 4.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            count = GetBuffCountFromAll(attacker, nameof(Buffs.RumbleCarpetBombCounter));
            if(count == 1)
            {
                other1 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missilePosition, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, true, (Champion)owner);
                AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, other1, new Buffs.RumbleCarpetBombSound1(), 1, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
            if(count == 6)
            {
                other1 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missilePosition, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, true, (Champion)owner);
                AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, other1, new Buffs.RumbleCarpetBombSound2(), 1, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
            rumbleSkinID = GetSkinID(attacker);
            SpellEffectCreate(out this.particle, out _, "rumble_ult_placeholder_01.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, missilePosition, target, default, default, true, default, default, false, false);
            if(rumbleSkinID == 2)
            {
                SpellEffectCreate(out this.particle1, out _, "rumble_incoming_mis_cannon_ball.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, missilePosition, target, default, default, true, default, default, false, false);
            }
            else if(rumbleSkinID == 1)
            {
                SpellEffectCreate(out this.particle1, out _, "rumble_incoming_mis_pineapple.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, missilePosition, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle1, out _, "rumble_incoming_mis.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, missilePosition, target, default, default, true, default, default, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, missilePosition, 205, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(teamOfOwner == TeamId.TEAM_BLUE)
                {
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.RumbleCarpetBombBuffOrder)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.RumbleCarpetBombBuffOrder(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                    }
                }
                else
                {
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.RumbleCarpetBombBuffDest)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.RumbleCarpetBombBuffDest(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                    }
                }
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            bool isStealthed; // UNUSED
            Vector3 targetPos; // UNUSED
            teamID = GetTeamID(owner);
            isStealthed = GetStealthed(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            targetPos = GetUnitPosition(target);
            charVars.Counter = 0;
        }
    }
}
namespace Buffs
{
    public class RumbleCarpetBombMissile : BBBuffScript
    {
        Vector3 missilePosition;
        public RumbleCarpetBombMissile(Vector3 missilePosition = default)
        {
            this.missilePosition = missilePosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.missilePosition);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 nextBuffVars_MissilePosition;
            nextBuffVars_MissilePosition = this.missilePosition;
            AddBuff(attacker, owner, new Buffs.RumbleCarpetBombEffect(nextBuffVars_MissilePosition), 10, 1, 6, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
        }
    }
}