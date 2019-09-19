CREATE TABLE usertable(id int NOT NULL IDENTITY PRIMARY KEY, name varchar(100), Phone varchar(15), gender varchar(12), email varchar(100), faceid varchar(100));

CREATE TABLE imagevalidation(id int NOT NULL IDENTITY PRIMARY KEY, validation_type varchar(100), validation_message varchar(255),isactive int);

CREATE TABLE gesture(id int NOT NULL IDENTITY PRIMARY KEY, gesture_name varchar(100), thumbnail_url varchar(max), gesture_message varchar(255), isactive int);

CREATE TABLE auditlog(id int NOT NULL IDENTITY PRIMARY KEY, layer varchar(100), result_type varchar(50), device_type varchar(50), userimage text);

CREATE TABLE verifytime(id int NOT NULL IDENTITY PRIMARY KEY, personid varchar(100), date varchar(25), checkin varchar(25), checkout varchar(25));


INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Face Availability','Face is not available',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Remove Sun Glasses','Please remove sunglasses',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Multiple Face','Multiple Faces are detected',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Expressions','Expression is not Neutral',0);

INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('One','https://faceholimage.blob.core.windows.net/faceholblobimage/one.jpg?sp=r&st=2019-05-08T06:52:23Z&se=2019-07-08T14:52:23Z&spr=https&sv=2018-03-28&sig=3CyT8YmZ3oXeTDVZFcrkD6u%2FZPRsBsqXwETHcTvV6BM%3D&sr=b','The Person is showing one',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Two','https://faceholimage.blob.core.windows.net/faceholblobimage/two.jpg?sp=r&st=2019-05-08T06:51:45Z&se=2019-07-08T14:51:45Z&spr=https&sv=2018-03-28&sig=B3plDRLlZZCBZ9JDRsiG%2BO%2F6ExOHjfv8J2OnJgRxj%2BA%3D&sr=b','The Person is showing Two',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Three','https://faceholimage.blob.core.windows.net/faceholblobimage/three.jpg?sp=r&st=2019-05-08T06:51:00Z&se=2019-07-08T14:51:00Z&spr=https&sv=2018-03-28&sig=3UefgIAqYDPQkoDAUwHGOEITb9DSc2M6xoRFlatdUhE%3D&sr=b','The Person is showing Three',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Four','https://faceholimage.blob.core.windows.net/faceholblobimage/four.jpg?sp=r&st=2019-05-08T06:50:12Z&se=2019-07-08T14:50:12Z&spr=https&sv=2018-03-28&sig=Q7z80NRGGWOxF8yi7TmCe4JxTB209HC1AJTV9IDjWJc%3D&sr=b','The Person is showing Four',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Five','https://faceholimage.blob.core.windows.net/faceholblobimage/five.jpg?sp=r&st=2019-05-08T06:48:12Z&se=2019-07-08T14:48:12Z&spr=https&sv=2018-03-28&sig=%2FtysHAhw%2FVqjoYQwnpN43Ld5iIcLYP7eBbNHusK2cgc%3D&sr=b','The Person is showing Five',0);