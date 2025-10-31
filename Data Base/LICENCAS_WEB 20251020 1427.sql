-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	8.0.43


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema licencas
--

CREATE DATABASE IF NOT EXISTS licencas;
USE licencas;

--
-- Definition of table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES   ('20250728150624_InitialCreate','8.0.13');
INSERT INTO `__efmigrationshistory` VALUES   ('20250728151254_updateContratoModel','8.0.13');
INSERT INTO `__efmigrationshistory` VALUES   ('20250814132119_createTableIdentity','8.0.19');
INSERT INTO `__efmigrationshistory` VALUES   ('20250814173543_updateAppIdentity','8.0.17');
INSERT INTO `__efmigrationshistory` VALUES   ('20250815120452_updateAppIdentity','8.0.17');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;


--
-- Definition of table `acesso`
--

DROP TABLE IF EXISTS `acesso`;
CREATE TABLE `acesso` (
  `idacesso` int unsigned NOT NULL AUTO_INCREMENT,
  `macaddress` varchar(40) NOT NULL,
  `data_acesso` datetime NOT NULL,
  `codigo_cliente` int unsigned NOT NULL,
  `soft` varchar(45) NOT NULL,
  PRIMARY KEY (`idacesso`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `acesso`
--

/*!40000 ALTER TABLE `acesso` DISABLE KEYS */;
/*!40000 ALTER TABLE `acesso` ENABLE KEYS */;


--
-- Definition of table `acessos`
--

DROP TABLE IF EXISTS `acessos`;
CREATE TABLE `acessos` (
  `macaddress` varchar(40) DEFAULT NULL,
  `data_acesso` datetime DEFAULT NULL,
  `soft` varchar(45) DEFAULT NULL,
  `razao_social` varchar(100) DEFAULT NULL,
  `nome_fantasia` varchar(100) DEFAULT NULL,
  `revenda` int unsigned DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Dumping data for table `acessos`
--

/*!40000 ALTER TABLE `acessos` DISABLE KEYS */;
/*!40000 ALTER TABLE `acessos` ENABLE KEYS */;


--
-- Definition of table `acessos_new`
--

DROP TABLE IF EXISTS `acessos_new`;
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

--
-- Dumping data for table `acessos_new`
--

/*!40000 ALTER TABLE `acessos_new` DISABLE KEYS */;
/*!40000 ALTER TABLE `acessos_new` ENABLE KEYS */;


--
-- Definition of table `acessosnew`
--

DROP TABLE IF EXISTS `acessosnew`;
CREATE TABLE `acessosnew` (
  `idAcessosNew` int unsigned NOT NULL AUTO_INCREMENT,
  `DataHora` datetime DEFAULT NULL,
  `idLicenca` int unsigned DEFAULT '0',
  `macaddress` varchar(30) DEFAULT NULL,
  `Processador` varchar(30) DEFAULT NULL,
  `Software` varchar(45) DEFAULT NULL,
  `ExternalIP` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idAcessosNew`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=283 DEFAULT CHARSET=latin1;

--
-- Definition of table `anagrafica`
--

DROP TABLE IF EXISTS `anagrafica`;
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

--
-- Dumping data for table `anagrafica`
--

/*!40000 ALTER TABLE `anagrafica` DISABLE KEYS */;
INSERT INTO `anagrafica` VALUES   (1,'Americana Sistemas de Identicacao Emb ltda','Aplipack1','Edimilson','13477-230','EDUARDO MEDON 909','SANTA SOFIA','AMERICANA','SP','10498623000190','165366464114','1934693141','financeiro@eurosistemas.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (2,'Qualibombas Motores Bombas e Ferr Ltda','Qualibombas','Elio','13277-090','ANGELO BOTURA  43','JARDIM SAO JORGE','VALINHOS','SP','02794929000165','708050661117','1938692088','qualibombas@superig.com.br',10,NULL);
INSERT INTO `anagrafica` VALUES   (3,'BIGGI COMÉRCIO E SERVIÇOS DE INFORMÁTICA LTDA ME','MR INFORMÁTICA','BIGGI','13273-350','DAS GARDENIAS 185 SL 03','PARQUE CECAP','VALINHOS','SP','03720552000162','708.054.365-110','(19)2121-6694','mr.sys@terra.com.br',10,NULL);
INSERT INTO `anagrafica` VALUES   (4,'Eurosistema','Eurosistema','Pasquini','20060','Rua Cassiopea 48','','CASSINA DE\' PECCHI','MI','10000000000','0','0240047318','elio@eurosistema.it',1,NULL);
INSERT INTO `anagrafica` VALUES   (5,'Bajo Hair Style','Bajo Hair Style','Bajo','20145','Via Pier Capponi 4','','Milano','MI','07618280155','0','02463044','info@hairbajo.com',4,NULL);
INSERT INTO `anagrafica` VALUES   (7,'ORI HOME DECOR','Ori Center','Murillo','14026-567','RUA LEDA VASSIMON 750','NOVA ALIANCA','RIBEIRAO PRETO','SP','12536269000176','582507492117','1636201806','contato@orihomedecor.com.br',10,NULL);
INSERT INTO `anagrafica` VALUES   (8,'Backup Brasil Sistemas Ltda - NoteLilian','Backup Brasil Sistemas Ltda','Joao Paulo','14020-060','CASEMIRO DE ABREU 256','VILA SEIXAS','RIBEIRAO PRETO','SP','03604807000121','582598080115','1636320723','joao@backupbrasil.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (9,'NOVA LIMP PRODUTOS DE IMPEZA LTDA','NOVA LIMP','DANIEL','14015-080','LAFAIETE 1858','CENTRO','RIBEIRAO PRETO','SP','03512783000180','565265r','16 3632-9981','nova_limp@hotmail.com',10,NULL);
INSERT INTO `anagrafica` VALUES   (10,'ANTONIO CELSO CHAPINA EIRELI','CHAMPS','CELSO','14640-000','NOVE DE JULHO','CENTRO','MORRO AGUDO','SP','04202089000120','467068603119','1638516515','champsl@uol.com.br',10,NULL);
INSERT INTO `anagrafica` VALUES   (11,'Jose Antonio Zacoler Trajes Me','Only for mem','Abel','13025-155','GENERAL OSORIO','CAMBUI','CAMPINAS','SP','06209764000178','795129572112','(19) 3253-0993','onlyforme@terra.com.br',10,NULL);
INSERT INTO `anagrafica` VALUES   (12,'DIEGO CAMPOS CARVALHO - ME','GRUPO ATLANTIDA','DIEGO','14015-060','FLORENCIO DE ABREU 1843','CENTRO','RIBEIRAO PRETO','SP','09390150000123','582766275111','1630247791','atlantida@grupoatlantida.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (13,'ALFAPOWER COMERCIAL AGRICOLA LTDA','ALFAPOWER','JOAO MANOEL','14095-440','ANTONIO GOMES DA SILVA JUNIOR','PARQUE INDUSTRIAL LAGOINHA Nº 790','RIBEIRAO PRETO','SP','19435354000197','797038565110','1639170406','joao.manoel@alfapower.com.br',10,NULL);
INSERT INTO `anagrafica` VALUES   (14,'SOLANGE MARIA BOTTARO DE SOUZA','Ra Usinagem','RENAN','13470-625','RUA RIO VERMELHO 175','JDIM BALSA 2','AMERICANA','SP','23976609000104','165290281117','19996024850','renanaugustoa@yahoo.com',10,NULL);
INSERT INTO `anagrafica` VALUES   (15,'KILUX TINTAS E VERNIZES LTDA - EPP','KILUX ','MARILUCIA','14075-700','VALENTIM JOAO MORETTI','PARQUE INDUSTRIAL TA','RIBEIRAO PRETO','SP','67228585000172','582318254112','1636262531','kilux@kilux.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (16,'Elio Pasquini Junior','Aplipack','Elio Pasquini Junior','13478-130','Rua Saturnino de Brito 319','Jardim Santana','Americana','SP','11296459000109','123456789012','19996764847','elio@aplipack.com',1,NULL);
INSERT INTO `anagrafica` VALUES   (17,'DAIANE ISABEL DE LIMA RAPOSO LTDA','CYBERDYNER','RONE','14071-450','IRIA AMANCIO DE ALMEIDA','ADELINO SIMIONI','RIBEIRAO PRETO','SP','26705999000168','','1633258711','rone@backupbrasil.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (18,'CRM – MÁQUINAS E SERVIÇOS LTDA-EPP.','CRM – MÁQUINAS E SERVIÇOS ','LUCIANO','14085-150','FREI CANECA 930','VILA TAMANDARE','RIBEIRAO PRETO','SP','00438212000155','582393024114','1632368610','crmmaquinas@crmmaquinas.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (19,'R & R AMARAL PANIFICADORA ','SKALLA','TEREZINHA','14071-140','GENERAL EUC DE FIGUE - 899 ','ADELINO SIMIONI','RIBEIRAO PRETO','SP','08103321000123','582710154119','(016)3638-3289','panificadorskalla@hotmail.com',1,NULL);
INSERT INTO `anagrafica` VALUES   (20,'JHS SERRLHERIA LTDA - ME','JHS SERRLHERIA ','Fernanda','14031-012','NILSON SPOSITO','VILA GUIOMAR','RIBEIRAO PRETO','SP','09101941000196','','39195436','serralheria.jhs@terra.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (21,'DEP. PRIMOS','DEP. PRIMOS','ALEXSANDER','09120-305','RUA; FERNANDO COSTA, 118','PARQUE GERASSI','SANTO ANDRE','SP','04597598000107','111111111111111111','1149743396','alex.primos@hotmail.com',1,NULL);
INSERT INTO `anagrafica` VALUES   (22,'LAMIR MENDES ROSA JUNIOR','CARNES JAVARI','LAMIR JUNIOR','14061-280','PRESIDENTE JOAO GOULART','GERALDO CORREIA DE C','RIBEIRAO PRETO','SP','','','16 3969-3524','vendas.lamir.mendes@hotmail.com',7,NULL);
INSERT INTO `anagrafica` VALUES   (23,'Geraldo Magela Mendes Junior ME','geraldo','Geraldo Magela','14051-080','CARLOS DE CAMPOS 1140','VILA MONTE ALEGRE','RIBEIRAO PRETO','SP','25355163000118','797255694115','16-3442-3202','vitiss.rp@gmail.com',1,NULL);
INSERT INTO `anagrafica` VALUES   (24,'M.F.S. Fernandes Scaglioni Locação de Máquinas e','MMC','Renata','14090-349','JOSE MANCUSO','JARDIM PAULISTANO','RIBEIRAO PRETO','SP','19563087000133','797027002110','34462778','comercial@mmclocacoes.com.br',1,NULL);
INSERT INTO `anagrafica` VALUES   (26,'SF SOUZA JUNIOR CELULAR ME','PROSCELL','SALVADOR','14010-070','AMADOR BUENO 663','CENTRO','RIBEIRAO PRETO','SP','06893998000187','582671594110','1632366033','CONTATO@PROSCELL.COM.BR',1,NULL);
INSERT INTO `anagrafica` VALUES   (33,'Elio Pasquini Junior','EUROSISTEMA','elio','13478-130','SATURNINO DE BRITO , 319','JARDIM SANTANA','AMERICANA','SP','06258877869','18141993','19 2648 6931','elio@eurosistema.it',1,'diegojr20');
INSERT INTO `anagrafica` VALUES   (34,'CARVALHO COMERCIO DE ALIMENTOS EIRELI','JARDINS','Mauricio','13474-764','Rua do Plyester 149','lotamento industrial','americana','sp','04154503000172','165247145116','19','email',1,'');
INSERT INTO `anagrafica` VALUES   (35,'LAMIR MENDES ROSA JUNIOR','CARNES JAVARI','LAMIR JUNIOR','14061-280','PRESIDENTE JOAO GOULART','GERALDO CORREIA DE C','RIBEIRAO PRETO','SP','12614360000162','','16 3969-3524','vendas.lamir.mendes@hotmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (36,'CARNES SUINA JERUSALEM','CARNES SUINA JERUSALEM','GERALDO / BUENO','09260-140','JERUSALEM, 155','PARQUE ORATORIO','SANTO ANDRE','SP','14546948000151','626414800119','11 4479-9289','CARNESSUINAJERUSALEM@GMAIL.COM',1,'');
INSERT INTO `anagrafica` VALUES   (37,'ANTONIO GLERIA E CIA LTDA','GLERIA','ROSE','14075-070','CAMPINAS 1218','VILA CARVALHO','RIBEIRAO PRETO','SP','56895311000110','582276551110','16 36265134','gleriacia@gleriacia.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (38,'Construtrio Comercio de Mat Const Ltda','Construtrio','Caio','14094-164','DOMINGOS F. VILLAS BOAS, 66','PARQUE DOS LAGOS','RIBEIRAO PRETO','SP','11202263000108','582826091114','016 39652665','contrutrio@hotmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (39,'ROMASUL EQUIPAMENTOS INDUSTRIAIS LTDA','ROMASUL','FABIOLA','14176-154','ANTONIO GATTO JUNIOR200','DISTRITO INDUSTRIAL','SERTAOZINHO','SP','55863955000164','664022739110','1621053500','romasul@netsite.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (40,'Lima e Talomoni comercio de Pecas','Fox Comercio de Pecas','Marcelo','14051-350','SALTO GRANDE','SUMAREZINHO','RIBEIRAO PRETO','SP','10854787000102','','1630110382','FOXCOMERCIOPECAS@HOTMAIL.COM',1,'');
INSERT INTO `anagrafica` VALUES   (41,'Madux Cosmeticos Ltda','Madux Cosmeticos Ltda','Marcos','14015-120','RUI BARBOSA','CENTRO','RIBEIRAO PRETO','SP','24102355000150','797184784115','16992745364','rone@cyberdyner.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (42,'MADUX COSMETICOS -ME','MADUX','MARCOS','14020-260','PRESIDENTE VARGAS - LADO IMPAR','JARDIM AMERICA','RIBEIRAO PRETO','SP','24102355000311','797184784115','01636203905','financeiro@maduxcosmeticos.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (43,'SISCATI E BENTO LTDA EPP','SISCATI COMPRESSORES','ROSE','14056-110','MARIA TEREZA BRAGA CERRI 444','PLANALTO VERDE','RIBEIRAO PRETO','SP','71972129000100','582373380119','16 36397084','vendas@siscati.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (44,'DENIS CAMPOS DE FREITAS RAMOS M-E','PLANCTON','DENIS','14070-000','DOUTOR DEMETRIO CHAGURI 1069','QUINTINO FACCI II','RIBEIRAO PRETO','SP','08162420000186','797025296114','16 3638-7760','DENIS@DENIS.COM',1,'');
INSERT INTO `anagrafica` VALUES   (45,'elio pasquini junior','','Elio Pasquini Junior','20030-','Via Cassiopea 48','','Cassina de Pecchi','MI','1111111111','','0222222222','eliopasquinijunior@gmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (46,'DEP. PRIMOS','DEP. PRIMOS','ALEXSANDER','09120-305','RUA; FERNANDO COSTA, 118','PARQUE GERASSI','SANTO ANDRE','SP','30046217000101','111111111111111111','1149743396','alex.primos@hotmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (47,'FRB ACESSORIOS INDUSTRIAIS LTDA ME','FRB','JOSÉ','14050-350','AUGUSTO SEVERO 372','VILA TIBERIO','RIBEIRAO PRETO','SP','24515531000185','797197593111','1633291681','vendasfrb@hotmail.com',1,'password');
INSERT INTO `anagrafica` VALUES   (48,'CONCEPTA DO BRASIL INS INDUSTRIAIS EIRELI ME','CONCEPTA','Raquel','09120-080','LUIS ARMSTRONG','CENTREVILLE','SANTO ANDRE','SP','10912187000153','626781663112','1144574747 ','concepta@concepta-tk.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (49,'LAMIR MENDES ROSA JUNIOR','','LAMIR JUNIOR','14061-280','PRESIDENTE JOAO GOULART','','RIBEIRAO PRETO','SP','13137680156','','16 3969-3524','vendas.lamir.mendes@hotmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (50,'CRS Fernandes ME','MMC Trans','Renata Fernandes Batista','14092-750','CARLOS AUGUSTO BRAZAO, 1450','JARDIM CADACAAM','RIBEIRAO PRETO','SP','09491974000191','582891576110','1636181040','renata@mmcconstrucoes.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (51,'LOTUS MEDICAL DISTRIBUIDORA E COMERCIO ','LOTUS MEDICAL','Rafaela','09561-020','HENRICA GRIGOLETTO RIZZO','OLIMPICO','SAO CAETANO DO SUL','SP','09238679000126','','1142262702','rafa@lotusmedical.com.br ',1,'');
INSERT INTO `anagrafica` VALUES   (52,'W A FERNANDES LOCACAO DE MAQUINAS EQUIP E VEÍC M.E','BLESS COMPANY','William','14092-750','CARLOS AUGUSTO BRAZAO','JARDIM CADACAAM','RIBEIRAO PRETO','SP','19179830000156','797014179117','3627-8320','administrativo@mmcconstrucoes.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (53,'CARLOS HENRIQUE POLLAK 39387727866','','Carlos','14070-000','DOUTOR DEMETRIO CHAGURI','QUINTINO FACCI II','RIBEIRAO PRETO','SP','24950691000152','797568086118','16) 98113-6577','carlospollak72@gmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (54,'RAMON SANTIAGO GARCIA 37827703828','Vida','LAMIR JUNIOR','14071-450','IRIA AMANCIO DE ALMEIDA','ADELINO SIMIONI','RIBEIRAO PRETO','SP','29903585000131','','16 3969-3524','vendas.lamir.mendes@hotmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (55,'NATUROVOS','NATUROVOS','VALCIR','100000','SALVADOR DO SUL','S','SALVADOR DO SUL','RS','00000000000000','000000000000','00000000000','elio@eurosistema.it',1,NULL);
INSERT INTO `anagrafica` VALUES   (56,'Frigorifico Santo Antonio Eirele','Santo Antonio','Lenadro','09240-171','RUA JAPAO 1173','JARDIM SANTO ANTONIO','SANTO ANDRE','SP','25284560000146','626862760117','(11) 2839-8428','cgcon@globo.com',1,'');
INSERT INTO `anagrafica` VALUES   (57,'Car Works','Car Works','Rubens','14085-330','HENRIQUE DUMONT - ATE 407/408','JARDIM MOSTEIRO','RIBEIRAO PRETO','SP','36120041000169','20119128','36352860','COMERCIAL@CARWORKS.COM.BR',1,'');
INSERT INTO `anagrafica` VALUES   (58,'RAFE LOCACAO DE ROUPA MASCULINA LTDA',NULL,'ABEL','13070118','R DOUTOR MIGUEL PENTEADO','JARDIM CHAPADAO','CAMPINAS','SP','39897979000188',NULL,'1932552454','contec@conteccampinas.com.br',1,'1234');
INSERT INTO `anagrafica` VALUES   (59,'EMPORIO LORD DO QUEIJO LTDA','EMPORIO LORD DO QUEIJO LTDA','Hugo','14065-510','RUA ANTONIO GALLO','JARDIM JOAQUIM PROCOPIO DE ARAUJO FERRAZ','Ribeirão Preto','sp','39957677000158','797675254114','(16) 9104-5807','hugo@hugo.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (60,'ENTREPOSTO DE CARNES ANGELA LTDA','CASA DE CARNES ANGELA','BUENO','03985-050','FREI RUPERTO DE JESUS','JARDIM ANGELA (ZONA','SAO PAULO','SP','39977777000146','130192761119','11 2379-4347','cgcon@globo.com',1,'');
INSERT INTO `anagrafica` VALUES   (61,'CYBERDYNER SISTEMAS E AUTOMAÇÃO ME','CYBERDYNER','RONE','14071-450','IRIA AMANCIO DE ALMEIDA','ADELINO SIMIONI','RIBEIRAO PRETO','SP','41575516000141','','1633258711','rone@cyberdyner.com.br',1,'');
INSERT INTO `anagrafica` VALUES   (62,'oliveira repres com e com de materiais ','Oliveira','Oliveira','14079-318','VITORIO PASCHOALIN','JARDIM FLORESTAN FER','RIBEIRAO PRETO','SP','40065454000165','796768936110','16981714012','oliveirarepresentacaorp@hotmail.com',1,'');
INSERT INTO `anagrafica` VALUES   (63,'DOCES LIDER COMERCIAL LTDA','LIDER DOCES ','RONE','15250-000','SÃO PEDRO','CENTRO','UNIAO PAULISTA','SP','48560104000187','11232154823','16981769009','TESTE@TESTE.COM.BR',1,'');
INSERT INTO `anagrafica` VALUES   (64,'Danilo Castilho','Aplipack','Edimilson','13477-230','EDUARDO MEDON 909','SANTA SOFIA','AMERICANA','SP','33478578876','165366464114','1934693141','financeiro@aplipack.com.br',1,'dqtc1304');
INSERT INTO `anagrafica` VALUES   (66,'EMPRESSA X','XXXX','119962350','13422252','RUA 190','CENTRO','LEME','SP','101584263000180','6626158','19336261525','X@X.COM.BR',6,'115599448');
INSERT INTO `anagrafica` VALUES   (67,'COVRE TRANSPORTE','COVRE','1199625154121','1362512','RUA 19','CENTRO ','LIMEIRA','SP','1049866300152','448151','1188541251','CONTATO@GMAIL.COM',5,'11622612');
INSERT INTO `anagrafica` VALUES   (68,'APLIPACK','APLIPACK','ADMIN','1362512','RU JOSE DE FERRAZ','CENTRO','AMERICANA','SP','13552165220022','66954845','19999999999','APLK@APLIPACK.COM',3,'66632626');
INSERT INTO `anagrafica` VALUES   (69,'APLIPACK','APLIPACK','ADMIN','1362512','RU JOSE DE FERRAZ','CENTRO','AMERICANA','SP','13552165220022','66954845','19999999999','APLK@APLIPACK.COM',3,'2222222');
INSERT INTO `anagrafica` VALUES   (70,'TESTE','TESTE','TESTE','1231331','RUA TESTE','TESTE 10000','TESTE','MG','16261555','87654654','78945654','YGGGY@GMAIL.COM',6,'123456');
INSERT INTO `anagrafica` VALUES   (72,'APLIPACK5','APLIPACK','ADMIN','1362512','RU JOSE DE FERRAZ','CENTRO','AMERICANA','SP','13552165220022','66954845','19999999999','APLK@APLIPACK.COM',2,'');
INSERT INTO `anagrafica` VALUES   (76,'APLIPACK09','APLIPACK','ADMIN','1362512','RU JOSE DE FERRAZ','CENTRO','AMERICANA','SP','13552165220022','66954845','19999999999','APLK@APLIPACK.COM',9,'');
INSERT INTO `anagrafica` VALUES   (77,'APLIPACK09','APLIPACK','ADMIN','1362512','RU JOSE DE FERRAZ','CENTRO','AMERICANA','SP','13552165220022','7784512122','19999999999','APLK@APLIPACK.COM',9,'');
INSERT INTO `anagrafica` VALUES   (79,'APLIPACK','APLIPACK','1622251232','13558420','RUA DA APLIPACK','JD AMAZNAS','AMERICANA','SP','1049866300152','8889116178','13111212123','CONTATO@GMAIL.COM',7,'1111111');
INSERT INTO `anagrafica` VALUES   (80,'COVRE TRANSPORTE 44','SOUZA BACK','16 222695945','13200002','RUA DA APLIPACK','CENTRO','AMERICANA','SP','1049866300152','448151','19999999999','APLK@APLIPACK.COM',8,'Aplk@123!');
INSERT INTO `anagrafica` VALUES   (84,'IBM','IBM','IBM','1366251','RUA DA IBM','CENTRO','LEME','SP','330215411022','2254874','19632654211','IBM@IBM.COM',28,'2261548');
INSERT INTO `anagrafica` VALUES   (95,'Roda Brask','Roda Brask','1548484851','13022202','Rua da Brask','Centro','Mogi Guacu','SP','1471000021584','996584211','025888545132','brask@brask.com.br',29,'222334455');
INSERT INTO `anagrafica` VALUES   (96,'TRUMP','TRUMP FANTASIA','666666666','136666','RUA DO TRUMP','CENTRO','UTHAN','SP','66652540085','12555412','1188754121','TRUMP@USA.COM',NULL,'123456');
INSERT INTO `anagrafica` VALUES   (99,'TONHAO DA LUA','TONHAO','19448512511','13413625','RUA DA TONHAO DA LUA','JD PEREIRA','BOTUCATU','SP','1688927000010','6663265111','15777878454','TONHAO@IG.COM.BR',31,'');
/*!40000 ALTER TABLE `anagrafica` ENABLE KEYS */;


--
-- Definition of table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
CREATE TABLE `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetroleclaims`
--

/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;


--
-- Definition of table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
CREATE TABLE `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetroles`
--

/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
INSERT INTO `aspnetroles` VALUES   ('9e44d79e-5a21-43e0-a396-05cb1d058676','User','USER',NULL);
INSERT INTO `aspnetroles` VALUES   ('c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a','Admin','ADMIN',NULL);
INSERT INTO `aspnetroles` VALUES   ('f23a050c-a1de-4f33-9a17-170ea452d2e9','SUPERADMIN','SUPERADMIN',NULL);
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;


--
-- Definition of table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetuserclaims`
--

/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;


--
-- Definition of table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetuserlogins`
--

/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;


--
-- Definition of table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetuserroles`
--

/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
INSERT INTO `aspnetuserroles` VALUES   ('030fc77d-258c-4c12-b7e3-9cc35538972c','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('03dc239f-9302-4c1d-946c-16b719b69bbd','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('067f407a-345b-4031-ab57-f3b6b622a8ef','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('083cd21e-ff92-4942-84a7-c69fa5c78085','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('2c4b8ef7-cbbd-4fd3-a14f-75fcb09b5bf0','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('2fcf3116-18d2-4734-8f55-e4a19a68383e','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('31e4aa2b-bc1d-4dce-8b98-6fe2a285ec7a','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('35c681b8-353e-4c9e-968d-ccc926ad0c0f','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('3c0bc722-5671-415c-8ac7-8b465ccdd6e4','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('554612bd-6dee-4864-8bc5-b640d4cb6fca','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('612d93b1-ae3b-4cfd-a36e-f196c1351cc2','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('6b12b225-c444-44db-b876-65f73774d0f5','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('6ddf277c-ab16-4823-a9bf-e11d94965baf','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('76fc3ed9-9254-42b7-a73a-94b9c22cc98e','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('8cfc3d54-0283-460a-95b6-34d62e8c951f','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('9939a52d-5589-40d9-b2e9-62daa9bb5108','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('bb8b3c51-8dcd-4963-a9f5-31d2d9ee930a','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('bba72af2-9f09-4e32-aa9e-97f738f36613','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('c368d3a8-6694-4a38-98f8-b74c8598b5a4','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('e674be10-3fcb-4c8b-8afb-04fb9c67c96a','9e44d79e-5a21-43e0-a396-05cb1d058676');
INSERT INTO `aspnetuserroles` VALUES   ('1ed06ba4-061a-4bee-a517-6133e26921c3','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('35eb1e83-9c9b-4de3-bdca-61a23ce1bc1b','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('40c85ae2-431c-4d56-8a34-c72c0c357d93','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('4153623c-24e5-4bb8-a2d7-4a5d867e3280','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('63bea60d-4e02-456f-b6a1-af070a289451','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('74e05602-dd0f-48f5-a840-c072c51a66e1','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('7f10e2b6-b8ae-406f-8f13-3f5931c6f83f','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('85a987c1-afa8-4c9b-8388-0992b6da4e8d','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('e986c6f4-d5fa-48f1-b8f7-1e70bb4fe8bd','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('e996adf3-d9d4-44f9-928d-91b8883b2afb','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('eaa0251d-0a93-4118-bbcd-fa3114420e21','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('f5e8a293-ae4e-4f06-a8e9-885aeadbda3a','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('fe4ba316-96b9-4035-a26d-988630abae89','c8e65ceb-9a56-4d7d-bc88-7aa44fb2403a');
INSERT INTO `aspnetuserroles` VALUES   ('236abb27-6234-4f71-8b9d-f41e03940547','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('2531c926-4b91-455e-b08c-d3e8ff907ec9','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('35eb1e83-9c9b-4de3-bdca-61a23ce1bc1b','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('36138686-e7e9-4975-8779-94829cff1e05','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('524c66f4-c370-432f-8cd8-e98e625e0e89','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('7a68f374-8546-4543-8327-8898ff6b8819','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('c185a539-7736-4166-99a2-42ea9cf9aa56','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('d1ab6a5d-02ce-4dae-a2c7-d47aafa52c47','f23a050c-a1de-4f33-9a17-170ea452d2e9');
INSERT INTO `aspnetuserroles` VALUES   ('fab1b6f3-9a28-4bf6-805f-b2b7df63bd45','f23a050c-a1de-4f33-9a17-170ea452d2e9');
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;


--
-- Definition of table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
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

--
-- Dumping data for table `aspnetusers`
--

/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES   ('030fc77d-258c-4c12-b7e3-9cc35538972c','juvenal','JUVENAL','juve@aplipack.com.br','JUVE@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEF/kN2vkMec+g7mAGbZbfDp/N+zxGVwypklVzTA4Q6ZkdEXIsM5qf0lnJewxGu6nDQ==','7SJ5EETIKBNQIOZUCEXDHE7KPXNSGU24','bb1d3714-5ebb-41a4-b73a-812536fe8c1f',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','7YHGGYAG',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('03dc239f-9302-4c1d-946c-16b719b69bbd','pedro','PEDRO','pedro@aplipack.com.br','PEDRO@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEM82LjhtNrTcKGLSsbkZ+AAm3me63j/vL7E4uaOOM2y9QpidPqmVm4+FtHmwbTSrTg==','E625PNOEYGLQFN27CISJDUGUUY64TENN','e0e6116a-6bf9-46fd-b45f-1ee6c6b179b8',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','888UH7YYG',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('067f407a-345b-4031-ab57-f3b6b622a8ef','guilherme','GUILHERME','gui@aplipack.com.br','GUI@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEH0XRBX1zhkloWrcngIORAiqCNSi82832SJZ29mlgNKU0Xw0hKJt8pShr8uNs0HdMQ==','JGQTM4GW74XY4AQZXDMBGEDS65J4GTBH','4de7f7a4-cff1-4dde-901e-d55ff0c1f92f',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','777UUUHYG',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('083cd21e-ff92-4942-84a7-c69fa5c78085','Bruno','BRUNO','brunete@aplipack.com.br','BRUNETE@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEATqDIxJvyfoiCe9RhhSG83IcdOBV1LMo2A3XrUd9+4paP7F2+RPM2pccSRPNsfF1g==','RATCVDWCDHAOIJCAPUDPPP3V7IOKCWVK','ca716aa8-2bae-4fd1-9380-804e8acf4420',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','119I99IQU7YBA','QJ5iKhCmSQMarpXAZcYuFdiGQR9XpNZdXHH19Dwt+noDwHUhFKbmDdRwVbGb3lz/1liYX0UYOiz7kwjNxfS/VoYfVpl+cvcwLSPj9vVhAdq5DtsJvVlPma6EPZk7fgfKqxstkFvRJ6CtaLBT/vcosuyNsGkiSu76083gLSOsP6M=','2025-08-18 15:35:00.799706');
INSERT INTO `aspnetusers` VALUES   ('12044b85-6efa-4ef0-8443-ad77ccc0598c','moraes','MORAES','moraes@aplipack.com','MORAES@APLIPACK.COM',0,'AQAAAAIAAYagAAAAECFqsIw2LQ3YI3mtxxE9XOFDkC9d6/agAtRajSl95bF7XUhv9EyhWGh2EgGwnIj65A==','VXRVVKFGPX72DPK2ITF5T7HJQ3KPXQNQ','c9c4c500-a08a-4b18-ae19-edc4b6cc68a4',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','99IJA8UUHYSG','q2Q1LCQA3CXbBY/qg5gXVHnxAn0uVsr+anSfPcPecsCwcp7hYBBOr9h+N1sn5hGQ8Gz/XxOYpiP6U9KqwpwcaQecPQ2GOtOVUdY7Hcxb5SxIg7FTxVSDATTbKJOUzaGBuGL7KOvnldjfZOGZaITYE/M850WTW+z6s9Y68WUCHY8=','2025-09-09 14:10:01.621820');
INSERT INTO `aspnetusers` VALUES   ('1d4ecf76-759f-4bf1-a1a5-3149d801f0f8','pool','POOL','pool@aplipack.com.br','POOL@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEHwNGo2RtvkjWVbultiSiw2/GV/pYcbv5DoK+msivmGE/D9hXjuK3+Dey/655iLvkQ==','TKYVYH3NYIFNOJJTL6OQX2U2TFADX7FS','d9f5f82a-f8dc-47c2-97a1-ac3ec65021a7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','1678QYUIQQ',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('1ec4a5dd-159c-4e5b-841b-4c80c1b4541a','souza','SOUZA','souza@aplipack.com','SOUZA@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEGYSVzRqJ+Ae9nnb4Fj/AmrtuvboYleEIfQx2SELQfQMyBStVQRwdwL3bVrNkXVJcg==','XMUWGVTPBEEJ47D7MWUWOFGVBN4R7M2D','068dd0fe-1687-4be7-a3ac-082686df5242',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','4RGB7YGV1S88','yllghnlu8ULaLflQAjYNgkQek8r01lnyu3Thf+rpzCCyMrdM0Xm4uI/4gAHt6+Uk1ms0cqKKpJZi9AcUy+2moO5GHr5iPwSlwhz7xrKBFoCdEs4xBERu2hrBFM7aSYleyBYToL386rkIxEEPQfDtnrHIXqLesrduN1ang2+CmTE=','2025-08-20 12:30:51.797575');
INSERT INTO `aspnetusers` VALUES   ('1ed06ba4-061a-4bee-a517-6133e26921c3','zebra','ZEBRA','zebra@zebra.com','ZEBRA@ZEBRA.COM',0,'AQAAAAIAAYagAAAAEJfQ7HciXvrXE4KwiO+pmAM3Mw0ekbTP0sjHTHwHb3ZPMnZyH0ZikfSpDmq/1J672w==','SDT5PU7DOCZMDICX7QFJOZKFB2MUBTD5','8d42415c-3f5e-48a7-82c5-624281a66376',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','34543ADFD',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('236abb27-6234-4f71-8b9d-f41e03940547','danilocastilho','DANILOCASTILHO','danilo.castilho@aplipack.com.br','DANILO.CASTILHO@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEEb2g+UXmXy7nDTNHbDwrK+dfDNwx6yd5C9nAcMkuDD7Ojs7mCQn1Rqnb3NUf/jkEQ==','DRT2CR26S2R6LHJHCCYROQULVU7QRORU','82ce9436-0840-49e3-a11a-e7192dbe346f',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','1234','Tb4b8PuskL2P2rgBJu4O4R+VwoK2hhlbtL1S3yUxZbGcwJoYRgtLx7awe1vanBwsSbFtK8AAurK9i0zcPQ4hbwBuDjZ8/m1hUwR/fU+0ITjOP9nkq9LzGIf58T+ogOxUZL8KBxSdXo9d6+oY3W6N86Dpbuq6rTJAhM092txlTvk=','2025-09-30 15:26:13.618537');
INSERT INTO `aspnetusers` VALUES   ('2531c926-4b91-455e-b08c-d3e8ff907ec9','joao2','JOAO2','joao@gmail.com','JOAO@GMAIL.COM',0,'AQAAAAIAAYagAAAAEOWVxYwVUlJTv5LdTsza8hbqcS/Ndl4jfvCIB4hWYjkKI/4xEIn5GJ//NLUEWvif0Q==','SK7V72EKIMOUXYUEBSBTC4W7RS3VPBSC','c0c9beb9-921f-4445-8995-913f6ca65a76',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','1345asy',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('2c4b8ef7-cbbd-4fd3-a14f-75fcb09b5bf0','fernando','FERNANDO','fernando@aplipack.com','FERNANDO@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEE3NCmQ5ic8NBhxwFNGdOOUKwAO9ljr8ex/UbhjuDAJct1I0/5tRC55L3qrjDXoRFg==','BDKVJDDTTZAXRLTRBCYL2CSG6BHY4FW7','50a87961-e4eb-47a6-bbeb-dfb0fbecaccf',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','8uhsy7t88','J1sARFhsUTkGe8F5md7LsDsRqLVH/MZf1zRJIEUoBkRl6n2NIQAniCVD/V9nhiB2m869lzxPguqxyUWUolMgj6UhX5nyjf+kgNwIG6r6xqbEnuML0dEV+ws11mpuP0up94bZUAYd/qr8Y3Ubm975krTGOr3RtscCJ/h4/c2iHFI=','2025-09-09 17:52:41.475856');
INSERT INTO `aspnetusers` VALUES   ('2caf65f0-3bfa-4a5d-ab54-3a1ee6536ea7','jose','JOSE','jose@aplipack.com','JOSE@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEOHq8I5q3ZQOemF+MgNkQxb9zgbye69IeEfI4fb0FvI/Ga8ztJZQujlSZuWUMXRj7A==','AZ7E2XWKRXJZBKOW2U3VZMPUUUJTHNR5','8e836e1a-7f8b-4a7f-bb62-0804f0dd4dde',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','666TA6FAGFF343','umt7RMFZ693Wvxo5NGFlygmgW8+kAILzUugtzBszMTHB2kyTNqTb2/133lmsoVMREpR1kraDhDeJaGl3g64IPSpIMUakq5TAlbvbr8J3WMBl/m6kMmrSEDDYV3L9CIV6NyiuqnrmJunuKRHVl8GATnJkhc19OA4DGw0BpDzo+DY=','2025-08-27 17:53:35.682019');
INSERT INTO `aspnetusers` VALUES   ('2fcf3116-18d2-4734-8f55-e4a19a68383e','fiales','FIALES','fiales24@aplipack.com','FIALES24@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEEu9rQKCqdJHNBmQNlnl+12l9dqrCEZhn8tfLRJnt1K+CHhb9/zCpTZss30kbNe8Nw==','NYZLUN7QBPARYXEJ4ZCUUXWPKPXA6SWH','87210aff-1c55-4797-8baf-898bebbea0a5',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','5165243615426','idyi/X9BA9uWgWJelt0j6eDOsLlNxSxyQfHWQ3S7IKKqk5puuVJvbnnkVKbSQ8zEr27jp/e5Nw/0SrGOytRzu6nxvcGX/CAcJyJLeM4O85Si/eGk7A7yWqsBBtyrPWy4HxJEytWQ16soVnY2t1W6wXRJQ97SJOTvlKLJpDOlytc=','2025-09-26 12:40:05.148000');
INSERT INTO `aspnetusers` VALUES   ('31e4aa2b-bc1d-4dce-8b98-6fe2a285ec7a','matheus','MATHEUS','mathe@gmail.com','MATHE@GMAIL.COM',0,'AQAAAAIAAYagAAAAEEJI9eUg0TAgnCLSEcglxQCjPe0OMhzstgRxKUp7/xXEIEZMgGA+xLU1tSrnQibcAg==','OGTIHIXNINGSB55N3FSP2PQGCFP2V4AD','9756b1df-7e35-46f7-a92d-ead8bde266b7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','5542565aa',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('345004eb-da56-40dc-84fb-a5bb5b558df0','gabriel','GABRIEL','gabriel@aplipack.com.br','GABRIEL@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAENCrUY1ZY/rhnsyLkT0dz8PBHEj8n/6LNGc/cWvgcZGqWl8qFewK0S+At//IwChjag==','OKXOIS2ERRPEJ5UVHJPZKXNQKZXSTVHR','694aa8f5-2d96-4fd3-950f-18eebfca7cb4',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','8UUHYAGYFFF',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('35c681b8-353e-4c9e-968d-ccc926ad0c0f','betinho','BETINHO','betinho@gmail.com','BETINHO@GMAIL.COM',0,'AQAAAAIAAYagAAAAEMFVv8C6y5927Crlp1ukVjGkjYOfAPxKtVYHtd3lzRRBn8VzARfLiFC3lynrG+1I6Q==','JPT3JLXTY3VRZLXYNCX2RPVFI5HKLCXB','f1b531fe-70df-497b-8265-da71d9538c7e',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','a6tgftaf',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('35eb1e83-9c9b-4de3-bdca-61a23ce1bc1b','root','ROOT','root@aplipack.com.br','ROOT@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEPuIwSXI7mRPtCncpR2JGOTUE5rDNQnsjPvXyTKwAuXZg3/lK+ghnix5OGjYKGYD+Q==','FSXZAEMPA2WEQZQS7DWKCIQTQCEBQRYW','8d47316a-34c5-4ca8-99ee-103e7c1ada4e',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','HUHUA7YAT6FSRD','CZCoj5aTy46uVGhytDAGLf0AmfbLWdnfd1/C7ESZ0KB93S+1XtrBgrhxG4Q6m5W3uEnwEGjjQfdxEWiBBHS4WgXEyZ49DNcNpnZCjchSBykh5M5ADWvUUeeMi8sYEpTETkjM/HJGLGk8o5d7D0Mljio+f+5FwGqLMIcJgd5PDCA=','2025-09-18 16:58:17.589708');
INSERT INTO `aspnetusers` VALUES   ('36138686-e7e9-4975-8779-94829cff1e05','clio','CLIO','clio@renault.com.br','CLIO@RENAULT.COM.BR',0,'AQAAAAIAAYagAAAAEKoiqWz04AwemGyHzo2vQeuvHn5W2+eEdYnfGs4ccROEY6L7MK78bBPVKlojSeJsXg==','O3EZ2XSD2SNABR2SYJCFSR37ZRGKAINQ','f0e624ed-8e1a-47b5-8966-c67f0f755c3e',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','55656AG',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('3c0bc722-5671-415c-8ac7-8b465ccdd6e4','fifiBalls','FIFIBALLS','fifi@aplipack.com','FIFI@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEOAQrAfjYYqHgeB1aBlL+KHnfAmvspcuF6G6xTOYWqnKTNqSLBpYs+6w2udZw/BbIg==','VLDTJXZAYWM6JBBMX7LOR7H5HM5MZHMH','98adba3d-20d0-48b8-b52b-4415e41b37b5',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('40c85ae2-431c-4d56-8a34-c72c0c357d93','luis','LUIS','luis@aplipack','LUIS@APLIPACK',0,'AQAAAAIAAYagAAAAEK1y6h9DZ9A6T8WqmLuFpR3GKK48AyyGhskT18GR3vkARO1eRTMzZZ70Yw5O3uJ4/w==','5LZNEG4TICVZB6JC4HYXMM5U7CCVXS6B','d883b993-23ba-44f5-8907-9b4d55e2dd90',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','8988989HU',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('4153623c-24e5-4bb8-a2d7-4a5d867e3280','claudio','CLAUDIO','clau@gmail.com','CLAU@GMAIL.COM',0,'AQAAAAIAAYagAAAAEFiz43dAe7wa9GsM+qwJBeydiFTRxfmxhi1YW4NAwfYWjU97JU/Y3lcTAxhmyLTCeA==','DVVT5XYJCSLHVNTU25T6SFIDYBXRM4PY','22545fc5-1c3c-4a57-9239-76a3c577e23c',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','IJJIA',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('524c66f4-c370-432f-8cd8-e98e625e0e89','63232','63232','63232@gmail.com','63232@GMAIL.COM',0,'AQAAAAIAAYagAAAAEG0tu5LD0ezJZf3+8mqH81xCIXXnMZx9TFSXhMxiGnpSCvA4jdKLsrliD/x7Yc0dEw==','KRFRDONJQTTG2WYFKPZHJHQHKJZQNOIX','f4f92346-a3bf-4a5c-a9f8-f46b967cca8f',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','7asidyagsd',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('554612bd-6dee-4864-8bc5-b640d4cb6fca','judas','JUDAS','judas@hotmail.com','JUDAS@HOTMAIL.COM',0,'AQAAAAIAAYagAAAAEB64f9mXP4em5iX+3LUzdmRacLW1LPapzIAQ51jYUp502Iwr16miMq4yME+UVeel1A==','EXWYWG34GHSV2LYAQ6G7RIVXGZLL6IRP','02099d54-7c18-45dd-beec-e42f4cd6a233',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','88UJHAH',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('612d93b1-ae3b-4cfd-a36e-f196c1351cc2','mickey','MICKEY','mickey@aplipack.com.br','MICKEY@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEBgC3vJFXIUhOhYpv4vEBUYfj6VEafPK93h3skA4VX545hC193LXSjmdqviMqtg82g==','YQHWLOSRF6SBXBSMDFLHZYAECXL5SIYV','cfbd33fc-c2ee-40e2-bff7-9e6638a520d7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','000OOKAIJ',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('63bea60d-4e02-456f-b6a1-af070a289451','judas3','JUDAS3','judas3@aplipack.com','JUDAS3@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEPxdj4NDQCGpkGiXWR3Wi3C63kniTha7tzQ2evn8ou3F/8gRDUkI1UqB1S/b0Nb3bg==','ML3IEHR6LEU7VV3VDMYTCC6JV6BTHC3U','1b892741-8f1b-4735-a358-5e6f05bd329f',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','67876abh',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('65090071-1208-4370-a96d-0598f1133081','MARIA','MARIA','MARIA@APLIPACK.COM','MARIA@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEIo50Fl1RRi5d39l/ll8LCl2sot+QNy659I4K63ESphjDAZNabcPwSNNzx+40JBSYA==','5MVHMFM6HG72HILF7RDEQD3NNQFHPNT7','d9dde005-c732-4429-9db8-8d364169e185',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','0OK99IAJUHGB',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('66d043d6-71cd-45be-8e28-4c996a132b3d','maycon','MAYCON','maycon@aplipack.com','MAYCON@APLIPACK.COM',0,'AQAAAAIAAYagAAAAELmA1NQP0OSCdkEkhub/ZqFmv1wlLYdJAReGqQ38s4NFpDMqBghjtqILAXPYCd4AUg==','2SEA5MNVHCNW6MP2JHS2Q3LJ6NXWLBXP','f81e33de-60bf-4266-a50a-d2dae4f9940c',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','142525ETRYUU','d8pXnO7f6xc7uqGfmNZrqk4sv9kLAO5PR+QRBgN1bZDWKLZanBvrfnGikqhRZMs+orEmI25jqLZJvzxa11QlZvWcY6mB1L+c4IENOCleSiPqGcYdPwkpWv/I4LiyeAjxMghPKw9JqUhN7enHVISBR4uUwJelx3BzXVrFwH0fXnI=','2025-08-27 17:18:04.224427');
INSERT INTO `aspnetusers` VALUES   ('682245fd-07bb-486b-b034-4d42b2cb532a','jair','JAIR','jairzinho@aplipack.com.br','JAIRZINHO@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEGQnQd5r84gNIyXoCldemKKM4xSTM8sBY+CXyYai9Jyp7sX4vKEH47/nxHY+yeWqIQ==','3HGQWLAFLTSZJXURTBJWLYRMLLZH5ZIQ','003f65a0-98cb-4d98-854a-579da3d6b9de',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','66TTAYGGGYA',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('6b12b225-c444-44db-b876-65f73774d0f5','huhuh','HUHUH','huhuhu@gmail.com','HUHUHU@GMAIL.COM',0,'AQAAAAIAAYagAAAAEKVPoHMgeZyio5wgSIjEfMz6vFfH9+a+nqGB3r9cwjLi1sIJrbMOdXYdZ2BjmrwNHQ==','BFNP4TGQJ6A7RLEQRIY7M5Y5C62CDL6O','c8e3492b-8919-4c87-b01c-a2218e14b6fe',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','88uhhau',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('6ddf277c-ab16-4823-a9bf-e11d94965baf','carlos','CARLOS','carlos@aplipack.com','CARLOS@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEEd9vgKs2rCNdHSWuh8pMSWbDckw+yNNYVHFVk7dODpLtTunAWIrhmJGnLjQQVVk7g==','6QAFRIYIL4CSIOUSSHL7TUN3EN3FC5HW','d10ca82e-f01a-4088-bfdf-27bf4b4eb643',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','45454AFTF',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('735aa1d2-6aea-43cb-922f-ab19e8a103cf','deed','DEED','deed@aplipack.com.br','DEED@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEFL4vV8azDhKgvu/Q4v23DRIcyNvuo31MpEqi5/novvpazDFb+xOIEBwO5xNf1x2ZA==','P342FL7RZFKOWZDGRBTVDRSABK4HDLV2','7ab9b643-938b-4980-8fb7-b28ff6f0af2b',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','1UHAYG',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('74e05602-dd0f-48f5-a840-c072c51a66e1','silva','SILVA','silva@gmail.com','SILVA@GMAIL.COM',0,'AQAAAAIAAYagAAAAEDoWRnR4qNM62OVfDUaXFYCQnWtMIARjPzqBqd87HVJOfcM/tsHXdDZ+SvZ1/UGPTg==','ZWNFLJCAFZOR74PEDFDI4NWNT5RI74VF','d81a1c22-9f96-4ece-84ad-7b83504f7063',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','IIJUHUA',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('76fc3ed9-9254-42b7-a73a-94b9c22cc98e','trreg','TRREG','treg@gmail.com','TREG@GMAIL.COM',0,'AQAAAAIAAYagAAAAENFaJyEq76otbvh2sBY9QU8buW9cxyUpoVGEIrnUpvwk1fHlUouJsAxMlug3ESUKsg==','Z3HSEOC55QZ7CVKQAULCVPN22Q3ATCET','0c00f148-dd2c-4133-a013-f6f422fe1fd7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','99i9iaj',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('796a24bc-7d96-40c0-a91c-d53c37ce5b19','kelly','KELLY','kelly@aplipack.com.br','KELLY@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEECVdeBzH1AdfwWZwM4Wbs6EyeQ3OzWUAVTGHultrfv/wRh67QhBqi+Ql94zEh0NDQ==','6YHBBURQWR33EADORVPLTIEC7WRTC73B','b2f87f9d-e9e0-4d21-a55d-bcf95c30b179',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','plmkojanjjj',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('7a68f374-8546-4543-8327-8898ff6b8819','marcos','MARCOS','marcos@gmail.com','MARCOS@GMAIL.COM',0,'AQAAAAIAAYagAAAAEKwVSxbRlidBWnYWi5EC8yx46uPc008emIJnrpNQ8naPqMPEBxLnuwpPkJHUPMLMJQ==','XYURMIO3HVH2DYLX2VHSVESUSJYJUX3A','bc970414-6b53-467d-807a-f3e498ce25e7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','66yhagg',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('7f10e2b6-b8ae-406f-8f13-3f5931c6f83f','manuel','MANUEL','manuel@gmail.com','MANUEL@GMAIL.COM',0,'AQAAAAIAAYagAAAAEL4DfSAuvEJDE1osk81SbHgydqxQPTv4KN2Nws84Q1I8vMB7SYAz0UockZR3va55lw==','IDGFIALZ4XFZG5EVWKRKX5J77JFULBKF','ed9e301b-35c4-4fb9-a42f-e3713bd3992b',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','a6ttgft',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('85a987c1-afa8-4c9b-8388-0992b6da4e8d','000000000003232','000000000003232','0303030@GMAIL.COM','0303030@GMAIL.COM',0,'AQAAAAIAAYagAAAAEOTzgyBbXw4dIqcOHyXr+XXCL3t36hya1edCGdZdprjJjn5ciSzqLGxtRV5Srdsdww==','RD5W6QL2RPJR2FUEOW6LHMZGTJWFSZMM','e5c2f8a1-9058-4dc3-8b83-dc21a04d62e8',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','00909A0SD',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('8cfc3d54-0283-460a-95b6-34d62e8c951f','ana','ANA','ana@gmail.com','ANA@GMAIL.COM',0,'AQAAAAIAAYagAAAAEKqpdcnwpWetzLSSDkcfmZHPdK/s5PO8DYmRCjC/wUu+Xdi3aO/jrQm1jSWEX3aYyg==','MYMUPSEYVX5P2M7IDZZZH7WHJI55AXCC','c5c2e669-53ca-4664-bc5b-2c790c0e213e',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','6YHGAGT',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('9939a52d-5589-40d9-b2e9-62daa9bb5108','peep','PEEP','peep@peep.com','PEEP@PEEP.COM',0,'AQAAAAIAAYagAAAAEOnhAEOhOEyiI7nYGUYeN9IlrdyisW31/B6ZEkoG05BtXsW+XdCpUMk2hPFasekUUw==','ZTXBAG4RWTICASBBJNKHJWSEHWEEV56M','a60910b9-69b0-4061-be1a-18cde607237e',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','Uuiahg78',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('ab5a9a0c-6512-4188-b630-5cb415c30766','julia','JULIA','julia@aplipack.com.br','JULIA@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEG57O9ht3wAimARCKV62xzFUT6xsGLcgtSUNyNU7fQHpZ8+W54jJ0QjgU4zNsV0Mww==','ADNMYURGNKUBRRSD3XHNNMKQSG6UTMAC','181640e6-404b-4c20-b6c2-24a618995921',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','119I92JJHU7YBA','nfqQHznZk7LI/C/QcFwES1bR53XMXWUpzv1YG2ni8GYdrwKDoGtmMRhXAgpRfdwOd6oWmmZHJTk1GqxIqokjKDl9tLYeDftsRLdbeNafBcQ0pVasITcdJWJ1OBdxAfbpoUm4Xrrj4HESLMwWAMnzL0+oHhzaIcP/q8R2F4JR2Sg=','2025-08-22 15:11:29.546025');
INSERT INTO `aspnetusers` VALUES   ('bb8b3c51-8dcd-4963-a9f5-31d2d9ee930a','bartolomeu','BARTOLOMEU','bartolo@gmail.com','BARTOLO@GMAIL.COM',0,'AQAAAAIAAYagAAAAEKTaJRD9mM4/bWkraPveCaRLYBfSzVkMXSfG451llaG8iEX+YFmhwsXv6kd7bx3rKQ==','6BZ4Q6NZQQJNRGUQS2AIE5QE7X6LUK7X','5e3dfa6f-77de-4426-93fc-8241cb826f9b',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','7YNJABH',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('bba72af2-9f09-4e32-aa9e-97f738f36613','souza2','SOUZA2','souza.ga@aplipack.com.br','SOUZA.GA@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAECMPmRsrE/AucNPMJKtjjv1VNCVwd02DMS5raDshBjf0JpICGiPpfr4AXaYK29p64w==','RAVKAG3ZX4K5MHXDPT5M67X22HL3RT3M','d49ffc5c-37a6-48fe-bca8-a76fa1628079',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','787A8S7D8',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('bc771afe-300c-44af-98d1-fcce05bbc000','gabriel2','GABRIEL2','gabriel2@aplipack.com.br','GABRIEL2@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEIufCmB25uRY9mNK0cRczRG+so8UHwyAg1Joj09PlgUIwBnd9xpyQMh1Hp8dM1jIaw==','6PBISN2HQT5UGSI4DS77OLYJ7JWQZ5AY','b2e19225-9066-4e18-9768-cc5c4438f6ff',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','666dtsfgfa',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('c185a539-7736-4166-99a2-42ea9cf9aa56','indio','INDIO','indio@gmail.com','INDIO@GMAIL.COM',0,'AQAAAAIAAYagAAAAEKhpevidMyS0uPbx2zopi92M5utxhrkwrhQG3azT9E/BPTwpcl4zaUqN4lsFdO+rZw==','ZLDKFX2WNK247UBWQTSPV3CQ2IHFTDYN','9a547773-250e-4148-8d5f-1d529ce9abcd',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','888uhhau',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('c1d10662-8f07-4b33-b440-41d0792f83c9','bruna','BRUNA','bruna@aplipack.com.br','BRUNA@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAECh1DmnFofDyRsrkw/0qOCsEFyuUinE+3QwR9bVnJhGJLY6dr2BWkOXDgXuETh41jA==','LV3NONIGGAP3YVV6XNM4PAN4NU4U72VZ','023203f6-22c8-4961-9dc3-0450845f2d48',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','0OLKAIJJJH',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('c368d3a8-6694-4a38-98f8-b74c8598b5a4','meuovo','MEUOVO','meuovo@gmail.com','MEUOVO@GMAIL.COM',0,'AQAAAAIAAYagAAAAEGq7MpD38Rm9epLajImUVllLtnnyDn0KWA50GbUf8g3MI5AS1zwVBsWnT4dOlTfUXg==','IK3HE3R4SPZEOK64C7C26V4ZMLHPZAOJ','45b0fd3c-b907-4496-bbac-b739fdb9014b',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','66ygya',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('d1ab6a5d-02ce-4dae-a2c7-d47aafa52c47','fox','FOX','fox@gmail.com','FOX@GMAIL.COM',0,'AQAAAAIAAYagAAAAEGSdTVSRhQBO4wDyRb/DYAt1egpV7FADNNXLukTBdyymo2qfq9lWfz2D+0xXOtOQiA==','7PBGCS2ZE7T54XYBVGS3GNT6GSMGEQQ7','ea9cd034-4d25-4110-9467-3640d8c67cff',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','999ijjka',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('e457defc-7769-409c-a31d-98c968ddea75','CAIO','CAIO','CAIO@APLIPACK.COM','CAIO@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEIrXXd5633D8UIiNx5Vnhik6kI+U9nWy2hwZe32RYv3fN4Wsc7mK/BzePGvVdqBaPA==','7OPGGE5MILD7JXSEZLIIQULLCVI3OO27','b16faefc-2a62-43d5-87d1-ebc90c5d59d2',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','9IJA8UHA7YG',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('e674be10-3fcb-4c8b-8afb-04fb9c67c96a','jamil','JAMIL','jamil@uol.com.br','JAMIL@UOL.COM.BR',0,'AQAAAAIAAYagAAAAEGS8ZSo4a5eGZHHC5nrIm9sEh6FAp/cFfNyv0jJyk4vesH4JjkTlslaN2nIkaouS4Q==','4WZDOMNASWIT2TNWKZ36LXUBR6S3A3GH','f0f06f22-4171-4bce-b998-38e7408cc736',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack',')OKNJ',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('e89dfe86-9feb-4362-a5d2-539957d14fb4','willian.adao','WILLIAN.ADAO','willian.adao@aplipack.com.br','WILLIAN.ADAO@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEDLvyPZtC+nvaFpS+zQBuxoEGZ0lGS4ebO7UZiUTOHEeLnRfjchwoFWjHDrfqIbYNw==','DRAP2NPEV72CMP7TROZVVV3DDHE45SCT','94f5b103-adcf-4275-b5ad-3fcbe84cd4a4',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','123asd','cK2RZY4vKErxLICMMwC6tW/ElNDHO+lDpfC4IbEyxfKuJ/lA3XJeDEoaBRDHVn1fFgyyanwmzRCIrCL+Booa7HFI/yr8hDzV50GngnW9TC1gboG7MrHC5agiRMolBzbysuqL7k0WXNSdpv90y7edzo+ja3kY8JO4/5FMH8pFQVs=','2025-08-22 15:31:33.686535');
INSERT INTO `aspnetusers` VALUES   ('e986c6f4-d5fa-48f1-b8f7-1e70bb4fe8bd','silvio','SILVIO','silvio@terra.com.br','SILVIO@TERRA.COM.BR',0,'AQAAAAIAAYagAAAAEC9IuDpK6nRl+9n4gkpwgwVLjiC75KJq0dA/J6HwQ2Bqy1faqSpQWSAEZkl4aTTYUA==','XPS5U35Q7WHZMP33IGF2I6FUX5HA4AIU','8c2d31f8-3707-4712-b2c1-892b89a298e7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','121343aftf',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('e996adf3-d9d4-44f9-928d-91b8883b2afb','joao','JOAO','joao@gmail.com','JOAO@GMAIL.COM',0,'AQAAAAIAAYagAAAAEEilPtpi670w+NZTPZmQ0+ofRKO4XlT2My3jN1UVr8vhTjjKYarUu+eKlFjhXhTkjw==','DCSRVYIB7MKZROKROEDGJPR4EKSE7BYA','7a3dadaf-06e1-49d2-9bb7-99c0acd359d7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','1345asy',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('eaa0251d-0a93-4118-bbcd-fa3114420e21','frank','FRANK','frank@aplipack.com.br','FRANK@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEBBrOjohvXdnS5tZFZVxG6pPzXKyiKWFcUzBhV+fthiXfLwrOEtYEzxJh5U40ZhDPw==','FYL7QZSURZUSLQ7OI4ARP2BMMF3AACCI','703d5172-9ff8-41c4-b72e-3c38d31edaa0',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','7777yhag1yt',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('f141bfb6-6969-42da-a64d-6d12147bd1b7','danilo','DANILO','danilo.castilho@aplipack.com.br','DANILO.CASTILHO@APLIPACK.COM.BR',0,'AQAAAAIAAYagAAAAEOzQX9NyLPZIbKvZ44OdPf65HRj9dvZL8BgzNBeYkpyfK8ucYjZtrn3va8QPCeukdA==','SU7R2J35J7DRHRJVFYLVXCWLAVCQXXQQ','6698f81b-9f27-4fe7-8823-ebc0f128e80f',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','123abc','nGtaz7nzd6p/PByKbkMMs785ndIreOzLM3NqTF0aeJnKKxlpE/CirBFYtUvFJ8iqEOV0MbazND1wRjOEw1IDblybEmoDvLcEb4HhVseqfngvWeMaIwnWMDWX3pRemMlMfXWhUh8QsSK2YY1GR/ONCtlT6v95t57fo7vUG0jU1y8=','2025-08-28 09:49:36.785691');
INSERT INTO `aspnetusers` VALUES   ('f5e8a293-ae4e-4f06-a8e9-885aeadbda3a','aplk','APLK','contato@aplipack.com','CONTATO@APLIPACK.COM',0,'AQAAAAIAAYagAAAAEDRR8kZdjSoHsE6vUSWPAbh1p1yONe22JKn1EoQq1AFSoSceDznX1FVCLYNQpDFc1A==','UYU2EFPID7O5OQXKSFKMCFPV6MNPRPJO','79fd1fc4-ff78-473f-9abe-7795f71b78e7',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','6DE395F6648C5','D66MtvnfrQmwKBgvWBL9bap7eMpFziDFxaVJHw7yx/dJoO8aDFjVxy3ZUWW0xmxvK2QskMuO+7y8T9xdEt32UN0/e+84105pj4bFOY1repVML+keCellVZTE32w9CTYh7TNMhH/d2XSgfpBYvVOOQMaf//rToZ/BP+Wb/R2NnwY=','2025-10-20 10:01:48.245744');
INSERT INTO `aspnetusers` VALUES   ('fab1b6f3-9a28-4bf6-805f-b2b7df63bd45','fox2010','FOX2010','2010@gmail.com','2010@GMAIL.COM',0,'AQAAAAIAAYagAAAAEDf+Wf2L2Hoextq+Vq0KeiBLN3TPL/lEXbKgrKRcmDbvKzx8UsJR+ETkPr6sL5KZnw==','PFOCS76BW6COPZE4WON2LUV5OKGHLJIT','845fc824-fd91-4b61-859f-f1c389f53493',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','556tyta',NULL,'0001-01-01 00:00:00.000000');
INSERT INTO `aspnetusers` VALUES   ('fe4ba316-96b9-4035-a26d-988630abae89','viviane','VIVIANE','viviane@zf.com.br','VIVIANE@ZF.COM.BR',0,'AQAAAAIAAYagAAAAEJV4wTw5BoXOjSbbeW9sY8Aibva/4uTYv0lsjBv9Lz6PPYGsZEHl1KgmiHHDRqlbaw==','IFRP6C2YZV3HBAPEJR5UW5LWDKMI4YAL','0ed153ab-15e3-4fdf-9860-cbfdb423861f',NULL,0,0,NULL,1,0,'http://localhost:7866','Aplipack','1QA2WS3','IDgUrseo9+CTIria995dBN5xrzI6AZmHiqsDoJ3kW08FTcoGbvHZE67uoxbUbOsyve9mkeCuzmQP4FbDn1TODhMt0X/9dUlFn47nevFaJ8sl8w1iafw7rkM7m3ehF/etkTXymGjCa5G+nRVZII90iNn6HcCP6dbt3VEal0KHLvY=','2025-09-26 17:25:45.531855');
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;


--
-- Definition of table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetusertokens`
--

/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;


--
-- Definition of table `cep`
--

DROP TABLE IF EXISTS `cep`;
CREATE TABLE `cep` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CEP` int DEFAULT NULL,
  `Codigo` int DEFAULT NULL,
  `Estado` varchar(2) DEFAULT NULL,
  `Codigo2` varchar(5) DEFAULT NULL,
  `Logradouro` varchar(60) DEFAULT NULL,
  `Bairro` varchar(60) DEFAULT NULL,
  `Cidade` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `cep`
--

/*!40000 ALTER TABLE `cep` DISABLE KEYS */;
/*!40000 ALTER TABLE `cep` ENABLE KEYS */;


--
-- Definition of table `chavevinc`
--

DROP TABLE IF EXISTS `chavevinc`;
CREATE TABLE `chavevinc` (
  `idChaveVinc` int unsigned NOT NULL AUTO_INCREMENT,
  `idAnagrafica` int unsigned DEFAULT NULL,
  `Chave` varchar(50) DEFAULT NULL,
  `NumLic` int unsigned DEFAULT '0',
  `DataVinculo` datetime DEFAULT NULL,
  PRIMARY KEY (`idChaveVinc`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chavevinc`
--

/*!40000 ALTER TABLE `chavevinc` DISABLE KEYS */;
/*!40000 ALTER TABLE `chavevinc` ENABLE KEYS */;


--
-- Definition of table `contrato`
--

DROP TABLE IF EXISTS `contrato`;
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
  CONSTRAINT `contrato_anagrafica_FK` FOREIGN KEY (`idCliente`) REFERENCES `anagrafica` (`idanagrafica`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `chk_qtdLicencas_nonneg` CHECK ((`qtdLicencas` >= 0))
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `contrato`
--

/*!40000 ALTER TABLE `contrato` DISABLE KEYS */;
INSERT INTO `contrato` VALUES   (1,4,'Plano Pleno',23,'2024-04-05 00:00:00.000000','2026-04-05 00:00:00.000000','Bienal',1,'Ativo','2026-04-05 00:00:00.000000','2024-04-05 00:00:00.000000','Contrato especial para grande empresa.','Contrato ativo.');
INSERT INTO `contrato` VALUES   (7,95,'Plano Full',12,'2024-04-05 00:00:00.000000','2026-04-05 00:00:00.000000','Bienal',1,'Ativo','2026-04-05 00:00:00.000000','2024-04-05 00:00:00.000000','Contrato para empresa Roda Brask','Contrato válido e em execução');
INSERT INTO `contrato` VALUES   (8,84,'PlanAdvanche',7,'2025-09-18 00:00:00.000000','2026-02-09 00:00:00.000000','Trimestral',1,'Ativo','2025-02-03 00:00:00.000000','2025-09-22 00:00:00.000000','Contrato para empresa ibm 2','Contrato ativo.');
INSERT INTO `contrato` VALUES   (12,96,'PlanAdvanche',2,'2025-08-01 00:00:00.000000','2025-10-31 00:00:00.000000','Mensal',1,'Ativo','2025-10-24 00:00:00.000000','2025-09-25 00:00:00.000000','','');
INSERT INTO `contrato` VALUES   (15,80,'PlanIntermedi',2,'2025-08-01 00:00:00.000000','2026-01-01 00:00:00.000000','Mensal',1,'Ativo','2025-09-01 00:00:00.000000','2025-08-01 00:00:00.000000','','Contrato válido e em execução');
INSERT INTO `contrato` VALUES   (16,13,'PlanAdvanche',1,'2025-09-01 00:00:00.000000','2025-10-01 00:00:00.000000','Mensal',1,'Ativo','2025-10-01 00:00:00.000000','2025-09-01 00:00:00.000000','','');
INSERT INTO `contrato` VALUES   (19,99,'PlanAdvanche',0,'2025-09-26 00:00:00.000000','2025-10-24 00:00:00.000000','Mensal',1,'Ativo','2025-10-24 00:00:00.000000','2025-09-26 00:00:00.000000','','Contrato próximo do vencimento');
INSERT INTO `contrato` VALUES   (21,80,'PlanAdvanche',5,'2025-10-10 00:00:00.000000','2026-01-30 00:00:00.000000','Trimestral',1,'','2026-01-09 00:00:00.000000','2025-10-10 00:00:00.000000','','');
INSERT INTO `contrato` VALUES   (22,63,'PlanIntermedi',9,'2025-10-14 00:00:00.000000','2026-02-27 00:00:00.000000','Trimestral',1,'Ativo','2026-01-09 00:00:00.000000','2025-10-14 00:00:00.000000','','Contrato válido e em execução');
/*!40000 ALTER TABLE `contrato` ENABLE KEYS */;


--
-- Definition of table `licenca`
--

DROP TABLE IF EXISTS `licenca`;
CREATE TABLE `licenca` (
  `numLic` int NOT NULL AUTO_INCREMENT,
  `IdCliente` int unsigned NOT NULL,
  `TipoLic` longtext,
  `MacAddress` longtext,
  `DataLic` datetime(6) NOT NULL,
  `scade` datetime(6) NOT NULL,
  `attivo` tinyint(1) NOT NULL DEFAULT '0',
  `IdRevenda` int unsigned NOT NULL,
  `SistemaOp` longtext,
  `DataAtivacao` datetime(6) DEFAULT NULL,
  `tipoPc` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `software` longtext,
  `ip` longtext,
  `processador` longtext,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'PendingActivation',
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
  CONSTRAINT `fk_licenca_licencachave` FOREIGN KEY (`idlicencachave`) REFERENCES `licencaschave` (`idLicencaChave`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1867 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `licenca`
--

/*!40000 ALTER TABLE `licenca` DISABLE KEYS */;
INSERT INTO `licenca` VALUES   (7,4,'FULL','1C:41:8E:C2:86:81','2025-08-15 18:45:58.357000','2025-08-15 18:45:58.357000',1,3,'LINUX','2025-08-15 18:45:58.357000','DESK','EURO','172.51.44.12','ADM','PENDENTE',5,'DESK7729',1);
INSERT INTO `licenca` VALUES   (8,4,'FULL','0F:1F:33:A3:7C:64','2025-08-15 18:45:58.357000','2025-08-15 18:45:58.357000',1,3,'LINUX','2025-08-15 18:45:58.357000','DESK','APLCODE','172.51.44.172','ADM','PENDENTE',7,'DESRD445',1);
INSERT INTO `licenca` VALUES   (512,4,'SOFT11111111111111','E8:40:F2:EC:84:35','2023-04-24 00:00:00.000000','2024-12-07 00:00:00.000000',1,1,'Microsoft Windows 10 Enterprise 2015 LTSB','2023-04-24 00:00:00.000000','Server','APLKProd','177.53.52.146','BFEBFBFF000306A9','Pendente Analise',6,'NATUROVOS',1);
INSERT INTO `licenca` VALUES   (591,55,'DEMO','DA:F3:72:2D:C9:50','2025-03-06 00:00:00.000000','2025-03-16 00:00:00.000000',0,1,'Microsoft Windows 11 Home Single Language','2025-03-06 00:00:00.000000','Server','EURObbs462PDV','172.69.114.53','BFEBFBFF000706E5','Pendente Analise',4,'RONE-NOTE',1);
INSERT INTO `licenca` VALUES   (592,44,'DEMO','DA:F3:72:2D:C9:50','2025-01-01 00:00:00.000000','2025-03-17 00:00:00.000000',0,1,'Microsoft Windows 11 Home Single Language','2025-03-07 00:00:00.000000','Server','EURObbs400','172.71.6.166','BFEBFBFF000706E5','Pendente Analise',7,'RONE-NOTE',1);
INSERT INTO `licenca` VALUES   (593,58,'DEMO','8C:B0:E9:09:3E:09','2025-03-11 00:00:00.000000','2025-03-21 00:00:00.000000',0,1,'Microsoft Windows 10 Home Single Language','2025-03-11 00:00:00.000000','Server','NOLEGGIO620','172.69.39.140','BFEBFBFF000806EC','Pendente Analise',32,'LAPTOP-EVKNSDTL',1);
INSERT INTO `licenca` VALUES   (594,58,'DEMO','D0:94:66:DE:08:D4','2025-03-17 00:00:00.000000','2025-03-27 00:00:00.000000',0,1,'Microsoft Windows 10 Home Single Language','2025-03-17 00:00:00.000000','Server','NOLEGGIO620','172.71.234.130','BFEBFBFF000906EA','Pendente Analise',31,'LAPTOP-EVKNSDTL',1);
INSERT INTO `licenca` VALUES   (595,10,'DEMO','5C:B4:7E:0B:E2:2B','2025-03-17 00:00:00.000000','2025-03-27 00:00:00.000000',0,1,'Microsoft Windows 10 Home Single Language','2025-03-17 00:00:00.000000','Server','NOLEGGIO620','172.71.234.177','BFEBFBFF000B06A3','Pendente Analise',20,'LAPTOP-EVKNSDTL',1);
INSERT INTO `licenca` VALUES   (596,19,'DEMO','D0:94:66:DE:08:D4','2025-03-17 00:00:00.000000','2025-03-27 00:00:00.000000',0,1,'Microsoft Windows 11 Home Single Language','2025-03-17 00:00:00.000000','Server','NOLEGGIO620','172.71.234.199','BFEBFBFF000906EA','Pendente Analise',8,'LAPTOP-EVKNSDTL',1);
INSERT INTO `licenca` VALUES   (1855,63,'PlanIntermedi','','2025-10-15 18:19:54.950177','2026-02-27 00:00:00.000000',0,0,'',NULL,'',NULL,NULL,'','PendingActivation',2275,'',1);
INSERT INTO `licenca` VALUES   (1856,63,'PlanIntermedi','','2025-10-15 18:20:07.526497','2026-02-27 00:00:00.000000',0,0,'',NULL,'',NULL,NULL,'','PendingActivation',2276,'',1);
INSERT INTO `licenca` VALUES   (1857,63,'PlanIntermedi','','2025-10-15 18:20:07.544716','2026-02-27 00:00:00.000000',0,0,'',NULL,'',NULL,NULL,'','PendingActivation',2277,'',1);
INSERT INTO `licenca` VALUES   (1858,63,'PlanIntermedi','','2025-10-15 18:24:15.707856','2026-02-27 00:00:00.000000',0,0,'',NULL,'',NULL,NULL,'','PendingActivation',2278,'',1);
INSERT INTO `licenca` VALUES   (1859,63,'PlanIntermedi','1b:b0:5a:1a:65:b0','2025-10-15 18:24:15.824300','2026-02-27 00:00:00.000000',1,0,'WINDOWS','2025-10-16 13:45:22.488329','',NULL,'172.58.69.66','000OK8','Active',2279,'desk002',1);
INSERT INTO `licenca` VALUES   (1860,63,'PlanIntermedi','00:01:03:04:05','2025-10-16 13:51:56.463730','2026-02-27 00:00:00.000000',1,0,'WINDOWS','2025-10-16 13:54:43.460858','',NULL,'200.225.225.0','000122','Active',2280,'DESK_01',1);
INSERT INTO `licenca` VALUES   (1861,63,'PlanIntermedi','1b:b0:5a:1a:26:2w','2025-10-16 13:51:56.501827','2026-02-27 00:00:00.000000',1,0,'WINDOWS','2025-10-16 15:22:31.966615','',NULL,'172.58.69.80','000O66','Active',2281,'desk007',1);
INSERT INTO `licenca` VALUES   (1862,63,'PlanIntermedi','','2025-10-16 19:36:49.098496','2026-02-27 00:00:00.000000',0,0,'',NULL,'',NULL,NULL,'','PendingActivation',2282,'',1);
INSERT INTO `licenca` VALUES   (1863,63,'PlanIntermedi','','2025-10-16 19:36:49.156833','2026-02-27 00:00:00.000000',0,0,'',NULL,'',NULL,NULL,'','PendingActivation',2283,'',1);
/*!40000 ALTER TABLE `licenca` ENABLE KEYS */;


--
-- Definition of table `licenca_arc`
--

DROP TABLE IF EXISTS `licenca_arc`;
CREATE TABLE `licenca_arc` (
  `numLic` int NOT NULL DEFAULT '0',
  `IdCliente` int NOT NULL,
  `TipoLic` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `MacAddress` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DataLic` datetime(6) NOT NULL,
  `scade` datetime(6) NOT NULL,
  `attivo` tinyint(1) NOT NULL,
  `IdRevenda` int NOT NULL,
  `SistemaOp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DataAtivacao` datetime(6) DEFAULT NULL,
  `tipo_pc` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `nome_computador` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `software` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ip` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `processador` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `idlicencachave` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Definition of table `licencalog`
--

DROP TABLE IF EXISTS `licencalog`;
CREATE TABLE `licencalog` (
  `idLog` int unsigned NOT NULL AUTO_INCREMENT,
  `numLic` int DEFAULT NULL,
  `Chave` varchar(100) DEFAULT NULL,
  `Endpoint` varchar(100) NOT NULL,
  `RequestPayload` json DEFAULT NULL,
  `ResponseCode` int DEFAULT NULL,
  `ClienteIp` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `mensagem` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`idLog`),
  KEY `idx_licenca_log_numLic` (`numLic`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



--
-- Definition of table `licencaschave`
--

DROP TABLE IF EXISTS `licencaschave`;
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
) ENGINE=InnoDB AUTO_INCREMENT=2287 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


--
-- Definition of table `licencaslivres`
--

DROP TABLE IF EXISTS `licencaslivres`;
CREATE TABLE `licencaslivres` (
  `idLicencaChave` int unsigned DEFAULT NULL,
  `Chave` varchar(50) DEFAULT NULL,
  `idSoftware` int unsigned DEFAULT NULL,
  `IdRevenda` int unsigned DEFAULT NULL,
  `NumLic` int unsigned DEFAULT NULL,
  `DataInser` datetime DEFAULT NULL,
  `TipoLic` varchar(20) DEFAULT NULL,
  `Entregue` int unsigned DEFAULT NULL,
  `EntreguePara` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Dumping data for table `licencaslivres`
--

/*!40000 ALTER TABLE `licencaslivres` DISABLE KEYS */;
/*!40000 ALTER TABLE `licencaslivres` ENABLE KEYS */;


--
-- Definition of table `revenda`
--

DROP TABLE IF EXISTS `revenda`;
CREATE TABLE `revenda` (
  `idrevenda` int unsigned NOT NULL AUTO_INCREMENT,
  `razaoSocial` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`idrevenda`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `revenda`
--

/*!40000 ALTER TABLE `revenda` DISABLE KEYS */;
INSERT INTO `revenda` VALUES   (1,'Eurosistema');
INSERT INTO `revenda` VALUES   (2,'EW Net');
INSERT INTO `revenda` VALUES   (3,'Backup Brasil');
INSERT INTO `revenda` VALUES   (4,'Eurosistema Italia');
INSERT INTO `revenda` VALUES   (5,'BIGGI');
INSERT INTO `revenda` VALUES   (6,'Informalab SRL');
INSERT INTO `revenda` VALUES   (7,'Rone');
INSERT INTO `revenda` VALUES   (8,'Madica');
INSERT INTO `revenda` VALUES   (9,'teste');
INSERT INTO `revenda` VALUES   (10,'SoftwareCODE');
INSERT INTO `revenda` VALUES   (11,'Coca Cola');
INSERT INTO `revenda` VALUES   (12,'COca Cola');
INSERT INTO `revenda` VALUES   (13,'Avon');
INSERT INTO `revenda` VALUES   (14,'Pepsi');
INSERT INTO `revenda` VALUES   (15,'hhugo');
INSERT INTO `revenda` VALUES   (16,'Dell');
INSERT INTO `revenda` VALUES   (17,'Intel');
INSERT INTO `revenda` VALUES   (18,'Ruth');
INSERT INTO `revenda` VALUES   (20,'Bosh');
INSERT INTO `revenda` VALUES   (21,'TRUMP');
INSERT INTO `revenda` VALUES   (22,'NOVA REVENDA');
INSERT INTO `revenda` VALUES   (23,'IBM');
INSERT INTO `revenda` VALUES   (24,'MOTEK 2025');
INSERT INTO `revenda` VALUES   (27,'NOVA REVENDA');
INSERT INTO `revenda` VALUES   (28,'NOVO CLIENTE');
INSERT INTO `revenda` VALUES   (29,'Roda Brask');
INSERT INTO `revenda` VALUES   (31,'TONHAO DA LUA');
/*!40000 ALTER TABLE `revenda` ENABLE KEYS */;


--
-- Definition of table `revenda_user`
--

DROP TABLE IF EXISTS `revenda_user`;
CREATE TABLE `revenda_user` (
  `idRevenda` int unsigned NOT NULL,
  `idUser` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`idRevenda`,`idUser`),
  KEY `fk_revenda_user_user` (`idUser`),
  CONSTRAINT `fk_revenda_user_revenda` FOREIGN KEY (`idRevenda`) REFERENCES `revenda` (`idrevenda`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_revenda_user_user` FOREIGN KEY (`idUser`) REFERENCES `aspnetusers` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


--
-- Definition of table `software`
--

DROP TABLE IF EXISTS `software`;
CREATE TABLE `software` (
  `idSoftware` int unsigned NOT NULL AUTO_INCREMENT,
  `nSoftware` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Descricao` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idSoftware`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `software`
--

/*!40000 ALTER TABLE `software` DISABLE KEYS */;
INSERT INTO `software` VALUES   (1,'EuroBBS','EuroBBS');
INSERT INTO `software` VALUES   (2,'EuroNFE40','EuroNFE40');
INSERT INTO `software` VALUES   (3,'TouchHair2020','TouchHair2020');
INSERT INTO `software` VALUES   (4,'APLKProd','APLK Producao');
INSERT INTO `software` VALUES   (5,'EURObbs400','Eurobbs400');
INSERT INTO `software` VALUES   (6,'LPN','Software LPN');
INSERT INTO `software` VALUES   (8,'APLKCODE','APLKCODE 2026');
/*!40000 ALTER TABLE `software` ENABLE KEYS */;


--
-- Definition of table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE `usuarios` (
  `idUsuario` int unsigned NOT NULL AUTO_INCREMENT,
  `Usuario` varchar(50) NOT NULL,
  `Senha` varchar(20) DEFAULT NULL,
  `Nivel` int unsigned DEFAULT '0',
  `IdRevenda` int unsigned DEFAULT '0',
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usuarios`
--

/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES   (1,'User','1234',9,10);
INSERT INTO `usuarios` VALUES   (2,'Mateus','1234',9,1);
INSERT INTO `usuarios` VALUES   (3,'elio.pasquini','123456',9,1);
INSERT INTO `usuarios` VALUES   (4,'danilo','fep921',9,1);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;




--
-- Definition of procedure `POMELO_AFTER_ADD_PRIMARY_KEY`
--

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`%` PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `POMELO_BEFORE_DROP_PRIMARY_KEY`
--

DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`%` PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
