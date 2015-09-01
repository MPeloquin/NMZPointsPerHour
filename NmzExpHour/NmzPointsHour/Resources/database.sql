BEGIN TRANSACTION;
CREATE TABLE "Monster" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`Name`	TEXT NOT NULL,
	`Points`	INTEGER NOT NULL,
	`Quest`	TEXT
);
INSERT INTO `Monster` VALUES (1,'Black Demon',17561,NULL);
INSERT INTO `Monster` VALUES (2,'Glod',15702,NULL);
INSERT INTO `Monster` VALUES (3,'Nezikchened
',17975,NULL);
INSERT INTO `Monster` VALUES (4,'Nazastarool',6611,NULL);
INSERT INTO `Monster` VALUES (5,'Slagilith',8264,NULL);
INSERT INTO `Monster` VALUES (6,'Ice Troll King',9297,NULL);
INSERT INTO `Monster` VALUES (7,'Witch''s experiment',2066,NULL);
INSERT INTO `Monster` VALUES (8,'The Everlasting',27479,'Dream Mentor');
INSERT INTO `Monster` VALUES (9,'Elvarg',9297,'Dragon Slayer');
INSERT INTO `Monster` VALUES (10,'Daggonoth Mother',8264,NULL);
INSERT INTO `Monster` VALUES (11,'Chronozon',18181,NULL);
INSERT INTO `Monster` VALUES (12,'Culinaromancer',8884,NULL);
INSERT INTO `Monster` VALUES (13,'Karamel',7024,NULL);
INSERT INTO `Monster` VALUES (14,'Khazard warlord',7438,NULL);
INSERT INTO `Monster` VALUES (15,'Evil Chicken',16735,NULL);
INSERT INTO `Monster` VALUES (16,'Dad',8264,NULL);
INSERT INTO `Monster` VALUES (17,'Me',8264,NULL);
INSERT INTO `Monster` VALUES (18,'Agrith Naar',7851,NULL);
INSERT INTO `Monster` VALUES (19,'The Untouchable',39876,'Dream Mentor');
INSERT INTO `Monster` VALUES (20,'King Roald',7231,NULL);
INSERT INTO `Monster` VALUES (21,'Gelatinnoth Mother',8264,NULL);
INSERT INTO `Monster` VALUES (22,'Arrg',9090,NULL);
INSERT INTO `Monster` VALUES (23,'Jungle Demon',21900,'Monkey Madness');
INSERT INTO `Monster` VALUES (24,'Fareed',18388,'Desert Treasure');
INSERT INTO `Monster` VALUES (25,'Tanglefoot',8057,NULL);
INSERT INTO `Monster` VALUES (26,'Giant Scarab',20454,'');
INSERT INTO `Monster` VALUES (27,'Tree spirit',8057,NULL);
INSERT INTO `Monster` VALUES (28,'Dessous',9710,NULL);
INSERT INTO `Monster` VALUES (29,'Kamil',15289,'Desert Treasure');
INSERT INTO `Monster` VALUES (30,'Moss giant',6818,NULL);
INSERT INTO `Monster` VALUES (31,'Dessourt',9710,NULL);
INSERT INTO `Monster` VALUES (32,'Damis',15082,NULL);
INSERT INTO `Monster` VALUES (33,'The Kendal',9090,NULL);
INSERT INTO `Monster` VALUES (34,'Bouncer',12190,NULL);
INSERT INTO `Monster` VALUES (35,'Giant Roc',13636,NULL);
INSERT INTO `Monster` VALUES (36,'',0,NULL);
INSERT INTO `Monster` VALUES (37,'',0,NULL);
INSERT INTO `Monster` VALUES (38,'',0,NULL);
INSERT INTO `Monster` VALUES (39,'',0,NULL);
CREATE TABLE "DreamMonster" (
	`IdDreamMonster`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`IdDream`	INTEGER,
	`IdMonster`	INTEGER,
	`TimeKilled`	INTEGER,
	FOREIGN KEY(`IdDream`) REFERENCES Dream ( IdDream ),
	FOREIGN KEY(`IdMonster`) REFERENCES Monster ( IdMonster )
);
CREATE TABLE "Dream" (
	`IdDream`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`DurationMs`	INTEGER NOT NULL,
	`Points`	INTEGER NOT NULL
);
COMMIT;
