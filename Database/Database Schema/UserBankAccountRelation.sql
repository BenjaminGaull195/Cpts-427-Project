CREATE TABLE UserBankRelation (
	userAccountID INT NOT NULL,
	bankAccountID INT NOT NULL,
	PRIMARY KEY (userAccountID, bankAccountID),
	FOREIGN KEY (userAccountID) REFERENCES UserAccounts(AccountID) ON DELETE CASCADE,
	FOREIGN KEY (bankAccountID) REFERENCES BankAccounts(AccountNum) ON DELETE CASCADE
);