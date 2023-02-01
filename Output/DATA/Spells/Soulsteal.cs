#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Soulsteal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, charVars.MagicDamageMod);
        }
        public override void OnKill()
        {
            Particle ar; // UNUSED
            float tempMana;
            if(target is Champion)
            {
                charVars.MagicDamageMod += 10;
                charVars.MagicDamageMod = Math.Min(charVars.MagicDamageMod, 70);
                SpellEffectCreate(out ar, out _, "MejaisSoulstealer_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                tempMana = GetPAR(target, PrimaryAbilityResourceType.MANA);
                IncPAR(owner, tempMana);
            }
        }
        public override void OnAssist()
        {
            Particle ar; // UNUSED
            if(target is Champion)
            {
                charVars.MagicDamageMod += 5;
                charVars.MagicDamageMod = Math.Min(charVars.MagicDamageMod, 70);
                SpellEffectCreate(out ar, out _, "MejaisSoulstealer_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}