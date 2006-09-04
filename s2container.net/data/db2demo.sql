DROP DATABASE S2DEMO;

CREATE DATABASE S2DEMO USING CODESET UTF-8 TERRITORY JP COLLATE USING IDENTITY;

CONNECT TO S2DEMO;

GRANT DBADM,CREATETAB,BINDADD,CONNECT,CREATE_NOT_FENCED_ROUTINE,IMPLICIT_SCHEMA,LOAD,CREATE_EXTERNAL_ROUTINE,QUIESCE_CONNECT ON DATABASE  TO USER S2DEMOUSER;

CREATE TABLE S2DEMOUSER.DEPT (
	DEPTNO SMALLINT NOT NULL,
	DNAME VARCHAR(14),
	LOC VARCHAR(13),
	VERSIONNO INTEGER,
	ACTIVE SMALLINT,
	CONSTRAINT PK_DEPT PRIMARY KEY (DEPTNO)
);

CREATE TABLE S2DEMOUSER.DEPT2 (
	DEPTNO SMALLINT NOT NULL,
	DNAME VARCHAR(14),
	ACTIVE SMALLINT,
	CONSTRAINT PK_DEPT2 PRIMARY KEY (DEPTNO)
);

CREATE TABLE S2DEMOUSER.EMP (
	EMPNO INTEGER NOT NULL,
	ENAME VARCHAR(10),
	JOB VARCHAR(9),
	MGR INTEGER,
	HIREDATE TIMESTAMP,
	SAL INTEGER,
	COMM INTEGER,
	DEPTNO SMALLINT,
	TSTAMP TIMESTAMP,
	CONSTRAINT PK_EMP PRIMARY KEY (EMPNO)
);

CREATE TABLE S2DEMOUSER.EMP2 (
	EMPNO INTEGER NOT NULL,
	ENAME VARCHAR(10),
	DEPTNO SMALLINT,
	CONSTRAINT PK_EMP2 PRIMARY KEY (EMPNO)
);

CREATE TABLE S2DEMOUSER.BASICTYPE (
	ID DECIMAL (18, 0) NOT NULL,
	BOOLTYPE DECIMAL (1, 0),
	BYTETYPE SMALLINT,
	SBYTETYPE SMALLINT,
	INT16TYPE SMALLINT,
	INT32TYPE INTEGER,
	INT64TYPE BIGINT,
	SINGLETYPE FLOAT,
	DOUBLETYPE DOUBLE PRECISION,
	DECIMALTYPE DECIMAL (28, 0),
	STRINGTYPE VARCHAR (1024),
	DATETIMETYPE TIMESTAMP,
	CONSTRAINT pk_basictype PRIMARY KEY (id)
);

INSERT INTO S2DEMOUSER.EMP VALUES (7369, 'SMITH',  'CLERK',		7902, TIMESTAMP('1980-12-17 00:00:00'),  800, NULL, 20, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7499, 'ALLEN',  'SALESMAN',	7698, TIMESTAMP('1981-02-20 00:00:00'), 1600,  300, 30, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7521, 'WARD',   'SALESMAN',	7698, TIMESTAMP('1981-02-22 00:00:00'), 1250,  500, 30, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7566, 'JONES',  'MANAGER',	7839, TIMESTAMP('1981-04-02 00:00:00'), 2975, NULL, 20, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7654, 'MARTIN', 'SALESMAN',	7698, TIMESTAMP('1981-09-28 00:00:00'), 1250, 1400, 30, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7698, 'BLAKE',  'MANAGER',	7839, TIMESTAMP('1981-05-01 00:00:00'), 2850, NULL, 30, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7782, 'CLARK',  'MANAGER',	7839, TIMESTAMP('1981-06-09 00:00:00'), 2450, NULL, 10, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7788, 'SCOTT',  'ANALYST',	7566, TIMESTAMP('1982-12-09 00:00:00'), 3000, NULL, 20, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7839, 'KING',   'PRESIDENT', NULL, TIMESTAMP('1981-11-17 00:00:00'), 5000, NULL, 10, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7844, 'TURNER', 'SALESMAN',	7698, TIMESTAMP('1981-09-08 00:00:00'), 1500,	 0, 30, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7876, 'ADAMS',  'CLERK',		7788, TIMESTAMP('1983-01-12 00:00:00'), 1100, NULL, 20, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7900, 'JAMES',  'CLERK',		7698, TIMESTAMP('1981-12-03 00:00:00'),  950, NULL, 30, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7902, 'FORD',   'ANALYST',	7566, TIMESTAMP('1981-12-03 00:00:00'), 3000, NULL, 20, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.EMP VALUES (7934, 'MILLER', 'CLERK',		7782, TIMESTAMP('1982-01-23 00:00:00'), 1300, NULL, 10, TIMESTAMP('2000-01-01 00:00:00'));
INSERT INTO S2DEMOUSER.DEPT VALUES (10, 'ACCOUNTING', 'NEW YORK', 0, 1);
INSERT INTO S2DEMOUSER.DEPT VALUES (20, 'RESEARCH',   'DALLAS',   0, 1);
INSERT INTO S2DEMOUSER.DEPT VALUES (30, 'SALES',	  'CHICAGO',  0, 1);
INSERT INTO S2DEMOUSER.DEPT VALUES (40, 'OPERATIONS', 'BOSTON',   0, 1);
INSERT INTO S2DEMOUSER.EMP2 VALUES (7369, 'SMITH', 20);
INSERT INTO S2DEMOUSER.EMP2 VALUES (7499, 'ALLEN', 30);
INSERT INTO S2DEMOUSER.DEPT2 VALUES (20, 'RESEARCH', 1);
INSERT INTO S2DEMOUSER.DEPT2 VALUES (30, 'SALES',    0);
INSERT INTO S2DEMOUSER.BASICTYPE VALUES (
	1,
	1,
	255,
	-128,
	32767,
	2147483647,
	9223372036854775807,
	9.876543,
	9.87654321098765,
	9999999999999999999999999999,
	'�|\���`',
	TIMESTAMP('1980-12-17 12:34:56')
);
INSERT INTO S2DEMOUSER.BASICTYPE (
	id
) VALUES (
	2
);

CONNECT RESET;
