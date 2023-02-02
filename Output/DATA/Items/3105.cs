#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3105 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                int nextBuffVars_MagicResistBonus;
                int nextBuffVars_ArmorBonus;
                int nextBuffVars_DamageBonus;
                nextBuffVars_MagicResistBonus = 15;
                nextBuffVars_ArmorBonus = 12;
                nextBuffVars_DamageBonus = 8;
                if(owner is Champion)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if(unit == owner)
                        {
                            AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraSelf(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        else
                        {
                            if(!owner.IsDead)
                            {
                                AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraFriend(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
                else
                {
                    if(!owner.IsDead)
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            ObjAIBase caster;
                            caster = GetPetOwner((Pet)owner);
                            if(unit == owner)
                            {
                                if(GetBuffCountFromCaster(owner, caster, nameof(Buffs.AegisoftheLegionAuraFriend)) == 0)
                                {
                                    AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraSelf(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                }
                            }
                            else if(unit != caster)
                            {
                                AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraFriend(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
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
    public class _3105 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle aegis;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.aegis, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_CASTER, default, default, true, owner, default, default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.aegis);
        }
    }
}