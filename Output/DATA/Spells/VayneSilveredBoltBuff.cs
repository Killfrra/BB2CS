#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneSilveredBoltBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "VayneSilverBolts",
            BuffTextureName = "Vayne_SilveredBolts.dds",
            IsDeathRecapSource = true,
            PersistsThroughDeath = true,
            SpellFXOverrideSkins = new[]{ "", },
            SpellToggleSlot = 2,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            bool canMove; // UNUSED
            bool isBlinded; // UNITIALIZED
            canMove = GetCanMove(owner);
            if(!isBlinded)
            {
                returnValue = true;
            }
            else
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        if(hitResult != HitResult.HIT_Dodge)
                        {
                            if(hitResult != HitResult.HIT_Miss)
                            {
                                int count;
                                count = GetBuffCountFromCaster(target, attacker, nameof(Buffs.VayneSilveredDebuff));
                                if(count == 2)
                                {
                                    TeamId teamID;
                                    Particle gragas; // UNUSED
                                    teamID = GetTeamID(attacker);
                                    SpellEffectCreate(out gragas, out _, "vayne_W_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, target.Position, target, default, default, true, false, false, false, false);
                                }
                                AddBuff(attacker, target, new Buffs.VayneSilveredDebuff(), 3, 1, 3.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                                if(count == 2)
                                {
                                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                                    damageAmount = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}