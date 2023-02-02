#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3037 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    float nextBuffVars_ManaRegenBonus;
                    nextBuffVars_ManaRegenBonus = 1.44f;
                    if(owner is Champion)
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if(unit == owner)
                            {
                                AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraSelf(nextBuffVars_ManaRegenBonus), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                            else
                            {
                                AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraFriend(nextBuffVars_ManaRegenBonus), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                        }
                    }
                    else
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            ObjAIBase caster;
                            caster = GetPetOwner((Pet)owner);
                            if(unit == owner)
                            {
                                if(GetBuffCountFromCaster(owner, caster, nameof(Buffs.ManaManipulatorAuraFriend)) == 0)
                                {
                                    AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraSelf(nextBuffVars_ManaRegenBonus), 1, 1, 4.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                                }
                            }
                            else if(unit != caster)
                            {
                                AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraFriend(nextBuffVars_ManaRegenBonus), 1, 1, 4.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3037 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle manaManipulator;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.manaManipulator, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_CASTER, default, default, true, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.manaManipulator);
        }
    }
}