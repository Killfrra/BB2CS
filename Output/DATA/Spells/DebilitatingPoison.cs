#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DebilitatingPoison : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", "twitch_debilitatingPoison_tar.troy", },
            BuffName = "DebilitatingPoison",
            BuffTextureName = "Twitch_Fade.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        public DebilitatingPoison(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            int count;
            float bonusMove;
            float totalMoveSpeedMod;
            count = GetBuffCountFromAll(owner, nameof(Buffs.DeadlyVenom));
            bonusMove = count * -0.06f;
            totalMoveSpeedMod = bonusMove + this.moveSpeedMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalMoveSpeedMod);
        }
    }
}
namespace Spells
{
    public class DebilitatingPoison : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {2, 2.6f, 3.2f, 3.8f, 4.4f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            nextBuffVars_MoveSpeedMod = -0.3f;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.DebilitatingPoison(nextBuffVars_MoveSpeedMod), 1, 1, this.effect0[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
            }
        }
    }
}