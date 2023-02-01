#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrTigerPunch : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_Finger", "R_Finger", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "UdyrTigerPunch",
            BuffTextureName = "Udyr_TigerStance.dds",
        };
        float activeAttackSpeed;
        public UdyrTigerPunch(float activeAttackSpeed = default)
        {
            this.activeAttackSpeed = activeAttackSpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.activeAttackSpeed);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.activeAttackSpeed);
        }
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerStance)) == 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerShred), (ObjAIBase)owner);
            }
        }
    }
}