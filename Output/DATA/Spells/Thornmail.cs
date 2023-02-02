#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Thornmail : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Thornmail",
            BuffTextureName = "3075_Thornmail.dds",
        };
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(attacker is not BaseTurret)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    if(hitResult != HitResult.HIT_Miss)
                    {
                        Particle noEstada; // UNUSED
                        float percentDamageTaken;
                        SpellEffectCreate(out noEstada, out _, "Thornmail_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
                        percentDamageTaken = damageAmount * 0.3f;
                        ApplyDamage((ObjAIBase)owner, attacker, percentDamageTaken, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_REACTIVE, 1, 0, 1, false, false);
                    }
                }
            }
        }
    }
}