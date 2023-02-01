#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Hide : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Hide",
            BuffTextureName = "Twitch_AlterEgo.dds",
        };
        public override void OnActivate()
        {
            SetStealthed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
        }
        public override void OnUpdateActions()
        {
            bool temp;
            temp = IsMoving(owner);
            if(temp)
            {
                SpellBuffRemove(owner, nameof(Buffs.HideInShadows), attacker);
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            SpellBuffRemove(owner, nameof(Buffs.HideInShadows), (ObjAIBase)owner);
            SpellBuffRemoveCurrent(owner);
        }
    }
}