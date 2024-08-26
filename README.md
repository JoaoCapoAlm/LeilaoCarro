# Teste Técnico Desenvolvedor - MarQ.
Projeto criado para o teste técnico para a vaga de desenvolvedor na MarQ.
Projeto desenvolvido por **João Capoani**.

## Descrição do teste
Seguem abaixo mais detalhes sobre o nosso teste técnico.
Lembrando que essa vaga é para Desenvolvedor Back End.
Criar uma API restful robusta de um sistema de Leilão de Carro.
Essa api precisa
- Criar cadastro de usuário
- Ao criar cadastro, ao inserir o cep deve preencher automaticamente o endereço (https://viacep.com.br/)
- Visualizar carros
- Criar/Deletar carros
- Dar lance em um carro
- Ser notificado ao ganhar um carro

Um diferencial dessa api seria
- Rotina todo dia de manhã às 08:00 enviando um e-mail para os ganhadores dos
carros, dando os parabéns pela conquista.

O Prazo de entrega são 5 dias corridos, se precisar de mais tempo, favor informar.

Formato de entrega: responda a este email com
- Link do código no GitHub ou similar
- Vídeo(s) explicando a abordagem do código, técnicas utilizadas, decisões
tomadas

PS. Para gravar os vídeos, recomendamos usar o Vimeo Record, que permite gravar
gratuitamente, compartilhando a tela e câmera ao mesmo tempo.

## Configuração
Caso queira rodar o projeto localmente, já haverá um banco de dados SQLite configurado no projeto, mas caso queira um banco limpo deverá seguir as seguintes etapas:
- Excluir os seguintes arquivos localizados na pasta LeilaoCarro/Data
  - leilao.db
  - leilao.db-shm
  - leilao.db-wal
- No prompt de comando ou PowerShell rodar o seguinte script
  - `dotnet ef database update`
