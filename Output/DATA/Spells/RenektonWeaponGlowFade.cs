#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonWeaponGlowFade : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            TeamId ownerVar;
            Particle a; // UNUSED
            ownerVar = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "Renekton_Weapon_CoolOff.troy", default, ownerVar, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "head", default, owner, default, default, true);
        }
    }
}