#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Stealth : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stealth",
            BuffTextureName = "Evelynn_ReadyToBetray.dds",
        };
        Fade iD;
        public override void OnActivate()
        {
            SetStealthed(owner, true);
            this.iD = PushCharacterFade(owner, 0.2f, 3);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            PopCharacterFade(owner, this.iD);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
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