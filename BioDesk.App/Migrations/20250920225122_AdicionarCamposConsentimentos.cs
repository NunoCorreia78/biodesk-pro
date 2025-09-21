using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BioDesk.App.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCamposConsentimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCompleto = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Genero = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    NIF = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Morada = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CodigoPostal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Profissao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EstadoCivil = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LocalHabitual = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ComoConheceu = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    QuemRecomendou = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UltimaConsulta = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsentimentosInformados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PacienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoTerapia = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ConteudoConsentimento = table.Column<string>(type: "TEXT", nullable: false),
                    AssinaturaDigital = table.Column<string>(type: "TEXT", nullable: true),
                    DataConsentimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConsentimentoObtido = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObservacoesAdicionais = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AceitaContactoFisico = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompreendeNaturezaTratamento = table.Column<bool>(type: "INTEGER", nullable: false),
                    AceitaRiscosAssociados = table.Column<bool>(type: "INTEGER", nullable: false),
                    PermiteExposicaoAreasCorporais = table.Column<bool>(type: "INTEGER", nullable: false),
                    TermosEspecificos = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IdentificacaoProfissional = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    LocalTratamento = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DataValidade = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CaminhoDocumento = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    HashVerificacao = table.Column<string>(type: "TEXT", nullable: true),
                    NaturopatiaCompreendeNatureza = table.Column<bool>(type: "INTEGER", nullable: false),
                    NaturopatiaObjetivosExplicados = table.Column<bool>(type: "INTEGER", nullable: false),
                    NaturopatiaRiscosDiscutidos = table.Column<bool>(type: "INTEGER", nullable: false),
                    NaturopatiaInformacaoPatologias = table.Column<bool>(type: "INTEGER", nullable: false),
                    NaturopatiaAlternativasDiscutidas = table.Column<bool>(type: "INTEGER", nullable: false),
                    NaturopatiaAutorizacaoCorresponsabilidade = table.Column<bool>(type: "INTEGER", nullable: false),
                    NaturopatiaRevogavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    NaturopatiaAssinatura = table.Column<string>(type: "TEXT", nullable: true),
                    NaturopatiaDataAssinatura = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OsteopatiaTecnicasExplicadas = table.Column<bool>(type: "INTEGER", nullable: false),
                    OsteopatiaContraindicacoesDiscutidas = table.Column<bool>(type: "INTEGER", nullable: false),
                    OsteopatiaRiscosExplicados = table.Column<bool>(type: "INTEGER", nullable: false),
                    OsteopatiaAutorizoContactoFisico = table.Column<bool>(type: "INTEGER", nullable: false),
                    OsteopatiaPossoInterromper = table.Column<bool>(type: "INTEGER", nullable: false),
                    OsteopatiaRevogavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    OsteopatiaAssinatura = table.Column<string>(type: "TEXT", nullable: true),
                    OsteopatiaDataAssinatura = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IridologiaNaturezaNaoInvasiva = table.Column<bool>(type: "INTEGER", nullable: false),
                    IridologiaAutorizoCapturaImagens = table.Column<bool>(type: "INTEGER", nullable: false),
                    IridologiaCompreensoLimitacoes = table.Column<bool>(type: "INTEGER", nullable: false),
                    IridologiaRevogavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    IridologiaAssinatura = table.Column<string>(type: "TEXT", nullable: true),
                    IridologiaDataAssinatura = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MedicinaQuanticaAbordagemComplementar = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicinaQuanticaNaturezaProcedimentos = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicinaQuanticaPossiveisReacoes = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicinaQuanticaContraindicacoes = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicinaQuanticaLiberdadeInterromper = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicinaQuanticaRevogavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicinaQuanticaAssinatura = table.Column<string>(type: "TEXT", nullable: true),
                    MedicinaQuanticaDataAssinatura = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RgpdInformacaoResponsavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    RgpdDireitosAcesso = table.Column<bool>(type: "INTEGER", nullable: false),
                    RgpdConsentimentoExplicito = table.Column<bool>(type: "INTEGER", nullable: false),
                    RgpdOpcaoNaoMarketing = table.Column<bool>(type: "INTEGER", nullable: false),
                    RgpdRevogavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    RgpdAssinatura = table.Column<string>(type: "TEXT", nullable: true),
                    RgpdDataAssinatura = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AssinaturaQuestionarioCompleto = table.Column<string>(type: "TEXT", nullable: true),
                    DataAssinaturaQuestionario = table.Column<DateTime>(type: "TEXT", nullable: true),
                    QuestionarioAssinado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsentimentosInformados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsentimentosInformados_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionariosSaude",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PacienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    QueixaPrincipal = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    ObjetivoAlivioDor = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoMelhorarMobilidade = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoMelhorarDigestao = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoReduzirStress = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoMelhorarSono = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoGestaoWeight = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoAumentarVitalidade = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoBemEstarGeral = table.Column<bool>(type: "INTEGER", nullable: false),
                    ObjetivoOutro = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SaudeGlobal = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    NivelDorAtual = table.Column<int>(type: "INTEGER", nullable: true),
                    GravidezAleitamento = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ImplantePacemaker = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImplanteStent = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImplanteProtesesArticulares = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImplantePlacasParafusos = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImplanteDIU = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImplantesOutros = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DiagnosticosMedicosAtuais = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    DoencaHipertensao = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaCardiopatia = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaDiabetesTipo1 = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaDiabetesTipo2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaTiroideia = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaDislipidemia = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaRenal = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaHepatica = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaAutoimune = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaOsteoporose = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaAsmaDPOC = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaOncologia = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaCoagulacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaEpilepsia = table.Column<bool>(type: "INTEGER", nullable: false),
                    DoencaAVCTIA = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicacaoAtual = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    SuplementosFitoterapia = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Anticoagulantes = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AlertaFraturasRecentes = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlertaOsteoporoseSevera = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlertaTumorInfecao = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlertaSindromesNeurologicas = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlertaAneurisma = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlertaHipermobilidade = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlertaCirurgiaRecente = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlertasOutros = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AlergiaPenicilina = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaAINEs = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaOpioides = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaAnestesicos = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaCorticosteroides = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaIodoContraste = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiasFarmacosOutros = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IntoleranciaGluten = table.Column<bool>(type: "INTEGER", nullable: false),
                    IntoleranciaLactose = table.Column<bool>(type: "INTEGER", nullable: false),
                    IntoleranciaProteínaLeite = table.Column<bool>(type: "INTEGER", nullable: false),
                    IntoleranciaOvos = table.Column<bool>(type: "INTEGER", nullable: false),
                    IntoleranciaMarisco = table.Column<bool>(type: "INTEGER", nullable: false),
                    IntoleranciaFrutosSecos = table.Column<bool>(type: "INTEGER", nullable: false),
                    IntoleranciaAlimentarOutras = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AlergiaLatex = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaNiquel = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaCosmeticos = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaAdesivos = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaPlantas = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiaContactoOutras = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    HistoriaAnafilaxia = table.Column<bool>(type: "INTEGER", nullable: false),
                    AnafilaxiaGatilho = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    JaFezCirurgias = table.Column<bool>(type: "INTEGER", nullable: false),
                    HistoricoCirurgias = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    JaTeveFraturas = table.Column<bool>(type: "INTEGER", nullable: false),
                    HistoricoFraturas = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ExercicioFisico = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Tabagismo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ConsumoAlcool = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ConsumoCafeina = table.Column<int>(type: "INTEGER", nullable: true),
                    HorasSono = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    QualidadeSono = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    NivelStress = table.Column<int>(type: "INTEGER", nullable: true),
                    ProfissaoErgonomia = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    HabitosAlimentares = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SintomaAzia = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaDorAbdominal = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaDistensao = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaNauseas = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaVomitos = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaDiarreia = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaObstipacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaAlternancia = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaFezesSangue = table.Column<bool>(type: "INTEGER", nullable: false),
                    FrequenciaEvacuacao = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    EscalaBristol = table.Column<int>(type: "INTEGER", nullable: true),
                    DiagnosticosDigestivos = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    HipertensaoConhecida = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaDorToracica = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaPalpitacoes = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaEdemas = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaDispneia = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaIntoleranciaEsforco = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaTosseCronica = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaPieiraAsma = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaApneiaSono = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorCervical = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorDorsal = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorLombar = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorSacroiliaca = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorAnca = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorJoelho = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorTornozeloPe = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorOmbro = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorCotovelo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorPunhoMao = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorATM = table.Column<bool>(type: "INTEGER", nullable: false),
                    DorOutrasLocalizacoes = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    PadraoAguda = table.Column<bool>(type: "INTEGER", nullable: false),
                    PadraoCronica = table.Column<bool>(type: "INTEGER", nullable: false),
                    PadraoIrradiacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    PadraoFormigueiros = table.Column<bool>(type: "INTEGER", nullable: false),
                    PadraoFraqueza = table.Column<bool>(type: "INTEGER", nullable: false),
                    PadraoRigidezMatinal = table.Column<bool>(type: "INTEGER", nullable: false),
                    PadraoBloqueios = table.Column<bool>(type: "INTEGER", nullable: false),
                    PadraoCaibras = table.Column<bool>(type: "INTEGER", nullable: false),
                    BandeiraPeridaPeso = table.Column<bool>(type: "INTEGER", nullable: false),
                    BandeiraFebre = table.Column<bool>(type: "INTEGER", nullable: false),
                    BandeiraTrauma = table.Column<bool>(type: "INTEGER", nullable: false),
                    BandeiraDeficesNeurologicos = table.Column<bool>(type: "INTEGER", nullable: false),
                    BandeiraIncontinencia = table.Column<bool>(type: "INTEGER", nullable: false),
                    BandeiraCancro = table.Column<bool>(type: "INTEGER", nullable: false),
                    BandeiraCorticoides = table.Column<bool>(type: "INTEGER", nullable: false),
                    MovimentosLimitados = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SintomaCefaleias = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaTonturas = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaDesmaios = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaConvulsoes = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaMemoria = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaAnsiedade = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaDepressao = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaPanico = table.Column<bool>(type: "INTEGER", nullable: false),
                    SintomaIdeacaoSuicida = table.Column<bool>(type: "INTEGER", nullable: false),
                    AcompanhamentoPsicologico = table.Column<bool>(type: "INTEGER", nullable: false),
                    RoncoApneia = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ProblemaEczema = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaPsoriase = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaUrticaria = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaAutoimune = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaHipoHipertiroidismo = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaDiabetes = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaSindromeMetabolica = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasOutrosDetalhes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ProblemaITURecorrentes = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaDorPelvica = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaIncontinencia = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaHiperplasiaProstatica = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaEndometriose = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaSPM = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaCicloIrregular = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaMenupausa = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemaInfertilidade = table.Column<bool>(type: "INTEGER", nullable: false),
                    CicloMenstrualDetalhes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    QuedasUltimoAno = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SegurancaCasaRelacoes = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConsumoRiscoAlcool = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConsumoRiscoTabaco = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConsumoRiscoCannabis = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConsumoRiscoOutras = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConsumoRiscoJogo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeclaracaoVeracidade = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeclaracaoComplementar = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutorizacaoPartilha = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutorizacaoContactos = table.Column<bool>(type: "INTEGER", nullable: false),
                    LocalData = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    AssinaturaUtente = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssinaturaProfissional = table.Column<bool>(type: "INTEGER", nullable: false),
                    CondicoesCronicas = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SintomasAtuais = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    TomaMedicacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlergiasAlimentos = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    AlergiasMedicamentos = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    AlergiasAmbientais = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    AlergiasPlantas = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    AlergiasSupplementos = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    HistoricoFamiliar = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Fuma = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConsomeAlcool = table.Column<bool>(type: "INTEGER", nullable: false),
                    PraticaExercicio = table.Column<bool>(type: "INTEGER", nullable: false),
                    DetalhesEstiloVida = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Gravida = table.Column<bool>(type: "INTEGER", nullable: true),
                    Amamentando = table.Column<bool>(type: "INTEGER", nullable: true),
                    DataUltimaMenstruacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HistoricoGinecologico = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ProblemasCardiovasculares = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasRespiratorios = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasDigestivos = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasNeurologicos = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasEndocrinos = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasRenais = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasMusculoesqueleticos = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProblemasPele = table.Column<bool>(type: "INTEGER", nullable: false),
                    DetalhesProblemasSaude = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    JaFezTerapiasComplementares = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExperienciaTerapiasComplementares = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ObservacoesAdicionais = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    DataPreenchimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Completo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionariosSaude", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionariosSaude_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssinaturasDigitais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PacienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionarioSaudeId = table.Column<int>(type: "INTEGER", nullable: true),
                    ConsentimentoInformadoId = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoDocumento = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DadosAssinatura = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ImagemAssinatura = table.Column<byte[]>(type: "BLOB", nullable: false),
                    DataAssinatura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HashVerificacao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    NumeroTracos = table.Column<int>(type: "INTEGER", nullable: false),
                    LarguraAssinatura = table.Column<double>(type: "REAL", nullable: false),
                    AlturaAssinatura = table.Column<double>(type: "REAL", nullable: false),
                    DispositivoUtilizado = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    VersaoApp = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ObservacoesValidacao = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    AssinaturaValida = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataUltimaVerificacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssinaturasDigitais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssinaturasDigitais_ConsentimentosInformados_ConsentimentoInformadoId",
                        column: x => x.ConsentimentoInformadoId,
                        principalTable: "ConsentimentosInformados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AssinaturasDigitais_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssinaturasDigitais_QuestionariosSaude_QuestionarioSaudeId",
                        column: x => x.QuestionarioSaudeId,
                        principalTable: "QuestionariosSaude",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Pacientes",
                columns: new[] { "Id", "Ativo", "Cidade", "CodigoPostal", "ComoConheceu", "DataCriacao", "DataNascimento", "Email", "EstadoCivil", "Genero", "LocalHabitual", "Morada", "NIF", "NomeCompleto", "Observacoes", "Profissao", "QuemRecomendou", "Telefone", "UltimaConsulta" },
                values: new object[,]
                {
                    { 1, true, "Lisboa", "1000-001", "Redes Sociais", new DateTime(2025, 8, 21, 23, 51, 19, 787, DateTimeKind.Local).AddTicks(9311), new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.silva@email.com", "Casado(a)", "Masculino", "Chão de Lopes", "Rua das Flores, 123\n1000-001 Lisboa", "123456789", "João Silva", null, "Engenheiro", null, "912345678", new DateTime(2025, 9, 13, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(9) },
                    { 2, true, "Porto", "4000-002", "Recomendação", new DateTime(2025, 8, 6, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1025), new DateTime(1978, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria.santos@email.com", "Solteiro(a)", "Feminino", "Samora Correia", "Avenida Central, 456\n4000-002 Porto", "987654321", "Maria Santos", null, "Professora", "Ana Costa", "923456789", new DateTime(2025, 9, 6, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1035) },
                    { 3, true, "Braga", "4700-003", "Website/Google", new DateTime(2025, 8, 31, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1053), new DateTime(1992, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos.ferreira@email.com", "União de Facto", "Masculino", "Online", "Praceta do Sol, 789\n4700-003 Braga", "456789123", "Carlos Ferreira", null, "Designer", null, "934567890", new DateTime(2025, 9, 17, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1057) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasDigitais_AssinaturaValida",
                table: "AssinaturasDigitais",
                column: "AssinaturaValida");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasDigitais_ConsentimentoInformadoId",
                table: "AssinaturasDigitais",
                column: "ConsentimentoInformadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasDigitais_DataAssinatura",
                table: "AssinaturasDigitais",
                column: "DataAssinatura");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasDigitais_HashVerificacao",
                table: "AssinaturasDigitais",
                column: "HashVerificacao");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasDigitais_PacienteId",
                table: "AssinaturasDigitais",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasDigitais_QuestionarioSaudeId",
                table: "AssinaturasDigitais",
                column: "QuestionarioSaudeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasDigitais_TipoDocumento",
                table: "AssinaturasDigitais",
                column: "TipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentimentosInformados_ConsentimentoObtido",
                table: "ConsentimentosInformados",
                column: "ConsentimentoObtido");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentimentosInformados_DataConsentimento",
                table: "ConsentimentosInformados",
                column: "DataConsentimento");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentimentosInformados_PacienteId",
                table: "ConsentimentosInformados",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentimentosInformados_TipoTerapia",
                table: "ConsentimentosInformados",
                column: "TipoTerapia");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Email",
                table: "Pacientes",
                column: "Email",
                unique: true,
                filter: "Email IS NOT NULL AND Email != ''");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_NIF",
                table: "Pacientes",
                column: "NIF",
                unique: true,
                filter: "NIF IS NOT NULL AND NIF != ''");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionariosSaude_DataPreenchimento",
                table: "QuestionariosSaude",
                column: "DataPreenchimento");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionariosSaude_PacienteId",
                table: "QuestionariosSaude",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssinaturasDigitais");

            migrationBuilder.DropTable(
                name: "ConsentimentosInformados");

            migrationBuilder.DropTable(
                name: "QuestionariosSaude");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
