// Armazenamento em memória dos usuários: username -> hash SHA-256 da senha
// Persiste durante toda a execução do programa
var usuarios = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Exercícios de Segurança e Criptografia ===");
    Console.WriteLine();
    Console.WriteLine("Selecione um exercício:");
    Console.WriteLine("  1 - Exercício 1: Conversão Base64");
    Console.WriteLine("  2 - Exercício 2: Cifra de César");
    Console.WriteLine("  3 - Exercício 3: Substituição Monoalfabética");
    Console.WriteLine("  4 - Exercício 4: Cadastro e Autenticação com Hash");
    Console.WriteLine("  0 - Sair");
    Console.WriteLine();
    Console.Write("Opção: ");

    string? input = Console.ReadLine();
    Console.WriteLine();

    switch (input)
    {
        case "1":
            Exercicio1();
            break;
        case "2":
            Exercicio2();
            break;
        case "3":
            Exercicio3();
            break;
        case "4":
            // Passa o dicionário por referência para persistir entre chamadas
            Exercicio4(usuarios);
            break;
        case "0":
            Console.WriteLine("Saindo...");
            return;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
}

// ─────────────────────────────────────────────────────────────────────────────
// Exercício 1 — Conversão Base64
// ─────────────────────────────────────────────────────────────────────────────
static void Exercicio1()
{
    Console.WriteLine("--- Exercício 1: Conversão Base64 ---");
    Console.WriteLine();
    Console.WriteLine("O que deseja fazer?");
    Console.WriteLine("  1 - Texto claro -> Base64");
    Console.WriteLine("  2 - Base64 -> Texto claro");
    Console.Write("Opção: ");

    string? opcao = Console.ReadLine();
    Console.WriteLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Digite o texto claro: ");
            string textoClaro = Console.ReadLine() ?? string.Empty;
            string base64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(textoClaro));
            Console.WriteLine();
            Console.WriteLine($"Resultado em Base64: {base64}");
            break;

        case "2":
            Console.Write("Digite a string Base64: ");
            string stringBase64 = Console.ReadLine() ?? string.Empty;
            try
            {
                string textoDecode = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(stringBase64));
                Console.WriteLine();
                Console.WriteLine($"Texto decodificado: {textoDecode}");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Erro: a string informada não é um Base64 válido.");
            }
            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

// ─────────────────────────────────────────────────────────────────────────────
// Exercício 2 — Cifra de César
// ─────────────────────────────────────────────────────────────────────────────
static void Exercicio2()
{
    Console.WriteLine("--- Exercício 2: Cifra de César ---");
    Console.WriteLine();
    Console.WriteLine("O que deseja fazer?");
    Console.WriteLine("  1 - Cifrar mensagem");
    Console.WriteLine("  2 - Decifrar mensagem");
    Console.Write("Opção: ");

    string? opcao = Console.ReadLine();
    Console.WriteLine();

    if (opcao != "1" && opcao != "2")
    {
        Console.WriteLine("Opção inválida.");
        return;
    }

    Console.Write("Digite o texto: ");
    string texto = Console.ReadLine() ?? string.Empty;

    Console.Write("Digite o deslocamento (k): ");
    if (!int.TryParse(Console.ReadLine(), out int k))
    {
        Console.WriteLine("Deslocamento inválido. Informe um número inteiro.");
        return;
    }

    string resultado = AplicarCifraCesar(texto, opcao == "1" ? k : -k);
    Console.WriteLine();
    Console.WriteLine(opcao == "1"
        ? $"Texto cifrado:   {resultado}"
        : $"Texto decifrado: {resultado}");
}

static string AplicarCifraCesar(string texto, int deslocamento)
{
    var sb = new System.Text.StringBuilder(texto.Length);

    foreach (char c in texto)
    {
        if (char.IsLetter(c))
        {
            char baseChar = char.IsUpper(c) ? 'A' : 'a';
            // (+26) garante resultado positivo mesmo com deslocamento negativo
            char cifrado = (char)(baseChar + ((c - baseChar + deslocamento) % 26 + 26) % 26);
            sb.Append(cifrado);
        }
        else
        {
            sb.Append(c); // preserva espaços, pontuação, números
        }
    }

    return sb.ToString();
}

// ─────────────────────────────────────────────────────────────────────────────
// Exercício 3 — Substituição Monoalfabética
// ─────────────────────────────────────────────────────────────────────────────
static void Exercicio3()
{
    Console.WriteLine("--- Exercício 3: Substituição Monoalfabética ---");
    Console.WriteLine();
    Console.WriteLine("O que deseja fazer?");
    Console.WriteLine("  1 - Cifrar mensagem");
    Console.WriteLine("  2 - Decifrar mensagem");
    Console.Write("Opção: ");

    string? opcao = Console.ReadLine();
    Console.WriteLine();

    if (opcao != "1" && opcao != "2")
    {
        Console.WriteLine("Opção inválida.");
        return;
    }

    Console.WriteLine("Digite o alfabeto chave (26 letras únicas, ex: ZEBRASCDFGHIJKLMNOPQTUVWXY):");
    Console.Write("Chave: ");
    string chave = (Console.ReadLine() ?? string.Empty).ToUpper().Trim();

    if (!ValidarChaveMonoalfabetica(chave, out string erroChave))
    {
        Console.WriteLine($"\nChave inválida: {erroChave}");
        return;
    }

    Console.Write("\nDigite o texto: ");
    string texto = Console.ReadLine() ?? string.Empty;

    string resultado = opcao == "1"
        ? CifrarMonoalfabetico(texto, chave)
        : DecifrarMonoalfabetico(texto, chave);

    Console.WriteLine();
    Console.WriteLine(opcao == "1"
        ? $"Texto cifrado:   {resultado}"
        : $"Texto decifrado: {resultado}");
}

static bool ValidarChaveMonoalfabetica(string chave, out string erro)
{
    if (chave.Length != 26)
    {
        erro = $"A chave deve ter exatamente 26 letras (encontrado: {chave.Length}).";
        return false;
    }

    if (!chave.All(char.IsLetter))
    {
        erro = "A chave deve conter apenas letras.";
        return false;
    }

    var duplicadas = chave.GroupBy(c => c).Where(g => g.Count() > 1).Select(g => g.Key);
    if (duplicadas.Any())
    {
        erro = $"A chave possui letras duplicadas: {string.Join(", ", duplicadas)}";
        return false;
    }

    erro = string.Empty;
    return true;
}

static string CifrarMonoalfabetico(string texto, string chave)
{
    var sb = new System.Text.StringBuilder(texto.Length);

    foreach (char c in texto)
    {
        if (char.IsLetter(c))
        {
            bool maiusculo = char.IsUpper(c);
            int indice = char.ToUpper(c) - 'A';
            char substituto = chave[indice];
            sb.Append(maiusculo ? char.ToUpper(substituto) : char.ToLower(substituto));
        }
        else
        {
            sb.Append(c);
        }
    }

    return sb.ToString();
}

static string DecifrarMonoalfabetico(string texto, string chave)
{
    var sb = new System.Text.StringBuilder(texto.Length);

    foreach (char c in texto)
    {
        if (char.IsLetter(c))
        {
            bool maiusculo = char.IsUpper(c);
            int indice = chave.IndexOf(char.ToUpper(c));
            char original = (char)('A' + indice);
            sb.Append(maiusculo ? char.ToUpper(original) : char.ToLower(original));
        }
        else
        {
            sb.Append(c);
        }
    }

    return sb.ToString();
}

// ─────────────────────────────────────────────────────────────────────────────
// Exercício 4 — Cadastro e Autenticação com Hash SHA-256
// ─────────────────────────────────────────────────────────────────────────────
static void Exercicio4(Dictionary<string, string> usuarios)
{
    Console.WriteLine("--- Exercício 4: Cadastro e Autenticação com Hash ---");
    Console.WriteLine();
    Console.WriteLine("O que deseja fazer?");
    Console.WriteLine("  1 - Cadastrar usuário");
    Console.WriteLine("  2 - Autenticar usuário");
    Console.Write("Opção: ");

    string? opcao = Console.ReadLine();
    Console.WriteLine();

    switch (opcao)
    {
        case "1":
            CadastrarUsuario(usuarios);
            break;
        case "2":
            AutenticarUsuario(usuarios);
            break;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

static void CadastrarUsuario(Dictionary<string, string> usuarios)
{
    Console.Write("Nome de usuário: ");
    string username = (Console.ReadLine() ?? string.Empty).Trim();

    if (string.IsNullOrEmpty(username))
    {
        Console.WriteLine("Nome de usuário não pode ser vazio.");
        return;
    }

    if (usuarios.ContainsKey(username))
    {
        Console.WriteLine($"Usuário '{username}' já existe.");
        return;
    }

    Console.Write("Senha: ");
    string senha = LerSenha();

    if (string.IsNullOrEmpty(senha))
    {
        Console.WriteLine("\nA senha não pode ser vazia.");
        return;
    }

    string hashSenha = CalcularHashSHA256(senha);
    usuarios[username] = hashSenha;

    Console.WriteLine($"\nUsuário '{username}' cadastrado com sucesso.");
    Console.WriteLine($"Hash SHA-256 armazenado: {hashSenha}");
}

static void AutenticarUsuario(Dictionary<string, string> usuarios)
{
    if (usuarios.Count == 0)
    {
        Console.WriteLine("Nenhum usuário cadastrado ainda. Faça o cadastro primeiro.");
        return;
    }

    Console.Write("Nome de usuário: ");
    string username = (Console.ReadLine() ?? string.Empty).Trim();

    Console.Write("Senha: ");
    string senha = LerSenha();
    Console.WriteLine();

    if (!usuarios.TryGetValue(username, out string? hashArmazenado))
    {
        Console.WriteLine("\nAcesso negado: usuário não encontrado.");
        return;
    }

    string hashInformado = CalcularHashSHA256(senha);

    Console.WriteLine($"\nHash informado:   {hashInformado}");
    Console.WriteLine($"Hash armazenado:  {hashArmazenado}");
    Console.WriteLine();

    if (hashInformado == hashArmazenado)
        Console.WriteLine("Acesso PERMITIDO. Autenticação bem-sucedida!");
    else
        Console.WriteLine("Acesso NEGADO. Senha incorreta.");
}

static string CalcularHashSHA256(string texto)
{
    byte[] bytes = System.Security.Cryptography.SHA256.HashData(
        System.Text.Encoding.UTF8.GetBytes(texto));
    return Convert.ToHexString(bytes).ToLower();
}

// Lê a senha sem exibi-la no terminal (exibe '*' para cada caractere digitado)
static string LerSenha()
{
    var sb = new System.Text.StringBuilder();
    ConsoleKeyInfo tecla;

    do
    {
        tecla = Console.ReadKey(intercept: true);

        if (tecla.Key == ConsoleKey.Backspace && sb.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
            Console.Write("\b \b");
        }
        else if (tecla.Key != ConsoleKey.Enter && tecla.Key != ConsoleKey.Backspace)
        {
            sb.Append(tecla.KeyChar);
            Console.Write('*');
        }
    }
    while (tecla.Key != ConsoleKey.Enter);

    return sb.ToString();
}
