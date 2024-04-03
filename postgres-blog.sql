--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-04-03 20:50:32

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
-- TOC entry 215 (class 1259 OID 16402)
-- Name: posts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.posts (
    id bigint NOT NULL,
    name text,
    description text
);


ALTER TABLE public.posts OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16417)
-- Name: posts_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.posts ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.posts_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 4831 (class 0 OID 16402)
-- Dependencies: 215
-- Data for Name: posts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.posts (id, name, description) FROM stdin;
1712167784818	adasd	asdasddd
1712167997957	ddddd	ddddddddddaaaaaadd
1712167612214	h	hghffghfgfghfgh aaaaa
1712167598918	fghfghfgh	fghfghfghaaaaa
1712078640534	ghjghj	ghjhj ghjghj ghjghj ghjghj ghjghj ghjghj ghjg ghjjdfgdf fghfghasdasd
1712168081594	asdasd	asdasd
\.


--
-- TOC entry 4838 (class 0 OID 0)
-- Dependencies: 216
-- Name: posts_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.posts_id_seq', 1, false);


-- Completed on 2024-04-03 20:50:33

--
-- PostgreSQL database dump complete
--

