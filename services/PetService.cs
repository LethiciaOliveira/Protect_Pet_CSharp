using Database;
using Npgsql;

namespace Services
{
    public class PetService
    {
        public static void CadastrarPet(int tutorId)
        {
            Console.Write("Nome do pet: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Espécie: ");
            string especie = Console.ReadLine() ?? "";

            Console.Write("Raça: ");
            string raca = Console.ReadLine() ?? "";

            Console.Write("Sexo: ");
            string sexo = Console.ReadLine() ?? "";

            Console.Write("Data de nascimento (AAAA-MM-DD): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine() ?? "");

            Console.WriteLine("Tipo de coleira:");
            Console.WriteLine("1 - Basica");
            Console.WriteLine("2 - Premium");
            Console.WriteLine("Enter - Nenhuma");

            string entradaColeira = Console.ReadLine() ?? "";

            int? coleiraId = null;

            if (entradaColeira == "1")
                coleiraId = 1;

            else if (entradaColeira == "2")
                coleiraId = 2;

            using var conn = Conexao.ObterConexao();

            string sql = @"
                INSERT INTO pets
                (nome, especie, raca, sexo, data_nascimento, tutor_id, coleira_id)

                VALUES
                (@nome, @especie, @raca, @sexo, @data, @tutor, @coleira)
            ";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@especie", especie);
            cmd.Parameters.AddWithValue("@raca", raca);
            cmd.Parameters.AddWithValue("@sexo", sexo);
            cmd.Parameters.AddWithValue("@data", dataNascimento);
            cmd.Parameters.AddWithValue("@tutor", tutorId);

            if (coleiraId == null)
                cmd.Parameters.AddWithValue("@coleira", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@coleira", coleiraId);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Pet cadastrado com sucesso!");
        }

        public static void ListarPets(int tutorId)
        {
            using var conn = Conexao.ObterConexao();

            string sql = @"
                SELECT
                    p.id,
                    p.nome,
                    p.especie,
                    p.raca,
                    p.sexo,
                    c.tipo

                FROM pets p

                LEFT JOIN coleiras c
                    ON p.coleira_id = c.id

                WHERE p.tutor_id = @tutor
            ";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@tutor", tutorId);

            using var reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== SEUS PETS ===");

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID:{reader["id"]} | " +
                    $"Nome:{reader["nome"]} | " +
                    $"Espécie:{reader["especie"]} | " +
                    $"Raça:{reader["raca"]} | " +
                    $"Sexo:{reader["sexo"]} | " +
                    $"Coleira:{reader["tipo"]}"
                );
            }
        }

        public static void ExcluirPet(int tutorId)
        {
            Console.Write("ID do pet para excluir: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            using var conn = Conexao.ObterConexao();

            string verificar = @"
                SELECT COUNT(*)
                FROM pets
                WHERE id = @id
                AND tutor_id = @tutor
            ";

            using var cmdVer = new NpgsqlCommand(verificar, conn);

            cmdVer.Parameters.AddWithValue("@id", id);
            cmdVer.Parameters.AddWithValue("@tutor", tutorId);

            int existe = Convert.ToInt32(cmdVer.ExecuteScalar());

            if (existe == 0)
            {
                Console.WriteLine("ID inválido ou pet não pertence a você.");
                return;
            }

            string sql = "DELETE FROM pets WHERE id = @id";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Pet excluído com sucesso!");
        }

        public static void AlterarPet(int tutorId)
        {
            Console.Write("ID do pet para alterar: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            using var conn = Conexao.ObterConexao();

            string verificar = @"
                SELECT COUNT(*)
                FROM pets
                WHERE id = @id
                AND tutor_id = @tutor
            ";

            using var cmdVer = new NpgsqlCommand(verificar, conn);

            cmdVer.Parameters.AddWithValue("@id", id);
            cmdVer.Parameters.AddWithValue("@tutor", tutorId);

            int existe = Convert.ToInt32(cmdVer.ExecuteScalar());

            if (existe == 0)
            {
                Console.WriteLine("Pet não encontrado.");
                return;
            }

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Nova espécie: ");
            string especie = Console.ReadLine() ?? "";

            Console.Write("Nova raça: ");
            string raca = Console.ReadLine() ?? "";

            Console.Write("Novo sexo: ");
            string sexo = Console.ReadLine() ?? "";

            string sql = @"
                UPDATE pets
                SET
                    nome = @nome,
                    especie = @especie,
                    raca = @raca,
                    sexo = @sexo
                WHERE id = @id
            ";

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@especie", especie);
            cmd.Parameters.AddWithValue("@raca", raca);
            cmd.Parameters.AddWithValue("@sexo", sexo);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Pet alterado com sucesso!");
        }
    }
}
