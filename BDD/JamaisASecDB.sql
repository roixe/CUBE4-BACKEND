CREATE DATABASE IF NOT EXISTS JamaisASecDB;

DROP TABLE `Clients`;
CREATE TABLE `Clients` (
	`id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(200) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	`addresse` VARCHAR(400) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	`mail` VARCHAR(320) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	`telephone` VARCHAR(12) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	PRIMARY KEY (`id`)
) ENGINE=InnoDB;


DROP TABLE `Fournisseurs`;
CREATE TABLE `Fournisseurs` (
	`id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(200) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	`addresse` VARCHAR(400) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	`mail` VARCHAR(320) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	`telephone` VARCHAR(12) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
	`SIRET` VARCHAR(20) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB;


DROP TABLE `Maisons`;
CREATE TABLE `Maisons` (
	`id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(200) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB;


DROP TABLE `Familles`;
CREATE TABLE `Familles` (
	`id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(200) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB;


DROP TABLE `Commandes`;
CREATE TABLE `Commandes` (
	`id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
	`reference` VARCHAR(16) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    `date` DATE() NOT NULL,
    `status` VARCHAR(30) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    `articles_commandes_id` INT(20) unsigned NOT NULL,
    `clients_id` INT(20) unsigned,
    `fournisseurs_id` INT(20) unsigned,
    PRIMARY KEY (`id`)
    FOREIGN KEY (`articles_commandes_id`) REFERENCES ArticlesCommandes(`id`)
    FOREIGN KEY (`clients_id`) REFERENCES Clients(`id`)
    FOREIGN KEY (`fournisseurs_id`) REFERENCES Fournisseurs(`id`)
) ENGINE=InnoDB;


DROP TABLE `ArticlesCommandes`;
CREATE TABLE `ArticlesCommandes` (
	`id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
    `articles_id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
    `commandes_id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
    PRIMARY KEY (`id`)
    FOREIGN KEY (`articles_id`) REFERENCES Articles(`id`)
    FOREIGN KEY (`commandes_id`) REFERENCES Commandes(`id`)
) ENGINE=InnoDB;


DROP TABLE `Articles`;
CREATE TABLE `Articles` (
	`id` INT(20) unsigned NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(200) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    `quantite` INT(20) unsigned NOT NULL,
    `image` VARCHAR(400) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    `prix_unitaire` INT(20) unsigned NOT NULL,
	`colisage` VARCHAR(200) NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    `quantite_min` INT(20) unsigned NOT NULL,    
    `annee` INT(20) unsigned NOT NULL,
    `description` TEXT() NOT NULL CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    `familles_id` INT(20) unsigned NOT NULL,
    `maisons_id` INT(20) unsigned NOT NULL,
    PRIMARY KEY (`id`)
    FOREIGN KEY (`familles_id`) REFERENCES Familles(`id`)
    FOREIGN KEY (`maisons_id`) REFERENCES Maisons(`id`)
) ENGINE=InnoDB;
