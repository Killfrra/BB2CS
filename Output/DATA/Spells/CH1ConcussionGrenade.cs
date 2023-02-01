#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CH1ConcussionGrenade : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Minion other2;
            level = GetCastSpellLevelPlusOne();
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            other2 = SpawnMinion("k", "TestCubeRender", "idle.lua", targetPos, teamID, true, true, false, false, true, true, 0, default, true, (Champion)attacker);
            SetNoRender(other2, true);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) > 0)
            {
                SpellCast((ObjAIBase)owner, other2, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
            }
            else
            {
                SpellCast((ObjAIBase)owner, other2, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
            }
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}