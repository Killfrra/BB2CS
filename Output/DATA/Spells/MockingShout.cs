#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MockingShout : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Mocking Shout",
            BuffTextureName = "48thSlave_Pacify.dds",
        };
        float damageMod;
        public MockingShout(float damageMod = default)
        {
            this.damageMod = damageMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.damageMod);
            ApplyAssistMarker(attacker, target, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageMod);
        }
    }
}
namespace Spells
{
    public class MockingShout : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {-0.3f, -0.375f, -0.45f, -0.525f, -0.6f};
        int[] effect1 = {-20, -35, -50, -65, -80};
        float[] effect2 = {-0.3f, -0.375f, -0.45f, -0.525f, -0.6f};
        int[] effect3 = {-20, -35, -50, -65, -80};
        int[] effect4 = {-20, -35, -50, -65, -80};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool temp;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                temp = false;
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    temp = true;
                }
                if(temp)
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_DamageMod;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FacingMe)) > 0)
            {
                nextBuffVars_MoveSpeedMod = this.effect0[level];
                nextBuffVars_DamageMod = this.effect1[level];
                AddBuff(attacker, target, new Buffs.MockingShoutSlow(nextBuffVars_MoveSpeedMod), 1, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.MockingShout(nextBuffVars_DamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            else
            {
                if(IsBehind(target, owner))
                {
                    nextBuffVars_MoveSpeedMod = this.effect2[level];
                    nextBuffVars_DamageMod = this.effect3[level];
                    AddBuff(attacker, target, new Buffs.MockingShoutSlow(nextBuffVars_MoveSpeedMod), 1, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.MockingShout(nextBuffVars_DamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    nextBuffVars_DamageMod = this.effect4[level];
                    AddBuff(attacker, target, new Buffs.MockingShout(nextBuffVars_DamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}