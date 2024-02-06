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

    insert into version(id, build, notes, type, path) VALUES ('D8327980-5634-4F19-A906-7178F7245A82', '1.0.0', 'notes', 1, '1.0.0.zip');
    insert into version(id, build, notes, type, path) VALUES ('9FF948C3-15B5-425E-BE77-CA98E451E4C8', '1.0.1', 'notes', 1, '1.0.1.zip');