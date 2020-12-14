-- Remove permissions from public user group
REVOKE CREATE ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON DATABASE bank FROM PUBLIC;

--because this role has no granted permissions, it may be removed
CREATE ROLE unregisteredUser;

CREATE ROLE loginService;
GRANT CONNECT ON DATABASE bank TO loginService;
GRANT SELECT ON TABLE userAccounts TO loginService;
GRANT INSERT ON TABLE userAccounts TO loginService;

CREATE ROLE registeredUser;
GRANT CONNECT ON DATABASE bank TO registeredUser;
GRANT SELECT ON TABLE bankAccounts, transactions TO registeredUser;

-- password defined here is a temporary filler
CREATE USER bankAdmin PASSWORD 'tempPassword';
GRANT CONNECT ON DATABASE bank TO bankAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON TABLE userAccounts, bankAccounts, transactions TO bankAdmin;




