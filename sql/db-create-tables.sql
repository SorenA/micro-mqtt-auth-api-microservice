CREATE TABLE IF NOT EXISTS `mqtt_users` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `Username` varchar(128) NOT NULL,
  `Password` varchar(512) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `mqtt_user_acl` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `MqttUserId` bigint(20) unsigned NOT NULL,
  `Type` varchar(64) NOT NULL,
  `TopicPattern` varchar(512) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_MqttUserId` (`MqttUserId`),
  CONSTRAINT `FK_MqttUserId` FOREIGN KEY (`MqttUserId`) REFERENCES `mqtt_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;