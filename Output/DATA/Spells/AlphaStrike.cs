#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlphaStrike : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Alpha Striking",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        bool alphaStrikeLaunched;
        public override void OnActivate()
        {
            this.alphaStrikeLaunched = false;
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.alphaStrikeLaunched)
            {
                SetCanAttack(owner, true);
                SetCanMove(owner, true);
                SetGhosted(owner, false);
                SetNoRender(owner, false);
                SetTargetable(owner, true);
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            if(!owner.IsDead)
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            if(this.alphaStrikeLaunched)
            {
                SetGhosted(owner, true);
                SetNoRender(owner, true);
                SetCanAttack(owner, false);
                SetCanMove(owner, false);
                SetTargetable(owner, false);
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if(spellName == nameof(Spells.AlphaStrike))
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            this.alphaStrikeLaunched = true;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class AlphaStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {100, 150, 200, 250, 300};
        float[] effect1 = {0.2f, 0.3f, 0.4f, 0.5f, 0.6f};
        int[] effect2 = {5, 5, 5, 5, 5};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            canMove = GetCanMove(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int targetNum;
            int nextBuffVars_BaseDamage;
            float nextBuffVars_ChanceToKill;
            targetNum = GetCastSpellTargetsHitPlusOne();
            if(targetNum == 1)
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.AlphaStrikeMarker(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            nextBuffVars_BaseDamage = this.effect0[level];
            nextBuffVars_ChanceToKill = this.effect1[level];
            AddBuff((ObjAIBase)owner, target, new Buffs.AlphaStrikeTarget(nextBuffVars_BaseDamage, nextBuffVars_ChanceToKill), 1, 1, this.effect2[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}