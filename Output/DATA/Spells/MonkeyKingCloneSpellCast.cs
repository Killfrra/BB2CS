#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingCloneSpellCast : BBBuffScript
    {
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            AddBuff((ObjAIBase)owner, attacker, new Buffs.MonkeyKingCloneSweep(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}