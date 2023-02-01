#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Sleep : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Sleep2_Glb.troy", },
            BuffName = "Sleep",
            BuffTextureName = "Teemo_TranquilizingShot.dds",
        };
        public override void OnActivate()
        {
            SetSleep(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSleep(owner, false);
        }
        public override void OnUpdateActions()
        {
            SetSleep(owner, true);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource != default)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}