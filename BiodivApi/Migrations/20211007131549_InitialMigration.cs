using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BiodivApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EncodedKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Permission = table.Column<int>(type: "integer", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TaxonomicGroup = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EnglishName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ScientificName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Habitat = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Threats = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalDistributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Place = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    SpecieId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalDistributions_Species_SpecieId",
                        column: x => x.SpecieId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Language = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Spelling = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SpecieId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalNames_Species_SpecieId",
                        column: x => x.SpecieId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpeciePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Photo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    SpecieId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeciePhotos_Species_SpecieId",
                        column: x => x.SpecieId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "Description", "EnglishName", "Habitat", "Name", "ScientificName", "Status", "TaxonomicGroup", "Threats" },
                values: new object[,]
                {
                    { 1, "Animal de couleur rougeâtre a noir en passant par le brun, il pèse pour un adulte de 265 a 320 kg pour le buffle de foret et de 500 a 700 kg pour le buffle de savane. Il est grégaire et peut former des groupes de plusieurs centaines de têtes. Il consomme plutôt des herbes et aime les endroits pas trop éloignés des points d’eau.", "Buffalo", "Forets et savanes", "Buffle", "Syncerus caffer", 5, "Bovin; sous-famille des Bubalina", "son habitat est de plus en plus dégradé, occupe par les champs et les habitations humaines. Par ailleurs, la viande étant très appréciée, l’espèce est menacée par le braconnage." },
                    { 28, "", "Maxwell's duiker", "Forêt humide", "Céphalophe de Maxwell", "Philantomba maxwelli", 6, "Ordre des artiodactyles, famille des bovidés", "" },
                    { 27, "", "Red sided duiker", "Savanes et steppes", "Céphalophe à flancs roux", "Cephalophus rufilatus", 6, "Ordre des artiodactyles, famille des bovidés", "" },
                    { 26, "", "Grimm's duiker", "Savanes et steppes", "Céphalophe de grimm", "Sylvicapra grimmia", 6, "Ordre des artiodactyles, famille des bovidés", "" },
                    { 25, "", "Sitatunga", "Savanes et steppes", "Sitatunga", "Tragelaphus spekei", 6, "Ordre des artiodactyles, famille des bovidés", "" },
                    { 24, "", "harnessed guib", "Savanes et steppes", "Guib harnaché", "Tragelaphus scriptus", 6, "Ordre des artiodactyles, famille des bovidés", "" },
                    { 23, "", "Céphalophe noir", "Savanes et steppes", "Black duiker", "Cephalophus niger", 6, "Ordre des artiodactyles, famille des bovidés", "" },
                    { 22, "", "Girafe", "Savanes et steppes", "Girafe", "Giraffa camelopardalis", 3, "Ordre des artiodactyles, famille des giraffidés", "" },
                    { 21, "", "Ratel", "Savanes et steppes", "Ratel", "Mellivora capensis", 3, "Ordre des carnivores, famille des mustélidés", "" },
                    { 20, "", "White-cheeked otter", "Savanes et steppes", "Loutre à joues blanches", "Aonyx capensis", 3, "Ordre des carnivores, famille des mustélidés", "" },
                    { 19, "", "Spotted otter", "Savanes et steppes", "Loutre à cou tacheté", "Lutra maculicolis", 3, "Ordre des carnivores, famille des mustélidés", "" },
                    { 18, "", "Zorilla", "Savanes et steppes", "Zorille commun", "Ictonyx striatus", 3, "Ordre des carnivores, famille des mustélidés", "" },
                    { 17, "", "Genet", "Savanes et steppes", "Genette commune", "Genetta genetta", 3, "Ordre des carnivores, famille des viverridés", "" },
                    { 16, "", "Nandinie", "Savanes et steppes", "Nandinie", "Nandinia binotata", 3, "Ordre des carnivores, famille des viverridés", "" },
                    { 15, "", "Civet", "Savanes et steppes", "Civette", "Civettictis civetta", 3, "Ordre des carnivores, famille des viverridés", "" },
                    { 14, "", "Red mongoose", "Savanes et steppes", "Mangouste rouge", "Herpestes sanguineus ", 3, "Ordre des carnivores, famille des herpestidés", "" },
                    { 13, "", "Marsh mongoose", "Savanes et steppes", "Mangouste des marais", "Atilax paludinosus", 3, "Ordre des carnivores, famille des herpestidés", "" },
                    { 12, "", "White tailed mongoose", "Savanes et steppes", "Mangouste à queue blanche", "Ichneumia albicauda", 3, "Ordre des carnivores, famille des herpestidés", "" },
                    { 11, "", "Ichemon mongoose", "Savanes et steppes", "Mangouste icheumon", "Herpestes ichneumon", 3, "Ordre des carnivores, famille des herpestidés", "" },
                    { 10, "", "Wild dog", "Savanes et steppes", "Lycaon", "Lycaon pictus", 3, "Ordre des carnivores, famille des canidés", "" },
                    { 9, "", "Golden jackal", "Savanes", "Chacal doré", "Canis aureus", 6, "Ordre des carnivores, famille des canidés", "" },
                    { 8, "", "Striped jackal", "Savanes", "Chacal à flancs rayés", "Canis adustus", 6, "Ordre des carnivores, famille des canidés", "" },
                    { 7, "", "Hyena", "Savanes", "Hyène", "Crocuta crocuta", 6, "Ordre des carnivores, famille des hyenidés", "" },
                    { 6, "", "Caracal", "Savanes humides", "Caracal", "Caracal caracal", 6, "Ordre des carnivores, famille des félidés", "" },
                    { 5, "", "Serval", "Savanes humides", "Serval", "Leptailurus serval", 6, "Ordre des carnivores, famille des félidés", "" },
                    { 4, "", "Cheetah", "Savanes et déserts", "Guépard", "Acinonyx jubatus", 4, "Ordre des carnivores, famille des félidés", "" },
                    { 3, "", "Leopard", "Forêts et savanes", "Léopard", "Panthera pardus", 4, "Ordre des carnivores, famille des félidés", "" },
                    { 2, "", "Lion", "Forêts et savanes", "Lion", "Panthera Leo", 4, "Ordre des carnivores, famille des félidés", "" },
                    { 29, "", "Red-fronted gazelle", "Forêt humide", "Gazelle à front roux", "Eudorcas rufifrons", 4, "Ordre des artiodactyles, famille des bovidés", "" },
                    { 30, "", "Ourébi", "Brousse, savane arborée, non loin de l'eau. Il peut vivre en plaine comme en montagne, jusqu'à 3 000 mètres d'altitude.", "Ourébi", "Ourebia ourebi", 6, "Ordre des artiodactyles, famille des bovidés", "" }
                });

            migrationBuilder.InsertData(
                table: "LocalDistributions",
                columns: new[] { "Id", "Image", "Place", "SpecieId" },
                values: new object[] { 1, "/LocalDistribution/1.png", "Bénin", 1 });

            migrationBuilder.InsertData(
                table: "LocalNames",
                columns: new[] { "Id", "Language", "SpecieId", "Spelling" },
                values: new object[] { 1, "Fon", 1, "Agbogbeton" });

            migrationBuilder.InsertData(
                table: "SpeciePhotos",
                columns: new[] { "Id", "Photo", "SpecieId" },
                values: new object[] { 1, "/SpeciePhoto/1.png", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_LocalDistributions_SpecieId",
                table: "LocalDistributions",
                column: "SpecieId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalNames_SpecieId",
                table: "LocalNames",
                column: "SpecieId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciePhotos_SpecieId",
                table: "SpeciePhotos",
                column: "SpecieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "LocalDistributions");

            migrationBuilder.DropTable(
                name: "LocalNames");

            migrationBuilder.DropTable(
                name: "SpeciePhotos");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
