
# 📘 Documentação de Funcionalidades Sugeridas com Base no Modelo de Dados

Este documento descreve possíveis métodos e funcionalidades que podem ser implementados no sistema com base na estrutura das tabelas presentes no banco de dados.

---

## 🔐 Revenda

- **AutenticarRevenda(login, senha)**  
  Verifica se o login e a senha da revenda são válidos.

- **CadastrarRevenda(dados)**  
  Insere uma nova revenda no sistema.

- **ListarClientesPorRevenda(idRevenda)**  
  Retorna todos os clientes associados a uma revenda.

---

## 🧾 Anagrafica (Clientes)

- **CadastrarCliente(dados)**  
  Registra um novo cliente no sistema.

- **BuscarClientePorCNPJ(cnpj)**  
  Localiza um cliente com base no CNPJ.

- **AtualizarCliente(id, dados)**  
  Atualiza informações de um cliente existente.

- **ListarContratos(idCliente)**  
  Mostra os contratos ativos e inativos de um cliente.

- **ListarLicencas(idCliente)**  
  Exibe todas as licenças vinculadas a um cliente.

---

## 📄 Contrato

- **CriarContrato(idCliente, plano, datas)**  
  Cria um novo contrato com dados como plano e periodicidade.

- **VerificarStatusContrato(idContrato)**  
  Retorna o status atual do contrato (ativo, vencido, etc).

- **RenovarContrato(idContrato)**  
  Atualiza a data de término e próximo pagamento.

---

## 🔑 LicencasChave

- **GerarChaveLicenca(idSoftware, idRevenda)**  
  Cria uma nova chave de licença para um software.

- **ListarChavesPorRevenda(idRevenda)**  
  Lista todas as chaves geradas por uma revenda.

- **EntregarChave(idLicencaChave, destinatario)**  
  Marca uma chave como entregue para determinado cliente ou técnico.

---

## 🪪 Licenca

- **AtivarLicenca(macAddress, chave)**  
  Realiza a ativação da licença usando a chave e dados do dispositivo.

- **ValidarLicenca(idLicenca)**  
  Verifica se a licença é válida (ativa, não expirada).

- **RegistrarDispositivo(idLicenca, ip, nomeComputador)**  
  Armazena dados da máquina associada à licença.

- **AtualizarStatusLicenca(idLicenca, status)**  
  Altera o status da licença (ativa, bloqueada, expirada, etc).

---

## 📊 Acessos

- **RegistrarAcesso(idLicenca, macAddress, ip)**  
  Salva o log de acesso de um dispositivo ao sistema.

- **ListarAcessosPorLicenca(idLicenca)**  
  Lista os acessos realizados por uma determinada licença.

---

## 🧠 Integrações Possíveis

- Integração REST para licenciamento automático
- Painel administrativo para acompanhamento de clientes, licenças e acessos
- Notificações automáticas de expiração de contratos
- Dashboard de licenças ativas/inativas por revenda e cliente

