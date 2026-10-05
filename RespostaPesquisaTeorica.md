RESPOSTA PESQUISA TEÓRICA:

**Questão 1: Codificação de Dados — Base64:** 

**1 \- Quais propriedades de segurança (Confidencialidade, Integridade, Disponibilidade) a técnica de Base64 ajuda a proteger? Justifique.**

Nenhuma. O Base64 não é um mecanismo de segurança ou criptografia, mas apenas um formato de conversão de dados.

Confidencialidade: Não protege. Como o Base64 não utiliza chaves ou senhas, o algoritmo é público e qualquer pessoa que intercepte a mensagem pode decodificá-la e ler o conteúdo instantaneamente. Ele apenas "ofusca" a visão, mas não oculta a informação.

Integridade: Não protege. O Base64 não gera hashes ou assinaturas digitais. Se um invasor alterar os caracteres da mensagem durante a transmissão, o destinatário não terá nenhum alerta nativo de que o dado foi adulterado.

Disponibilidade: Não protege. A técnica trata apenas da representação dos dados e não possui nenhuma função relacionada a manter sistemas no ar ou garantir o acesso à informação.

**2 \- Para qual finalidade a codificação Base64 é melhor empregada na arquitetura de sistemas e redes?**

A principal finalidade do Base64 é a transmissão segura de dados binários através de protocolos que foram desenhados para lidar apenas com texto simples.

Muitos sistemas de comunicação e formatos de dados (como E-mail/SMTP, APIs REST usando JSON, XML e partes do HTTP) têm dificuldade em transportar arquivos crus (como uma imagem, um documento PDF ou uma chave de criptografia) sem corrompê-los.

O Base64 resolve esse problema traduzindo qualquer arquivo complexo para um alfabeto seguro e universal (apenas letras, números e os símbolos \+ e /). Isso garante que os dados cheguem intactos ao destino, onde são convertidos de volta para o formato original.

**Questão 2: Criptografia Clássica — Cifra de César:** 

**1 \- Qual é o tamanho do espaço de chaves na Cifra de César para o alfabeto inglês tradicional (26 letras)?**

O espaço de chaves na Cifra de César é de apenas 25 chaves efetivas (variando de 1 a 25).

Matematicamente, existem 26 opções numéricas dentro do módulo (\$k \\pmod{26}\$). No entanto, um deslocamento (chave $k$) igual a $0$ ou igual a $26$ não altera o texto (a mensagem cifrada fica idêntica ao texto claro original). Qualquer chave maior que 26 é apenas um ciclo que recairá sobre os mesmos 25 deslocamentos básicos.

2 \- Por que um ataque de força bruta é extremamente trivial contra este método?  
Um ataque de força bruta é extremamente trivial devido ao tamanho minúsculo do espaço de chaves.

A força bruta consiste em testar todas as chaves possíveis até encontrar a correta. Como existem apenas 25 deslocamentos que alteram o texto, um invasor precisa testar, no máximo, 25 combinações.

Para um computador moderno, gerar e ler as 25 variações de uma mensagem leva frações de milissegundo, permitindo que a quebra da criptografia seja instantânea. Mesmo que o ataque fosse feito manualmente por um humano (com papel e caneta), o texto claro poderia ser descoberto em poucos minutos tentando ler os resultados.

**Questão 3: Criptografia Clássica — Substituição Monoalfabética:** 

**1 \- Embora o espaço de chaves seja enorme ($26!\approx 4\times 1{0}^{26}$ combinações), ela é vulnerável a qual tipo de técnica de criptanálise?**

A Cifra de Substituição Monoalfabética é vulnerável a uma técnica de criptanálise chamada **Análise de Frequência** (Frequency Analysis).

Apesar de ser impossível para um ser humano (ou até mesmo computadores de forma puramente aleatória) testar todas as $4\times 1{0}^{26}$ chaves por força bruta clássica, o algoritmo falha em ocultar os padrões naturais da linguagem em que o texto foi escrito.

**2 \- Explique como essa técnica funciona.**  
**A Análise de Frequência baseia-se no fato de que, em qualquer idioma natural, certas letras aparecem com muito mais frequência do que outras.**

Como a Substituição Monoalfabética substitui uma letra do texto claro sempre pela mesma letra no texto cifrado (por exemplo, todo "A" vira "Z"), **a frequência das letras é preservada**.

A técnica funciona com os seguintes passos:

1. **Contagem e Estatística:** O criptanalista conta quantas vezes cada letra aparece no texto cifrado.  
2. **Comparação com o Idioma:** Ele compara essas contagens com a tabela de frequência conhecida do idioma. Por exemplo, em português, as letras "A" e "E" são as mais comuns (aparecendo cerca de 14% e 12% das vezes, respectivamente). No inglês, a letra "E" domina (cerca de 12.7%).  
3. **Mapeamento Lógico:** Se a letra "X" for a mais comum no texto cifrado de uma mensagem em inglês, há uma probabilidade altíssima de que "X" seja a substituição da letra "E".  
4. **Análise de Dígrafos e Trígrafos:** Além de letras isoladas, o atacante analisa os pares e trios de letras mais comuns. No inglês, "TH" e "THE" são muito frequentes; no português, "QU", "OS", "AS", "ENT". Ao cruzar essas sílabas, o atacante descobre o restante do alfabeto como em um quebra-cabeças.

Portanto, a Análise de Frequência destrói a cifra porque ataca a "forma" do texto, e não o tamanho do espaço de chaves, resolvendo a equação em questão de minutos ou horas, mesmo existindo bilhões de combinações possíveis.

**Questão 4: Armazenamento Seguro de Senhas e Criptoanálise:**

**1 \- O que são Rainbow Tables e como elas permitem descobrir senhas a partir de hashes vazados sem precisar testar todas as combinações na hora?**

As **Rainbow Tables** (Tabelas Arco-Íris) são enormes bancos de dados pré-computados contendo milhões de senhas comuns associadas aos seus respectivos hashes (por exemplo, "123456" \= **e10adc39...**, "senha123" \= **ef797c81...**).

Elas utilizam uma técnica chamada *time-memory trade-off* (compromisso entre tempo e memória). Em um ataque de força bruta tradicional ou ataque de dicionário, o invasor precisa calcular o hash de cada tentativa de senha na hora para ver se coincide com o hash roubado. Isso exige muito poder de processamento (tempo).

Com as Rainbow Tables, esse cálculo demorado já foi feito previamente por supercomputadores. Se um invasor rouba um banco de dados cheio de hashes e possui uma Rainbow Table do mesmo algoritmo (ex: MD5), ele apenas faz uma **busca (lookup)**. Se o hash roubado estiver na tabela, ele descobre a senha original quase instantaneamente.

**2 \- O que é o Salt e de que forma a adição dele no processo de geração do hash protege o banco de dados contra ataques de Rainbow Tables?**

O **Salt** (sal) é uma string ou conjunto de dados aleatórios que é adicionado à senha do usuário antes de ela passar pela função de hash. Por exemplo: o sistema gera um texto aleatório como **x9A@1z**, junta isso com a senha do usuário (**senha123**) e faz o hash de **x9A@1zsenha123**. Esse "salt" é salvo em texto claro no banco de dados junto ao usuário.

A adição do Salt protege o banco contra Rainbow Tables de duas maneiras:

1. **Inutiliza tabelas pré-computadas:** Uma Rainbow Table só funciona para senhas comuns. Quando adicionamos um Salt de, por exemplo, 16 caracteres aleatórios, uma senha fraca como **123456** transforma-se em **8jD\*2\!kL123456**. Nenhum Rainbow Table terá esse valor pré-calculado no mundo.  
2. **Evita hashes idênticos para senhas idênticas:** Se dois usuários (João e Maria) usarem a exata mesma senha (**flamengo**), sem o Salt, o hash de ambos será idêntico no banco de dados. Com o Salt, João recebe um Salt **XYZ** e Maria recebe um Salt **ABC.** Como os dados de entrada ficaram diferentes (**XYZflamengo vs ABCflamengo**), os hashes resultantes serão completamente distintos. O atacante precisaria criar uma Rainbow Table monstruosa e única *para cada* usuário, o que é matematicamente inviável (falta de espaço de armazenamento mundial para isso).

