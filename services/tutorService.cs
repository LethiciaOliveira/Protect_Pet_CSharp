using Database;
using Npgsql;

namespace Services
{
    public class TutorService
    {
        public static void AlterarTutor(int tutorId)
        {
            using var conn = Conexao.ObterConexao();

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Novo sobrenome: ");
            string sobrenome = Console.ReadLine() ?? "";

            Console.Write("Novo telefone: ");
            string telefone = Console.ReadLine() ?? "";

            Console.Write("Novo email: ");
            string email = Console.ReadLine() ?? "";

            string sql = @"
                UPDATE tutores
                SET
                    nome = @nome,
                    sobrenome = @sobrenome,
                    telefone = @telefone,
                    email = @email
                WHERE id = @id
            ";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@sobrenome", sobrenome);
            cmd.Parameters.AddWithValue("@telefone", telefone);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@id", tutorId);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Tutor alterado com sucesso!");
        }

        public static void MostrarDadosTutor(int tutorId)
        {
            using var conn = Conexao.ObterConexao();

            string sql = @"
                SELECT
                    nome,
                    sobrenome,
                    cpf,
                    telefone,
                    email
                FROM tutores
                WHERE id = @id
            ";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", tutorId);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine("\n===== MEUS DADOS =====");

                Console.WriteLine($"Nome: {reader["nome"]}");
                Console.WriteLine($"Sobrenome: {reader["sobrenome"]}");
                Console.WriteLine($"CPF: {reader["cpf"]}");
                Console.WriteLine($"Telefone: {reader["telefone"]}");
                Console.WriteLine($"Email: {reader["email"]}");
            }
            else
            {
                Console.WriteLine("Tutor não encontrado.");
            }
        }
    }
}