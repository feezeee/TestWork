CREATE DATABASE testdb;
GO
USE testdb;
GO

/****** Object:  Table [dbo].[Companies]    Script Date: 13.11.2021 20:01:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE TABLE form_types(
	form_type_id INT PRIMARY KEY IDENTITY,
	form_type_name VARCHAR(32) NOT NULL UNIQUE
);
CREATE TABLE companies(
	company_id INT PRIMARY KEY,
	company_name VARCHAR(32) NOT NULL UNIQUE,	
	form_type_id INT REFERENCES form_types(form_type_id) NOT NULL,
);
CREATE TABLE  positions(
	position_id INT PRIMARY KEY IDENTITY,
	position_name VARCHAR(32) NOT NULL UNIQUE
);
CREATE TABLE workers(
	worker_id INT PRIMARY KEY IDENTITY,
	worker_last_mame VARCHAR(32) NOT NULL,
	worker_first_name VARCHAR(32) NOT NULL,
	worker_middle_name VARCHAR(32) NOT NULL,
	worker_date_employment DATE NOT NULL,
	position_id INT REFERENCES positions(position_id) NOT NULL,
	company_id INT REFERENCES companies(company_id) NOT NULL
);


INSERT positions(position_name) VALUES ('Разработчик '),('Тестировщик '),('Бизнес-аналитик'),('Менеджер');
INSERT form_types(form_type_name) VALUES ('ООО'),('ОАО');
INSERT companies(company_id, company_name, form_type_id) VALUES (1, 'Qulix Systems', 1);
INSERT workers(worker_last_mame, worker_first_name, worker_middle_name, worker_date_employment, position_id, company_id) VALUES ('Скурат','Денис','Сергеевич','26.08.2002',1,1);