using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class WaterElemental : BaseCreature
    {
        [Constructible]
        public WaterElemental() : base(AIType.AI_Mage)
        {
            Body = 16;
            BaseSoundID = 278;

            SetStr(126, 155);
            SetDex(66, 85);
            SetInt(101, 125);

            SetHits(76, 93);

            SetDamage(7, 9);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 10, 25);
            SetResistance(ResistanceType.Cold, 10, 25);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 60.1, 75.0);
            SetSkill(SkillName.MagicResist, 100.1, 115.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;

            CanSwim = true;

            PackItem(new BlackPearl(3));
        }

        public override string CorpseName => "a water elemental corpse";
        public override string DefaultName => "a water elemental";

        public override bool BleedImmune => true;
        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Potions);
        }
    }
}
