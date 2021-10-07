using Biodiv.Entities;
using BiodivApi.Entities;
using BiodivApi.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Extensions
{
    public static class ModelBuilderSeedExtension
    {
        private static readonly Specie[] DefaultSpecies =
        {
            new()
            {
                Id = 1,
                Name = "Buffle",
                EnglishName = "Buffalo",
                ScientificName = "Syncerus caffer",
                TaxonomicGroup = "Bovin; sous-famille des Bubalina",
                Status = ConservationStatus.Nt,
                Habitat = "Forets et savanes",
                Description =
                    "Animal de couleur rougeâtre a noir en passant par le brun, il pèse pour un adulte de 265 a 320 kg pour le buffle de foret et de 500 a 700 kg pour le buffle de savane. Il est grégaire et peut former des groupes de plusieurs centaines de têtes. Il consomme plutôt des herbes et aime les endroits pas trop éloignés des points d’eau.",
                Threats =
                    "son habitat est de plus en plus dégradé, occupe par les champs et les habitations humaines. Par ailleurs, la viande étant très appréciée, l’espèce est menacée par le braconnage."
            },
            new()
            {
                Id = 2,
                Name = "Lion",
                EnglishName = "Lion",
                ScientificName = "Panthera Leo",
                TaxonomicGroup = "Ordre des carnivores, famille des félidés",
                Status = ConservationStatus.Vu,
                Habitat = "Forêts et savanes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 3,
                Name = "Léopard",
                EnglishName = "Leopard",
                ScientificName = "Panthera pardus",
                TaxonomicGroup = "Ordre des carnivores, famille des félidés",
                Status = ConservationStatus.Vu,
                Habitat = "Forêts et savanes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 4,
                Name = "Guépard",
                EnglishName = "Cheetah",
                ScientificName = "Acinonyx jubatus",
                TaxonomicGroup = "Ordre des carnivores, famille des félidés",
                Status = ConservationStatus.Vu,
                Habitat = "Savanes et déserts",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 5,
                Name = "Serval",
                EnglishName = "Serval",
                ScientificName = "Leptailurus serval",
                TaxonomicGroup = "Ordre des carnivores, famille des félidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes humides",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 6,
                Name = "Caracal",
                EnglishName = "Caracal",
                ScientificName = "Caracal caracal",
                TaxonomicGroup = "Ordre des carnivores, famille des félidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes humides",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 7,
                Name = "Hyène",
                EnglishName = "Hyena",
                ScientificName = "Crocuta crocuta",
                TaxonomicGroup = "Ordre des carnivores, famille des hyenidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 8,
                Name = "Chacal à flancs rayés",
                EnglishName = "Striped jackal",
                ScientificName = "Canis adustus",
                TaxonomicGroup = "Ordre des carnivores, famille des canidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 9,
                Name = "Chacal doré",
                EnglishName = "Golden jackal",
                ScientificName = "Canis aureus",
                TaxonomicGroup = "Ordre des carnivores, famille des canidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 10,
                Name = "Lycaon",
                EnglishName = "Wild dog",
                ScientificName = "Lycaon pictus",
                TaxonomicGroup = "Ordre des carnivores, famille des canidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 11,
                Name = "Mangouste icheumon",
                EnglishName = "Ichemon mongoose",
                ScientificName = "Herpestes ichneumon",
                TaxonomicGroup = "Ordre des carnivores, famille des herpestidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 12,
                Name = "Mangouste à queue blanche",
                EnglishName = "White tailed mongoose",
                ScientificName = "Ichneumia albicauda",
                TaxonomicGroup = "Ordre des carnivores, famille des herpestidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 13,
                Name = "Mangouste des marais",
                EnglishName = "Marsh mongoose",
                ScientificName = "Atilax paludinosus",
                TaxonomicGroup = "Ordre des carnivores, famille des herpestidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 14,
                Name = "Mangouste rouge",
                EnglishName = "Red mongoose",
                ScientificName = "Herpestes sanguineus ",
                TaxonomicGroup = "Ordre des carnivores, famille des herpestidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 15,
                Name = "Civette",
                EnglishName = "Civet",
                ScientificName = "Civettictis civetta",
                TaxonomicGroup = "Ordre des carnivores, famille des viverridés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 16,
                Name = "Nandinie",
                EnglishName = "Nandinie",
                ScientificName = "Nandinia binotata",
                TaxonomicGroup = "Ordre des carnivores, famille des viverridés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 17,
                Name = "Genette commune",
                EnglishName = "Genet",
                ScientificName = "Genetta genetta",
                TaxonomicGroup = "Ordre des carnivores, famille des viverridés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 18,
                Name = "Zorille commun",
                EnglishName = "Zorilla",
                ScientificName = "Ictonyx striatus",
                TaxonomicGroup = "Ordre des carnivores, famille des mustélidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 19,
                Name = "Loutre à cou tacheté",
                EnglishName = "Spotted otter",
                ScientificName = "Lutra maculicolis",
                TaxonomicGroup = "Ordre des carnivores, famille des mustélidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 20,
                Name = "Loutre à joues blanches",
                EnglishName = "White-cheeked otter",
                ScientificName = "Aonyx capensis",
                TaxonomicGroup = "Ordre des carnivores, famille des mustélidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 21,
                Name = "Ratel",
                EnglishName = "Ratel",
                ScientificName = "Mellivora capensis",
                TaxonomicGroup = "Ordre des carnivores, famille des mustélidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 22,
                Name = "Girafe",
                EnglishName = "Girafe",
                ScientificName = "Giraffa camelopardalis",
                TaxonomicGroup = "Ordre des artiodactyles, famille des giraffidés",
                Status = ConservationStatus.En,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 23,
                Name = "Black duiker",
                EnglishName = "Céphalophe noir",
                ScientificName = "Cephalophus niger",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 24,
                Name = "Guib harnaché",
                EnglishName = "harnessed guib",
                ScientificName = "Tragelaphus scriptus",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 25,
                Name = "Sitatunga",
                EnglishName = "Sitatunga",
                ScientificName = "Tragelaphus spekei",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 26,
                Name = "Céphalophe de grimm",
                EnglishName = "Grimm's duiker",
                ScientificName = "Sylvicapra grimmia",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 27,
                Name = "Céphalophe à flancs roux",
                EnglishName = "Red sided duiker",
                ScientificName = "Cephalophus rufilatus",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Lc,
                Habitat = "Savanes et steppes",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 28,
                Name = "Céphalophe de Maxwell",
                EnglishName = "Maxwell's duiker",
                ScientificName = "Philantomba maxwelli",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Lc,
                Habitat = "Forêt humide",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 29,
                Name = "Gazelle à front roux",
                EnglishName = "Red-fronted gazelle",
                ScientificName = "Eudorcas rufifrons",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Vu,
                Habitat = "Forêt humide",
                Description = "",
                Threats = ""
            },
            new()
            {
                Id = 30,
                Name = "Ourébi",
                EnglishName = "Ourébi",
                ScientificName = "Ourebia ourebi",
                TaxonomicGroup = "Ordre des artiodactyles, famille des bovidés",
                Status = ConservationStatus.Lc,
                Habitat = "Brousse, savane arborée, non loin de l'eau. Il peut vivre en plaine comme en montagne, jusqu'à 3 000 mètres d'altitude.",
                Description = "",
                Threats = ""
            }
        };

        private static readonly LocalName[] DefaultLocalNames =
        {
            new()
            {
                Id = 1,
                Language = "Fon",
                SpecieId = 1,
                Spelling = "Agbogbeton"
            }
        };

        private static readonly SpeciePhoto[] DefaultSpeciePhotos =
        {
            new()
            {
                Id = 1,
                Photo = "/SpeciePhoto/1.png",
                SpecieId = 1
            }
        };

        private static readonly LocalDistribution[] DefaultLocalDistributions =
        {
            new()
            {
                Id = 1,
                Image = "/LocalDistribution/1.png",
                Place = "Bénin",
                SpecieId = 1
            }
        };

        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Specie>()
                .HasData(DefaultSpecies);
            builder.Entity<LocalName>()
                .HasData(DefaultLocalNames);
            builder.Entity<SpeciePhoto>()
                .HasData(DefaultSpeciePhotos);
            builder.Entity<LocalDistribution>()
                .HasData(DefaultLocalDistributions);
        }
    }
}