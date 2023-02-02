#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Starcall : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.35f,
            SpellDamageRatio = 0.35f,
        };
        int[] effect0 = {60, 85, 110, 135, 160};
        int[] effect1 = {-8, -9, -10, -11, -12};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool temp;
            temp = false;
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 610, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
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
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_DamageToDeal;
            int nextBuffVars_StarcallShred;
            nextBuffVars_DamageToDeal = this.effect0[level];
            nextBuffVars_StarcallShred = this.effect1[level];
            AddBuff(attacker, target, new Buffs.StarcallDamage(nextBuffVars_DamageToDeal, nextBuffVars_StarcallShred), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Starcall : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Starcall",
            BuffTextureName = "Soraka_Starcall.dds",
        };
        float resistanceMod;
        public Starcall(float resistanceMod = default)
        {
            this.resistanceMod = resistanceMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.resistanceMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.resistanceMod);
        }
    }
}