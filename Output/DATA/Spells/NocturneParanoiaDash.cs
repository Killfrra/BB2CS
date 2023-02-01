#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneParanoiaDash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NocturneParanoia",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        float dashSpeed;
        Vector3 targetPos;
        Particle greenDash;
        bool hasDealtDamage;
        Particle selfParticle;
        bool willRemove;
        float damageToDeal;
        int[] effect0 = {3500, 4250, 5000};
        int[] effect1 = {150, 250, 350};
        public NocturneParanoiaDash(float dashSpeed = default, Vector3 targetPos = default, Particle greenDash = default)
        {
            this.dashSpeed = dashSpeed;
            this.targetPos = targetPos;
            this.greenDash = greenDash;
        }
        public override void OnActivate()
        {
            int level;
            Vector3 targetPos; // UNUSED
            float maxTrackDistance;
            float distanceCheck; // UNITIALIZED
            float baseDamage;
            float physPreMod;
            float physPostMod;
            float distance;
            Particle asdf; // UNUSED
            OverrideAnimation("Run", "Spell4", owner);
            //RequireVar(this.greenDash);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.hasDealtDamage = false;
            targetPos = this.targetPos;
            maxTrackDistance = this.effect0[level];
            MoveToUnit(owner, attacker, this.dashSpeed, 0, ForceMovementOrdersType.CANCEL_ORDER, 0, maxTrackDistance, distanceCheck, 0);
            SpellEffectCreate(out this.selfParticle, out _, "NocturneParanoiaDash_trail.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, default, default, false, false);
            this.willRemove = false;
            SetCanAttack(owner, false);
            SetGhosted(owner, true);
            baseDamage = this.effect1[level];
            physPreMod = GetFlatPhysicalDamageMod(owner);
            physPostMod = 1.2f * physPreMod;
            this.damageToDeal = physPostMod + baseDamage;
            if(!this.hasDealtDamage)
            {
                distance = DistanceBetweenObjects("Owner", "Attacker");
                if(distance <= 300)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, this.damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, true, true, (ObjAIBase)owner);
                    SpellEffectCreate(out asdf, out _, "NocturneParanoiaDash_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                    this.hasDealtDamage = true;
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.greenDash);
            SpellEffectRemove(this.selfParticle);
            SetCanAttack(owner, true);
            SetGhosted(owner, false);
            ClearOverrideAnimation("Run", owner);
            SpellBuffRemove(owner, nameof(Buffs.UnstoppableForceMarker), (ObjAIBase)owner, 0);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
        }
        public override void OnUpdateActions()
        {
            float distance;
            Particle asdf; // UNUSED
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
            if(!this.hasDealtDamage)
            {
                distance = DistanceBetweenObjects("Owner", "Attacker");
                if(distance <= 300)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, this.damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, true, true, (ObjAIBase)owner);
                    SpellEffectCreate(out asdf, out _, "NocturneParanoiaDash_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                    this.hasDealtDamage = true;
                }
            }
        }
        public override void OnMoveEnd()
        {
            TeamId teamOfOwner;
            SetCanAttack(owner, false);
            this.willRemove = true;
            SpellBuffRemove(owner, nameof(Buffs.NocturneParanoiaDash), (ObjAIBase)owner, 0);
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, default, true))
                {
                    SpellBuffClear(unit, nameof(Buffs.NocturneParanoiaDashSound));
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, default, true))
                {
                    SpellBuffClear(unit, nameof(Buffs.NocturneParanoiaDashSound));
                }
            }
        }
        public override void OnMoveSuccess()
        {
            if(attacker is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, attacker);
            }
        }
    }
}