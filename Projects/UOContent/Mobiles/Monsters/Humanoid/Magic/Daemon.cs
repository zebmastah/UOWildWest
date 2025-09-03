using ModernUO.Serialization;
using Server.Ethics;
using Server.Factions;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class Daemon : BaseCreature
    {
        [Constructible]
        public Daemon() : base(AIType.AI_Mage)
        {
            Name = NameList.RandomName("daemon");
            Body = 9;
            BaseSoundID = 357;

            SetStr(476, 505);
            SetDex(76, 95);
            SetInt(301, 325);

            SetHits(286, 303);

            SetDamage(7, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 60);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 70.1, 80.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.MagicResist, 85.1, 95.0);
            SetSkill(SkillName.Tactics, 70.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 58;
        }

        public override string CorpseName => "a daemon corpse";

        public override Faction FactionAllegiance => Shadowlords.Instance;
        public override Ethic EthicAllegiance => Ethic.Evil;

        public override bool CanRummageCorpses => true;

        public override Poison PoisonImmune => Poison.Regular;

        public override int TreasureMapLevel => 4;

        public override int Meat => 1;
        
        public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
        }
    }
}
