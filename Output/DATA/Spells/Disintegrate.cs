#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Disintegrate : BBBuffScript
    {
        float manaCost;
        public Disintegrate(float manaCost = default)
        {
            this.manaCost = manaCost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaCost);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeath()
        {
            if(attacker.IsDead)
            {
            }
            else
            {
                IncPAR(attacker, this.manaCost, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}
namespace Spells
{
    public class Disintegrate : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {85, 125, 165, 205, 245};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            float tempManaCost;
            float nextBuffVars_ManaCost;
            TeamId teamID;
            int annieSkinID;
            Particle a; // UNUSED
            Particle b; // UNUSED
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania_particle));
            if(count >= 1)
            {
                ApplyStun(attacker, target, charVars.StunDuration);
                SpellBuffRemove(owner, nameof(Buffs.Pyromania_particle), (ObjAIBase)owner, 0);
            }
            tempManaCost = GetPARCost();
            nextBuffVars_ManaCost = tempManaCost;
            AddBuff(attacker, target, new Buffs.Disintegrate(nextBuffVars_ManaCost), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 0, false, false, attacker);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pyromania(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            teamID = GetTeamID(owner);
            annieSkinID = GetSkinID(owner);
            if(annieSkinID == 5)
            {
                SpellEffectCreate(out a, out _, "DisintegrateHit_tar_frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out b, out _, "Disintegrate_hit_frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "DisintegrateHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out b, out _, "Disintegrate_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}