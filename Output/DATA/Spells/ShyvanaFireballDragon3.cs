#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShyvanaFireballDragon3 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {70, 110, 150, 190, 230};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle a; // UNUSED
            float spellBaseDamage;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "Incinerate_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            spellBaseDamage = this.effect0[level];
            AddBuff(attacker, target, new Buffs.ShyvanaFireballParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.ShyvanaFireballMissile(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 pointToSpawn;
            Vector3 pointToFace;
            Minion other1;
            teamID = GetTeamID(owner);
            pointToSpawn = GetPointByUnitFacingOffset(owner, 25, 0);
            pointToFace = GetPointByUnitFacingOffset(owner, -100, 0);
            other1 = SpawnMinion("ConeBreathMarker", "TestCubeRender10Vision", "idle.lua", pointToSpawn, teamID ?? TeamId.TEAM_NEUTRAL, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            FaceDirection(other1, pointToFace);
            AddBuff(attacker, other1, new Buffs.ShyvanaFireballDragonMinion(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}