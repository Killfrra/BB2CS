#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CamouflageStealth : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CamouflageStealth",
            BuffTextureName = "Teemo_Camouflage.dds",
        };
        Fade iD;
        Vector3 lastPosition;
        public override void OnActivate()
        {
            int teemoSkinID;
            SetStealthed(owner, true);
            teemoSkinID = GetSkinID(owner);
            if(teemoSkinID == 4)
            {
                this.iD = PushCharacterFade(owner, 0.3f, 0.2f);
            }
            else
            {
                this.iD = PushCharacterFade(owner, 0.3f, 0.2f);
            }
            this.lastPosition = GetUnitPosition(owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            PopCharacterFade(owner, this.iD);
            AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, target, new Buffs.CamouflageBuff(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
        public override void OnUpdateStats()
        {
            Vector3 curPosition;
            Vector3 lastPosition;
            float distance;
            SetStealthed(owner, true);
            curPosition = GetUnitPosition(owner);
            lastPosition = this.lastPosition;
            distance = DistanceBetweenPoints(curPosition, lastPosition);
            if(distance != 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.CastingBreaksStealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if(!spellVars.CastingBreaksStealth)
            {
            }
            else
            {
                if(!spellVars.DoesntTriggerSpellCasts)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}