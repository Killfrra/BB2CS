#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OlafAxeThrow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        Vector3 facingPos;
        Vector3 targetPos;
        float[] effect0 = {-0.24f, -0.28f, -0.32f, -0.36f, -0.4f};
        int[] effect1 = {0, 0, 0, 0, 0};
        public OlafAxeThrow(Vector3 facingPos = default, Vector3 targetPos = default)
        {
            this.facingPos = facingPos;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.facingPos);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            TeamId teamID;
            Vector3 targetPos;
            Minion other3;
            Vector3 facingPos;
            float cooldownPerc;
            float cooldownMult;
            float durationVar;
            float nextBuffVars_MovementSpeedMod;
            int nextBuffVars_AttackSpeedMod;
            int level;
            teamID = GetTeamID(owner);
            targetPos = this.targetPos;
            other3 = SpawnMinion("HiddenMinion", "OlafAxe", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, default, false, (Champion)owner);
            facingPos = this.facingPos;
            FaceDirection(other3, facingPos);
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownMult = 1 + cooldownPerc;
            durationVar = 10 * cooldownMult;
            durationVar -= 0.5f;
            AddBuff(attacker, other3, new Buffs.OlafAxeExpirationTimer(), 1, 1, durationVar, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MovementSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = this.effect1[level];
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, other3.Position, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.OlafSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.OlafAxeThrowDamage(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class OlafAxeThrow : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {-0.24f, -0.28f, -0.32f, -0.36f, -0.4f};
        int[] effect1 = {0, 0, 0, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_MovementSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = this.effect1[level];
            BreakSpellShields(target);
            AddBuff((ObjAIBase)owner, target, new Buffs.OlafSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.OlafAxeThrowDamage(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}