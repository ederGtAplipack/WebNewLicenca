-- DROP TABLES na ordem inversa das dependências
DROP TABLE IF EXISTS `licencas`.`acessos_new`;
DROP TABLE IF EXISTS `licencas`.`contrato`;
DROP TABLE IF EXISTS `licencas`.`licenca`;
DROP TABLE IF EXISTS `licencas`.`anagrafica`;
DROP TABLE IF EXISTS `licencas`.`licencaschave`;
DROP TABLE IF EXISTS `licencas`.`software`;
DROP TABLE IF EXISTS `licencas`.`cep`;
DROP TABLE IF EXISTS `licencas`.`revenda`;

-- Tabela Revenda
CREATE TABLE `licencas`.`revenda` (
    `idrevenda` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `razao_social` VARCHAR(100) NOT NULL,
    `login` VARCHAR(50) NOT NULL,
    `senha` VARCHAR(100) NOT NULL,
    PRIMARY KEY (`idrevenda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Software
CREATE TABLE `licencas`.`software` (
    `idSoftware` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Software` VARCHAR(50) DEFAULT NULL,
    `Descricao` VARCHAR(255) DEFAULT NULL,
    PRIMARY KEY (`idSoftware`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela LicencasChave
CREATE TABLE `licencas`.`licencaschave` (
    `idLicencaChave` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Chave` VARCHAR(50) DEFAULT NULL,
    `idSoftware` INT UNSIGNED DEFAULT NULL,
    `IdRevenda` INT UNSIGNED DEFAULT NULL,
    `NumLic` INT UNSIGNED DEFAULT NULL,
    `DataInser` DATETIME DEFAULT NULL,
    `TipoLic` VARCHAR(20) DEFAULT NULL,
    `Entregue` INT UNSIGNED DEFAULT NULL,
    `EntreguePara` VARCHAR(50) DEFAULT NULL,
    PRIMARY KEY (`idLicencaChave`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Anagrafica
CREATE TABLE `licencas`.`anagrafica` (
    `idanagrafica` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `razao_social` VARCHAR(100) DEFAULT NULL,
    `nome_fantasia` VARCHAR(100) DEFAULT NULL,
    `contato` VARCHAR(100) DEFAULT NULL,
    `cep` VARCHAR(10) DEFAULT NULL,
    `endereco` VARCHAR(100) DEFAULT NULL,
    `bairro` VARCHAR(100) DEFAULT NULL,
    `cidade` VARCHAR(100) DEFAULT NULL,
    `uf` VARCHAR(4) DEFAULT NULL,
    `cnpj` VARCHAR(20) DEFAULT NULL,
    `ie` VARCHAR(20) DEFAULT NULL,
    `telefone` VARCHAR(20) DEFAULT NULL,
    `email` VARCHAR(100) DEFAULT NULL,
    `idrevenda` INT UNSIGNED DEFAULT NULL,
    `Senha` VARCHAR(20) DEFAULT NULL,
    PRIMARY KEY (`idanagrafica`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Licenca
CREATE TABLE `licencas`.`licenca` (
    `numLic` INT NOT NULL,
    `IdCliente` INT UNSIGNED NOT NULL,
    `TipoLic` LONGTEXT DEFAULT NULL,
    `MacAddress` LONGTEXT DEFAULT NULL,
    `DataLic` DATETIME(6) NOT NULL,
    `scade` DATETIME(6) NOT NULL,
    `attivo` TINYINT(1) NOT NULL,
    `IdRevenda` INT UNSIGNED NOT NULL,
    `SistemaOp` LONGTEXT DEFAULT NULL,
    `DataAtivacao` DATETIME(6) DEFAULT NULL,
    `tipo_pc` LONGTEXT DEFAULT NULL,
    `nome_computador` LONGTEXT DEFAULT NULL,
    `software` LONGTEXT DEFAULT NULL,
    `ip` LONGTEXT DEFAULT NULL,
    `processador` LONGTEXT DEFAULT NULL,
    `Status` VARCHAR(50) DEFAULT NULL,
    `idlicencachave` INT UNSIGNED DEFAULT NULL,
    PRIMARY KEY (`numLic`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Contrato
CREATE TABLE `licencas`.`contrato` (
    `id_contrato` VARCHAR(255) NOT NULL,
    `id_cliente` INT UNSIGNED NOT NULL,
    `plano` LONGTEXT DEFAULT NULL,
    `qtd_licencas` INT NOT NULL,
    `data_inicio` DATETIME(6) NOT NULL,
    `data_fim` DATETIME(6) DEFAULT NULL,
    `Periodicidade` LONGTEXT NOT NULL,
    `PagamentoEmDia` TINYINT(1) NOT NULL,
    `StatusContrato` LONGTEXT NOT NULL,
    `DataProximoPagamento` DATETIME(6) DEFAULT NULL,
    `DataUltimoPagamento` DATETIME(6) DEFAULT NULL,
    `Observacoes` LONGTEXT DEFAULT NULL,
    `StatusDescricao` LONGTEXT NOT NULL,
    PRIMARY KEY (`id_contrato`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela CEP
CREATE TABLE `licencas`.`cep` (
    `ID` INT NOT NULL AUTO_INCREMENT,
    `CEP` INT DEFAULT NULL,
    `Codigo` INT DEFAULT NULL,
    `Estado` VARCHAR(2) DEFAULT NULL,
    `Codigo2` VARCHAR(5) DEFAULT NULL,
    `Logradouro` VARCHAR(60) DEFAULT NULL,
    `Bairro` VARCHAR(60) DEFAULT NULL,
    `Cidade` VARCHAR(60) DEFAULT NULL,
    PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Acessos_New
CREATE TABLE `licencas`.`acessos_new` (
    `id_acesso_new` VARCHAR(255) NOT NULL,
    `data_hora` DATETIME(6) NOT NULL,
    `id_licenca` INT NOT NULL,
    `mac_address` LONGTEXT NOT NULL,
    `processador` LONGTEXT NOT NULL,
    `software` LONGTEXT NOT NULL,
    `external_IP` LONGTEXT NOT NULL,
    PRIMARY KEY (`id_acesso_new`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Adicionando as FOREIGN KEYS (somente após todas as tabelas criadas)
ALTER TABLE `licencas`.`licenca`
    ADD CONSTRAINT `fk_licenca_anagrafica`     FOREIGN KEY (`IdCliente`)       REFERENCES `licencas`.`anagrafica`(`idanagrafica`),
    ADD CONSTRAINT `fk_licenca_revenda`        FOREIGN KEY (`IdRevenda`)       REFERENCES `licencas`.`revenda`(`idrevenda`),
    ADD CONSTRAINT `fk_licenca_licencachave`   FOREIGN KEY (`idlicencachave`)  REFERENCES `licencas`.`licencaschave`(`idLicencaChave`);

ALTER TABLE `licencas`.`contrato`
    ADD CONSTRAINT `fk_contrato_anagrafica`    FOREIGN KEY (`id_cliente`)      REFERENCES `licencas`.`anagrafica`(`idanagrafica`);

ALTER TABLE `licencas`.`acessos_new`
    ADD CONSTRAINT `fk_acessos_new_licenca`    FOREIGN KEY (`id_licenca`)      REFERENCES `licencas`.`licenca`(`numLic`);

ALTER TABLE `licencas`.`licencaschave`
    ADD CONSTRAINT `fk_licencaschave_software` FOREIGN KEY (`idSoftware`)      REFERENCES `licencas`.`software`(`idSoftware`);

ALTER TABLE `licencas`.`anagrafica`
    ADD CONSTRAINT `fk_anagrafica_revenda`     FOREIGN KEY (`idrevenda`)       REFERENCES `licencas`.`revenda`(`idrevenda`);
