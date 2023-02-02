#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_AegisShield2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "C_BUFFBONE_GLB_CENTER_LOC", },
            AutoBuffActivateEffect = new[]{ "pantheon_aoz_passive.troy", },
            BuffName = "PantheonAegisShield",
            BuffTextureName = "Pantheon_AOZ.dds",
        };
        bool executeOnce;
        public override void OnActivate()
        {
            //RequireVar(this.executeOnce);
            if(!this.executeOnce)
            {
                bool isMoving;
                isMoving = IsMoving(owner);
                if(isMoving)
                {
                    this.executeOnce = true;
                    OverrideAnimation("Run", "Run2", owner);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.executeOnce)
            {
                ClearOverrideAnimation("Run", owner);
            }
        }
        public override void OnUpdateActions()
        {
            if(!this.executeOnce)
            {
                bool isMoving;
                isMoving = IsMoving(owner);
                if(isMoving)
                {
                    this.executeOnce = true;
                    OverrideAnimation("Run", "Run2", owner);
                }
            }
        }
    }
}