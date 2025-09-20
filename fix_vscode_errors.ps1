# Script para eliminar definitivamente os erros CS0103 no VS Code
Write-Host "=== SOLUÇÂO DEFINITIVA PARA ERROS CS0103 ===" -ForegroundColor Green

# 1. Garantir que todos os processos do VS Code estão parados
Write-Host "1. Parando processos do VS Code..." -ForegroundColor Yellow
Get-Process -Name "code*" -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

# 2. Limpar cache de extensões C#
Write-Host "2. Limpando cache das extensões C#..." -ForegroundColor Yellow
$userProfile = $env:USERPROFILE
$extensionPaths = @(
    "$userProfile\.vscode\extensions\ms-dotnettools.csharp*",
    "$userProfile\.vscode\extensions\ms-dotnettools.vscode-dotnet-runtime*",
    "$userProfile\AppData\Roaming\Code\User\workspaceStorage",
    "$userProfile\AppData\Roaming\Code\logs"
)

foreach ($path in $extensionPaths) {
    if (Test-Path $path) {
        Remove-Item $path -Recurse -Force -ErrorAction SilentlyContinue
        Write-Host "   Cache removido: $path" -ForegroundColor Gray
    }
}

# 3. Limpar completamente obj e bin
Write-Host "3. Limpando obj e bin..." -ForegroundColor Yellow
dotnet clean --verbosity quiet
Remove-Item ".\BioDesk.App\obj" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item ".\BioDesk.App\bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item ".\BioDesk.Tests\obj" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item ".\BioDesk.Tests\bin" -Recurse -Force -ErrorAction SilentlyContinue

# 4. Recompilar tudo
Write-Host "4. Recompilando projeto..." -ForegroundColor Yellow
dotnet restore --verbosity quiet
dotnet build --verbosity quiet

# 5. Verificar se arquivos .g.cs foram criados
Write-Host "5. Verificando arquivos .g.cs..." -ForegroundColor Yellow
$gFiles = Get-ChildItem -Path ".\BioDesk.App\obj" -Filter "*.g.cs" -Recurse -ErrorAction SilentlyContinue
if ($gFiles.Count -gt 0) {
    Write-Host "   ✅ $($gFiles.Count) arquivos .g.cs encontrados" -ForegroundColor Green
    foreach ($file in $gFiles) {
        Write-Host "      - $($file.FullName)" -ForegroundColor Gray
    }
} else {
    Write-Host "   ❌ Nenhum arquivo .g.cs encontrado!" -ForegroundColor Red
    exit 1
}

# 6. Reiniciar VS Code com configurações otimizadas
Write-Host "6. Reiniciando VS Code..." -ForegroundColor Yellow
Start-Sleep -Seconds 2

# Abrir VS Code com configurações específicas
$vscodeArgs = @(
    ".",
    "--disable-extension", "ms-vscode.cpptools",
    "--disable-extension", "ms-vscode.cmake-tools"
)

Start-Process "code" -ArgumentList $vscodeArgs -NoNewWindow

Write-Host "=== SOLUÇÂO APLICADA COM SUCESSO ===" -ForegroundColor Green
Write-Host ""
Write-Host "PRÓXIMOS PASSOS:" -ForegroundColor Cyan
Write-Host "1. Aguarde o VS Code carregar completamente" -ForegroundColor White
Write-Host "2. Pressione Ctrl+Shift+P" -ForegroundColor White
Write-Host "3. Execute: 'Developer: Reload Window'" -ForegroundColor White
Write-Host "4. Se ainda houver erros, execute: 'C#: Restart Language Server'" -ForegroundColor White
Write-Host ""
Write-Host "Os erros CS0103 devem estar resolvidos agora!" -ForegroundColor Green