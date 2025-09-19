using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BioDesk.App.Data;
using BioDesk.App.Models;
using BioDesk.App.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BioDesk.App.ViewModels;

public partial class FichaPacienteViewModel : ObservableObject
{
    private readonly BioDeskDbContext _dbContext;
    private readonly NavigationService _navigationService;
    private int _pacienteId;

    [ObservableProperty]
    private int abaAtiva = 0;

    [ObservableProperty]
    private string nomePaciente = "Novo Paciente";

    [ObservableProperty]
    private int idadePaciente = 0;

    // Propriedades do modelo Paciente
    [ObservableProperty]
    private string nomeCompleto = string.Empty;

    [ObservableProperty]
    private DateTime dataNascimento = DateTime.Now.AddYears(-30);

    [ObservableProperty]
    private string? genero;

    [ObservableProperty]
    private string? nifPaciente;

    [ObservableProperty]
    private string? telefone;

    [ObservableProperty]
    private string? email;

    [ObservableProperty]
    private string? morada;

    [ObservableProperty]
    private string? cidade;

    [ObservableProperty]
    private string? codigoPostal;

    [ObservableProperty]
    private string? profissao;

    [ObservableProperty]
    private string? estadoCivil;

    [ObservableProperty]
    private string? localHabitual2;

    [ObservableProperty]
    private string? comoConheceu;

    [ObservableProperty]
    private string? quemRecomendou;

    [ObservableProperty]
    private string? observacoes;

    // Propriedade calculada para mostrar a idade
    public int Idade => DateTime.Now.Year - DataNascimento.Year - 
        (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);

    // Propriedade para mostrar/ocultar campo "Quem Recomendou"
    public bool MostrarCampoRecomendacao => ComoConheceu == "Recomendação";

    // Propriedades para controlo de alterações
    [ObservableProperty]
    private bool temAlteracoesNaoGuardadas;

    private bool _estaCarregandoDados = false;

    // Dados originais para comparação
    private string? _nomeCompletoOriginal;
    private DateTime _dataNascimentoOriginal;
    private string? _generoOriginal;
    private string? _nifOriginal;
    private string? _telefoneOriginal;
    private string? _emailOriginal;
    private string? _moradaOriginal;
    private string? _cidadeOriginal;
    private string? _codigoPostalOriginal;
    private string? _profissaoOriginal;
    private string? _estadoCivilOriginal;
    private string? _localHabitualOriginal;
    private string? _comoConheceuOriginal;
    private string? _quemRecomendouOriginal;
    private string? _observacoesOriginal;

    public FichaPacienteViewModel(BioDeskDbContext dbContext, NavigationService navigationService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        try
        {
            BioDesk.App.App.DebugLog("=== INICIANDO GUARDAR PACIENTE ===");
            BioDesk.App.App.DebugLog($"Nome: '{NomeCompleto}'");
            BioDesk.App.App.DebugLog($"Data Nascimento: {DataNascimento}");
            BioDesk.App.App.DebugLog($"Local: '{LocalHabitual2}'");
            BioDesk.App.App.DebugLog($"Telefone: '{Telefone}'");
            BioDesk.App.App.DebugLog($"Email: '{Email}'");
            
            // Validação obrigatória
            if (string.IsNullOrWhiteSpace(NomeCompleto))
            {
                MessageBox.Show("Nome completo é obrigatório!", "Erro de Validação", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar duplicações de email e NIF (se preenchidos)
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var emailExiste = await _dbContext.Pacientes
                    .AnyAsync(p => p.Email == Email && p.Id != _pacienteId && p.Ativo);
                
                if (emailExiste)
                {
                    MessageBox.Show($"Já existe um paciente com o email '{Email}'!", "Email Duplicado", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(NifPaciente))
            {
                var nifExiste = await _dbContext.Pacientes
                    .AnyAsync(p => p.NIF == NifPaciente && p.Id != _pacienteId && p.Ativo);
                
                if (nifExiste)
                {
                    MessageBox.Show($"Já existe um paciente com o NIF '{NifPaciente}'!", "NIF Duplicado", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            // Verificar duplicação de nome completo e data de nascimento
            var nomeDataExiste = await _dbContext.Pacientes
                .AnyAsync(p => p.NomeCompleto == NomeCompleto && 
                             p.DataNascimento == DataNascimento && 
                             p.Id != _pacienteId && p.Ativo);
            
            if (nomeDataExiste)
            {
                var resultado = MessageBox.Show(
                    $"Já existe um paciente com o nome '{NomeCompleto}' e a data de nascimento {DataNascimento:dd/MM/yyyy}.\n\nDeseja continuar mesmo assim?", 
                    "Possível Duplicação", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Question);
                
                if (resultado != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            // Garantir que a base de dados existe
            await _dbContext.Database.EnsureCreatedAsync();
            BioDesk.App.App.DebugLog("Base de dados verificada/criada");

            Paciente paciente;
            
            if (_pacienteId == 0)
            {
                // Novo paciente
                BioDesk.App.App.DebugLog("Criando novo paciente");
                paciente = new Paciente();
                
                // Atualizar todos os dados (converter strings vazias para null)
                paciente.NomeCompleto = NomeCompleto;
                paciente.DataNascimento = DataNascimento;
                paciente.Genero = string.IsNullOrWhiteSpace(Genero) ? null : Genero;
                paciente.NIF = string.IsNullOrWhiteSpace(NifPaciente) ? null : NifPaciente;
                paciente.Telefone = string.IsNullOrWhiteSpace(Telefone) ? null : Telefone;
                paciente.Email = string.IsNullOrWhiteSpace(Email) ? null : Email;
                paciente.Morada = string.IsNullOrWhiteSpace(Morada) ? null : Morada;
                paciente.Cidade = string.IsNullOrWhiteSpace(Cidade) ? null : Cidade;
                paciente.CodigoPostal = string.IsNullOrWhiteSpace(CodigoPostal) ? null : CodigoPostal;
                paciente.Profissao = string.IsNullOrWhiteSpace(Profissao) ? null : Profissao;
                paciente.EstadoCivil = string.IsNullOrWhiteSpace(EstadoCivil) ? null : EstadoCivil;
                paciente.LocalHabitual = string.IsNullOrWhiteSpace(LocalHabitual2) ? null : LocalHabitual2;
                paciente.ComoConheceu = string.IsNullOrWhiteSpace(ComoConheceu) ? null : ComoConheceu;
                paciente.QuemRecomendou = string.IsNullOrWhiteSpace(QuemRecomendou) ? null : QuemRecomendou;
                paciente.Observacoes = string.IsNullOrWhiteSpace(Observacoes) ? null : Observacoes;
                
                _dbContext.Pacientes.Add(paciente);
                BioDesk.App.App.DebugLog("Paciente adicionado ao contexto");
            }
            else
            {
                // Paciente existente
                BioDesk.App.App.DebugLog($"Atualizando paciente existente ID: {_pacienteId}");
                BioDesk.App.App.DebugLog($"Atualizando paciente existente ID: {_pacienteId}");
                var pacienteExistente = await _dbContext.Pacientes.FindAsync(_pacienteId);
                
                if (pacienteExistente == null)
                {
                    var erro = "Paciente não encontrado na base de dados!";
                    BioDesk.App.App.DebugLog($"ERRO: {erro}");
                    MessageBox.Show(erro, "Erro", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                paciente = pacienteExistente;
                
                // Atualizar dados do paciente existente (converter strings vazias para null)
                paciente.NomeCompleto = NomeCompleto;
                paciente.DataNascimento = DataNascimento;
                paciente.Genero = string.IsNullOrWhiteSpace(Genero) ? null : Genero;
                paciente.NIF = string.IsNullOrWhiteSpace(NifPaciente) ? null : NifPaciente;
                paciente.Telefone = string.IsNullOrWhiteSpace(Telefone) ? null : Telefone;
                paciente.Email = string.IsNullOrWhiteSpace(Email) ? null : Email;
                paciente.Morada = string.IsNullOrWhiteSpace(Morada) ? null : Morada;
                paciente.Cidade = string.IsNullOrWhiteSpace(Cidade) ? null : Cidade;
                paciente.CodigoPostal = string.IsNullOrWhiteSpace(CodigoPostal) ? null : CodigoPostal;
                paciente.Profissao = string.IsNullOrWhiteSpace(Profissao) ? null : Profissao;
                paciente.EstadoCivil = string.IsNullOrWhiteSpace(EstadoCivil) ? null : EstadoCivil;
                paciente.LocalHabitual = string.IsNullOrWhiteSpace(LocalHabitual2) ? null : LocalHabitual2;
                paciente.ComoConheceu = string.IsNullOrWhiteSpace(ComoConheceu) ? null : ComoConheceu;
                paciente.QuemRecomendou = string.IsNullOrWhiteSpace(QuemRecomendou) ? null : QuemRecomendou;
                paciente.Observacoes = string.IsNullOrWhiteSpace(Observacoes) ? null : Observacoes;
                
                _dbContext.Pacientes.Update(paciente);
                BioDesk.App.App.DebugLog("Paciente atualizado no contexto");
            }

            // Salvar alterações na base de dados
            BioDesk.App.App.DebugLog("Salvando alterações na base de dados...");
            var rowsAffected = await _dbContext.SaveChangesAsync();
            BioDesk.App.App.DebugLog($"Operação concluída! Linhas afetadas: {rowsAffected}");

            // Se é um novo paciente, atualizar o ID
            if (_pacienteId == 0)
            {
                _pacienteId = paciente.Id;
                BioDesk.App.App.DebugLog($"Novo paciente criado com ID: {_pacienteId}");
                
                // Atualizar header
                NomePaciente = paciente.NomeCompleto;
                IdadePaciente = paciente.Idade;
            }

            // Marcar como guardado e atualizar dados originais
            TemAlteracoesNaoGuardadas = false;
            SalvarDadosOriginais();

            MessageBox.Show("Dados guardados com sucesso!", "Sucesso", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            
            BioDesk.App.App.DebugLog("=== PACIENTE GUARDADO COM SUCESSO ===");
        }
        catch (Exception ex)
        {
            BioDesk.App.App.DebugLog($"=== ERRO AO GUARDAR PACIENTE ===");
            BioDesk.App.App.DebugLog($"Erro: {ex.Message}");
            BioDesk.App.App.DebugLog($"StackTrace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                BioDesk.App.App.DebugLog($"InnerException: {ex.InnerException.Message}");
            }
            
            MessageBox.Show($"Erro ao guardar dados do paciente:\n\n{ex.Message}\n\nVerifique os dados e tente novamente.", 
                "Erro ao Guardar", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Voltar()
    {
        if (TemAlteracoesNaoGuardadas)
        {
            var resultado = MessageBox.Show(
                "Tem alterações não guardadas. Deseja guardar antes de sair?",
                "Alterações Não Guardadas",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            switch (resultado)
            {
                case MessageBoxResult.Yes:
                    // Tentar guardar primeiro
                    _ = Task.Run(async () =>
                    {
                        await GuardarAsync();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _navigationService.GoTo("Pacientes");
                        });
                    });
                    return;
                case MessageBoxResult.Cancel:
                    return; // Não sair
                case MessageBoxResult.No:
                    // Continuar sem guardar
                    break;
            }
        }
        
        _navigationService.GoTo("Pacientes");
    }

    // Método para carregar dados de um paciente existente
    public async Task CarregarPacienteAsync(int pacienteId)
    {
        _estaCarregandoDados = true;
        _pacienteId = pacienteId;
        
        if (pacienteId == 0)
        {
            // Novo paciente
            NomePaciente = "Novo Paciente";
            IdadePaciente = 0;
            
            // Limpar todos os campos
            NomeCompleto = string.Empty;
            DataNascimento = DateTime.Now.AddYears(-30);
            Genero = null;
            NifPaciente = null;
            Telefone = null;
            Email = null;
            Morada = null;
            Cidade = null;
            CodigoPostal = null;
            Profissao = null;
            EstadoCivil = null;
            LocalHabitual2 = null;
            ComoConheceu = null;
            QuemRecomendou = null;
            Observacoes = null;
            
            SalvarDadosOriginais();
            TemAlteracoesNaoGuardadas = false;
            _estaCarregandoDados = false;
            return;
        }

        var paciente = await _dbContext.Pacientes.FindAsync(pacienteId);
        if (paciente != null)
        {
            NomeCompleto = paciente.NomeCompleto;
            DataNascimento = paciente.DataNascimento;
            Genero = paciente.Genero;
            NifPaciente = paciente.NIF;
            Telefone = paciente.Telefone;
            Email = paciente.Email;
            Morada = paciente.Morada;
            Cidade = paciente.Cidade;
            CodigoPostal = paciente.CodigoPostal;
            Profissao = paciente.Profissao;
            EstadoCivil = paciente.EstadoCivil;
            LocalHabitual2 = paciente.LocalHabitual;
            ComoConheceu = paciente.ComoConheceu;
            QuemRecomendou = paciente.QuemRecomendou;
            Observacoes = paciente.Observacoes;
            
            // Atualizar header
            NomePaciente = paciente.NomeCompleto;
            IdadePaciente = paciente.Idade;
            
            SalvarDadosOriginais();
            TemAlteracoesNaoGuardadas = false;
        }
        
        _estaCarregandoDados = false;
    }

    // Método para salvar dados originais para comparação
    private void SalvarDadosOriginais()
    {
        _nomeCompletoOriginal = NomeCompleto;
        _dataNascimentoOriginal = DataNascimento;
        _generoOriginal = Genero;
        _nifOriginal = NifPaciente;
        _telefoneOriginal = Telefone;
        _emailOriginal = Email;
        _moradaOriginal = Morada;
        _cidadeOriginal = Cidade;
        _codigoPostalOriginal = CodigoPostal;
        _profissaoOriginal = Profissao;
        _estadoCivilOriginal = EstadoCivil;
        _localHabitualOriginal = LocalHabitual2;
        _comoConheceuOriginal = ComoConheceu;
        _quemRecomendouOriginal = QuemRecomendou;
        _observacoesOriginal = Observacoes;
    }

    // Método para verificar se há alterações
    private void VerificarAlteracoes()
    {
        if (_estaCarregandoDados) return;

        var temAlteracoes = 
            NomeCompleto != _nomeCompletoOriginal ||
            DataNascimento != _dataNascimentoOriginal ||
            Genero != _generoOriginal ||
            NifPaciente != _nifOriginal ||
            Telefone != _telefoneOriginal ||
            Email != _emailOriginal ||
            Morada != _moradaOriginal ||
            Cidade != _cidadeOriginal ||
            CodigoPostal != _codigoPostalOriginal ||
            Profissao != _profissaoOriginal ||
            EstadoCivil != _estadoCivilOriginal ||
            LocalHabitual2 != _localHabitualOriginal ||
            ComoConheceu != _comoConheceuOriginal ||
            QuemRecomendou != _quemRecomendouOriginal ||
            Observacoes != _observacoesOriginal;

        TemAlteracoesNaoGuardadas = temAlteracoes;
    }

    // Método para atualizar campos calculados quando a data de nascimento muda
    partial void OnDataNascimentoChanged(DateTime value)
    {
        OnPropertyChanged(nameof(Idade));
        IdadePaciente = Idade;
        BioDesk.App.App.DebugLog($"Idade calculada: {Idade} anos");
        VerificarAlteracoes();
    }

    // Método para mostrar/ocultar campo condicional
    partial void OnComoConheceuChanged(string? value)
    {
        OnPropertyChanged(nameof(MostrarCampoRecomendacao));
        if (value != "Recomendação")
        {
            QuemRecomendou = null;
        }
        VerificarAlteracoes();
    }

    // Atualizar nome no paciente no cabeçalho
    partial void OnNomeCompletoChanged(string value)
    {
        NomePaciente = string.IsNullOrWhiteSpace(value) ? "Novo Paciente" : value;
        VerificarAlteracoes();
    }

    // Métodos para detectar alterações em todos os campos
    partial void OnGeneroChanged(string? value) => VerificarAlteracoes();
    partial void OnNifPacienteChanged(string? value) => VerificarAlteracoes();
    partial void OnTelefoneChanged(string? value) => VerificarAlteracoes();
    partial void OnEmailChanged(string? value) => VerificarAlteracoes();
    partial void OnMoradaChanged(string? value) => VerificarAlteracoes();
    partial void OnCidadeChanged(string? value) => VerificarAlteracoes();
    partial void OnCodigoPostalChanged(string? value) => VerificarAlteracoes();
    partial void OnProfissaoChanged(string? value) => VerificarAlteracoes();
    partial void OnEstadoCivilChanged(string? value) => VerificarAlteracoes();
    partial void OnLocalHabitual2Changed(string? value) => VerificarAlteracoes();
    partial void OnQuemRecomendouChanged(string? value) => VerificarAlteracoes();
    partial void OnObservacoesChanged(string? value) => VerificarAlteracoes();
}
