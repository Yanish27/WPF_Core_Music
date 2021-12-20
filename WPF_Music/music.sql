-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : Dim 19 déc. 2021 à 15:00
-- Version du serveur :  5.7.31
-- Version de PHP : 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `music`
--

-- --------------------------------------------------------

--
-- Structure de la table `albums`
--

DROP TABLE IF EXISTS `albums`;
CREATE TABLE IF NOT EXISTS `albums` (
  `Idalbums` int(11) NOT NULL AUTO_INCREMENT,
  `Titre` varchar(255) NOT NULL,
  `ArtistIdArtist` int(11) NOT NULL,
  PRIMARY KEY (`Idalbums`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `albums`
--

INSERT INTO `albums` (`Idalbums`, `Titre`, `ArtistIdArtist`) VALUES
(1, 'La nuit', 1),
(2, 'La nuit', 2);

-- --------------------------------------------------------

--
-- Structure de la table `artist`
--

DROP TABLE IF EXISTS `artist`;
CREATE TABLE IF NOT EXISTS `artist` (
  `IdArtist` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Annee` varchar(255) NOT NULL,
  `Discription` varchar(255) NOT NULL,
  PRIMARY KEY (`IdArtist`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `artist`
--

INSERT INTO `artist` (`IdArtist`, `Name`, `Annee`, `Discription`) VALUES
(1, 'Halaoui', '20011', 'ABCDEFfa'),
(2, 'YANIS', '1999', 'AVDsddazaz'),
(3, 'HENRY', '2001', 'Jazzzzz');

-- --------------------------------------------------------

--
-- Structure de la table `rating`
--

DROP TABLE IF EXISTS `rating`;
CREATE TABLE IF NOT EXISTS `rating` (
  `IdRatings` int(11) NOT NULL,
  `Grade` varchar(255) NOT NULL,
  `AlbumsIdAlbums` int(11) NOT NULL,
  PRIMARY KEY (`IdRatings`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `rating`
--

INSERT INTO `rating` (`IdRatings`, `Grade`, `AlbumsIdAlbums`) VALUES
(1, '5', 1);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
