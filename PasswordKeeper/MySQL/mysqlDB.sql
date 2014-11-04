CREATE DATABASE  IF NOT EXISTS `password_keeper` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `password_keeper`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: 127.0.0.1    Database: password_keeper
-- ------------------------------------------------------
-- Server version	5.6.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `pass_unit`
--

DROP TABLE IF EXISTS `pass_unit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pass_unit` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `url` varchar(45) DEFAULT NULL,
  `description` text,
  `expires` datetime DEFAULT NULL,
  `UNIT_TYPE_id` int(11) DEFAULT NULL,
  `USER_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `UNIT_TYPE_id` (`UNIT_TYPE_id`),
  KEY `USER_id` (`USER_id`),
  CONSTRAINT `pass_unit_ibfk_1` FOREIGN KEY (`UNIT_TYPE_id`) REFERENCES `unit_type` (`id`),
  CONSTRAINT `pass_unit_ibfk_2` FOREIGN KEY (`USER_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pass_unit`
--

LOCK TABLES `pass_unit` WRITE;
/*!40000 ALTER TABLE `pass_unit` DISABLE KEYS */;
INSERT INTO `pass_unit` VALUES (6,'oGame','satanailo','sifra123','ogame.ba','Borbae','2014-11-22 00:20:35',3,5),(7,'coursera.org','strze','123456789','www.coursera.org','Java','9999-12-31 23:59:59',2,5),(8,'Armor Games','RBS','123456789','www.armorgames.com','Flash games','9999-12-31 23:59:59',3,5);
/*!40000 ALTER TABLE `pass_unit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unit_type`
--

DROP TABLE IF EXISTS `unit_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `unit_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unit_type`
--

LOCK TABLES `unit_type` WRITE;
/*!40000 ALTER TABLE `unit_type` DISABLE KEYS */;
INSERT INTO `unit_type` VALUES (1,'General','Passwords from uninformation technology field.'),(2,'Web','Passwords from web.'),(3,'Games','Passwords from games.'),(4,'Email','Passwords for emails.'),(5,'Windows','Passwords operation system.');
/*!40000 ALTER TABLE `unit_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (5,'szeljic','25f9e794323b453885f5181f1b624d0b'),(6,'strale','07ca5f4993e608abdcd0f53687bf5d5c'),(7,'Strahinja','6ebe76c9fb411be97b3b0d48b791a7c9'),(8,'szeljic','60d6ea68bdf457e10a3a18ba34974917'),(9,'szeljic','e388c1c5df4933fa01f6da9f92595589'),(10,'szeljic','25f9e794323b453885f5181f1b624d0b'),(11,'szeljicw','25d55ad283aa400af464c76d713c07ad');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-11-01 14:11:16
