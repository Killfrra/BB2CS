#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShyvanaFireballDragon : BBSpellScript
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
            SpellEffectCreate(out a, out _, "shyvana_flameBreath_dragon_burn.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            spellBaseDamage = this.effect0[level];
            AddBuff(attacker, target, new Buffs.ShyvanaFireballParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.ShyvanaFireballMissile(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}