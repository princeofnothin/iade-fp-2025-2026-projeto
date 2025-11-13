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


