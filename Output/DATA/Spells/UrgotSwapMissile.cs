#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotSwapMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "SwapArrow_green.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            Vector3 ownerPos;
            Vector3 targetPos;
            TeamId teamID;
            Minion other2;
            Vector3 targetOffsetPos;
            Minion other3;
            Vector3 ownerOffset;
            if(ExecutePeriodically(0.1f, ref this.lastTimeExecuted, true))
            {
                ownerPos = GetUnitPosition(owner);
                targetPos = GetUnitPosition(attacker);
                teamID = GetTeamID(attacker);
                other2 = SpawnMinion("enemy", "TestCubeRender", "idle.lua", targetPos, teamID, true, true, false, true, true, true, 0, default, true);
                AddBuff((ObjAIBase)owner, other2, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                FaceDirection(other2, ownerPos);
                targetOffsetPos = GetPointByUnitFacingOffset(other2, 80, 90);
                other3 = SpawnMinion("ownerMinion", "TestCubeRender", "idle.lua", ownerPos, teamID, true, true, false, true, true, true, 0, default, true);
                AddBuff((ObjAIBase)owner, other3, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                FaceDirection(other3, targetPos);
                ownerOffset = GetPointByUnitFacingOffset(other3, 80, 90);
                SetSpell((ObjAIBase)owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.UrgotSwapMissile));
                SpellCast((ObjAIBase)owner, attacker, targetPos, targetPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, ownerOffset);
                SetSpell(attacker, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.UrgotSwapMissile2));
                SpellCast(attacker, owner, ownerPos, ownerPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, targetOffsetPos);
                AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}
namespace Spells
{
    public class UrgotSwapMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            count = GetBuffCountFromAll(target, nameof(Buffs.UrgotSwapMarker));
            if(count != 0)
            {
                DestroyMissile(missileNetworkID);
            }
        }
    }
}