#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasBarrelRoll : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasBarrelRoll",
            BuffTextureName = "GragasBarrelRoll.dds",
            SpellToggleSlot = 1,
        };
        Vector3 targetPos;
        public GragasBarrelRoll(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            TeamId teamID;
            int gragasSkinID;
            Vector3 targetPos;
            Minion other1;
            int nextBuffVars_SkinID;
            if(spellName == nameof(Spells.GragasBarrelRollMissile))
            {
                teamID = GetTeamID(owner);
                gragasSkinID = GetSkinID(owner);
                targetPos = this.targetPos;
                other1 = SpawnMinion("DoABarrelRoll", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, default, true, (Champion)owner);
                nextBuffVars_SkinID = gragasSkinID;
                AddBuff(other1, owner, new Buffs.GragasBarrelRollBoom(nextBuffVars_SkinID), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                nextBuffVars_SkinID = gragasSkinID;
                AddBuff(other1, other1, new Buffs.GragasBarrelRollRender(), 1, 1, 20, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class GragasBarrelRoll : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {85, 140, 195, 250, 305};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            int nextBuffVars_DamageLevel;
            Vector3 nextBuffVars_TargetPos;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_DamageLevel = this.effect0[level];
            nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, owner, new Buffs.GragasBarrelRoll(nextBuffVars_TargetPos), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellCast(attacker, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}