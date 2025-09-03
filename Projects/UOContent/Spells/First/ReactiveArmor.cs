using System;
using System.Collections.Generic;
using ModernUO.CodeGeneratedEvents;
using Server.Engines.BuffIcons;
using Server.Mobiles;

namespace Server.Spells.First
{
    public class ReactiveArmorSpell : MagerySpell
    {
        private static readonly SpellInfo _info = new(
            "Reactive Armor",
            "Flam Sanct",
            236,
            9011,
            Reagent.Garlic,
            Reagent.SpidersSilk,
            Reagent.SulfurousAsh
        );

        private static readonly Dictionary<Mobile, ResistanceMod[]> _table = new();

        public ReactiveArmorSpell(Mobile caster, Item scroll = null) : base(caster, scroll, _info)
        {
        }

        public override SpellCircle Circle => SpellCircle.First;

        public override bool CheckCast()
        {
            if (Core.AOS)
            {
                return true;
            }

            if (Caster.MeleeDamageAbsorb > 0)
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return false;
            }

            if (!Caster.CanBeginAction<DefensiveSpell>())
            {
                Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (Core.AOS)
            {
                /* The reactive armor spell increases the caster's physical resistance, while lowering the caster's elemental resistances.
                 * 15 + (Inscription/20) Physcial bonus
                 * -5 Elemental
                 * The reactive armor spell has an indefinite duration, becoming active when cast, and deactivated when re-cast.
                 * Reactive Armor, Protection, and Magic Reflection will stay on even after logging out,
                 * even after dying, until you turn them off by casting them again.
                 * (+20 physical -5 elemental at 100 Inscription)
                 */

                if (CheckSequence())
                {
                    if (_table.Remove(Caster, out var mods))
                    {
                        Caster.PlaySound(0x1ED);
                        Caster.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);

                        for (var i = 0; i < mods.Length; ++i)
                        {
                            Caster.RemoveResistanceMod(mods[i]);
                        }

                        (Caster as PlayerMobile)?.RemoveBuff(BuffIcon.ReactiveArmor);
                    }
                    else
                    {
                        Caster.PlaySound(0x1E9);
                        Caster.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);

                        mods =
                        [
                            new ResistanceMod(
                                ResistanceType.Physical,
                                "PhysicalResistReactiveArmorSpell",
                                15 + (int)(Caster.Skills.Inscribe.Value / 20)
                            ),
                            new ResistanceMod(ResistanceType.Fire, "FireResistReactiveArmorSpell", -5),
                            new ResistanceMod(ResistanceType.Cold, "ColdResistReactiveArmorSpell", -5),
                            new ResistanceMod(ResistanceType.Poison, "PoisonResistReactiveArmorSpell", -5),
                            new ResistanceMod(ResistanceType.Energy, "EnergyResistReactiveArmorSpell", -5)
                        ];

                        _table[Caster] = mods;

                        for (var i = 0; i < mods.Length; ++i)
                        {
                            Caster.AddResistanceMod(mods[i]);
                        }

                        var physresist = 15 + (int)(Caster.Skills.Inscribe.Value / 20);
                        var args = $"{physresist}\t{5}\t{5}\t{5}\t{5}";

                        (Caster as PlayerMobile)?.AddBuff(new BuffInfo(BuffIcon.ReactiveArmor, 1075812, 1075813, args: args));
                    }
                }

                FinishSequence();
            }
            else
            {
                if (Caster.MeleeDamageAbsorb > 0)
                {
                    Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                }
                else if (!Caster.CanBeginAction<DefensiveSpell>())
                {
                    Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                }
                else if (CheckSequence())
                {
                    if (Caster.BeginAction<DefensiveSpell>())
                    {
                        var value = Math.Clamp(
                            (int)(Caster.Skills.Magery.Value + Caster.Skills.Meditation.Value +
                                  Caster.Skills.Inscribe.Value) / 3,
                            1,
                            75
                        );

                        Caster.MeleeDamageAbsorb = value;

                        Caster.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);
                        Caster.PlaySound(0x1F2);
                    }
                    else
                    {
                        Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                    }
                }

                FinishSequence();
            }
        }

        [OnEvent(nameof(PlayerMobile.PlayerDeletedEvent))]
        public static void EndArmor(Mobile m)
        {
            if (!_table.Remove(m, out var mods))
            {
                return;
            }

            for (var i = 0; i < mods?.Length; ++i)
            {
                m.RemoveResistanceMod(mods[i]);
            }

            (m as PlayerMobile)?.RemoveBuff(BuffIcon.ReactiveArmor);
        }
    }
}
