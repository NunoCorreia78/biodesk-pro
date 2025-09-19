using System;
using Microsoft.Data.Sqlite;

class Program
{
    static void Main()
    {
        var connectionString = @"Data Source=..\Data\BioDesk_Pacientes.db";
        
        try
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            
            Console.WriteLine("=== 🏥 BASE DE DADOS DO BIODESK PRO ===");
            
            // Mostrar tabelas
            var tablesCommand = connection.CreateCommand();
            tablesCommand.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
            
            using var tablesReader = tablesCommand.ExecuteReader();
            Console.WriteLine("\n📋 TABELAS:");
            while (tablesReader.Read())
            {
                Console.WriteLine($"  - {tablesReader.GetString(0)}");
            }
            
            // Mostrar esquema da tabela Pacientes
            Console.WriteLine("\n🏗️ ESQUEMA DA TABELA 'Pacientes':");
            var schemaCommand = connection.CreateCommand();
            schemaCommand.CommandText = "PRAGMA table_info(Pacientes);";
            
            using var schemaReader = schemaCommand.ExecuteReader();
            Console.WriteLine("ID | Nome Campo    | Tipo         | NotNull | Default | PK");
            Console.WriteLine("---|---------------|--------------|---------|---------|---");
            while (schemaReader.Read())
            {
                Console.WriteLine($"{schemaReader.GetInt32(0),2} | {schemaReader.GetString(1),-13} | {schemaReader.GetString(2),-12} | {(schemaReader.GetInt32(3) == 1 ? "Sim" : "Não"),7} | {schemaReader.GetValue(4)??"NULL",-7} | {(schemaReader.GetInt32(5) == 1 ? "Sim" : "Não"),2}");
            }
            
            // Contar registos
            Console.WriteLine("\n📊 DADOS:");
            var countCommand = connection.CreateCommand();
            countCommand.CommandText = "SELECT COUNT(*) FROM Pacientes;";
            var count = countCommand.ExecuteScalar();
            Console.WriteLine($"Total de pacientes: {count}");
            
            // Mostrar registos (se existirem)
            if (Convert.ToInt32(count) > 0)
            {
                Console.WriteLine("\n👥 PACIENTES REGISTADOS:");
                var patientsCommand = connection.CreateCommand();
                patientsCommand.CommandText = @"
                    SELECT Id, NomeCompleto, DataNascimento, Genero, Email, Telefone, DataCriacao 
                    FROM Pacientes 
                    WHERE Ativo = 1
                    ORDER BY Id;";
                
                using var patientsReader = patientsCommand.ExecuteReader();
                Console.WriteLine("ID | Nome                          | Nascimento | Género    | Email                     | Telefone   | Criado");
                Console.WriteLine("---|-------------------------------|------------|-----------|---------------------------|------------|------------------");
                
                while (patientsReader.Read())
                {
                    var id = patientsReader.GetInt32(0);
                    var nome = patientsReader.IsDBNull(1) ? "NULL" : patientsReader.GetString(1);
                    var nascimento = patientsReader.IsDBNull(2) ? "NULL" : patientsReader.GetDateTime(2).ToString("dd/MM/yyyy");
                    var genero = patientsReader.IsDBNull(3) ? "NULL" : patientsReader.GetString(3);
                    var email = patientsReader.IsDBNull(4) ? "NULL" : patientsReader.GetString(4);
                    var telefone = patientsReader.IsDBNull(5) ? "NULL" : patientsReader.GetString(5);
                    var criado = patientsReader.IsDBNull(6) ? "NULL" : patientsReader.GetDateTime(6).ToString("dd/MM/yyyy HH:mm");
                    
                    // Truncar nome se for muito longo
                    if (nome.Length > 29) nome = nome.Substring(0, 26) + "...";
                    if (email != "NULL" && email.Length > 25) email = email.Substring(0, 22) + "...";
                    
                    Console.WriteLine($"{id,2} | {nome,-29} | {nascimento,-10} | {genero,-9} | {email,-25} | {telefone,-10} | {criado}");
                }
            }
            
            // Mostrar índices
            Console.WriteLine("\n🔍 ÍNDICES:");
            var indexCommand = connection.CreateCommand();
            indexCommand.CommandText = "SELECT name, sql FROM sqlite_master WHERE type='index' AND name NOT LIKE 'sqlite_%';";
            
            using var indexReader = indexCommand.ExecuteReader();
            while (indexReader.Read())
            {
                Console.WriteLine($"  - {indexReader.GetString(0)}");
                if (!indexReader.IsDBNull(1))
                    Console.WriteLine($"    SQL: {indexReader.GetString(1)}");
            }
            
            Console.WriteLine("\n✅ Consulta concluída com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}
