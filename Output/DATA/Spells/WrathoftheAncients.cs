#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class WrathoftheAncients : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.35f,
            SpellDamageRatio = 0.35f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, attacker, new Buffs.WrathoftheAncients(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class WrathoftheAncients : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_jaw", "L_jaw", },
            AutoBuffActivateEffect = new[]{ "MouthFoam.troy", "MouthFoam.troy", },
            BuffName = "WrathoftheAncients",
            BuffTextureName = "Averdrian_AstralBeam.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.4f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.Stealth), false))
                {
                    AddBuff(attacker, unit, new Buffs.WrathDamage(), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}