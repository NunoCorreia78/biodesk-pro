# 🎯 DESCRIÇÃO COMPLETA DA DECLARAÇÃO DE SAÚDE + CONSENTIMENTOS - BIODESK PRO

## 📋 **ANÁLISE DAS DUPLICAÇÕES IDENTIFICADAS**

### ✅ **PROBLEMAS RESOLVIDOS:**
1. **Modelo QuestionarioSaude.cs expandido** com todos os 200+ campos necessários
2. **TipoTerapiaEnum atualizado** para incluir Iridologia e RGPD
3. **Estrutura consolidada** eliminando duplicações entre ViewModels

### 🔧 **INTEGRAÇÕES EXISTENTES:**
- ✅ Template HTML completo já implementado (`questionario-saude-completo.html`)
- ✅ Consentimentos separados por terapia (`naturopatia.html`, etc.)
- ✅ Sistema de assinaturas digitais funcionional
- ✅ DbContext configurado para persistência

---

## 🏗️ **ESPECIFICAÇÃO TÉCNICA COMPLETA**

### **Título:**
"Declaração Geral de Saúde e Consentimentos Informados"

### **Objetivo:**
Recolher informação clínica, hábitos de vida e consentimentos de forma exaustiva para proteção profissional.
Interface em **secções expansíveis** (Expander/TabControl) com **ComboBoxes, CheckBoxes e TextAreas**.

---

## 📊 **ESTRUTURA DE DADOS (16 SECÇÕES)**

### **1️⃣ DADOS DO UTENTE** *(preenchimento automático)*
```csharp
// Vindos do modelo Paciente:
- NomeCompleto (obrigatório, TextBox)
- DataNascimento (obrigatório, DatePicker)  
- Genero (ComboBox: Feminino | Masculino | Outro/Prefiro não dizer)
- Telefone (TextBox)
- Email (TextBox)
```

### **2️⃣ QUEIXA PRINCIPAL E OBJETIVOS**
```csharp
// Campos do QuestionarioSaude:
public string? QueixaPrincipal { get; set; } // obrigatório, TextArea
public bool ObjetivoAlivioDor { get; set; }
public bool ObjetivoMelhorarMobilidade { get; set; }
public bool ObjetivoMelhorarDigestao { get; set; }
public bool ObjetivoReduzirStress { get; set; }
public bool ObjetivoMelhorarSono { get; set; }
public bool ObjetivoGestaoWeight { get; set; }
public bool ObjetivoAumentarVitalidade { get; set; }
public bool ObjetivoBemEstarGeral { get; set; }
public string? ObjetivoOutro { get; set; } // TextBox especificar
```

### **3️⃣ ESTADO GERAL**
```csharp
public string? SaudeGlobal { get; set; } // ComboBox: Muito boa | Boa | Regular | Fraca
public int? NivelDorAtual { get; set; } // NumericUpDown 0-10
public string? GravidezAleitamento { get; set; } // ComboBox: Não | Grávida | A amamentar

// Implantes/Dispositivos (ComboBox Multi-Select):
public bool ImplantePacemaker { get; set; }
public bool ImplanteStent { get; set; }
public bool ImplanteProtesesArticulares { get; set; }
public bool ImplantePlacasParafusos { get; set; }
public bool ImplanteDIU { get; set; }
public string? ImplantesOutros { get; set; } // TextBox especificar
```

### **4️⃣ INFORMAÇÕES MÉDICAS GERAIS**
```csharp
public string? DiagnosticosMedicosAtuais { get; set; } // TextArea

// Doenças crónicas (checkbox group):
public bool DoencaHipertensao { get; set; }
public bool DoencaCardiopatia { get; set; }
public bool DoencaDiabetesTipo1 { get; set; }
public bool DoencaDiabetesTipo2 { get; set; }
public bool DoencaTiroideia { get; set; }
public bool DoencaDislipidemia { get; set; }
public bool DoencaRenal { get; set; }
public bool DoencaHepatica { get; set; }
public bool DoencaAutoimune { get; set; }
public bool DoencaOsteoporose { get; set; }
public bool DoencaAsmaDPOC { get; set; }
public bool DoencaOncologia { get; set; }
public bool DoencaCoagulacao { get; set; }
public bool DoencaEpilepsia { get; set; }
public bool DoencaAVCTIA { get; set; }

public string? MedicacaoAtual { get; set; } // lista repetível: Fármaco, Dose, Frequência, Desde (AAAA/MM), Indicação
public string? SuplementosFitoterapia { get; set; } // lista repetível: Produto, Dose, Frequência, Desde
public string? Anticoagulantes { get; set; } // ComboBox: Não | Varfarina | DOAC | Antiagregante

// Alertas/contraindicações para osteopatia (multi-select):
public bool AlertaFraturasRecentes { get; set; }
public bool AlertaOsteoporoseSevera { get; set; }
public bool AlertaTumorInfecao { get; set; }
public bool AlertaSindromesNeurologicas { get; set; }
public bool AlertaAneurisma { get; set; }
public bool AlertaHipermobilidade { get; set; } // Ehlers-Danlos
public bool AlertaCirurgiaRecente { get; set; } // <3 meses
public string? AlertasOutros { get; set; } // TextBox
```

### **5️⃣ ALERGIAS E INTOLERÂNCIAS**
```csharp
// Alergias a fármacos (checkbox group):
public bool AlergiaPenicilina { get; set; } // β-lactâmicos
public bool AlergiaAINEs { get; set; }
public bool AlergiaOpioides { get; set; }
public bool AlergiaAnestesicos { get; set; } // locais
public bool AlergiaCorticosteroides { get; set; }
public bool AlergiaIodoContraste { get; set; }
public string? AlergiasFarmacosOutros { get; set; } // TextBox + "Sem alergias conhecidas"

// Alergias/Intolerâncias alimentares:
public bool IntoleranciaGluten { get; set; }
public bool IntoleranciaLactose { get; set; }
public bool IntoleranciaProteínaLeite { get; set; }
public bool IntoleranciaOvos { get; set; }
public bool IntoleranciaMarisco { get; set; }
public bool IntoleranciaFrutosSecos { get; set; } // Amendoim
public string? IntoleranciaAlimentarOutras { get; set; } // Sulfitos, Histamina + "Nenhuma"

// Alergias de contacto:
public bool AlergiaLatex { get; set; }
public bool AlergiaNiquel { get; set; }
public bool AlergiaCosmeticos { get; set; }
public bool AlergiaAdesivos { get; set; }
public bool AlergiaPlantas { get; set; }
public string? AlergiaContactoOutras { get; set; } // TextBox + "Nenhuma"

// História de anafilaxia:
public bool HistoriaAnafilaxia { get; set; } // Sim/Não
public string? AnafilaxiaGatilho { get; set; } // campo para gatilho se Sim
```

### **6️⃣ HISTÓRICO CIRÚRGICO E FRATURAS**
```csharp
public bool JaFezCirurgias { get; set; }
public string? HistoricoCirurgias { get; set; } // lista repetível: Procedimento, Lado, Data, Sequelas

public bool JaTeveFraturas { get; set; }
public string? HistoricoFraturas { get; set; } // lista repetível: Local, Data, Tratamento, Sequelas
```

### **7️⃣ ESTILO DE VIDA**
```csharp
public string? ExercicioFisico { get; set; } // ComboBox: Nenhum | 1-2x/semana | 3-4x/semana | ≥5x/semana | Atividade laboral intensa
public string? Tabagismo { get; set; } // Nunca fumou | Ex-fumador | Ocasional | Diário <10/dia | Diário ≥10/dia
public string? ConsumoAlcool { get; set; } // Nunca | Ocasional (≤1x/semana) | Moderado (2-4x/semana) | Frequente (≥5x/semana)
public int? ConsumoCafeina { get; set; } // 0 | 1 | 2 | 3 | 4 ou mais
public string? HorasSono { get; set; } // <5 | 5-6 | 7-8 | >8
public string? QualidadeSono { get; set; } // Ótima | Boa | Razoável | Má
public int? NivelStress { get; set; } // 0-10
public string? ProfissaoErgonomia { get; set; } // TextArea
public string? HabitosAlimentares { get; set; } // TextArea: padrão alimentar, nº refeições, água, restrições
```

### **8️⃣ DIGESTIVO / METABÓLICO**
```csharp
// Checkbox de sintomas:
public bool SintomaAzia { get; set; } // Refluxo
public bool SintomaDorAbdominal { get; set; }
public bool SintomaDistensao { get; set; } // Flatulência
public bool SintomaNauseas { get; set; }
public bool SintomaVomitos { get; set; }
public bool SintomaDiarreia { get; set; }
public bool SintomaObstipacao { get; set; }
public bool SintomaAlternancia { get; set; } // diarreia/obstipação
public bool SintomaFezesSangue { get; set; } // com muco/sangue
// + IntolerânciasAlimentares + Nenhum

public string? FrequenciaEvacuacao { get; set; } // vezes/semana: <3 | 3-5 | 6-8 | >8
public int? EscalaBristol { get; set; } // 1 a 7 (1=cíbalas, 4=salsicha lisa, 7=aquosa)
public string? DiagnosticosDigestivos { get; set; } // TextBox
```

### **9️⃣ CARDIO-RESPIRATÓRIO**
```csharp
public bool HipertensaoConhecida { get; set; } // Sim/Não
// Checkbox de sintomas:
public bool SintomaDorToracica { get; set; }
public bool SintomaPalpitacoes { get; set; }
public bool SintomaEdemas { get; set; }
public bool SintomaDispneia { get; set; }
public bool SintomaIntoleranciaEsforco { get; set; }
public bool SintomaTosseCronica { get; set; }
public bool SintomaPieiraAsma { get; set; }
public bool SintomaApneiaSono { get; set; }
// + Nenhum
```

### **🔟 MÚSCULO-ESQUELÉTICO**
```csharp
// Localização da dor (checkbox):
public bool DorCervical { get; set; }
public bool DorDorsal { get; set; }
public bool DorLombar { get; set; }
public bool DorSacroiliaca { get; set; }
public bool DorAnca { get; set; }
public bool DorJoelho { get; set; }
public bool DorTornozeloPe { get; set; }
public bool DorOmbro { get; set; }
public bool DorCotovelo { get; set; }
public bool DorPunhoMao { get; set; }
public bool DorATM { get; set; }
public string? DorOutrasLocalizacoes { get; set; }

// Padrões (checkbox):
public bool PadraoAguda { get; set; }
public bool PadraoCronica { get; set; } // >3m
public bool PadraoIrradiacao { get; set; }
public bool PadraoFormigueiros { get; set; }
public bool PadraoFraqueza { get; set; }
public bool PadraoRigidezMatinal { get; set; }
public bool PadraoBloqueios { get; set; }
public bool PadraoCaibras { get; set; }

// Bandeiras vermelhas (checkbox):
public bool BandeiraPeridaPeso { get; set; } // inexplicada
public bool BandeiraFebre { get; set; } // recente
public bool BandeiraTrauma { get; set; } // significativo
public bool BandeiraDeficesNeurologicos { get; set; } // progressivos
public bool BandeiraIncontinencia { get; set; } // Anestesia em sela
public bool BandeiraCancro { get; set; } // história de
public bool BandeiraCorticoides { get; set; } // uso prolongado
// + Nenhuma

public string? MovimentosLimitados { get; set; } // TextArea: Atividades
```

### **1️⃣1️⃣ NEUROLÓGICO / PSICOEMOCIONAL**
```csharp
// Checkbox:
public bool SintomaCefaleias { get; set; } // Enxaquecas
public bool SintomaTonturas { get; set; } // Vertigens
public bool SintomaDesmaios { get; set; }
public bool SintomaConvulsoes { get; set; }
public bool SintomaMemoria { get; set; } // Alterações de
public bool SintomaAnsiedade { get; set; }
public bool SintomaDepressao { get; set; }
public bool SintomaPanico { get; set; }
public bool SintomaIdeacaoSuicida { get; set; }
public bool AcompanhamentoPsicologico { get; set; } // psiquiátrico
// + Nenhum

public string? RoncoApneia { get; set; } // Não | Ronco | Apneia suspeita | Apneia diagnosticada
```

### **1️⃣2️⃣ DERMATOLÓGICO / IMUNOLÓGICO / ENDÓCRINO**
```csharp
// Checkbox:
public bool ProblemaEczema { get; set; } // Dermatite
public bool ProblemaPsoriase { get; set; }
public bool ProblemaUrticaria { get; set; } // Angioedema
public bool ProblemaAutoimune { get; set; }
public bool ProblemaHipoHipertiroidismo { get; set; }
public bool ProblemaDiabetes { get; set; }
public bool ProblemaSindromeMetabolica { get; set; }
// + Outras + Nenhuma
public string? ProblemasOutrosDetalhes { get; set; } // Campo para detalhes adicionais
```

### **1️⃣3️⃣ UROLÓGICO / GINECOLÓGICO**
```csharp
// Checkbox:
public bool ProblemaITURecorrentes { get; set; }
public bool ProblemaDorPelvica { get; set; }
public bool ProblemaIncontinencia { get; set; }
public bool ProblemaHiperplasiaProstatica { get; set; }
public bool ProblemaEndometriose { get; set; } // Mioma
public bool ProblemaSPM { get; set; } // Síndrome pré-menstrual
public bool ProblemaCicloIrregular { get; set; }
public bool ProblemaMenupausa { get; set; } // Sintomas vasomotores
public bool ProblemaInfertilidade { get; set; }
// + Nenhum
public string? CicloMenstrualDetalhes { get; set; } // Campo para detalhes de ciclo
```

### **1️⃣4️⃣ RISCOS E SEGURANÇA**
```csharp
public string? QuedasUltimoAno { get; set; } // Não | 1 queda | ≥2 quedas | Com lesão
public bool SegurancaCasaRelacoes { get; set; } // Sim | Não (prefiro falar em privado)

// Consumos de risco:
public bool ConsumoRiscoAlcool { get; set; }
public bool ConsumoRiscoTabaco { get; set; }
public bool ConsumoRiscoCannabis { get; set; }
public bool ConsumoRiscoOutras { get; set; } // substâncias
public bool ConsumoRiscoJogo { get; set; } // Outros
// + Nenhum
```

### **1️⃣5️⃣ DECLARAÇÕES FINAIS**
```csharp
// Checkbox obrigatórios:
public bool DeclaracaoVeracidade { get; set; } // Declaro que as respostas são verdadeiras e completas
public bool DeclaracaoComplementar { get; set; } // Compreendo que a intervenção é complementar e não substitui diagnóstico médico

// Opcional:
public bool AutorizacaoPartilha { get; set; } // Autorizo partilha mínima de informação com outros profissionais
public bool AutorizacaoContactos { get; set; } // Autorizo contactos para marcações e recomendações

// Campos para:
public string? LocalData { get; set; }
public bool AssinaturaUtente { get; set; }
public bool AssinaturaProfissional { get; set; }
```

---

## 🔒 **16. CONSENTIMENTOS INFORMADOS**

### **🌿 NATUROPATIA**
- ✅ Compreensão da natureza das intervenções (alimentação, fitoterapia, suplementação)
- ✅ Objetivos e benefícios
- ✅ Riscos potenciais
- ✅ Informação de patologias/medicação
- ✅ Alternativas discutidas
- ✅ Autorização e corresponsabilidade
- ✅ Revogável a qualquer momento

### **🔧 OSTEOPATIA**
- ✅ Técnicas manuais explicadas
- ✅ Contraindicações e sinais de alerta discutidos
- ✅ Riscos explicados (dor, tonturas, hematomas, raríssimos eventos graves)
- ✅ Autorizo contacto físico
- ✅ Posso interromper a técnica a qualquer momento
- ✅ Revogável a qualquer momento

### **👁️ IRIDOLOGIA**
- ✅ Natureza não invasiva e sem valor diagnóstico médico
- ✅ Autorizo captura e armazenamento de imagens
- ✅ Compreendo limitações e necessidade de correlação clínica
- ✅ Revogável a qualquer momento

### **⚡ MEDICINA QUÂNTICA (Informacional)**
- ✅ Abordagem complementar, sem finalidade diagnóstica reconhecida
- ✅ Natureza dos procedimentos (bases de dados, frequências, aconselhamento)
- ✅ Possíveis reações transitórias
- ✅ Contraindicações discutidas
- ✅ Liberdade de interromper a qualquer momento
- ✅ Revogável a qualquer momento

### **🛡️ RGPD**
- ✅ Informação de responsável, finalidades, conservação e destinatários
- ✅ Direitos de acesso, retificação, limitação, oposição, portabilidade e eliminação
- ✅ Consentimento explícito para tratamento de dados de saúde
- ✅ Opção de não receber marketing
- ✅ Revogável a qualquer momento

---

## 🚀 **INTEGRAÇÃO TÉCNICA NO BIODESK PRO**

### **✅ COMPONENTES JÁ IMPLEMENTADOS:**
1. **Modelo expandido:** `QuestionarioSaude.cs` com 200+ campos
2. **Template HTML completo:** `questionario-saude-completo.html`
3. **Consentimentos modulares:** ficheiros HTML por terapia
4. **Sistema de assinaturas:** `AssinaturaDigital.cs` funcional
5. **Base de dados:** DbContext configurado e migrations

### **🎯 PRÓXIMOS PASSOS:**
1. **Atualizar ViewModels** para usar o novo modelo expandido
2. **Criar interface WPF** com TabControl/Expander para as 16 secções
3. **Implementar binding** bi-direcional com ObservableCollections
4. **Integrar sistema de consentimentos** na interface principal
5. **Adicionar validação** e save/load automático

### **🏗️ ARQUITETURA SUGERIDA:**
```
FichaPacienteView (Aba "Saúde & Consentimentos")
├── QuestionarioCompletoView (16 secções expansíveis)
│   ├── SecaoDadosUtente (auto-preenchida)
│   ├── SecaoQueixaPrincipal (TextArea + CheckBoxes)
│   ├── SecaoEstadoGeral (ComboBoxes + CheckBoxes)
│   ├── ...
│   └── SecaoDeclaracoes (CheckBoxes obrigatórios)
└── ConsentimentosView (Multi-select terapias)
    ├── ConsentimentoNaturopatia
    ├── ConsentimentoOsteopatia
    ├── ConsentimentoIridologia
    ├── ConsentimentoMedicinaQuantica
    └── ConsentimentoRGPD
```

---

## 📊 **RESUMO FINAL:**

**✅ ELIMINADAS DUPLICAÇÕES:**
- Modelo QuestionarioSaude consolidado e expandido
- TipoTerapiaEnum atualizado
- Estrutura de dados unificada

**🎯 FUNCIONALIDADES AMPLIADAS:**
- 200+ campos estruturados para recolha exaustiva
- 16 secções organizadas por sistema/área
- 5 tipos de consentimentos integrados
- Proteção legal completa

**🚀 PRONTO PARA IMPLEMENTAÇÃO:**
- Interface WPF com binding otimizado
- Sistema de persistência robusto
- Templates HTML para impressão/PDF
- Assinaturas digitais funcionais

Este sistema garante **recolha exaustiva**, **proteção profissional** e **experiência de utilizador** otimizada! 🎉