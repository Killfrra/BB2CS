#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XerathMageChainsExtended : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {70, 120, 170, 220, 270};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle a; // UNUSED
            Particle b; // UNUSED
            bool debuff;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "Xerath_Bolt_hit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out b, out _, "Xerath_Bolt_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            debuff = true;
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkinDragon)) > 0)
            {
                debuff = false;
            }
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.ResistantSkin)) > 0)
            {
                debuff = false;
            }
            if(debuff)
            {
                AddBuff(attacker, target, new Buffs.XerathMageChains(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 0, false, false, attacker);
        }
    }
}