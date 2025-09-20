#!/usr/bin/env pwsh

# Script para resolver CS0103 permanentemente no VS Code

Write-Host "🔧 Resolvendo erros CS0103 no VS Code..." -ForegroundColor Yellow

# 1. Reiniciar OmniSharp Server
Write-Host "📡 Reiniciando OmniSharp Server..." -ForegroundColor Green
code --command 'omnisharp.restart'

# 2. Recarregar workspace
Write-Host "🔄 Recarregando workspace..." -ForegroundColor Green
code --command 'workbench.action.reloadWindow'

# 3. Rebuild clean
Write-Host "🏗️ Fazendo rebuild limpo..." -ForegroundColor Green
dotnet clean --configuration Debug
dotnet build --configuration Debug --verbosity minimal

# 4. Verificar se arquivos .g.cs existem
$gcsFiles = Get-ChildItem -Path "BioDesk.App\obj" -Filter "*.g.cs" -Recurse
Write-Host "✅ Arquivos .g.cs encontrados: $($gcsFiles.Count)" -ForegroundColor Green

# 5. Restart C# extension
Write-Host "🔌 Reiniciando extensão C#..." -ForegroundColor Green
code --command 'workbench.action.reloadWindow'

Write-Host "🎉 Processo concluído! Os erros CS0103 devem ser resolvidos." -ForegroundColor Cyan
Write-Host "💡 Se os erros persistirem, reinicie o VS Code completamente." -ForegroundColor Yellow