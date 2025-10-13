# Por que usar Dapper com SQL Raw no CineFlow?

## Contexto

Este projeto utiliza **Dapper** como micro-ORM e **SQL raw** para todas as operações de banco de dados. Esta decisão arquitetural foi tomada deliberadamente para maximizar o aprendizado de SQL e aplicar os conceitos estudados no livro "Learning SQL" da O'Reilly.

Claramente no mercado de trabalho o normal é se utilizar o EF por ser um ORM robusto, o dapper não é muito utilizado atualmente devido a alta carga do EF ter sido reduzida e sempre ser atualizado. Porém por questões de CONSOLIDAR aprendizado decidi utilizar o Dapper

---

## Vantagens do Dapper

### 1. Performance Superior

Dapper é conhecido como o "King of Micro ORMs" por sua performance excepcional:

- **Mapeamento direto**: Converte `DataReader` diretamente para objetos C# com overhead mínimo
- **Sem proxy classes**: Não gera classes intermediárias como ORMs pesados (Entity Framework)
- **Compiled queries**: Queries são compiladas e cacheadas internamente
- **Benchmarks**: Em testes, Dapper é apenas 5-10% mais lento que ADO.NET puro, mas 50-100% mais rápido que Entity Framework em cenários de leitura intensiva

Comparação de performance (100.000 consultas):
```
ADO.NET Raw:        ~500ms
Dapper:             ~525ms (+5%)
Entity Framework:   ~1200ms (+140%)
```

### 2. Controle Total sobre SQL

Diferente de ORMs completos que geram SQL automaticamente, com Dapper você:

- **Escreve SQL manualmente**: Controle total sobre cada query
- **Otimiza índices**: Sabe exatamente quais colunas são usadas
- **Evita N+1 queries**: Usa JOINs explícitos em vez de lazy loading
- **Utiliza recursos PostgreSQL**: CTEs, window functions, arrays, JSONB, etc.

Exemplo de query otimizada que seria difícil com ORM tradicional:
```sql
WITH sessoes_proximas AS (
    SELECT 
        s.*,
        COUNT(a.id_assento) FILTER (WHERE a.status = 'livre') as assentos_disponiveis
    FROM sessao s
    INNER JOIN sala sa ON s.id_sala = sa.id_sala
    LEFT JOIN assento a ON sa.id_sala = a.id_sala
    WHERE s.horario_inicio BETWEEN NOW() AND NOW() + INTERVAL '7 days'
    GROUP BY s.id_sessao
)
SELECT * FROM sessoes_proximas WHERE assentos_disponiveis > 0;
```

### 3. Transparência e Debugging

Com SQL raw:

- **Logs legíveis**: Vê exatamente o SQL executado no banco
- **Debugging simples**: Copia o SQL e testa diretamente no pgAdmin
- **Profiling fácil**: Usa `EXPLAIN ANALYZE` para otimizar queries
- **Sem surpresas**: Não há "magia" gerando SQL inesperado

### 4. Portabilidade

- **Menos acoplamento**: Trocar de banco requer apenas reescrever SQL, não refatorar toda aplicação
- **Migrations simples**: Scripts SQL puro são universalmente compreendidos
- **Versionamento claro**: Arquivos `.sql` são fáceis de versionar e revisar

---

## Por que NÃO usar Entity Framework?

### Problemas típicos de ORMs pesados:

#### 1. Abstração excessiva oculta problemas
```csharp
// Entity Framework - parece simples, mas...
var filmes = await _context.Filmes
    .Include(f => f.Sessoes)
    .ThenInclude(s => s.Sala)
    .ToListAsync();

// Gera SQL complexo e potencialmente ineficiente
// Você não tem controle sobre JOINs, ORDER BY, etc.
```

#### 2. N+1 Query Problem
```csharp
// Código aparentemente inocente:
foreach (var filme in filmes)
{
    // Cada iteração faz UMA query adicional!
    Console.WriteLine(filme.Sessoes.Count);
}
// Total: 1 query inicial + N queries = péssima performance
```

#### 3. Dificuldade com queries complexas
```csharp
// Tentar fazer agregações complexas no EF:
var resultado = await _context.Reservas
    .Where(r => r.Status == "paga")
    .GroupBy(r => new { r.Sessao.IdFilme, r.DataReserva.Date })
    .Select(g => new { 
        Filme = g.Key.IdFilme,
        Data = g.Key.Date,
        Total = g.Sum(x => x.ValorTotal)
    })
    .ToListAsync();

// Difícil de ler, difícil de debugar, SQL gerado é complexo
```

#### 4. Overhead de tracking
```csharp
// Entity Framework rastreia TODAS as entidades por padrão
var filmes = await _context.Filmes.ToListAsync();
// Consome memória e processamento para detectar mudanças
// Mesmo que você só esteja lendo dados!
```

---

## Vantagens Pedagógicas: Aplicando "Learning SQL"

### 1. Prática direta de conceitos do livro

O livro "Learning SQL" ensina conceitos fundamentais que são aplicados diretamente no código:

#### Capítulo 3: Query Primer
```csharp
// Exemplo direto do livro aplicado no projeto
public async Task<List> GetFilmesEmCartaz()
{
    return await _db.QueryAsync(@"
        SELECT 
            id_filme,
            nome_filme,
            genero,
            duracao,
            classificacao_etaria
        FROM filme
        WHERE data_lancamento <= CURRENT_DATE
          AND (data_fim_cartaz IS NULL OR data_fim_cartaz >= CURRENT_DATE)
        ORDER BY nome_filme"
    );
}
```

#### Capítulo 5: Joining Tables
```csharp
// Aplicando JOINs conforme ensinado no livro
public async Task<List> GetSessoesComDetalhes(int idFilme)
{
    return await _db.QueryAsync(@"
        SELECT 
            s.id_sessao,
            s.horario_inicio,
            f.nome_filme,
            sa.nome as nome_sala,
            sa.tipo_sala,
            COUNT(a.id_assento) FILTER (WHERE a.status = 'livre') as assentos_disponiveis
        FROM sessao s
        INNER JOIN filme f ON s.id_filme = f.id_filme
        INNER JOIN sala sa ON s.id_sala = sa.id_sala
        LEFT JOIN assento a ON sa.id_sala = a.id_sala
        WHERE s.id_filme = @IdFilme
          AND s.horario_inicio > NOW()
        GROUP BY s.id_sessao, f.nome_filme, sa.nome, sa.tipo_sala
        ORDER BY s.horario_inicio",
        new { IdFilme = idFilme }
    );
}
```

#### Capítulo 8: Grouping and Aggregates
```csharp
// Agregações como no livro
public async Task GetRelatorioVendas(DateTime dataInicio, DateTime dataFim)
{
    return await _db.QueryAsync(@"
        SELECT 
            DATE(r.data_reserva) as data,
            COUNT(*) as total_reservas,
            SUM(r.valor_total) as receita_total,
            AVG(r.valor_total) as ticket_medio
        FROM reserva r
        WHERE r.status = 'paga'
          AND r.data_reserva BETWEEN @DataInicio AND @DataFim
        GROUP BY DATE(r.data_reserva)
        HAVING SUM(r.valor_total) > 0
        ORDER BY data DESC",
        new { DataInicio = dataInicio, DataFim = dataFim }
    );
}
```

#### Capítulo 9: Subqueries
```csharp
// Subqueries conforme ensinado
public async Task<List> GetFilmesMaisAvaliados(int limit)
{
    return await _db.QueryAsync(@"
        SELECT 
            f.*,
            (SELECT COUNT(*) FROM avaliacao WHERE id_filme = f.id_filme) as num_avaliacoes,
            (SELECT AVG(nota) FROM avaliacao WHERE id_filme = f.id_filme) as media_avaliacao
        FROM filme f
        WHERE (
            SELECT COUNT(*) 
            FROM avaliacao 
            WHERE id_filme = f.id_filme
        ) >= 10
        ORDER BY media_avaliacao DESC
        LIMIT @Limit",
        new { Limit = limit }
    );
}
```

#### Capítulo 11: Conditional Logic
```csharp
// CASE statements
public async Task<List> GetFilmesPorClassificacao()
{
    return await _db.QueryAsync(@"
        SELECT 
            nome_filme,
            classificacao_etaria,
            CASE classificacao_etaria
                WHEN 'L' THEN 'Livre para todos'
                WHEN '10' THEN 'Não recomendado para menores de 10 anos'
                WHEN '12' THEN 'Não recomendado para menores de 12 anos'
                WHEN '14' THEN 'Não recomendado para menores de 14 anos'
                WHEN '16' THEN 'Não recomendado para menores de 16 anos'
                WHEN '18' THEN 'Não recomendado para menores de 18 anos'
                ELSE 'Classificação desconhecida'
            END as descricao_classificacao
        FROM filme
        ORDER BY classificacao_etaria"
    );
}
```

#### Capítulo 12: Transactions
```csharp
// Transações conforme o livro
public async Task CriarReservaComTransacao(ReservaDto dto)
{
    using var transaction = await _db.BeginTransactionAsync();
    
    try
    {
        // 1. Criar reserva
        var idReserva = await _db.QueryFirstAsync(@"
            INSERT INTO reserva (cpf_cliente, status, valor_total, data_reserva)
            VALUES (@CpfCliente, 'pendente', @ValorTotal, NOW())
            RETURNING id_reserva",
            dto,
            transaction
        );

        // 2. Marcar assentos como reservados
        await _db.ExecuteAsync(@"
            UPDATE assento 
            SET status = 'reservado'
            WHERE id_assento = ANY(@Assentos)
              AND status = 'livre'",
            new { Assentos = dto.Assentos },
            transaction
        );

        // 3. Verificar se todos assentos foram reservados
        var assentosReservados = await _db.QueryFirstAsync(@"
            SELECT COUNT(*) 
            FROM assento 
            WHERE id_assento = ANY(@Assentos) 
              AND status = 'reservado'",
            new { Assentos = dto.Assentos },
            transaction
        );

        if (assentosReservados != dto.Assentos.Count)
        {
            await transaction.RollbackAsync();
            throw new Exception("Alguns assentos não estão disponíveis");
        }

        await transaction.CommitAsync();
        return idReserva;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}
```

### 2. Compreensão profunda do banco de dados

Escrever SQL manualmente força você a:

- Entender como índices funcionam
- Conhecer planos de execução (EXPLAIN)
- Identificar gargalos de performance
- Otimizar queries com base em dados reais

Exemplo de análise de performance:
```sql
EXPLAIN ANALYZE
SELECT 
    f.nome_filme,
    COUNT(r.id_reserva) as total_reservas
FROM filme f
LEFT JOIN sessao s ON f.id_filme = s.id_filme
LEFT JOIN reserva r ON s.id_sessao = r.id_sessao
WHERE r.status = 'paga'
  AND r.data_reserva >= CURRENT_DATE - INTERVAL '30 days'
GROUP BY f.id_filme, f.nome_filme
ORDER BY total_reservas DESC;

-- Resultado mostra:
-- Seq Scan on reserva  (cost=0.00..1234.56 rows=10000)
-- Solução: criar índice em reserva(status, data_reserva)
```

### 3. Preparação para o mercado

Conhecimento de SQL é requisito em 90% das vagas de backend:

- Entrevistas técnicas frequentemente incluem desafios SQL
- Debugging de problemas de performance requer SQL
- Migrations e otimizações exigem conhecimento profundo

---

## Quando usar Entity Framework?

Entity Framework tem seu lugar em cenários específicos:

### Use EF quando:
- Projeto com time júnior que precisa de produtividade rápida
- CRUD simples sem queries complexas
- Prazo apertado e performance não é crítica
- Necessita de migrations automáticas gerenciadas pelo framework

### Use Dapper quando:
- Performance é crítica (sistemas de alta escala)
- Queries complexas com agregações, CTEs, window functions
- Quer aprender SQL profundamente
- Precisa de controle total sobre o banco de dados
- Está otimizando custos de infraestrutura (menos recursos = menos custo)

---


Cada repository contém SQL raw:
```csharp
public class FilmeRepository : IFilmeRepository
{
    private readonly IDbConnection _db;

    public async Task GetByIdAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync(
            "SELECT * FROM filme WHERE id_filme = @Id",
            new { Id = id }
        );
    }

    public async Task CreateAsync(Filme filme)
    {
        return await _db.QueryFirstAsync(@"
            INSERT INTO filme (nome_filme, genero, duracao, classificacao_etaria)
            VALUES (@NomeFilme, @Genero, @Duracao, @ClassificacaoEtaria)
            RETURNING id_filme",
            filme
        );
    }
}
```

---

## Conclusão

A escolha de Dapper com SQL raw no CineFlow não é apenas técnica, mas pedagógica:

1. Aplica diretamente os conceitos do livro "Learning SQL"
2. Desenvolve habilidades valiosas para o mercado
3. Garante performance superior desde o MVP
4. Mantém o código transparente e debugável
5. Prepara o desenvolvedor para sistemas complexos e de alta escala

O overhead de escrever SQL manualmente é compensado pelo:
- Conhecimento profundo adquirido
- Performance superior
- Controle total sobre o sistema
- Facilidade de otimização futura

Este é um investimento em aprendizado que se paga ao longo da carreira.

---

## Referências

- O'Reilly - Learning SQL, 3rd Edition (Alan Beaulieu)
- Dapper GitHub: https://github.com/DapperLib/Dapper
- PostgreSQL Documentation: https://www.postgresql.org/docs/
- Benchmark: Dapper vs Entity Framework (Marc Gravell)