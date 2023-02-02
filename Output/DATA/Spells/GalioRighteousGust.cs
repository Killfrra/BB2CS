#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GalioRighteousGust : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.2f, 0.28f, 0.36f, 0.44f, 0.52f};
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance; // UNUSED
            Vector3 nextBuffVars_TargetPos;
            Minion other3;
            float nextBuffVars_MoveSpeedMod;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 1100, 0);
            nextBuffVars_TargetPos = targetPos;
            other3 = SpawnMinion("RighteousGustLauncher", "TestCubeRender", "idle.lua", ownerPos, teamID ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            AddBuff((ObjAIBase)owner, other3, new Buffs.GalioRighteousGust(nextBuffVars_TargetPos), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            AddBuff(attacker, attacker, new Buffs.GalioRighteousGustHaste(nextBuffVars_MoveSpeedMod), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GalioRighteousGust : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "GalioRighteousGust",
            BuffTextureName = "",
        };
        Vector3 targetPos;
        int level;
        int[] effect0 = {60, 105, 150, 195, 240};
        public GalioRighteousGust(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            float aPMod;
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetStealthed(owner, true);
            //RequireVar(this.targetPos);
            targetPos = this.targetPos;
            FaceDirection(owner, targetPos);
            aPMod = GetFlatMagicDamageMod(attacker);
            IncPermanentFlatMagicDamageMod(owner, aPMod);
            this.level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GalioRighteousGustMissile));
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, this.level, true, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetStealthed(owner, true);
        }
        public override void OnSpellHit()
        {
            int level;
            TeamId teamID;
            Champion other1;
            Particle hitVFX; // UNUSED
            level = this.level;
            teamID = GetTeamID(owner);
            other1 = GetChampionBySkinName("Galio", teamID ?? TeamId.TEAM_UNKNOWN);
            BreakSpellShields(target);
            ApplyDamage(other1, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, other1);
            SpellEffectCreate(out hitVFX, out _, "galio_windTunnel_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
        }
    }
}