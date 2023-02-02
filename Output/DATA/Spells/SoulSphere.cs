#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SoulSphere : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Soul Sphere",
            BuffTextureName = "3049_Prismatic_Sphere.dds",
        };
        Particle particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "Aura_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, target, default, default, false);
            IncPermanentFlatHPPoolMod(owner, 200);
            IncPermanentFlatMagicDamageMod(owner, 20);
        }
        public override void OnDeactivate(bool expired)
        {
            float hP;
            SpellEffectRemove(this.particle);
            IncPermanentFlatHPPoolMod(owner, -200);
            IncPermanentFlatMagicDamageMod(owner, -20);
            hP = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(hP <= 200)
            {
                float hPToAdd;
                hPToAdd = 1 - hP;
                IncHealth(owner, hPToAdd, owner);
            }
            else
            {
                IncHealth(owner, -200, owner);
            }
        }
    }
}