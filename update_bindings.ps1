# Script para adicionar data binding aos controles do questionário de saúde
param(
    [string]$FilePath = "c:\BioDesk PRO\BioDesk.App\Views\FichaPacienteView.xaml"
)

# Mapeamento de controles e suas propriedades de binding
$controls = @{
    'txt_AlergiasAlimentares' = 'AlergiasAlimentares'
    'txt_AlergiasMedicamentos' = 'AlergiasMedicamentos'
    'txt_AlergiasAmbientais' = 'AlergiasAmbientais'
    'txt_AlergiasPlantasSupl' = 'AlergiasPlantasSupl'
    'txt_HistoricoCirurgico' = 'HistoricoCirurgico'
    'txt_Fraturas' = 'Fraturas'
    'txt_HabitosAlimentares' = 'HabitosAlimentares'
    'cb_AtividadeFisica' = 'AtividadeFisica'
    'cb_Tabagismo' = 'Tabagismo'
    'cb_ConsumoAlcool' = 'ConsumoAlcool'
}

Write-Host "Atualizando bindings no arquivo: $FilePath"

$content = Get-Content $FilePath -Raw

foreach ($control in $controls.GetEnumerator()) {
    $controlName = $control.Key
    $propertyName = $control.Value
    
    Write-Host "Processando: $controlName -> $propertyName"
    
    if ($controlName.StartsWith('txt_')) {
        # Para TextBox
        $oldPattern = "(<TextBox\s+Name=`"$controlName`"\s+(?:[^>]*?\s+)?)(\s*PlaceholderText=)"
        $newPattern = "`$1Text=`"{Binding $propertyName, UpdateSourceTrigger=PropertyChanged}`"`r`n                                                `$2"
        $content = $content -replace $oldPattern, $newPattern
    }
    elseif ($controlName.StartsWith('cb_')) {
        # Para ComboBox
        $oldPattern = "(<ComboBox\s+Name=`"$controlName`"\s+(?:[^>]*?\s+)?)(\s*BorderBrush=)"
        $newPattern = "`$1SelectedValue=`"{Binding $propertyName, UpdateSourceTrigger=PropertyChanged}`"`r`n                                                 `$2"
        $content = $content -replace $oldPattern, $newPattern
    }
}

Set-Content $FilePath -Value $content -Encoding UTF8

Write-Host "Bindings atualizados com sucesso!"