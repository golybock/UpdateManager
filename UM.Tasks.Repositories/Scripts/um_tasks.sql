create table worker
(
    id        uuid         not null
        primary key,
    role_id   integer      not null,
    full_name varchar(250) not null,
    login     varchar(250) not null
        unique,
    password  bytea        not null
);

create table task
(
    id           uuid                                   not null
        primary key,
    description  text                                   not null,
    worker_id    uuid
        references worker,
    priority     integer,
    status       integer,
    created_time timestamp with time zone default now() not null,
    start_time   timestamp with time zone,
    end_time     timestamp with time zone,
    client_email text,
    solution     text
);