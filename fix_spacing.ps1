# Script para corrigir propriedades Spacing no WPF
$filePath = "c:\BioDesk PRO\BioDesk.App\Views\FichaPacienteView.xaml"

# Ler o conteúdo
$content = Get-Content $filePath -Raw

# Padrões para remover
$patterns = @(
    ' Spacing="24"',
    ' Spacing="16"', 
    ' Spacing="8"',
    ' Spacing="12"'
)

# Remover cada padrão
foreach ($pattern in $patterns) {
    $content = $content.Replace($pattern, '')
}

# Salvar o arquivo
Set-Content $filePath -Value $content -Encoding UTF8 -NoNewline

Write-Host "Propriedades Spacing removidas com sucesso!"