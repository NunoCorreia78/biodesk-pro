# BioDesk PRO

Sistema de Gestão Médica desenvolvido em WPF .NET 8 com arquitetura MVVM e injeção de dependências.

## 🚀 Como Executar

### Pré-requisitos
- **.NET 8 SDK** (versão LTS recomendada)
- **Windows 10/11**
- **Visual Studio 2022** ou **VS Code** (opcional, para desenvolvimento)

### Passos para executar

```bash
# 1. Verificar versão do .NET
dotnet --version        # deve devolver 8.x

# 2. Restaurar dependências
dotnet restore

# 3. Compilar o projeto
dotnet build

# 4. Executar a aplicação
dotnet run --project "BioDesk.App/BioDesk.App.csproj"
```

## 📁 Estrutura do Projeto

```
BioDesk PRO/
├─ BioDesk.sln                    # Solução principal
├─ global.json                    # Versão fixa do .NET SDK
├─ Directory.Build.props           # Configurações globais
├─ nuget.config                    # Configuração NuGet
├─ BioDesk.App/                    # Projeto WPF principal
│  ├─ Views/                       # Views da aplicação
│  ├─ ViewModels/                  # ViewModels (MVVM)
│  ├─ Services/                    # Serviços (NavigationService, AppPaths)
│  ├─ Styles/                      # Estilos e Design Tokens
│  ├─ images_interface/            # Imagens da interface
│  └─ utilitarios/                 # Assets, templates e consentimentos
└─ BioDesk.Tests/                  # Projeto de testes
```

## 🎨 Funcionalidades

### ✅ Implementadas
- **Navegação MVVM**: Sistema de navegação baseado em serviços
- **Design System**: Cores, tipografia e controlos consistentes
- **Atalhos de Teclado**: Alt+1 (Pacientes), Alt+2 (Íris Anónima)
- **Tratamento de Exceções**: Log automático em `utilitarios/logs/`
- **Injeção de Dependências**: Microsoft.Extensions.DependencyInjection

### 🚧 Em Desenvolvimento
- **Gestão de Pacientes**: Listar, pesquisar e gerir históricos clínicos
- **Avaliação de Íris**: Sessão anónima para carregar imagem e sobrepor mapa

## 🔧 Configuração do Logo

**Temporariamente**, o logo foi substituído por um emoji 🩺 para evitar erros.

Para usar o logo real:

1. Substitua o ficheiro: `BioDesk.App/images_interface/logo.png`
2. Dimensões recomendadas: 256×256 pixels
3. Formato: PNG com transparência
4. Descomente as linhas do logo no `HomeView.xaml` (linhas ~27-30)

```xml
<!-- Descomentar estas linhas após adicionar logo.png -->
<Image Source="pack://application:,,,/images_interface/logo.png" 
       Width="64" Height="64" 
       Margin="0,0,16,0"
       RenderOptions.BitmapScalingMode="HighQuality"/>
```

## 🛠️ Troubleshooting

### "PresentationFramework não encontrado"
Garantir que tem as configurações corretas:
- `<TargetFramework>net8.0-windows</TargetFramework>`
- `<UseWPF>true</UseWPF>`

### "Restore falhou"
- Verificar `nuget.config` (apenas nuget.org)
- Confirmar ligação à internet

### "Assets não aparecem"
Confirmar no `.csproj`:
```xml
<Content Include="images_interface\**\*;utilitarios\**\*">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</Content>
```

### "Ação GitHub falhou"
- Confirmar versão do SDK no `global.json`
- Verificar workflow em `.github/workflows/dotnet-build.yml`

### "Aplicação não abre"
- Verificar se tem .NET 8 Runtime instalado
- Executar `dotnet --version` deve devolver 8.x
- A aplicação WPF pode executar em segundo plano - verificar no Gestor de Tarefas

### "Erro Fatal ao abrir"
- **RESOLVIDO**: Era causado por logo.png inexistente
- Agora usa emoji 🩺 temporariamente
- Para usar logo real, siga instruções na secção "Configuração do Logo"

## 📦 Dependências

- **CommunityToolkit.Mvvm** 8.2.2 - MVVM toolkit
- **Microsoft.Extensions.DependencyInjection** 8.0.0 - Injeção de dependências

## 🔒 Robustez de Build

### Versionamento Fixo
- `global.json` fixa o SDK em .NET 8.0.x
- `Directory.Build.props` define configurações globais
- `packages.lock.json` garante versões consistentes

### Qualidade de Código
- Warnings tratados como erros (`TreatWarningsAsErrors`)
- Analyzers .NET ativados (`EnableNETAnalyzers`)
- Nullable reference types ativadas

### CI/CD
- GitHub Actions com build automático
- Testes automáticos
- Build para Release

## 📋 Design Tokens

### Cores
- **Primary**: #1C4532 (Verde escuro)
- **Surface**: #F5F7F6 (Cinza claro)
- **Text Primary**: #1E2A27
- **Text Secondary**: #5A6B66

### Tipografia
- **Família**: Segoe UI
- **Tamanhos**: Title (28px), Subtitle (20px), Body (14px)

## 🏥 Sobre o BioDesk PRO

Sistema médico focado em naturopatia e medicina integrativa, com funcionalidades para:
- Gestão de pacientes e históricos clínicos
- Avaliação de íris com sobreposição de mapas
- Geração de prescrições e consentimentos
- Interface intuitiva e acessível

---

**Versão**: 0.1  
**Plataforma**: Windows (.NET 8)  
**Arquitetura**: MVVM + DI