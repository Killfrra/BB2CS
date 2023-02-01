#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LayWaste : BBBuffScript
    {
        float damageAmount;
        TeamId teamOfOwner;
        Particle particle;
        public LayWaste(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAmount);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
            this.teamOfOwner = GetTeamID(attacker);
            SpellEffectCreate(out this.particle, out _, "LayWaste_point.troy", default, this.teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            Particle particle; // UNUSED
            float numUnits;
            float damageAmount;
            SpellEffectRemove(this.particle);
            SpellEffectCreate(out particle, out _, "LayWaste_tar.troy", default, this.teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            numUnits = 0;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                numUnits++;
            }
            if(numUnits == 1)
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                }
            }
            else if(numUnits >= 2)
            {
                damageAmount = this.damageAmount / 2;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
                }
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}
namespace Spells
{
    public class LayWaste : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 120, 160, 200, 240};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Region bubbleID; // UNUSED
            Minion other3;
            float nextBuffVars_DamageAmount;
            teamOfOwner = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            bubbleID = AddPosPerceptionBubble(teamOfOwner, 200, targetPos, 1, default, false);
            other3 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            nextBuffVars_DamageAmount = this.effect0[level];
            AddBuff(attacker, other3, new Buffs.LayWaste(nextBuffVars_DamageAmount), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}