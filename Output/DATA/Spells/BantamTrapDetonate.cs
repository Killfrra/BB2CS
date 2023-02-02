#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BantamTrapDetonate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teemoTeam;
            Particle particle; // UNUSED
            teemoTeam = GetTeamID(attacker);
            SpellEffectCreate(out particle, out _, "ShroomMine.troy", default, teemoTeam ?? TeamId.TEAM_UNKNOWN, 300, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float nextBuffVars_AttackSpeedMod;
                float nextBuffVars_MoveSpeedMod;
                BreakSpellShields(unit);
                nextBuffVars_AttackSpeedMod = 0;
                nextBuffVars_MoveSpeedMod = -0.6f;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
            }
            ApplyDamage((ObjAIBase)owner, owner, 500, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}