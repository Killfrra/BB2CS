#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SpellImmunity : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Spellimmunity.troy", },
            BuffName = "Spell Immunity",
            BuffTextureName = "FallenAngel_BlackShield.dds",
        };
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                Say(owner, "game_lua_SpellImmunity");
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            SetMagicImmune(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetMagicImmune(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetMagicImmune(owner, true);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
        }
    }
}