#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MoveQuick : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.MoveQuick(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MoveQuick : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Move Quick2",
            BuffTextureName = "Teemo_MoveQuick.dds",
            SpellFXOverrideSkins = new[]{ "SuperTeemo", },
            SpellToggleSlot = 2,
        };
        bool customRun;
        Particle moveQuickParticle;
        float[] effect0 = {0.2f, 0.28f, 0.36f, 0.44f, 0.52f};
        public override void OnActivate()
        {
            int teemoSkinID;
            object _6; // UNITIALIZED
            SpellBuffClear(owner, nameof(Buffs.TeemoMoveQuickSpeed));
            teemoSkinID = GetSkinID(owner);
            this.customRun = false;
            if(teemoSkinID == 4)
            {
                SpellEffectCreate(out this.moveQuickParticle, out _, "MoveQuick_buf2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            }
            else if(_6 == default)
            {
                SpellEffectCreate(out this.moveQuickParticle, out _, "MoveQuick_buf2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.moveQuickParticle, out _, "MoveQuick_buf2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            if(teemoSkinID == 6)
            {
                this.customRun = true;
                OverrideAnimation("Run", "RunFly", owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.moveQuickParticle);
            SpellBuffClear(owner, nameof(Buffs.TeemoMoveQuickDebuff));
            AddBuff(attacker, target, new Buffs.TeemoMoveQuickSpeed(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
            if(this.customRun)
            {
                OverrideAnimation("Run", "Run", owner);
            }
        }
        public override void OnUpdateStats()
        {
            int level;
            float moveSpeedBonus;
            int teemoSkinID; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            moveSpeedBonus = this.effect0[level];
            IncPercentMovementSpeedMod(owner, moveSpeedBonus);
            teemoSkinID = GetSkinID(owner);
        }
    }
}