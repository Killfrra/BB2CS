#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCOTGSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserCOTGSelf",
            BuffTextureName = "Mordekaiser_COTG.dds",
            NonDispellable = true,
        };
        float petDamage;
        float petAP;
        public MordekaiserCOTGSelf(float petDamage = default, float petAP = default)
        {
            this.petDamage = petDamage;
            this.petAP = petAP;
        }
        public override void OnActivate()
        {
            //RequireVar(this.petDamage);
            //RequireVar(this.petAP);
            IncPermanentFlatPhysicalDamageMod(attacker, this.petDamage);
            IncPermanentFlatMagicDamageMod(attacker, this.petAP);
            SetBuffToolTipVar(1, this.petDamage);
            SetBuffToolTipVar(2, this.petAP);
        }
        public override void OnDeactivate(bool expired)
        {
            this.petDamage *= -1;
            this.petAP *= -1;
            IncPermanentFlatMagicDamageMod(owner, this.petAP);
            IncPermanentFlatPhysicalDamageMod(owner, this.petDamage);
        }
    }
}