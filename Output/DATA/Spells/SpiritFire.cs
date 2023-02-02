#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SpiritFire : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {55, 95, 135, 175, 215};
        int[] effect1 = {55, 95, 135, 175, 215};
        int[] effect2 = {-20, -25, -30, -35, -40};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            int nextBuffVars_InitialDamage;
            int nextBuffVars_Damage;
            Vector3 nextBuffVars_TargetPos;
            int nextBuffVars_ArmorReduction;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_InitialDamage = this.effect0[level];
            nextBuffVars_Damage = this.effect1[level];
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_ArmorReduction = this.effect2[level];
            AddBuff(attacker, attacker, new Buffs.SpiritFire(nextBuffVars_TargetPos, nextBuffVars_InitialDamage, nextBuffVars_Damage, nextBuffVars_ArmorReduction), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SpiritFire : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Vector3 targetPos;
        int initialDamage;
        int damage;
        int armorReduction;
        Region bubbleID; // UNUSED
        public SpiritFire(Vector3 targetPos = default, int initialDamage = default, int damage = default, int armorReduction = default)
        {
            this.targetPos = targetPos;
            this.initialDamage = initialDamage;
            this.damage = damage;
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 targetPos;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.initialDamage);
            //RequireVar(this.damage);
            //RequireVar(this.armorReduction);
            targetPos = this.targetPos;
            SpellEffectCreate(out a, out _, "nassus_spiritFire_warning.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            this.bubbleID = AddPosPerceptionBubble(teamID, 200, targetPos, 2.6f, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float nextBuffVars_InitialDamage;
            float nextBuffVars_Damage;
            float nextBuffVars_ArmorReduction;
            Vector3 nextBuffVars_TargetPos;
            nextBuffVars_InitialDamage = this.initialDamage;
            nextBuffVars_Damage = this.damage;
            nextBuffVars_ArmorReduction = this.armorReduction;
            nextBuffVars_TargetPos = this.targetPos;
            AddBuff(attacker, owner, new Buffs.SpiritFireAoE(nextBuffVars_InitialDamage, nextBuffVars_Damage, nextBuffVars_ArmorReduction, nextBuffVars_TargetPos), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}