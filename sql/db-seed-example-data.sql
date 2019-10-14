/*!40000 ALTER TABLE `mqtt_users` DISABLE KEYS */;
INSERT INTO `mqtt_users` (`Id`, `Username`, `Password`) VALUES
	(1, 'admin', '123'),
	(2, 'user', '456');
/*!40000 ALTER TABLE `mqtt_users` ENABLE KEYS */;

/*!40000 ALTER TABLE `mqtt_user_acl` DISABLE KEYS */;
INSERT INTO `mqtt_user_acl` (`Id`, `MqttUserId`, `Type`, `TopicPattern`) VALUES
	(3, 1, 'pub', '#'),
	(4, 1, 'sub', '#'),
	(5, 2, 'pub', 'sensors/+/metrics/#'),
	(6, 2, 'pub', 'sensors/+/actions/restart'),
	(7, 2, 'sub', 'sensors/1/metrics/#');
/*!40000 ALTER TABLE `mqtt_user_acl` ENABLE KEYS */;