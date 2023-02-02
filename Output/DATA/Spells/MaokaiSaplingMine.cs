#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiSaplingMine : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };
        float mineDamageAmount;
        bool sprung;
        TeamId teamID; // UNUSED
        bool active;
        AttackableUnit homingBeacon;
        bool detonated;
        float sprungCount;
        Particle particle;
        Particle particle2;
        Particle particle3;
        Particle particle4;
        float lastTimeExecuted;
        Region perceptionBubble; // UNUSED
        public MaokaiSaplingMine(float mineDamageAmount = default, bool sprung = default)
        {
            this.mineDamageAmount = mineDamageAmount;
            this.sprung = sprung;
        }
        public override void OnActivate()
        {
            Vector3 pos;
            //RequireVar(this.sprung);
            if(!this.sprung)
            {
                //RequireVar(this.mineDamageAmount);
                SetCanMove(owner, false);
                SetGhosted(owner, true);
                SetInvulnerable(owner, true);
                SetTargetable(owner, false);
                SetCanAttack(owner, false);
                this.teamID = GetTeamID(owner);
                this.active = false;
                this.homingBeacon;
                this.detonated = false;
                this.sprungCount = 0;
            }
            pos = GetPointByUnitFacingOffset(owner, 10, 180);
            FaceDirection(owner, pos);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            if(!this.detonated)
            {
                Particle particle; // UNUSED
                SpellEffectCreate(out particle, out _, "maoki_sapling_detonate.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, default, default, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.mineDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                }
            }
            SetInvulnerable(owner, false);
            SetNoRender(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
            SpellEffectRemove(this.particle4);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                TeamId teamID;
                teamID = GetTeamID(attacker);
                if(this.active)
                {
                    bool canSee;
                    if(this.sprung)
                    {
                        AttackableUnit other1;
                        Particle particle; // UNUSED
                        SetCanMove(owner, true);
                        other1 = this.homingBeacon;
                        this.sprungCount++;
                        if(this.sprungCount >= 11)
                        {
                            if(!this.detonated)
                            {
                                this.detonated = true;
                                SpellEffectCreate(out particle, out _, "maoki_sapling_detonate.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, default, default, false, false);
                                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                                {
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.mineDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                                }
                            }
                            SpellBuffRemoveCurrent(owner);
                            BreakExecution();
                        }
                        else
                        {
                            if(this.sprungCount >= 2)
                            {
                                IssueOrder(owner, OrderType.MoveTo, default, other1);
                                if(this.sprungCount == 2)
                                {
                                    PlayAnimation("Run", 0, owner, false, false, false);
                                }
                            }
                            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 10, default, true))
                            {
                                canSee = CanSeeTarget(owner, unit);
                                if(canSee)
                                {
                                    if(!this.detonated)
                                    {
                                        SpellBuffRemove(owner, nameof(Buffs.MaokaiSapling2), (ObjAIBase)owner, 0);
                                        this.detonated = true;
                                        SpellEffectCreate(out particle, out _, "maoki_sapling_detonate.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, default, default, false, false);
                                        foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                                        {
                                            BreakSpellShields(unit);
                                            ApplyDamage(attacker, unit, this.mineDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                                        }
                                    }
                                }
                                SpellBuffRemoveCurrent(owner);
                            }
                        }
                    }
                    else
                    {
                        foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 8, default, true))
                        {
                            canSee = CanSeeTarget(owner, unit);
                            if(canSee)
                            {
                                if(!this.sprung)
                                {
                                    TeamId unitTeam;
                                    unitTeam = GetTeamID(unit);
                                    this.perceptionBubble = AddUnitPerceptionBubble(unitTeam, 10, owner, 2.5f, default, owner, false);
                                    this.sprung = true;
                                    this.sprungCount = 0;
                                    this.homingBeacon = unit;
                                    FaceDirection(owner, unit.Position);
                                    OverrideAnimation("Idle1", "Pop", owner);
                                    AddBuff((ObjAIBase)owner, owner, new Buffs.MaokaiSapling2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.sprungCount++;
                    if(this.sprungCount >= 2)
                    {
                        this.sprungCount = 0;
                        this.active = true;
                        SpellEffectCreate(out this.particle, out this.particle2, "maokai_sapling_rdy_indicator_green.troy", "maokai_sapling_rdy_indicator_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "BUFFBONE_CSTM_STEM_3", default, target, default, default, false, default, default, false, false);
                        SpellEffectCreate(out this.particle3, out this.particle4, "maokai_sapling_team_id_green.troy", "maokai_sapling_team_id_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
                    }
                }
            }
        }
    }
}