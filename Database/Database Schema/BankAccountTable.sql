CREATE TABLE BankAccounts (
	accountNum BIGSERIAL PRIMARY KEY,
	accountName VARCHAR,
	accountType VARCHAR NOT NULL,
	amount FLOAT NOT NULL
);

ALTER SEQUENCE BankAccounts_accountNum_seq RESTART 111111111111;