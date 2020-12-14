--revoke privileges from users
REVOKE unregisteredUser FROM test_unregistered;
REVOKE registeredUser FROM test_registered;
REVOKE loginService FROM test_loginService;
REVOKE bankAdmin FROM test_admin;