create database um_files;

create table if not exists version
(
    id        uuid                                   not null
        primary key,
    build     text                                   not null,
    timestamp timestamp with time zone default now() not null,
    notes     text                                   not null,
    type      integer                                not null,
    path      text                                   not null
);

create table if not exists dependency
(
    id      uuid not null
        primary key,
    name    text not null,
    version text not null
);

create table if not exists version_dependencies
(
    id            serial
        primary key,
    version_id    uuid not null
        references version,
    dependency_id uuid not null
        references dependency
);