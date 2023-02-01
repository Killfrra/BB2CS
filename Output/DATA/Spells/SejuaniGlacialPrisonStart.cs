#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniGlacialPrisonStart : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "Sejuani_GlacialPrison.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        float prisonDamage;
        float secondaryDuration;
        public SejuaniGlacialPrisonStart(float prisonDamage = default, float secondaryDuration = default)
        {
            this.prisonDamage = prisonDamage;
            this.secondaryDuration = secondaryDuration;
        }
        public override void OnActivate()
        {
            //RequireVar(this.prisonDamage);
            //RequireVar(this.secondaryDuration);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            ObjAIBase caster;
            TeamId teamID;
            Particle particle2; // UNUSED
            Particle particle3; // UNUSED
            if(spellName == nameof(Spells.SejuaniGlacialPrison))
            {
                caster = SetBuffCasterUnit();
                teamID = GetTeamID(caster);
                SpellEffectCreate(out particle2, out _, "sejuani_ult_impact_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, default, default, default, true, false, false, false, false);
                SpellEffectCreate(out particle3, out _, "sejuani_ult_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, default, default, default, true, false, false, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(caster, missileEndPosition, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(GetBuffCountFromCaster(unit, caster, nameof(Buffs.SejuaniGlacialPrisonCheck)) == 0)
                    {
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, this.prisonDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                        AddBuff(caster, unit, new Buffs.SejuaniGlacialPrisonDetonate(), 1, 1, this.secondaryDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class SejuaniGlacialPrisonStart : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {150, 250, 350};
        public override void SelfExecute()
        {
            int prisonDamage;
            float nextBuffVars_PrisonDamage;
            float nextBuffVars_SecondaryDuration;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            TeamId teamID;
            Region perBubb; // UNUSED
            prisonDamage = this.effect0[level];
            nextBuffVars_PrisonDamage = prisonDamage;
            nextBuffVars_SecondaryDuration = 1;
            AddBuff((ObjAIBase)owner, owner, new Buffs.SejuaniGlacialPrisonStart(nextBuffVars_PrisonDamage, nextBuffVars_SecondaryDuration), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1000, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                teamID = GetTeamID(unit);
                perBubb = AddUnitPerceptionBubble(teamID, 30, owner, 1, default, owner, false);
            }
        }
    }
}