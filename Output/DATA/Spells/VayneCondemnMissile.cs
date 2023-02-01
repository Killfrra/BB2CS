#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneCondemnMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VayneCondemn",
            BuffTextureName = "Vayne_Condemn.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        bool collide;
        Vector3 castPoint;
        float damage;
        public VayneCondemnMissile(Vector3 castPoint = default, float damage = default)
        {
            this.castPoint = castPoint;
            this.damage = damage;
        }
        public override void OnCollisionTerrain()
        {
            this.collide = true;
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            Particle hi; // UNUSED
            Vector3 castPoint;
            Vector3 ownerPos;
            float distance;
            float pushBack; // UNUSED
            float castOffset;
            Vector3 targetPos;
            this.collide = false;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.castPoint);
            //RequireVar(this.damage);
            SpellEffectCreate(out hi, out _, "vayne_E_tar.troy", default, TeamId.TEAM_NEUTRAL, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, false, false, false, false);
            StartTrackingCollisions(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            castPoint = this.castPoint;
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, castPoint);
            pushBack = distance + 475;
            castOffset = GetOffsetAngle(owner, this.castPoint);
            targetPos = GetPointByUnitFacingOffset(owner, -475, castOffset);
            Move(owner, targetPos, 2000, 0, 0, ForceMovementType.FIRST_COLLISION_HIT, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
        }
        public override void OnDeactivate(bool expired)
        {
            Particle hi; // UNUSED
            if(this.collide)
            {
                ApplyStun(attacker, owner, 1.5f);
                ApplyDamage(attacker, owner, this.damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
                SpellEffectCreate(out hi, out _, "Vayne_E_terrain_tar.troy", default, TeamId.TEAM_NEUTRAL, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, false, false, false, false);
            }
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            StartTrackingCollisions(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
        }
    }
}
namespace Spells
{
    public class VayneCondemnMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {45, 80, 115, 150, 185};
        float[] effect1 = {0.04f, 0.05f, 0.06f, 0.07f, 0.08f};
        int[] effect2 = {20, 30, 40, 50, 60};
        int[] effect3 = {45, 80, 115, 150, 185};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 nextBuffVars_CastPoint;
            float nextBuffVars_Damage;
            float damage;
            float aD;
            float bonusDamage;
            Particle hi; // UNUSED
            int count;
            TeamId teamID;
            TeamId teamIDTarget;
            Particle gragas; // UNUSED
            float tarMaxHealth;
            float rankScaling;
            float flatScaling;
            float damageToDeal;
            nextBuffVars_CastPoint = charVars.CastPoint;
            damage = this.effect0[level];
            aD = GetFlatPhysicalDamageMod(owner);
            bonusDamage = aD * 0.5f;
            damage += bonusDamage;
            nextBuffVars_Damage = damage;
            SpellEffectCreate(out hi, out _, "vayne_E_tar.troy", default, TeamId.TEAM_NEUTRAL, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, target, default, default, true, false, false, false, false);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.VayneSilveredBolts)) > 0)
            {
                count = GetBuffCountFromCaster(target, attacker, nameof(Buffs.VayneSilveredDebuff));
                if(count == 2)
                {
                    teamID = GetTeamID(attacker);
                    teamIDTarget = GetTeamID(target);
                    SpellEffectCreate(out gragas, out _, "vayne_W_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, target.Position, target, default, default, true, false, false, false, false);
                    level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SpellBuffClear(target, nameof(Buffs.VayneSilveredDebuff));
                    tarMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                    rankScaling = this.effect1[level];
                    flatScaling = this.effect2[level];
                    damageToDeal = tarMaxHealth * rankScaling;
                    damageToDeal += flatScaling;
                    if(teamIDTarget == TeamId.TEAM_NEUTRAL)
                    {
                        damageToDeal = Math.Min(damageToDeal, 200);
                    }
                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
                else
                {
                    AddBuff(attacker, target, new Buffs.VayneSilveredDebuff(), 3, 1, 3.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ApplyDamage(attacker, target, this.effect3[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0.5f, false, false, attacker);
            AddBuff(attacker, target, new Buffs.VayneCondemnMissile(nextBuffVars_CastPoint, nextBuffVars_Damage), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
        }
    }
}