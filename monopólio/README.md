#+ 🎲 Monopólio — Projeto Académico

**Unidade Curricular:** Fundamentos da Progrmação
# Projeto: Jogo Monopólio

## Elementos do Grupo

- Carlos Lima — 20240740
- Fábio Rómulo — 20241821

---

## Estratégias de Implementação

O jogo foi desenvolvido em **C#**, com execução no **terminal/console**. A implementação assenta em princípios de programação orientada a objetos e em estruturas de dados como **classes, listas, arrays e enums**. A lógica do jogo foi organizada em métodos e serviços distintos para garantir modularidade e facilitar a manutenção.

Pontos-chave da implementação:

- Utilização do padrão arquitetural **MVC (Model–View–Controller)** para separar responsabilidades.
- `Model`: entidades como `Player`, `Space`, `Board`, `Card` e `GameState`.
- `View`: componentes que imprimem o tabuleiro e mensagens no terminal (formatadores de output).
- `Controller`: processa comandos, gere turnos e orquestra as ações entre `Model` e `View`.
- Validação de ações (ex.: verificar saldo para comprar, validade de movimentos).
- Uso de `Random` para eventos aleatórios (cartas, lançamentos de dados).

### Funcionalidades adicionais

- Impressão visual do tabuleiro em formato texto
- Mensagens de feedback claras para o utilizador
- Sistema de cartas (Oportunidade / Caixa Comunitária)

---

## Distribuição de Tarefas

- **Carlos Lima** → funcionalidades e controllers (lógica de jogo, processamento de comandos)
- **Fábio Rómulo** → entidades, models e views (representação do tabuleiro, formatação de output)

> Nota: A divisão de tarefas foi colaborativa; ambos participaram na revisão e testes finais.

---

## Estrutura e Organização do Projeto

O projeto segue o padrão MVC para promover a separação de responsabilidades e facilitar manutenção:

- Model: classes que representam o estado do jogo (jogadores, propriedades, tabuleiro, cartas).
- View: responsáveis pela apresentação no terminal (BoardView, PlayerView, GameView).
- Controller: recebem comandos do utilizador, validam ações e atualizam o Model; também invocam a View para apresentar resultados.

---

## Problemas conhecidos no código

Alguns problemas verosímeis identificados (exemplos para investigação):

- Cálculo de impostos/propinas: em certas combinações de propriedades, o cálculo da renda aplicada a jogadores pode duplicar o valor cobrado.
- Lógica da prisão: a condição de saída da prisão (número de turnos e pagamento) pode não estar a ser atualizada corretamente para todos os jogadores.
- Formatação do tabuleiro: em terminais com largura restrita, o layout textual do tabuleiro pode quebrar e desalinha colunas.
- Gestão de ficheiros (persistência): atualmente o estado é em memória; salvar/carregar pode não funcionar ou não existir implementação.
- Conflitos de concorrência (cenário multi-threaded): se futuramente for adicionada assíncronia, a atualização do `GameState` pode sofrer race conditions.

Estas entradas servem como pontos de partida para debugging e melhoria contínua.

---

## Estruturas de Dados Utilizadas

- **Classes**: `Player`, `Space`, `Board`, `Card`, `GameState`, entre outras.
- **Listas** (`List<T>`): coleções dinâmicas de jogadores, propriedades e cartas.
- **Arrays**: representações matriciais do tabuleiro, quando aplicável.
- **Enums**: para tipos fixos (ex.: `SpaceType`, `CardType`, `PlayerStatus`).
- **Random**: geração de números aleatórios para cartas e dados.
- **Dicionários** (`Dictionary<K,V>`) (opcional): mapas de espaços para proprietários, preços e outras associações rápidas.

---


Este projeto foi desenvolvido no âmbito da Unidade Curricular **Programação II**, durante o ano letivo **2025/2026**.

## Lista de Comandos e Funcionamento

- RJ `Nome`
  - Regista um novo jogador com saldo inicial de 1200€.
  - Erros: nome inválido, jogador existente.
  - Exemplo: `RJ alex`
- LJ
  - Lista jogadores registados ordenados por vitórias.
  - Exemplo: `LJ`
- IJ `Nome1 Nome2 [Nome3 Nome4]`
  - Inicia jogo com 2–4 jogadores registados e posiciona todos em `Start`.
  - Erros: contagem inválida; jogador inexistente.
  - Exemplo: `IJ alex romulo`
- DJ
  - Imprime tabuleiro 7×7, jogador da vez, dinheiro e propriedades.
  - Exemplo: `DJ`
- LD `Nome [v1 v2]`
  - Lança dados e move: 1º dado horizontal (− esquerda / + direita), 2º dado vertical (− baixo / + cima), com wrap.
  - Valores opcionais para testes: `v1,v2 ∈ {−3,−2,−1,1,2,3}`.
  - Duplos: relança obrigatoriamente; dois duplos seguidos enviam para prisão.
  - Erros: não é a vez; instrução inválida.
  - Exemplo: `LD alex 3 -1`
- CE `Nome`
  - Compra o espaço atual se for propriedade disponível e sem dono.
  - Erros: espaço não à venda; já comprado; saldo insuficiente.
  - Exemplo: `CE alex`
- PA `Nome`
  - Paga renda ao dono do espaço atual.
  - Fórmula: `Preço × 0,25 + Preço × 0,75 × NúmeroDeCasas`.
  - Se não tiver saldo suficiente, é eliminado.
  - Erros: não é necessário pagar.
  - Exemplo: `PA romulo`
- CC `Nome NomeDoEspaço`
  - Compra casa no espaço indicado; requer possuir todos os espaços da cor; custo `0,6 × Preço`; máximo 4 casas.
  - Erros: espaço inválido; não é propriedade; não possui todos; já tem 4 casas; saldo insuficiente.
  - Exemplo: `CC alex Red1`
- TC `Nome`
  - Tira carta em `Chance` ou `Community` e aplica efeitos automaticamente; pode eliminar se não conseguir pagar.
  - Erros: carta já tirada; espaço inválido/sem cartas.
  - Exemplo: `TC alex`
- TT `Nome`
  - Termina o turno após cumprir `LD` e todas as ações obrigatórias (`PA`/`TC`) e sem relançamento pendente por duplo; avança para próximo jogador.
  - Erros: falta `LD`; falta `PA`/`TC`; tem duplo pendente.
  - Exemplo: `TT alex`

## Exemplo de Sessão (Alex e Romulo)
- `RJ alex`
- `RJ romulo`
- `IJ alex romulo`
- `DJ`
- `LD alex 3 -1`
- `CE alex`
- `PA romulo`
- `TC alex`
- `TT alex`

---

## Requisitos e Execução
- Requisitos: .NET SDK 8+   
- Compilar: `dotnet build .\monopólio\monopólio.csproj`  
- Executar: `dotnet run --project .\monopólio\monopólio.csproj`  
- Entrada por consola: cada linha é um comando; linha vazia termina a aplicação.

## Fluxo de Turno
- Estados de turno: `RollingDice → DiceRolled → (ações PA/TC/CE/CC) → TT`, prisão: `InPrison`  
- Inicialização define `RollingDice` (`src/Controllers/GameController.cs:73`).  
- `LD` define `DiceRolled` (`src/Controllers/GameController.cs:160`, `src/Controllers/GameController.cs:236`).  
- `TT` só avança se LD foi feito, sem obrigações pendentes e sem relançamento por duplo (`src/Controllers/GameController.cs:368`–`src/Controllers/GameController.cs:392`).

## Determinismo de Testes
- `LD nome v1 v2` aceita apenas `−3, −2, −1, 1, 2, 3` (`src/Controllers/CommandRouter.cs:70`).  
- Aplicação determinística dos dados: (`src/Controllers/GameController.cs:235`).  
- API para setar valores dos dados: `Dice.SetRoll` (`src/Models/Dice.cs:68`).

## Regras de Duplos e Prisão
- Em duplo, o jogador fica obrigado a relançar após ações obrigatórias (`src/Controllers/GameController.cs:189`, `src/Controllers/GameController.cs:262`).  
- Dois duplos consecutivos enviam para a prisão (`src/Controllers/GameController.cs:171`, `src/Controllers/GameController.cs:246`).

## Layout do Tabuleiro (Resumo)
- Tabuleiro 7×7 com `Start` no centro (3,3) e espaços especiais configurados (`src/Rules/BoardLayout.cs:13`).  
- Utilidades de índice/coordenação do tabuleiro (`src/Models/Board.cs:63`).

## Cálculos Financeiros
- Dinheiro inicial: 1200€ (`src/Models/Player.cs:25`, `src/Controllers/GameController.cs:61`).  
- Renda: `Preço × 0,25 + Preço × 0,75 × nº de casas` (`src/Models/Space.cs:69`).  
- Casa: `0,6 × Preço` (máximo 4) (`src/Services/PurchaseService.cs:61`, `src/Models/Space.cs:82`).  
- LuxTax: 80€ vão para `FreePark` (`src/Controllers/GameController.cs:324`), recolhidos ao terminar em `FreePark` (`src/Controllers/GameController.cs:318`).

## Erros e Mensagens Comuns
- `Instrução inválida.` (formato/valores fora das regras) (`src/Controllers/CommandRouter.cs:131`).  
- `Não existe um jogo em curso.` (`IJ` não chamado) (`src/Controllers/GameController.cs:141`).  
- `Não é a vez do jogador.` (ordem de turno) (`src/Controllers/GameController.cs:154`).  
- `Ainda não lançou os dados.` / `Ainda falta pagar aluguer.` / `Ainda falta tirar carta.` / `Saiu duplo — tem de lançar novamente...` (`src/Controllers/GameController.cs:370`–`src/Controllers/GameController.cs:389`).

## Lista de Comandos e Funcionamento

- RJ `Nome`
  - Regista um novo jogador com saldo inicial de 1200€.
  - Erros: nome inválido, jogador existente.
  - Exemplo: `RJ Ana`
- LJ
  - Lista jogadores registados ordenados por vitórias.
  - Exemplo: `LJ`
- IJ `Nome1 Nome2 [Nome3 Nome4]`
  - Inicia jogo com 2–4 jogadores registados e posiciona todos em `Start`.
  - Erros: contagem inválida; jogador inexistente.
  - Exemplo: `IJ Ana Bruno`
- DJ
  - Imprime tabuleiro 7×7, jogador da vez, dinheiro e propriedades.
  - Exemplo: `DJ`
- LD `Nome [v1 v2]`
  - Lança dados e move: 1º dado horizontal (− esquerda / + direita), 2º dado vertical (− baixo / + cima), com wrap.
  - Valores opcionais para testes: `v1,v2 ∈ {−3,−2,−1,1,2,3}`.
  - Duplos: relança obrigatoriamente; dois duplos seguidos enviam para prisão.
  - Erros: não é a vez; instrução inválida.
  - Exemplo: `LD Ana` ou `LD Ana 3 -3`
- CE `Nome`
  - Compra o espaço atual se for propriedade disponível e sem dono.
  - Erros: espaço não à venda; já comprado; saldo insuficiente.
  - Exemplo: `CE Ana`
- PA `Nome`
  - Paga renda ao dono do espaço atual.
  - Fórmula: `Preço × 0,25 + Preço × 0,75 × NúmeroDeCasas`.
  - Se não tiver saldo suficiente, é eliminado.
  - Erros: não é necessário pagar.
  - Exemplo: `PA Bruno`
- CC `Nome NomeDoEspaço`
  - Compra casa no espaço indicado; requer possuir todos os espaços da cor; custo `0,6 × Preço`; máximo 4 casas.
  - Erros: espaço inválido; não é propriedade; não possui todos; já tem 4 casas; saldo insuficiente.
  - Exemplo: `CC Ana Red1`
- TC `Nome`
  - Tira carta em `Chance` ou `Community` e aplica efeitos automaticamente; pode eliminar se não conseguir pagar.
  - Erros: carta já tirada; espaço inválido/sem cartas.
  - Exemplo: `TC Ana`
- TT `Nome`
  - Termina o turno após cumprir LD e todas as ações obrigatórias (PA/TC) e sem relançamento pendente por duplo; avança para próximo jogador.
  - Erros: falta LD; falta PA/TC; tem duplo pendente.
  - Exemplo: `TT Ana`


