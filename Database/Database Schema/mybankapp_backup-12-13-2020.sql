--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1
-- Dumped by pg_dump version 12.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: bankaccounts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bankaccounts (
    accountnum bigint NOT NULL,
    accountname character varying,
    accounttype character varying NOT NULL,
    amount double precision NOT NULL
);


ALTER TABLE public.bankaccounts OWNER TO postgres;

--
-- Name: bankaccounts_accountnum_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bankaccounts_accountnum_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.bankaccounts_accountnum_seq OWNER TO postgres;

--
-- Name: bankaccounts_accountnum_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bankaccounts_accountnum_seq OWNED BY public.bankaccounts.accountnum;


--
-- Name: transactions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transactions (
    accountid bigint NOT NULL,
    transactionid bigint NOT NULL,
    transactiondate timestamp without time zone NOT NULL,
    transactiontype character varying NOT NULL,
    transactionamount double precision NOT NULL
);


ALTER TABLE public.transactions OWNER TO postgres;

--
-- Name: transactions_transactionid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.transactions_transactionid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.transactions_transactionid_seq OWNER TO postgres;

--
-- Name: transactions_transactionid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.transactions_transactionid_seq OWNED BY public.transactions.transactionid;


--
-- Name: useraccounts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.useraccounts (
    accountid integer NOT NULL,
    username character varying(255),
    email character varying(255) NOT NULL,
    passwordhash character varying NOT NULL,
    lastname character varying(255) NOT NULL,
    firstname character varying(255) NOT NULL
);


ALTER TABLE public.useraccounts OWNER TO postgres;

--
-- Name: useraccounts_accountid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.useraccounts_accountid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.useraccounts_accountid_seq OWNER TO postgres;

--
-- Name: useraccounts_accountid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.useraccounts_accountid_seq OWNED BY public.useraccounts.accountid;


--
-- Name: userbankrelation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.userbankrelation (
    useraccountid integer NOT NULL,
    bankaccountid bigint NOT NULL
);


ALTER TABLE public.userbankrelation OWNER TO postgres;

--
-- Name: bankaccounts accountnum; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bankaccounts ALTER COLUMN accountnum SET DEFAULT nextval('public.bankaccounts_accountnum_seq'::regclass);


--
-- Name: transactions transactionid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions ALTER COLUMN transactionid SET DEFAULT nextval('public.transactions_transactionid_seq'::regclass);


--
-- Name: useraccounts accountid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.useraccounts ALTER COLUMN accountid SET DEFAULT nextval('public.useraccounts_accountid_seq'::regclass);


--
-- Data for Name: bankaccounts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.bankaccounts (accountnum, accountname, accounttype, amount) FROM stdin;
111111111116	TestAccount1	Checking	0
111111111117	TestAccount1	Checking	0
111111111118	TestAccount2	Checking	0
111111111119	TestAccount3	Savings	0
111111111120	TestAccount4	Checking	0
\.


--
-- Data for Name: transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transactions (accountid, transactionid, transactiondate, transactiontype, transactionamount) FROM stdin;
111111111119	1111	1999-01-08 04:05:06	Deposit	5000
\.


--
-- Data for Name: useraccounts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.useraccounts (accountid, username, email, passwordhash, lastname, firstname) FROM stdin;
111111111	test1	test@test.com	jksdhgjkhsfjkhdsakjf	testlast	testfirst
111111112	test2	test@test.com	jksdhgjkhsfjkhdsakjf	testlast	testfirst
111111113	testprofile	benjamin.gaull@wsu.edu	AQAAAAEAACcQAAAAEGjjKks2KHzPx2WSI11O0axoYyLBi2zTiA0DolgMHEpqiH5u5UQhseqRXmN4cKO2MA==	Gaull	Benjamin
111111114	teestprofile2	gaullfamily@msn.com	AQAAAAEAACcQAAAAEAZYChOdtiA+yJxnRvUApM72HnYhWroP1t/NRAn8DIC6Jiox7a2IgGCG5D2lMAniPw==	Gaull	DeAnn
\.


--
-- Data for Name: userbankrelation; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.userbankrelation (useraccountid, bankaccountid) FROM stdin;
111111113	111111111119
111111113	111111111120
\.


--
-- Name: bankaccounts_accountnum_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bankaccounts_accountnum_seq', 111111111120, true);


--
-- Name: transactions_transactionid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transactions_transactionid_seq', 1111, true);


--
-- Name: useraccounts_accountid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.useraccounts_accountid_seq', 111111114, true);


--
-- Name: bankaccounts bankaccounts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bankaccounts
    ADD CONSTRAINT bankaccounts_pkey PRIMARY KEY (accountnum);


--
-- Name: transactions transactions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_pkey PRIMARY KEY (accountid, transactionid);


--
-- Name: useraccounts useraccounts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.useraccounts
    ADD CONSTRAINT useraccounts_pkey PRIMARY KEY (accountid);


--
-- Name: userbankrelation userbankrelation_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userbankrelation
    ADD CONSTRAINT userbankrelation_pkey PRIMARY KEY (useraccountid, bankaccountid);


--
-- Name: transactions transactions_accountid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_accountid_fkey FOREIGN KEY (accountid) REFERENCES public.bankaccounts(accountnum) ON DELETE CASCADE;


--
-- Name: userbankrelation userbankrelation_bankaccountid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userbankrelation
    ADD CONSTRAINT userbankrelation_bankaccountid_fkey FOREIGN KEY (bankaccountid) REFERENCES public.bankaccounts(accountnum) ON DELETE CASCADE;


--
-- Name: userbankrelation userbankrelation_useraccountid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userbankrelation
    ADD CONSTRAINT userbankrelation_useraccountid_fkey FOREIGN KEY (useraccountid) REFERENCES public.useraccounts(accountid) ON DELETE CASCADE;


--
-- Name: TABLE bankaccounts; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT ON TABLE public.bankaccounts TO "authorizedUser";
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.bankaccounts TO "BankAdministrator";


--
-- Name: SEQUENCE bankaccounts_accountnum_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,USAGE ON SEQUENCE public.bankaccounts_accountnum_seq TO "authorizedUser";
GRANT SELECT ON SEQUENCE public.bankaccounts_accountnum_seq TO "BankAdministrator";


--
-- Name: TABLE transactions; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT ON TABLE public.transactions TO "authorizedUser";
GRANT SELECT,INSERT,DELETE ON TABLE public.transactions TO "BankAdministrator";


--
-- Name: SEQUENCE transactions_transactionid_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT ON SEQUENCE public.transactions_transactionid_seq TO "authorizedUser";


--
-- Name: TABLE useraccounts; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT ON TABLE public.useraccounts TO "LoginService";
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.useraccounts TO "BankAdministrator";


--
-- Name: SEQUENCE useraccounts_accountid_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,USAGE ON SEQUENCE public.useraccounts_accountid_seq TO "LoginService";
GRANT SELECT,USAGE ON SEQUENCE public.useraccounts_accountid_seq TO "authorizedUser";
GRANT ALL ON SEQUENCE public.useraccounts_accountid_seq TO "BankAdministrator";


--
-- Name: TABLE userbankrelation; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT ON TABLE public.userbankrelation TO "authorizedUser";
GRANT SELECT,INSERT,DELETE ON TABLE public.userbankrelation TO "BankAdministrator";


--
-- PostgreSQL database dump complete
--

