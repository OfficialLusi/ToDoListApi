CREATE TABLE Users ( 
	UserId			INTEGER PRIMARY KEY	AUTOINCREMENT	,             
	UserGuid		TEXT NOT NULL						,             
	UserName		TEXT NOT NULL						,             
	UserSurname		TEXT NOT NULL						,          
	UserEmail		TEXT NOT NULL						,            
	UserCreatedOn	TEXT NOT NULL						,
	Salt			TEXT NULL							,
	HashCode		TEXT NULL			
	);                                  


CREATE TABLE Activities ( 
   ActivityId							INTEGER PRIMARY KEY	AUTOINCREMENT	,                
   UserId								INTEGER								,                            
   ActivityGuid							TEXT NOT NULL						,                
   ActivityTitle						TEXT NOT NULL						,               
   ActivityDescription					TEXT NOT NULL						,             
   ActivityCreatedOn					TEXT NOT NULL						,           
   ActivityModifiedOn					TEXT NULL							,    
   FOREIGN KEY (UserId) REFERENCES Users(UserId) 
   );

CREATE TABLE AdminUser (
	AdminId			INTEGER PRIMARY KEY AUTOINCREMENT	,
	AdminGuid		TEXT NOT NULL						,
	AdminName		TEXT NOT NULL						,
	AdminPassword	TEXT NOT NULL						
	);

INSERT INTO AdminUser (UserGuid, AdminName, AdminPassword) 
VALUES ('366166CC-37D8-4669-9057-4068DF6D4BC8', 'admin', 'admin');