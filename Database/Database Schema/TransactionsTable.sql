DROP TABLE Transactions;

CREATE TABLE Transactions (
	accountID INT NOT NULL,
	transactionID BIGSERIAL NOT NULL,
	transactionDate TIMESTAMP NOT NULL,
	transactionType VARCHAR NOT NULL,
	transactionAmount FLOAT NOT NULL,
	PRIMARY KEY (accountID, transactionID),
	FOREIGN KEY (accountID) REFERENCES BankAccounts(AccountNum) ON DELETE CASCADE
);

ALTER SEQUENCE transactions_transactionid_seq RESTART 1111;