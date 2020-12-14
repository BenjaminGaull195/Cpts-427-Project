-- the following users are used to test the roles defined above and are dropped after completion of testing 
CREATE USER test_unregistered WITH PASSWORD 'secret_passwd';
GRANT unregisteredUser TO test_unregistered;
CREATE USER test_registered WITH PASSWORD 'secret_passwd2';
GRANT registeredUser TO test_regisered;
CREATE USER test_loginService WITH PASSWORD 'secret_passwd3';
GRANT loginService TO test_loginService;
CREATE USER test_admin WITH PASSWORD 'secret_passwd4';
GRANT bankAdmin TO test_admin;