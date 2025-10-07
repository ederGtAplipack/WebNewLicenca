-- licencas.software definição

CREATE TABLE `software` (
  `idSoftware` int unsigned NOT NULL AUTO_INCREMENT,
  `nSoftware` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Descricao` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idSoftware`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.aspnetusers definição

CREATE TABLE `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `ValidAudience` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ValidIssuer` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `key` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RefreshToken` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RefreshTokenExpiryTime` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.revenda definição

CREATE TABLE `revenda` (
  `idrevenda` int unsigned NOT NULL AUTO_INCREMENT,
  `razaoSocial` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`idrevenda`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.aspnetroles definição

CREATE TABLE `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.anagrafica definição

CREATE TABLE `anagrafica` (
  `idanagrafica` int unsigned NOT NULL AUTO_INCREMENT,
  `razaoSocial` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `nomeFantasia` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `contato` varchar(100) DEFAULT NULL,
  `cep` varchar(10) DEFAULT NULL,
  `endereco` varchar(100) DEFAULT NULL,
  `bairro` varchar(100) DEFAULT NULL,
  `cidade` varchar(100) DEFAULT NULL,
  `uf` varchar(4) DEFAULT NULL,
  `cnpj` varchar(20) DEFAULT NULL,
  `ie` varchar(20) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `idrevenda` int unsigned DEFAULT NULL,
  `Senha` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idanagrafica`),
  KEY `anagrafica_revenda_fk` (`idrevenda`),
  CONSTRAINT `anagrafica_revenda_fk` FOREIGN KEY (`idrevenda`) REFERENCES `revenda` (`idrevenda`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.licencaschave definição

CREATE TABLE `licencaschave` (
  `idLicencaChave` int unsigned NOT NULL AUTO_INCREMENT,
  `Chave` varchar(50) DEFAULT NULL,
  `idSoftware` int unsigned DEFAULT NULL,
  `IdRevenda` int unsigned DEFAULT NULL,
  `NumLic` int unsigned DEFAULT NULL,
  `DataInser` datetime DEFAULT NULL,
  `TipoLic` varchar(20) DEFAULT NULL,
  `Entregue` int unsigned DEFAULT NULL,
  `EntreguePara` varchar(50) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `Status` varchar(30) DEFAULT 'Available',
  PRIMARY KEY (`idLicencaChave`),
  UNIQUE KEY `licencaschave_Chave_IDX` (`Chave`) USING BTREE,
  KEY `fk_licencaschave_software` (`idSoftware`),
  KEY `licencaschave_Status_IDX` (`Status`) USING BTREE,
  CONSTRAINT `fk_licencaschave_software` FOREIGN KEY (`idSoftware`) REFERENCES `software` (`idSoftware`)
) ENGINE=InnoDB AUTO_INCREMENT=2207 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.contrato definição

CREATE TABLE `contrato` (
  `idContrato` int unsigned NOT NULL AUTO_INCREMENT,
  `idCliente` int unsigned NOT NULL,
  `plano` longtext,
  `qtdLicencas` int NOT NULL,
  `dataInicio` datetime(6) NOT NULL,
  `dataFim` datetime(6) DEFAULT NULL,
  `Periodicidade` longtext NOT NULL,
  `PagamentoEmDia` tinyint(1) NOT NULL,
  `StatusContrato` longtext NOT NULL,
  `DataProximoPagamento` datetime(6) DEFAULT NULL,
  `DataUltimoPagamento` datetime(6) DEFAULT NULL,
  `Observacoes` longtext,
  `StatusDescricao` longtext NOT NULL,
  PRIMARY KEY (`idContrato`),
  KEY `contrato_anagrafica_FK` (`idCliente`),
  CONSTRAINT `contrato_anagrafica_FK` FOREIGN KEY (`idCliente`) REFERENCES `anagrafica` (`idanagrafica`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.revenda_user definição

CREATE TABLE `revenda_user` (
  `idRevenda` int unsigned NOT NULL,
  `idUser` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`idRevenda`,`idUser`),
  KEY `fk_revenda_user_user` (`idUser`),
  CONSTRAINT `fk_revenda_user_revenda` FOREIGN KEY (`idRevenda`) REFERENCES `revenda` (`idrevenda`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_revenda_user_user` FOREIGN KEY (`idUser`) REFERENCES `aspnetusers` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.aspnetuserroles definição

CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.licenca definição

CREATE TABLE `licenca` (
  `numLic` int NOT NULL AUTO_INCREMENT,
  `IdCliente` int unsigned NOT NULL,
  `TipoLic` longtext,
  `MacAddress` longtext,
  `DataLic` datetime(6) NOT NULL,
  `scade` datetime(6) NOT NULL,
  `attivo` tinyint(1) NOT NULL,
  `IdRevenda` int unsigned NOT NULL,
  `SistemaOp` longtext,
  `DataAtivacao` datetime(6) DEFAULT NULL,
  `tipoPc` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `software` longtext,
  `ip` longtext,
  `processador` longtext,
  `Status` varchar(50) DEFAULT NULL,
  `idlicencachave` int unsigned DEFAULT NULL,
  `nomeComputador` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `MaxDevices` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`numLic`),
  KEY `fk_licenca_anagrafica` (`IdCliente`),
  KEY `fk_licenca_revenda` (`IdRevenda`),
  KEY `fk_licenca_licencachave` (`idlicencachave`),
  KEY `licenca_scade_IDX` (`scade`) USING BTREE,
  KEY `licenca_Status_IDX` (`Status`) USING BTREE,
  CONSTRAINT `fk_licenca_anagrafica` FOREIGN KEY (`IdCliente`) REFERENCES `anagrafica` (`idanagrafica`),
  CONSTRAINT `fk_licenca_licencachave` FOREIGN KEY (`idlicencachave`) REFERENCES `licencaschave` (`idLicencaChave`)
) ENGINE=InnoDB AUTO_INCREMENT=1800 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.acessos_new definição

CREATE TABLE `acessos_new` (
  `id_acesso_new` int unsigned NOT NULL AUTO_INCREMENT,
  `data_hora` datetime(6) NOT NULL,
  `id_licenca` int NOT NULL,
  `mac_address` longtext NOT NULL,
  `processador` longtext NOT NULL,
  `software` longtext NOT NULL,
  `external_IP` longtext NOT NULL,
  PRIMARY KEY (`id_acesso_new`),
  KEY `fk_acessos_new_licenca` (`id_licenca`),
  CONSTRAINT `acessos_new_licenca_FK` FOREIGN KEY (`id_licenca`) REFERENCES `licenca` (`numLic`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- licencas.licencadispositivo definição

CREATE TABLE `licencadispositivo` (
  `idDispositivo` int unsigned NOT NULL AUTO_INCREMENT,
  `numLic` int NOT NULL,
  `DeviceFingerprint` varchar(255) NOT NULL,
  `DeviceInfo` json DEFAULT NULL,
  `ActivatedAt` datetime DEFAULT NULL,
  `LastSeenAt` datetime DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`idDispositivo`),
  KEY `fk_licenca_dispositivo_licenca` (`numLic`),
  CONSTRAINT `fk_licenca_dispositivo_licenca` FOREIGN KEY (`numLic`) REFERENCES `licenca` (`numLic`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;