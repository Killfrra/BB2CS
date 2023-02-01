#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeathCraze : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DeathCraze",
            BuffTextureName = "DrMundo_Nethershade.dds",
        };
        float baseDamage;
        float damageAdded;
        public override void OnActivate()
        {
            //RequireVar(charVars.MundoPercent);
            //RequireVar(this.baseDamage);
            //RequireVar(this.damageAdded);
        }
        public override void OnUpdateStats()
        {
            this.damageAdded = this.baseDamage * charVars.MundoPercent;
            IncFlatPhysicalDamageMod(owner, this.damageAdded);
        }
        public override void OnUpdateActions()
        {
            this.baseDamage = GetBaseAttackDamage(owner);
        }
    }
}