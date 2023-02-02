#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GalioRighteousGustMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.2f, 0.28f, 0.36f, 0.44f, 0.52f};
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            TeamId teamID;
            Vector3 targetPos;
            float nextBuffVars_MoveSpeedMod;
            Minion other3;
            teamID = GetTeamID(owner);
            level = GetCastSpellLevelPlusOne();
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            other3 = SpawnMinion("RighteousGust", "TestCube", "idle.lua", missilePosition, teamID ?? TeamId.TEAM_UNKNOWN, false, false, false, false, false, true, 100, false, true);
            FaceDirection(other3, targetPos);
            AddBuff((ObjAIBase)owner, other3, new Buffs.GalioRighteousGustMissile(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GalioRighteousGustMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GalioRighteousGustMissile",
            BuffTextureName = "",
        };
        float moveSpeedMod;
        Particle windVFXAlly;
        Particle windVFXEnemy;
        int level; // UNUSED
        float lastTimeExecuted;
        public GalioRighteousGustMissile(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            float moveSpeedMod; // UNUSED
            TeamId teamID;
            Vector3 orientationPoint;
            SetNoRender(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetStealthed(owner, true);
            //RequireVar(this.moveSpeedMod);
            moveSpeedMod = this.moveSpeedMod;
            teamID = GetTeamID(owner);
            orientationPoint = GetPointByUnitFacingOffset(owner, 10000, 0);
            SpellEffectCreate(out this.windVFXAlly, out this.windVFXEnemy, "galio_windTunnel_rune.troy", "galio_windTunnel_rune_team_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "head", owner.Position, owner, default, default, false, default, default, false, false, orientationPoint);
            this.level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.ExtraSlots);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.windVFXAlly);
            SpellEffectRemove(this.windVFXEnemy);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetStealthed(owner, true);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_MoveSpeedMod;
            TeamId teamID;
            Champion other1;
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            teamID = GetTeamID(owner);
            other1 = GetChampionBySkinName("Galio", teamID ?? TeamId.TEAM_UNKNOWN);
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 175, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.GalioRighteousGustMissile), false))
                {
                    if(unit != attacker)
                    {
                        if(IsInFront(attacker, unit))
                        {
                            if(IsBehind(unit, attacker))
                            {
                                AddBuff(other1, unit, new Buffs.GalioRighteousGustHaste(nextBuffVars_MoveSpeedMod), 1, 1, 0.3f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                ApplyAssistMarker(other1, unit, 10);
                            }
                            else
                            {
                                SpellBuffRemove(unit, nameof(Buffs.GalioRighteousGustHaste), other1, 0);
                            }
                        }
                        else
                        {
                            SpellBuffRemove(unit, nameof(Buffs.GalioRighteousGustHaste), other1, 0);
                        }
                    }
                }
            }
        }
    }
}